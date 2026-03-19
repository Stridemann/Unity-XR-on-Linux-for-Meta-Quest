using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Assertions;
using XrSpatialEntityIdEXT = System.UInt64;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Parent component list structure, used to query component data. Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/> is correctly initialized
    /// > to <see cref="XrStructureType.SpatialComponentParentListEXT"/>.
    /// </remarks>
    public readonly unsafe struct XrSpatialComponentParentListEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct:
        /// <see cref="XrStructureType.SpatialComponentParentListEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// No such structures are defined in core OpenXR or `EXT_spatial_entity`.
        /// </summary>
        public void* next { get; }

        /// <summary>
        /// The count of elements in <see cref="parents"/>. Must be greater than `0`.
        /// </summary>
        public uint parentCount { get; }

        /// <summary>
        /// Pointer to an array of parent components. Must be non-null.
        /// </summary>
        public XrSpatialEntityIdEXT* parents { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="parentCount">The count of elements in <paramref name="parents"/>.</param>
        /// <param name="parents">Pointer to an array of parent components.</param>
        public XrSpatialComponentParentListEXT(void* next, uint parentCount, XrSpatialEntityIdEXT* parents)
        {
            Assert.IsTrue(parentCount > 0);
            Assert.IsTrue(parents != null);

            type = XrStructureType.SpatialComponentParentListEXT;
            this.next = next;
            this.parentCount = parentCount;
            this.parents = parents;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="parentCount">The count of elements in <paramref name="parents"/>.</param>
        /// <param name="parents">Pointer to an array of parent components.</param>
        public XrSpatialComponentParentListEXT(uint parentCount, XrSpatialEntityIdEXT* parents)
            : this(null, parentCount, parents) { }

        /// <summary>
        /// Construct an instance from a native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="parents">Native array of parent components. Must be non-empty.</param>
        public XrSpatialComponentParentListEXT(void* next, NativeArray<XrSpatialEntityIdEXT> parents)
            : this(next, (uint)parents.Length, (XrSpatialEntityIdEXT*)parents.GetUnsafePtr()) { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a native array.
        /// </summary>
        /// <param name="parents">Native array of parent components. Must be non-empty.</param>
        public XrSpatialComponentParentListEXT(NativeArray<XrSpatialEntityIdEXT> parents)
            : this(null, (uint)parents.Length, (XrSpatialEntityIdEXT*)parents.GetUnsafePtr()) { }

        /// <summary>
        /// Construct an instance from a read-only native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="parents">Read-only native array of parent components. Must be non-empty.</param>
        public XrSpatialComponentParentListEXT(void* next, NativeArray<XrSpatialEntityIdEXT>.ReadOnly parents)
            : this(next, (uint)parents.Length, (XrSpatialEntityIdEXT*)parents.GetUnsafeReadOnlyPtr()) { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a read-only native array.
        /// </summary>
        /// <param name="parents">Read-only native array of parent components. Must be non-empty.</param>
        public XrSpatialComponentParentListEXT(NativeArray<XrSpatialEntityIdEXT>.ReadOnly parents)
            : this(null, (uint)parents.Length, (XrSpatialEntityIdEXT*)parents.GetUnsafeReadOnlyPtr()) { }
    }
}
