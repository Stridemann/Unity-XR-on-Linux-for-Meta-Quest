# Technical Documentation

This document explains every component of the Linux VR compatibility layer in detail: what each file does, why it exists, how it works, what pitfalls were encountered, and what alternative approaches were tried and failed.

## Table of Contents

- [The Problem](#the-problem)
- [Architecture Overview](#architecture-overview)
- [Component 1: Bionic-to-glibc Compatibility Shims](#component-1-bionic-to-glibc-compatibility-shims)
- [Component 2: OpenXR API Layer (strip_android)](#component-2-openxr-api-layer-strip_android)
- [Component 3: Vulkan Implicit Layer (force_timeline_semaphore)](#component-3-vulkan-implicit-layer-force_timeline_semaphore)
- [Component 4: Fake JVM Shim](#component-4-fake-jvm-shim)
- [Component 5: OpenXR Loader Path Patch](#component-5-openxr-loader-path-patch)
- [Component 6: Launch Script](#component-6-launch-script)
- [Unity Configuration](#unity-configuration)
- [System Packages](#system-packages)
- [Build Process](#build-process)
- [Approaches Tried and Failed](#approaches-tried-and-failed)
- [Order of Problems Solved](#order-of-problems-solved)

---

## The Problem

Unity's OpenXR plugin (`com.unity.xr.openxr`) does not officially support the Linux Editor for VR. However, the package does ship an Android x86_64 native binary at:

```
Packages/com.unity.xr.openxr/Runtime/android/x64/libUnityOpenXR.so
```

This binary is an ELF shared library compiled against Android's **bionic libc**, not glibc. When Unity on Linux tries to load it, the dynamic linker fails because:

1. **ABI incompatibility**: bionic exports symbols under a `LIBC` version tag (e.g., `memcpy@LIBC`), while glibc uses `GLIBC_2.x.x` tags (e.g., `memcpy@GLIBC_2.14`). The linker cannot resolve the version strings.
2. **Android-specific APIs**: The binary calls `JNI_OnLoad`, expects a `JavaVM`, and requests `XR_KHR_android_create_instance` — none of which exist on desktop Linux.
3. **Graphics API mismatch**: The binary's OpenGL path uses EGL (Android's graphics binding), which doesn't exist on desktop Linux X11/Wayland.
4. **Missing Vulkan features**: The binary doesn't enable `VK_KHR_timeline_semaphore`, which SteamVR requires for frame synchronization.

Each of these problems causes a different crash at a different stage of initialization. This project fixes all four.

---

## Architecture Overview

```
┌────────────────────────────────────────────────────────────────┐
│                        Unity Editor                            │
│                     (launched via script)                       │
├──────────────┬──────────────┬──────────────┬───────────────────┤
│  LD_PRELOAD  │ LD_LIBRARY_  │   OpenXR     │    Vulkan         │
│  fake_jvm.so │ PATH shims   │   API Layer  │    Implicit Layer │
├──────────────┼──────────────┼──────────────┼───────────────────┤
│ Intercepts   │ libc.so      │ Strips       │ Injects           │
│ dlopen() to  │ libm.so      │ XR_KHR_      │ VK_KHR_timeline_  │
│ call         │ libdl.so     │ android_     │ semaphore into    │
│ JNI_OnLoad   │ (LIBC tags)  │ create_      │ vkCreateDevice    │
│ with stub VM │              │ instance     │                   │
└──────┬───────┴──────┬───────┴──────┬───────┴───────┬───────────┘
       │              │              │               │
       ▼              ▼              ▼               ▼
  libUnityOpenXR.so  glibc       SteamVR          Vulkan ICD
  (Android binary)              (OpenXR Runtime)  (GPU driver)
```

---

## Component 1: Bionic-to-glibc Compatibility Shims

### Files
| File | Purpose |
|---|---|
| `NativeFix/compat_libc.c` | libc shim — 150+ functions (stdio, string, memory, pthread, locale, wchar, time, etc.) |
| `NativeFix/compat_libm.c` | libm shim — math functions (tanf, roundf) |
| `NativeFix/compat_libdl.c` | libdl shim — dynamic linking functions (dlopen, dlsym, dlclose, dladdr, dl_iterate_phdr) |
| `NativeFix/version_libc.map` | Linker version script defining the `LIBC` version tag |
| `NativeFix/version_libm.map` | Same for libm |
| `NativeFix/version_libdl.map` | Same for libdl |
| `NativeFix/build_compat.sh` | Build script for all three shims |

### How It Works

Android's bionic libc and Linux's glibc export the same C standard library functions, but with different **ELF symbol versions**. For example:

- bionic: `memcpy@LIBC`
- glibc: `memcpy@GLIBC_2.14`

When `libUnityOpenXR.so` is loaded, the dynamic linker (`ld-linux.so`) looks for `memcpy` with version string `LIBC`. Since glibc doesn't define that version, loading fails.

The shims solve this by:
1. Creating wrapper functions that call the real glibc functions
2. Using GCC's `.symver` directive to tag each wrapper with the `LIBC` version:
   ```c
   void *compat_memcpy(void *d, const void *s, size_t n) { return memcpy(d, s, n); }
   __asm__(".symver compat_memcpy, memcpy@LIBC");
   ```
3. Using a linker version script (`version_libc.map`) that simply declares the `LIBC` version tag:
   ```
   LIBC {
   };
   ```
4. The shims are compiled as `libc.so`, `libm.so`, `libdl.so` and placed on `LD_LIBRARY_PATH` ahead of the system libraries

### Key Implementation Details

#### `__sF` / stdio Translation

Bionic defines `FILE __sF[3]` where `__sF[0]=stdin`, `__sF[1]=stdout`, `__sF[2]=stderr`. glibc uses separate `FILE *stdin`, `*stdout`, `*stderr` pointers. The shim provides a fake `__sF` array and a `translate_stream()` function that intercepts all stdio calls (fprintf, fwrite, fflush, etc.) and maps `__sF` references to real glibc streams:

```c
static inline FILE *translate_stream(FILE *f) {
    char *p = (char *)f;
    char *base = (char *)__sF_impl;
    if (p >= base && p < base + sizeof(__sF_impl)) {
        int idx = (p - base) / sizeof(bionic_FILE);
        if (idx == 0) return stdin;
        if (idx == 1) return stdout;
        if (idx == 2) return stderr;
    }
    return f;
}
```

#### `__errno` vs `__errno_location`

bionic: `int *__errno(void)` — glibc: `int *__errno_location(void)`. Simple redirect.

#### Variadic Functions

Functions like `snprintf`, `sscanf`, `syscall`, and `syslog` are variadic. They cannot be wrapped with a generic macro — each needs a manually written wrapper that unpacks arguments with `va_list` and calls the `v`-prefixed variant (e.g., `snprintf` → `vsnprintf`).

#### Fortify-source (`__*_chk`) Functions

glibc's fortify-source wrappers (`__strlen_chk`, `__memmove_chk`, `__vsnprintf_chk`) are also present in bionic with `LIBC` versioning. These are forwarded directly.

### Pitfalls Encountered

1. **`memchr` macro conflict**: On some systems, `<string.h>` defines `memchr` as a macro. This clashed with defining `compat_memchr`. Fixed by `#undef memchr` before the wrapper and using an `extern` declaration.

2. **Generic wrapper macro failures**: Initially attempted a macro like `#define WRAP(name, ret, ...) ret compat_##name(__VA_ARGS__) { return name(__VA_ARGS__); }` but this fails for:
   - Variadic functions (can't forward `...`)
   - Functions with function-pointer arguments (macro can't parse the signature)
   - Functions where the name is also a macro (e.g., `memchr`, `pthread_equal`)

   **Resolution**: Every problematic symbol was written out manually.

3. **Missing symbols at runtime**: The initial set of wrapped symbols was incomplete. Each missing symbol caused a new `DllNotFoundException`. The set was expanded iteratively by running the binary and checking which `LIBC`-versioned symbols it needed (via `readelf -V`).

---

## Component 2: OpenXR API Layer (strip_android)

### Files
| File | Purpose |
|---|---|
| `NativeFix/openxr_layer/strip_android_layer.c` | OpenXR API layer source |
| `NativeFix/openxr_layer/build_layer.sh` | Build script + manifest installation |

### How It Works

The Android `libUnityOpenXR.so` binary calls `xrCreateInstance` with `XR_KHR_android_create_instance` in its enabled extensions list and an `XrInstanceCreateInfoAndroidKHR` struct chained via the `next` pointer. SteamVR on Linux doesn't support this extension, so `xrCreateInstance` fails.

This OpenXR API layer intercepts the instance creation call and:
1. **Filters** `XR_KHR_android_create_instance` from `enabledExtensionNames`
2. **Removes** `XrInstanceCreateInfoAndroidKHR` (structure type `1000008000`) from the `pNext` chain
3. **Forwards** the cleaned-up request to the next layer/runtime

### Key Functions

#### `xrNegotiateLoaderApiLayerInterface`
Entry point called by the OpenXR loader during initialization. Validates the loader's version info and registers the layer's `getInstanceProcAddr` and `createApiLayerInstance` callbacks.

#### `strip_android_xrCreateApiLayerInstance`
The core interception function. It:
- Shallow-copies the `XrInstanceCreateInfo` struct
- Walks the `enabledExtensionNames` array, copying all names except `XR_KHR_android_create_instance` into a filtered array
- Walks the `next` chain, unlinking any struct with type `XR_TYPE_INSTANCE_CREATE_INFO_ANDROID_KHR_VALUE` (1000008000)
- Advances the layer chain (`nextInfo = layerInfo->nextInfo->next`) and calls the next layer's `nextCreateApiLayerInstance`

#### `strip_android_xrGetInstanceProcAddr`
Pass-through — forwards all `xrGetInstanceProcAddr` calls to the next layer/runtime.

### Layer Manifest

The build script installs a JSON manifest to `~/.local/share/openxr/1/api_layers/implicit.d/`:

```json
{
    "file_format_version": "1.0.0",
    "api_layer": {
        "name": "XR_APILAYER_NOVENDOR_strip_android",
        "library_path": "/absolute/path/to/libXrApiLayer_strip_android.so",
        "api_version": "1.0",
        "implementation_version": "1",
        "description": "Strips XR_KHR_android_create_instance...",
        "enable_environment": "STRIP_ANDROID_XR_LAYER",
        "disable_environment": "DISABLE_STRIP_ANDROID_XR_LAYER"
    }
}
```

The layer is **implicit** (always loaded if enabled) and activated by setting `STRIP_ANDROID_XR_LAYER=1`.

### Pitfalls Encountered

1. **Missing `disable_environment`**: The OpenXR loader requires implicit layers to specify both `enable_environment` AND `disable_environment`. Initially only `enable_environment` was set, causing the loader to reject the layer with: `Implicit layer ... is missing "disable_environment"`.

2. **Layer chain advancement**: The layer must advance `nextInfo` to `layerInfo->nextInfo->next` before calling `nextCreateApiLayerInstance`. Failing to do so causes the same layer to be called again in an infinite loop.

---

## Component 3: Vulkan Implicit Layer (force_timeline_semaphore)

### Files
| File | Purpose |
|---|---|
| `NativeFix/vulkan_layer/force_timeline_sem.c` | Vulkan implicit layer source |
| `NativeFix/vulkan_layer/build_vk_layer.sh` | Build script + manifest installation |

### How It Works

SteamVR's OpenXR runtime uses Vulkan timeline semaphores (`VK_KHR_timeline_semaphore`) for frame synchronization. Unity doesn't enable this extension/feature when creating its Vulkan device. When SteamVR tries to create a timeline semaphore, the Vulkan validation layer reports:

```
VULKAN: VALIDATION ERROR: vkCreateSemaphore(): timelineSemaphore feature was not enabled.
```

…and then the process crashes (SIGSEGV).

This Vulkan implicit layer intercepts two Vulkan calls:

#### `hook_vkCreateInstance`
- Finds the layer link chain in `pCreateInfo->pNext` (struct type `VK_STRUCTURE_TYPE_LOADER_INSTANCE_CREATE_INFO` with `function == VK_LAYER_LINK_INFO`)
- Advances the chain by one link (consumes this layer's entry)
- Resolves `vkCreateInstance` from the next layer and calls it
- Stores the next layer's `vkGetInstanceProcAddr`, `vkDestroyInstance`, and `vkCreateDevice` function pointers for later use

#### `hook_vkCreateDevice` (the core fix)
1. **Finds the device layer link chain** and advances it
2. **Adds `VK_KHR_timeline_semaphore`** to `ppEnabledExtensionNames` if not already present
3. **Enables the `timelineSemaphore` feature**: walks the `pNext` chain looking for `VkPhysicalDeviceTimelineSemaphoreFeatures`. If found, sets `timelineSemaphore = VK_TRUE`. If not found, prepends a new one to the chain.
4. Calls the next layer's `vkCreateDevice` with the patched info

#### `vkNegotiateLoaderLayerInterfaceVersion`
Entry point for the Vulkan loader. Registers the layer's `pfnGetInstanceProcAddr` and `pfnGetDeviceProcAddr`.

### Layer Manifest

Installed to `~/.local/share/vulkan/implicit_layer.d/`:

```json
{
    "file_format_version": "1.0.0",
    "layer": {
        "name": "VK_LAYER_NOVENDOR_force_timeline_sem",
        "type": "GLOBAL",
        "library_path": "/absolute/path/to/libVkLayer_force_timeline_sem.so",
        "api_version": "1.3.0",
        "implementation_version": "1",
        "description": "Forces VK_KHR_timeline_semaphore...",
        "enable_environment": { "ENABLE_FORCE_TIMELINE_SEM": "1" },
        "disable_environment": { "DISABLE_FORCE_TIMELINE_SEM": "1" }
    }
}
```

### Why Not Just Use Vulkan 1.2+?

Timeline semaphores are core in Vulkan 1.2+, but the **feature still needs to be explicitly enabled** in `VkDeviceCreateInfo`. Unity creates its Vulkan device without the feature struct, so even on Vulkan 1.3 hardware, the feature is not active.

### Pitfalls Encountered

1. **Layer dispatch chain mechanics**: Vulkan layers use a linked list in the `pNext` chain for dispatch. Each layer must advance the chain before calling the next layer. Getting this wrong causes crashes or infinite recursion. The `find_chain_info()` helper walks `pNext` looking for the right struct type and `function` field.

2. **Resolving `vkCreateDevice` from the right source**: The device-creation layer chain provides `pfnNextGetInstanceProcAddr`, not `pfnNextGetDeviceProcAddr` for resolving `vkCreateDevice`. The function must be resolved via `nextGIPA(NULL, "vkCreateDevice")`. If the chain doesn't provide a valid link (some loaders skip it), the fallback is `g_nextCreateDevice` saved from instance creation.

3. **Multiple crashes during development**: The Vulkan layer was the hardest component to get right. Several iterations crashed because:
   - The chain was not advanced properly (double-dispatch to the same layer)
   - `vkCreateDevice` was resolved from the wrong function pointer
   - The `VkPhysicalDeviceTimelineSemaphoreFeatures` struct was stack-allocated but went out of scope before the driver read it (fixed by keeping it alive in the function scope)

4. **Vulkan header dependency**: To avoid pulling in the full Vulkan SDK headers (which may not be installed), the layer defines all needed Vulkan types inline. This is brittle but portable — it only depends on `stdint.h`, `stdlib.h`, `string.h`, `stdio.h`.

---

## Component 4: Fake JVM Shim

### Files
| File | Purpose |
|---|---|
| `NativeFix/fake_jvm.c` | `LD_PRELOAD` shim that provides a fake JavaVM |

### How It Works

The Android `libUnityOpenXR.so` has a `JNI_OnLoad` entry point. On Android, the Java runtime calls this automatically when loading the library via `System.loadLibrary()`. On Linux desktop, `dlopen()` is used instead, and `JNI_OnLoad` is never called. This leaves the plugin's internal `JavaVM*` pointer as NULL.

Later, when the plugin's graphics thread starts, it calls `JavaVM::GetEnv()` to get a JNI environment — on a NULL pointer. Crash.

The shim intercepts `dlopen()` via `LD_PRELOAD`:

```c
void *dlopen(const char *filename, int flags)
{
    void *handle = real_dlopen(filename, flags);
    if (handle && filename && strstr(filename, "libUnityOpenXR.so")) {
        init_fake_jni();
        PFN_JNI_OnLoad onLoad = dlsym(handle, "JNI_OnLoad");
        if (onLoad) onLoad(&g_fake_java_vm, NULL);
    }
    return handle;
}
```

### Fake JVM Structure

The JNI architecture is pointer-to-pointer-to-vtable:

```
JavaVM* → pointer → JNIInvokeInterface (vtable)
                     ├── DestroyJavaVM
                     ├── AttachCurrentThread
                     ├── DetachCurrentThread
                     ├── GetEnv          ← returns fake JNIEnv
                     └── AttachCurrentThreadAsDaemon
```

Each function in the vtable is a stub that returns `JNI_OK`:

- `GetEnv` returns a pointer to a fake `JNIEnv` with 256 no-op function pointers
- `AttachCurrentThread` does the same
- `DestroyJavaVM` and `DetachCurrentThread` are no-ops

The shim also exports `JNI_GetCreatedJavaVMs()` in case anything queries for existing JVMs.

### Pitfalls Encountered

1. **JNI pointer indirection**: JNI uses triple indirection — `JavaVM` is a `pointer-to-pointer-to-vtable`. Getting the struct layout wrong causes the plugin to dereference into garbage memory. The key insight: `JavaVM *vm` means `*vm` is the vtable pointer, and `(*vm)->GetEnv(vm, ...)` is the actual call.

2. **JNIEnv stub size**: The `JNINativeInterface` (JNIEnv vtable) has ~230 function pointers. Rather than defining all of them, we use an array of 256 function pointers all pointing to a single `jni_stub()` that returns NULL. This covers any JNI function the plugin might call without crashing.

3. **Thread safety**: `init_fake_jni()` uses a static `initialized` flag. This is safe because `dlopen()` for `libUnityOpenXR.so` happens once on the main thread before any graphics threads start.

---

## Component 5: OpenXR Loader Path Patch

### Files
| File | Purpose |
|---|---|
| `Packages/com.unity.xr.openxr/Runtime/OpenXRLoader.cs` | Patched Unity source |

### How It Works

Unity's `LoadOpenXRSymbols()` method resolves the path to the OpenXR loader library. On Linux, the default logic falls through to a path like `RuntimeLoaders/universalwindows/arm64/openxr_loader.dll` — a Windows UWP binary that obviously can't load on Linux.

The fix adds a `#elif UNITY_EDITOR_LINUX` block:

```csharp
#elif UNITY_EDITOR_LINUX
    loaderPath = "/usr/lib/libopenxr_loader.so";
```

This points Unity to the system-installed OpenXR loader (from the `openxr` package), which knows how to find and load SteamVR's OpenXR runtime.

### Why This Approach?

Unity's native `Plugin_LoadLibrary` uses `dladdr`-relative path construction when the library name has no `/`. Using a full absolute path bypasses that logic entirely, which is the safest approach.

---

## Component 6: Launch Script

### File
`launch_unity_vr.sh`

### What It Does (in order)

1. **Builds compat shims** if not already built (`NativeFix/build_compat.sh`)
2. **Sets `LD_LIBRARY_PATH`** to include `NativeFix/` so bionic-versioned symbols are resolved from our shims
3. **Sets `XR_LOADER_DEBUG=all`** for verbose OpenXR loader logging (diagnostic)
4. **Enables Vulkan validation layers** (`VK_INSTANCE_LAYERS`, `VK_LAYER_ENABLES`) for crash diagnostics
5. **Sets `ENABLE_FORCE_TIMELINE_SEM=1`** to activate the Vulkan implicit layer
6. **Builds the Vulkan layer** if not already built
7. **Sets `STRIP_ANDROID_XR_LAYER=1`** to activate the OpenXR API layer
8. **Builds the fake JVM shim** if not already built
9. **Sets `LD_PRELOAD`** to include `libfake_jvm.so`
10. **Builds the OpenXR layer** if not already built
11. **Sets HiDPI scaling** (`GDK_SCALE=2`, `GDK_DPI_SCALE=0.5`)
12. **Launches Unity** with `-force-vulkan` flag

### Why `-force-vulkan`?

The Unity Editor defaults to OpenGL Core on Linux. The Android OpenXR binary's OpenGL path uses EGL (`eglGetDisplay`, `eglCreateContext`), which is Android's graphics binding — it doesn't work on desktop Linux X11/Wayland. The Vulkan path uses `XrGraphicsBindingVulkanKHR`, which is platform-independent.

---

## Unity Configuration

### OpenXR Package Settings (`Assets/XR/Settings/OpenXRPackageSettings.asset`)

**Critical change**: The **Oculus Touch Controller Profile** must be enabled for the Standalone build target:

```yaml
m_Name: OculusTouchControllerProfile Standalone
m_enabled: 1    # Was 0 — must be 1
```

SteamVR (via ALVR) emulates Oculus Touch controllers. The "Meta Quest Touch Plus" profile (`XR_META_touch_controller_plus`) is **not** supported by SteamVR — only the base Oculus Touch profile is.

### OpenXR Editor Settings (`Assets/XR/Settings/OpenXR Editor Settings.asset`)

```yaml
m_vulkanAdditionalGraphicsQueue: 1    # Enable additional Vulkan queue for XR
m_vulkanOffscreenSwapchainNoMainDisplay: 1
```

### XR Plug-in Management

OpenXR must be enabled for the Standalone platform. The Oculus XR plugin (`com.unity.xr.oculus`) is also installed but not strictly required for this approach.

---

## System Packages

### Arch Linux

```bash
# Build tools
sudo pacman -S base-devel

# OpenXR
sudo pacman -S openxr

# Vulkan (AMD example — use nvidia for NVIDIA GPUs)
sudo pacman -S vulkan-radeon vulkan-validation-layers

# SteamVR: Install via Steam
# ALVR: Install from AUR or GitHub releases
```

### Ubuntu/Debian

```bash
sudo apt install build-essential libopenxr-loader1 libopenxr-dev \
    mesa-vulkan-drivers vulkan-validationlayers
```

### What Each Package Provides

| Package | Provides | Needed For |
|---|---|---|
| `base-devel` / `build-essential` | GCC, make, binutils | Compiling all native shims |
| `openxr` / `libopenxr-loader1` | `/usr/lib/libopenxr_loader.so` | Unity loading the OpenXR runtime |
| `vulkan-*-drivers` | Vulkan ICD (GPU driver) | Vulkan rendering |
| `vulkan-validation-layers` | `VK_LAYER_KHRONOS_validation` | Crash diagnostics (optional in production) |

---

## Build Process

### Do Other Developers Need to Build?

**No manual building required.** The `launch_unity_vr.sh` script automatically detects missing `.so` files and builds them on first run. Developers only need GCC installed.

### What Gets Built

| Component | Build Command | Output | Installed To |
|---|---|---|---|
| Compat shims | `build_compat.sh` | `NativeFix/libc.so`, `libm.so`, `libdl.so` | Stays in `NativeFix/` |
| Fake JVM | Inline gcc in launch script | `NativeFix/libfake_jvm.so` | Stays in `NativeFix/` |
| OpenXR layer | `build_layer.sh` | `NativeFix/openxr_layer/libXrApiLayer_strip_android.so` | Manifest to `~/.local/share/openxr/1/api_layers/implicit.d/` |
| Vulkan layer | `build_vk_layer.sh` | `NativeFix/vulkan_layer/libVkLayer_force_timeline_sem.so` | Manifest to `~/.local/share/vulkan/implicit_layer.d/` |

### Layer Manifest Installation

Both the OpenXR and Vulkan layers install JSON manifest files to the user's home directory. These manifests point to the absolute path of the `.so` files in the project directory.

**Important**: If you move the project directory, you need to rebuild the layers (delete the `.so` files and re-run the launch script) so the manifests point to the correct paths.

### Build Flags

All components are compiled with:
- `-shared -fPIC` — position-independent shared libraries
- `-O2` — optimization (important for wrapper performance)
- `-Wall` — warnings enabled
- Compat shims additionally use `-Wl,--version-script=version_*.map` for the `LIBC` version tag and link against `-lpthread -lrt -lm -ldl` respectively

---

## Approaches Tried and Failed

### 1. Using the Linux x64 OpenXR Plugin Binary

Unity doesn't ship a Linux x64 `libUnityOpenXR.so`. The only native binaries available are:
- `android/x64/libUnityOpenXR.so` (Android x86_64 — what we use)
- `android/arm64/libUnityOpenXR.so` (Android ARM64)
- `windows/x64/UnityOpenXR.dll` (Windows)
- `universalwindows/arm64/openxr_loader.dll` (UWP ARM64)

There is no `linux/x64/` variant. The Android x86_64 binary is the closest to running on Linux desktop (same ISA, just different libc).

### 2. Forcing OpenGL Core Instead of Vulkan

Initially set `UNITY_FORCE_OPENGL_CORE=1` thinking it would be simpler. The Android binary does support OpenGL, but via **EGL** (Android's graphics binding layer). On desktop Linux, OpenGL uses **GLX** (X11) or **EGL on Wayland**. The binary called `eglGetDisplay()` and `eglCreateContext()` which returned NULL on X11, causing `xrCreateSession` to fail with `XR_ERROR_GRAPHICS_DEVICE_INVALID`.

**Resolution**: Switched to Vulkan with `-force-vulkan`. The Vulkan path uses `XrGraphicsBindingVulkanKHR` which is platform-independent.

### 3. Using an Explicit OpenXR API Layer (vs. Implicit)

Initially tried to register the strip_android layer as an **explicit** API layer (no `enable_environment`). The OpenXR loader on some configurations didn't load explicit API layers reliably via environment variables. Switched to an **implicit** layer with `enable_environment` / `disable_environment`, which is the standard mechanism for layers that should always be active when an environment variable is set.

### 4. Patching the Binary Directly

Considered hex-patching `libUnityOpenXR.so` to remove the `XR_KHR_android_create_instance` extension string. This would be fragile (breaks on any Unity version update) and doesn't solve the JNI dependency or the Vulkan timeline semaphore issue. The API layer approach is cleaner and version-independent.

### 5. Providing a Real JVM

Considered bundling a real JVM (`libjvm.so`) and letting `JNI_OnLoad` run against it. This would add a ~200MB dependency and still wouldn't work because the plugin expects an Android-specific `JNIEnv` with `android/app/Activity` class references. The fake stub approach is simpler and sufficient — the plugin only needs `GetEnv` to return non-NULL.

---

## Order of Problems Solved

This is the chronological order in which issues were discovered and fixed. Each fix revealed the next crash:

1. **`DllNotFoundException: version 'LIBC' not found`**
   → Created bionic-to-glibc compatibility shims

2. **`Failed to load openxr runtime loader`**
   → Patched `OpenXRLoader.cs` to use `/usr/lib/libopenxr_loader.so`

3. **`xrCreateInstance` fails (Android extension not supported)**
   → Created OpenXR API layer to strip `XR_KHR_android_create_instance`

4. **SIGSEGV: `vkCreateSemaphore(): timelineSemaphore feature was not enabled`**
   → Created Vulkan implicit layer to force-enable `VK_KHR_timeline_semaphore`

5. **SIGSEGV: `JavaVM::GetEnv(this=0x0)` — NULL JavaVM pointer**
   → Created `LD_PRELOAD` fake JVM shim

6. **Controllers not working (no input, no hand models visible)**
   → Enabled Oculus Touch Controller Profile in OpenXR settings (was disabled; "Meta Quest Touch Plus" was enabled instead, which SteamVR doesn't support)

---

## APK Builds for Meta Quest

### Why APK Builds Don't Need NativeFix

The entire `NativeFix/` layer exists only for **editor play mode on Linux**. When building an APK:
- The APK runs on Android natively — bionic libc is the real libc
- The Quest has its own OpenXR runtime — no SteamVR, no extension stripping needed
- Android has a real JVM (ART) — `JNI_OnLoad` is called normally by the system
- The Quest's Vulkan driver handles timeline semaphores natively
- The `OpenXRLoader.cs` patch is guarded by `#elif UNITY_EDITOR_LINUX` — doesn't affect Android builds

### Android NDK Broken Symlinks (Linux Host)

**Problem**: Unity's bundled Android NDK (r27c) has broken symbolic links when installed on Linux. The symlinks in `NDK/toolchains/llvm/prebuilt/linux-x86_64/bin/` (such as `clang`, `clang++`, `ld`, `llvm-strip`, etc.) are absolute symlinks pointing into a non-existent `android-ndk-r27c/` subdirectory.

**Symptom**: APK build fails with:
```
/bin/sh: line 1: .../clang++: No such file or directory
```

**Root cause**: The NDK packaging creates symlinks like:
```
clang++ → /path/to/NDK/android-ndk-r27c/toolchains/.../bin/clang
```
But the actual installed layout doesn't have the `android-ndk-r27c/` subdirectory — the binaries are directly in `NDK/toolchains/.../bin/`.

**Fix**: Recreate 14 broken symlinks as relative links to the actual binaries in the same directory. The real binaries are `clang-18`, `lld`, `llvm-ar`, `llvm-symbolizer`, `llvm-objcopy`, `llvm-readobj`, and `llvm-rc`. All other tools are symlinks to these. See `README.md` for the exact commands.

**Pitfall**: This fix needs to be re-applied if Unity Hub updates the Android Build Support module, as it will re-extract the NDK with the same broken symlinks.

### Meta Quest Support Feature (Black Screen Fix)

**Problem**: After successfully building and deploying an APK to Quest 3, the app starts but shows only a black window in 2D mode — it never enters fullscreen VR.

**Root cause**: The **Meta Quest Support** feature (`MetaQuestFeature Android`) was disabled (`m_enabled: 0`) in `OpenXRPackageSettings.asset`. This feature is critical for Quest APK builds because it:
1. Adds `XR_OCULUS_android_initialize_loader` to the requested OpenXR extensions — the Quest's OpenXR runtime requires this for initialization
2. Configures the Android manifest with Quest-specific entries (VR intent filters, device categories)
3. Declares target devices (Quest, Quest 2, Quest Pro, Quest 3, Quest 3S)

Without this feature, the APK starts as a regular Android app. The OpenXR runtime doesn't initialize, so no VR session is created.

**Fix**: Enable `MetaQuestFeature Android` in `OpenXRPackageSettings.asset` (`m_enabled: 1`). Also enable controller profiles for Android:
- **Oculus Touch Controller Profile** (Android) — base controller support
- **Meta Quest Touch Plus Controller Profile** (Android) — Quest 3's native controllers

These are separate settings from the Standalone (editor) profiles.

### ALVR + UFW Firewall Pitfall

**Problem**: When using UFW (Uncomplicated Firewall) on Linux, ALVR's automatic firewall configuration creates rules with **multiport** entries (multiple ports in a single rule). These multiport rules don't work correctly with UFW's iptables backend on some configurations.

**Symptom**: Quest and PC ALVR client can't discover or connect to each other, despite being on the same WiFi network and ALVR being properly installed on both devices.

**Fix**: Either:
1. **Restart the PC** — this clears the problematic iptables rules; ALVR will recreate them correctly on next launch
2. **Add ports manually** — add individual UFW rules for each ALVR port instead of relying on automatic configuration
