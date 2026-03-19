/*
 * Fake JavaVM/JNIEnv for libUnityOpenXR.so (Android binary on Linux)
 *
 * The Android OpenXR plugin expects JNI_OnLoad to be called by the Java
 * runtime when the library is loaded, providing a JavaVM pointer. On Linux
 * desktop (via dlopen), JNI_OnLoad is never called, leaving the plugin's
 * internal JavaVM pointer NULL. When the graphics thread starts, the plugin
 * tries JavaVM::GetEnv() on NULL and crashes.
 *
 * This LD_PRELOAD shim intercepts dlopen. When libUnityOpenXR.so is loaded,
 * it calls JNI_OnLoad with a fake JavaVM. The fake VM's GetEnv returns
 * JNI_OK with a minimal JNIEnv stub so the binary doesn't crash.
 */

#define _GNU_SOURCE
#include <dlfcn.h>
#include <stdint.h>
#include <stdlib.h>
#include <string.h>
#include <stdio.h>

typedef int32_t jint;
typedef void* jobject;

#define JNI_OK        0
#define JNI_VERSION_1_6 0x00010006

/* Forward declarations */
typedef const void **JNIEnv;
typedef const void **JavaVM;

/* === JNIEnv stub table (256 function pointers, all returning NULL/0) === */

static void *jni_stub(void) { return NULL; }

struct FakeJNINativeInterface {
    void *functions[256];
};

static struct FakeJNINativeInterface g_jni_native_interface;
static const void *g_jni_env_vtable_ptr; /* JNIEnv = pointer to vtable */

/* === JNIInvokeInterface (JavaVM vtable) === */

struct FakeJNIInvokeInterface {
    void *reserved0;
    void *reserved1;
    void *reserved2;
    jint (*DestroyJavaVM)(JavaVM *);
    jint (*AttachCurrentThread)(JavaVM *, void **, void *);
    jint (*DetachCurrentThread)(JavaVM *);
    jint (*GetEnv)(JavaVM *, void **, jint);
    jint (*AttachCurrentThreadAsDaemon)(JavaVM *, void **, void *);
};

static jint fake_DestroyJavaVM(JavaVM *vm) { (void)vm; return JNI_OK; }

static jint fake_AttachCurrentThread(JavaVM *vm, void **penv, void *args)
{
    (void)vm; (void)args;
    if (penv) *penv = (void *)&g_jni_env_vtable_ptr;
    return JNI_OK;
}

static jint fake_DetachCurrentThread(JavaVM *vm) { (void)vm; return JNI_OK; }

static jint fake_GetEnv(JavaVM *vm, void **penv, jint version)
{
    (void)vm; (void)version;
    if (penv) *penv = (void *)&g_jni_env_vtable_ptr;
    return JNI_OK;
}

static jint fake_AttachCurrentThreadAsDaemon(JavaVM *vm, void **penv, void *args)
{
    (void)vm; (void)args;
    if (penv) *penv = (void *)&g_jni_env_vtable_ptr;
    return JNI_OK;
}

static struct FakeJNIInvokeInterface g_vm_vtable = {
    .reserved0 = NULL,
    .reserved1 = NULL,
    .reserved2 = NULL,
    .DestroyJavaVM = fake_DestroyJavaVM,
    .AttachCurrentThread = fake_AttachCurrentThread,
    .DetachCurrentThread = fake_DetachCurrentThread,
    .GetEnv = fake_GetEnv,
    .AttachCurrentThreadAsDaemon = fake_AttachCurrentThreadAsDaemon,
};

/* JavaVM is a pointer-to-pointer-to-vtable: *vm gives the vtable pointer */
static struct FakeJNIInvokeInterface *g_fake_java_vm = &g_vm_vtable;

static void init_fake_jni(void)
{
    static int initialized = 0;
    if (initialized) return;
    initialized = 1;

    for (int i = 0; i < 256; i++)
        g_jni_native_interface.functions[i] = (void *)jni_stub;

    g_jni_env_vtable_ptr = (const void *)&g_jni_native_interface;

    fprintf(stderr, "[fake_jvm] Fake JavaVM/JNIEnv initialized\n");
}

/* === Intercept dlopen === */

void *dlopen(const char *filename, int flags)
{
    static void *(*real_dlopen)(const char *, int) = NULL;
    if (!real_dlopen)
        real_dlopen = (void *(*)(const char *, int))dlsym(RTLD_NEXT, "dlopen");

    void *handle = real_dlopen(filename, flags);

    if (handle && filename && strstr(filename, "libUnityOpenXR.so")) {
        init_fake_jni();

        typedef jint (*PFN_JNI_OnLoad)(JavaVM *, void *);
        PFN_JNI_OnLoad onLoad = (PFN_JNI_OnLoad)dlsym(handle, "JNI_OnLoad");
        if (onLoad) {
            fprintf(stderr, "[fake_jvm] Calling JNI_OnLoad(%p) for %s\n",
                    (void *)&g_fake_java_vm, filename);
            jint result = onLoad((JavaVM *)&g_fake_java_vm, NULL);
            fprintf(stderr, "[fake_jvm] JNI_OnLoad returned %d\n", result);
        } else {
            fprintf(stderr, "[fake_jvm] WARNING: JNI_OnLoad not found in %s\n", filename);
        }
    }

    return handle;
}

/* Provide JNI_GetCreatedJavaVMs in case anything calls it */
jint JNI_GetCreatedJavaVMs(JavaVM **vmBuf, jint bufLen, jint *nVMs)
{
    init_fake_jni();
    if (vmBuf && bufLen > 0)
        vmBuf[0] = (JavaVM *)&g_fake_java_vm;
    if (nVMs)
        *nVMs = 1;
    return JNI_OK;
}
