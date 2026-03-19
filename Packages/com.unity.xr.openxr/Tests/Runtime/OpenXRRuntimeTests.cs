using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NUnit.Framework;
using UnityEngine.XR.OpenXR;
using UnityEngine.XR.OpenXR.Features;
using UnityEngine.XR.OpenXR.Features.Mock;
using UnityEngine.TestTools;
using UnityEngine.XR.OpenXR.NativeTypes;
using UnityEngine.Diagnostics;

namespace UnityEngine.XR.OpenXR.Tests
{
    class OpenXRRuntimeTests : OpenXRLoaderSetup
    {
        [Test]
        public void TestAvailableExtensions()
        {
            // This test verifies that the list of available extensions contains a subset of known extensions.
            // If certain known extensions are removed from the mock this test should reflect that.
            base.InitializeAndStart();
            string[] extensions = OpenXRRuntime.GetAvailableExtensions();
            HashSet<string> extensionsSet = new HashSet<string>(extensions);

            List<string> expectedExtensions = new List<string>()
            {
                "XR_UNITY_mock_test",
                "XR_UNITY_null_gfx",
                "XR_KHR_visibility_mask",
                "XR_EXT_user_presence",
                "XR_EXT_conformance_automation",
                "XR_KHR_composition_layer_depth",
                "XR_VARJO_quad_views",
                "XR_MSFT_secondary_view_configuration",
                "XR_EXT_eye_gaze_interaction",
                "XR_MSFT_hand_interaction",
                "XR_MSFT_first_person_observer",
                "XR_META_performance_metrics",
                "XR_EXT_performance_settings"
            };

            foreach (string expectedExtension in expectedExtensions)
            {
                Assert.IsTrue(extensionsSet.Contains(expectedExtension), $"extensionsSet missing \"{expectedExtension}\"");
            }

            base.StopAndShutdown();
        }

        [UnityTest]
        public IEnumerator SystemIdRetrieved()
        {
            bool systemIdReceived = false;
            MockRuntime.Instance.TestCallback = (methodName, param) =>
            {
                if (methodName == nameof(OpenXRFeature.OnSystemChange))
                {
                    systemIdReceived = true;
                    Assert.AreEqual(2, param);
                }

                return true;
            };

            base.InitializeAndStart();
            yield return null;

            Assert.IsTrue(systemIdReceived);
        }

        [UnityTest]
        public IEnumerator SessionBegan()
        {
            bool sessionBegan = false;
            MockRuntime.Instance.TestCallback = (methodName, param) =>
            {
                if (methodName == nameof(OpenXRFeature.OnSessionBegin))
                {
                    sessionBegan = true;
                    Assert.AreEqual(3, param);
                }

                return true;
            };

            base.InitializeAndStart();
            yield return null;

            Assert.IsTrue(sessionBegan);
        }

        [UnityTest]
        public IEnumerator SessionEnded()
        {
            bool sessionStarted = false;
            bool sessionEnded = false;
            MockRuntime.Instance.TestCallback = (methodName, param) =>
            {
                switch (methodName)
                {
                    case nameof(OpenXRFeature.OnSessionBegin):
                        Assert.IsFalse(sessionStarted);
                        sessionStarted = true;
                        Assert.AreEqual(3, param);
                        break;
                    case nameof(OpenXRFeature.OnSessionEnd):
                        Assert.IsTrue(sessionStarted);
                        Assert.AreEqual(3, param);
                        sessionStarted = false;
                        sessionEnded = true;
                        break;
                }

                return true;
            };

            base.InitializeAndStart();

            const int ITERATION_MAX_COUNT = 10;
            int waitCount = 0;
            while (!sessionStarted && waitCount++ < ITERATION_MAX_COUNT)
                yield return null;

            Assert.IsTrue(sessionStarted);

            base.StopAndShutdown();

            Assert.IsTrue(sessionEnded);
        }

        [Test]
        public void SessionDestroyed()
        {
            bool sessionDestroyed = false;
            MockRuntime.Instance.TestCallback = (methodName, param) =>
            {
                if (methodName == nameof(OpenXRFeature.OnSessionDestroy))
                {
                    sessionDestroyed = true;
                    Assert.AreEqual(3, param);
                }

                return true;
            };

            base.InitializeAndStart();
            base.StopAndShutdown();

            Assert.IsTrue(sessionDestroyed);
        }

        [Test]
        public void InstanceDestroyed()
        {
            object instance = null;
            bool instanceDestroyed = false;
            MockRuntime.Instance.TestCallback = (methodName, param) =>
            {
                if (methodName == nameof(OpenXRFeature.OnInstanceCreate))
                {
                    instance = param;
                }

                if (methodName == nameof(OpenXRFeature.OnInstanceDestroy))
                {
                    instanceDestroyed = true;
                    Assert.AreEqual(instance, param);
                }

                return true;
            };

            base.InitializeAndStart();
            base.StopAndShutdown();

            Assert.IsTrue(instanceDestroyed);
        }

        [UnityTest]
        public IEnumerator XrSpaceApp()
        {
            bool spaceAppSet = false;
            bool spaceAppRemoved = false;
            ulong oldSpaceApp = 0;

            MockRuntime.Instance.TestCallback = (methodName, param) =>
            {
                // this function checks to see if the initial SetAppSpace call
                // from unity_session.cpp. if you change the default setup in unity_session.cpp
                // you will need to update the value here so that the handle matches.
                // this also makes an assumption that the 3rd space we create is the "Stage"
                // space and that the handles are deterministic.
                if (methodName == nameof(OpenXRFeature.OnAppSpaceChange))
                {
                    spaceAppSet = (oldSpaceApp == 0 && (ulong)param == 3);
                    spaceAppRemoved = (oldSpaceApp == 3 && (ulong)param == 0);
                    oldSpaceApp = (ulong)param;
                }

                return true;
            };

            base.InitializeAndStart();
            yield return null;

            Assert.IsTrue(spaceAppSet);
            Assert.IsFalse(spaceAppRemoved);

            base.StopAndShutdown();
            yield return null;
        }

        [UnityTest]
        public IEnumerator RuntimeName()
        {
            base.InitializeAndStart();
            yield return null;
            Assert.AreEqual(OpenXRRuntime.name, "Unity Mock Runtime");
        }

        [UnityTest]
        public IEnumerator ExtensionCallbackOrder()
        {
            var callbackQueue = new List<string>();
            MockRuntime.Instance.TestCallback = (methodName, param) =>
            {
                // xrSessionStateChanged is called multiple times, we won't validate it here.
                if (methodName != nameof(OpenXRFeature.OnSessionStateChange))
                    callbackQueue.Add(methodName);
                return true;
            };

            base.InitializeAndStart();
            yield return null;
            base.StopAndShutdown();
            yield return null;

            var expectedCallbackOrder = new List<string>()
            {
                nameof(OpenXRFeature.HookGetInstanceProcAddr),
                nameof(OpenXRFeature.OnInstanceCreate),
                nameof(OpenXRFeature.OnSystemChange),
                nameof(OpenXRFeature.OnSubsystemCreate),
                nameof(OpenXRFeature.OnSessionCreate),
                nameof(OpenXRFeature.OnFormFactorChange),
                nameof(OpenXRFeature.OnEnvironmentBlendModeChange),
                nameof(OpenXRFeature.OnViewConfigurationTypeChange),
                nameof(OpenXRFeature.OnSessionBegin),
                nameof(OpenXRFeature.OnAppSpaceChange),
                nameof(OpenXRFeature.OnSubsystemStart),
                nameof(OpenXRFeature.OnSubsystemStop),
                nameof(OpenXRFeature.OnSessionEnd),
                nameof(OpenXRFeature.OnSessionExiting),
                nameof(OpenXRFeature.OnSubsystemDestroy),
                nameof(OpenXRFeature.OnSessionDestroy),
                nameof(OpenXRFeature.OnInstanceDestroy)
            };

#if UNITY_EDITOR
            var activeBuildTarget = UnityEditor.EditorUserBuildSettings.activeBuildTarget;
            var activeBuildTargetGroup = UnityEditor.BuildPipeline.GetBuildTargetGroup(activeBuildTarget);
            var settings = UnityEditor.XR.Management.XRGeneralSettingsPerBuildTarget.XRGeneralSettingsForBuildTarget(activeBuildTargetGroup);
            // Validations only occur if the OpenXRLoader is active.
            var isLoaderActive = !(settings == null || !settings.Manager.activeLoaders.Any(loader => loader is OpenXRLoader));
            if (isLoaderActive)
                expectedCallbackOrder.Insert(0,nameof(OpenXRFeature.GetValidationChecks));
            Assert.AreEqual(expectedCallbackOrder, callbackQueue);
#endif
            Assert.AreEqual(expectedCallbackOrder, callbackQueue);
        }

        [UnityTest]
        public IEnumerator TestConsistentFeatureValues()
        {
            HashSet<string> methodsUsingSession = new HashSet<string>()
            {
                "OnSessionCreate",
                "OnSessionBegin",
                "OnSessionEnd",
                "OnSessionDestroy",
                "OnSessionLossPending",
                "OnSessionExiting"
            };

            HashSet<string> methodsUsingInstance = new HashSet<string>()
            {
                "OnInstanceCreate",
                "OnInstanceDestroy"
            };

            Dictionary<string, ulong> methodToSessionValue = new Dictionary<string, ulong>();
            Dictionary<string, ulong> methodToInstanceValue = new Dictionary<string, ulong>();

            MockRuntime.Instance.TestCallback = (methodName, param) =>
            {
                if (methodsUsingSession.Contains(methodName))
                {
                    Assert.IsFalse(methodToSessionValue.ContainsKey(methodName));
                    methodToSessionValue[methodName] = (ulong)param;
                }
                else if (methodsUsingInstance.Contains(methodName))
                {
                    Assert.IsFalse(methodToInstanceValue.ContainsKey(methodName));
                    methodToInstanceValue[methodName] = (ulong)param;
                }

                return true;
            };

            base.InitializeAndStart();
            yield return null;
            base.StopAndShutdown();
            yield return null;

            ulong? sessionValue = null;
            ulong? instanceValue = null;

            foreach (var pair in methodToSessionValue)
            {
                if (sessionValue.HasValue)
                {
                    Assert.AreEqual(sessionValue, pair.Value);
                }
                else
                {
                    sessionValue = pair.Value;
                }
            }

            foreach (var pair in methodToInstanceValue)
            {
                if (instanceValue.HasValue)
                {
                    Assert.AreEqual(instanceValue, pair.Value);
                }
                else
                {
                    instanceValue = pair.Value;
                }
            }

            Assert.IsTrue(sessionValue.HasValue);
            Assert.IsTrue(instanceValue.HasValue);
        }

        [UnityTest]
        public IEnumerator XrSessionStateChanged()
        {
            var states = new List<XrSessionState>();
            MockRuntime.Instance.TestCallback = (methodName, param) =>
            {
                if (methodName == nameof(OpenXRFeature.OnSessionStateChange))
                {
                    var oldState = (XrSessionState)((MockRuntime.XrSessionStateChangedParams)param).OldState;
                    var newState = (XrSessionState)((MockRuntime.XrSessionStateChangedParams)param).NewState;
                    CheckValidStateTransition(oldState, newState);
                    states.Add(newState);
                }

                return true;
            };

            Assert.AreEqual(XrSessionState.Unknown, MockRuntime.sessionState);
            base.InitializeAndStart();
            yield return null;
            Assert.AreEqual(XrSessionState.Focused, MockRuntime.sessionState);
            base.StopAndShutdown();
            yield return null;
            Assert.AreEqual(XrSessionState.Unknown, MockRuntime.sessionState);

            var expected = new List<XrSessionState>()
            {
                XrSessionState.Idle,
                XrSessionState.Ready,
                XrSessionState.Synchronized,
                XrSessionState.Visible,
                XrSessionState.Focused,
                XrSessionState.Visible,
                XrSessionState.Synchronized,
                XrSessionState.Stopping,
                XrSessionState.Idle,
                XrSessionState.Exiting,
            };

            Assert.AreEqual(states, expected);
        }

        [UnityTest]
        public IEnumerator EnableSpecExtension()
        {
            AddExtension(MockRuntime.XR_UNITY_mock_test);

            base.InitializeAndStart();

            yield return null;

            Assert.AreEqual(10, MockRuntime.Instance.XrInstance);
        }

        [UnityTest]
        public IEnumerator CheckSpecExtensionVersion()
        {
            AddExtension(MockRuntime.XR_UNITY_mock_test);

            base.InitializeAndStart();

            yield return null;

            Assert.AreEqual(123, OpenXRRuntime.GetExtensionVersion(MockRuntime.XR_UNITY_mock_test));
        }

        [UnityTest]
        public IEnumerator CheckSpecExtensionEnabled()
        {
            AddExtension(MockRuntime.XR_UNITY_mock_test);

            base.InitializeAndStart();

            yield return null;

            Assert.AreEqual(true, OpenXRRuntime.IsExtensionEnabled(MockRuntime.XR_UNITY_mock_test));
        }

        static OpenXRSettings.DepthSubmissionMode[] depthModes = new OpenXRSettings.DepthSubmissionMode[]
        {
            OpenXRSettings.DepthSubmissionMode.None,
            OpenXRSettings.DepthSubmissionMode.Depth16Bit,
            OpenXRSettings.DepthSubmissionMode.Depth24Bit
        };

        [UnityTest]
        [UnityPlatform(exclude = new[] { RuntimePlatform.Android })] // Vulkan doesn't have depth on earlier versions of unity
        public IEnumerator CheckDepthSubmissionMode([ValueSource("depthModes")] OpenXRSettings.DepthSubmissionMode depthMode)
        {
            base.InitializeAndStart();
            yield return null;
            OpenXRSettings.Instance.depthSubmissionMode = depthMode;
            yield return null;
            Assert.AreEqual(depthMode, OpenXRSettings.Instance.depthSubmissionMode);
        }

        [UnityTest]
        public IEnumerator CheckRenderMode()
        {
            base.InitializeAndStart();

            yield return null;

            OpenXRSettings.Instance.renderMode = OpenXRSettings.RenderMode.SinglePassInstanced;
            yield return null;
            Assert.AreEqual(OpenXRSettings.Instance.renderMode, OpenXRSettings.RenderMode.SinglePassInstanced);

            OpenXRSettings.Instance.renderMode = OpenXRSettings.RenderMode.MultiPass;
            yield return null;
            Assert.AreEqual(OpenXRSettings.Instance.renderMode, OpenXRSettings.RenderMode.MultiPass);
        }

        [UnityTest]
        public IEnumerator CheckSpecExtensionEnabledAtXrInstanceCreated()
        {
            AddExtension(MockRuntime.XR_UNITY_mock_test);

            bool xrCreateInstanceCalled = false;
            bool containsMockExt = false;
            MockRuntime.Instance.TestCallback = (methodName, param) =>
            {
                if (methodName == nameof(OpenXRFeature.OnInstanceCreate))
                {
                    containsMockExt = OpenXRRuntime.IsExtensionEnabled(MockRuntime.XR_UNITY_mock_test);
                    xrCreateInstanceCalled = true;
                }

                return true;
            };

            base.InitializeAndStart();
            yield return null;
            Assert.IsTrue(xrCreateInstanceCalled);
            Assert.IsTrue(containsMockExt);
        }

        /// <summary>
        /// List of extensions to test against runtime loader version greater than 1.1
        /// </summary>
        protected static readonly (string extName, bool expected)[] s_ExtensionsEnableExamples1_1 =
        {
            ("XR_EXT_palm_pose", true), //not available for mockruntime, but promoted to core in 1.1 loader, so expect enabled as default.
            ("XR_EXT_hp_mixed_reality_controller", true), //available for mockruntime, also promoted to core in 1.1 loader, so expect enabled as default.
            ("XR_VARJO_quad_views", true), //available for mockruntime, also promoted to core in 1.1 loader, so expect enabled as default.
            ("XR_EXT_local_floor", true), //not available for mockruntime, but promoted to core in 1.1 loader, so expect enabled as default.
            ("XR_KHR_composition_layer_cylinder", false), // not available for mockruntime, so expect not enabled.
            ("XR_EXT_eye_gaze_interaction", true), // available for mockruntime, so expect enabled.
            ("XR_KHR_maintenance1", true), // not available for mockruntime, but promoted to core in 1.1 loader, so expect enabled as default.
        };

        [UnityTest]
        public IEnumerator CheckExtensionEnabledRuntimeAPIVersion1_1([ValueSource(nameof(s_ExtensionsEnableExamples1_1))] (string extName, bool expected) extension)
        {
            AddExtension(extension.extName);

            bool xrCreateInstanceCalled = false;
            bool extensionEnabled = false;

            MockRuntime.Instance.TestCallback = (methodName, param) =>
            {
                if (methodName == nameof(OpenXRFeature.OnInstanceCreate))
                {
                    xrCreateInstanceCalled = true;
                    extensionEnabled = OpenXRRuntime.IsExtensionEnabled(extension.extName);
                }

                return true;
            };

            base.InitializeAndStart();
            yield return null;
            Assert.IsTrue(xrCreateInstanceCalled);
            Assert.IsTrue(extensionEnabled == extension.expected);
        }

        /// <summary>
        /// List of extensions to test against runtime loader version 1.0
        /// </summary>
        protected static readonly (string extName, bool expected)[] s_ExtensionsEnableExamples1_0 =
        {
            ("XR_EXT_palm_pose", false), //not available for mockruntime, so expect not enabled.
            ("XR_EXT_hp_mixed_reality_controller", true), //available for mockruntime, so expect enabled.
            ("XR_VARJO_quad_views", true), //available for mockruntime, so expect enabled as default.
            ("XR_EXT_local_floor", false), //not available for mockruntime, , so expect not enabled.
            ("XR_KHR_composition_layer_cylinder", false), // not available for mockruntime, so expect not enabled.
            ("XR_EXT_eye_gaze_interaction", true), // available for mockruntime, so expect enabled.
            ("XR_KHR_maintenance1", false), // not available for mockruntime, so expect not enabled.
        };

        [UnityTest]
        public IEnumerator CheckExtensionEnabledRuntimeAPIVersion1_0([ValueSource(nameof(s_ExtensionsEnableExamples1_0))] (string extName, bool expected) extension)
        {
            AddExtension(extension.extName);

            bool xrCreateInstanceCalled = false;
            bool extensionEnabled = false;
            int attemptCount = 0;
            MockRuntime.SetFunctionCallback("xrCreateInstance", (name) =>
            {
                attemptCount += 1;
                if (attemptCount < 2)
                {
                    return XrResult.ApiVersionUnsupported;
                }
                else
                {
                    return XrResult.Success;
                }
            });

            MockRuntime.Instance.TestCallback = (methodName, param) =>
            {
                if (methodName == nameof(OpenXRFeature.OnInstanceCreate))
                {
                    xrCreateInstanceCalled = true;
                    extensionEnabled = OpenXRRuntime.IsExtensionEnabled(extension.extName);
                }

                return true;
            };

            base.InitializeAndStart();
            yield return null;
            Assert.IsTrue(xrCreateInstanceCalled);
            Assert.IsTrue(extensionEnabled == extension.expected);
        }

        [UnityTest]
        public IEnumerator SimulatePause()
        {
            // Initialize and make sure the frame loop is running
            InitializeAndStart();
            yield return new WaitForXrFrame();

            // Pause will stop the loaders directly
            loader.displaySubsystem.Stop();
            loader.inputSubsystem.Stop();

            yield return null;

            // Runtime will transition to idle
            MockRuntime.TransitionToState(XrSessionState.Visible, false);
            yield return null;
            MockRuntime.TransitionToState(XrSessionState.Synchronized, false);
            yield return null;
            MockRuntime.TransitionToState(XrSessionState.Stopping, false);
            yield return null;
            MockRuntime.TransitionToState(XrSessionState.Idle, false);
            yield return null;

            yield return null;

            // Unpause will start the loaders directly
            loader.displaySubsystem.Start();
            loader.inputSubsystem.Start();

            yield return null;

            // And then transition to ready
            MockRuntime.TransitionToState(XrSessionState.Ready, false);
            yield return new WaitForXrFrame();
        }

        void DisableHandInteraction()
        {
            foreach (var ext in OpenXRSettings.Instance.features)
            {
                if (ext.nameUi == "Hand Interaction Profile")
                {
                    ext.enabled = false;
                    return;
                }
            }
        }

        [Category("HMD")]
        [UnityTest]
        public IEnumerator UserPresence()
        {
            AddExtension("XR_EXT_user_presence");

            List<InputDevice> hmdDevices = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeadMounted, hmdDevices);
            Assert.That(hmdDevices.Count == 0, Is.True);

            InitializeAndStart();
            // Wait two frames to let the input catch up with the renderer
            yield return new WaitForXrFrame(2);

            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeadMounted, hmdDevices);
            Assert.That(hmdDevices.Count > 0, Is.True);

            bool hasValue = hmdDevices[0].TryGetFeatureValue(CommonUsages.userPresence, out bool isUserPresent);
            Assert.That(hasValue, Is.True);
            Assert.That(isUserPresent, Is.True);

            //mock no user present
            bool hasUserPresent = false;
            MockRuntime.CauseUserPresenceChange(hasUserPresent);
            yield return new WaitForXrFrame(2);
            hasValue = hmdDevices[0].TryGetFeatureValue(CommonUsages.userPresence, out isUserPresent);
            Assert.That(hasValue, Is.True);
            Assert.That(isUserPresent, Is.False);

            //mock has user present
            hasUserPresent = true;
            MockRuntime.CauseUserPresenceChange(hasUserPresent);
            yield return new WaitForXrFrame(2);
            hasValue = hmdDevices[0].TryGetFeatureValue(CommonUsages.userPresence, out isUserPresent);
            Assert.That(hasValue, Is.True);
            Assert.That(isUserPresent, Is.True);

        }

        [UnityTest]
        public IEnumerator RefreshRate()
        {
            base.InitializeAndStart();

            yield return null;
            // TODO: 19.4 has an additional frame of latency until fix is backported.
            yield return null;

            var displaySubsystem = GetFirstDisplaySubsystem();
            var ret = displaySubsystem.TryGetDisplayRefreshRate(out float refreshRate);
            Assert.IsTrue(ret);
            Assert.That(refreshRate, Is.EqualTo(60.0f).Within(0.01f));
        }

        static XRDisplaySubsystem GetFirstDisplaySubsystem()
        {
            List<XRDisplaySubsystem> displays = new();
            SubsystemManager.GetSubsystems(displays);
            if (displays.Count == 0)
            {
                Debug.Log("No display subsystem found.");
                return null;
            }
            return displays[0];
        }

        [UnityTest]
        [UnityPlatform(RuntimePlatform.WindowsEditor, RuntimePlatform.WindowsPlayer)]
        public IEnumerator PreInitRealGfxAPI()
        {
            // remove the null gfx device from requested extensions
            MockRuntime.Instance.openxrExtensionStrings = "";

            bool initedRealGfxApi = false;
            MockRuntime.Instance.TestCallback = (s, o) =>
            {
                if (s == nameof(OpenXRFeature.OnInstanceCreate))
                {
                    initedRealGfxApi = new[]
                    {
                        "XR_KHR_D3D11_enable",
                        "XR_KHR_D3D12_enable",
                        "XR_KHR_opengl_enable",
                        "XR_KHR_opengl_es_enable",
                        "XR_KHR_vulkan_enable",
                        "XR_KHR_vulkan_enable2",
                    }.Any(OpenXRRuntime.IsExtensionEnabled);
                }

                return true;
            };

            base.InitializeAndStart();
            yield return null;

            Assert.That(initedRealGfxApi, Is.True);
        }

        [UnityPlatform(exclude = new[] { RuntimePlatform.OSXEditor, RuntimePlatform.OSXPlayer })] // OSX doesn't support single-pass very well, disable for test.
        [UnityTest]
        public IEnumerator CombinedFrustum()
        {
            var cameraGO = new GameObject("Test Cam");
            var camera = cameraGO.AddComponent<Camera>();

#if UNITY_ANDROID
            if (!SystemInfo.supportsMultiview)
                LogAssert.ignoreFailingMessages = true;
#endif

            base.InitializeAndStart();
            OpenXRSettings.Instance.renderMode = OpenXRSettings.RenderMode.SinglePassInstanced;

            yield return new WaitForXrFrame(2);

            var displays = new List<XRDisplaySubsystem>();
            SubsystemManager.GetSubsystems(displays);

            Assert.That(displays.Count, Is.EqualTo(1));

            Assert.That(displays[0].GetRenderPassCount(), Is.EqualTo(1));

            displays[0].GetRenderPass(0, out var renderPass);

            renderPass.GetRenderParameter(camera, 0, out var renderParam0);
            renderPass.GetRenderParameter(camera, 1, out var renderParam1);
            displays[0].GetCullingParameters(camera, renderPass.cullingPassIndex, out var cullingParams);

            // no sense in re-implementing the combining logic here, just the fact they're different shows that we're not using left eye or right eye for culling.
            Assert.That(cullingParams.stereoViewMatrix, Is.Not.EqualTo(renderParam0.view));
            Assert.That(cullingParams.stereoProjectionMatrix, Is.Not.EqualTo(renderParam0.projection));

            Assert.That(cullingParams.stereoViewMatrix, Is.Not.EqualTo(renderParam1.view));
            Assert.That(cullingParams.stereoProjectionMatrix, Is.Not.EqualTo(renderParam0.projection));

            Object.Destroy(cameraGO);
            LogAssert.ignoreFailingMessages = false;
        }

        [UnityTest]
        public IEnumerator InvalidLocateSpace()
        {
            MockRuntime.Instance.TestCallback = (methodName, param) =>
            {
                switch (methodName)
                {
                    case nameof(OpenXRFeature.OnInstanceCreate):
                        // Set the location space to invalid data
                        MockRuntime.SetSpace(XrReferenceSpaceType.View, Vector3.zero, Quaternion.identity, XrSpaceLocationFlags.None);
                        MockRuntime.SetViewState(XrViewConfigurationType.PrimaryStereo, XrViewStateFlags.None);
                        break;
                }

                return true;
            };

            base.InitializeAndStart();

            // Wait a few frames to let the input catch up with the renderer
            yield return new WaitForXrFrame(2);

            MockRuntime.GetEndFrameStats(out var primaryLayerCount, out var secondaryLayerCount);
            Assert.IsTrue(primaryLayerCount == 0);
        }

        [UnityTest]
        public IEnumerator FirstPersonObserver()
        {
            AddExtension("XR_MSFT_secondary_view_configuration");
            AddExtension("XR_MSFT_first_person_observer");
            base.InitializeAndStart();

            MockRuntime.ActivateSecondaryView(XrViewConfigurationType.SecondaryMonoFirstPersonObserver, true);

            yield return new WaitForXrFrame(2);

            MockRuntime.GetEndFrameStats(out var primaryLayerCount, out var secondaryLayerCount);
            Assert.IsTrue(secondaryLayerCount == 1);

            MockRuntime.ActivateSecondaryView(XrViewConfigurationType.SecondaryMonoFirstPersonObserver, false);

            yield return new WaitForXrFrame(2);

            MockRuntime.GetEndFrameStats(out primaryLayerCount, out secondaryLayerCount);
            Assert.IsTrue(secondaryLayerCount == 0);
        }

        [UnityTest]
        public IEnumerator FirstPersonObserverInvalidSecondaryView()
        {
            AddExtension("XR_MSFT_secondary_view_configuration");
            AddExtension("XR_MSFT_first_person_observer");
            base.InitializeAndStart();

            MockRuntime.ActivateSecondaryView(XrViewConfigurationType.SecondaryMonoFirstPersonObserver, true);

            yield return new WaitForXrFrame(2);

            MockRuntime.GetEndFrameStats(out var primaryLayerCount, out var secondaryLayerCount);
            Assert.IsTrue(secondaryLayerCount == 1);

            // make mock runtime return invalid state for xrWaitFrame to make sure we don't crash
            MockRuntime.ActivateSecondaryView(0, false);

            yield return new WaitForXrFrame(2);

            if (OpenXRSettings.ActiveBuildTargetInstance.latencyOptimization !=
                    OpenXRSettings.LatencyOptimization.PrioritizeInputPolling)
            {
                MockRuntime.GetEndFrameStats(out primaryLayerCount, out secondaryLayerCount);
                Assert.IsTrue(secondaryLayerCount == 0);
            }
        }

        [UnityTest]
        public IEnumerator ThirdPersonObserver()
        {
            AddExtension("XR_MSFT_secondary_view_configuration");
            AddExtension("XR_MSFT_third_person_observer_private");
            base.InitializeAndStart();

            MockRuntime.ActivateSecondaryView(XrViewConfigurationType.SecondaryMonoThirdPersonObserver, true);

            yield return new WaitForXrFrame(2);

            MockRuntime.GetEndFrameStats(out var primaryLayerCount, out var secondaryLayerCount);
            Assert.IsTrue(secondaryLayerCount == 1);

            MockRuntime.ActivateSecondaryView(XrViewConfigurationType.SecondaryMonoThirdPersonObserver, false);

            yield return new WaitForXrFrame(2);

            MockRuntime.GetEndFrameStats(out primaryLayerCount, out secondaryLayerCount);
            Assert.IsTrue(secondaryLayerCount == 0);
        }

        [UnityTest]
        public IEnumerator FirstPersonObserverRestartWhileActive()
        {
            AddExtension("XR_MSFT_secondary_view_configuration");
            AddExtension("XR_MSFT_first_person_observer");
            base.InitializeAndStart();

            MockRuntime.ActivateSecondaryView(XrViewConfigurationType.SecondaryMonoFirstPersonObserver, true);
            yield return new WaitForXrFrame(1);

            MockRuntime.GetEndFrameStats(out var primaryLayerCount, out var secondaryLayerCount);
            Assert.IsTrue(secondaryLayerCount == 1);

            // Transition to ready, which was causing a crash.
            MockRuntime.TransitionToState(XrSessionState.Visible, false);
            yield return null;
            MockRuntime.TransitionToState(XrSessionState.Synchronized, false);
            yield return null;
            MockRuntime.TransitionToState(XrSessionState.Stopping, false);
            yield return null;
            MockRuntime.TransitionToState(XrSessionState.Idle, false);
            yield return null;
            MockRuntime.TransitionToState(XrSessionState.Ready, false);
            yield return null;

            // Check that secondary layer is still there
            MockRuntime.GetEndFrameStats(out primaryLayerCount, out secondaryLayerCount);
            Assert.IsTrue(secondaryLayerCount == 1);

            // Transition back to focused
            MockRuntime.TransitionToState(XrSessionState.Synchronized, false);
            yield return null;
            MockRuntime.TransitionToState(XrSessionState.Visible, false);
            yield return null;
            MockRuntime.TransitionToState(XrSessionState.Focused, false);
            yield return null;

            yield return new WaitForXrFrame(2);

            // Verify secondary layer is still up and running
            MockRuntime.GetEndFrameStats(out primaryLayerCount, out secondaryLayerCount);
            Assert.IsTrue(secondaryLayerCount == 1);

            // Make sure we can turn it off
            MockRuntime.ActivateSecondaryView(XrViewConfigurationType.SecondaryMonoFirstPersonObserver, false);
            yield return new WaitForXrFrame(2);

            MockRuntime.GetEndFrameStats(out primaryLayerCount, out secondaryLayerCount);
            Assert.IsTrue(secondaryLayerCount == 0);
        }

        [UnityTest]
        public IEnumerator VarjoQuadViews()
        {
            AddExtension("XR_VARJO_quad_views");
            OpenXRSettings.Instance.renderMode = OpenXRSettings.RenderMode.MultiPass;
            // This is a Runtime 1.0 only test
            int attemptCount = 0;
            MockRuntime.SetFunctionCallback("xrCreateInstance", (name) =>
            {
                attemptCount += 1;
                if (attemptCount < 2)
                {
                    return XrResult.ApiVersionUnsupported;
                }
                else
                {
                    return XrResult.Success;
                }
            });
            base.InitializeAndStart();
            yield return null;
            yield return null;
            Assert.AreEqual(4, loader.displaySubsystem.GetRenderPassCount());

            OpenXRSettings.Instance.renderMode = OpenXRSettings.RenderMode.SinglePassInstanced;
            yield return null;
            yield return null;
            Assert.AreEqual(3, loader.displaySubsystem.GetRenderPassCount());

            base.StopAndShutdown();
        }

        [UnityTest]
        public IEnumerator NullFeature()
        {
            // Insert a null entry into the features list
            var features = OpenXRSettings.Instance.features.ToList();
            features.Insert(1, null);
            OpenXRSettings.Instance.features = features.ToArray();

            base.InitializeAndStart();

            // Wait two frames to make sure nothing else shakes out
            yield return null;
            yield return null;

            base.StopAndShutdown();
        }

        /// <summary>
        /// Tests whether or not the Initialize method of OpenXRLoader will properly handle an exception being thrown
        /// </summary>
        [Test]
        public void InitializeException()
        {
            MockRuntime.Instance.TestCallback = (methodName, param) =>
            {
                switch (methodName)
                {
                    case nameof(OpenXRFeature.HookGetInstanceProcAddr):
                        throw new Exception("Testing exception within Initialize");
                }

                return true;
            };

            LogAssert.ignoreFailingMessages = true;
            base.InitializeAndStart();
            LogAssert.ignoreFailingMessages = false;

            // The static instance should not be set if initialize failed
            Assert.IsTrue(OpenXRLoaderBase.Instance == null);
        }

        [DllImport("UnityOpenXR", EntryPoint = "unity_ext_GetRegenerateTrackingOriginFlag")]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool GetRegenerateTrackingOriginFlag();

        [UnityTest]
        public IEnumerator RegenerateTrackingOriginFlagTest()
        {
            // First, make sure that LocalFloor is being used in place of Floor (so that the LocalFloor code is triggered)
            OpenXRSettings.SetAllowRecentering(true);

            // This is a Runtime 1.0 only test - It depends on XR_EXT_local_floor not being active, which only happens for Runtime 1.0
            int attemptCount = 0;
            MockRuntime.SetFunctionCallback("xrCreateInstance", (name) =>
            {
                attemptCount += 1;
                if (attemptCount < 2)
                {
                    return XrResult.ApiVersionUnsupported;
                }
                else
                {
                    return XrResult.Success;
                }
            });

            base.InitializeAndStart();

            // Make sure that we're setting the TrackingOrigin to floor (to force the generation of the local floor space at time = 0)
            XRInputSubsystem inputSubsystem = Loader.GetLoadedSubsystem<XRInputSubsystem>();
            inputSubsystem.TrySetTrackingOriginMode(TrackingOriginModeFlags.Device);
            inputSubsystem.TrySetTrackingOriginMode(TrackingOriginModeFlags.Floor);

            // Since time = 0, XR_EXT_local_floor is not active, and LocalFloor is requested, this will trigger a tracking origin regeneration.
            Assert.IsTrue(GetRegenerateTrackingOriginFlag());

            yield return null;

            // Advancing several frames will allow the tracking origin regeneration to clear.
            yield return new WaitForTrackingOriginRegeneration();

            // Check that the tracking origin has been regenerated.
            Assert.IsFalse(GetRegenerateTrackingOriginFlag());

            yield return null;
        }

        [UnityTest]
        public IEnumerator FloorTrackingOriginIsRegenerated()
        {
            List<ulong> spaceSequence = new();
            MockRuntime.Instance.TestCallback = (methodName, param) =>
            {
                if (methodName == nameof(OpenXRFeature.OnAppSpaceChange))
                {
                    spaceSequence.Add((ulong)param);
                }

                return true;
            };

            OpenXRSettings.SetAllowRecentering(false);
            OpenXRSettings.RefreshRecenterSpace();

            base.InitializeAndStart();
            yield return null;

            XRInputSubsystem inputSubsystem = Loader.GetLoadedSubsystem<XRInputSubsystem>();
            inputSubsystem.TrySetTrackingOriginMode(TrackingOriginModeFlags.Floor);
            yield return null;

            OpenXRSettings.SetAllowRecentering(false);
            OpenXRSettings.RefreshRecenterSpace();
            bool regenFlagSet = GetRegenerateTrackingOriginFlag();
            yield return null;
            yield return new WaitForTrackingOriginRegeneration();
            bool regenFlagProcessed = GetRegenerateTrackingOriginFlag();

            base.StopAndShutdown();

            Assert.IsTrue(regenFlagSet, "Failed to set regeneration flag");
            Assert.IsFalse(regenFlagProcessed, "Regeneration flag was not processed");

            var distinctCount = spaceSequence.Distinct().ToList().Count;
            Assert.IsTrue(spaceSequence.Count == distinctCount, "Some XR Space handles didn't change");
        }

        /// <summary>
        /// Tests the Meta Dynamic Resolution extension and specifically chooses a
        /// resolution that is lesser than the original. The viewport scale will be < 1
        /// and we check that this is populated on the XRDisplaySubsystem render parameters.
        /// </summary>
        [UnityTest]
        public IEnumerator MetaRecommendedResolutionTest()
        {
            AddExtension("XR_META_recommended_layer_resolution");
            bool oldUsingSuggestedResolutionScale = UnityEngine.XR.OpenXR.Features.AutomaticDynamicResolutionFeature.usingSuggestedResolutionScale;
            UnityEngine.XR.OpenXR.Features.AutomaticDynamicResolutionFeature.SetUsingSuggestedResolutionScale(true);

            try
            {
                var cameraGO = new GameObject("Test Cam");
                var camera = cameraGO.AddComponent<Camera>();

#if UNITY_ANDROID
            if (!SystemInfo.supportsMultiview)
                LogAssert.ignoreFailingMessages = true;
#endif

                base.InitializeAndStart();
                OpenXRSettings.Instance.renderMode = OpenXRSettings.RenderMode.SinglePassInstanced;
                yield return new WaitForXrFrame(2);

                int createSwapchainCalls = 0;
                MockRuntime.SetFunctionCallback("xrCreateSwapchain", (name) =>
                {
                    createSwapchainCalls += 1;
                    return XrResult.Success;
                });

                // This will also change the output of xrGetRecommendedLayerResolutionMETA
                // This is what the extension will check for the new extents.
                // This test assumes that the values chosen here are less than the default width/height.
                MockRuntime.ChangeRecommendedImageRectExtents(300, 300);

                yield return new WaitForXrFrame(2);

                Assert.IsTrue(createSwapchainCalls == 0, "We should not have created a new swapchain.");

                // Fetch the viewports on the RenderParameters
                var displays = new List<XRDisplaySubsystem>();
                SubsystemManager.GetSubsystems(displays);
                Assert.That(displays.Count, Is.EqualTo(1));
                Assert.That(displays[0].GetRenderPassCount(), Is.EqualTo(1));
                displays[0].GetRenderPass(0, out var renderPass);
                renderPass.GetRenderParameter(camera, 0, out var renderParam0);
                renderPass.GetRenderParameter(camera, 1, out var renderParam1);

                // This is the RenderParam viewport that we're setting within OpenXR
                // This is in normalized device coordinates.
                // Assuming we've decreased the width/height of the scale, the width should be < 1.
                Rect renderParamLeftRect = renderParam0.viewport;
                Rect renderParamRightRect = renderParam1.viewport;
                Assert.IsTrue(renderParamLeftRect.width < 1.0f, "Left Rectangle scale should be less than 1.");
                Assert.IsTrue(renderParamRightRect.width < 1.0f, "Right Rectangle scale should be less than 1.");

                yield return null;
#if UNITY_EDITOR
            Object.DestroyImmediate(cameraGO);
#else
                Object.Destroy(cameraGO);
#endif
                LogAssert.ignoreFailingMessages = false;
            }
            finally
            {
                UnityEngine.XR.OpenXR.Features.AutomaticDynamicResolutionFeature.SetUsingSuggestedResolutionScale(oldUsingSuggestedResolutionScale);
            }
        }

        /// <summary>
        /// Tests the Android Dynamic Resolution extension and specifically chooses a
        /// resolution that is lesser than the original. The viewport scale will be < 1
        /// and we check that this is populated on the XRDisplaySubsystem render parameters.
        /// </summary>
        [UnityTest]
        public IEnumerator AndroidRecommendedResolutionTest()
        {
            AddExtension("XR_ANDROID_recommended_resolution");
            bool oldUsingSuggestedResolutionScale = UnityEngine.XR.OpenXR.Features.AutomaticDynamicResolutionFeature.usingSuggestedResolutionScale;
            UnityEngine.XR.OpenXR.Features.AutomaticDynamicResolutionFeature.SetUsingSuggestedResolutionScale(true);

            try
            {
                var cameraGO = new GameObject("Test Cam");
                var camera = cameraGO.AddComponent<Camera>();

#if UNITY_ANDROID
            if (!SystemInfo.supportsMultiview)
                LogAssert.ignoreFailingMessages = true;
#endif

                base.InitializeAndStart();
                OpenXRSettings.Instance.renderMode = OpenXRSettings.RenderMode.SinglePassInstanced;
                yield return new WaitForXrFrame(2);

                int createSwapchainCalls = 0;
                MockRuntime.SetFunctionCallback("xrCreateSwapchain", (name) =>
                {
                    createSwapchainCalls += 1;
                    return XrResult.Success;
                });

                // This will change the output of xrEnumerateViewConfigurationViews
                // This is what the extension will check for the new extents.
                // This test assumes that the values chosen here are less than the default width/height.
                MockRuntime.ChangeRecommendedImageRectExtents(300, 300);

                // Trigger a Recommended Resolution Changed Event, so that the change to the recommended image rect extents will be seen.
                MockRuntime.CauseRecommendedResolutionChangedEvent();

                yield return new WaitForXrFrame(2);

                Assert.IsTrue(createSwapchainCalls == 0, "We should not have created a new swapchain.");

                // Fetch the viewports on the RenderParameters
                var displays = new List<XRDisplaySubsystem>();
                SubsystemManager.GetSubsystems(displays);
                Assert.That(displays.Count, Is.EqualTo(1));
                Assert.That(displays[0].GetRenderPassCount(), Is.EqualTo(1));
                displays[0].GetRenderPass(0, out var renderPass);
                renderPass.GetRenderParameter(camera, 0, out var renderParam0);
                renderPass.GetRenderParameter(camera, 1, out var renderParam1);

                // This is the RenderParam viewport that we're setting within OpenXR
                // This is in normalized device coordinates.
                // Assuming we've decreased the width/height of the scale, the width should be < 1.
                Rect renderParamLeftRect = renderParam0.viewport;
                Rect renderParamRightRect = renderParam1.viewport;
                Assert.IsTrue(renderParamLeftRect.width < 1.0f, "Left Rectangle scale should be less than 1.");
                Assert.IsTrue(renderParamRightRect.width < 1.0f, "Right Rectangle scale should be less than 1.");

                yield return null;
#if UNITY_EDITOR
            Object.DestroyImmediate(cameraGO);
#else
                Object.Destroy(cameraGO);
#endif
                LogAssert.ignoreFailingMessages = false;
            }
            finally
            {
                UnityEngine.XR.OpenXR.Features.AutomaticDynamicResolutionFeature.SetUsingSuggestedResolutionScale(oldUsingSuggestedResolutionScale);
            }
        }

        /// <summary>
        /// Tests the Meta Dynamic Resolution extension and specifically chooses a
        /// resolution that is greater than the original. The viewport scale will be > 1
        /// and will require a swapchain regeneration.
        /// </summary>
        [UnityTest]
        public IEnumerator MetaRecommendedResolutionSwapchainRegenerationTest()
        {
            AddExtension("XR_META_recommended_layer_resolution");
            bool oldUsingSuggestedResolutionScale = UnityEngine.XR.OpenXR.Features.AutomaticDynamicResolutionFeature.usingSuggestedResolutionScale;
            UnityEngine.XR.OpenXR.Features.AutomaticDynamicResolutionFeature.SetUsingSuggestedResolutionScale(true);

            try
            {
                base.InitializeAndStart();
                OpenXRSettings.Instance.renderMode = OpenXRSettings.RenderMode.SinglePassInstanced;
                yield return new WaitForXrFrame(2);

                // Modify xrCreateSwapchains so we can keep track of the number of calls
                int createSwapchainCalls = 0;
                MockRuntime.SetFunctionCallback("xrCreateSwapchain", (name) =>
                {
                    createSwapchainCalls += 1;
                    return XrResult.Success;
                });

                // This will also change the output of xrGetRecommendedLayerResolutionMETA
                // This is what the extension will check for the new extents.
                // This test assumes that the values chosen here are more than the default width/height.
                MockRuntime.ChangeRecommendedImageRectExtents(4000, 4000);

                yield return new WaitForXrFrame(2);

                Assert.IsTrue(createSwapchainCalls > 0, "We should have created a new swapchain.");

                yield return null;
            }
            finally
            {
                UnityEngine.XR.OpenXR.Features.AutomaticDynamicResolutionFeature.SetUsingSuggestedResolutionScale(oldUsingSuggestedResolutionScale);
            }
        }

        /// <summary>
        /// Tests the Android Dynamic Resolution extension and specifically chooses a
        /// resolution that is greater than the original. The viewport scale will be > 1
        /// and will require a swapchain regeneration.
        /// </summary>
        [UnityTest]
        public IEnumerator AndroidRecommendedResolutionSwapchainRegenerationTest()
        {
            AddExtension("XR_ANDROID_recommended_resolution");
            bool oldUsingSuggestedResolutionScale = UnityEngine.XR.OpenXR.Features.AutomaticDynamicResolutionFeature.usingSuggestedResolutionScale;
            UnityEngine.XR.OpenXR.Features.AutomaticDynamicResolutionFeature.SetUsingSuggestedResolutionScale(true);

            try
            {
                base.InitializeAndStart();
                OpenXRSettings.Instance.renderMode = OpenXRSettings.RenderMode.SinglePassInstanced;
                yield return new WaitForXrFrame(2);

                // Modify xrCreateSwapchains so we can keep track of the number of calls
                int createSwapchainCalls = 0;
                MockRuntime.SetFunctionCallback("xrCreateSwapchain", (name) =>
                {
                    createSwapchainCalls += 1;
                    return XrResult.Success;
                });

                // This will change the output of xrEnumerateViewConfigurationViews
                // This is what the extension will check for the new extents.
                // This test assumes that the values chosen here are less than the default width/height.
                MockRuntime.ChangeRecommendedImageRectExtents(4000, 4000);

                // Trigger a Recommended Resolution Changed Event, so that the change to the recommended image rect extents will be seen.
                MockRuntime.CauseRecommendedResolutionChangedEvent();

                yield return new WaitForXrFrame(2);

                Assert.IsTrue(createSwapchainCalls > 0, "We should have created a new swapchain.");

                yield return null;
            }
            finally
            {
                UnityEngine.XR.OpenXR.Features.AutomaticDynamicResolutionFeature.SetUsingSuggestedResolutionScale(oldUsingSuggestedResolutionScale);
            }
        }

        /// <summary>
        /// Simulates what can happen when trying to reconnect Link mode after a link disconnect, but
        /// the headset is reporting a form factor unavailable error.
        /// In this case, the app has already been started, and the runtime reports a form factor unavailable
        /// error when trying to initialize xr again. What should happen is a restart loop to restart XR.
        /// </summary>
        [UnityTest]
        public IEnumerator RestartLoopTest()
        {
            float initialTimeBetweenRestarts = OpenXRRestarter.TimeBetweenRestartAttempts;
            bool initialKeepFunctionCallbacks = MockRuntime.KeepFunctionCallbacks;
            var initialXRGetSystemCallback = MockRuntime.GetBeforeFunctionCallback("xrGetSystem");
            try
            {
                MockRuntime.KeepFunctionCallbacks = true;
                float timeBetweenRestarts = 0.5f;

                yield return null;

                // Reduce the time between restarts to reduce the time of this test.
                OpenXRRestarter.TimeBetweenRestartAttempts = timeBetweenRestarts;

                int resetAttempts = 0;
                MockRuntime.SetFunctionCallback("xrGetSystem", (name) =>
                {
                    OpenXRLoaderBase.Internal_SetSuccessfullyInitialized(true);
                    MockRuntime.KeepFunctionCallbacks = true;
                    resetAttempts += 1;
                    if (resetAttempts <= 2)
                    {
                        return XrResult.FormFactorUnavailable;
                    }
                    else
                    {
                        return XrResult.Success;
                    }
                });

                // Trigger initialize, which should throw an error from xrGetSystem,
                // This will trigger a restart, which should trigger another error from xrGetSystem,
                // Which should trigger another restart, etc. until xrGetSystem returns a success.
                LogAssert.ignoreFailingMessages = true;
                base.InitializeAndStart();

                yield return new WaitForLoaderRestart(10, true);
                Assert.AreEqual(3, resetAttempts);
            }
            finally
            {
                MockRuntime.KeepFunctionCallbacks = initialKeepFunctionCallbacks;
                MockRuntime.SetFunctionCallback("xrGetSystem", initialXRGetSystemCallback);
                OpenXRRestarter.TimeBetweenRestartAttempts = initialTimeBetweenRestarts;
                OpenXRLoaderBase.Internal_SetSuccessfullyInitialized(false);
            }
        }

        /// <summary>
        /// Tests that OpenXRRuntime.wantsToRestart is a global switch for de-activating the restarting of XR.
        /// </summary>
        [UnityTest]
        public IEnumerator RestartLoopDisabledTest()
        {
            OpenXRRuntime.wantsToRestart += () => false;
            OpenXRRuntime.wantsToQuit += () => true;
            float initialTimeBetweenRestarts = OpenXRRestarter.TimeBetweenRestartAttempts;
            var initialXRGetSystemCallback = MockRuntime.GetBeforeFunctionCallback("xrGetSystem");
            try
            {
                float timeBetweenRestarts = 1.0f;

                yield return null;

                // Should have 0 restart attempts before starting.
                Debug.Log("Restart Attempts:" + OpenXRRestarter.PauseAndRestartAttempts.ToString());
                Assert.AreEqual(0, OpenXRRestarter.PauseAndRestartAttempts);

                // Reduce the time between restarts to reduce the time of this test.
                OpenXRRestarter.TimeBetweenRestartAttempts = timeBetweenRestarts;

                // Trigger initialize, which should throw the form factor unavailable error.
                MockRuntime.SetFunctionCallback("xrGetSystem", (name) =>
                {
                    OpenXRLoaderBase.Internal_SetSuccessfullyInitialized(true);
                    return XrResult.FormFactorUnavailable;
                });
                base.InitializeAndStart();

                // This retry attempt should not succeed since we manually set wantsToRestart = false.
                yield return new WaitForLoaderShutdown();
                Assert.IsTrue(OpenXRLoader.Instance == null, "OpenXR should not be initialized");
            }
            finally
            {
                OpenXRRestarter.TimeBetweenRestartAttempts = initialTimeBetweenRestarts;
                OpenXRRuntime.wantsToRestart -= () => false;
                OpenXRRuntime.wantsToQuit -= () => true;
                MockRuntime.SetFunctionCallback("xrGetSystem", initialXRGetSystemCallback);
                OpenXRLoaderBase.Internal_SetSuccessfullyInitialized(false);
            }
        }

        /// <summary>
        /// Simulates what happens when trying to initialize xr for the first time, and the headset is disconnected,
        /// causing xrGetSystem to report a form factor unavailable error.
        /// By default, xr initialization should not be retried per feedback from Microsoft.
        /// </summary>
        [UnityTest]
        public IEnumerator RestartLoopDisabledBeforeInitializationTest()
        {
            OpenXRLoaderBase.Internal_SetSuccessfullyInitialized(false);
            float initialTimeBetweenRestarts = OpenXRRestarter.TimeBetweenRestartAttempts;
            var initialXRGetSystemCallback = MockRuntime.GetBeforeFunctionCallback("xrGetSystem");
            try
            {
                float timeBetweenRestarts = 1.0f;

                yield return null;

                // Reduce the time between restarts to reduce the time of this test.
                OpenXRRestarter.TimeBetweenRestartAttempts = timeBetweenRestarts;

                // When XRGetSystem is called, start listening for a null OpenXRLoader since initialization has
                // already started.
                WaitForNullXRLoader nullLoaderYieldInstruction = new WaitForNullXRLoader();
                MockRuntime.SetFunctionCallback("xrGetSystem", (name) =>
                {
                    Debug.Log("Calling XRGetSystem");
                    nullLoaderYieldInstruction.StartListening();
                    return XrResult.FormFactorUnavailable;
                });

                // Trigger initialize, which should throw the form factor unavailable error.
                base.InitializeAndStart();

                yield return nullLoaderYieldInstruction;
                Assert.IsTrue(OpenXRLoader.Instance == null, "OpenXR should not be initialized");
            }
            finally
            {
                OpenXRRestarter.TimeBetweenRestartAttempts = initialTimeBetweenRestarts;
                MockRuntime.SetFunctionCallback("xrGetSystem", initialXRGetSystemCallback);
                OpenXRLoaderBase.Internal_SetSuccessfullyInitialized(false);
            }
        }

        /// <summary>
        /// Simulates what happens when trying to initialize xr for the first time, and the runtime reports
        /// a form factor unavailable error for xrGetSystem. If the user chooses, the runtime can retry initialization again and again
        /// while waiting for the xrGetSystem call to succeed.
        /// By default, xr initialization should not be retried per feedback from Microsoft. This loop needs to be
        /// explicitly enabled.
        /// </summary>
        [UnityTest]
        public IEnumerator XRGetSystemLoopBeforeInitializationTest()
        {
            OpenXRLoaderBase.Internal_SetSuccessfullyInitialized(false);
            float initialTimeBetweenRestarts = OpenXRRestarter.TimeBetweenRestartAttempts;
            bool initialKeepFunctionCallbacks = MockRuntime.KeepFunctionCallbacks;
            var initialXRGetSystemCallback = MockRuntime.GetBeforeFunctionCallback("xrGetSystem");
            bool initialRetryInitializationOnFormFactorErrors = OpenXRRuntime.retryInitializationOnFormFactorErrors;
            try
            {
                yield return null;

                // Enable this to prevent xrGetSystem callback override from getting overwritten.
                MockRuntime.KeepFunctionCallbacks = true;

                // Reduce the time between restarts to reduce the time of this test.
                OpenXRRestarter.TimeBetweenRestartAttempts = 1.0f;

                // Enable the xrGetSystem retry loop per this test.
                OpenXRRuntime.retryInitializationOnFormFactorErrors = true;

                // Enable this to ignore the error messages in the logs.
                LogAssert.ignoreFailingMessages = true;

                int resetAttempts = 0;
                MockRuntime.SetFunctionCallback("xrGetSystem", (name) =>
                {
                    MockRuntime.KeepFunctionCallbacks = true;

                    resetAttempts += 1;
                    if (resetAttempts <= 2)
                    {
                        return XrResult.FormFactorUnavailable;
                    }
                    else
                    {
                        return XrResult.Success;
                    }
                });

                // Trigger initialize, which should call into xrGetSystem.
                base.InitializeAndStart();

                yield return new WaitForLoaderRestart(10, true);
                Assert.AreEqual(3, resetAttempts);
                Assert.IsTrue(OpenXRLoader.Instance != null, "OpenXR should be initialized");
            }
            finally
            {
                MockRuntime.KeepFunctionCallbacks = initialKeepFunctionCallbacks;
                OpenXRRuntime.retryInitializationOnFormFactorErrors = initialRetryInitializationOnFormFactorErrors;
                OpenXRRestarter.TimeBetweenRestartAttempts = initialTimeBetweenRestarts;
                MockRuntime.SetFunctionCallback("xrGetSystem", initialXRGetSystemCallback);
                OpenXRLoaderBase.Internal_SetSuccessfullyInitialized(false);
            }
        }

        [UnityTest]
        public IEnumerator WantsToRestartTrue()
        {
            OpenXRRuntime.wantsToRestart += () => true;
            OpenXRRuntime.wantsToRestart += () => true;
            OpenXRRuntime.wantsToRestart += () => true;

            InitializeAndStart();

            yield return new WaitForXrFrame(2);

            MockRuntime.TransitionToState(XrSessionState.LossPending, true);

            yield return new WaitForLoaderRestart();
            yield return new WaitForXrFrame(1);
        }

        [UnityTest]
        public IEnumerator WantsToRestartFalse()
        {
            OpenXRRuntime.wantsToRestart += () => true;
            OpenXRRuntime.wantsToRestart += () => false;
            OpenXRRuntime.wantsToRestart += () => true;

            InitializeAndStart();
            yield return new WaitForXrFrame(2);

            MockRuntime.TransitionToState(XrSessionState.LossPending, true);

            yield return new WaitForLoaderShutdown();
        }

        [UnityTest]
        public IEnumerator WantsToQuitTrue()
        {
            var onQuit = false;
            OpenXRRuntime.wantsToQuit += () => true;
            OpenXRRuntime.wantsToQuit += () => true;
            OpenXRRuntime.wantsToQuit += () => true;
            OpenXRRestarter.Instance.onQuit += () => onQuit = true;

            InitializeAndStart();
            yield return new WaitForXrFrame(2);

            MockRuntime.CauseInstanceLoss();

            yield return new WaitForLoaderShutdown();

            Assert.IsTrue(OpenXRLoader.Instance == null, "OpenXR should not be running");
            Assert.IsTrue(onQuit, "Quit was not called");
        }

        [UnityTest]
        public IEnumerator WantsToQuitFalse()
        {
            var onQuit = false;
            OpenXRRuntime.wantsToQuit += () => true;
            OpenXRRuntime.wantsToQuit += () => false;
            OpenXRRuntime.wantsToQuit += () => true;
            OpenXRRestarter.Instance.onQuit += () => onQuit = true;

            InitializeAndStart();
            yield return new WaitForXrFrame(2);

            MockRuntime.CauseInstanceLoss();

            yield return new WaitForLoaderShutdown();

            Assert.IsTrue(OpenXRLoader.Instance == null, "OpenXR should not be running");
            Assert.IsFalse(onQuit, "Quit was not called");
        }

        [UnityTest]
        public IEnumerator LossPendingCausesRestart()
        {
            bool lossPendingReceived = false;
            MockRuntime.Instance.TestCallback = (methodName, param) =>
            {
                switch (methodName)
                {
                    case nameof(OpenXRFeature.OnSessionLossPending):
                        lossPendingReceived = true;
                        break;
                }

                return true;
            };

            InitializeAndStart();

            yield return new WaitForXrFrame(1);

            Assert.IsTrue(MockRuntime.TransitionToState(XrSessionState.LossPending, true), "Failed to transition to loss pending state");

            yield return new WaitForLoaderRestart();

            Assert.IsTrue(lossPendingReceived);
        }

        [UnityTest]
        public IEnumerator CreateSwapChainRuntimeError()
        {
            MockRuntime.SetFunctionCallback("xrCreateSwapchain", (func) => XrResult.RuntimeFailure);

            InitializeAndStart();

            yield return new WaitForLoaderShutdown();

            Assert.IsTrue(OpenXRLoader.Instance == null, "OpenXR should not be initialized");
        }

        /// <summary>
        /// Simulates what can happen when trying to reconnect Link mode after a link disconnect. During
        /// xr re-initialization, creating the swapchain can result in a session lost error, which should trigger a
        /// loop to attempt to restart xr.
        /// </summary>
        [UnityTest]
        public IEnumerator CreateSwapChainSessionLostError()
        {
            OpenXRLoaderBase.Internal_SetSuccessfullyInitialized(true);
            float initialTimeBetweenRestarts = OpenXRRestarter.TimeBetweenRestartAttempts;
            var initialXRCreateSwapchainCallback = MockRuntime.GetBeforeFunctionCallback("xrCreateSwapchain");
            try
            {
                float timeBetweenRestarts = 1.0f;

                // Reduce the time between restarts to reduce the time of this test.
                OpenXRRestarter.TimeBetweenRestartAttempts = timeBetweenRestarts;

                MockRuntime.SetFunctionCallback("xrCreateSwapchain", (func) => XrResult.SessionLost);
                InitializeAndStart();

                yield return new WaitForLoaderRestart(10, true);

                Assert.IsTrue(OpenXRLoader.Instance != null, "OpenXR should be initialized");
            }
            finally
            {
                OpenXRRestarter.TimeBetweenRestartAttempts = initialTimeBetweenRestarts;
                MockRuntime.SetFunctionCallback("xrCreateSwapchain", initialXRCreateSwapchainCallback);
                OpenXRLoaderBase.Internal_SetSuccessfullyInitialized(false);
            }
        }

        [UnityTest]
        public IEnumerator CreateSessionRuntimeFailure()
        {
            MockRuntime.SetFunctionCallback("xrCreateSession", (func) => XrResult.RuntimeFailure);

            InitializeAndStart();

            yield return null;

            Assert.IsTrue(DoesDiagnosticReportContain(new System.Text.RegularExpressions.Regex(@"xrCreateSession: XR_ERROR_RUNTIME_FAILURE")));
            Assert.IsTrue(OpenXRLoader.Instance.currentLoaderState == OpenXRLoaderBase.LoaderState.Stopped, "OpenXR should be stopped");
        }

        [UnityTest]
        public IEnumerator EndFrameRuntimeFailure()
        {
            InitializeAndStart();

            yield return new WaitForXrFrame(2);

            MockRuntime.SetFunctionCallback("xrEndFrame", (func) => XrResult.RuntimeFailure);

            yield return null;
            yield return null;
            yield return null;

            Assert.IsTrue(DoesDiagnosticReportContain(new System.Text.RegularExpressions.Regex(@"xrEndFrame: XR_ERROR_RUNTIME_FAILURE")));
            Assert.IsTrue(OpenXRLoader.Instance == null, "OpenXR should be shutdown");
        }

        [UnityTest]
        public IEnumerator MultipleRestart()
        {
            InitializeAndStart();
            yield return new WaitForXrFrame();

            OpenXRRestarter.Instance.ShutdownAndRestart();
            yield return new WaitForXrFrame();

            OpenXRRestarter.Instance.ShutdownAndRestart();
            yield return new WaitForXrFrame();
        }

        [UnityTest]
        [UnityPlatform(include = new[] { RuntimePlatform.Android })]
        public IEnumerator AndroidThreadSettingsSetAtInitialization()
        {
            InitializeAndStart();

            yield return new WaitForXrFrame(1);

            if (!OpenXRRuntime.IsExtensionEnabled("XR_KHR_android_thread_settings"))
            {
                Assert.Inconclusive("Current XR runtime is not compatible with XR_KHR_android_thread_settings extension");
            }

            var threadSettingsCount = MockRuntime.GetRegisteredAndroidThreadsCount();
            var mainThreadFound = MockRuntime.IsAndroidThreadTypeRegistered(1); // XR_ANDROID_THREAD_TYPE_APPLICATION_MAIN_KHR
            var renderThreadFound = MockRuntime.IsAndroidThreadTypeRegistered(3); // XR_ANDROID_THREAD_TYPE_RENDERER_MAIN_KHR

            StopAndShutdown();

            Debug.Log($"threadSettings.Count={threadSettingsCount}");
            Assert.AreEqual(2, threadSettingsCount, "Unexpected number of Android thread settings registered.");
            Assert.IsTrue(mainThreadFound, "Main thread not found in Android thread settings.");
            Assert.IsTrue(renderThreadFound, "Graphics thread not found in Android thread settings.");
        }

        /// <summary>
        /// To test out BuiltinExtensions behavior works as expected. Refer to unity_builtin_extension_list.cpp to see all supported buildinExtensions.
        /// For EnabledByDefault_NoOverride type, when exts are available, exts should be enabled by default. If not available, exts not enabled.
        /// For EnabledByDefault_AllowOverride type, when exts available, exts enabled externally. ext should be enabled.
        /// For EnabledExternally_NoOverride type, when exts available, If enabled externally, ext should be enabled.
        /// </summary>
        [UnityTest]
        public IEnumerator BuiltInExtensionsBehavior()
        {
            // Enable exts externally for test purposes.
            AddExtension("XR_META_performance_metrics");
            AddExtension("XR_FB_swapchain_update_state");
            AddExtension("XR_EXT_performance_settings");
            AddExtension("XR_META_vulkan_swapchain_create_info");
            InitializeAndStart();

            yield return new WaitForXrFrame(1);
            // EnableByDefault_NoOverride type:
            // single ext case:  {{"XR_MSFT_holographic_window_attachment"}, CreateMSFTCoreWindowExtension, kBuiltinExtensionEnableType_EnableByDefault_NoOverride}
            // XR_MSFT_holographic_window_attachment is not available, so should not be enabled.
            Assert.IsFalse(OpenXRRuntime.IsExtensionEnabled("XR_MSFT_holographic_window_attachment"), "XR_MSFT_holographic_window_attachment EnableByDefault_NoOverride is not available, so should not enabled.");

            // multiple exts case:  {{"XR_FB_foveation", "XR_FB_foveation_configuration", "XR_FB_swapchain_update_state"}, CreateSymmetricProjectionExtension, kBuiltinExtensionEnableType_EnableByDefault_NoOverride}
            // XR_FB_foveation & XR_FB_foveation_configuration are available, so should be enabled by default.
            Assert.IsTrue(OpenXRRuntime.IsExtensionEnabled("XR_FB_foveation"), "XR_FB_foveation EnableByDefault_NoOverride is available and should be enabled.");
            Assert.IsTrue(OpenXRRuntime.IsExtensionEnabled("XR_FB_foveation_configuration"), "XR_FB_foveation_configuration EnableByDefault_NoOverride is available and should be enabled.");

            // EnabledByDefault_AllowOverride type:
            // {{"XR_META_performance_metrics"}, CreateMetaPerformanceMetricsExtension, kBuiltinExtensionEnableType_EnableByDefault_AllowOverride}
            // XR_META_performance_metrics is available, and enabled externally, so should be enabled.
            Assert.IsTrue(OpenXRRuntime.IsExtensionEnabled("XR_META_performance_metrics"), "XR_META_performance_metrics EnabledByDefault_AllowOverride, should be enabled.");

            // EnabledExternally_NoOverride type:
            // {{"XR_EXT_performance_settings"}, CreatePerformanceSettingsExtension, kBuiltinExtensionEnableType_EnabledExternally_NoOverride }
            // XR_EXT_performance_settings is available, and enabled externally, so should be enabled.
            Assert.IsTrue(OpenXRRuntime.IsExtensionEnabled("XR_EXT_performance_settings"), "XR_EXT_performance_settings EnabledExternally_NoOverride enabled externally, should be enabled.");

            // {{"XR_META_vulkan_swapchain_create_info"}, CreateMetaVulkanSwapchainExtension, kBuiltinExtensionEnableType_EnabledExternally_NoOverride}
            // XR_META_performance_metrics is not available, but enabled externally, so should not be enabled.
            Assert.IsFalse(OpenXRRuntime.IsExtensionEnabled("XR_META_vulkan_swapchain_create_info"), "XR_META_vulkan_swapchain_create_info EnabledExternally_NoOverride is not available, should not be enabled.");

            // {{"XR_FB_foveation", "XR_FB_foveation_configuration", "XR_FB_swapchain_update_state", "XR_FB_foveation_vulkan"}, CreateOculusFoveationExtension, kBuiltinExtensionEnableType_EnabledExternally_NoOverride}
            // XR_FB_swapchain_update_state is not available, but enabled externally. XR_FB_foveation_vulkan is not availble.
            Assert.IsFalse(OpenXRRuntime.IsExtensionEnabled("XR_FB_foveation_vulkan"), "XR_FB_foveation_vulkan extension should be EnabledExternally_NoOverride.");
        }
    }
}
