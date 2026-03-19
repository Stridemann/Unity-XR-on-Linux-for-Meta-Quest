namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// A minimal implementation of <see cref="XrFutureCompletionBaseHeaderEXT"/>, containing only the properties
    /// present in the base header structure. This struct is intended for use by asynchronous operations that do not
    /// have other outputs or return values beyond an `XrResult` value. Provided by `EXT_future`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/> is correctly initialized
    /// > to <see cref="XrStructureType.FutureCompletionEXT"/>.
    /// </remarks>
    public readonly unsafe struct XrFutureCompletionEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.FutureCompletionEXT"/>.
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
        /// <param name="next">The next pointer.</param>
        /// <param name="futureResult">The future result.</param>
        public XrFutureCompletionEXT(void* next, XrResult futureResult)
        {
            type = XrStructureType.FutureCompletionEXT;
            this.next = next;
            this.futureResult = futureResult;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="futureResult">The future result.</param>
        public XrFutureCompletionEXT(XrResult futureResult)
            : this(null, futureResult) { }
    }
}
