using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Represents a spatial component data query condition,
    /// used by `OpenXRNativeApi.xrQuerySpatialComponentDataEXT`. Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/> is correctly initialized
    /// > to <see cref="XrStructureType.SpatialComponentDataQueryConditionEXT"/>.
    /// </remarks>
    /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrQuerySpatialComponentDataEXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialComponentDataQueryConditionEXT@,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialComponentDataQueryResultEXT@)"/>
    /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrQuerySpatialComponentDataEXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialComponentDataQueryConditionEXT@,Unity.Collections.Allocator,Unity.Collections.NativeArray{System.UInt64}@,Unity.Collections.NativeArray{UnityEngine.XR.OpenXR.NativeTypes.XrSpatialEntityTrackingStateEXT}@)"/>
    public readonly unsafe struct XrSpatialComponentDataQueryConditionEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.SpatialComponentDataQueryConditionEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        /// <seealso cref="XrSpatialDiscoveryPersistenceUuidFilterEXT"/>
        /// <seealso cref="XrSpatialFilterTrackingStateEXT"/>
        public void* next { get; }

        /// <summary>
        /// The count of elements in <see cref="componentTypes"/>.
        /// </summary>
        public uint componentTypeCount { get; }

        /// <summary>
        /// Pointer to an array of component types for which to get the data from the snapshot.
        /// Can be `null` if <see cref="componentTypeCount"/> is `0`.
        /// </summary>
        public XrSpatialComponentTypeEXT* componentTypes { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="componentTypeCount">Count of elements in <paramref name="componentTypes"/>.</param>
        /// <param name="componentTypes">Pointer to an array of component types, if <see cref="componentTypeCount"/> is
        /// greater than `0`.</param>
        public XrSpatialComponentDataQueryConditionEXT(
            void* next, uint componentTypeCount, XrSpatialComponentTypeEXT* componentTypes)
        {
            type = XrStructureType.SpatialComponentDataQueryConditionEXT;
            this.next = next;
            this.componentTypeCount = componentTypeCount;
            this.componentTypes = componentTypes;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="componentTypeCount">Count of elements in <paramref name="componentTypes"/>.</param>
        /// <param name="componentTypes">Pointer to an array of component types, if <see cref="componentTypeCount"/> is
        /// greater than `0`.</param>
        public XrSpatialComponentDataQueryConditionEXT(
            uint componentTypeCount, XrSpatialComponentTypeEXT* componentTypes)
            : this(null, componentTypeCount, componentTypes) { }

        /// <summary>
        /// Construct an instance from a `NativeArray`.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="componentTypes">The array of component types.</param>
        public XrSpatialComponentDataQueryConditionEXT(
            void* next, NativeArray<XrSpatialComponentTypeEXT> componentTypes)
            : this(next, (uint)componentTypes.Length, (XrSpatialComponentTypeEXT*)componentTypes.GetUnsafePtr()) { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a `NativeArray`.
        /// </summary>
        /// <param name="componentTypes">The array of component types.</param>
        public XrSpatialComponentDataQueryConditionEXT(NativeArray<XrSpatialComponentTypeEXT> componentTypes)
            : this(null, (uint)componentTypes.Length, (XrSpatialComponentTypeEXT*)componentTypes.GetUnsafePtr()) { }
    }
}
