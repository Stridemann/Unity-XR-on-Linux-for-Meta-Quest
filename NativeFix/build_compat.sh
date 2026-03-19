#!/bin/bash
set -euo pipefail
cd "$(dirname "$0")"

echo "Building bionic-to-glibc compatibility shims..."

CFLAGS="-shared -fPIC -O2 -Wall -Wno-deprecated-declarations"

echo "  [1/3] libc.so"
gcc $CFLAGS -o libc.so compat_libc.c \
    -Wl,-soname,libc.so \
    -Wl,--version-script=version_libc.map \
    -lpthread -lrt

echo "  [2/3] libm.so"
gcc $CFLAGS -o libm.so compat_libm.c \
    -Wl,-soname,libm.so \
    -Wl,--version-script=version_libm.map \
    -lm

echo "  [3/3] libdl.so"
gcc $CFLAGS -o libdl.so compat_libdl.c \
    -Wl,-soname,libdl.so \
    -Wl,--version-script=version_libdl.map \
    -ldl

echo "Done. Built:"
ls -la libc.so libm.so libdl.so
echo ""
echo "Verifying LIBC version definitions:"
for lib in libc.so libm.so libdl.so; do
    versions=$(readelf -V "$lib" 2>/dev/null | grep "LIBC" | head -1)
    echo "  $lib: $versions"
done
