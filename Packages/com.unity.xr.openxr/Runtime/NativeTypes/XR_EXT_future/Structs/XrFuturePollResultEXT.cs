namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// The poll result struct used by `OpenXRNativeApi.xrPollFutureEXT`. Provided by `EXT_future`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/> is correctly initialized
    /// > to <see cref="XrStructureType.FuturePollResultEXT"/>.
    /// </remarks>
    /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrPollFutureEXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrFuturePollInfoEXT@,UnityEngine.XR.OpenXR.NativeTypes.XrFuturePollResultEXT@)"/>
    /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrPollFutureEXT(UnityEngine.XR.OpenXR.NativeTypes.XrFuturePollInfoEXT@,UnityEngine.XR.OpenXR.NativeTypes.XrFuturePollResultEXT@)"/>
    public unsafe struct XrFuturePollResultEXT
    {
        /// <summary>
        /// Get a default instance with an initialized <see cref="type"/> property.
        /// </summary>
        public static XrFuturePollResultEXT defaultValue => new(0);

        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.FuturePollResultEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        public void* next { get; set; }

        /// <summary>
        /// The state of the future passed to `OpenXRNativeApi.xrPollFutureEXT`.
        /// </summary>
        /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrPollFutureEXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrFuturePollInfoEXT@,UnityEngine.XR.OpenXR.NativeTypes.XrFuturePollResultEXT@)"/>
        /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrPollFutureEXT(UnityEngine.XR.OpenXR.NativeTypes.XrFuturePollInfoEXT@,UnityEngine.XR.OpenXR.NativeTypes.XrFuturePollResultEXT@)"/>
        public XrFutureStateEXT state { get; set; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="state">The state of the future.</param>
        public XrFuturePollResultEXT(void* next, XrFutureStateEXT state)
        {
            type = XrStructureType.FuturePollResultEXT;
            this.next = next;
            this.state = state;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="state">The state of the future.</param>
        public XrFuturePollResultEXT(XrFutureStateEXT state) : this(null, state)
        { }
    }
}
