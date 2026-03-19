namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Represents the types of features that can be configured for a capability. Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    public enum XrSpatialCapabilityFeatureEXT
    {
        /// <summary>
        /// Represents the fixed-sized marker feature provided by `XR_EXT_spatial_marker_tracking`.
        /// Equivalent to the OpenXR value `XR_SPATIAL_CAPABILITY_FEATURE_MARKER_TRACKING_FIXED_SIZE_MARKERS_EXT`.
        /// </summary>
        MarkerTrackingFixedSizeMarkers = 1000743000,

        /// <summary>
        /// Represents the static marker feature provided by `XR_EXT_spatial_marker_tracking`.
        /// Equivalent to the OpenXR value `XR_SPATIAL_CAPABILITY_FEATURE_MARKER_TRACKING_STATIC_MARKERS_EXT`.
        /// </summary>
        MarkerTrackingStaticMarkers = 1000743001,
    }
}
