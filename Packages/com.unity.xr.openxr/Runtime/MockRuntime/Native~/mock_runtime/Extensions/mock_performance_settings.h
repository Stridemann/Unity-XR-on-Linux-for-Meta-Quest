#pragma once

#include <map>
#include <memory>

#include "../mock.h"
#include "IUnityInterface.h"
#include "openxr/openxr.h"
#include "openxr/openxr_platform_defines.h"

class MockRuntime;

class MockPerformanceSettings
{
public:
    static void Init(MockRuntime& runtime);
    static void Deinit();
    static MockPerformanceSettings* Instance();

    XrResult SetPerformanceLevel(XrSession session, XrPerfSettingsDomainEXT domain, XrPerfSettingsLevelEXT level);
    XrPerfSettingsLevelEXT GetPerformanceLevelHint(XrPerfSettingsDomainEXT domain);

    XrPerfSettingsNotificationLevelEXT GetPerformanceSettingsNotificationLevel(XrPerfSettingsDomainEXT domain, XrPerfSettingsSubDomainEXT subdomain);
    void SetPerformanceSettingsNotificationLevel(XrPerfSettingsDomainEXT domain, XrPerfSettingsSubDomainEXT subdomain, XrPerfSettingsNotificationLevelEXT nextLevel);

private:
    static std::unique_ptr<MockPerformanceSettings> s_ext;

    MockRuntime& m_Runtime;

    std::map<XrPerfSettingsDomainEXT, XrPerfSettingsLevelEXT> m_PerformanceLevelHints;
    std::map<std::pair<XrPerfSettingsDomainEXT, XrPerfSettingsSubDomainEXT>, XrPerfSettingsNotificationLevelEXT> m_notificationLevel;

    MockPerformanceSettings(MockRuntime& runtime);
};

XrResult MockPerformanceSettings_GetInstanceProcAddr(const char* name, PFN_xrVoidFunction* function);
