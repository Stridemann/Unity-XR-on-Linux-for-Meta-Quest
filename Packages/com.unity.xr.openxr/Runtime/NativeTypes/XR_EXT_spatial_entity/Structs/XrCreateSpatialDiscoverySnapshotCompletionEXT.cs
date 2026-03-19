using XrSpatialSnapshotEXT = System.UInt64;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// The completion struct for `OpenXRNativeApi.xrCreateSpatialDiscoverySnapshotCompleteEXT`.
    /// Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use either <see cref="defaultValue"/> or a constructor with parameters to ensure that <see cref="type"/>
    /// > is correctly initialized to <see cref="XrStructureType.CreateSpatialDiscoverySnapshotCompletionEXT"/>.
    /// </remarks>
    /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrCreateSpatialDiscoverySnapshotCompleteEXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrCreateSpatialDiscoverySnapshotCompletionInfoEXT@,UnityEngine.XR.OpenXR.NativeTypes.XrCreateSpatialDiscoverySnapshotCompletionEXT@)"/>
    /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrCreateSpatialDiscoverySnapshotCompleteEXT(System.UInt64,System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrCreateSpatialDiscoverySnapshotCompletionEXT@)"/>
    public unsafe struct XrCreateSpatialDiscoverySnapshotCompletionEXT
    {
        /// <summary>
        /// Get a default instance with an initialized <see cref="type"/> property.
        /// </summary>
        public static XrCreateSpatialDiscoverySnapshotCompletionEXT defaultValue => new(default, 0);

        /// <summary>
        /// The `XrStructureType` of this struct:
        /// <see cref="XrStructureType.CreateSpatialDiscoverySnapshotCompletionEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        public void* next { get; set; }

        /// <summary>
        /// The result of the spatial discovery snapshot creation operation.
        ///
        /// Success codes:
        /// * <see cref="XrResult.Success"/>
        /// * <see cref="XrResult.LossPending"/>
        ///
        /// Failure codes:
        /// * <see cref="XrResult.RuntimeFailure"/>
        /// * <see cref="XrResult.InstanceLost"/>
        /// * <see cref="XrResult.SessionLost"/>
        /// * <see cref="XrResult.OutOfMemory"/>
        /// * <see cref="XrResult.LimitReached"/>
        /// </summary>
        public XrResult futureResult { get; set; }

        /// <summary>
        /// The newly created spatial discovery snapshot, if `futureResult.IsSuccess()`.
        /// </summary>
        public XrSpatialSnapshotEXT snapshot { get; set; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="futureResult">The future result.</param>
        /// <param name="snapshot">The snapshot.</param>
        public XrCreateSpatialDiscoverySnapshotCompletionEXT(
            void* next, XrResult futureResult, XrSpatialSnapshotEXT snapshot)
        {
            type = XrStructureType.CreateSpatialDiscoverySnapshotCompletionEXT;
            this.next = next;
            this.futureResult = futureResult;
            this.snapshot = snapshot;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="futureResult">The future result.</param>
        /// <param name="snapshot">The snapshot.</param>
        public XrCreateSpatialDiscoverySnapshotCompletionEXT(XrResult futureResult, XrSpatialSnapshotEXT snapshot)
            : this(null, futureResult, snapshot) { }
    }
}
