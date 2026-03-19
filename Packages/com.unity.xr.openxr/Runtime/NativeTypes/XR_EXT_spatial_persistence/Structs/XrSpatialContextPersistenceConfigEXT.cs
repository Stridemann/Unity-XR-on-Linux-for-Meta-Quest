using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Assertions;
using XrSpatialPersistenceContextEXT = System.UInt64;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Capability configuration struct for the persistence capability.
    /// Provided by `XR_EXT_spatial_persistence`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/> is correctly initialized
    /// > to <see cref="XrStructureType.SpatialContextPersistenceConfigEXT"/>.
    /// </remarks>
    public readonly unsafe struct XrSpatialContextPersistenceConfigEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.SpatialContextPersistenceConfigEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        public void* next { get; }

        /// <summary>
        /// The count of elements in <see cref="persistenceContexts"/>. Must be greater than `0`.
        /// </summary>
        public uint persistenceContextCount { get; }

        /// <summary>
        /// Pointer to an array of persistence contexts from which the spatial context can discover persisted entities.
        /// Must be non-null.
        /// </summary>
        public XrSpatialPersistenceContextEXT* persistenceContexts { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="persistenceContextCount">The count of elements in <paramref name="persistenceContexts"/>.
        /// Must be greater than `0`.</param>
        /// <param name="persistenceContexts">Pointer to an array of persistence contexts. Must be non-null.</param>
        public XrSpatialContextPersistenceConfigEXT(
            void* next, uint persistenceContextCount, XrSpatialPersistenceContextEXT* persistenceContexts)
        {
            Assert.IsTrue(persistenceContextCount > 0);
            Assert.IsTrue(persistenceContexts != null);

            type = XrStructureType.SpatialContextPersistenceConfigEXT;
            this.next = next;
            this.persistenceContextCount = persistenceContextCount;
            this.persistenceContexts = persistenceContexts;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="persistenceContextCount">The count of elements in <paramref name="persistenceContexts"/>.
        /// Must be greater than `0`.</param>
        /// <param name="persistenceContexts">Pointer to an array of persistence contexts. Must be non-null.</param>
        public XrSpatialContextPersistenceConfigEXT(
            uint persistenceContextCount, XrSpatialPersistenceContextEXT* persistenceContexts)
            : this(null, persistenceContextCount, persistenceContexts) { }

        /// <summary>
        /// Construct an instance from a native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="persistenceContexts">Native array of persistence contexts. Must be non-empty.</param>
        public XrSpatialContextPersistenceConfigEXT(
            void* next, NativeArray<XrSpatialPersistenceContextEXT> persistenceContexts)
            : this(
                next,
                (uint)persistenceContexts.Length,
                (XrSpatialPersistenceContextEXT*)persistenceContexts.GetUnsafePtr())
        { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a native array.
        /// </summary>
        /// <param name="persistenceContexts">Native array of persistence contexts. Must be non-empty.</param>
        public XrSpatialContextPersistenceConfigEXT(
            NativeArray<XrSpatialPersistenceContextEXT> persistenceContexts)
            : this(
                null,
                (uint)persistenceContexts.Length,
                (XrSpatialPersistenceContextEXT*)persistenceContexts.GetUnsafePtr())
        { }

        /// <summary>
        /// Construct an instance from a read-only native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="persistenceContexts">Read-only native array of persistence contexts. Must be non-empty.</param>
        public XrSpatialContextPersistenceConfigEXT(
            void* next, NativeArray<XrSpatialPersistenceContextEXT>.ReadOnly persistenceContexts)
            : this(
                next,
                (uint)persistenceContexts.Length,
                (XrSpatialPersistenceContextEXT*)persistenceContexts.GetUnsafeReadOnlyPtr())
        { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a read-only native array.
        /// </summary>
        /// <param name="persistenceContexts">Read-only native array of persistence contexts. Must be non-empty.</param>
        public XrSpatialContextPersistenceConfigEXT(
            NativeArray<XrSpatialPersistenceContextEXT>.ReadOnly persistenceContexts)
            : this(
                null,
                (uint)persistenceContexts.Length,
                (XrSpatialPersistenceContextEXT*)persistenceContexts.GetUnsafeReadOnlyPtr())
        { }
    }
}
