using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Assertions;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Base header for capability configuration structs. Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/> is correctly initialized.
    /// </remarks>
    public readonly unsafe struct XrSpatialCapabilityConfigurationBaseHeaderEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        public void* next { get; }

        /// <summary>
        /// The capability to configure.
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
        /// <param name="type">The structure type.</param>
        /// <param name="next">The next pointer.</param>
        /// <param name="capability">The capability.</param>
        /// <param name="enabledComponentCount">The enabled component count.</param>
        /// <param name="enabledComponents">Pointer to the enabled components array.</param>
        public XrSpatialCapabilityConfigurationBaseHeaderEXT(
            XrStructureType type,
            void* next,
            XrSpatialCapabilityEXT capability,
            uint enabledComponentCount,
            XrSpatialComponentTypeEXT* enabledComponents)
        {
            Assert.IsTrue(enabledComponentCount > 0);
            Assert.IsTrue(enabledComponents != null);

            this.type = type;
            this.next = next;
            this.capability = capability;
            this.enabledComponentCount = enabledComponentCount;
            this.enabledComponents = enabledComponents;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="type">The structure type.</param>
        /// <param name="capability">The capability.</param>
        /// <param name="enabledComponentCount">The enabled component count.</param>
        /// <param name="enabledComponents">Pointer to the enabled components array.</param>
        public XrSpatialCapabilityConfigurationBaseHeaderEXT(
            XrStructureType type,
            XrSpatialCapabilityEXT capability,
            uint enabledComponentCount,
            XrSpatialComponentTypeEXT* enabledComponents)
            : this(type, null, capability, enabledComponentCount, enabledComponents) { }

        /// <summary>
        /// Construct an instance from a native array.
        /// </summary>
        /// <param name="type">The structure type.</param>
        /// <param name="next">The next pointer.</param>
        /// <param name="capability">The capability.</param>
        /// <param name="enabledComponents">Native array of enabled components.</param>
        public XrSpatialCapabilityConfigurationBaseHeaderEXT(
            XrStructureType type,
            void* next,
            XrSpatialCapabilityEXT capability,
            NativeArray<XrSpatialComponentTypeEXT> enabledComponents)
            : this(
                type,
                next,
                capability,
                (uint)enabledComponents.Length,
                (XrSpatialComponentTypeEXT*)enabledComponents.GetUnsafePtr())
        { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from a native array.
        /// </summary>
        /// <param name="type">The structure type.</param>
        /// <param name="capability">The capability.</param>
        /// <param name="enabledComponents">Native array of enabled components.</param>
        public XrSpatialCapabilityConfigurationBaseHeaderEXT(
            XrStructureType type,
            XrSpatialCapabilityEXT capability,
            NativeArray<XrSpatialComponentTypeEXT> enabledComponents)
            : this(
                type,
                null,
                capability,
                (uint)enabledComponents.Length,
                (XrSpatialComponentTypeEXT*)enabledComponents.GetUnsafePtr())
        { }
    }
}
