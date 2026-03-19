using System;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine.TestTools;
using UnityEngine.XR.Management;
using UnityEngine.XR.OpenXR.Features.Mock;
using UnityEngine.XR.OpenXR.NativeTypes;
using UnityEngine.XR.OpenXR.TestTooling;
using XrInstance = System.UInt64;
using XrSystemId = System.UInt64;
using XrFormFactor = System.UInt32;

namespace UnityEngine.XR.OpenXR.Tests
{
    unsafe struct XrSystemPropertiesHeader
    {
        internal XrStructureType type;
        internal void* next;
    }

    unsafe struct XrSystemGetInfo
    {
        internal XrStructureType type;
        internal void* next;
        internal XrFormFactor formFactor;
    }

    unsafe delegate XrResult GetSystemProperties_Delegate(XrSystemPropertiesHeader* systemPropertiesStruct);
    unsafe delegate XrResult GetSystem_Delegate(XrInstance instance, XrSystemGetInfo* getInfo, XrSystemId* systemId);

    class TestToolingTests
    {
        static readonly string[] s_LifecycleCalls =
        {
            "OnInstanceCreate",
            "OnSessionCreate",
            "OnSessionBegin",
            "OnSessionEnd",
            "OnSessionDestroy",
            "OnSessionExiting",
            "OnSubsystemCreate",
            "OnSubsystemStart",
            "OnSubsystemStop",
            "OnSubsystemDestroy",
            "OnSessionStateChange",
        };

        const uint XR_FB_spatial_entity_SPEC_VERSION = 3;
        const uint XR_TYPE_SYSTEM_GET_INFO = 4;
        const uint XR_TYPE_SYSTEM_USER_PRESENCE_PROPERTIES_EXT = 1000470001;
        const uint XR_TYPE_EVENT_DATA_USER_PRESENCE_CHANGED_EXT = 1000470000;
        internal const int EventDataBufferSize = 128;

        static uint s_NumTimesMockSysPropertiesCalled;

        static unsafe IntPtr GetSysProperties_UserPresence_MockCallback() =>
            Marshal.GetFunctionPointerForDelegate((GetSystemProperties_Delegate)SysProperties_UserPresence_MockCallback);

        [MonoPInvokeCallback(typeof(GetSystemProperties_Delegate))]
        static unsafe XrResult SysProperties_UserPresence_MockCallback(XrSystemPropertiesHeader* systemPropertiesStruct)
        {
            s_NumTimesMockSysPropertiesCalled++;
            Assert.IsTrue(systemPropertiesStruct != null);
            Assert.IsTrue((uint)systemPropertiesStruct->type == XR_TYPE_SYSTEM_USER_PRESENCE_PROPERTIES_EXT);
            return XrResult.Success;
        }

        static uint s_NumTimesMockGetSystemCalled;

        static unsafe IntPtr GetSystem_MockCallback() =>
            Marshal.GetFunctionPointerForDelegate((GetSystem_Delegate)System_MockCallback);

        [MonoPInvokeCallback(typeof(GetSystem_Delegate))]
        static unsafe XrResult System_MockCallback(XrInstance instance, XrSystemGetInfo* getInfo, XrSystemId* systemId)
        {
            s_NumTimesMockGetSystemCalled++;
            Assert.IsTrue(getInfo != null);
            Assert.IsTrue((uint)getInfo->type == XR_TYPE_SYSTEM_GET_INFO);
            Assert.IsTrue(systemId != null);
            *systemId = 2;
            return XrResult.Success;
        }

        unsafe struct XrEventDataUserPresenceChangedEXT
        {
            /* XrStructureType */
            public uint type;
            /* const void* */
            public void* next;
            /* XrSession */
            public ulong session;
            /* XrBool32 */
            public uint isUserPresent;
        }

        [UnityTest]
        public IEnumerator SessionAndInstanceAreValid()
        {
            bool xrSessionIsValid, xrInstanceIsValid;
            using (var testEnvironment = MockOpenXREnvironment.CreateEnvironment())
            {
                testEnvironment.Start();

                yield return new WaitForSeconds(1);

                xrSessionIsValid = MockRuntime.Instance.XrSession != 0;
                xrInstanceIsValid = MockRuntime.Instance.XrInstance != 0;

                testEnvironment.Stop();

                yield return new WaitForSeconds(1);
            }

            Assert.IsTrue(xrSessionIsValid, "Session isn't valid");
            Assert.IsTrue(xrInstanceIsValid, "Instance isn't valid");
        }

        [UnityTest]
        public IEnumerator MockRuntimeEnvironmentCanBeRunMultipleTimes()
        {
            bool xrSessionIsValid, xrInstanceIsValid;
            using (var testEnvironment = MockOpenXREnvironment.CreateEnvironment())
            {
                testEnvironment.Start();

                yield return new WaitForSeconds(1);

                testEnvironment.Stop();

                yield return new WaitForSeconds(1);

                testEnvironment.Start();

                yield return new WaitForSeconds(1);

                xrSessionIsValid = MockRuntime.Instance.XrSession != 0;
                xrInstanceIsValid = MockRuntime.Instance.XrInstance != 0;

                testEnvironment.Stop();

                yield return new WaitForSeconds(1);
            }

            Assert.IsTrue(xrSessionIsValid, "Session isn't valid");
            Assert.IsTrue(xrInstanceIsValid, "Instance isn't valid");
        }

        [UnityTest]
        public IEnumerator MockRuntimeCallbacksReceived(
            [ValueSource(nameof(s_LifecycleCalls))] string lifecycleCallback)
        {
            bool callbackSuccess = true;
            object methodsCallback(string methodName, object param)
            {
                if (methodName.Contains(lifecycleCallback))
                {
                    callbackSuccess = true;
                }

                return true;
            }

            using (var testEnvironment = MockOpenXREnvironment.CreateEnvironment())
            {
                MockRuntime.Instance.TestCallback = methodsCallback;

                testEnvironment.Start();
                yield return new WaitForSeconds(1);

                testEnvironment.Stop();
                yield return new WaitForSeconds(1);
            }

            Assert.True(callbackSuccess, $"Callback \"{lifecycleCallback}\" was not called");
        }

        [UnityTest]
        public IEnumerator MockRuntimeFeatureIsEnabled()
        {
            using var testEnvironment = MockOpenXREnvironment.CreateEnvironment();
            var testFeature = testEnvironment.Settings.GetFeature<MockRuntime>();
            Assert.NotNull(testFeature, "Feature was not found before running MockRuntime");

            testEnvironment.Start();
            yield return new WaitForSeconds(1);

            testFeature = testEnvironment.Settings.GetFeature<MockRuntime>();

            Assert.NotNull(testFeature, "Feature was not found while running MockRuntime");
            Assert.True(testFeature.enabled, "Feature was not enabled");

            testEnvironment.Stop();
            yield return new WaitForSeconds(1);
        }

        [UnityTest]
        public IEnumerator OriginalXRGeneralSettingsAreStoredAndRestored()
        {
            var originalSettings = XRGeneralSettings.Instance;

            using (var testEnvironment = MockOpenXREnvironment.CreateEnvironment())
            {
                testEnvironment.Start();
                yield return new WaitForSeconds(1);

                testEnvironment.Stop();
                yield return new WaitForSeconds(1);
            }

            var restoredSettings = XRGeneralSettings.Instance;

            if (originalSettings == null)
            {
                Assert.True(restoredSettings == null, "XR settings weren't deleted after test");
            }
            else
            {
                Assert.NotNull(originalSettings, "Original settings were deleted");
                Assert.AreSame(originalSettings, restoredSettings, "Settings were not restored");
            }
        }

        [UnityTest]
        [UnityPlatform(include = new RuntimePlatform[] { RuntimePlatform.WindowsEditor, RuntimePlatform.OSXEditor, RuntimePlatform.LinuxEditor })]
        public IEnumerator TestAssetsAreCreatedAndDeleted()
        {
            var testAssetsPath = Path.GetFullPath(Path.Combine("Assets", "Temp", "Test"));
            bool testAssetsExist;

            using (var testEnvironment = MockOpenXREnvironment.CreateEnvironment())
            {
                yield return new WaitForSeconds(1);

                testAssetsExist = Directory.EnumerateFiles(testAssetsPath, "*", SearchOption.AllDirectories).Any();
            }

            var testAssetsStillExist = Directory.Exists(testAssetsPath);

            Assert.True(testAssetsExist, "Test assets were not created after environment set up");
            Assert.False(testAssetsStillExist, "Test assets were not deleted after environment tear down");
        }

        [UnityTest]
        public IEnumerator OriginalLoaderIsPreserved()
        {
            var activeLoaders = GetNamesOfActiveLoaders();

            using (var environment = MockOpenXREnvironment.CreateEnvironment())
            {
                environment.Start();
                yield return new WaitForSeconds(1);

                environment.Stop();
                yield return new WaitForSeconds(1);
            }

            var restoredLoaders = GetNamesOfActiveLoaders();

            if (restoredLoaders.Count > 0)
            {
                foreach (var loader in activeLoaders)
                {
                    Assert.Contains(loader, restoredLoaders, $"Loader \"{loader}\" is missing");
                }
            }
            else
            {
                Assert.IsEmpty(restoredLoaders, "No loaders remaining were expected in the settings");
            }
        }

        [UnityTest]
        public IEnumerator OriginalOpenXRSettingsArePreserved()
        {
            var originalSettings = OpenXRSettings.Instance;

            using (var environment = MockOpenXREnvironment.CreateEnvironment())
            {
                environment.Start();
                yield return new WaitForSeconds(1);

                environment.Stop();
                yield return new WaitForSeconds(1);
            }

            var restoredSettings = OpenXRSettings.Instance;

            Assert.AreSame(originalSettings, restoredSettings, "Settings were not restored");
        }

        [UnityTest]
        public IEnumerator AddExtensionIsNotPersisted()
        {
            const string sampleExtension = "XR_KHR_maintenance1";

            using (var environment = MockOpenXREnvironment.CreateEnvironment())
            {
                environment.Settings.RequestUseExtension(sampleExtension);

                environment.Start();
                yield return new WaitForSeconds(1);
                environment.Stop();
                yield return new WaitForSeconds(1);
            }

            Assert.IsFalse(IsExtensionAdded(sampleExtension), $"Sample extension {sampleExtension} was not removed");
        }

        /// <summary>
        /// Tests below are for Mock OpenXR Environment support for extensions test methods
        /// </summary>
#if !OPENXR_USE_KHRONOS_LOADER
        // the Android Generic Loader is *not* supported by this API
        [UnityTest]
        public IEnumerator TestAddSupportedExtension()
        {
            using var mockRuntimeEnvironment = MockOpenXREnvironment.CreateEnvironment();

            // Request extension that mock runtime doesn't support by default.
            mockRuntimeEnvironment.Settings.RequestUseExtension("XR_FB_spatial_entity");
            // Enable extension
            mockRuntimeEnvironment.AddSupportedExtension("XR_FB_spatial_entity", XR_FB_spatial_entity_SPEC_VERSION);

            mockRuntimeEnvironment.Start();
            yield return new WaitForSeconds(1);

            Debug.Log("about to assert");
            Assert.IsTrue(OpenXRRuntime.IsExtensionEnabled("XR_FB_spatial_entity"), "XR_FB_spatial_entity should be enabled.");
            Assert.IsFalse(OpenXRRuntime.IsExtensionEnabled("XR_META_spatial_entity_discovery"), "XR_META_spatial_entity_discovery shouldn't be enabled.");

            mockRuntimeEnvironment.Stop();
            yield return new WaitForSeconds(1);
        }
#endif

        /// <summary>
        /// Tests that mocked system properties get methods are called when desired.
        /// </summary>
        [UnityTest]
        public IEnumerator TestSetSysPropsFunctionForXrStructureType()
        {
            using var mockRuntimeEnvironment = MockOpenXREnvironment.CreateEnvironment();

            // our "XR_TYPE_SYSTEM_USER_PRESENCE_PROPERTIES_EXT" mock should not be called at the start
            s_NumTimesMockSysPropertiesCalled = 0;
            mockRuntimeEnvironment.Start();
            yield return new WaitForSeconds(0.1F);
            Assert.IsTrue(s_NumTimesMockSysPropertiesCalled == 0);
            mockRuntimeEnvironment.Stop();
            yield return new WaitForSeconds(0.1F);

            // our "XR_TYPE_SYSTEM_USER_PRESENCE_PROPERTIES_EXT" mock SHOULD be called when SET
            mockRuntimeEnvironment.SetSysPropertiesFunctionForXrStructureType(
                XR_TYPE_SYSTEM_USER_PRESENCE_PROPERTIES_EXT,
                GetSysProperties_UserPresence_MockCallback());
            s_NumTimesMockSysPropertiesCalled = 0;
            mockRuntimeEnvironment.Start();
            yield return new WaitForSeconds(0.1F);
            Assert.IsTrue(s_NumTimesMockSysPropertiesCalled > 0);
            mockRuntimeEnvironment.Stop();
            yield return new WaitForSeconds(0.1F);

            // our "XR_TYPE_SYSTEM_USER_PRESENCE_PROPERTIES_EXT" mock SHOULD NOT be called when CLEARED
            mockRuntimeEnvironment.SetSysPropertiesFunctionForXrStructureType(
                XR_TYPE_SYSTEM_USER_PRESENCE_PROPERTIES_EXT,
                IntPtr.Zero);
            s_NumTimesMockSysPropertiesCalled = 0;
            mockRuntimeEnvironment.Start();
            yield return new WaitForSeconds(0.1F);
            Assert.IsTrue(s_NumTimesMockSysPropertiesCalled == 0);
            mockRuntimeEnvironment.Stop();
            yield return new WaitForSeconds(0.1F);
        }

        /// <summary>
        /// Tests that mocked OpenXR function pointer methods are called when desired.
        /// </summary>
        [UnityTest]
        public IEnumerator TestSetFunctionForInterceptor()
        {
            using var mockRuntimeEnvironment = MockOpenXREnvironment.CreateEnvironment();

            // our "xrGetSystem" mock should not be called at the start
            Console.WriteLine("TestSetFunctionForInterceptor() first test");
            s_NumTimesMockGetSystemCalled = 0;
            mockRuntimeEnvironment.Start();
            yield return new WaitForSeconds(0.1F);
            Assert.IsTrue(s_NumTimesMockGetSystemCalled == 0);
            mockRuntimeEnvironment.Stop();
            yield return new WaitForSeconds(0.1F);

            // our "xrGetSystem" mock SHOULD be called when SET
            Console.WriteLine("TestSetFunctionForInterceptor() second test");
            mockRuntimeEnvironment.SetFunctionForInterceptor(
                "xrGetSystem", GetSystem_MockCallback());
            s_NumTimesMockGetSystemCalled = 0;
            mockRuntimeEnvironment.Start();
            yield return new WaitForSeconds(0.1F);
            Assert.IsTrue(s_NumTimesMockGetSystemCalled > 0);
            mockRuntimeEnvironment.Stop();
            yield return new WaitForSeconds(0.1F);

            // our "xrGetSystem" mock SHOULD NOT be called when CLEARED
            Console.WriteLine("TestSetFunctionForInterceptor() third test");
            mockRuntimeEnvironment.SetFunctionForInterceptor(
                "xrGetSystem", IntPtr.Zero);
            s_NumTimesMockGetSystemCalled = 0;
            mockRuntimeEnvironment.Start();
            yield return new WaitForSeconds(0.1F);
            Assert.IsTrue(s_NumTimesMockGetSystemCalled == 0);
            mockRuntimeEnvironment.Stop();
            yield return new WaitForSeconds(0.1F);
        }

        /// <summary>
        /// Tests below are for Mock OpenXR Environment support for EnqueueMockEventData and ProcessEventQueue
        /// </summary>
        [UnityTest]
        public IEnumerator TestEventQueueAPIs()
        {
            using var mockRuntimeEnvironment = MockOpenXREnvironment.CreateEnvironment();

            // Utilize unit test for XR_EXT_user_presence extension - UserPresence in OpenXRRuntimeTests.cs
            mockRuntimeEnvironment.Settings.RequestUseExtension("XR_EXT_user_presence");

            mockRuntimeEnvironment.Start();
            yield return new WaitForSeconds(1);

            List<InputDevice> hmdDevices = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeadMounted, hmdDevices);
            Assert.That(hmdDevices.Count > 0, Is.True);

            //Queue Event to set userPresent to False.
            var userPresenceEventData = new XrEventDataUserPresenceChangedEXT
            {
                type = XR_TYPE_EVENT_DATA_USER_PRESENCE_CHANGED_EXT,
                next = null,
                isUserPresent = 0 // Set userPresent to False
            };
            IntPtr unmanagedEventData = Marshal.AllocHGlobal(EventDataBufferSize);
            Marshal.StructureToPtr(userPresenceEventData, unmanagedEventData, false);
            mockRuntimeEnvironment.EnqueueMockEventData(unmanagedEventData);
            Marshal.FreeHGlobal(unmanagedEventData);
            mockRuntimeEnvironment.ProcessEventQueue();
            yield return new WaitForSeconds(1);

            bool hasValue = hmdDevices[0].TryGetFeatureValue(CommonUsages.userPresence, out bool isUserPresent);
            Assert.That(hasValue, Is.True);
            Assert.That(isUserPresent, Is.False);
            // Queue Event to set userPresent to True.
            userPresenceEventData = new XrEventDataUserPresenceChangedEXT
            {
                type = XR_TYPE_EVENT_DATA_USER_PRESENCE_CHANGED_EXT,
                next = null,
                isUserPresent = 1 // Set userPresent to True
            };
            unmanagedEventData = Marshal.AllocHGlobal(EventDataBufferSize);
            Marshal.StructureToPtr(userPresenceEventData, unmanagedEventData, false);
            mockRuntimeEnvironment.EnqueueMockEventData(unmanagedEventData);
            Marshal.FreeHGlobal(unmanagedEventData);
            mockRuntimeEnvironment.ProcessEventQueue();
            yield return new WaitForSeconds(1);

            hasValue = hmdDevices[0].TryGetFeatureValue(CommonUsages.userPresence, out isUserPresent);
            Assert.That(hasValue, Is.True);
            Assert.That(isUserPresent, Is.True);

            mockRuntimeEnvironment.Stop();
            yield return new WaitForSeconds(1);
        }

        static List<string> GetNamesOfActiveLoaders()
        {
            var activeLoaders = XRGeneralSettings.Instance.Manager.activeLoaders;

            if (activeLoaders != null && activeLoaders.Count > 0)
            {
                return XRGeneralSettings.Instance.Manager.activeLoaders
                    .Where(loader => loader != null)
                    .Select(loader => loader.name)
                    .OrderBy(name => name)
                    .ToList();
            }

            return new List<string>();
        }

        static bool IsExtensionAdded(string sampleExtension)
        {
            return MockRuntime.Instance.openxrExtensionStrings.Contains(sampleExtension);
        }
    }
}
