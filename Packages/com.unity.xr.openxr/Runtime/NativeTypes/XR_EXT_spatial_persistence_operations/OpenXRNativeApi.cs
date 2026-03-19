using System.Runtime.InteropServices;
using XrFutureEXT = System.UInt64;
using XrSpatialPersistenceContextEXT = System.UInt64;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    public static partial class OpenXRNativeApi
    {
        /// <summary>
        /// Persists the spatial entity defined in <paramref name="persistInfo"/> to the given persistence context.
        /// Provided by `XR_EXT_spatial_persistence_operations`.
        /// </summary>
        /// <param name="persistenceContext">A persistence context previously created using
        /// `xrCreateSpatialPersistenceContextAsyncEXT`.</param>
        /// <param name="persistInfo">The persist info struct.</param>
        /// <param name="future">The output future.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.LimitReached"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialPersistenceScopeIncompatibleEXT"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialEntityIdInvalidEXT"/></description></item>
        ///   <item><description><see cref="XrResult.PermissionInsufficient"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        [DllImport(
            InternalConstants.openXRLibrary,
            EntryPoint = "EXT_spatial_persistence_operations_xrPersistSpatialEntityAsyncEXT")]
        public static extern XrResult xrPersistSpatialEntityAsyncEXT(
            XrSpatialPersistenceContextEXT persistenceContext,
            in XrSpatialEntityPersistInfoEXT persistInfo,
            out XrFutureEXT future);

        /// <summary>
        /// Completes the asynchronous operation started by <see cref="xrPersistSpatialEntityAsyncEXT"/>.
        /// Provided by `XR_EXT_spatial_persistence_operations`.
        /// </summary>
        /// <param name="persistenceContext">The persistence context previously passed to
        /// `xrPersistSpatialEntityAsyncEXT`.</param>
        /// <param name="future">The future previously received from `xrPersistSpatialEntityAsyncEXT`.</param>
        /// <param name="completion">The output completion struct.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.LimitReached"/></description></item>
        ///   <item><description><see cref="XrResult.FuturePendingEXT"/></description></item>
        ///   <item><description><see cref="XrResult.FutureInvalidEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        public static XrResult xrPersistSpatialEntityCompleteEXT(
            XrSpatialPersistenceContextEXT persistenceContext,
            XrFutureEXT future,
            out XrPersistSpatialEntityCompletionEXT completion)
        {
            completion = XrPersistSpatialEntityCompletionEXT.defaultValue;
            return xrPersistSpatialEntityCompleteEXT_native(persistenceContext, future, ref completion);
        }

        [DllImport(
            InternalConstants.openXRLibrary,
            EntryPoint = "EXT_spatial_persistence_operations_xrPersistSpatialEntityCompleteEXT")]
        static extern XrResult xrPersistSpatialEntityCompleteEXT_native(
            XrSpatialPersistenceContextEXT persistenceContext,
            XrFutureEXT future,
            ref XrPersistSpatialEntityCompletionEXT completion);

        /// <summary>
        /// Unpersists the spatial entity defined in <paramref name="unpersistInfo"/> from the given persistence
        /// context.
        /// Provided by `XR_EXT_spatial_persistence_operations`.
        /// </summary>
        /// <param name="persistenceContext">A persistence context previously created using
        /// `xrCreateSpatialPersistenceContextAsyncEXT`.</param>
        /// <param name="unpersistInfo">The unpersist info struct.</param>
        /// <param name="future">The output future.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.LimitReached"/></description></item>
        ///   <item><description><see cref="XrResult.PermissionInsufficient"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        [DllImport(
            InternalConstants.openXRLibrary,
            EntryPoint = "EXT_spatial_persistence_operations_xrUnpersistSpatialEntityAsyncEXT")]
        public static extern XrResult xrUnpersistSpatialEntityAsyncEXT(
            XrSpatialPersistenceContextEXT persistenceContext,
            in XrSpatialEntityUnpersistInfoEXT unpersistInfo,
            out XrFutureEXT future);

        /// <summary>
        /// Completes the asynchronous operation started by <see cref="xrUnpersistSpatialEntityAsyncEXT"/>.
        /// Provided by `XR_EXT_spatial_persistence_operations`.
        /// </summary>
        /// <param name="persistenceContext">The persistence context previously passed to
        /// `xrUnpersistSpatialEntityAsyncEXT`.</param>
        /// <param name="future">The future previously received from `xrUnpersistSpatialEntityAsyncEXT`.</param>
        /// <param name="completion">The output completion struct.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.FuturePendingEXT"/></description></item>
        ///   <item><description><see cref="XrResult.FutureInvalidEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        public static XrResult xrUnpersistSpatialEntityCompleteEXT(
            XrSpatialPersistenceContextEXT persistenceContext,
            XrFutureEXT future,
            out XrUnpersistSpatialEntityCompletionEXT completion)
        {
            completion = XrUnpersistSpatialEntityCompletionEXT.defaultValue;
            return xrUnpersistSpatialEntityCompleteEXT_native(persistenceContext, future, ref completion);
        }

        [DllImport(
            InternalConstants.openXRLibrary,
            EntryPoint = "EXT_spatial_persistence_operations_xrUnpersistSpatialEntityCompleteEXT")]
        static extern XrResult xrUnpersistSpatialEntityCompleteEXT_native(
            XrSpatialPersistenceContextEXT persistenceContext,
            XrFutureEXT future,
            ref XrUnpersistSpatialEntityCompletionEXT completion);
    }
}
