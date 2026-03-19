using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Assertions;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Mesh 2D component list structure, used to query for component data. Provided by `XR_EXT_spatial_plane_tracking`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/> is correctly initialized
    /// > to <see cref="XrStructureType.SpatialComponentMesh2DListEXT"/>.
    /// </remarks>
    public readonly unsafe struct XrSpatialComponentMesh2DListEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.SpatialComponentMesh2DListEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        public void* next { get; }

        /// <summary>
        /// The count of elements in <see cref="meshes"/>. Must be greater than `0`.
        /// </summary>
        public uint meshCount { get; }

        /// <summary>
        /// Pointer to an array of mesh 2D components. Must be non-null.
        /// </summary>
        public XrSpatialMeshDataEXT* meshes { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="meshCount">The count of elements in <paramref name="meshes"/>.
        /// Must be greater than `0`.</param>
        /// <param name="meshes">Pointer to an array of mesh 2D components. Must be non-null.</param>
        public XrSpatialComponentMesh2DListEXT(void* next, uint meshCount, XrSpatialMeshDataEXT* meshes)
        {
            Assert.IsTrue(meshCount > 0);
            Assert.IsTrue(meshes != null);

            type = XrStructureType.SpatialComponentMesh2DListEXT;
            this.next = next;
            this.meshCount = meshCount;
            this.meshes = meshes;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="meshCount">The count of elements in <paramref name="meshes"/>.
        /// Must be greater than `0`.</param>
        /// <param name="meshes">Pointer to an array of mesh 2D components. Must be non-null.</param>
        public XrSpatialComponentMesh2DListEXT(uint meshCount, XrSpatialMeshDataEXT* meshes)
            : this(null, meshCount, meshes) { }

        /// <summary>
        /// Construct an instance from a native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="meshes">Native array of mesh 2D components. Must be non-empty.</param>
        public XrSpatialComponentMesh2DListEXT(void* next, NativeArray<XrSpatialMeshDataEXT> meshes)
            : this(next, (uint)meshes.Length, (XrSpatialMeshDataEXT*)meshes.GetUnsafePtr()) { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a native array.
        /// </summary>
        /// <param name="meshes">Native array of mesh 2D components. Must be non-empty.</param>
        public XrSpatialComponentMesh2DListEXT(NativeArray<XrSpatialMeshDataEXT> meshes)
            : this(null, (uint)meshes.Length, (XrSpatialMeshDataEXT*)meshes.GetUnsafePtr()) { }

        /// <summary>
        /// Construct an instance from a read-only native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="meshes">Read-only native array of mesh 2D components. Must be non-empty.</param>
        public XrSpatialComponentMesh2DListEXT(void* next, NativeArray<XrSpatialMeshDataEXT>.ReadOnly meshes)
            : this(next, (uint)meshes.Length, (XrSpatialMeshDataEXT*)meshes.GetUnsafeReadOnlyPtr()) { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a read-only native array.
        /// </summary>
        /// <param name="meshes">Read-only native array of mesh 2D components. Must be non-empty.</param>
        public XrSpatialComponentMesh2DListEXT(NativeArray<XrSpatialMeshDataEXT>.ReadOnly meshes)
            : this(null, (uint)meshes.Length, (XrSpatialMeshDataEXT*)meshes.GetUnsafeReadOnlyPtr()) { }
    }
}
