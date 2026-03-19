# Unity XR on Linux for Meta Quest

Run Unity 6 VR projects on **Linux** with a **Meta Quest 3** headset via **SteamVR + ALVR** — no Windows required.

Unity's OpenXR plugin ships an Android x86_64 native binary (`libUnityOpenXR.so`) even on desktop Linux. This binary was compiled against Android's bionic libc and expects Android-specific APIs (JNI, EGL, `XR_KHR_android_create_instance`). On a standard Linux desktop with glibc, it simply crashes.

This project provides a set of compatibility shims and API layers that bridge the gap, making the Android OpenXR binary work transparently on desktop Linux with SteamVR.

## What This Project Provides

- **Bionic-to-glibc compatibility shims** — Shared libraries that export bionic's `LIBC`-versioned symbols, forwarding calls to their glibc equivalents
- **OpenXR API layer** — Strips Android-specific extensions (`XR_KHR_android_create_instance`) from OpenXR instance creation
- **Vulkan implicit layer** — Force-enables `VK_KHR_timeline_semaphore` that SteamVR requires but Unity doesn't enable
- **Fake JVM shim** — Provides a stub `JavaVM`/`JNIEnv` via `LD_PRELOAD` so the Android binary's JNI code doesn't crash
- **Patched OpenXR loader path** — Directs Unity to use the system `libopenxr_loader.so` instead of a Windows/UWP binary
- **Launch script** — Orchestrates all of the above with proper environment variables

## Status

**Working.** Both editor play mode and standalone APK builds are functional.

### Tested Platforms

| Mode | Status | Details |
|---|---|---|
| **Play from Editor (Linux)** | Tested | Via SteamVR + ALVR streaming to Quest 3. Use `launch_unity_vr.sh` |
| **Play from Editor (Android build profile)** | Tested | Same as above — build profile doesn't matter for editor play |
| **Build APK → Meta Quest 3** | Tested | Direct on-device VR via USB ADB. No ALVR/SteamVR needed |

### Tested Configuration
- Arch Linux (kernel 6.x)
- Unity 6 (tested on 6000.3.11f1)
- Meta Quest 3
- ALVR 20.x + SteamVR (for editor streaming)
- Vulkan renderer

Controllers, head tracking, and rendering all function. This should work on other glibc-based Linux distributions (Ubuntu, Fedora, etc.) with appropriate package name adjustments.

## Requirements

### Hardware
- A PC with a Vulkan-capable GPU (NVIDIA or AMD)
- Meta Quest 3 (or other Quest headset)
- WiFi connection between PC and Quest (or USB cable)

### Software — Linux Host
| Package | Arch Linux | Ubuntu/Debian equivalent |
|---|---|---|
| GCC | `base-devel` | `build-essential` |
| OpenXR loader | `openxr` | `libopenxr-loader1`, `libopenxr-dev` |
| Vulkan drivers | `vulkan-radeon` or `nvidia` | `mesa-vulkan-drivers` or `nvidia-driver` |
| Vulkan validation layers | `vulkan-validation-layers` | `vulkan-validationlayers` |
| SteamVR | Steam → Tools → SteamVR | Same |
| ALVR | [AUR: `alvr`](https://aur.archlinux.org/packages/alvr) or [GitHub releases](https://github.com/alvr-org/ALVR) | GitHub releases |

### Software — Unity
- **Unity 6** (tested on 6000.3.11f1) with **Linux Build Support**
- **Android Build Support** (for APK builds) — install via Unity Hub → Add Modules
- Required packages (via Unity Package Manager):
  - `com.unity.xr.openxr` (1.16.1)
  - `com.unity.xr.management` (4.5.4)
  - `com.unity.xr.interaction.toolkit` (3.3.1) — for controller input
  - `com.unity.inputsystem` (1.19.0)

### Software — Quest
- Developer mode enabled on Quest
- For editor streaming: ALVR client installed on Quest
- For APK builds: USB cable + ADB debugging enabled

## Quick Start (This Project)

1. **Clone this repository**
2. **Install system packages** (see table above)
3. **Set up ALVR:**
   - Install ALVR on both PC and Quest
   - Pair and connect them (WiFi or USB)
   - Ensure SteamVR launches via ALVR
4. **Edit `launch_unity_vr.sh`:**
   - Update `UNITY_EDITOR` path to your Unity installation
   - Adjust `GDK_SCALE`/`GDK_DPI_SCALE` if you don't have a HiDPI display (remove or set to 1)
5. **Launch:**
   ```bash
   chmod +x launch_unity_vr.sh
   ./launch_unity_vr.sh
   ```
   The script auto-builds all native shims on first run.
6. **In Unity:** Press Play. The scene should appear in your Quest headset.

## Setting Up Your Own Project

If you want to add Linux VR support to an **existing** Unity project:

### Step 1: Copy the NativeFix Directory

Copy the entire `NativeFix/` directory into your Unity project root (next to `Assets/`).

### Step 2: Copy and Adapt the Launch Script

Copy `launch_unity_vr.sh` to your project root. Edit:
- `UNITY_EDITOR` — path to your Unity editor binary
- Remove HiDPI lines if not needed

### Step 3: Patch `OpenXRLoader.cs`

In `Packages/com.unity.xr.openxr/Runtime/OpenXRLoader.cs`, find the `LoadOpenXRSymbols()` method and add a Linux-specific loader path. Look for the `#elif UNITY_EDITOR_OSX` section and add after it:

```csharp
#elif UNITY_EDITOR_LINUX
            loaderPath = "/usr/lib/libopenxr_loader.so";
```

Also add this guard around the `EditorBuildSettings.TryGetConfigObject` block so it doesn't override the Linux path:

```csharp
#if UNITY_EDITOR && !UNITY_EDITOR_LINUX
            // Pass down active loader path to plugin
            // ...existing code...
#endif
```

### Step 4: Configure Unity OpenXR Settings

In Unity Editor:

1. **Edit → Project Settings → XR Plug-in Management → OpenXR**:
   - Ensure OpenXR is enabled for Standalone
   - Under **Interaction Profiles**, enable **Oculus Touch Controller Profile** (not "Meta Quest Touch Plus" — SteamVR doesn't support the latter)

2. **OpenXR Editor Settings** (via the .asset file or UI):
   - Enable "Vulkan Additional Graphics Queue" (`m_vulkanAdditionalGraphicsQueue: 1`)

### Step 5: Set Up Your Scene

Use the XR Interaction Toolkit sample scenes, or add an **XR Origin (XR Rig)** to your scene:
- Import XRI Starter Assets from the Package Manager samples
- Use the `DemoScene` or `XR Interaction Setup` prefab

### Step 6: Launch

```bash
./launch_unity_vr.sh
```

## Building an APK for Meta Quest 3

The `NativeFix/` shims and layers are **only needed for editor play mode on Linux**. APK builds run natively on Android — no compatibility hacks required.

### Prerequisites

1. **Android Build Support** installed in Unity Hub (includes SDK, NDK, JDK)
2. **Developer mode** enabled on Quest 3
3. **USB cable** connected, with USB debugging authorized on the Quest

### Steps

1. **Switch platform**: File → Build Settings → Android → Switch Platform
2. **Configure Player Settings** (Edit → Project Settings → Player → Android tab):
   - Minimum API Level: **Android 10.0 (API level 29)** or higher
   - Target Architecture: **ARM64** only
   - Scripting Backend: **IL2CPP** (required for ARM64)
3. **Enable OpenXR for Android**: Edit → Project Settings → XR Plug-in Management → Android tab → check **OpenXR**
4. **Enable Meta Quest Support**: Under OpenXR features (Android tab), enable:
   - **Meta Quest Support** — required for Quest initialization and manifest setup
   - **Oculus Touch Controller Profile** — base controller input
   - **Meta Quest Touch Plus Controller Profile** — Quest 3 native controllers
5. **Build**: File → Build Settings → Build and Run (with Quest connected via USB)

### Known Issue: NDK Broken Symlinks on Linux

Unity's bundled Android NDK may have **broken symbolic links** on Linux. The symlinks in `.../NDK/toolchains/llvm/prebuilt/linux-x86_64/bin/` (like `clang++`, `ld`) point to a non-existent `android-ndk-r27c/` subdirectory.

**Symptom**: Build fails with `/bin/sh: line 1: .../clang++: No such file or directory`

**Fix**: Recreate the symlinks to point to the actual binaries in the same directory:

```bash
NDK_BIN="$HOME/Unity/Hub/Editor/6000.3.11f1/Editor/Data/PlaybackEngines/AndroidPlayer/NDK/toolchains/llvm/prebuilt/linux-x86_64/bin"
cd "$NDK_BIN"
ln -sf clang-18 clang
ln -sf clang clang++
ln -sf lld ld.lld
ln -sf lld lld-link
ln -sf lld ld64.lld
ln -sf lld wasm-ld
ln -sf ld.lld ld
ln -sf llvm-ar llvm-lib
ln -sf llvm-ar llvm-dlltool
ln -sf llvm-ar llvm-ranlib
ln -sf llvm-symbolizer llvm-addr2line
ln -sf llvm-objcopy llvm-strip
ln -sf llvm-readobj llvm-readelf
ln -sf llvm-rc llvm-windres
```

### Known Issue: Black Screen on Quest

If the APK starts but shows only a black window (not fullscreen VR), the **Meta Quest Support** feature is not enabled for Android builds. This feature is critical — it sets up the Quest's OpenXR loader initialization and Android manifest entries.

Go to: Edit → Project Settings → XR Plug-in Management → OpenXR (Android tab) → Enable **Meta Quest Support**.

## ALVR + Firewall (UFW) Pitfall

If you use **UFW** (Uncomplicated Firewall) on Linux, ALVR's automatic firewall configuration may create rules with **multiport** entries that don't work correctly. This causes the Quest and ALVR to fail to discover each other.

**Symptom**: Quest and PC ALVR can't connect despite being on the same network.

**Fix**: Either add the ALVR ports manually as individual rules, or restart your PC — the reboot clears the problematic multiport rules and ALVR will re-create them correctly on next launch.

## Project Structure

```
├── launch_unity_vr.sh              # Main launch script
├── NativeFix/
│   ├── build_compat.sh             # Builds bionic-to-glibc shims
│   ├── compat_libc.c               # libc shim (stdio, string, pthread, etc.)
│   ├── compat_libm.c               # libm shim (math functions)
│   ├── compat_libdl.c              # libdl shim (dlopen, dlsym, etc.)
│   ├── version_libc.map            # Linker version script for LIBC tag
│   ├── version_libm.map            # Linker version script for LIBC tag
│   ├── version_libdl.map           # Linker version script for LIBC tag
│   ├── fake_jvm.c                  # LD_PRELOAD JNI/JavaVM stub
│   ├── openxr_layer/
│   │   ├── strip_android_layer.c   # OpenXR API layer source
│   │   └── build_layer.sh          # Build + install layer manifest
│   └── vulkan_layer/
│       ├── force_timeline_sem.c    # Vulkan implicit layer source
│       └── build_vk_layer.sh       # Build + install layer manifest
├── Packages/
│   └── com.unity.xr.openxr/
│       └── Runtime/OpenXRLoader.cs # Patched: Linux loader path
├── Assets/
│   └── XR/Settings/
│       ├── OpenXRPackageSettings.asset  # Oculus Touch profile enabled
│       └── OpenXR Editor Settings.asset # Vulkan queue settings
└── Documentation/
    ├── TECHNICAL.md                # Detailed technical documentation
    └── AI_GUIDE.md                 # Guide for AI agents
```

## Troubleshooting

| Symptom | Likely Cause | Fix |
|---|---|---|
| `DllNotFoundException: .../libm.so.6: version 'LIBC' not found` | Compat shims not built or not on `LD_LIBRARY_PATH` | Run via `launch_unity_vr.sh`; it builds them automatically |
| `Failed to load openxr runtime loader` | Unity trying to load Windows/UWP OpenXR loader | Patch `OpenXRLoader.cs` (Step 3 above) |
| `xrCreateInstance` fails | Android extension not stripped | Ensure `STRIP_ANDROID_XR_LAYER=1` is set and layer is built |
| SIGSEGV in `libUnityOpenXR.so` during Play | Missing timeline semaphore or JVM | Ensure Vulkan layer + fake JVM shim are active |
| No image in headset but no crash | ALVR not connected, or wrong graphics API | Check ALVR connection; ensure `-force-vulkan` flag is used |
| Controllers don't work | Wrong interaction profile enabled | Enable "Oculus Touch Controller Profile" in OpenXR settings |
| UI too small/large | HiDPI scaling | Adjust `GDK_SCALE`/`GDK_DPI_SCALE` in launch script |
| APK build fails: `clang++: No such file or directory` | Broken NDK symlinks on Linux | See [NDK Broken Symlinks](#known-issue-ndk-broken-symlinks-on-linux) fix above |
| APK: black window on Quest, not fullscreen VR | Meta Quest Support feature disabled | Enable it in OpenXR settings for Android |
| ALVR can't find Quest on network | UFW multiport rule bug | Restart PC or add ALVR ports manually |

## License

MIT License. See [LICENSE](LICENSE) for details.

## Author

**Stridemann**

> Full disclosure: The author used **Cursor IDE with Claude (claude-4.16-opus-high)** to diagnose and build this entire compatibility layer. The process involved iterative debugging of native crashes, reverse-engineering the Android binary's dependencies, and building four separate native shims/layers. See [Documentation/TECHNICAL.md](Documentation/TECHNICAL.md) for the full story.

## Support

**This project is provided as-is with no support.** It is a proof-of-concept demonstrating that Unity VR can work on Linux with Meta Quest. If it breaks, the [technical documentation](Documentation/TECHNICAL.md) and [AI guide](Documentation/AI_GUIDE.md) should give you (or your AI assistant) enough context to debug it.
