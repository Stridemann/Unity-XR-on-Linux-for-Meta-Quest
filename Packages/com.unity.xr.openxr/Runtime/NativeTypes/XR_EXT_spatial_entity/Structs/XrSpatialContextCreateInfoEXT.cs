using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Assertions;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Represents the creation info for a spatial context. Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/> is correctly initialized
    /// > to <see cref="XrStructureType.SpatialContextCreateInfoEXT"/>.
    /// </remarks>
    public readonly unsafe struct XrSpatialContextCreateInfoEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.SpatialContextCreateInfoEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        /// <seealso cref="XrSpatialContextPersistenceConfigEXT"/>
        public void* next { get; }

        /// <summary>
        /// The count of elements in <see cref="capabilityConfigs"/>. Must be greater than `0`.
        /// </summary>
        public uint capabilityConfigCount { get; }

        /// <summary>
        /// Pointer to an array of capability configuration pointers to use for the created spatial context.
        /// Must be non-null, and each element of the array must be valid.
        /// </summary>
        /// <seealso cref="XrSpatialCapabilityConfigurationPlaneTrackingEXT"/>
        /// <seealso cref="XrSpatialCapabilityConfigurationAnchorEXT"/>
        /// <seealso cref="XrSpatialCapabilityConfigurationQrCodeEXT"/>
        /// <seealso cref="XrSpatialCapabilityConfigurationMicroQrCodeEXT"/>
        /// <seealso cref="XrSpatialCapabilityConfigurationArucoMarkerEXT"/>
        /// <seealso cref="XrSpatialCapabilityConfigurationAprilTagEXT"/>
        public XrSpatialCapabilityConfigurationBaseHeaderEXT** capabilityConfigs { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="capabilityConfigCount">The count of elements in <paramref name="capabilityConfigs"/>.</param>
        /// <param name="capabilityConfigs">Pointer to an array of capability configs.</param>
        public XrSpatialContextCreateInfoEXT(
            void* next, uint capabilityConfigCount, XrSpatialCapabilityConfigurationBaseHeaderEXT** capabilityConfigs)
        {
            Assert.IsTrue(capabilityConfigCount > 0);
            Assert.IsTrue(capabilityConfigs != null);

            type = XrStructureType.SpatialContextCreateInfoEXT;
            this.next = next;
            this.capabilityConfigCount = capabilityConfigCount;
            this.capabilityConfigs = capabilityConfigs;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="capabilityConfigCount">The count of elements in <paramref name="capabilityConfigs"/>.
        /// Must be greater than `0`.</param>
        /// <param name="capabilityConfigs">Pointer to an array of capability configs. Must be non-null.</param>
        public XrSpatialContextCreateInfoEXT(
            uint capabilityConfigCount, XrSpatialCapabilityConfigurationBaseHeaderEXT** capabilityConfigs)
            : this(null, capabilityConfigCount, capabilityConfigs) { }

        /// <summary>
        /// Construct an instance from a native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="capabilityConfigs">Native array of capability configs. Must be non-empty.</param>
        public XrSpatialContextCreateInfoEXT(void* next, NativeArray<IntPtr> capabilityConfigs)
            : this(
                next,
                (uint)capabilityConfigs.Length,
                (XrSpatialCapabilityConfigurationBaseHeaderEXT**)capabilityConfigs.GetUnsafePtr())
        { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a native array.
        /// </summary>
        /// <param name="capabilityConfigs">Native array of capability configs. Must be non-empty.</param>
        public XrSpatialContextCreateInfoEXT(NativeArray<IntPtr> capabilityConfigs)
            : this(
                null,
                (uint)capabilityConfigs.Length,
                (XrSpatialCapabilityConfigurationBaseHeaderEXT**)capabilityConfigs.GetUnsafePtr())
        { }

        /// <summary>
        /// Construct an instance from a read-only native array.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="capabilityConfigs">Read-only native array of capability configs. Must be non-empty.</param>
        public XrSpatialContextCreateInfoEXT(void* next, NativeArray<IntPtr>.ReadOnly capabilityConfigs)
            : this(
                next,
                (uint)capabilityConfigs.Length,
                (XrSpatialCapabilityConfigurationBaseHeaderEXT**)capabilityConfigs.GetUnsafeReadOnlyPtr())
        { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a read-only native array.
        /// </summary>
        /// <param name="capabilityConfigs">Read-only native array of capability configs. Must be non-empty.</param>
        public XrSpatialContextCreateInfoEXT(NativeArray<IntPtr>.ReadOnly capabilityConfigs)
            : this(
                null,
                (uint)capabilityConfigs.Length,
                (XrSpatialCapabilityConfigurationBaseHeaderEXT**)capabilityConfigs.GetUnsafeReadOnlyPtr())
        { }
    }
}
