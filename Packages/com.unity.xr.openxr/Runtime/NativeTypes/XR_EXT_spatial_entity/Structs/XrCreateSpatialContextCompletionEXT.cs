using XrSpatialContextEXT = System.UInt64;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// The completion struct for `OpenXRNativeApi.xrCreateSpatialContextCompleteEXT`.
    /// Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use either <see cref="defaultValue"/> or a constructor with parameters to ensure that <see cref="type"/>
    /// > is correctly initialized to <see cref="XrStructureType.CreateSpatialContextCompletionEXT"/>.
    /// </remarks>
    /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrCreateSpatialContextCompleteEXT(System.UInt64,System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrCreateSpatialContextCompletionEXT@)"/>
    /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrCreateSpatialContextCompleteEXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrCreateSpatialContextCompletionEXT@)"/>
    public unsafe struct XrCreateSpatialContextCompletionEXT
    {
        /// <summary>
        /// Get a default instance with an initialized <see cref="type"/> property.
        /// </summary>
        public static XrCreateSpatialContextCompletionEXT defaultValue => new(default, 0);

        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.CreateSpatialContextCompletionEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        public void* next { get; set; }

        /// <summary>
        /// The result of the spatial context creation operation.
        /// </summary>
        /// <remarks>
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
        /// </remarks>
        public XrResult futureResult { get; set; }

        /// <summary>
        /// The newly created spatial context, if `futureResult.IsSuccess()`.
        /// </summary>
        /// <remarks>
        /// If `futureResult.IsSuccess()`, the spatial context is valid within the lifecycle of the session or until
        /// you destroy it with <see cref="OpenXRNativeApi.xrDestroySpatialContextEXT"/>, whichever comes first.
        /// </remarks>
        public XrSpatialContextEXT spatialContext { get; set; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="futureResult">The future result.</param>
        /// <param name="spatialContext">The spatial context.</param>
        public XrCreateSpatialContextCompletionEXT(
            void* next, XrResult futureResult, XrSpatialContextEXT spatialContext)
        {
            type = XrStructureType.CreateSpatialContextCompletionEXT;
            this.next = next;
            this.futureResult = futureResult;
            this.spatialContext = spatialContext;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="futureResult">The future result.</param>
        /// <param name="spatialContext">The spatial context.</param>
        public XrCreateSpatialContextCompletionEXT(
            XrResult futureResult, XrSpatialContextEXT spatialContext)
            : this(null, futureResult, spatialContext) { }
    }
}
