using XrSpatialContextEXT = System.UInt64;
using XrSpatialEntityIdEXT = System.UInt64;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// The persist info struct used by <see cref="OpenXRNativeApi.xrPersistSpatialEntityAsyncEXT"/>.
    /// Provided by `XR_EXT_persistence_operations`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/> is correctly initialized
    /// > to <see cref="XrStructureType.SpatialEntityPersistInfoEXT"/>.
    /// </remarks>
    public readonly unsafe struct XrSpatialEntityPersistInfoEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.SpatialEntityPersistInfoEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// No such structures are defined in core OpenXR or `XR_EXT_spatial_persistence_operations`.
        /// </summary>
        public void* next { get; }

        /// <summary>
        /// The spatial context to which the entity defined by <see cref="spatialEntityId"/> belongs.
        /// </summary>
        public XrSpatialContextEXT spatialContext { get; }

        /// <summary>
        /// The ID of the spatial entity to persist.
        /// </summary>
        public XrSpatialEntityIdEXT spatialEntityId { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="spatialContext">The spatial context to which the entity defined by
        /// <paramref name="spatialEntityId"/> belongs.</param>
        /// <param name="spatialEntityId">The ID of the spatial entity to persist.</param>
        public XrSpatialEntityPersistInfoEXT(
            void* next, XrSpatialContextEXT spatialContext, XrSpatialEntityIdEXT spatialEntityId)
        {
            type = XrStructureType.SpatialEntityPersistInfoEXT;
            this.next = next;
            this.spatialContext = spatialContext;
            this.spatialEntityId = spatialEntityId;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="spatialContext">The spatial context to which the entity defined by
        /// <paramref name="spatialEntityId"/> belongs.</param>
        /// <param name="spatialEntityId">The ID of the spatial entity to persist.</param>
        public XrSpatialEntityPersistInfoEXT(XrSpatialContextEXT spatialContext, XrSpatialEntityIdEXT spatialEntityId)
            : this(null, spatialContext, spatialEntityId) { }
    }
}
