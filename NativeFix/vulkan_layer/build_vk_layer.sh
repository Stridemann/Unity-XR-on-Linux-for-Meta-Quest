#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
SRC="$SCRIPT_DIR/force_timeline_sem.c"
SO="$SCRIPT_DIR/libVkLayer_force_timeline_sem.so"

echo "=== Building Vulkan layer: force_timeline_sem ==="

gcc -shared -fPIC -O2 -Wall -Wextra \
    -o "$SO" "$SRC"

echo "Built: $SO"

MANIFEST_DIR="$HOME/.local/share/vulkan/implicit_layer.d"
mkdir -p "$MANIFEST_DIR"

cat > "$MANIFEST_DIR/VkLayer_force_timeline_sem.json" <<ENDJSON
{
    "file_format_version": "1.0.0",
    "layer": {
        "name": "VK_LAYER_NOVENDOR_force_timeline_sem",
        "type": "GLOBAL",
        "library_path": "$SO",
        "api_version": "1.3.0",
        "implementation_version": "1",
        "description": "Forces VK_KHR_timeline_semaphore and timelineSemaphore feature on vkCreateDevice",
        "enable_environment": {
            "ENABLE_FORCE_TIMELINE_SEM": "1"
        },
        "disable_environment": {
            "DISABLE_FORCE_TIMELINE_SEM": "1"
        }
    }
}
ENDJSON

echo "Manifest installed: $MANIFEST_DIR/VkLayer_force_timeline_sem.json"
echo "=== Done ==="
