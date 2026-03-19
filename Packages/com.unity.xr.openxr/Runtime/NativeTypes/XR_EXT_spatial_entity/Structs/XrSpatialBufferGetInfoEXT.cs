using XrSpatialBufferIdEXT = System.UInt64;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Represents the information necessary to get the contents of a <see cref="XrSpatialBufferEXT"/>.
    /// Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/> is correctly initialized
    /// > to <see cref="XrStructureType.SpatialBufferGetInfoEXT"/>.
    /// </remarks>
    public readonly unsafe struct XrSpatialBufferGetInfoEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.SpatialBufferGetInfoEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        public void* next { get; }

        /// <summary>
        /// The ID of the buffer whose data to retrieve.
        /// </summary>
        public XrSpatialBufferIdEXT bufferId { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="bufferId">The buffer ID.</param>
        public XrSpatialBufferGetInfoEXT(void* next, XrSpatialBufferIdEXT bufferId)
        {
            type = XrStructureType.SpatialBufferGetInfoEXT;
            this.next = next;
            this.bufferId = bufferId;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="bufferId">The buffer ID.</param>
        public XrSpatialBufferGetInfoEXT(XrSpatialBufferIdEXT bufferId)
            : this(null, bufferId) { }
    }
}
