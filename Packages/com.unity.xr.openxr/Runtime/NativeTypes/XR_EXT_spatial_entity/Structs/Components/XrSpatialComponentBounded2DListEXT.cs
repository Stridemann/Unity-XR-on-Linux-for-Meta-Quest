using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Assertions;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Bounded 2d component list structure, used to query component data. Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/> is correctly initialized
    /// > to <see cref="XrStructureType.SpatialComponentBounded2DListEXT"/>.
    /// </remarks>
    public readonly unsafe struct XrSpatialComponentBounded2DListEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct:
        /// <see cref="XrStructureType.SpatialComponentBounded2DListEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// No such structures are defined in core OpenXR or this extension.
        /// </summary>
        public void* next { get; }

        /// <summary>
        /// The count of elements in <see cref="bounds"/>. Must be greater than `0`.
        /// </summary>
        public uint boundCount { get; }

        /// <summary>
        /// Pointer to an array of bounded 2D components. Must be non-null.
        /// </summary>
        public XrSpatialBounded2DDataEXT* bounds { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="boundCount">The count of elements in <paramref name="bounds"/>.
        /// Must be greater than `0`.</param>
        /// <param name="bounds">Pointer to an array of bounded 2D components.</param>
        public XrSpatialComponentBounded2DListEXT(void* next, uint boundCount, XrSpatialBounded2DDataEXT* bounds)
        {
            Assert.IsTrue(boundCount > 0);
            Assert.IsTrue(bounds != null);

            type = XrStructureType.SpatialComponentBounded2DListEXT;
            this.next = next;
            this.boundCount = boundCount;
            this.bounds = bounds;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="boundCount">The count of elements in <paramref name="bounds"/>.</param>
        /// <param name="bounds">Pointer to an array of bounded 2D components, or `null` if
        /// <paramref name="boundCount"/> is `0`.</param>
        public XrSpatialComponentBounded2DListEXT(uint boundCount, XrSpatialBounded2DDataEXT* bounds)
            : this(null, boundCount, bounds) { }

        /// <summary>
        /// Construct an instance from a native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="bounds">Native array of bounded 2D components. Must be non-empty.</param>
        public XrSpatialComponentBounded2DListEXT(void* next, NativeArray<XrSpatialBounded2DDataEXT> bounds)
            : this(next, (uint)bounds.Length, (XrSpatialBounded2DDataEXT*)bounds.GetUnsafePtr()) { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a native array.
        /// </summary>
        /// <param name="bounds">Native array of bounded 2D components. Must be non-empty.</param>
        public XrSpatialComponentBounded2DListEXT(NativeArray<XrSpatialBounded2DDataEXT> bounds)
            : this(null, (uint)bounds.Length, (XrSpatialBounded2DDataEXT*)bounds.GetUnsafePtr()) { }

        /// <summary>
        /// Construct an instance from a read-only native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="bounds">Read-only native array of bounded 2D components. Must be non-empty.</param>
        public XrSpatialComponentBounded2DListEXT(void* next, NativeArray<XrSpatialBounded2DDataEXT>.ReadOnly bounds)
            : this(next, (uint)bounds.Length, (XrSpatialBounded2DDataEXT*)bounds.GetUnsafeReadOnlyPtr()) { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a read-only native array.
        /// </summary>
        /// <param name="bounds">Read-only native array of bounded 2D components. Must be non-empty.</param>
        public XrSpatialComponentBounded2DListEXT(NativeArray<XrSpatialBounded2DDataEXT>.ReadOnly bounds)
            : this(null, (uint)bounds.Length, (XrSpatialBounded2DDataEXT*)bounds.GetUnsafeReadOnlyPtr()) { }
    }
}
