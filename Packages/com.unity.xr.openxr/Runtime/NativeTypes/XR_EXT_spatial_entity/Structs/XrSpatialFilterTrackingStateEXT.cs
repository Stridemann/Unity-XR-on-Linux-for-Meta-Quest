namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Allows you to filter the entities in a snapshot based on their tracking state.
    /// Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/>
    /// > is correctly initialized to <see cref="XrStructureType.SpatialFilterTrackingStateEXT"/>.
    ///
    /// Use this struct by chaining it
    /// to the `next` pointer of <see cref="XrSpatialDiscoverySnapshotCreateInfoEXT"/> or
    /// <see cref="XrSpatialComponentDataQueryConditionEXT"/> to scope the discovery snapshot or component data query,
    /// respectively, to only include entities whose tracking state matches <see cref="trackingState"/>.
    /// </remarks>
    public readonly unsafe struct XrSpatialFilterTrackingStateEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.SpatialFilterTrackingStateEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        public void* next { get; }

        /// <summary>
        /// The tracking state for which to apply the filter.
        /// </summary>
        public XrSpatialEntityTrackingStateEXT trackingState { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="trackingState">The tracking state.</param>
        public XrSpatialFilterTrackingStateEXT(void* next, XrSpatialEntityTrackingStateEXT trackingState)
        {
            type = XrStructureType.SpatialFilterTrackingStateEXT;
            this.next = next;
            this.trackingState = trackingState;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="trackingState">The tracking state.</param>
        public XrSpatialFilterTrackingStateEXT(XrSpatialEntityTrackingStateEXT trackingState)
            : this(null, trackingState) { }
    }
}
