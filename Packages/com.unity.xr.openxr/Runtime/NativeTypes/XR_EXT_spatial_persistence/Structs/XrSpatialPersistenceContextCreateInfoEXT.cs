namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Represents the creation info for a persistence context.
    /// Used by `OpenXRNativeApi.xrCreateSpatialPersistenceContextAsyncEXT`.
    /// Provided by `XR_EXT_spatial_persistence`.
    /// </summary>
    /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrCreateSpatialPersistenceContextAsyncEXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialPersistenceContextCreateInfoEXT@,System.UInt64@)"/>
    /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrCreateSpatialPersistenceContextAsyncEXT(UnityEngine.XR.OpenXR.NativeTypes.XrSpatialPersistenceContextCreateInfoEXT@,System.UInt64@)"/>
    public readonly unsafe struct XrSpatialPersistenceContextCreateInfoEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.SpatialPersistenceContextCreateInfoEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        public void* next { get; }

        /// <summary>
        /// The storage scope to use for the created persistence context.
        /// </summary>
        public XrSpatialPersistenceScopeEXT scope { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="scope">The storage scope to use for the created persistence context.</param>
        public XrSpatialPersistenceContextCreateInfoEXT(void* next, XrSpatialPersistenceScopeEXT scope)
        {
            type = XrStructureType.SpatialPersistenceContextCreateInfoEXT;
            this.next = next;
            this.scope = scope;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="scope">The storage scope to use for the created persistence context.</param>
        public XrSpatialPersistenceContextCreateInfoEXT(XrSpatialPersistenceScopeEXT scope)
            : this(null, scope) { }
    }
}
