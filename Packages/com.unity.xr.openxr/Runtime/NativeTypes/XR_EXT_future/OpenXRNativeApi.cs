using System.Runtime.InteropServices;
using XrFutureEXT = System.UInt64;
using XrInstance = System.UInt64;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// OpenXR native API.
    ///
    /// For more information, refer to [OpenXR native API](xref:openxr-native-api) in the user manual.
    /// </summary>
    public static partial class OpenXRNativeApi
    {
        /// <summary>
        /// Poll the state of a future scoped to the given OpenXR instance. Provided by `XR_EXT_future`.
        /// </summary>
        /// <param name="instance">The OpenXR instance for which the future in <paramref name="pollInfo"/> was
        /// returned.</param>
        /// <param name="pollInfo">The poll info.</param>
        /// <param name="pollResult">The output result struct.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.FutureInvalidEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > `pollResult.state` is only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        public static XrResult xrPollFutureEXT(
            XrInstance instance, in XrFuturePollInfoEXT pollInfo, out XrFuturePollResultEXT pollResult)
        {
            pollResult = XrFuturePollResultEXT.defaultValue;
            return xrPollFutureEXT_native(instance, pollInfo, ref pollResult);
        }

        [DllImport(InternalConstants.openXRLibrary, EntryPoint = "EXT_future_xrPollFutureEXT")]
        static extern XrResult xrPollFutureEXT_native(
            XrInstance instance, in XrFuturePollInfoEXT pollInfo, ref XrFuturePollResultEXT pollResult);

        /// <summary>
        /// Poll the state of a future scoped to the current OpenXR instance. Provided by `XR_EXT_future`.
        /// </summary>
        /// <param name="pollInfo">The poll info.</param>
        /// <param name="pollResult">The output result struct.</param>
        /// <returns>The result of the operation.\
        /// \
        /// `nativeStatusCode` success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        /// </list>
        /// `nativeStatusCode` failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.FutureInvalidEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > `pollResult.state` is only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        public static OpenXRResultStatus xrPollFutureEXT(
            in XrFuturePollInfoEXT pollInfo, out XrFuturePollResultEXT pollResult)
        {
            pollResult = XrFuturePollResultEXT.defaultValue;
            return xrPollFutureEXT_usingContext_native(pollInfo, ref pollResult);
        }

        [DllImport(InternalConstants.openXRLibrary, EntryPoint = "EXT_future_xrPollFutureEXT_usingContext")]
        static extern OpenXRResultStatus xrPollFutureEXT_usingContext_native(
            in XrFuturePollInfoEXT pollInfo, ref XrFuturePollResultEXT pollResult);

        /// <summary>
        /// Poll the state of a future scoped to the current OpenXR instance. Provided by `XR_EXT_future`.
        /// This convenience overload creates an `XrFuturePollInfoEXT` on your behalf using the given future.
        /// </summary>
        /// <param name="future">The future.</param>
        /// <param name="pollResult">The output result struct.</param>
        /// <returns>The result of the operation.\
        /// \
        /// `nativeStatusCode` success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        /// </list>
        /// `nativeStatusCode` failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.FutureInvalidEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > `pollResult.state` is only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        public static OpenXRResultStatus xrPollFutureEXT(XrFutureEXT future, out XrFuturePollResultEXT pollResult)
        {
            var pollInfo = new XrFuturePollInfoEXT(future);
            pollResult = XrFuturePollResultEXT.defaultValue;
            return xrPollFutureEXT_usingContext_native(pollInfo, ref pollResult);
        }

        /// <summary>
        /// Cancels the given future and signals that the async operation is not required.
        /// Provided by `XR_EXT_future`.
        /// </summary>
        /// <param name="instance">The OpenXR instance for which the future in <see cref="cancelInfo"/> was
        /// returned.</param>
        /// <param name="cancelInfo">The cancel info.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.FutureInvalidEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!WARNING]
        /// > You are responsible to synchronize this operation with any other threads you created that use this
        /// > future.
        /// After the future has been cancelled, any functions using this future must return
        /// <see cref="XrResult.FutureInvalidEXT"/>.
        /// </remarks>
        [DllImport(InternalConstants.openXRLibrary, EntryPoint = "EXT_future_xrCancelFutureEXT")]
        public static extern XrResult xrCancelFutureEXT(XrInstance instance, in XrFutureCancelInfoEXT cancelInfo);

        /// <summary>
        /// Cancels the given future for the current instance and signals that the async operation is not required.
        /// Provided by `XR_EXT_future`.
        /// </summary>
        /// <param name="cancelInfo">The cancel info.</param>
        /// <returns>The result of the operation.\
        /// \
        /// `nativeStatusCode` success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        /// </list>
        /// `nativeStatusCode` failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.FutureInvalidEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!WARNING]
        /// > You are responsible to synchronize this operation with any other threads you created that use this
        /// > future.
        ///
        /// After the future has been cancelled, any functions using this future must return
        /// <see cref="XrResult.FutureInvalidEXT"/>.
        /// </remarks>
        [DllImport(InternalConstants.openXRLibrary, EntryPoint = "EXT_future_xrCancelFutureEXT_usingContext")]
        public static extern OpenXRResultStatus xrCancelFutureEXT(in XrFutureCancelInfoEXT cancelInfo);

        /// <summary>
        /// Cancels the given future for the current instance and signals that the async operation is not required.
        /// Provided by `XR_EXT_future`. This convenience overload creates an `XrFutureCancelInfoEXT` on your
        /// behalf using the given future.
        /// </summary>
        /// <param name="future">The future.</param>
        /// <returns>The result of the operation.\
        /// \
        /// `nativeStatusCode` success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        /// </list>
        /// `nativeStatusCode` failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.FutureInvalidEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!WARNING]
        /// > You are responsible to synchronize this operation with any other threads you created that use this
        /// > future.
        ///
        /// After the future has been cancelled, any functions using this future must return
        /// <see cref="XrResult.FutureInvalidEXT"/>.
        /// </remarks>
        public static OpenXRResultStatus xrCancelFutureEXT(XrFutureEXT future)
        {
            var cancelInfo = new XrFutureCancelInfoEXT(future);
            return xrCancelFutureEXT(cancelInfo);
        }
    }
}
