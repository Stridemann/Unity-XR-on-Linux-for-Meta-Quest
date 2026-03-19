namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Represents the types of capabilities that can be enabled for a spatial context.
    /// Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    public enum XrSpatialCapabilityEXT
    {
        /// <summary>
        /// Represents the plane tracking capability provided by `XR_EXT_spatial_plane_tracking`.
        /// Equivalent to the OpenXR value `XR_SPATIAL_CAPABILITY_PLANE_TRACKING_EXT`.
        /// </summary>
        PlaneTracking = 1000741000,

        /// <summary>
        /// Represents the QR Code tracking capability provided by `XR_EXT_spatial_marker_tracking`.
        /// Equivalent to the OpenXR value `XR_SPATIAL_CAPABILITY_MARKER_TRACKING_QR_CODE_EXT`.
        /// </summary>
        MarkerTrackingQRCode = 1000743000,

        /// <summary>
        /// Represents the Micro QR Code tracking capability provided by `XR_EXT_spatial_marker_tracking`.
        /// Equivalent to the OpenXR value `XR_SPATIAL_CAPABILITY_MARKER_TRACKING_MICRO_QR_CODE_EXT`.
        /// </summary>
        MarkerTrackingMicroQRCode = 1000743001,

        /// <summary>
        /// Represents the Aruco marker tracking capability provided by `XR_EXT_spatial_marker_tracking`.
        /// Equivalent to the OpenXR value `XR_SPATIAL_CAPABILITY_MARKER_TRACKING_ARUCO_MARKER_EXT`.
        /// </summary>
        MarkerTrackingArucoMarker = 1000743002,

        /// <summary>
        /// Represents the April Tag tracking capability provided by `XR_EXT_spatial_marker_tracking`.
        /// Equivalent to the OpenXR value `XR_SPATIAL_CAPABILITY_MARKER_TRACKING_APRIL_TAG_EXT`.
        /// </summary>
        MarkerTrackingAprilTag = 1000743003,

        /// <summary>
        /// Represents the anchor capability provided by `XR_EXT_spatial_anchor`.
        /// Equivalent to the OpenXR value `XR_SPATIAL_CAPABILITY_ANCHOR_EXT`.
        /// </summary>
        Anchor = 1000762000,
    }
}
