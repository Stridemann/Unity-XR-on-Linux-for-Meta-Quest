using XrFutureEXT = System.UInt64;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// The cancel info struct used by `OpenXRNativeApi.xrCancelFutureEXT`. Provided by `XR_EXT_future`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/> is correctly initialized
    /// > to <see cref="XrStructureType.FutureCancelInfoEXT"/>.
    /// </remarks>
    /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrCancelFutureEXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrFutureCancelInfoEXT@)"/>
    /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrCancelFutureEXT(UnityEngine.XR.OpenXR.NativeTypes.XrFutureCancelInfoEXT@)"/>
    public readonly unsafe struct XrFutureCancelInfoEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.FutureCancelInfoEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        public void* next { get; }

        /// <summary>
        /// The future to cancel.
        /// </summary>
        public XrFutureEXT future { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="future">The future to cancel.</param>
        public XrFutureCancelInfoEXT(void* next, XrFutureEXT future)
        {
            type = XrStructureType.FutureCancelInfoEXT;
            this.next = next;
            this.future = future;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="future">The future to cancel.</param>
        public XrFutureCancelInfoEXT(XrFutureEXT future) : this(null, future)
        { }
    }
}
