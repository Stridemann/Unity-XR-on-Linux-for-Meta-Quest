using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Assertions;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Anchor component list structure, used to query component data. Provided by `XR_EXT_spatial_anchor`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/> is correctly initialized
    /// > to <see cref="XrStructureType.SpatialComponentAnchorListEXT"/>.
    /// </remarks>
    public readonly unsafe struct XrSpatialComponentAnchorListEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.SpatialComponentAnchorListEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        public void* next { get; }

        /// <summary>
        /// The count of elements in <see cref="locations"/>. Must be greater than `0`.
        /// </summary>
        public uint locationCount { get; }

        /// <summary>
        /// Pointer to an array of anchor components. Must be non-null.
        /// </summary>
        public XrPosef* locations { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="locationCount">The count of elements in <paramref name="locations"/>.
        /// Must be greater than `0`.</param>
        /// <param name="locations">Pointer to an array of anchor components. Must be non-null.</param>
        public XrSpatialComponentAnchorListEXT(void* next, uint locationCount, XrPosef* locations)
        {
            Assert.IsTrue(locationCount > 0);
            Assert.IsTrue(locations != null);

            type = XrStructureType.SpatialComponentAnchorListEXT;
            this.next = next;
            this.locationCount = locationCount;
            this.locations = locations;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="locationCount">The count of elements in <paramref name="locations"/>.
        /// Must be greater than `0`.</param>
        /// <param name="locations">Pointer to an array of anchor components. Must be non-null.</param>
        public XrSpatialComponentAnchorListEXT(uint locationCount, XrPosef* locations)
            : this(null, locationCount, locations) { }

        /// <summary>
        /// Construct an instance from a native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="locations">Native array of anchor components. Must be non-empty.</param>
        public XrSpatialComponentAnchorListEXT(void* next, NativeArray<XrPosef> locations)
            : this(next, (uint)locations.Length, (XrPosef*)locations.GetUnsafePtr()) { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a native array.
        /// </summary>
        /// <param name="locations">Native array of anchor components. Must be non-empty.</param>
        public XrSpatialComponentAnchorListEXT(NativeArray<XrPosef> locations)
            : this(null, (uint)locations.Length, (XrPosef*)locations.GetUnsafePtr()) { }

        /// <summary>
        /// Construct an instance from a read-only native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="locations">Read-only native array of anchor components. Must be non-empty.</param>
        public XrSpatialComponentAnchorListEXT(void* next, NativeArray<XrPosef>.ReadOnly locations)
            : this(next, (uint)locations.Length, (XrPosef*)locations.GetUnsafeReadOnlyPtr()) { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a read-only native array.
        /// </summary>
        /// <param name="locations">Read-only native array of anchor components. Must be non-empty.</param>
        public XrSpatialComponentAnchorListEXT(NativeArray<XrPosef>.ReadOnly locations)
            : this(null, (uint)locations.Length, (XrPosef*)locations.GetUnsafeReadOnlyPtr()) { }
    }
}
