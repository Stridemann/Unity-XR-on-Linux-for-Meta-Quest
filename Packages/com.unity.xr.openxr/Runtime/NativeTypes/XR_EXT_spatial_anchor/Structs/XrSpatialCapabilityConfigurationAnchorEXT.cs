using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Assertions;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Capability configuration struct for the anchor capability.
    /// Provided by `XR_EXT_spatial_anchor`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/> is correctly initialized
    /// > to <see cref="XrStructureType.SpatialCapabilityConfigurationAnchorEXT"/>.
    /// </remarks>
    public readonly unsafe struct XrSpatialCapabilityConfigurationAnchorEXT : ISpatialCapabilityConfiguration
    {
        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.SpatialCapabilityConfigurationAnchorEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        public void* next { get; }

        /// <summary>
        /// The capability being configured: <see cref="XrSpatialCapabilityEXT.Anchor"/>.
        /// </summary>
        public XrSpatialCapabilityEXT capability { get; }

        /// <summary>
        /// The count of elements in <see cref="enabledComponents"/>. Must be greater than `0`.
        /// </summary>
        public uint enabledComponentCount { get; }

        /// <summary>
        /// Pointer to an array of component types to enable for this capability. Must be non-null.
        /// </summary>
        public XrSpatialComponentTypeEXT* enabledComponents { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="enabledComponentCount">The count of elements in <paramref name="enabledComponents"/>.
        /// Must be greater than `0`.</param>
        /// <param name="enabledComponents">Pointer to an array of component types to enable for this capability.
        /// Must be non-null.</param>
        public XrSpatialCapabilityConfigurationAnchorEXT(
            void* next, uint enabledComponentCount, XrSpatialComponentTypeEXT* enabledComponents)
        {
            Assert.IsTrue(enabledComponentCount > 0);
            Assert.IsTrue(enabledComponents != null);

            type = XrStructureType.SpatialCapabilityConfigurationAnchorEXT;
            this.next = next;
            capability = XrSpatialCapabilityEXT.Anchor;
            this.enabledComponentCount = enabledComponentCount;
            this.enabledComponents = enabledComponents;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="enabledComponentCount">The count of elements in <paramref name="enabledComponents"/>.
        /// Must be greater than `0`.</param>
        /// <param name="enabledComponents">Pointer to an array of component types to enable for this capability.
        /// Must be non-null.</param>
        public XrSpatialCapabilityConfigurationAnchorEXT(
            uint enabledComponentCount, XrSpatialComponentTypeEXT* enabledComponents)
            : this(null, enabledComponentCount, enabledComponents) { }

        /// <summary>
        /// Construct an instance from a native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="enabledComponents">Native array of component types to enable for this capability.
        /// Must be non-empty.</param>
        public XrSpatialCapabilityConfigurationAnchorEXT(
            void* next, NativeArray<XrSpatialComponentTypeEXT> enabledComponents)
            : this(
                next, (uint)enabledComponents.Length, (XrSpatialComponentTypeEXT*)enabledComponents.GetUnsafePtr())
        { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a native array.
        /// </summary>
        /// <param name="enabledComponents">Native array of component types to enable for this capability.
        /// Must be non-empty.</param>
        public XrSpatialCapabilityConfigurationAnchorEXT(NativeArray<XrSpatialComponentTypeEXT> enabledComponents)
            : this(
                null, (uint)enabledComponents.Length, (XrSpatialComponentTypeEXT*)enabledComponents.GetUnsafePtr())
        { }

        /// <summary>
        /// Construct an instance from a read-only native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="enabledComponents">Read-only native array of component types to enable for this capability.
        /// Must be non-empty.</param>
        public XrSpatialCapabilityConfigurationAnchorEXT(
            void* next, NativeArray<XrSpatialComponentTypeEXT>.ReadOnly enabledComponents)
            : this(
                next,
                (uint)enabledComponents.Length,
                (XrSpatialComponentTypeEXT*)enabledComponents.GetUnsafeReadOnlyPtr())
        { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a read-only native array.
        /// </summary>
        /// <param name="enabledComponents">Read-only native array of component types to enable for this capability.
        /// Must be non-empty.</param>
        public XrSpatialCapabilityConfigurationAnchorEXT(
            NativeArray<XrSpatialComponentTypeEXT>.ReadOnly enabledComponents)
            : this(
                null,
                (uint)enabledComponents.Length,
                (XrSpatialComponentTypeEXT*)enabledComponents.GetUnsafeReadOnlyPtr())
        { }
    }
}
