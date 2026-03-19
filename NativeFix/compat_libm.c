/*
 * Bionic-to-glibc compatibility shim for libm.so
 *
 * Provides LIBC-versioned math function wrappers that forward
 * to real glibc libm implementations.
 */

#include <math.h>

float compat_tanf(float x) { return tanf(x); }
__asm__(".symver compat_tanf, tanf@LIBC");

float compat_roundf(float x) { return roundf(x); }
__asm__(".symver compat_roundf, roundf@LIBC");
