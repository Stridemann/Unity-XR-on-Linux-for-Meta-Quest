/*
 * Bionic-to-glibc compatibility shim for libdl.so
 *
 * Provides LIBC-versioned dl* function wrappers that forward
 * to real glibc libdl implementations.
 */

#define _GNU_SOURCE
#include <dlfcn.h>
#include <link.h>

void *compat_dlopen(const char *filename, int flags) { return dlopen(filename, flags); }
__asm__(".symver compat_dlopen, dlopen@LIBC");

int compat_dlclose(void *handle) { return dlclose(handle); }
__asm__(".symver compat_dlclose, dlclose@LIBC");

void *compat_dlsym(void *handle, const char *symbol) { return dlsym(handle, symbol); }
__asm__(".symver compat_dlsym, dlsym@LIBC");

char *compat_dlerror(void) { return dlerror(); }
__asm__(".symver compat_dlerror, dlerror@LIBC");

int compat_dladdr(const void *addr, Dl_info *info) { return dladdr(addr, info); }
__asm__(".symver compat_dladdr, dladdr@LIBC");

int compat_dl_iterate_phdr(int (*callback)(struct dl_phdr_info *, size_t, void *), void *data) {
    return dl_iterate_phdr(callback, data);
}
__asm__(".symver compat_dl_iterate_phdr, dl_iterate_phdr@LIBC");
