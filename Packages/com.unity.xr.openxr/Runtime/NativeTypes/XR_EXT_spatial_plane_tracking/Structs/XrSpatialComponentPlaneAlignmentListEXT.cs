using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Assertions;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Plane alignment component list structure, used to query component data.
    /// Provided by `XR_EXT_spatial_plane_tracking`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/> is correctly initialized
    /// > to <see cref="XrStructureType.SpatialComponentPlaneAlignmentListEXT"/>.
    /// </remarks>
    public readonly unsafe struct XrSpatialComponentPlaneAlignmentListEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.SpatialComponentPlaneAlignmentListEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        public void* next { get; }

        /// <summary>
        /// The count of elements in <see cref="planeAlignments"/>. Must be greater than `0`.
        /// </summary>
        public uint planeAlignmentCount { get; }

        /// <summary>
        /// Pointer to an array of plane alignment components. Must be non-null.
        /// </summary>
        public XrSpatialPlaneAlignmentEXT* planeAlignments { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="planeAlignmentCount">The count of elements in <paramref name="planeAlignments"/>.
        /// Must be greater than `0`.</param>
        /// <param name="planeAlignments">Pointer to an array of plane alignment components. Must be non-null.</param>
        public XrSpatialComponentPlaneAlignmentListEXT(
            void* next, uint planeAlignmentCount, XrSpatialPlaneAlignmentEXT* planeAlignments)
        {
            Assert.IsTrue(planeAlignmentCount > 0);
            Assert.IsTrue(planeAlignments != null);

            type = XrStructureType.SpatialComponentPlaneAlignmentListEXT;
            this.next = next;
            this.planeAlignmentCount = planeAlignmentCount;
            this.planeAlignments = planeAlignments;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="planeAlignmentCount">The count of elements in <paramref name="planeAlignments"/>.
        /// Must be greater than `0`.</param>
        /// <param name="planeAlignments">Pointer to an array of plane alignment components. Must be non-null.</param>
        public XrSpatialComponentPlaneAlignmentListEXT(
            uint planeAlignmentCount, XrSpatialPlaneAlignmentEXT* planeAlignments)
            : this(null, planeAlignmentCount, planeAlignments) { }

        /// <summary>
        /// Construct an instance from a native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="planeAlignments">Native array of plane alignment components. Must be non-empty.</param>
        public XrSpatialComponentPlaneAlignmentListEXT(
            void* next, NativeArray<XrSpatialPlaneAlignmentEXT> planeAlignments)
            : this(next, (uint)planeAlignments.Length, (XrSpatialPlaneAlignmentEXT*)planeAlignments.GetUnsafePtr()) { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a native array.
        /// </summary>
        /// <param name="planeAlignments">Native array of plane alignment components. Must be non-empty.</param>
        public XrSpatialComponentPlaneAlignmentListEXT(NativeArray<XrSpatialPlaneAlignmentEXT> planeAlignments)
            : this(null, (uint)planeAlignments.Length, (XrSpatialPlaneAlignmentEXT*)planeAlignments.GetUnsafePtr()) { }

        /// <summary>
        /// Construct an instance from a read-only native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="planeAlignments">Read-only native array of plane alignment components.
        /// Must be non-empty.</param>
        public XrSpatialComponentPlaneAlignmentListEXT(
            void* next, NativeArray<XrSpatialPlaneAlignmentEXT>.ReadOnly planeAlignments)
            : this(
                next,
                (uint)planeAlignments.Length,
                (XrSpatialPlaneAlignmentEXT*)planeAlignments.GetUnsafeReadOnlyPtr())
        { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a read-only native array.
        /// </summary>
        /// <param name="planeAlignments">Read-only native array of plane alignment components.
        /// Must be non-empty.</param>
        public XrSpatialComponentPlaneAlignmentListEXT(NativeArray<XrSpatialPlaneAlignmentEXT>.ReadOnly planeAlignments)
            : this(
                null,
                (uint)planeAlignments.Length,
                (XrSpatialPlaneAlignmentEXT*)planeAlignments.GetUnsafeReadOnlyPtr())
        { }
    }
}
