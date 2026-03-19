#include "mock_khr_android_thread_settings.h"

#ifdef XR_USE_PLATFORM_ANDROID

#define CHECK_EXT_INIT()                                  \
    if (nullptr == MockAndroidThreadSettings::Instance()) \
        return XR_ERROR_FUNCTION_UNSUPPORTED;

std::unique_ptr<MockAndroidThreadSettings> MockAndroidThreadSettings::s_Instance;

MockAndroidThreadSettings* MockAndroidThreadSettings::Instance()
{
    return s_Instance.get();
}

void MockAndroidThreadSettings::Init(MockRuntime& runtime)
{
    s_Instance.reset(new MockAndroidThreadSettings(runtime));
}

void MockAndroidThreadSettings::Deinit()
{
    s_Instance.reset();
}

MockAndroidThreadSettings::MockAndroidThreadSettings(MockRuntime& runtime)
    : m_Runtime{runtime}
{
}

XrResult MockAndroidThreadSettings::SetAndroidApplicationThread(XrAndroidThreadTypeKHR threadType, uint32_t threadId)
{
    m_AssignedThreadTypes[threadId] = threadType;
    return XR_SUCCESS;
}

bool MockAndroidThreadSettings::IsAndroidThreadTypeRegistered(XrAndroidThreadTypeKHR threadType) const
{
    for (auto i = m_AssignedThreadTypes.cbegin(); i != m_AssignedThreadTypes.cend(); ++i)
    {
        if (i->second == threadType)
        {
            return true;
        }
    }
    return false;
}

uint32_t MockAndroidThreadSettings::GetRegisteredAndroidThreadsCount() const
{
    return m_AssignedThreadTypes.size();
}

extern "C" XrResult UNITY_INTERFACE_EXPORT XRAPI_PTR
xrSetAndroidApplicationThreadKHR(
    XrSession session,
    XrAndroidThreadTypeKHR threadType,
    uint32_t threadId)
{
    LOG_FUNC();
    CHECK_SESSION(session);
    CHECK_EXT_INIT();
    MOCK_HOOK_BEFORE();

    const XrResult result =
        MockAndroidThreadSettings::Instance()->SetAndroidApplicationThread(
            threadType,
            threadId);

    MOCK_HOOK_AFTER(result);

    return result;
}

XrResult MockAndroidThreadSettings_GetInstanceProcAddr(const char* name, PFN_xrVoidFunction* function)
{
    GET_PROC_ADDRESS(xrSetAndroidApplicationThreadKHR)
    return XR_ERROR_FEATURE_UNSUPPORTED;
}

#undef CHECK_EXT_INIT
#endif // XR_USE_PLATFORM_ANDROID