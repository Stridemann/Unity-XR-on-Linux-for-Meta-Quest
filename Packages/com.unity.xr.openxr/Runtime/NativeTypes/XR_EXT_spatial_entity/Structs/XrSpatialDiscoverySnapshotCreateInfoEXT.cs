using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Creation info for a discovery snapshot, used by
    /// <see cref="OpenXRNativeApi.xrCreateSpatialDiscoverySnapshotAsyncEXT"/>. Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/> is correctly initialized
    /// > to <see cref="XrStructureType.SpatialDiscoverySnapshotCreateInfoEXT"/>.
    /// </remarks>
    public readonly unsafe struct XrSpatialDiscoverySnapshotCreateInfoEXT
    {
        /// <summary>
        /// Get a default instance with no component types specified.
        /// </summary>
        public static XrSpatialDiscoverySnapshotCreateInfoEXT defaultValue = new(0, null);

        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.SpatialDiscoverySnapshotCreateInfoEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// No such structures are defined in core OpenXR or this extension.
        /// </summary>
        // See also: XrSpatialDiscoveryPersistenceUuidFilterEXT, XrSpatialFilterTrackingStateEXT
        public void* next { get; }

        /// <summary>
        /// The count of elements in the <see cref="componentTypes"/> array.
        /// </summary>
        public uint componentTypeCount { get; }

        /// <summary>
        /// The pointer to an array of component types.
        /// </summary>
        public XrSpatialComponentTypeEXT* componentTypes { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="componentTypeCount">The count of elements in <paramref name="componentTypes"/>.</param>
        /// <param name="componentTypes">The pointer to an array of component types.</param>
        public XrSpatialDiscoverySnapshotCreateInfoEXT(
            void* next, uint componentTypeCount, XrSpatialComponentTypeEXT* componentTypes)
        {
            type = XrStructureType.SpatialDiscoverySnapshotCreateInfoEXT;
            this.next = next;
            this.componentTypeCount = componentTypeCount;
            this.componentTypes = componentTypes;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="componentTypeCount">The count of elements in <paramref name="componentTypes"/>.</param>
        /// <param name="componentTypes">The pointer to an array of component types.</param>
        public XrSpatialDiscoverySnapshotCreateInfoEXT(
            uint componentTypeCount, XrSpatialComponentTypeEXT* componentTypes)
            : this(null, componentTypeCount, componentTypes) { }

        /// <summary>
        /// Construct an instance from a native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="componentTypes">Native array of component types.</param>
        public XrSpatialDiscoverySnapshotCreateInfoEXT(
            void* next, NativeArray<XrSpatialComponentTypeEXT> componentTypes)
            : this(next, (uint)componentTypes.Length, (XrSpatialComponentTypeEXT*)componentTypes.GetUnsafePtr()) { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a native array.
        /// </summary>
        /// <param name="componentTypes">Native array of component types.</param>
        public XrSpatialDiscoverySnapshotCreateInfoEXT(NativeArray<XrSpatialComponentTypeEXT> componentTypes)
            : this(null, (uint)componentTypes.Length, (XrSpatialComponentTypeEXT*)componentTypes.GetUnsafePtr()) { }

        /// <summary>
        /// Construct an instance from a read-only native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="componentTypes">Read-only native array of component types.</param>
        public XrSpatialDiscoverySnapshotCreateInfoEXT(
            void* next, NativeArray<XrSpatialComponentTypeEXT>.ReadOnly componentTypes)
            : this(
                next,
                (uint)componentTypes.Length,
                (XrSpatialComponentTypeEXT*)componentTypes.GetUnsafeReadOnlyPtr())
        { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a read-only native array.
        /// </summary>
        /// <param name="componentTypes">Read-only native array of component types.</param>
        public XrSpatialDiscoverySnapshotCreateInfoEXT(NativeArray<XrSpatialComponentTypeEXT>.ReadOnly componentTypes)
            : this(
                null,
                (uint)componentTypes.Length,
                (XrSpatialComponentTypeEXT*)componentTypes.GetUnsafeReadOnlyPtr())
        { }
    }
}
