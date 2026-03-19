#pragma once

#include "../mock.h"
#include <map>
#include <memory>

#ifdef XR_USE_PLATFORM_ANDROID
class MockAndroidThreadSettings
{
public:
    static MockAndroidThreadSettings* Instance();
    static void Init(MockRuntime& runtime);
    static void Deinit();

    MockAndroidThreadSettings(MockRuntime& runtime);

    XrResult SetAndroidApplicationThread(XrAndroidThreadTypeKHR threadType, uint32_t threadId);
    bool IsAndroidThreadTypeRegistered(XrAndroidThreadTypeKHR threadType) const;
    uint32_t GetRegisteredAndroidThreadsCount() const;

private:
    static std::unique_ptr<MockAndroidThreadSettings> s_Instance;

    MockRuntime& m_Runtime;
    std::map<uint32_t, XrAndroidThreadTypeKHR> m_AssignedThreadTypes{};
};

XrResult MockAndroidThreadSettings_GetInstanceProcAddr(const char* name, PFN_xrVoidFunction* function);
#endif
