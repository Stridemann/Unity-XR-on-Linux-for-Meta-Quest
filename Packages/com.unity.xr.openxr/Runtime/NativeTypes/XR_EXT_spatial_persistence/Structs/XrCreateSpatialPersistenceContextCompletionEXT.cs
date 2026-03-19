using XrSpatialPersistenceContextEXT = System.UInt64;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// The completion struct for `OpenXRNativeApi.xrCreateSpatialPersistenceContextCompleteEXT`.
    /// Provided by `XR_EXT_spatial_persistence`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use either <see cref="defaultValue"/> or a constructor with parameters to ensure that <see cref="type"/>
    /// > is correctly initialized to <see cref="XrStructureType.CreateSpatialPersistenceContextCompletionEXT"/>.
    /// </remarks>
    /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrCreateSpatialPersistenceContextCompleteEXT(System.UInt64,System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrCreateSpatialPersistenceContextCompletionEXT@)"/>
    /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrCreateSpatialPersistenceContextCompleteEXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrCreateSpatialPersistenceContextCompletionEXT@)"/>
    public unsafe struct XrCreateSpatialPersistenceContextCompletionEXT
    {
        /// <summary>
        /// Get a default instance with an initialized <see cref="type"/> property.
        /// </summary>
        public static XrCreateSpatialPersistenceContextCompletionEXT defaultValue => new(0, 0, 0);

        /// <summary>
        /// The `XrStructureType` of this struct:
        /// <see cref="XrStructureType.CreateSpatialPersistenceContextCompletionEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        public void* next { get; set; }

        /// <summary>
        /// The `XrResult` of the spatial persistence context creation operation.
        /// </summary>
        /// <remarks>
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.LimitReached"/></description></item>
        ///   <item><description><see cref="XrResult.PermissionInsufficient"/></description></item>
        /// </list>
        /// </remarks>
        public XrResult futureResult { get; set; }

        /// <summary>
        /// The `XrSpatialPersistenceContextResultEXT` of the spatial persistence context creation operation,
        /// if `futureResult.IsSuccess()`.
        /// </summary>
        public XrSpatialPersistenceContextResultEXT createResult { get; set; }

        /// <summary>
        /// The created persistence context handle, if both `futureResult.IsSuccess()` and `createResult.IsSuccess()`.
        /// </summary>
        public XrSpatialPersistenceContextEXT persistenceContext { get; set; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="futureResult">The `XrResult` of the spatial persistence context creation operation.</param>
        /// <param name="createResult">The `XrSpatialPersistenceContextResultEXT` of the spatial persistence context
        /// creation operation.</param>
        /// <param name="persistenceContext">The persistence context handle, if both `futureResult.IsSuccess()` and
        /// `createResult.IsSuccess()`.</param>
        public XrCreateSpatialPersistenceContextCompletionEXT(
            void* next,
            XrResult futureResult,
            XrSpatialPersistenceContextResultEXT createResult,
            XrSpatialPersistenceContextEXT persistenceContext)
        {
            type = XrStructureType.CreateSpatialPersistenceContextCompletionEXT;
            this.next = next;
            this.futureResult = futureResult;
            this.createResult = createResult;
            this.persistenceContext = persistenceContext;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="futureResult">The `XrResult` of the spatial persistence context creation operation.</param>
        /// <param name="createResult">The `XrSpatialPersistenceContextResultEXT` of the spatial persistence context
        /// creation operation.</param>
        /// <param name="persistenceContext">The persistence context handle, if both `futureResult.IsSuccess()` and
        /// `createResult.IsSuccess()`.</param>
        public XrCreateSpatialPersistenceContextCompletionEXT(
            XrResult futureResult,
            XrSpatialPersistenceContextResultEXT createResult,
            XrSpatialPersistenceContextEXT persistenceContext)
            : this(null, futureResult, createResult, persistenceContext) { }
    }
}
