namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// The unpersist info struct used by <see cref="OpenXRNativeApi.xrUnpersistSpatialEntityAsyncEXT"/>.
    /// Provided by `XR_EXT_spatial_persistence_operations`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/> is correctly initialized
    /// > to <see cref="XrStructureType.SpatialEntityUnpersistInfoEXT"/>.
    /// </remarks>
    public readonly unsafe struct XrSpatialEntityUnpersistInfoEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.SpatialEntityUnpersistInfoEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// No such structures are defined in core OpenXR or `XR_EXT_spatial_persistence_operations`.
        /// </summary>
        public void* next { get; }

        /// <summary>
        /// The persistant UUID of the spatial entity to be unpersisted.
        /// </summary>
        public XrUuid persistUuid { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="persistUuid">The persistant UUID of the spatial entity to be unpersisted.</param>
        public XrSpatialEntityUnpersistInfoEXT(void* next, XrUuid persistUuid)
        {
            type = XrStructureType.SpatialEntityUnpersistInfoEXT;
            this.next = next;
            this.persistUuid = persistUuid;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="persistUuid">The persistant UUID of the spatial entity to be unpersisted.</param>
        public XrSpatialEntityUnpersistInfoEXT(XrUuid persistUuid)
            : this(null, persistUuid) { }
    }
}
