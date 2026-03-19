using XrSpatialContextEXT = System.UInt64;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Represents an event where the runtime recommends that you call
    /// <see cref="OpenXRNativeApi.xrCreateSpatialDiscoverySnapshotAsyncEXT"/> for the given spatial context.
    /// Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/>
    /// > is correctly initialized to <see cref="XrStructureType.EventDataSpatialDiscoveryRecommendedEXT"/>.
    ///
    /// You can avoid excessive calls to <see cref="OpenXRNativeApi.xrCreateSpatialDiscoverySnapshotAsyncEXT"/>
    /// by waiting for this event to be invoked before you create a new discovery snapshot.
    ///
    /// The OpenXR Plug-in calls `xrPollEvent` every frame during <see cref="Application.onBeforeRender"/>.
    /// To subscribe to this event type, use <see cref="OpenXRLoaderBase.TrySubscribeToEventType"/>.
    /// </remarks>
    public readonly unsafe struct XrEventDataSpatialDiscoveryRecommendedEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.EventDataSpatialDiscoveryRecommendedEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        public void* next { get; }

        /// <summary>
        /// The spatial context for which discovery is being recommended by the runtime.
        /// </summary>
        public XrSpatialContextEXT spatialContext { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="spatialContext">The spatial context.</param>
        public XrEventDataSpatialDiscoveryRecommendedEXT(void* next, XrSpatialContextEXT spatialContext)
        {
            type = XrStructureType.EventDataSpatialDiscoveryRecommendedEXT;
            this.next = next;
            this.spatialContext = spatialContext;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="spatialContext">The spatial context.</param>
        public XrEventDataSpatialDiscoveryRecommendedEXT(XrSpatialContextEXT spatialContext)
            : this(null, spatialContext) { }
    }
}
