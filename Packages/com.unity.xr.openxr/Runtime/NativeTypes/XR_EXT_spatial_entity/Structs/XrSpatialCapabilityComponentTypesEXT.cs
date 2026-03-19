namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Used by `OpenXRNativeApi.xrEnumerateSpatialCapabilityComponentTypesEXT` to get the supported
    /// component types for a given capability. Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use either <see cref="defaultValue"/> or a constructor with parameters to ensure that <see cref="type"/>
    /// > is correctly initialized to <see cref="XrStructureType.SpatialCapabilityComponentTypesEXT"/>.
    /// </remarks>
    /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrEnumerateSpatialCapabilityComponentTypesEXT(System.UInt64,System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialCapabilityEXT,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialCapabilityComponentTypesEXT@)"/>
    /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrEnumerateSpatialCapabilityComponentTypesEXT(UnityEngine.XR.OpenXR.NativeTypes.XrSpatialCapabilityEXT,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialCapabilityComponentTypesEXT@)"/>
    public unsafe struct XrSpatialCapabilityComponentTypesEXT
    {
        /// <summary>
        /// Get a default instance with an initialized <see cref="type"/> property.
        /// </summary>
        public static XrSpatialCapabilityComponentTypesEXT defaultValue => new(0, 0, null);

        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.SpatialCapabilityComponentTypesEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// No such structures are defined in core OpenXR or this extension.
        /// </summary>
        public void* next { get; set; }

        /// <summary>
        /// The capacity of the array, or `0` to indicate a request to retrieve the required capacity.
        /// </summary>
        public uint componentTypeCapacityInput { get; set; }

        /// <summary>
        /// The number of component types, or the required capacity in the case that
        /// <see cref="componentTypeCapacityInput"/> is insufficient.
        /// </summary>
        public uint componentTypeCountOutput { get; set; }

        /// <summary>
        /// Pointer to an array of component types.
        /// Can be `null` if <see cref="componentTypeCapacityInput"/> is `0`.
        /// </summary>
        public XrSpatialComponentTypeEXT* componentTypes { get; set; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="componentTypeCapacityInput">The capacity input.</param>
        /// <param name="componentTypeCountOutput">The count output</param>
        /// <param name="componentTypes">Pointer to an array of component types.</param>
        public XrSpatialCapabilityComponentTypesEXT(
            void* next,
            uint componentTypeCapacityInput,
            uint componentTypeCountOutput,
            XrSpatialComponentTypeEXT* componentTypes)
        {
            type = XrStructureType.SpatialCapabilityComponentTypesEXT;
            this.next = next;
            this.componentTypeCapacityInput = componentTypeCapacityInput;
            this.componentTypeCountOutput = componentTypeCountOutput;
            this.componentTypes = componentTypes;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="componentTypeCapacityInput">The capacity input.</param>
        /// <param name="componentTypeCountOutput">The count output</param>
        /// <param name="componentTypes">Pointer to an array of component types.</param>
        public XrSpatialCapabilityComponentTypesEXT(
            uint componentTypeCapacityInput, uint componentTypeCountOutput, XrSpatialComponentTypeEXT* componentTypes)
            : this(null, componentTypeCapacityInput, componentTypeCountOutput, componentTypes) { }
    }
}
