using XrSpatialEntityEXT = System.UInt64;
using XrSpace = System.UInt64;
using XrTime = System.Int64;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Creation info for spatial update snapshot, used by
    /// <see cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrCreateSpatialUpdateSnapshotEXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialUpdateSnapshotCreateInfoEXT@,System.UInt64@)"/>.
    /// Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/>
    /// > is correctly initialized to <see cref="XrStructureType.SpatialUpdateSnapshotCreateInfoEXT"/>.
    /// </remarks>
    public readonly unsafe struct XrSpatialUpdateSnapshotCreateInfoEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct:
        /// <see cref="XrStructureType.SpatialUpdateSnapshotCreateInfoEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// No such structures are defined in core OpenXR or this extension.
        /// </summary>
        public void* next { get; }

        /// <summary>
        /// The count of elements in <see cref="entities"/>.
        /// </summary>
        public uint entityCount { get; }

        /// <summary>
        /// Pointer to an array of spatial entities for which the runtime must include component data in the snapshot.
        /// </summary>
        public XrSpatialEntityEXT* entities { get; }

        /// <summary>
        /// The count of elements in <see cref="componentTypes"/>.
        /// </summary>
        public uint componentTypeCount { get; }

        /// <summary>
        /// Pointer to an array of component types for which the runtime must include data in the snapshot.
        /// </summary>
        public XrSpatialComponentTypeEXT* componentTypes { get; }

        /// <summary>
        /// The `XrSpace` relative to which all the locations of the update snapshot will be located.
        /// </summary>
        public XrSpace baseSpace { get; }

        /// <summary>
        /// The time at which all the locations of the update snapshot will be located.
        /// </summary>
        public XrTime time { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="entityCount">Count of elements in <paramref name="entities"/>.</param>
        /// <param name="entities">The entities to include in the update snapshot.</param>
        /// <param name="componentTypeCount">Count of elements in <paramref name="componentTypes"/>.</param>
        /// <param name="componentTypes">The component types to include in the update snapshot.</param>
        /// <param name="baseSpace">The base space of the update snapshot.</param>
        /// <param name="time">The time of the update snapshot.</param>
        public XrSpatialUpdateSnapshotCreateInfoEXT(
            void* next,
            uint entityCount,
            XrSpatialEntityEXT* entities,
            uint componentTypeCount,
            XrSpatialComponentTypeEXT* componentTypes,
            XrSpace baseSpace,
            XrTime time)
        {
            type = XrStructureType.SpatialUpdateSnapshotCreateInfoEXT;
            this.next = next;
            this.entityCount = entityCount;
            this.entities = entities;
            this.componentTypeCount = componentTypeCount;
            this.componentTypes = componentTypes;
            this.baseSpace = baseSpace;
            this.time = time;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="entityCount">Count of elements in <paramref name="entities"/>.</param>
        /// <param name="entities">The entities to include in the update snapshot.</param>
        /// <param name="componentTypeCount">Count of elements in <paramref name="componentTypes"/>.</param>
        /// <param name="componentTypes">The component types to include in the update snapshot.</param>
        /// <param name="baseSpace">The base space of the update snapshot.</param>
        /// <param name="time">The time of the update snapshot.</param>
        public XrSpatialUpdateSnapshotCreateInfoEXT(
            uint entityCount,
            XrSpatialEntityEXT* entities,
            uint componentTypeCount,
            XrSpatialComponentTypeEXT* componentTypes,
            XrSpace baseSpace,
            XrTime time)
            : this(null, entityCount, entities, componentTypeCount, componentTypes, baseSpace, time) { }
    }
}
