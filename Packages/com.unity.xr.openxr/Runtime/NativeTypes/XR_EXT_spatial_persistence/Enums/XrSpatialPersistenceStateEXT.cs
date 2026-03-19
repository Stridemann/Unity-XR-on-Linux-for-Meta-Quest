namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Represents the different states of a persisted UUID. Provided by `XR_EXT_spatial_persistence`.
    /// </summary>
    public enum XrSpatialPersistenceStateEXT
    {
        /// <summary>
        /// The persisted UUID has been successfully loaded from storage.
        /// Equivalent to the OpenXR value `XR_SPATIAL_PERSISTENCE_STATE_LOADED_EXT`.
        /// </summary>
        Loaded = 1,

        /// <summary>
        /// The persisted UUID was not found in storage, either because it was removed or because it never existed.
        /// Equivalent to the OpenXR value `XR_SPATIAL_PERSISTENCE_STATE_NOT_FOUND_EXT`.
        /// </summary>
        NotFound = 2
    }
}
