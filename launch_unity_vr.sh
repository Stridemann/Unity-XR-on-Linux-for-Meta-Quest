#!/bin/bash
#
# Launch Unity Editor with bionic-to-glibc compatibility shims
# for OpenXR VR development on Linux (Meta Quest 3 via SteamVR/ALVR)
#
# The OpenXR plugin's native library (libUnityOpenXR.so) is an Android x86_64
# binary compiled against bionic libc. This script sets up the environment so
# the bionic LIBC-versioned symbol requirements are satisfied by our wrapper
# libraries in NativeFix/.
#

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
NATIVEFIX_DIR="$SCRIPT_DIR/NativeFix"
UNITY_EDITOR="/home/stridarch/Unity/Hub/Editor/6000.3.11f1/Editor/Unity"
PROJECT_PATH="$SCRIPT_DIR"

# Build compat libraries if they don't exist
if [ ! -f "$NATIVEFIX_DIR/libc.so" ] || [ ! -f "$NATIVEFIX_DIR/libm.so" ] || [ ! -f "$NATIVEFIX_DIR/libdl.so" ]; then
    echo "Building bionic compatibility shims..."
    bash "$NATIVEFIX_DIR/build_compat.sh"
fi

# Prepend NativeFix to library path so dlopen finds our compat libs
# when Unity loads libUnityOpenXR.so (which needs libm.so, libdl.so, libc.so
# with LIBC version tags)
export LD_LIBRARY_PATH="${NATIVEFIX_DIR}${LD_LIBRARY_PATH:+:$LD_LIBRARY_PATH}"

# OpenXR loader debug output (set to "all" for full diagnostics, remove for production)
export XR_LOADER_DEBUG=all

# Vulkan validation layer for crash diagnostics (remove for production)
export VK_INSTANCE_LAYERS=VK_LAYER_KHRONOS_validation
export VK_LAYER_ENABLES=VK_VALIDATION_FEATURE_ENABLE_BEST_PRACTICES_EXT

# Force-enable VK_KHR_timeline_semaphore on all VkDevice creations.
# SteamVR's OpenXR runtime needs timeline semaphores for frame sync, but Unity
# doesn't enable the feature by default.
export ENABLE_FORCE_TIMELINE_SEM=1

# Build the Vulkan layer if it doesn't exist
VK_LAYER_SO="$NATIVEFIX_DIR/vulkan_layer/libVkLayer_force_timeline_sem.so"
if [ ! -f "$VK_LAYER_SO" ]; then
    echo "Building force_timeline_sem Vulkan layer..."
    bash "$NATIVEFIX_DIR/vulkan_layer/build_vk_layer.sh"
fi

# Activate our API layer that strips XR_KHR_android_create_instance from
# the Android binary's xrCreateInstance calls (SteamVR/Linux doesn't support it)
export STRIP_ANDROID_XR_LAYER=1

# Fake JavaVM for the Android binary: intercepts dlopen of libUnityOpenXR.so
# and calls JNI_OnLoad with a stub JavaVM so the binary's JNI code doesn't crash.
FAKE_JVM_SO="$NATIVEFIX_DIR/libfake_jvm.so"
if [ ! -f "$FAKE_JVM_SO" ]; then
    echo "Building fake JVM shim..."
    gcc -shared -fPIC -O2 -o "$FAKE_JVM_SO" "$NATIVEFIX_DIR/fake_jvm.c" -ldl
fi
export LD_PRELOAD="${FAKE_JVM_SO}${LD_PRELOAD:+:$LD_PRELOAD}"

# Build the API layer if it doesn't exist
LAYER_SO="$NATIVEFIX_DIR/openxr_layer/libXrApiLayer_strip_android.so"
if [ ! -f "$LAYER_SO" ]; then
    echo "Building strip_android OpenXR API layer..."
    bash "$NATIVEFIX_DIR/openxr_layer/build_layer.sh"
fi

# HiDPI scaling
export GDK_SCALE=2
export GDK_DPI_SCALE=0.5

echo "=== Unity VR Launch ==="
echo "Project:       $PROJECT_PATH"
echo "NativeFix:     $NATIVEFIX_DIR"
echo "LD_LIBRARY_PATH includes NativeFix: yes"
echo "OpenXR Runtime: $(cat ~/.config/openxr/1/active_runtime.json 2>/dev/null | grep -o '"name" : "[^"]*"' || echo 'not configured')"
echo "========================"

# Force Vulkan renderer: the Editor defaults to OpenGL Core on Linux, but the
# Android OpenXR binary's OpenGL path uses EGL (Android-only). The Vulkan path
# uses XrGraphicsBindingVulkanKHR which is platform-independent.
exec "$UNITY_EDITOR" -projectPath "$PROJECT_PATH" -force-vulkan "$@"
