namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// The completion struct for <see cref="OpenXRNativeApi.xrPersistSpatialEntityCompleteEXT"/>.
    /// Provided by `XR_EXT_spatial_persistence_operations`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use either <see cref="defaultValue"/> or a constructor with parameters to ensure that <see cref="type"/>
    /// > is correctly initialized to <see cref="XrStructureType.PersistSpatialEntityCompletionEXT"/>.
    /// </remarks>
    public unsafe struct XrPersistSpatialEntityCompletionEXT
    {
        /// <summary>
        /// Get a default instance with an initialized <see cref="type"/> property.
        /// </summary>
        public static XrPersistSpatialEntityCompletionEXT defaultValue => new(0, 0, XrUuid.empty);

        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.PersistSpatialEntityCompletionEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        public void* next { get; set; }

        /// <summary>
        /// The `XrResult` of the spatial entity persist operation.
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
        /// </list>
        /// </remarks>
        public XrResult futureResult { get; set; }

        /// <summary>
        /// The `XrSpatialPersistenceContextResultEXT` of the spatial entity persist operation, if
        /// `futureResult.IsSuccess()`.
        /// </summary>
        public XrSpatialPersistenceContextResultEXT persistResult { get; set; }

        /// <summary>
        /// The persistant UUID of the spatial entity, if both `futureResult.IsSuccess()` and
        /// `persistResult.IsSuccess()`.
        /// </summary>
        public XrUuid persistUuid { get; set; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="futureResult">The `XrResult` of the spatial entity persist operation.</param>
        /// <param name="persistResult">The `XrSpatialPersistenceContextResultEXT` of the spatial entity persist
        /// operation.</param>
        /// <param name="persistUuid">The persistant UUID of the spatial entity, if both `futureResult.IsSuccess()`
        /// and `persistResult.IsSuccess()`.</param>
        public XrPersistSpatialEntityCompletionEXT(
            void* next, XrResult futureResult, XrSpatialPersistenceContextResultEXT persistResult, XrUuid persistUuid)
        {
            type = XrStructureType.PersistSpatialEntityCompletionEXT;
            this.next = next;
            this.futureResult = futureResult;
            this.persistResult = persistResult;
            this.persistUuid = persistUuid;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="futureResult">The `XrResult` of the spatial entity persist operation.</param>
        /// <param name="persistResult">The `XrSpatialPersistenceContextResultEXT` of the spatial entity persist
        /// operation.</param>
        /// <param name="persistUuid">The persistant UUID of the spatial entity, if both `futureResult.IsSuccess()`
        /// and `persistResult.IsSuccess()`.</param>
        public XrPersistSpatialEntityCompletionEXT(
            XrResult futureResult, XrSpatialPersistenceContextResultEXT persistResult, XrUuid persistUuid)
            : this(null, futureResult, persistResult, persistUuid) { }
    }
}
