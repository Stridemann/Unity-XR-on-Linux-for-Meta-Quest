/*
 * OpenXR API Layer: strip_android
 *
 * Intercepts xrCreateApiLayerInstance to remove XR_KHR_android_create_instance
 * from the enabled extensions list and strip XrInstanceCreateInfoAndroidKHR
 * from the next-pointer chain.
 *
 * This allows the Android-compiled libUnityOpenXR.so to create an
 * OpenXR instance on desktop Linux where the runtime (SteamVR) does
 * not support Android-specific extensions.
 */

#include <openxr/openxr.h>
#include <openxr/openxr_loader_negotiation.h>
#include <string.h>
#include <stdlib.h>
#include <stdio.h>

#define XR_TYPE_INSTANCE_CREATE_INFO_ANDROID_KHR_VALUE 1000008000

static PFN_xrGetInstanceProcAddr g_nextGetInstanceProcAddr = NULL;

typedef struct GenericBaseStruct {
    XrStructureType type;
    const struct GenericBaseStruct *next;
} GenericBaseStruct;

static XrResult strip_android_xrCreateApiLayerInstance(
    const XrInstanceCreateInfo *info,
    const XrApiLayerCreateInfo *layerInfo,
    XrInstance *instance)
{
    XrInstanceCreateInfo patched = *info;

    /* --- Strip XR_KHR_android_create_instance from enabled extensions --- */
    const char **filtered = NULL;
    uint32_t filteredCount = 0;

    if (info->enabledExtensionCount > 0) {
        filtered = (const char **)malloc(info->enabledExtensionCount * sizeof(const char *));
        if (!filtered)
            return XR_ERROR_OUT_OF_MEMORY;

        for (uint32_t i = 0; i < info->enabledExtensionCount; i++) {
            if (strcmp(info->enabledExtensionNames[i], "XR_KHR_android_create_instance") != 0) {
                filtered[filteredCount++] = info->enabledExtensionNames[i];
            } else {
                fprintf(stderr, "[strip_android] Removed XR_KHR_android_create_instance from enabled extensions\n");
            }
        }
        patched.enabledExtensionNames = filtered;
        patched.enabledExtensionCount = filteredCount;
    }

    /* --- Strip XrInstanceCreateInfoAndroidKHR from the next chain --- */
    const GenericBaseStruct *prev = NULL;
    const GenericBaseStruct *curr = (const GenericBaseStruct *)info->next;
    const GenericBaseStruct *newHead = curr;

    while (curr != NULL) {
        if ((int32_t)curr->type == XR_TYPE_INSTANCE_CREATE_INFO_ANDROID_KHR_VALUE) {
            fprintf(stderr, "[strip_android] Removed XrInstanceCreateInfoAndroidKHR (type=%d) from next chain\n",
                    (int)curr->type);
            if (prev == NULL) {
                newHead = curr->next;
            } else {
                ((GenericBaseStruct *)prev)->next = curr->next;
            }
            curr = curr->next;
            continue;
        }
        prev = curr;
        curr = curr->next;
    }

    patched.next = newHead;

    /* Forward to the next layer / runtime via the layer chain */
    XrApiLayerCreateInfo nextLayerInfo = *layerInfo;
    nextLayerInfo.nextInfo = layerInfo->nextInfo->next;

    g_nextGetInstanceProcAddr = layerInfo->nextInfo->nextGetInstanceProcAddr;

    XrResult result = layerInfo->nextInfo->nextCreateApiLayerInstance(
        &patched, &nextLayerInfo, instance);

    free(filtered);

    if (result == XR_SUCCESS) {
        fprintf(stderr, "[strip_android] xrCreateInstance succeeded!\n");
    } else {
        fprintf(stderr, "[strip_android] xrCreateInstance returned %d\n", (int)result);
    }

    return result;
}

static XrResult strip_android_xrGetInstanceProcAddr(
    XrInstance instance,
    const char *name,
    PFN_xrVoidFunction *function)
{
    /* Pass everything through to the next layer / runtime */
    return g_nextGetInstanceProcAddr(instance, name, function);
}

XrResult xrNegotiateLoaderApiLayerInterface(
    const XrNegotiateLoaderInfo *loaderInfo,
    const char *layerName,
    XrNegotiateApiLayerRequest *apiLayerRequest)
{
    if (loaderInfo == NULL || layerName == NULL || apiLayerRequest == NULL)
        return XR_ERROR_INITIALIZATION_FAILED;

    if (loaderInfo->structType != XR_LOADER_INTERFACE_STRUCT_LOADER_INFO ||
        loaderInfo->structVersion != XR_LOADER_INFO_STRUCT_VERSION ||
        loaderInfo->structSize != sizeof(XrNegotiateLoaderInfo))
        return XR_ERROR_INITIALIZATION_FAILED;

    if (apiLayerRequest->structType != XR_LOADER_INTERFACE_STRUCT_API_LAYER_REQUEST ||
        apiLayerRequest->structVersion != XR_API_LAYER_INFO_STRUCT_VERSION ||
        apiLayerRequest->structSize != sizeof(XrNegotiateApiLayerRequest))
        return XR_ERROR_INITIALIZATION_FAILED;

    if (loaderInfo->minInterfaceVersion > XR_CURRENT_LOADER_API_LAYER_VERSION ||
        loaderInfo->maxInterfaceVersion < XR_CURRENT_LOADER_API_LAYER_VERSION)
        return XR_ERROR_INITIALIZATION_FAILED;

    apiLayerRequest->layerInterfaceVersion = XR_CURRENT_LOADER_API_LAYER_VERSION;
    apiLayerRequest->layerApiVersion = XR_CURRENT_API_VERSION;
    apiLayerRequest->getInstanceProcAddr = strip_android_xrGetInstanceProcAddr;
    apiLayerRequest->createApiLayerInstance = strip_android_xrCreateApiLayerInstance;

    fprintf(stderr, "[strip_android] API layer negotiated successfully\n");
    return XR_SUCCESS;
}
