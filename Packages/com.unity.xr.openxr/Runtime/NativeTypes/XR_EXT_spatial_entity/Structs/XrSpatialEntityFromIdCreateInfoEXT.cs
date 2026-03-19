using XrSpatialEntityIdEXT = System.UInt64;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Creation info for a spatial entity handle from the entity's ID, used by
    /// <see cref="OpenXRNativeApi.xrCreateSpatialEntityFromIdEXT"/>. Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/> is correctly initialized
    /// > to <see cref="XrStructureType.SpatialEntityFromIdCreateInfoEXT"/>.
    /// </remarks>
    public readonly unsafe struct XrSpatialEntityFromIdCreateInfoEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.SpatialEntityFromIdCreateInfoEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// No such structures are defined in core OpenXR or `EXT_spatial_entity`.
        /// </summary>
        public void* next { get; }

        /// <summary>
        /// The ID of the entity for which to create a handle.
        /// </summary>
        public XrSpatialEntityIdEXT entityId { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="entityId">The entity ID.</param>
        public XrSpatialEntityFromIdCreateInfoEXT(void* next, XrSpatialEntityIdEXT entityId)
        {
            type = XrStructureType.SpatialEntityFromIdCreateInfoEXT;
            this.next = next;
            this.entityId = entityId;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="entityId">The entity ID.</param>
        public XrSpatialEntityFromIdCreateInfoEXT(XrSpatialEntityIdEXT entityId)
            : this(null, entityId) { }
    }
}
