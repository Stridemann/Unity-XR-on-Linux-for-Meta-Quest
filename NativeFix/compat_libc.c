/*
 * Bionic-to-glibc compatibility shim for libc.so
 *
 * Android's bionic libc exports symbols under a "LIBC" version tag.
 * glibc uses "GLIBC_2.x.x" version tags. This library provides
 * LIBC-versioned wrappers that forward to real glibc implementations,
 * allowing Android x86_64 .so binaries to load on Linux.
 */

#define _GNU_SOURCE
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <errno.h>
#include <locale.h>
#include <wchar.h>
#include <wctype.h>
#include <ctype.h>
#include <time.h>
#include <pthread.h>
#include <sched.h>
#include <unistd.h>
#include <syslog.h>
#include <sys/syscall.h>
#include <stdarg.h>
#include <dlfcn.h>

/* ========================================================================
 * Bionic __sF compatibility
 *
 * Bionic: FILE __sF[3] where __sF[0]=stdin, __sF[1]=stdout, __sF[2]=stderr
 * glibc:  FILE *stdin, *stdout, *stderr (separate pointers)
 *
 * We provide a dummy __sF array and intercept stdio functions that take
 * FILE* to translate __sF references to real glibc streams.
 * ======================================================================== */

typedef struct { char _pad[256]; } bionic_FILE;

bionic_FILE __sF_impl[3];
__asm__(".symver __sF_impl, __sF@LIBC");

static __attribute__((always_inline)) inline FILE *translate_stream(FILE *f) {
    char *p = (char *)f;
    char *base = (char *)__sF_impl;
    if (p >= base && p < base + (int)sizeof(__sF_impl)) {
        int idx = (int)((p - base) / (int)sizeof(bionic_FILE));
        if (idx == 0) return stdin;
        if (idx == 1) return stdout;
        if (idx == 2) return stderr;
    }
    return f;
}

/* ========================================================================
 * Bionic-specific symbols with no glibc equivalent
 * ======================================================================== */

int *compat___errno(void) { return __errno_location(); }
__asm__(".symver compat___errno, __errno@LIBC");

void compat_android_set_abort_message(const char *msg) { (void)msg; }
__asm__(".symver compat_android_set_abort_message, android_set_abort_message@LIBC");

/* ========================================================================
 * Stdio wrappers (need FILE* translation for __sF compat)
 * ======================================================================== */

int compat_fputc(int c, FILE *stream) { return fputc(c, translate_stream(stream)); }
__asm__(".symver compat_fputc, fputc@LIBC");

int compat_vfprintf(FILE *stream, const char *fmt, va_list ap) {
    return vfprintf(translate_stream(stream), fmt, ap);
}
__asm__(".symver compat_vfprintf, vfprintf@LIBC");

int compat_fprintf(FILE *stream, const char *fmt, ...) {
    va_list ap;
    va_start(ap, fmt);
    int ret = vfprintf(translate_stream(stream), fmt, ap);
    va_end(ap);
    return ret;
}
__asm__(".symver compat_fprintf, fprintf@LIBC");

int compat_fflush(FILE *stream) {
    if (!stream) return fflush(NULL);
    return fflush(translate_stream(stream));
}
__asm__(".symver compat_fflush, fflush@LIBC");

size_t compat_fwrite(const void *ptr, size_t size, size_t nmemb, FILE *stream) {
    return fwrite(ptr, size, nmemb, translate_stream(stream));
}
__asm__(".symver compat_fwrite, fwrite@LIBC");

/* ========================================================================
 * Variadic / complex-signature wrappers (written manually)
 * ======================================================================== */

int compat_vsnprintf(char *buf, size_t size, const char *fmt, va_list ap) {
    return vsnprintf(buf, size, fmt, ap);
}
__asm__(".symver compat_vsnprintf, vsnprintf@LIBC");

int compat_snprintf(char *buf, size_t size, const char *fmt, ...) {
    va_list ap;
    va_start(ap, fmt);
    int ret = vsnprintf(buf, size, fmt, ap);
    va_end(ap);
    return ret;
}
__asm__(".symver compat_snprintf, snprintf@LIBC");

int compat_sscanf(const char *str, const char *fmt, ...) {
    va_list ap;
    va_start(ap, fmt);
    int ret = vsscanf(str, fmt, ap);
    va_end(ap);
    return ret;
}
__asm__(".symver compat_sscanf, sscanf@LIBC");

int compat_vsscanf(const char *str, const char *fmt, va_list ap) {
    return vsscanf(str, fmt, ap);
}
__asm__(".symver compat_vsscanf, vsscanf@LIBC");

int compat_vasprintf(char **strp, const char *fmt, va_list ap) {
    return vasprintf(strp, fmt, ap);
}
__asm__(".symver compat_vasprintf, vasprintf@LIBC");

int compat_swprintf(wchar_t *wcs, size_t maxlen, const wchar_t *fmt, ...) {
    va_list ap;
    va_start(ap, fmt);
    int ret = vswprintf(wcs, maxlen, fmt, ap);
    va_end(ap);
    return ret;
}
__asm__(".symver compat_swprintf, swprintf@LIBC");

/* syslog is variadic */
void compat_openlog(const char *ident, int option, int facility) {
    openlog(ident, option, facility);
}
__asm__(".symver compat_openlog, openlog@LIBC");

void compat_closelog(void) { closelog(); }
__asm__(".symver compat_closelog, closelog@LIBC");

void compat_syslog(int priority, const char *format, ...) {
    va_list ap;
    va_start(ap, format);
    vsyslog(priority, format, ap);
    va_end(ap);
}
__asm__(".symver compat_syslog, syslog@LIBC");

/* syscall is variadic */
long compat_syscall(long number, ...) {
    /* For common syscalls used by the OpenXR binary (futex, etc.), forward
       via the real syscall(). We use inline asm for a 6-arg passthrough. */
    va_list ap;
    va_start(ap, number);
    long a1 = va_arg(ap, long);
    long a2 = va_arg(ap, long);
    long a3 = va_arg(ap, long);
    long a4 = va_arg(ap, long);
    long a5 = va_arg(ap, long);
    long a6 = va_arg(ap, long);
    va_end(ap);
    return syscall(number, a1, a2, a3, a4, a5, a6);
}
__asm__(".symver compat_syscall, syscall@LIBC");

/* ========================================================================
 * CXA / init (function-pointer args need manual wrappers)
 * ======================================================================== */

extern int __cxa_atexit(void (*)(void *), void *, void *);
int compat___cxa_atexit(void (*func)(void *), void *arg, void *dso) {
    return __cxa_atexit(func, arg, dso);
}
__asm__(".symver compat___cxa_atexit, __cxa_atexit@LIBC");

extern void __cxa_finalize(void *);
void compat___cxa_finalize(void *dso) { __cxa_finalize(dso); }
__asm__(".symver compat___cxa_finalize, __cxa_finalize@LIBC");

extern int __register_atfork(void (*)(void), void (*)(void), void (*)(void), void *);
int compat___register_atfork(void (*prepare)(void), void (*parent)(void),
                              void (*child)(void), void *dso) {
    return __register_atfork(prepare, parent, child, dso);
}
__asm__(".symver compat___register_atfork, __register_atfork@LIBC");

/* ========================================================================
 * Memory functions (memchr is a macro on some systems, write manually)
 * ======================================================================== */

#undef memchr
void *compat_memcpy(void *d, const void *s, size_t n) { return memcpy(d, s, n); }
__asm__(".symver compat_memcpy, memcpy@LIBC");

void *compat_memset(void *s, int c, size_t n) { return memset(s, c, n); }
__asm__(".symver compat_memset, memset@LIBC");

void *compat_memmove(void *d, const void *s, size_t n) { return memmove(d, s, n); }
__asm__(".symver compat_memmove, memmove@LIBC");

int compat_memcmp(const void *a, const void *b, size_t n) { return memcmp(a, b, n); }
__asm__(".symver compat_memcmp, memcmp@LIBC");

extern void *memchr(const void *, int, size_t);
void *compat_memchr(const void *s, int c, size_t n) { return memchr(s, c, n); }
__asm__(".symver compat_memchr, memchr@LIBC");

/* ========================================================================
 * String functions
 * ======================================================================== */

size_t compat_strlen(const char *s) { return strlen(s); }
__asm__(".symver compat_strlen, strlen@LIBC");

int compat_strcmp(const char *a, const char *b) { return strcmp(a, b); }
__asm__(".symver compat_strcmp, strcmp@LIBC");

int compat_strncmp(const char *a, const char *b, size_t n) { return strncmp(a, b, n); }
__asm__(".symver compat_strncmp, strncmp@LIBC");

char *compat_strncpy(char *d, const char *s, size_t n) { return strncpy(d, s, n); }
__asm__(".symver compat_strncpy, strncpy@LIBC");

char *compat_strcpy(char *d, const char *s) { return strcpy(d, s); }
__asm__(".symver compat_strcpy, strcpy@LIBC");

int compat_atoi(const char *s) { return atoi(s); }
__asm__(".symver compat_atoi, atoi@LIBC");

/* ========================================================================
 * stdlib
 * ======================================================================== */

void compat_abort(void) { abort(); }
__asm__(".symver compat_abort, abort@LIBC");

void *compat_malloc(size_t size) { return malloc(size); }
__asm__(".symver compat_malloc, malloc@LIBC");

void *compat_realloc(void *ptr, size_t size) { return realloc(ptr, size); }
__asm__(".symver compat_realloc, realloc@LIBC");

void compat_free(void *ptr) { free(ptr); }
__asm__(".symver compat_free, free@LIBC");

int compat_posix_memalign(void **memptr, size_t alignment, size_t size) {
    return posix_memalign(memptr, alignment, size);
}
__asm__(".symver compat_posix_memalign, posix_memalign@LIBC");

/* ========================================================================
 * String conversion
 * ======================================================================== */

double compat_strtod(const char *s, char **e) { return strtod(s, e); }
__asm__(".symver compat_strtod, strtod@LIBC");

float compat_strtof(const char *s, char **e) { return strtof(s, e); }
__asm__(".symver compat_strtof, strtof@LIBC");

long compat_strtol(const char *s, char **e, int base) { return strtol(s, e, base); }
__asm__(".symver compat_strtol, strtol@LIBC");

long double compat_strtold(const char *s, char **e) { return strtold(s, e); }
__asm__(".symver compat_strtold, strtold@LIBC");

long long compat_strtoll(const char *s, char **e, int base) { return strtoll(s, e, base); }
__asm__(".symver compat_strtoll, strtoll@LIBC");

unsigned long compat_strtoul(const char *s, char **e, int base) { return strtoul(s, e, base); }
__asm__(".symver compat_strtoul, strtoul@LIBC");

unsigned long long compat_strtoull(const char *s, char **e, int base) { return strtoull(s, e, base); }
__asm__(".symver compat_strtoull, strtoull@LIBC");

/* locale-specific conversions */
long double compat_strtold_l(const char *s, char **e, locale_t l) { return strtold_l(s, e, l); }
__asm__(".symver compat_strtold_l, strtold_l@LIBC");

long long compat_strtoll_l(const char *s, char **e, int base, locale_t l) { return strtoll_l(s, e, base, l); }
__asm__(".symver compat_strtoll_l, strtoll_l@LIBC");

unsigned long long compat_strtoull_l(const char *s, char **e, int base, locale_t l) { return strtoull_l(s, e, base, l); }
__asm__(".symver compat_strtoull_l, strtoull_l@LIBC");

int compat_strcoll_l(const char *a, const char *b, locale_t l) { return strcoll_l(a, b, l); }
__asm__(".symver compat_strcoll_l, strcoll_l@LIBC");

size_t compat_strftime_l(char *s, size_t max, const char *fmt, const struct tm *tm, locale_t l) {
    return strftime_l(s, max, fmt, tm, l);
}
__asm__(".symver compat_strftime_l, strftime_l@LIBC");

size_t compat_strxfrm_l(char *d, const char *s, size_t n, locale_t l) { return strxfrm_l(d, s, n, l); }
__asm__(".symver compat_strxfrm_l, strxfrm_l@LIBC");

char *compat_strerror_r(int errnum, char *buf, size_t buflen) { return strerror_r(errnum, buf, buflen); }
__asm__(".symver compat_strerror_r, strerror_r@LIBC");

/* ========================================================================
 * Wide string
 * ======================================================================== */

size_t compat_wcslen(const wchar_t *s) { return wcslen(s); }
__asm__(".symver compat_wcslen, wcslen@LIBC");

size_t compat_wcstombs(char *d, const wchar_t *s, size_t n) { return wcstombs(d, s, n); }
__asm__(".symver compat_wcstombs, wcstombs@LIBC");

wchar_t *compat_wmemcpy(wchar_t *d, const wchar_t *s, size_t n) { return wmemcpy(d, s, n); }
__asm__(".symver compat_wmemcpy, wmemcpy@LIBC");

wchar_t *compat_wmemset(wchar_t *s, wchar_t c, size_t n) { return wmemset(s, c, n); }
__asm__(".symver compat_wmemset, wmemset@LIBC");

wchar_t *compat_wmemchr(const wchar_t *s, wchar_t c, size_t n) { return (wchar_t *)wmemchr(s, c, n); }
__asm__(".symver compat_wmemchr, wmemchr@LIBC");

int compat_wmemcmp(const wchar_t *a, const wchar_t *b, size_t n) { return wmemcmp(a, b, n); }
__asm__(".symver compat_wmemcmp, wmemcmp@LIBC");

wchar_t *compat_wmemmove(wchar_t *d, const wchar_t *s, size_t n) { return wmemmove(d, s, n); }
__asm__(".symver compat_wmemmove, wmemmove@LIBC");

int compat_wcrtomb(char *s, wchar_t wc, mbstate_t *ps) { return wcrtomb(s, wc, ps); }
__asm__(".symver compat_wcrtomb, wcrtomb@LIBC");

int compat_wcscoll_l(const wchar_t *a, const wchar_t *b, locale_t l) { return wcscoll_l(a, b, l); }
__asm__(".symver compat_wcscoll_l, wcscoll_l@LIBC");

size_t compat_wcsnrtombs(char *d, const wchar_t **s, size_t nwc, size_t len, mbstate_t *ps) {
    return wcsnrtombs(d, s, nwc, len, ps);
}
__asm__(".symver compat_wcsnrtombs, wcsnrtombs@LIBC");

size_t compat_wcsxfrm_l(wchar_t *d, const wchar_t *s, size_t n, locale_t l) { return wcsxfrm_l(d, s, n, l); }
__asm__(".symver compat_wcsxfrm_l, wcsxfrm_l@LIBC");

int compat_wctob(wint_t c) { return wctob(c); }
__asm__(".symver compat_wctob, wctob@LIBC");

double compat_wcstod(const wchar_t *s, wchar_t **e) { return wcstod(s, e); }
__asm__(".symver compat_wcstod, wcstod@LIBC");

float compat_wcstof(const wchar_t *s, wchar_t **e) { return wcstof(s, e); }
__asm__(".symver compat_wcstof, wcstof@LIBC");

long compat_wcstol(const wchar_t *s, wchar_t **e, int base) { return wcstol(s, e, base); }
__asm__(".symver compat_wcstol, wcstol@LIBC");

long double compat_wcstold(const wchar_t *s, wchar_t **e) { return wcstold(s, e); }
__asm__(".symver compat_wcstold, wcstold@LIBC");

long long compat_wcstoll(const wchar_t *s, wchar_t **e, int base) { return wcstoll(s, e, base); }
__asm__(".symver compat_wcstoll, wcstoll@LIBC");

unsigned long compat_wcstoul(const wchar_t *s, wchar_t **e, int base) { return wcstoul(s, e, base); }
__asm__(".symver compat_wcstoul, wcstoul@LIBC");

unsigned long long compat_wcstoull(const wchar_t *s, wchar_t **e, int base) { return wcstoull(s, e, base); }
__asm__(".symver compat_wcstoull, wcstoull@LIBC");

/* ========================================================================
 * Multibyte
 * ======================================================================== */

wint_t compat_btowc(int c) { return btowc(c); }
__asm__(".symver compat_btowc, btowc@LIBC");

size_t compat_mbrlen(const char *s, size_t n, mbstate_t *ps) { return mbrlen(s, n, ps); }
__asm__(".symver compat_mbrlen, mbrlen@LIBC");

size_t compat_mbrtowc(wchar_t *pwc, const char *s, size_t n, mbstate_t *ps) { return mbrtowc(pwc, s, n, ps); }
__asm__(".symver compat_mbrtowc, mbrtowc@LIBC");

size_t compat_mbsnrtowcs(wchar_t *d, const char **s, size_t nms, size_t len, mbstate_t *ps) {
    return mbsnrtowcs(d, s, nms, len, ps);
}
__asm__(".symver compat_mbsnrtowcs, mbsnrtowcs@LIBC");

size_t compat_mbsrtowcs(wchar_t *d, const char **s, size_t len, mbstate_t *ps) {
    return mbsrtowcs(d, s, len, ps);
}
__asm__(".symver compat_mbsrtowcs, mbsrtowcs@LIBC");

int compat_mbtowc(wchar_t *pwc, const char *s, size_t n) { return mbtowc(pwc, s, n); }
__asm__(".symver compat_mbtowc, mbtowc@LIBC");

/* ========================================================================
 * Locale
 * ======================================================================== */

struct lconv *compat_localeconv(void) { return localeconv(); }
__asm__(".symver compat_localeconv, localeconv@LIBC");

locale_t compat_newlocale(int mask, const char *locale, locale_t base) { return newlocale(mask, locale, base); }
__asm__(".symver compat_newlocale, newlocale@LIBC");

void compat_freelocale(locale_t loc) { freelocale(loc); }
__asm__(".symver compat_freelocale, freelocale@LIBC");

char *compat_setlocale(int category, const char *locale) { return setlocale(category, locale); }
__asm__(".symver compat_setlocale, setlocale@LIBC");

locale_t compat_uselocale(locale_t loc) { return uselocale(loc); }
__asm__(".symver compat_uselocale, uselocale@LIBC");

/* ========================================================================
 * ctype (locale-aware)
 * ======================================================================== */

int compat_isdigit_l(int c, locale_t l) { return isdigit_l(c, l); }
__asm__(".symver compat_isdigit_l, isdigit_l@LIBC");

int compat_islower_l(int c, locale_t l) { return islower_l(c, l); }
__asm__(".symver compat_islower_l, islower_l@LIBC");

int compat_isupper_l(int c, locale_t l) { return isupper_l(c, l); }
__asm__(".symver compat_isupper_l, isupper_l@LIBC");

int compat_isxdigit_l(int c, locale_t l) { return isxdigit_l(c, l); }
__asm__(".symver compat_isxdigit_l, isxdigit_l@LIBC");

int compat_tolower_l(int c, locale_t l) { return tolower_l(c, l); }
__asm__(".symver compat_tolower_l, tolower_l@LIBC");

int compat_toupper_l(int c, locale_t l) { return toupper_l(c, l); }
__asm__(".symver compat_toupper_l, toupper_l@LIBC");

/* ========================================================================
 * wctype (locale-aware)
 * ======================================================================== */

int compat_iswalpha_l(wint_t c, locale_t l) { return iswalpha_l(c, l); }
__asm__(".symver compat_iswalpha_l, iswalpha_l@LIBC");

int compat_iswblank_l(wint_t c, locale_t l) { return iswblank_l(c, l); }
__asm__(".symver compat_iswblank_l, iswblank_l@LIBC");

int compat_iswcntrl_l(wint_t c, locale_t l) { return iswcntrl_l(c, l); }
__asm__(".symver compat_iswcntrl_l, iswcntrl_l@LIBC");

int compat_iswdigit_l(wint_t c, locale_t l) { return iswdigit_l(c, l); }
__asm__(".symver compat_iswdigit_l, iswdigit_l@LIBC");

int compat_iswlower_l(wint_t c, locale_t l) { return iswlower_l(c, l); }
__asm__(".symver compat_iswlower_l, iswlower_l@LIBC");

int compat_iswprint_l(wint_t c, locale_t l) { return iswprint_l(c, l); }
__asm__(".symver compat_iswprint_l, iswprint_l@LIBC");

int compat_iswpunct_l(wint_t c, locale_t l) { return iswpunct_l(c, l); }
__asm__(".symver compat_iswpunct_l, iswpunct_l@LIBC");

int compat_iswspace_l(wint_t c, locale_t l) { return iswspace_l(c, l); }
__asm__(".symver compat_iswspace_l, iswspace_l@LIBC");

int compat_iswupper_l(wint_t c, locale_t l) { return iswupper_l(c, l); }
__asm__(".symver compat_iswupper_l, iswupper_l@LIBC");

int compat_iswxdigit_l(wint_t c, locale_t l) { return iswxdigit_l(c, l); }
__asm__(".symver compat_iswxdigit_l, iswxdigit_l@LIBC");

wint_t compat_towlower_l(wint_t c, locale_t l) { return towlower_l(c, l); }
__asm__(".symver compat_towlower_l, towlower_l@LIBC");

wint_t compat_towupper_l(wint_t c, locale_t l) { return towupper_l(c, l); }
__asm__(".symver compat_towupper_l, towupper_l@LIBC");

/* ========================================================================
 * Fortify-source checked functions
 * ======================================================================== */

extern void __stack_chk_fail(void);
void compat___stack_chk_fail(void) { __stack_chk_fail(); }
__asm__(".symver compat___stack_chk_fail, __stack_chk_fail@LIBC");

extern size_t __strlen_chk(const char *, size_t);
size_t compat___strlen_chk(const char *s, size_t maxlen) { return __strlen_chk(s, maxlen); }
__asm__(".symver compat___strlen_chk, __strlen_chk@LIBC");

extern void *__memmove_chk(void *, const void *, size_t, size_t);
void *compat___memmove_chk(void *d, const void *s, size_t len, size_t dstlen) {
    return __memmove_chk(d, s, len, dstlen);
}
__asm__(".symver compat___memmove_chk, __memmove_chk@LIBC");

extern int __vsnprintf_chk(char *, size_t, int, size_t, const char *, va_list);
int compat___vsnprintf_chk(char *s, size_t maxlen, int flag, size_t slen, const char *fmt, va_list ap) {
    return __vsnprintf_chk(s, maxlen, flag, slen, fmt, ap);
}
__asm__(".symver compat___vsnprintf_chk, __vsnprintf_chk@LIBC");

extern size_t __ctype_get_mb_cur_max(void);
size_t compat___ctype_get_mb_cur_max(void) { return __ctype_get_mb_cur_max(); }
__asm__(".symver compat___ctype_get_mb_cur_max, __ctype_get_mb_cur_max@LIBC");

/* ========================================================================
 * Time
 * ======================================================================== */

struct tm *compat_localtime_r(const time_t *t, struct tm *result) { return localtime_r(t, result); }
__asm__(".symver compat_localtime_r, localtime_r@LIBC");

int compat_clock_gettime(clockid_t clk_id, struct timespec *tp) { return clock_gettime(clk_id, tp); }
__asm__(".symver compat_clock_gettime, clock_gettime@LIBC");

int compat_nanosleep(const struct timespec *req, struct timespec *rem) { return nanosleep(req, rem); }
__asm__(".symver compat_nanosleep, nanosleep@LIBC");

/* ========================================================================
 * pthread (function-pointer args written manually)
 * ======================================================================== */

int compat_pthread_equal(pthread_t t1, pthread_t t2) { return pthread_equal(t1, t2); }
__asm__(".symver compat_pthread_equal, pthread_equal@LIBC");

pthread_t compat_pthread_self(void) { return pthread_self(); }
__asm__(".symver compat_pthread_self, pthread_self@LIBC");

int compat_pthread_mutex_lock(pthread_mutex_t *m) { return pthread_mutex_lock(m); }
__asm__(".symver compat_pthread_mutex_lock, pthread_mutex_lock@LIBC");

int compat_pthread_mutex_trylock(pthread_mutex_t *m) { return pthread_mutex_trylock(m); }
__asm__(".symver compat_pthread_mutex_trylock, pthread_mutex_trylock@LIBC");

int compat_pthread_mutex_unlock(pthread_mutex_t *m) { return pthread_mutex_unlock(m); }
__asm__(".symver compat_pthread_mutex_unlock, pthread_mutex_unlock@LIBC");

int compat_pthread_mutex_destroy(pthread_mutex_t *m) { return pthread_mutex_destroy(m); }
__asm__(".symver compat_pthread_mutex_destroy, pthread_mutex_destroy@LIBC");

int compat_pthread_mutex_init(pthread_mutex_t *m, const pthread_mutexattr_t *a) {
    return pthread_mutex_init(m, a);
}
__asm__(".symver compat_pthread_mutex_init, pthread_mutex_init@LIBC");

int compat_pthread_mutexattr_destroy(pthread_mutexattr_t *a) { return pthread_mutexattr_destroy(a); }
__asm__(".symver compat_pthread_mutexattr_destroy, pthread_mutexattr_destroy@LIBC");

int compat_pthread_mutexattr_init(pthread_mutexattr_t *a) { return pthread_mutexattr_init(a); }
__asm__(".symver compat_pthread_mutexattr_init, pthread_mutexattr_init@LIBC");

int compat_pthread_mutexattr_settype(pthread_mutexattr_t *a, int type) {
    return pthread_mutexattr_settype(a, type);
}
__asm__(".symver compat_pthread_mutexattr_settype, pthread_mutexattr_settype@LIBC");

int compat_pthread_cond_broadcast(pthread_cond_t *c) { return pthread_cond_broadcast(c); }
__asm__(".symver compat_pthread_cond_broadcast, pthread_cond_broadcast@LIBC");

int compat_pthread_cond_wait(pthread_cond_t *c, pthread_mutex_t *m) {
    return pthread_cond_wait(c, m);
}
__asm__(".symver compat_pthread_cond_wait, pthread_cond_wait@LIBC");

int compat_pthread_cond_signal(pthread_cond_t *c) { return pthread_cond_signal(c); }
__asm__(".symver compat_pthread_cond_signal, pthread_cond_signal@LIBC");

int compat_pthread_cond_timedwait(pthread_cond_t *c, pthread_mutex_t *m, const struct timespec *t) {
    return pthread_cond_timedwait(c, m, t);
}
__asm__(".symver compat_pthread_cond_timedwait, pthread_cond_timedwait@LIBC");

int compat_pthread_cond_destroy(pthread_cond_t *c) { return pthread_cond_destroy(c); }
__asm__(".symver compat_pthread_cond_destroy, pthread_cond_destroy@LIBC");

void *compat_pthread_getspecific(pthread_key_t key) { return pthread_getspecific(key); }
__asm__(".symver compat_pthread_getspecific, pthread_getspecific@LIBC");

int compat_pthread_setspecific(pthread_key_t key, const void *value) {
    return pthread_setspecific(key, value);
}
__asm__(".symver compat_pthread_setspecific, pthread_setspecific@LIBC");

int compat_pthread_detach(pthread_t thread) { return pthread_detach(thread); }
__asm__(".symver compat_pthread_detach, pthread_detach@LIBC");

int compat_pthread_join(pthread_t thread, void **retval) { return pthread_join(thread, retval); }
__asm__(".symver compat_pthread_join, pthread_join@LIBC");

int compat_pthread_key_create(pthread_key_t *key, void (*destructor)(void *)) {
    return pthread_key_create(key, destructor);
}
__asm__(".symver compat_pthread_key_create, pthread_key_create@LIBC");

int compat_pthread_key_delete(pthread_key_t key) { return pthread_key_delete(key); }
__asm__(".symver compat_pthread_key_delete, pthread_key_delete@LIBC");

int compat_pthread_once(pthread_once_t *once_control, void (*init_routine)(void)) {
    return pthread_once(once_control, init_routine);
}
__asm__(".symver compat_pthread_once, pthread_once@LIBC");

int compat_pthread_rwlock_rdlock(pthread_rwlock_t *rwlock) { return pthread_rwlock_rdlock(rwlock); }
__asm__(".symver compat_pthread_rwlock_rdlock, pthread_rwlock_rdlock@LIBC");

int compat_pthread_rwlock_unlock(pthread_rwlock_t *rwlock) { return pthread_rwlock_unlock(rwlock); }
__asm__(".symver compat_pthread_rwlock_unlock, pthread_rwlock_unlock@LIBC");

int compat_pthread_rwlock_wrlock(pthread_rwlock_t *rwlock) { return pthread_rwlock_wrlock(rwlock); }
__asm__(".symver compat_pthread_rwlock_wrlock, pthread_rwlock_wrlock@LIBC");

/* ========================================================================
 * sched
 * ======================================================================== */

int compat_sched_yield(void) { return sched_yield(); }
__asm__(".symver compat_sched_yield, sched_yield@LIBC");

/* ========================================================================
 * Misc
 * ======================================================================== */

pid_t compat_gettid(void) { return gettid(); }
__asm__(".symver compat_gettid, gettid@LIBC");

long compat_sysconf(int name) { return sysconf(name); }
__asm__(".symver compat_sysconf, sysconf@LIBC");
