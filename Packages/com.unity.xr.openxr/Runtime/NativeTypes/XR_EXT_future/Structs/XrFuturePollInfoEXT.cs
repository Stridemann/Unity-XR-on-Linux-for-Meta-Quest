using XrFutureEXT = System.UInt64;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// The poll info struct used by `OpenXRNativeApi.xrPollFutureEXT`. Provided by `EXT_future`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/> is correctly initialized
    /// > to <see cref="XrStructureType.FuturePollInfoEXT"/>.
    /// </remarks>
    /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrPollFutureEXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrFuturePollInfoEXT@,UnityEngine.XR.OpenXR.NativeTypes.XrFuturePollResultEXT@)"/>
    /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrPollFutureEXT(UnityEngine.XR.OpenXR.NativeTypes.XrFuturePollInfoEXT@,UnityEngine.XR.OpenXR.NativeTypes.XrFuturePollResultEXT@)"/>
    public readonly unsafe struct XrFuturePollInfoEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.FuturePollInfoEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        public void* next { get; }

        /// <summary>
        /// The future being polled.
        /// </summary>
        public XrFutureEXT future { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="future">The future being polled.</param>
        public XrFuturePollInfoEXT(void* next, XrFutureEXT future)
        {
            type = XrStructureType.FuturePollInfoEXT;
            this.next = next;
            this.future = future;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="future">The future being polled.</param>
        public XrFuturePollInfoEXT(XrFutureEXT future) : this(null, future)
        { }
    }
}
