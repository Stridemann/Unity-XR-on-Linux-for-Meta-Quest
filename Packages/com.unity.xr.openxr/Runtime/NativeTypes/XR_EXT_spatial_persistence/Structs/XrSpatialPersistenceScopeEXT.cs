namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Identifies the different types of persistence context scopes.
    /// Provided `XR_EXT_spatial_persistence`.
    /// </summary>
    public enum XrSpatialPersistenceScopeEXT
    {
        /// <summary>
        /// Provides read-only access to spatial entities persisted and managed by the OpenXR runtime.
        /// </summary>
        SystemManaged = 1,

        /// <summary>
        /// Provides read and write access to persisted spatial entities via `XR_EXT_spatial_persistence_operations`,
        /// limited to spatial anchors on the same device, for the same user and same app.
        /// (Added by the `XR_EXT_spatial_persistence_operations` extension)
        /// </summary>
        LocalAnchors = 1000781000
    }
}
