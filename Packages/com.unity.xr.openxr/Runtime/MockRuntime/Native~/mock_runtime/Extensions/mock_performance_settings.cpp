#include "mock_performance_settings.h"

#define CHECK_PERF_SETTINGS_EXT()                       \
    if (nullptr == MockPerformanceSettings::Instance()) \
        return XR_ERROR_FUNCTION_UNSUPPORTED;

std::unique_ptr<MockPerformanceSettings> MockPerformanceSettings::s_ext;

void MockPerformanceSettings::Init(MockRuntime& runtime)
{
    s_ext.reset(new MockPerformanceSettings(runtime));
}

void MockPerformanceSettings::Deinit()
{
    s_ext.reset();
}

MockPerformanceSettings* MockPerformanceSettings::Instance()
{
    return s_ext.get();
}

MockPerformanceSettings::MockPerformanceSettings(MockRuntime& runtime)
    : m_Runtime(runtime)
{
}

XrResult MockPerformanceSettings::SetPerformanceLevel(XrSession session, XrPerfSettingsDomainEXT domain, XrPerfSettingsLevelEXT level)
{
    if (domain != XR_PERF_SETTINGS_DOMAIN_CPU_EXT &&
        domain != XR_PERF_SETTINGS_DOMAIN_GPU_EXT)
    {
        return XR_ERROR_VALIDATION_FAILURE;
    }

    if (level != XR_PERF_SETTINGS_LEVEL_POWER_SAVINGS_EXT &&
        level != XR_PERF_SETTINGS_LEVEL_SUSTAINED_LOW_EXT &&
        level != XR_PERF_SETTINGS_LEVEL_SUSTAINED_HIGH_EXT &&
        level != XR_PERF_SETTINGS_LEVEL_BOOST_EXT)
    {
        return XR_ERROR_VALIDATION_FAILURE;
    }

    m_PerformanceLevelHints[domain] = level;

    return XR_SUCCESS;
}

XrPerfSettingsLevelEXT MockPerformanceSettings::GetPerformanceLevelHint(XrPerfSettingsDomainEXT domain)
{
    if (m_PerformanceLevelHints.find(domain) != m_PerformanceLevelHints.end())
    {
        return m_PerformanceLevelHints[domain];
    }

    return XR_PERF_SETTINGS_LEVEL_SUSTAINED_HIGH_EXT; // Default value per spec
}

XrPerfSettingsNotificationLevelEXT MockPerformanceSettings::GetPerformanceSettingsNotificationLevel(XrPerfSettingsDomainEXT domain, XrPerfSettingsSubDomainEXT subdomain)
{
    auto pair = std::make_pair(domain, subdomain);
    return m_notificationLevel[pair];
}

void MockPerformanceSettings::SetPerformanceSettingsNotificationLevel(XrPerfSettingsDomainEXT domain, XrPerfSettingsSubDomainEXT subdomain, XrPerfSettingsNotificationLevelEXT nextLevel)
{
    auto pair = std::make_pair(domain, subdomain);
    m_notificationLevel[pair] = nextLevel;
}

extern "C" XrResult UNITY_INTERFACE_EXPORT XRAPI_PTR
xrPerfSettingsSetPerformanceLevelEXT(
    XrSession session,
    XrPerfSettingsDomainEXT domain,
    XrPerfSettingsLevelEXT level)
{
    LOG_FUNC();
    CHECK_SESSION(session);
    CHECK_PERF_SETTINGS_EXT();
    MOCK_HOOK_BEFORE();

    const XrResult result =
        MockPerformanceSettings::Instance()->SetPerformanceLevel(session, domain, level);

    MOCK_HOOK_AFTER(result);

    return result;
}

XrResult MockPerformanceSettings_GetInstanceProcAddr(const char* name, PFN_xrVoidFunction* function)
{
    GET_PROC_ADDRESS(xrPerfSettingsSetPerformanceLevelEXT);
    return XR_ERROR_FUNCTION_UNSUPPORTED;
}

#undef CHECK_PERF_SETTINGS_EXT
