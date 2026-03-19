#region PerformanceNotificationExample
using UnityEngine;
using UnityEngine.XR.OpenXR.Features.Extensions.PerformanceSettings;

namespace UnityEngine.XR.OpenXR.CodeSamples.Editor.Tests
{
    public class PerformanceNotificationExample : MonoBehaviour
    {
        void OnEnable()
        {
            // Subscribe to the performance notification event at runtime
            XrPerformanceSettingsFeature.OnXrPerformanceChangeNotification +=
                OnPerformanceChangeNotification;
        }

        void OnDisable()
        {
            XrPerformanceSettingsFeature.OnXrPerformanceChangeNotification -=
                OnPerformanceChangeNotification;
        }

        // Process the notification when it happens
        void OnPerformanceChangeNotification(PerformanceChangeNotification notification)
        {
            switch (notification.toLevel)
            {
                case PerformanceNotificationLevel.Normal:
                    // Let the application run as normal execution
                    RestorePerformance(
                        notification.domain,
                        notification.subDomain
                    );
                    break;
                case PerformanceNotificationLevel.Warning:
                    // Reduce workload of low priority tasks
                    LimitPerformance(notification.domain, notification.subDomain);
                    break;
                case PerformanceNotificationLevel.Impaired:
                    // Execute app with the minimum required processes
                    // for the given domain and subdomain
                    ReducePerformance(notification.domain, notification.subDomain);
                    break;
            }
        }

        // Example stubs for user-defined methods
        void RestorePerformance(
            PerformanceDomain domain,
            PerformanceSubDomain subDomain)
        { /* ... */ }
        void LimitPerformance(
            PerformanceDomain domain,
            PerformanceSubDomain subDomain)
        { /* ... */ }
        void ReducePerformance(
            PerformanceDomain domain,
            PerformanceSubDomain subDomain)
        { /* ... */ }
    }
}
#endregion
// Used in Documentation~/features/performance-settings.md
// This example demonstrates subscribing to and handling performance notifications.
