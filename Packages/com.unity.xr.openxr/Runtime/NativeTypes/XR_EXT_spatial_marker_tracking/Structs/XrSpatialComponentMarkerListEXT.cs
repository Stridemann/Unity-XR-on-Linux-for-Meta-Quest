using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Assertions;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Marker component list structure, used to query component data. Provided by `XR_EXT_spatial_marker_tracking`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/> is correctly initialized
    /// > to <see cref="XrStructureType.SpatialComponentMarkerListEXT"/>.
    /// </remarks>
    public readonly unsafe struct XrSpatialComponentMarkerListEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.SpatialComponentMarkerListEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        public void* next { get; }

        /// <summary>
        /// The count of elements in <see cref="markers"/>. Must be greater than `0`.
        /// </summary>
        public uint markerCount { get; }

        /// <summary>
        /// Pointer to an array of marker components. Must be non-null.
        /// </summary>
        public XrSpatialMarkerDataEXT* markers { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="markerCount">The count of elements in <paramref name="markers"/>.
        /// Must be greater than `0`.</param>
        /// <param name="markers">Pointer to an array of marker components. Must be non-null.</param>
        public XrSpatialComponentMarkerListEXT(void* next, uint markerCount, XrSpatialMarkerDataEXT* markers)
        {
            Assert.IsTrue(markerCount > 0);
            Assert.IsTrue(markers != null);

            type = XrStructureType.SpatialComponentMarkerListEXT;
            this.next = next;
            this.markerCount = markerCount;
            this.markers = markers;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="markerCount">The count of elements in <paramref name="markers"/>.
        /// Must be greater than `0`.</param>
        /// <param name="markers">Pointer to an array of marker components. Must be non-null.</param>
        public XrSpatialComponentMarkerListEXT(uint markerCount, XrSpatialMarkerDataEXT* markers)
            : this(null, markerCount, markers) { }

        /// <summary>
        /// Construct an instance from a native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="markers">Native array of marker components. Must be non-empty.</param>
        public XrSpatialComponentMarkerListEXT(void* next, NativeArray<XrSpatialMarkerDataEXT> markers)
            : this(next, (uint)markers.Length, (XrSpatialMarkerDataEXT*)markers.GetUnsafePtr()) { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a native array.
        /// </summary>
        /// <param name="markers">Native array of marker components. Must be non-empty.</param>
        public XrSpatialComponentMarkerListEXT(NativeArray<XrSpatialMarkerDataEXT> markers)
            : this(null, (uint)markers.Length, (XrSpatialMarkerDataEXT*)markers.GetUnsafePtr()) { }

        /// <summary>
        /// Construct an instance from a read-only native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="markers">Read-only native array of marker components. Must be non-empty.</param>
        public XrSpatialComponentMarkerListEXT(void* next, NativeArray<XrSpatialMarkerDataEXT>.ReadOnly markers)
            : this(next, (uint)markers.Length, (XrSpatialMarkerDataEXT*)markers.GetUnsafeReadOnlyPtr()) { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a read-only native array.
        /// </summary>
        /// <param name="markers">Read-only native array of marker components. Must be non-empty.</param>
        public XrSpatialComponentMarkerListEXT(NativeArray<XrSpatialMarkerDataEXT>.ReadOnly markers)
            : this(null, (uint)markers.Length, (XrSpatialMarkerDataEXT*)markers.GetUnsafeReadOnlyPtr()) { }
    }
}
