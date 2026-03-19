namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// A C# interface used to enforce conformance of derived structs from
    /// <see cref="XrSpatialCapabilityConfigurationBaseHeaderEXT"/>.
    /// </summary>
    public unsafe interface ISpatialCapabilityConfiguration
    {
        /// <summary>
        /// The `XrStructureType` of the struct.
        /// </summary>
        XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        void* next { get; }

        /// <summary>
        /// The capability to configure.
        /// </summary>
        XrSpatialCapabilityEXT capability { get; }

        /// <summary>
        /// The count of elements in <see cref="enabledComponents"/>. Must be greater than `0`.
        /// </summary>
        uint enabledComponentCount { get; }

        /// <summary>
        /// Pointer to an array of component types to enable for this capability. Must be non-null.
        /// </summary>
        XrSpatialComponentTypeEXT* enabledComponents { get; }
    }
}
