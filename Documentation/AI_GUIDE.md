# AI Agent Guide

This document is written for AI coding assistants (Claude, GPT, Copilot, etc.) helping users debug or extend this project. It provides the essential context you need to understand the system without reading every source file.

## Quick Context

This project makes Unity's OpenXR VR plugin work on desktop Linux. The plugin ships an **Android x86_64 binary** (`libUnityOpenXR.so`) that was compiled against Android's bionic libc. Four separate native shims/layers bridge the gap to desktop Linux's glibc + SteamVR.

## The Five Fixes (in initialization order)

| # | Problem | Fix | Mechanism |
|---|---|---|---|
| 1 | bionic `LIBC`-versioned symbols vs glibc `GLIBC_2.x.x` | Compat shim libraries in `NativeFix/` | `LD_LIBRARY_PATH` |
| 2 | Unity loads wrong OpenXR loader (.dll) | Patched `OpenXRLoader.cs` | Hardcoded path to `/usr/lib/libopenxr_loader.so` |
| 3 | `XR_KHR_android_create_instance` unsupported on Linux | OpenXR API layer strips it | `STRIP_ANDROID_XR_LAYER=1` env var |
| 4 | `VK_KHR_timeline_semaphore` not enabled by Unity | Vulkan implicit layer injects it | `ENABLE_FORCE_TIMELINE_SEM=1` env var |
| 5 | `JNI_OnLoad` never called → NULL JavaVM | `LD_PRELOAD` fake JVM shim | Intercepts `dlopen()` |

## Debugging Decision Tree

If the user reports a crash or error, use this to quickly identify which component is involved:

```
Error contains "version 'LIBC' not found"?
  → Compat shims not loaded. Check LD_LIBRARY_PATH includes NativeFix/.

Error contains "Failed to load openxr runtime loader"?
  → OpenXRLoader.cs patch missing or wrong path. Check UNITY_EDITOR_LINUX block.

Error contains "xrCreateInstance" failure?
  → OpenXR layer not active. Check STRIP_ANDROID_XR_LAYER=1 and layer .so exists.
  → Also check: is SteamVR running? Is the OpenXR runtime configured?
  → Check: ~/.config/openxr/1/active_runtime.json should point to SteamVR.

Error contains "timelineSemaphore" or "vkCreateSemaphore"?
  → Vulkan layer not active. Check ENABLE_FORCE_TIMELINE_SEM=1 and layer .so exists.

SIGSEGV in libUnityOpenXR.so with "JavaVM::GetEnv(this=0x0)"?
  → Fake JVM not loaded. Check LD_PRELOAD includes libfake_jvm.so.

No crash but nothing appears in headset?
  → Check: Is ALVR connected? Is SteamVR running?
  → Check: Was Unity launched with -force-vulkan?
  → Check: Is the scene using an XR Origin/XR Rig?

Controllers don't work (editor play mode)?
  → Check OpenXR interaction profiles in OpenXRPackageSettings.asset.
  → "Oculus Touch Controller Profile" STANDALONE must be enabled (m_enabled: 1).
  → "Meta Quest Touch Plus" is NOT supported by SteamVR/ALVR.

APK build fails with "clang++: No such file or directory"?
  → Unity's NDK has broken symlinks on Linux.
  → Fix: recreate symlinks in NDK/toolchains/llvm/prebuilt/linux-x86_64/bin/
  → See README.md "Known Issue: NDK Broken Symlinks on Linux" for exact commands.

APK runs on Quest but shows black window (not fullscreen VR)?
  → "Meta Quest Support" feature is disabled for Android builds.
  → Enable MetaQuestFeature Android in OpenXRPackageSettings.asset (m_enabled: 1).
  → Also enable controller profiles for Android (separate from Standalone).

ALVR can't connect Quest to PC?
  → If UFW is active, its multiport rules may be broken.
  → Restart PC or add ALVR ports as individual UFW rules.
```

## File Locations You'll Need

| What | Path |
|---|---|
| Launch script | `launch_unity_vr.sh` |
| All native C code | `NativeFix/` |
| Bionic compat shims | `NativeFix/compat_libc.c`, `compat_libm.c`, `compat_libdl.c` |
| Linker version maps | `NativeFix/version_libc.map`, `version_libm.map`, `version_libdl.map` |
| Build script (shims) | `NativeFix/build_compat.sh` |
| Fake JVM shim | `NativeFix/fake_jvm.c` |
| OpenXR API layer | `NativeFix/openxr_layer/strip_android_layer.c` |
| OpenXR layer build | `NativeFix/openxr_layer/build_layer.sh` |
| Vulkan implicit layer | `NativeFix/vulkan_layer/force_timeline_sem.c` |
| Vulkan layer build | `NativeFix/vulkan_layer/build_vk_layer.sh` |
| Patched OpenXR loader | `Packages/com.unity.xr.openxr/Runtime/OpenXRLoader.cs` |
| OpenXR settings | `Assets/XR/Settings/OpenXRPackageSettings.asset` |
| Editor XR settings | `Assets/XR/Settings/OpenXR Editor Settings.asset` |
| Unity packages | `Packages/manifest.json` |

## Common Tasks

### Adding a New Bionic Symbol

If a Unity update adds new bionic-versioned symbol requirements:

1. Run `readelf -V NativeFix/libc.so` to see current symbols
2. Run the binary and check the error for which `LIBC`-versioned symbol is missing
3. Add a wrapper in the appropriate `compat_*.c` file:
   ```c
   return_type compat_FUNCNAME(args) { return FUNCNAME(args); }
   __asm__(".symver compat_FUNCNAME, FUNCNAME@LIBC");
   ```
4. For variadic functions, use `va_list` and call the `v`-prefixed variant
5. Rebuild: `rm NativeFix/libc.so && ./launch_unity_vr.sh`

### Updating for a New Unity Version

The `OpenXRLoader.cs` patch is in the Unity package cache. If `com.unity.xr.openxr` is updated:
1. Re-apply the `UNITY_EDITOR_LINUX` block in `LoadOpenXRSymbols()`
2. Check if new extensions are requested (may need OpenXR layer updates)
3. Check if the Android binary's symbol requirements changed (may need new compat wrappers)

### Moving the Project Directory

The OpenXR and Vulkan layer manifests contain absolute paths. After moving:
```bash
rm NativeFix/openxr_layer/libXrApiLayer_strip_android.so
rm NativeFix/vulkan_layer/libVkLayer_force_timeline_sem.so
rm NativeFix/libfake_jvm.so
./launch_unity_vr.sh   # Rebuilds everything with new paths
```

## Key Architectural Decisions

1. **Why Android x86_64 binary?** — It's the only `libUnityOpenXR.so` that runs on x86_64 Linux. No Linux-native version exists.

2. **Why Vulkan, not OpenGL?** — The Android binary's OpenGL path uses EGL (Android-only). Vulkan's `XrGraphicsBindingVulkanKHR` is platform-independent.

3. **Why LD_PRELOAD for JVM, not a layer?** — `JNI_OnLoad` must be called at `dlopen` time, before any OpenXR/Vulkan initialization. `LD_PRELOAD` is the only mechanism that intercepts `dlopen()` early enough.

4. **Why implicit layers, not explicit?** — Implicit layers with `enable_environment` are the standard mechanism for optional layers activated by env vars. More reliable across different OpenXR/Vulkan loader versions.

5. **Why inline Vulkan type definitions?** — Avoids requiring the Vulkan SDK headers to be installed. The layer only needs a handful of struct definitions and function pointer types.

## APK Builds vs Editor Play Mode

The `NativeFix/` shims are **only for editor play mode**. APK builds run natively on Android and don't need any of them. Key differences:

| Setting | Editor (Standalone) | APK (Android) |
|---|---|---|
| Meta Quest Support | Not needed | **Must be enabled** |
| Oculus Touch Controller | Needed for SteamVR | Needed |
| Meta Quest Touch Plus | Not supported by SteamVR | Supported natively |
| NativeFix shims | Required | Not used |
| OpenXRLoader.cs patch | Required | Not affected (guarded by `#elif UNITY_EDITOR_LINUX`) |
| `-force-vulkan` flag | Required | Not needed (Quest uses Vulkan natively) |

**Important**: `OpenXRPackageSettings.asset` has **separate settings per build target**. Features named `*Feature Android` and `*Profile Android` control Android builds; `*Feature Standalone` and `*Profile Standalone` control editor play mode. Enabling a feature for one target does NOT enable it for the other.

## Things That Might Break

- **Unity version update**: May change `OpenXRLoader.cs` (re-apply patch), add new bionic symbol requirements, or change the Android binary
- **Unity Android Build Support update**: May re-extract NDK with broken symlinks (re-apply symlink fix)
- **OpenXR plugin update**: Same as above; additionally the layer negotiation API version might change
- **SteamVR update**: Could change extension requirements; monitor SteamVR release notes
- **Vulkan driver update**: Unlikely to break, but validation layer version mismatches can cause warnings
- **ALVR update**: May change controller emulation behavior; check interaction profiles
- **UFW/firewall changes**: ALVR auto-configured rules may break; see ALVR+UFW pitfall
