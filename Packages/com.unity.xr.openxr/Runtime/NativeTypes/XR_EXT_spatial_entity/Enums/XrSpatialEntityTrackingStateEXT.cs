namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Describes the tracking state of a spatial entity. Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    public enum XrSpatialEntityTrackingStateEXT
    {
        /// <summary>
        /// The runtime has stopped tracking this entity and will never resume tracking it.
        /// Equivalent to the OpenXR value `XR_SPATIAL_ENTITY_TRACKING_STATE_STOPPED_EXT`.
        /// </summary>
        Stopped = 1,

        /// <summary>
        /// The runtime has paused tracking this entity but may resume tracking it in the future.
        /// Equivalent to the OpenXR value `XR_SPATIAL_ENTITY_TRACKING_STATE_PAUSED_EXT`.
        /// </summary>
        Paused = 2,

        /// <summary>
        /// The runtime is currently tracking this entity and its component data is valid.
        /// Equivalent to the OpenXR value `XR_SPATIAL_ENTITY_TRACKING_STATE_TRACKING_EXT`.
        /// </summary>
        Tracking = 3,
    }
}
