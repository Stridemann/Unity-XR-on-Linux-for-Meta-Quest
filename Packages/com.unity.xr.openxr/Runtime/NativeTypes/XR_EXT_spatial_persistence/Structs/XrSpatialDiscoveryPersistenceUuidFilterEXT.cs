using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Assertions;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Use this struct in the next chain of <see cref="XrSpatialDiscoverySnapshotCreateInfoEXT"/> to scope the
    /// discovery operation to only include entities whose persisted UUIDs are in the given array.
    /// Provided by `XR_EXT_spatial_persistence`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/> is correctly initialized
    /// > to <see cref="XrStructureType.SpatialDiscoveryPersistenceUuidFilterEXT"/>.
    /// </remarks>
    public readonly unsafe struct XrSpatialDiscoveryPersistenceUuidFilterEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct:
        /// <see cref="XrStructureType.SpatialDiscoveryPersistenceUuidFilterEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// No such structures are defined in core OpenXR or `XR_EXT_spatial_persistence`.
        /// </summary>
        public void* next { get; }

        /// <summary>
        /// The count of elements in <see cref="persistedUuids"/>. Must be greater than `0`.
        /// </summary>
        public uint persistedUuidCount { get; }

        /// <summary>
        /// Pointer to an array of persisted UUIDs. Must be non-null.
        /// </summary>
        public XrUuid* persistedUuids { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="persistedUuidCount">The count of elements in <paramref name="persistedUuids"/>.</param>
        /// <param name="persistedUuids">Pointer to an array of persisted UUIDs.</param>
        public XrSpatialDiscoveryPersistenceUuidFilterEXT(void* next, uint persistedUuidCount, XrUuid* persistedUuids)
        {
            Assert.IsTrue(persistedUuidCount > 0);
            Assert.IsTrue(persistedUuids != null);

            type = XrStructureType.SpatialDiscoveryPersistenceUuidFilterEXT;
            this.next = next;
            this.persistedUuidCount = persistedUuidCount;
            this.persistedUuids = persistedUuids;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="persistedUuidCount">The count of elements in <paramref name="persistedUuids"/>.</param>
        /// <param name="persistedUuids">Pointer to an array of persisted UUIDs.</param>
        public XrSpatialDiscoveryPersistenceUuidFilterEXT(uint persistedUuidCount, XrUuid* persistedUuids)
            : this(null, persistedUuidCount, persistedUuids) { }

        /// <summary>
        /// Construct an instance from a native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="persistedUuids">Native array of persisted UUIDs.</param>
        public XrSpatialDiscoveryPersistenceUuidFilterEXT(void* next, NativeArray<XrUuid> persistedUuids)
            : this(next, (uint)persistedUuids.Length, (XrUuid*)persistedUuids.GetUnsafePtr()) { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a native array.
        /// </summary>
        /// <param name="persistedUuids">Native array of persisted UUIDs.</param>
        public XrSpatialDiscoveryPersistenceUuidFilterEXT(NativeArray<XrUuid> persistedUuids)
            : this((uint)persistedUuids.Length, (XrUuid*)persistedUuids.GetUnsafePtr()) { }

        /// <summary>
        /// Construct an instance from a read-only native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="persistedUuids">Read-only native array of persisted UUIDs.</param>
        public XrSpatialDiscoveryPersistenceUuidFilterEXT(void* next, NativeArray<XrUuid>.ReadOnly persistedUuids)
            : this(next, (uint)persistedUuids.Length, (XrUuid*)persistedUuids.GetUnsafeReadOnlyPtr()) { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a native array.
        /// </summary>
        /// <param name="persistedUuids">Read-only native array of persisted UUIDs.</param>
        public XrSpatialDiscoveryPersistenceUuidFilterEXT(NativeArray<XrUuid>.ReadOnly persistedUuids)
            : this((uint)persistedUuids.Length, (XrUuid*)persistedUuids.GetUnsafeReadOnlyPtr()) { }
    }
}
