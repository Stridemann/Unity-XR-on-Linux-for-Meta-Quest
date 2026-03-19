#include "../mock.h"

#if defined(XR_USE_GRAPHICS_API_D3D12)

struct MockD3D12
{
    ID3D12Device* device = nullptr;
};

static MockD3D12 s_MockD3D12 = {};

extern "C" XrResult UNITY_INTERFACE_EXPORT XRAPI_PTR xrGetD3D12GraphicsRequirementsKHR(
    XrInstance instance,
    XrSystemId systemId,
    XrGraphicsRequirementsD3D12KHR* graphicsRequirements)
{
    IDXGIAdapter* pAdapter;
    std::vector<IDXGIAdapter*> vAdapters;
    IDXGIFactory* pFactory = NULL;

    // Create a DXGIFactory object.
    if (FAILED(CreateDXGIFactory(__uuidof(IDXGIFactory), (void**)&pFactory)))
        return XR_ERROR_RUNTIME_FAILURE;

    XrResult xrresult = XR_ERROR_RUNTIME_FAILURE;
    graphicsRequirements->adapterLuid = {};

    for (UINT i = 0; pFactory->EnumAdapters(i, &pAdapter) != DXGI_ERROR_NOT_FOUND; ++i)
    {
        DXGI_ADAPTER_DESC desc = {};
        pAdapter->GetDesc(&desc);
        graphicsRequirements->adapterLuid = desc.AdapterLuid;
        xrresult = XR_SUCCESS;
        break;
    }

    if (pFactory)
        pFactory->Release();

    return xrresult;
}

extern "C" XrResult UNITY_INTERFACE_EXPORT XRAPI_PTR MockD3D12_xrEnumerateSwapchainFormats(XrSession session, uint32_t formatCapacityInput, uint32_t* formatCountOutput, int64_t* formats)
{
    if (nullptr == formatCountOutput)
        return XR_ERROR_VALIDATION_FAILURE;

    *formatCountOutput = 6;

    if (formatCapacityInput == 0)
        return XR_SUCCESS;

    if (nullptr == formats)
        return XR_ERROR_VALIDATION_FAILURE;

    if (formatCapacityInput < 6)
        return XR_ERROR_SIZE_INSUFFICIENT;

    formats[0] = DXGI_FORMAT_R8G8B8A8_UNORM_SRGB;
    formats[1] = DXGI_FORMAT_B8G8R8A8_UNORM_SRGB;
    formats[2] = DXGI_FORMAT_R8G8B8A8_UNORM;
    formats[3] = DXGI_FORMAT_B8G8R8A8_UNORM;
    formats[4] = DXGI_FORMAT_D16_UNORM;
    formats[5] = DXGI_FORMAT_D32_FLOAT_S8X24_UINT;
    return XR_SUCCESS;
}

static XrResult MockD3D12_xrCreateSwapchain(XrSession session, const XrSwapchainCreateInfo* createInfo, XrSwapchain* swapchain)
{
    if (nullptr == s_MockD3D12.device)
    {
        MOCK_TRACE_DEBUG("Mock device is null");
        return XR_ERROR_RUNTIME_FAILURE;
    }

    D3D12_HEAP_PROPERTIES heapProps{};
    heapProps.Type = D3D12_HEAP_TYPE_DEFAULT;
    heapProps.CPUPageProperty = D3D12_CPU_PAGE_PROPERTY_UNKNOWN;
    heapProps.MemoryPoolPreference = D3D12_MEMORY_POOL_UNKNOWN;

    D3D12_RESOURCE_DESC desc{};
    desc.Dimension = D3D12_RESOURCE_DIMENSION_TEXTURE2D;
    desc.Width = createInfo->width;
    desc.Height = createInfo->height;
    desc.MipLevels = createInfo->mipCount;
    desc.DepthOrArraySize = createInfo->arraySize;
    desc.SampleDesc.Count = createInfo->sampleCount;
    desc.SampleDesc.Quality = 0;
    desc.Flags = D3D12_RESOURCE_FLAG_ALLOW_RENDER_TARGET;

    if (createInfo->usageFlags & XR_SWAPCHAIN_USAGE_DEPTH_STENCIL_ATTACHMENT_BIT)
    {
        if (createInfo->format == DXGI_FORMAT_D16_UNORM)
            desc.Format = DXGI_FORMAT_R16_TYPELESS;
        else
            desc.Format = DXGI_FORMAT_R32G8X24_TYPELESS;

        desc.Flags |= D3D12_RESOURCE_FLAG_ALLOW_DEPTH_STENCIL;
    }
    else
    {
        desc.Format = DXGI_FORMAT_R8G8B8A8_TYPELESS;
    }

    ID3D12Resource* texture;
    const HRESULT result = s_MockD3D12.device->CreateCommittedResource(
        &heapProps,
        D3D12_HEAP_FLAG_NONE,
        &desc,
        D3D12_RESOURCE_STATE_RENDER_TARGET,
        nullptr,
        IID_PPV_ARGS(&texture));
    if (FAILED(result))
    {
        MOCK_TRACE_DEBUG("ID3D12Device::CreateCommittedResource failed, HRESULT: 0x%08x", result);
        return XR_ERROR_RUNTIME_FAILURE;
    }
    else
    {
        MOCK_TRACE_DEBUG("ID3D12Device::CreateCommittedResource success");
    }

    *swapchain = (XrSwapchain)texture;

    return XR_SUCCESS;
}

extern "C" XrResult UNITY_INTERFACE_EXPORT XRAPI_PTR MockD3D12_xrCreateSwapchainHook(XrSession session, const XrSwapchainCreateInfo* createInfo, XrSwapchain* swapchain)
{
    LOG_FUNC();
    MOCK_HOOK_NAMED("xrCreateSwapchain", MockD3D12_xrCreateSwapchain(session, createInfo, swapchain));
}

extern "C" XrResult UNITY_INTERFACE_EXPORT XRAPI_PTR MockD3D12_xrDestroySwapChain(XrSwapchain swapchain)
{
    ID3D12Resource* texture = (ID3D12Resource*)swapchain;
    texture->Release();
    return XR_SUCCESS;
}

extern "C" XrResult UNITY_INTERFACE_EXPORT XRAPI_PTR MockD3D12_xrEnumerateSwapchainImages(XrSwapchain swapchain, uint32_t imageCapacityInput, uint32_t* imageCountOutput, XrSwapchainImageBaseHeader* images)
{
    LOG_FUNC();

    *imageCountOutput = 1;

    if (images == nullptr)
        return XR_SUCCESS;

    if (imageCapacityInput < *imageCountOutput)
        return XR_ERROR_VALIDATION_FAILURE;

    XrSwapchainImageD3D12KHR* d3d12images = (XrSwapchainImageD3D12KHR*)images;
    d3d12images[0].texture = (ID3D12Resource*)swapchain;
    return XR_SUCCESS;
}

extern "C" XrResult UNITY_INTERFACE_EXPORT XRAPI_PTR MockD3D12_xrAcquireSwapchainImage(XrSwapchain swapchain, const XrSwapchainImageAcquireInfo* acquireInfo, uint32_t* index)
{
    LOG_FUNC();

    *index = 0;

    return XR_SUCCESS;
}

extern "C" XrResult UNITY_INTERFACE_EXPORT XRAPI_PTR MockD3D12_xrReleaseSwapchainImage(XrSwapchain swapchain, const XrSwapchainImageReleaseInfo* releaseInfo)
{
    LOG_FUNC();
    return XR_SUCCESS;
}

/// <summary>
/// Hook xrCreateSession to get the necessary D3D12 handles
/// </summary>
extern "C" XrResult UNITY_INTERFACE_EXPORT XRAPI_PTR xrCreateSession(XrInstance instance, const XrSessionCreateInfo* createInfo, XrSession* session);
extern "C" XrResult UNITY_INTERFACE_EXPORT XRAPI_PTR MockD3D12_xrCreateSession(XrInstance instance, const XrSessionCreateInfo* createInfo, XrSession* session)
{
    s_MockD3D12 = {};

    if (createInfo->next != nullptr)
    {
        XrGraphicsBindingD3D12KHR* bindings = (XrGraphicsBindingD3D12KHR*)createInfo->next;
        if (bindings->type == XR_TYPE_GRAPHICS_BINDING_D3D12_KHR)
        {
            s_MockD3D12.device = bindings->device;
        }
    }

    return xrCreateSession(instance, createInfo, session);
}

XrResult MockD3D12_GetInstanceProcAddr(const char* name, PFN_xrVoidFunction* function)
{
    LOG_FUNC();
    GET_PROC_ADDRESS_REMAP(xrCreateSession, MockD3D12_xrCreateSession)
    GET_PROC_ADDRESS_REMAP(xrEnumerateSwapchainFormats, MockD3D12_xrEnumerateSwapchainFormats)
    GET_PROC_ADDRESS_REMAP(xrCreateSwapchain, MockD3D12_xrCreateSwapchainHook)
    GET_PROC_ADDRESS_REMAP(xrDestroySwapChain, MockD3D12_xrDestroySwapChain)
    GET_PROC_ADDRESS_REMAP(xrEnumerateSwapchainImages, MockD3D12_xrEnumerateSwapchainImages)
    GET_PROC_ADDRESS_REMAP(xrAcquireSwapchainImage, MockD3D12_xrAcquireSwapchainImage)
    GET_PROC_ADDRESS_REMAP(xrReleaseSwapchainImage, MockD3D12_xrReleaseSwapchainImage)
    GET_PROC_ADDRESS(xrGetD3D12GraphicsRequirementsKHR)
    return XR_ERROR_FUNCTION_UNSUPPORTED;
}

#endif
