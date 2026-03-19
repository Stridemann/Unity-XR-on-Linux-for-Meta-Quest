using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace UnityEngine.XR.OpenXR.Features.Extensions.PerformanceSettings
{
    /// <summary>
    /// Hardware system.
    /// </summary>
    /// <remarks>
    /// Use the members of this enumeration when setting a performance hint with <see cref="XRPerformanceSettingsFeature.SetPerformanceLevelHint(PerformanceDomain, PerformanceLevelHint)"/>.
    ///
    /// Members of this enumeration are reported in the events dispatched by <see cref="XRPerformanceSettingsFeature.UnityAction{PerformanceChangeNotification} OnXrPerformanceChangeNotification"/>.
    /// </remarks>
    public enum PerformanceDomain
    {
        /// <summary>
        /// CPU hardware domain.
        /// </summary>
        Cpu = 1,
        /// <summary>
        /// Graphics hardware domain.
        /// </summary>
        Gpu = 2
    }

    /// <summary>
    /// Specific context of the hardware domain.
    /// </summary>
    /// <remarks>
    /// Members of this enumeration are reported in the events dispatched by
    /// <see cref="XRPerformanceSettingsFeature.UnityAction{PerformanceChangeNotification} OnXrPerformanceChangeNotification"/>.
    /// </remarks>
    public enum PerformanceSubDomain
    {
        /// <summary>
        /// Composition of submitted layers.
        /// </summary>
        Compositing = 1,
        /// <summary>
        /// Graphics rendering and frame submission.
        /// </summary>
        Rendering = 2,
        /// <summary>
        /// Physical device temperature.
        /// </summary>
        Thermal = 3
    }

    /// <summary>
    /// Performance level of the platform.
    /// </summary>
    /// <remarks>
    /// Use the members of this enumeration when setting a performance hint with
    /// <see cref="XRPerformanceSettingsFeature.SetPerformanceLevelHint(PerformanceDomain, PerformanceLevelHint)"/>.
    /// </remarks>
    public enum PerformanceLevelHint
    {
        /// <summary>
        /// The application will enter a non-XR section,
        /// so power savings will be prioritized over all.
        /// </summary>
        PowerSavings = 0,
        /// <summary>
        /// The application will enter a low and stable complexity section,
        /// so power usage will be prioritized over late frame rendering.
        /// </summary>
        SustainedLow = 25,
        /// <summary>
        /// The application will enter a high or dynamic complexity section,
        /// so the application performance will be prioritized within sustainable thermal ranges.
        /// </summary>
        SustainedHigh = 50,
        /// <summary>
        /// The application will enter a very high complexity section,
        /// so performance will be boosted over sustainable thermal ranges.
        /// Note that usage of this level hint is recommended for short durations (less than 30 seconds).
        /// </summary>
        Boost = 75
    }

    /// <summary>
    /// Notification level about the performance state of the platform.
    /// </summary>
    /// <remarks>
    /// Members of this enumeration are reported in the events dispatched by
    /// <see cref="XRPerformanceSettingsFeature.UnityAction{PerformanceChangeNotification} OnXrPerformanceChangeNotification"/>.
    /// </remarks>
    public enum PerformanceNotificationLevel
    {
        /// <summary>
        /// Performance is in nominal status.
        /// </summary>
        Normal = 0,
        /// <summary>
        /// Early warning for potential performance degradation.
        /// </summary>
        Warning = 25,
        /// <summary>
        /// Performance is degraded.
        /// </summary>
        Impaired = 75
    }

    /// <summary>
    /// Notification about the performance state of the platform.
    /// </summary>
    /// <remarks>
    /// This struct is part of the events dispatched by
    /// <see cref="XRPerformanceSettingsFeature.UnityAction{PerformanceChangeNotification} OnXrPerformanceChangeNotification"/>.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct PerformanceChangeNotification
    {
        /// <summary>
        /// Platform domain that the notification is about.
        /// </summary>
        public PerformanceDomain domain;
        /// <summary>
        /// Platform subdomain that the notification is about.
        /// </summary>
        public PerformanceSubDomain subDomain;
        /// <summary>
        /// Previous performance level.
        /// </summary>
        public PerformanceNotificationLevel fromLevel;
        /// <summary>
        /// Upcoming performance level.
        /// </summary>
        public PerformanceNotificationLevel toLevel;
    }
}
