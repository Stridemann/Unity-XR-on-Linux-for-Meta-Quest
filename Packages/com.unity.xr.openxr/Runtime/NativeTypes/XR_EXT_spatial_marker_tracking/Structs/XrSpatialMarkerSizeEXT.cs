namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Use this struct with <see cref="XrSpatialCapabilityFeatureEXT.MarkerTrackingFixedSizeMarkers"/>
    /// to specify a pre-defined size for your markers, enabling the runtime to generate more accurate pose and size
    /// information for tracked markers.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/> is correctly initialized
    /// > to <see cref="XrStructureType.SpatialMarkerSizeEXT"/>.
    /// </remarks>
    public readonly unsafe struct XrSpatialMarkerSizeEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.SpatialMarkerSizeEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        public void* next { get; }

        /// <summary>
        /// The length in meters of all sides of all markers.
        /// (All marker types provided by `XR_EXT_spatial_marker_tracking` are geometric squares.)
        /// </summary>
        public float markerSideLength { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="markerSideLength">The length in meters of all sides of all markers.</param>
        public XrSpatialMarkerSizeEXT(void* next, float markerSideLength)
        {
            type = XrStructureType.SpatialMarkerSizeEXT;
            this.next = next;
            this.markerSideLength = markerSideLength;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="markerSideLength">The length in meters of all sides of all markers.</param>
        public XrSpatialMarkerSizeEXT(float markerSideLength)
            : this(null, markerSideLength) { }
    }
}
