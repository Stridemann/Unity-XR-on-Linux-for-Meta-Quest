#!/bin/bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
OUT="$SCRIPT_DIR/libXrApiLayer_strip_android.so"

echo "Building strip_android OpenXR API layer..."
gcc -shared -fPIC -O2 -Wall \
    -o "$OUT" \
    "$SCRIPT_DIR/strip_android_layer.c"

echo "Built: $OUT"

LAYER_DIR="$HOME/.local/share/openxr/1/api_layers/implicit.d"
mkdir -p "$LAYER_DIR"

MANIFEST="$LAYER_DIR/XR_APILAYER_NOVENDOR_strip_android.json"
cat > "$MANIFEST" <<ENDJSON
{
    "file_format_version": "1.0.0",
    "api_layer": {
        "name": "XR_APILAYER_NOVENDOR_strip_android",
        "library_path": "$OUT",
        "api_version": "1.0",
        "implementation_version": "1",
        "description": "Strips XR_KHR_android_create_instance from xrCreateInstance calls so Android-compiled OpenXR plugins can work on desktop Linux",
        "enable_environment": "STRIP_ANDROID_XR_LAYER",
        "disable_environment": "DISABLE_STRIP_ANDROID_XR_LAYER"
    }
}
ENDJSON

echo "Installed manifest: $MANIFEST"
echo "Set STRIP_ANDROID_XR_LAYER=1 to activate the layer."
