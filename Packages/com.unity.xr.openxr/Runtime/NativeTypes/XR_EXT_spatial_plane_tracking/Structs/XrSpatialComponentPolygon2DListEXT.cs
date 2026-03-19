using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Assertions;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Polygon 2D component list structure, used to query component data. Provided by `XR_EXT_spatial_plane_tracking`.
    /// </summary>
    public readonly unsafe struct XrSpatialComponentPolygon2DListEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.SpatialComponentPolygon2DListEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        public void* next { get; }

        /// <summary>
        /// The count of elements in <see cref="polygons"/>. Must be greater than `0`.
        /// </summary>
        public uint polygonCount { get; }

        /// <summary>
        /// Pointer to an array of polygon 2D components. Must be non-null.
        /// </summary>
        public XrSpatialPolygon2DDataEXT* polygons { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="polygonCount">The count of elements in <paramref name="polygons"/>.
        /// Must be greater than `0`.</param>
        /// <param name="polygons">Pointer to an array of polygon 2D components. Must be non-null.</param>
        public XrSpatialComponentPolygon2DListEXT(void* next, uint polygonCount, XrSpatialPolygon2DDataEXT* polygons)
        {
            Assert.IsTrue(polygonCount > 0);
            Assert.IsTrue(polygons != null);

            type = XrStructureType.SpatialComponentPolygon2DListEXT;
            this.next = next;
            this.polygonCount = polygonCount;
            this.polygons = polygons;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="polygonCount">The count of elements in <paramref name="polygons"/>.
        /// Must be greater than `0`.</param>
        /// <param name="polygons">Pointer to an array of polygon 2D components. Must be non-null.</param>
        public XrSpatialComponentPolygon2DListEXT(uint polygonCount, XrSpatialPolygon2DDataEXT* polygons)
            : this(null, polygonCount, polygons) { }

        /// <summary>
        /// Construct an instance from a native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="polygons">Native array of polygon 2D components. Must be non-null.</param>
        public XrSpatialComponentPolygon2DListEXT(void* next, NativeArray<XrSpatialPolygon2DDataEXT> polygons)
            : this(next, (uint)polygons.Length, (XrSpatialPolygon2DDataEXT*)polygons.GetUnsafePtr()) { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a native array.
        /// </summary>
        /// <param name="polygons">Native array of polygon 2D components. Must be non-null.</param>
        public XrSpatialComponentPolygon2DListEXT(NativeArray<XrSpatialPolygon2DDataEXT> polygons)
            : this(null, (uint)polygons.Length, (XrSpatialPolygon2DDataEXT*)polygons.GetUnsafePtr()) { }

        /// <summary>
        /// Construct an instance from a read-only native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="polygons">Read-only native array of polygon 2D components. Must be non-null.</param>
        public XrSpatialComponentPolygon2DListEXT(void* next, NativeArray<XrSpatialPolygon2DDataEXT>.ReadOnly polygons)
            : this(next, (uint)polygons.Length, (XrSpatialPolygon2DDataEXT*)polygons.GetUnsafeReadOnlyPtr()) { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a native array.
        /// </summary>
        /// <param name="polygons">Read-only native array of polygon 2D components. Must be non-null.</param>
        public XrSpatialComponentPolygon2DListEXT(NativeArray<XrSpatialPolygon2DDataEXT>.ReadOnly polygons)
            : this(null, (uint)polygons.Length, (XrSpatialPolygon2DDataEXT*)polygons.GetUnsafeReadOnlyPtr()) { }
    }
}
