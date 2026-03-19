/*
 * Vulkan Implicit Layer: force_timeline_semaphore
 *
 * Intercepts vkCreateDevice to enable VK_KHR_timeline_semaphore and the
 * timelineSemaphore feature. Uses the traditional pNext layer link chain
 * mechanism for proper dispatch.
 */

#include <stdint.h>
#include <stdlib.h>
#include <string.h>
#include <stdio.h>

#define VK_DEFINE_HANDLE(object) typedef struct object##_T* object;
#define VKAPI_CALL
#define VKAPI_PTR
#define VKAPI_ATTR
#define VK_TRUE  1
#define VK_FALSE 0
#define VK_SUCCESS 0

#define VK_STRUCTURE_TYPE_DEVICE_CREATE_INFO 3
#define VK_STRUCTURE_TYPE_LOADER_INSTANCE_CREATE_INFO 47
#define VK_STRUCTURE_TYPE_LOADER_DEVICE_CREATE_INFO 48
#define VK_STRUCTURE_TYPE_PHYSICAL_DEVICE_TIMELINE_SEMAPHORE_FEATURES 1000207000

#define VK_LAYER_LINK_INFO 0

typedef uint32_t VkBool32;
typedef uint32_t VkFlags;
typedef uint32_t VkStructureType;

VK_DEFINE_HANDLE(VkInstance)
VK_DEFINE_HANDLE(VkPhysicalDevice)
VK_DEFINE_HANDLE(VkDevice)

typedef struct VkBaseOutStructure {
    VkStructureType sType;
    struct VkBaseOutStructure *pNext;
} VkBaseOutStructure;

typedef void(VKAPI_PTR *PFN_vkVoidFunction)(void);
typedef PFN_vkVoidFunction(VKAPI_PTR *PFN_vkGetInstanceProcAddr)(VkInstance, const char *);
typedef PFN_vkVoidFunction(VKAPI_PTR *PFN_vkGetDeviceProcAddr)(VkDevice, const char *);

typedef struct VkAllocationCallbacks VkAllocationCallbacks;

typedef struct VkPhysicalDeviceFeatures {
    VkBool32 features[55];
} VkPhysicalDeviceFeatures;

typedef struct VkDeviceQueueCreateInfo {
    VkStructureType sType;
    const void *pNext;
    VkFlags flags;
    uint32_t queueFamilyIndex;
    uint32_t queueCount;
    const float *pQueuePriorities;
} VkDeviceQueueCreateInfo;

typedef struct VkDeviceCreateInfo {
    VkStructureType sType;
    const void *pNext;
    VkFlags flags;
    uint32_t queueCreateInfoCount;
    const VkDeviceQueueCreateInfo *pQueueCreateInfos;
    uint32_t enabledLayerCount;
    const char *const *ppEnabledLayerNames;
    uint32_t enabledExtensionCount;
    const char *const *ppEnabledExtensionNames;
    const VkPhysicalDeviceFeatures *pEnabledFeatures;
} VkDeviceCreateInfo;

typedef struct VkPhysicalDeviceTimelineSemaphoreFeatures {
    VkStructureType sType;
    void *pNext;
    VkBool32 timelineSemaphore;
} VkPhysicalDeviceTimelineSemaphoreFeatures;

typedef struct VkApplicationInfo {
    VkStructureType sType;
    const void *pNext;
    const char *pApplicationName;
    uint32_t applicationVersion;
    const char *pEngineName;
    uint32_t engineVersion;
    uint32_t apiVersion;
} VkApplicationInfo;

typedef struct VkInstanceCreateInfo {
    VkStructureType sType;
    const void *pNext;
    VkFlags flags;
    const VkApplicationInfo *pApplicationInfo;
    uint32_t enabledLayerCount;
    const char *const *ppEnabledLayerNames;
    uint32_t enabledExtensionCount;
    const char *const *ppEnabledExtensionNames;
} VkInstanceCreateInfo;

typedef int(VKAPI_PTR *PFN_vkCreateInstance)(const VkInstanceCreateInfo *,
                                              const VkAllocationCallbacks *, VkInstance *);
typedef int(VKAPI_PTR *PFN_vkCreateDevice)(VkPhysicalDevice, const VkDeviceCreateInfo *,
                                            const VkAllocationCallbacks *, VkDevice *);
typedef void(VKAPI_PTR *PFN_vkDestroyInstance)(VkInstance, const VkAllocationCallbacks *);
typedef void(VKAPI_PTR *PFN_vkDestroyDevice)(VkDevice, const VkAllocationCallbacks *);

/* Layer link structures */
typedef struct VkLayerInstanceLink_ {
    struct VkLayerInstanceLink_ *pNext;
    PFN_vkGetInstanceProcAddr pfnNextGetInstanceProcAddr;
    PFN_vkGetInstanceProcAddr pfnNextGetPhysicalDeviceProcAddr;
} VkLayerInstanceLink;

typedef struct VkLayerDeviceLink_ {
    struct VkLayerDeviceLink_ *pNext;
    PFN_vkGetInstanceProcAddr pfnNextGetInstanceProcAddr;
    PFN_vkGetDeviceProcAddr pfnNextGetDeviceProcAddr;
} VkLayerDeviceLink;

typedef struct {
    VkStructureType sType;
    const void *pNext;
    int function;
    union {
        VkLayerInstanceLink *pLayerInfo;
        VkLayerDeviceLink *pDeviceInfo;
    } u;
} VkLayerCreateInfo;

typedef enum VkNegotiateLayerStructType {
    LAYER_NEGOTIATE_INTERFACE_STRUCT = 1,
} VkNegotiateLayerStructType;

typedef struct VkNegotiateLayerInterface {
    VkNegotiateLayerStructType sType;
    void *pNext;
    uint32_t loaderLayerInterfaceVersion;
    PFN_vkGetInstanceProcAddr pfnGetInstanceProcAddr;
    PFN_vkGetDeviceProcAddr pfnGetDeviceProcAddr;
    PFN_vkGetInstanceProcAddr pfnGetPhysicalDeviceProcAddr;
} VkNegotiateLayerInterface;

/* Per-instance state */
static PFN_vkGetInstanceProcAddr g_nextGIPA = NULL;
static PFN_vkGetDeviceProcAddr g_nextGDPA = NULL;
static PFN_vkCreateDevice g_nextCreateDevice = NULL;
static PFN_vkDestroyInstance g_nextDestroyInstance = NULL;

static VkLayerCreateInfo *find_chain_info(const void *pNext, int function)
{
    VkBaseOutStructure *curr = (VkBaseOutStructure *)pNext;
    while (curr) {
        if (curr->sType == VK_STRUCTURE_TYPE_LOADER_INSTANCE_CREATE_INFO ||
            curr->sType == VK_STRUCTURE_TYPE_LOADER_DEVICE_CREATE_INFO) {
            VkLayerCreateInfo *ci = (VkLayerCreateInfo *)curr;
            if (ci->function == function)
                return ci;
        }
        curr = curr->pNext;
    }
    return NULL;
}

/* --- vkCreateInstance --- */
static int VKAPI_CALL hook_vkCreateInstance(
    const VkInstanceCreateInfo *pCreateInfo,
    const VkAllocationCallbacks *pAllocator,
    VkInstance *pInstance)
{
    VkLayerCreateInfo *chain = find_chain_info(pCreateInfo->pNext, VK_LAYER_LINK_INFO);
    if (!chain || !chain->u.pLayerInfo) {
        fprintf(stderr, "[force_timeline_sem] ERROR: no instance layer link\n");
        return -1;
    }

    PFN_vkGetInstanceProcAddr nextGIPA = chain->u.pLayerInfo->pfnNextGetInstanceProcAddr;
    chain->u.pLayerInfo = chain->u.pLayerInfo->pNext;

    PFN_vkCreateInstance nextCI =
        (PFN_vkCreateInstance)(void *)nextGIPA(NULL, "vkCreateInstance");
    if (!nextCI) {
        fprintf(stderr, "[force_timeline_sem] ERROR: cannot resolve vkCreateInstance\n");
        return -1;
    }

    int result = nextCI(pCreateInfo, pAllocator, pInstance);
    if (result != VK_SUCCESS)
        return result;

    g_nextGIPA = nextGIPA;
    g_nextDestroyInstance =
        (PFN_vkDestroyInstance)(void *)nextGIPA(*pInstance, "vkDestroyInstance");
    g_nextCreateDevice =
        (PFN_vkCreateDevice)(void *)nextGIPA(*pInstance, "vkCreateDevice");

    fprintf(stderr, "[force_timeline_sem] Instance created, nextCreateDevice=%p\n",
            (void *)(uintptr_t)g_nextCreateDevice);
    return VK_SUCCESS;
}

/* --- vkDestroyInstance --- */
static void VKAPI_CALL hook_vkDestroyInstance(VkInstance instance, const VkAllocationCallbacks *pAlloc)
{
    if (g_nextDestroyInstance) g_nextDestroyInstance(instance, pAlloc);
    g_nextGIPA = NULL;
    g_nextDestroyInstance = NULL;
    g_nextCreateDevice = NULL;
}

/* --- vkCreateDevice (the actual patching logic) --- */
static int VKAPI_CALL hook_vkCreateDevice(
    VkPhysicalDevice physicalDevice,
    const VkDeviceCreateInfo *pCreateInfo,
    const VkAllocationCallbacks *pAllocator,
    VkDevice *pDevice)
{
    fprintf(stderr, "[force_timeline_sem] hook_vkCreateDevice called!\n");

    /* Advance the device layer link chain for next layers */
    VkLayerCreateInfo *chain = find_chain_info(pCreateInfo->pNext, VK_LAYER_LINK_INFO);
    PFN_vkCreateDevice nextCD = NULL;

    if (chain && chain->u.pDeviceInfo) {
        PFN_vkGetInstanceProcAddr devNextGIPA = chain->u.pDeviceInfo->pfnNextGetInstanceProcAddr;
        PFN_vkGetDeviceProcAddr devNextGDPA = chain->u.pDeviceInfo->pfnNextGetDeviceProcAddr;
        chain->u.pDeviceInfo = chain->u.pDeviceInfo->pNext;
        nextCD = (PFN_vkCreateDevice)(void *)devNextGIPA(NULL, "vkCreateDevice");
        if (devNextGDPA)
            g_nextGDPA = devNextGDPA;
    }

    if (!nextCD)
        nextCD = g_nextCreateDevice;

    if (!nextCD) {
        fprintf(stderr, "[force_timeline_sem] ERROR: no vkCreateDevice to call\n");
        return -1;
    }

    VkDeviceCreateInfo patched = *pCreateInfo;

    /* Add VK_KHR_timeline_semaphore extension */
    int has_ext = 0;
    for (uint32_t i = 0; i < pCreateInfo->enabledExtensionCount; i++) {
        if (pCreateInfo->ppEnabledExtensionNames[i] &&
            strcmp(pCreateInfo->ppEnabledExtensionNames[i], "VK_KHR_timeline_semaphore") == 0) {
            has_ext = 1;
            break;
        }
    }

    const char **newExts = NULL;
    if (!has_ext) {
        newExts = malloc((pCreateInfo->enabledExtensionCount + 1) * sizeof(const char *));
        if (newExts) {
            memcpy(newExts, pCreateInfo->ppEnabledExtensionNames,
                   pCreateInfo->enabledExtensionCount * sizeof(const char *));
            newExts[pCreateInfo->enabledExtensionCount] = "VK_KHR_timeline_semaphore";
            patched.enabledExtensionCount = pCreateInfo->enabledExtensionCount + 1;
            patched.ppEnabledExtensionNames = newExts;
            fprintf(stderr, "[force_timeline_sem] Added VK_KHR_timeline_semaphore extension\n");
        }
    }

    /* Enable timelineSemaphore feature */
    int has_feature = 0;
    VkBaseOutStructure *curr = (VkBaseOutStructure *)pCreateInfo->pNext;
    while (curr) {
        if (curr->sType == VK_STRUCTURE_TYPE_PHYSICAL_DEVICE_TIMELINE_SEMAPHORE_FEATURES) {
            VkPhysicalDeviceTimelineSemaphoreFeatures *f =
                (VkPhysicalDeviceTimelineSemaphoreFeatures *)curr;
            f->timelineSemaphore = VK_TRUE;
            has_feature = 1;
            fprintf(stderr, "[force_timeline_sem] Enabled timelineSemaphore in existing pNext\n");
            break;
        }
        curr = curr->pNext;
    }

    VkPhysicalDeviceTimelineSemaphoreFeatures tsFeature;
    if (!has_feature) {
        memset(&tsFeature, 0, sizeof(tsFeature));
        tsFeature.sType = VK_STRUCTURE_TYPE_PHYSICAL_DEVICE_TIMELINE_SEMAPHORE_FEATURES;
        tsFeature.pNext = (void *)patched.pNext;
        tsFeature.timelineSemaphore = VK_TRUE;
        patched.pNext = &tsFeature;
        fprintf(stderr, "[force_timeline_sem] Prepended timelineSemaphore feature to pNext\n");
    }

    int result = nextCD(physicalDevice, &patched, pAllocator, pDevice);
    free(newExts);
    fprintf(stderr, "[force_timeline_sem] vkCreateDevice returned %d\n", result);
    return result;
}

/* --- vkGetInstanceProcAddr dispatcher --- */
static PFN_vkVoidFunction VKAPI_CALL hook_vkGetInstanceProcAddr(VkInstance instance, const char *pName)
{
    if (!pName) return NULL;

    if (strcmp(pName, "vkCreateInstance") == 0)
        return (PFN_vkVoidFunction)hook_vkCreateInstance;
    if (strcmp(pName, "vkDestroyInstance") == 0)
        return (PFN_vkVoidFunction)hook_vkDestroyInstance;
    if (strcmp(pName, "vkCreateDevice") == 0)
        return (PFN_vkVoidFunction)hook_vkCreateDevice;
    if (strcmp(pName, "vkGetInstanceProcAddr") == 0)
        return (PFN_vkVoidFunction)hook_vkGetInstanceProcAddr;

    if (g_nextGIPA)
        return g_nextGIPA(instance, pName);
    return NULL;
}

/* --- vkGetDeviceProcAddr dispatcher --- */
static PFN_vkVoidFunction VKAPI_CALL hook_vkGetDeviceProcAddr(VkDevice device, const char *pName)
{
    if (g_nextGDPA)
        return g_nextGDPA(device, pName);
    return NULL;
}

/* --- Negotiation --- */
VKAPI_ATTR int VKAPI_CALL
vkNegotiateLoaderLayerInterfaceVersion(VkNegotiateLayerInterface *pVersionStruct)
{
    if (!pVersionStruct || pVersionStruct->sType != LAYER_NEGOTIATE_INTERFACE_STRUCT)
        return -1;

    if (pVersionStruct->loaderLayerInterfaceVersion > 2)
        pVersionStruct->loaderLayerInterfaceVersion = 2;

    pVersionStruct->pfnGetInstanceProcAddr = hook_vkGetInstanceProcAddr;
    pVersionStruct->pfnGetDeviceProcAddr = hook_vkGetDeviceProcAddr;
    pVersionStruct->pfnGetPhysicalDeviceProcAddr = NULL;

    fprintf(stderr, "[force_timeline_sem] Layer negotiated (v%u)\n",
            pVersionStruct->loaderLayerInterfaceVersion);
    return VK_SUCCESS;
}
