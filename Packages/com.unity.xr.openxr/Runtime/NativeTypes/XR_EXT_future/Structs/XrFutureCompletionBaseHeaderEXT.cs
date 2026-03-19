namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Base header for future completion structs. Provided by `EXT_future`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/> is correctly initialized.
    /// </remarks>
    public readonly unsafe struct XrFutureCompletionBaseHeaderEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        public void* next { get; }

        /// <summary>
        /// The result of the async operation associated with the future passed to the completion function.
        /// </summary>
        public XrResult futureResult { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="type">The `XrStructureType`.</param>
        /// <param name="next">The next pointer.</param>
        /// <param name="futureResult">The future result.</param>
        public XrFutureCompletionBaseHeaderEXT(XrStructureType type, void* next, XrResult futureResult)
        {
            this.type = type;
            this.next = next;
            this.futureResult = futureResult;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="type">The `XrStructureType`.</param>
        /// <param name="futureResult">The future result.</param>
        public XrFutureCompletionBaseHeaderEXT(XrStructureType type, XrResult futureResult)
            : this(type, null, futureResult) { }
    }
}
