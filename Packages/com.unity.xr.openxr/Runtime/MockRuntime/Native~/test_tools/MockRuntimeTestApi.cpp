#include "mock.h"

#include <memory>
#include <queue>
#include <unordered_map>
#include <vector>

// to enable logging, uncomment this line and rebuild with bee, or define this within
// the build script itself.
//#define ENABLE_TRACE_LOGGING

#ifdef ENABLE_TRACE_LOGGING

IUnityXRTrace* s_Trace = nullptr;
extern "C" void UNITY_INTERFACE_EXPORT UNITY_INTERFACE_API UnityPluginLoad(IUnityInterfaces* unityInterfaces)
{
    s_Trace = unityInterfaces->Get<IUnityXRTrace>();
}

#define TRACE_LOG(STRING, ...) MOCK_TRACE_LOG(STRING, ##__VA_ARGS__)

#else

#define TRACE_LOG(STRING, ...)

#endif //ENABLE_TRACE_LOGGING

// UnityPlugin events

typedef void (*GetSystemProperties_MockFunction_t)(void* systemPropertiesStruct);

class MockRuntimeTestApi
{
public:
    static MockRuntimeTestApi& GetOrCreate();
    static void Cleanup();

    PFN_xrGetInstanceProcAddr InstallGetInstanceProcAddrInterceptor(PFN_xrGetInstanceProcAddr oldFunc);

    void AddSupportedExtension(const char* extensionName, uint32_t extensionVersion);
    void SetSysPropertiesFunctionForXrStructureType(XrStructureType type, GetSystemProperties_MockFunction_t function);
    void SetFunctionForInterceptor(const char* methodName, PFN_xrVoidFunction function);
    void EnqueueMockEventData(const XrEventDataBuffer* eventData);
    bool DequeueMockEventData(XrEventDataBuffer* eventData);

private:
    const std::vector<XrExtensionProperties>& GetSupportedExtensions()
    {
        return m_SupportedExtensionProperties;
    }

    PFN_xrGetInstanceProcAddr oldInstanceProcAddr = nullptr;
    PFN_xrEnumerateInstanceExtensionProperties oldEnumerateInstanceProps = nullptr;
    PFN_xrGetSystemProperties oldGetSystemProps = nullptr;
    PFN_xrPollEvent oldPollEvent = nullptr;

    std::vector<XrExtensionProperties> m_SupportedExtensionProperties;
    std::unordered_map<uint32_t, GetSystemProperties_MockFunction_t> m_SystemPropertyTypeFunctions;
    std::unordered_map<std::string, PFN_xrVoidFunction> m_GetInstanceProcAddrFunctions;
    std::queue<XrEventDataBuffer> m_EventQueue;

    static XrResult XRAPI_PTR GetInstanceProcAddr_InterceptorFunction(XrInstance instance, const char* name, PFN_xrVoidFunction* function);
    static XrResult XRAPI_PTR EnumerateInstanceExtensionProperties(const char* layerName, uint32_t propertyCapacityInput, uint32_t* propertyCountOutput, XrExtensionProperties* properties);
    static XrResult XRAPI_PTR GetSystemProperties(XrInstance instance, XrSystemId systemId, XrSystemProperties* properties);
    static XrResult XRAPI_PTR PollEvent(XrInstance instance, XrEventDataBuffer* eventData);

    static std::shared_ptr<MockRuntimeTestApi> s_Instance;
};

std::shared_ptr<MockRuntimeTestApi> MockRuntimeTestApi::s_Instance;

MockRuntimeTestApi& MockRuntimeTestApi::GetOrCreate()
{
    if (!s_Instance)
    {
        s_Instance = std::make_shared<MockRuntimeTestApi>();
    }
    return *s_Instance;
}

void MockRuntimeTestApi::Cleanup()
{
    s_Instance.reset();
}

// THE TEST "API" SET

PFN_xrGetInstanceProcAddr MockRuntimeTestApi::InstallGetInstanceProcAddrInterceptor(PFN_xrGetInstanceProcAddr oldFunc)
{
    oldInstanceProcAddr = oldFunc;
    return GetInstanceProcAddr_InterceptorFunction;
}

void MockRuntimeTestApi::AddSupportedExtension(const char* extensionName, uint32_t extensionVersion)
{
    XrExtensionProperties newSupportedExtension = {};

    newSupportedExtension.type = XR_TYPE_EXTENSION_PROPERTIES;
    newSupportedExtension.next = nullptr;
    strcpy(newSupportedExtension.extensionName, extensionName);
    newSupportedExtension.extensionVersion = extensionVersion;

    m_SupportedExtensionProperties.emplace_back(newSupportedExtension);
}

void MockRuntimeTestApi::SetSysPropertiesFunctionForXrStructureType(XrStructureType type, GetSystemProperties_MockFunction_t function)
{
    if (function != nullptr)
    {
        m_SystemPropertyTypeFunctions[type] = function;
    }
    else
    {
        auto it = m_SystemPropertyTypeFunctions.find(type);
        if (it != m_SystemPropertyTypeFunctions.end())
        {
            m_SystemPropertyTypeFunctions.erase(it);
        }
    }
}

void MockRuntimeTestApi::SetFunctionForInterceptor(const char* methodName, PFN_xrVoidFunction function)
{
    if (function != nullptr)
    {
        m_GetInstanceProcAddrFunctions[std::string(methodName)] = function;
    }
    else
    {
        auto it = m_GetInstanceProcAddrFunctions.find(std::string(methodName));
        if (it != m_GetInstanceProcAddrFunctions.end())
        {
            m_GetInstanceProcAddrFunctions.erase(it);
        }
    }
}

void MockRuntimeTestApi::EnqueueMockEventData(const XrEventDataBuffer* eventData)
{
    m_EventQueue.push(*eventData);
}

bool MockRuntimeTestApi::DequeueMockEventData(XrEventDataBuffer* eventData)
{
    if (m_EventQueue.size() > 0)
    {
        *eventData = m_EventQueue.front();
        m_EventQueue.pop();
        return true;
    }

    return false;
}

// OPENXR FUNCTION OVERRIDES

XrResult XRAPI_PTR MockRuntimeTestApi::PollEvent(XrInstance instance, XrEventDataBuffer* eventData)
{
    TRACE_LOG("MockRuntimeTestApi::PollEvent(%d)\n", instance);

    XrResult result = XR_EVENT_UNAVAILABLE;

    auto myInstance = MockRuntimeTestApi::s_Instance;

    if (myInstance->oldPollEvent != nullptr)
    {
        result = myInstance->oldPollEvent(instance, eventData);
    }

    // at this point if no event was polled by the previous function
    // then we can service our own queue

    if (result == XR_EVENT_UNAVAILABLE)
    {
        // change to success if we have our own event to dequeue
        auto dequeueSuccess = myInstance->DequeueMockEventData(eventData);
        if (dequeueSuccess)
        {
            TRACE_LOG("--> Successfully dequeued event of type: %d\n", eventData->type);
        }
        result = dequeueSuccess ? XR_SUCCESS : result;
    }

    return result;
}

XrResult XRAPI_PTR MockRuntimeTestApi::GetSystemProperties(XrInstance instance, XrSystemId systemId, XrSystemProperties* properties)
{
    TRACE_LOG("MockRuntimeTestApi::GetSystemProperties(%d, %d)\n", instance, systemId);

    auto myInstance = MockRuntimeTestApi::s_Instance;

    if (myInstance->oldGetSystemProps != nullptr)
    {
        myInstance->oldGetSystemProps(instance, systemId, properties);
    }

    auto next = properties->next;

    while (next != nullptr)
    {
        auto typedProperties = (XrBaseOutStructure*)next;

        auto iter = myInstance->m_SystemPropertyTypeFunctions.find(typedProperties->type);
        if (iter != myInstance->m_SystemPropertyTypeFunctions.end() && iter->second != nullptr)
        {
            TRACE_LOG("--> Calling system properties function for type: %d\n", typedProperties->type);
            iter->second(typedProperties);
        }

        next = typedProperties->next;
    }

    return XR_SUCCESS;
}

XrResult XRAPI_PTR MockRuntimeTestApi::EnumerateInstanceExtensionProperties(const char* layerName, uint32_t propertyCapacityInput, uint32_t* propertyCountOutput, XrExtensionProperties* properties)
{
    TRACE_LOG("MockRuntimeTestApi::EnumerateInstanceExtensionProperties(%s, %d)\n", layerName, propertyCapacityInput);

    auto myInstance = MockRuntimeTestApi::s_Instance;

    uint32_t totalPropertyCount = 0;

    if (myInstance->oldEnumerateInstanceProps != nullptr)
    {
        myInstance->oldEnumerateInstanceProps(layerName, propertyCapacityInput, propertyCountOutput, properties);
        totalPropertyCount += *propertyCountOutput;
    }

    uint32_t prevEnumerationCount = totalPropertyCount;
    totalPropertyCount += (uint32_t)MockRuntimeTestApi::GetOrCreate().GetSupportedExtensions().size();

    *propertyCountOutput = totalPropertyCount;

    if (propertyCapacityInput == 0)
        return XR_SUCCESS;

    if (propertyCapacityInput < totalPropertyCount)
        return XR_ERROR_VALIDATION_FAILURE;

    uint32_t count = prevEnumerationCount;
    while (count < totalPropertyCount)
    {
        auto addedExtension = MockRuntimeTestApi::GetOrCreate().GetSupportedExtensions().at(count - prevEnumerationCount);
        TRACE_LOG("--> Adding extension named ['%s'] to enumeration.\n", addedExtension.extensionName);
        properties[count] = addedExtension;
        ++count;
    }

    return XR_SUCCESS;
}

XrResult XRAPI_PTR MockRuntimeTestApi::GetInstanceProcAddr_InterceptorFunction(XrInstance instance, const char* name, PFN_xrVoidFunction* function)
{
    TRACE_LOG("MockRuntimeTestApi::GetInstanceProcAddr_InterceptorFunction(%s)\n", name);

    XrResult result = XR_SUCCESS;

    auto myInstance = MockRuntimeTestApi::s_Instance;

    if (myInstance->oldInstanceProcAddr != nullptr)
    {
        result = myInstance->oldInstanceProcAddr(instance, name, function);
    }
    else
    {
        result = XR_ERROR_RUNTIME_FAILURE;
    }

    // ... if we are enumerating through extensions via "xrEnumerateInstanceExtensionProperties"
    // then return the set that have been added.
    if (0 == strcmp(name, "xrEnumerateInstanceExtensionProperties"))
    {
        // do not null check here since caching the old pointer is bad -- it could change.
        myInstance->oldEnumerateInstanceProps = (PFN_xrEnumerateInstanceExtensionProperties)*function;
        *function = (PFN_xrVoidFunction)MockRuntimeTestApi::EnumerateInstanceExtensionProperties;
        TRACE_LOG("--> Returning test fixture enumeration function.\n");
        return XR_SUCCESS;
    }

    // ... if we are providing type-specific system properties via "xrGetSystemProperties"
    // then be sure to execute the designated mock function.
    if (0 == strcmp(name, "xrGetSystemProperties"))
    {
        // do not null check here since caching the old pointer is bad -- it could change.
        myInstance->oldGetSystemProps = (PFN_xrGetSystemProperties)*function;
        *function = (PFN_xrVoidFunction)MockRuntimeTestApi::GetSystemProperties;
        TRACE_LOG("--> Returning test fixture system properties function.\n");
        return XR_SUCCESS;
    }

    // ... if we are servicing the event queue
    if (0 == strcmp(name, "xrPollEvent"))
    {
        // do not null check here since caching the old pointer is bad -- it could change.
        myInstance->oldPollEvent = (PFN_xrPollEvent)*function;
        *function = (PFN_xrVoidFunction)MockRuntimeTestApi::PollEvent;
        TRACE_LOG("--> Returning test fixture poll event function.\n");
        return XR_SUCCESS;
    }

    // ... otherwise look into the mock function set which are bound by name
    auto it = myInstance->m_GetInstanceProcAddrFunctions.find(std::string(name));
    if (it != myInstance->m_GetInstanceProcAddrFunctions.end())
    {
        TRACE_LOG("--> registered function was found.\n");
        *function = it->second;
        result = XR_SUCCESS;
    }
    else
    {
        TRACE_LOG("--> registered function was NOT found.\n");
    }

    return result;
}

// NATIVE API FUNCTIONS

#define EXPORT(__retval) extern "C" __retval UNITY_INTERFACE_EXPORT UNITY_INTERFACE_API

EXPORT(void)
MockRuntimeTestApi_Cleanup()
{
    TRACE_LOG("MockRuntimeTestApi_Cleanup()\n");

    MockRuntimeTestApi::Cleanup();
}

EXPORT(PFN_xrGetInstanceProcAddr)
MockRuntimeTestApi_InstallGetInstanceProcAddrInterceptor(PFN_xrGetInstanceProcAddr oldFunc)
{
    TRACE_LOG("MockRuntimeTestApi_InstallGetInstanceProcAddrInterceptor()\n");

    return MockRuntimeTestApi::GetOrCreate().InstallGetInstanceProcAddrInterceptor(oldFunc);
}

EXPORT(void)
MockRuntimeTestApi_AddSupportedExtension(const char* extensionName, uint32_t extensionVersion)
{
    TRACE_LOG("MockRuntimeTestApi_AddSupportedExtension(%s, %d)\n", extensionName, extensionVersion);

    MockRuntimeTestApi::GetOrCreate().AddSupportedExtension(extensionName, extensionVersion);
}

EXPORT(void)
MockRuntimeTestApi_SetSysPropertiesFunctionForXrStructureType(XrStructureType type, GetSystemProperties_MockFunction_t function)
{
    TRACE_LOG("MockRuntimeTestApi_SetSysPropertiesFunctionForXrStructureType(%d)\n", type);

    MockRuntimeTestApi::GetOrCreate().SetSysPropertiesFunctionForXrStructureType(type, function);
}

EXPORT(void)
MockRuntimeTestApi_SetFunctionForInterceptor(const char* methodName, PFN_xrVoidFunction function)
{
    TRACE_LOG("MockRuntimeTestApi_SetFunctionForInterceptor('%s') called with function that is %s\n", methodName, function != nullptr ? "NOT null" : "NULL");

    MockRuntimeTestApi::GetOrCreate().SetFunctionForInterceptor(methodName, function);
}

EXPORT(void)
MockRuntimeTestApi_EnqueueMockEventData(const XrEventDataBuffer* eventData)
{
    TRACE_LOG("MockRuntimeTestApi_EnqueueMockEventData('%d')\n", eventData->type);

    MockRuntimeTestApi::GetOrCreate().EnqueueMockEventData(eventData);
}
