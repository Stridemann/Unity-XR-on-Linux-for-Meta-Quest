using System.Runtime.InteropServices;
#if UNITY_EDITOR
using UnityEditor.XR.OpenXR.Features;
#endif
using UnityEngine.Events;
using UnityEngine.XR.OpenXR.NativeTypes;

namespace UnityEngine.XR.OpenXR.Features.Extensions.PerformanceSettings
{
    /// <summary>
    /// Allows interaction with the Performance settings API,
    /// which allows the application to provide hints to the runtime about the performance characteristics of the application,
    /// and to receive notifications from the runtime about changes in performance state.
    /// </summary>
    /// <remarks>
    /// Refer to [XR performance settings](xref:openxr-performance-settings) for additional information.
    /// </remarks>
#if UNITY_EDITOR
    [OpenXRFeature(
        UiName = "XR Performance Settings",
        Desc = "Optional extension for providing performance hints to runtime and receive notifications aobut device performance changes.",
        Company = "Unity",
        DocumentationLink = Constants.k_DocumentationManualURL + "features/performance-settings.html",
        OpenxrExtensionStrings = extensionString,
        Version = "1.0.0",
        FeatureId = featureId
    )]
#endif
    public class XrPerformanceSettingsFeature : OpenXRFeature
    {
        /// <summary>
        /// The feature id string.
        /// </summary>
        public const string featureId = "com.unity.openxr.feature.extension.performance_settings";

        /// <summary>
        /// Name of the OpenXR extension for performance settings.
        /// </summary>
        public const string extensionString = "XR_EXT_performance_settings";

        /// <summary>
        /// Subscribe to this event to receive performance change notifications from the OpenXR runtime.
        /// </summary>
        /// <remarks>
        /// Refer to [Performance notifications](xref:openxr-performance-settings#performance-settings-notifications) for additional information.
        /// </remarks>
        /// <example>
        /// <para>
        /// Example of subscribing to the event and handling the performance change notification:
        /// </para>
        /// <code lang="cs">
        /// <![CDATA[
        /// XrPerformanceSettingsFeature.OnXrPerformanceChangeNotification += OnGPUPerformanceChange;
        /// ]]>
        /// </code>
        /// <para>
        /// Example of handling the performance change notification:
        /// </para>
        /// <code lang="cs">
        /// <![CDATA[
        /// void OnGPUPerformanceChange(PerformanceChangeNotification notification)
        /// {
        ///     if (notification.domain != Domain.GPU)
        ///     {
        ///         return;
        ///     }
        ///
        ///     if (notification.toLevel ==  PerformanceNotificationLevel.Normal)
        ///     {
        ///         // Performance has improved
        ///         UseDefaultQuality();
        ///     }
        ///     else
        ///     {
        ///         // Performance has degraded
        ///         UseReducedQuality();
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public static event UnityAction<PerformanceChangeNotification> OnXrPerformanceChangeNotification;

        /// <summary>
        /// Provides the OpenXR runtime with the desired performance level to be used for the specified domain.
        /// </summary>
        /// <param name="domain">Domain for which the performance hit will be sent.</param>
        /// <param name="level">Desired performance asked by the application.</param>
        /// <returns>True if the performance level hint was successfully set, false otherwise.</returns>
        /// <remarks>
        /// Refer to [Performance level hints](xref: openxr-performance-settings#performance-settings-level-hints) for additional information.
        /// </remarks>
        public static bool SetPerformanceLevelHint(PerformanceDomain domain, PerformanceLevelHint level)
        {
            if (OpenXRRuntime.IsExtensionEnabled(extensionString))
            {
                return NativeApi.xr_performance_settings_setPerformanceLevel(domain, level);
            }
            return false;
        }

        /// <summary>
        /// When an instance of the Performance setting feature is created, it allows us to confirm that the instance has been created, that the extension is enabled
        /// and we have successfully registed the performance notification callback
        /// </summary>
        /// <param name="xrInstance">XR Session Instance</param>
        /// <returns>True if the instance has successfully been created. Otherwise it returns false.</returns>
        protected internal override bool OnInstanceCreate(ulong xrInstance)
        {
            return base.OnInstanceCreate(xrInstance) &&
                OpenXRRuntime.IsExtensionEnabled(extensionString) &&
                NativeApi.xr_performance_settings_setEventCallback(OnXrPerformanceNotificationCallback);
        }

        [AOT.MonoPInvokeCallback(typeof(NativeApi.XrPerformanceNotificationDelegate))]
        private static void OnXrPerformanceNotificationCallback(PerformanceChangeNotification notification)
        {
            OnXrPerformanceChangeNotification?.Invoke(notification);
        }

        internal static class NativeApi
        {
            internal delegate void XrPerformanceNotificationDelegate(PerformanceChangeNotification notification);

            [DllImport("UnityOpenXR")]
            [return: MarshalAs(UnmanagedType.U1)]
            internal static extern bool xr_performance_settings_setEventCallback(XrPerformanceNotificationDelegate callback);

            [DllImport("UnityOpenXR")]
            [return: MarshalAs(UnmanagedType.U1)]
            internal static extern bool xr_performance_settings_setPerformanceLevel(PerformanceDomain domain, PerformanceLevelHint level);
        }
    }
}
