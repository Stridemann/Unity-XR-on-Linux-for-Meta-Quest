using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Assertions;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Capability configuration struct for the QR code tracking capability.
    /// Provided by `XR_EXT_spatial_marker_tracking`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/> is correctly initialized
    /// > to <see cref="XrStructureType.SpatialCapabilityConfigurationQrCodeEXT"/>.
    /// </remarks>
    public readonly unsafe struct XrSpatialCapabilityConfigurationQrCodeEXT : ISpatialCapabilityConfiguration
    {
        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.SpatialCapabilityConfigurationQrCodeEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        /// <remarks>
        /// If the runtime supports <see cref="XrSpatialCapabilityFeatureEXT.MarkerTrackingFixedSizeMarkers"/>,
        /// you can chain an <see cref="XrSpatialMarkerSizeEXT"/> to this instance to specify the size of your markers
        /// for more accurate pose and size detection.
        ///
        /// Likewise, if the runtime supports <see cref="XrSpatialCapabilityFeatureEXT.MarkerTrackingStaticMarkers"/>,
        /// you can chain an <see cref="XrSpatialMarkerStaticOptimizationEXT"/> to specify that your markers aren't
        /// expected to move.
        /// </remarks>
        public void* next { get; }

        /// <summary>
        /// The capability being configured: <see cref="XrSpatialCapabilityEXT.MarkerTrackingQRCode"/>.
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
        public XrSpatialCapabilityConfigurationQrCodeEXT(
            void* next, uint enabledComponentCount, XrSpatialComponentTypeEXT* enabledComponents)
        {
            Assert.IsTrue(enabledComponentCount > 0);
            Assert.IsTrue(enabledComponents != null);

            type = XrStructureType.SpatialCapabilityConfigurationQrCodeEXT;
            this.next = next;
            capability = XrSpatialCapabilityEXT.MarkerTrackingQRCode;
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
        public XrSpatialCapabilityConfigurationQrCodeEXT(
            uint enabledComponentCount, XrSpatialComponentTypeEXT* enabledComponents)
            : this(null, enabledComponentCount, enabledComponents) { }

        /// <summary>
        /// Construct an instance from a native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="enabledComponents">Native array of component types to enable for this capability.
        /// Must be non-null.</param>
        public XrSpatialCapabilityConfigurationQrCodeEXT(
            void* next, NativeArray<XrSpatialComponentTypeEXT> enabledComponents)
            : this(next, (uint)enabledComponents.Length, (XrSpatialComponentTypeEXT*)enabledComponents.GetUnsafePtr())
        { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a native array.
        /// </summary>
        /// <param name="enabledComponents">Native array of component types to enable for this capability.
        /// Must be non-null.</param>
        public XrSpatialCapabilityConfigurationQrCodeEXT(NativeArray<XrSpatialComponentTypeEXT> enabledComponents)
            : this(null, (uint)enabledComponents.Length, (XrSpatialComponentTypeEXT*)enabledComponents.GetUnsafePtr())
        { }

        /// <summary>
        /// Construct an instance from a read-only native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="enabledComponents">Read-only native array of component types to enable for this capability.
        /// Must be non-null.</param>
        public XrSpatialCapabilityConfigurationQrCodeEXT(
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
        /// Must be non-null.</param>
        public XrSpatialCapabilityConfigurationQrCodeEXT(
            NativeArray<XrSpatialComponentTypeEXT>.ReadOnly enabledComponents)
            : this(
                null,
                (uint)enabledComponents.Length,
                (XrSpatialComponentTypeEXT*)enabledComponents.GetUnsafeReadOnlyPtr())
        { }
    }
}
