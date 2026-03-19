namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// The completion struct for <see cref="OpenXRNativeApi.xrUnpersistSpatialEntityCompleteEXT"/>.
    /// Provided by `XR_EXT_spatial_persistence_operations`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use either <see cref="defaultValue"/> or a constructor with parameters to ensure that <see cref="type"/>
    /// > is correctly initialized to <see cref="XrStructureType.UnpersistSpatialEntityCompletionEXT"/>.
    /// </remarks>
    public unsafe struct XrUnpersistSpatialEntityCompletionEXT
    {
        /// <summary>
        /// Get a default instance with an initialized <see cref="type"/> property.
        /// </summary>
        public static XrUnpersistSpatialEntityCompletionEXT defaultValue => new(0, 0);

        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.UnpersistSpatialEntityCompletionEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        public void* next { get; set; }

        /// <summary>
        /// The `XrResult` of the spatial entity unpersist operation.
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
        /// The `XrSpatialPersistenceContextResultEXT` of the spatial entity unpersist operation,
        /// if `futureResult.IsSuccess()`.
        /// </summary>
        public XrSpatialPersistenceContextResultEXT unpersistResult { get; set; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="futureResult">The `XrResult` of the spatial entity unpersist operation.</param>
        /// <param name="unpersistResult">The `XrSpatialPersistenceContextResultEXT` of the spatial entity unpersist
        /// operation.</param>
        public XrUnpersistSpatialEntityCompletionEXT(
            void* next, XrResult futureResult, XrSpatialPersistenceContextResultEXT unpersistResult)
        {
            type = XrStructureType.UnpersistSpatialEntityCompletionEXT;
            this.next = next;
            this.futureResult = futureResult;
            this.unpersistResult = unpersistResult;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="futureResult">The `XrResult` of the spatial entity unpersist operation.</param>
        /// <param name="unpersistResult">The `XrSpatialPersistenceContextResultEXT` of the spatial entity unpersist
        /// operation.</param>
        public XrUnpersistSpatialEntityCompletionEXT(
            XrResult futureResult, XrSpatialPersistenceContextResultEXT unpersistResult)
            : this(null, futureResult, unpersistResult) { }
    }
}
