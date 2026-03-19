using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Assertions;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Persistence component list structure, used to query component data. Provided by `XR_EXT_spatial_persistence`.
    /// </summary>
    public readonly unsafe struct XrSpatialComponentPersistenceListEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.SpatialComponentPersistenceListEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        public void* next { get; }

        /// <summary>
        /// The count of elements in <see cref="persistData"/>. Must be greater than `0`.
        /// </summary>
        public uint persistDataCount { get; }

        /// <summary>
        /// Pointer to an array of persistence components. Must be non-null.
        /// </summary>
        public XrSpatialPersistenceDataEXT* persistData { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="persistDataCount">The count of elements in <paramref name="persistData"/>.
        /// Must be greater than `0`.</param>
        /// <param name="persistData">Pointer to an array of persistence components. Must be non-null.</param>
        public XrSpatialComponentPersistenceListEXT(
            void* next, uint persistDataCount, XrSpatialPersistenceDataEXT* persistData)
        {
            Assert.IsTrue(persistDataCount > 0);
            Assert.IsTrue(persistData != null);

            type = XrStructureType.SpatialComponentPersistenceListEXT;
            this.next = next;
            this.persistDataCount = persistDataCount;
            this.persistData = persistData;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="persistDataCount">The count of elements in <paramref name="persistData"/>.
        /// Must be greater than `0`.</param>
        /// <param name="persistData">Pointer to an array of persistence components. Must be non-null.</param>
        public XrSpatialComponentPersistenceListEXT(uint persistDataCount, XrSpatialPersistenceDataEXT* persistData)
            : this(null, persistDataCount, persistData) { }

        /// <summary>
        /// Construct an instance from a native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="persistData">Native array of persistence components. Must be non-empty.</param>
        public XrSpatialComponentPersistenceListEXT(void* next, NativeArray<XrSpatialPersistenceDataEXT> persistData)
            : this(next, (uint)persistData.Length, (XrSpatialPersistenceDataEXT*)persistData.GetUnsafePtr()) { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a native array.
        /// </summary>
        /// <param name="persistData">Native array of persistence components. Must be non-empty.</param>
        public XrSpatialComponentPersistenceListEXT(NativeArray<XrSpatialPersistenceDataEXT> persistData)
            : this(null, (uint)persistData.Length, (XrSpatialPersistenceDataEXT*)persistData.GetUnsafePtr()) { }

        /// <summary>
        /// Construct an instance from a read-only native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="persistData">Read-only native array of persistence components. Must be non-empty.</param>
        public XrSpatialComponentPersistenceListEXT(
            void* next, NativeArray<XrSpatialPersistenceDataEXT>.ReadOnly persistData)
            : this(next, (uint)persistData.Length, (XrSpatialPersistenceDataEXT*)persistData.GetUnsafeReadOnlyPtr()) { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a read-only native array.
        /// </summary>
        /// <param name="persistData">Read-only native array of persistence components. Must be non-empty.</param>
        public XrSpatialComponentPersistenceListEXT(NativeArray<XrSpatialPersistenceDataEXT>.ReadOnly persistData)
            : this(null, (uint)persistData.Length, (XrSpatialPersistenceDataEXT*)persistData.GetUnsafeReadOnlyPtr()) { }
    }
}
