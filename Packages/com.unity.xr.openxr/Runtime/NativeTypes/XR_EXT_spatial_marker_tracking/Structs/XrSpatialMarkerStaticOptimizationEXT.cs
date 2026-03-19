using XrBool32 = System.UInt32;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Use this struct with <see cref="XrSpatialCapabilityFeatureEXT.MarkerTrackingStaticMarkers"/> to specify
    /// that the runtime should assume that all markers are static (unable to move), enabling the runtime to
    /// generate more accurate pose and size information for tracked markers in this case.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/> is correctly initialized
    /// > to <see cref="XrStructureType.SpatialMarkerStaticOptimizationEXT"/>.
    /// </remarks>
    public readonly unsafe struct XrSpatialMarkerStaticOptimizationEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.SpatialMarkerStaticOptimizationEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        public void* next { get; }

        /// <summary>
        /// Indicates whether all markers in the space aren't expected to move.
        /// </summary>
        public XrBool32 optimizeForStaticMarker { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="optimizeForStaticMarker">`true` if all markers in the space aren't expected to move.
        /// Otherwise, `false`.</param>
        public XrSpatialMarkerStaticOptimizationEXT(void* next, bool optimizeForStaticMarker)
        {
            type = XrStructureType.SpatialMarkerStaticOptimizationEXT;
            this.next = next;
            this.optimizeForStaticMarker = optimizeForStaticMarker ? 1u : 0;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="optimizeForStaticMarker">`true` if all markers in the space aren't expected to move.
        /// Otherwise, `false`.</param>
        public XrSpatialMarkerStaticOptimizationEXT(bool optimizeForStaticMarker)
            : this(null, optimizeForStaticMarker) { }
    }
}
