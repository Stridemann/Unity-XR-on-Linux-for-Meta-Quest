using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools;
using UnityEngine.XR.OpenXR.Features.Extensions.PerformanceSettings;
using UnityEngine.XR.OpenXR.Features.Mock;

namespace UnityEngine.XR.OpenXR.Tests
{
    class OpenXRPerformanceSettingsTest : OpenXRLoaderSetup
    {
        [UnityTest]
        public IEnumerator PerformanceLevelHintIsSet()
        {
            EnableMockRuntime();
            EnableFeature<XrPerformanceSettingsFeature>();
            InitializeAndStart();
            yield return new WaitForXrFrame(2);

            const PerformanceDomain performanceDomain = PerformanceDomain.Cpu;
            const PerformanceLevelHint expectedPerformanceLevel = PerformanceLevelHint.SustainedLow;

            // Set performance level hint
            bool callSuccess = XrPerformanceSettingsFeature.SetPerformanceLevelHint(performanceDomain, expectedPerformanceLevel);

            yield return new WaitForXrFrame();

            // Get performance level hint in MockRuntime
            PerformanceLevelHint performanceHintInMock = MockRuntime.PerformanceSettings_GetPerformanceLevelHint(performanceDomain);

            StopAndShutdown();

            Assert.IsTrue(callSuccess, "Setting performance level hint failed.");
            Assert.AreEqual(expectedPerformanceLevel, performanceHintInMock, "Performance level hint wasn't set correctly.");
        }

        [UnityTest]
        public IEnumerator ReceiveEventNotification_NormalToWarning()
        {
            EnableMockRuntime();
            EnableFeature<XrPerformanceSettingsFeature>();
            InitializeAndStart();
            yield return new WaitForXrFrame(2);

            PerformanceChangeNotification expectedNotification = new()
            {
                domain = PerformanceDomain.Cpu,
                subDomain = PerformanceSubDomain.Thermal,
                fromLevel = PerformanceNotificationLevel.Normal,
                toLevel = PerformanceNotificationLevel.Warning
            };
            bool receivedEvent = false;
            bool notificationMatches = false;

            // Subscribe to performance level change event
            XrPerformanceSettingsFeature.OnXrPerformanceChangeNotification += (notification) =>
            {
                receivedEvent = true;
                notificationMatches = expectedNotification.Equals(notification);
            };

            // Trigger a performance level change event
            MockRuntime.PerformanceSettings_CauseNotification(PerformanceDomain.Cpu, PerformanceSubDomain.Thermal, PerformanceNotificationLevel.Warning);
            yield return new WaitForXrFrame(2);

            StopAndShutdown();

            // Verify that the event was received
            Assert.IsTrue(receivedEvent, "Performance change notification event wasn't received.");
            Assert.IsTrue(notificationMatches, "Performance change notification doesn't match expected notification.");
        }

        [UnityTest]
        public IEnumerator ExtensionNotInitialized()
        {
            EnableFeature<XrPerformanceSettingsFeature>(false);
            InitializeAndStart();
            yield return new WaitForXrFrame(2);

            try
            {
                Assert.IsFalse(XrPerformanceSettingsFeature.SetPerformanceLevelHint(PerformanceDomain.Cpu, PerformanceLevelHint.SustainedLow), "Setting performance level hint should fail when the extension is not initialized.");
            }
            finally
            {
                StopAndShutdown();
            }
        }
    }
}
