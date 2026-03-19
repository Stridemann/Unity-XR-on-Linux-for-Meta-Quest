using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Assertions;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Plane semantic label component list structure, used to query component data.
    /// Provided by `XR_EXT_spatial_plane_tracking`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/> is correctly initialized
    /// > to <see cref="XrStructureType.SpatialComponentPlaneSemanticLabelListEXT"/>.
    /// </remarks>
    public readonly unsafe struct XrSpatialComponentPlaneSemanticLabelListEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct:
        /// <see cref="XrStructureType.SpatialComponentPlaneSemanticLabelListEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        public void* next { get; }

        /// <summary>
        /// The count of elements in <see cref="semanticLabels"/>. Must be greater than `0`.
        /// </summary>
        public uint semanticLabelCount { get; }

        /// <summary>
        /// Pointer to an array of plane semantic label components. Must be non-null.
        /// </summary>
        public XrSpatialPlaneSemanticLabelEXT* semanticLabels { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="semanticLabelCount">The count of elements in <paramref name="semanticLabels"/>.
        /// Must be greater than `0`.</param>
        /// <param name="semanticLabels">Pointer to an array of plane semantic label components.
        /// Must be non-null.</param>
        public XrSpatialComponentPlaneSemanticLabelListEXT(
            void* next, uint semanticLabelCount, XrSpatialPlaneSemanticLabelEXT* semanticLabels)
        {
            Assert.IsTrue(semanticLabelCount > 0);
            Assert.IsTrue(semanticLabels != null);

            type = XrStructureType.SpatialComponentPlaneSemanticLabelListEXT;
            this.next = next;
            this.semanticLabelCount = semanticLabelCount;
            this.semanticLabels = semanticLabels;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="semanticLabelCount">The count of elements in <paramref name="semanticLabels"/>.
        /// Must be greater than `0`.</param>
        /// <param name="semanticLabels">Pointer to an array of plane semantic label components.
        /// Must be non-null.</param>
        public XrSpatialComponentPlaneSemanticLabelListEXT(
            uint semanticLabelCount, XrSpatialPlaneSemanticLabelEXT* semanticLabels)
            : this(null, semanticLabelCount, semanticLabels) { }

        /// <summary>
        /// Construct an instance from a native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="semanticLabels">Native array of plane semantic label components. Must be non-empty.</param>
        public XrSpatialComponentPlaneSemanticLabelListEXT(
            void* next, NativeArray<XrSpatialPlaneSemanticLabelEXT> semanticLabels)
            : this(
                next, (uint)semanticLabels.Length, (XrSpatialPlaneSemanticLabelEXT*)semanticLabels.GetUnsafePtr())
        { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a native array.
        /// </summary>
        /// <param name="semanticLabels">Native array of plane semantic label components. Must be non-empty.</param>
        public XrSpatialComponentPlaneSemanticLabelListEXT(NativeArray<XrSpatialPlaneSemanticLabelEXT> semanticLabels)
            : this(
                null, (uint)semanticLabels.Length, (XrSpatialPlaneSemanticLabelEXT*)semanticLabels.GetUnsafePtr())
        { }

        /// <summary>
        /// Construct an instance from a read-only native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="semanticLabels">Read-only native array of plane semantic label components.
        /// Must be non-empty.</param>
        public XrSpatialComponentPlaneSemanticLabelListEXT(
            void* next, NativeArray<XrSpatialPlaneSemanticLabelEXT>.ReadOnly semanticLabels)
            : this(
                next,
                (uint)semanticLabels.Length,
                (XrSpatialPlaneSemanticLabelEXT*)semanticLabels.GetUnsafeReadOnlyPtr())
        { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a read-only native array.
        /// </summary>
        /// <param name="semanticLabels">Read-only native array of plane semantic label components.
        /// Must be non-empty.</param>
        public XrSpatialComponentPlaneSemanticLabelListEXT(
            NativeArray<XrSpatialPlaneSemanticLabelEXT>.ReadOnly semanticLabels)
            : this(
                null,
                (uint)semanticLabels.Length,
                (XrSpatialPlaneSemanticLabelEXT*)semanticLabels.GetUnsafeReadOnlyPtr())
        { }
    }
}
