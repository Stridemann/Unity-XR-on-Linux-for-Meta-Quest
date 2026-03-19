using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using XrFutureEXT = System.UInt64;
using XrInstance = System.UInt64;
using XrSession = System.UInt64;
using XrSpatialPersistenceContextEXT = System.UInt64;
using XrSystemId = System.UInt64;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    public static partial class OpenXRNativeApi
    {
        /// <summary>
        /// Creates persistence context scoped to the given session.
        /// </summary>
        /// <param name="session">The session in which the persistence context will be active.</param>
        /// <param name="createInfo">The creation info.</param>
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
        ///   <item><description><see cref="XrResult.SpatialPersistenceScopeUnsupportedEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        [DllImport(
            InternalConstants.openXRLibrary,
            EntryPoint = "EXT_spatial_persistence_xrCreateSpatialPersistenceContextAsyncEXT")]
        public static extern XrResult xrCreateSpatialPersistenceContextAsyncEXT(
            XrSession session, in XrSpatialPersistenceContextCreateInfoEXT createInfo, out XrFutureEXT future);

        /// <summary>
        /// Creates a persistence context scoped to the current session.
        /// </summary>
        /// <param name="createInfo">The creation info.</param>
        /// <param name="future">The output future.</param>
        /// <returns>The result of the operation.\
        /// \
        /// `nativeStatusCode` success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// `nativeStatusCode` failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.LimitReached"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialPersistenceScopeUnsupportedEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        [DllImport(
            InternalConstants.openXRLibrary,
            EntryPoint = "EXT_spatial_persistence_xrCreateSpatialPersistenceContextAsyncEXT_usingContext")]
        public static extern OpenXRResultStatus xrCreateSpatialPersistenceContextAsyncEXT(
            in XrSpatialPersistenceContextCreateInfoEXT createInfo, out XrFutureEXT future);

        /// <summary>
        /// Enumerates the types of persistence scopes that are supported by the given OpenXR instance and system ID.
        /// </summary>
        /// <param name="instance">The OpenXR instance.</param>
        /// <param name="systemId">The ID of the system whose persistence scopes will be enumerated.</param>
        /// <param name="persistenceScopeCapacityInput">The capacity of <paramref name="persistenceScopes"/>,
        /// or `0` to indicate a request to retrieve the required capacity.</param>
        /// <param name="persistenceScopeCountOutput">The number of persistence scopes, or the required capacity if
        /// <paramref name="persistenceScopeCapacityInput"/> is insufficient.</param>
        /// <param name="persistenceScopes">Pointer to an array of persistence scopes. Can be `null` if
        /// <paramref name="persistenceScopeCapacityInput"/> is `0`.</param>
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
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SystemInvalid"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        [DllImport(
            InternalConstants.openXRLibrary,
            EntryPoint = "EXT_spatial_persistence_xrEnumerateSpatialPersistenceScopesEXT")]
        public static extern unsafe XrResult xrEnumerateSpatialPersistenceScopesEXT(
            XrInstance instance,
            XrSystemId systemId,
            uint persistenceScopeCapacityInput,
            out uint persistenceScopeCountOutput,
            XrSpatialPersistenceScopeEXT* persistenceScopes);

        /// <summary>
        /// Enumerates the types of persistence scopes that are supported by the given OpenXR instance and system ID.
        /// </summary>
        /// <param name="instance">The OpenXR instance.</param>
        /// <param name="systemId">The ID of the system whose persistence scopes will be enumerated.</param>
        /// <param name="allocator">The allocation strategy to use for <paramref name="persistenceScopes"/>.</param>
        /// <param name="persistenceScopes">Output native array of persistence scopes.</param>
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
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SystemInvalid"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        ///
        /// You are responsible to `Dispose` the output native array if you pass `Allocator.Persistent` as the
        /// <paramref name="allocator"/> value.
        /// </remarks>
        public static unsafe XrResult xrEnumerateSpatialPersistenceScopesEXT(
            XrInstance instance,
            XrSystemId systemId,
            Allocator allocator,
            out NativeArray<XrSpatialPersistenceScopeEXT> persistenceScopes)
        {
            var result = xrEnumerateSpatialPersistenceScopesEXT(instance, systemId, 0, out var scopeCountOutput, null);
            if (result.IsError())
            {
                persistenceScopes = default;
                return result;
            }

            persistenceScopes = new NativeArray<XrSpatialPersistenceScopeEXT>(
                checked((int)scopeCountOutput), allocator);

            return xrEnumerateSpatialPersistenceScopesEXT(
                instance,
                systemId,
                scopeCountOutput,
                out _,
                (XrSpatialPersistenceScopeEXT*)persistenceScopes.GetUnsafePtr());
        }

        /// <summary>
        /// Enumerates the types of persistence scopes that are supported by the current OpenXR instance and system ID.
        /// </summary>
        /// <param name="persistenceScopeCapacityInput">The capacity of <paramref name="persistenceScopes"/>,
        /// or `0` to indicate a request to retrieve the required capacity.</param>
        /// <param name="persistenceScopeCountOutput">The number of persistence scopes, or the required capacity if
        /// <paramref name="persistenceScopeCapacityInput"/> is insufficient.</param>
        /// <param name="persistenceScopes">Pointer to an array of persistence scopes. Can be `null` if
        /// <paramref name="persistenceScopeCapacityInput"/> is `0`.</param>
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
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SystemInvalid"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        [DllImport(
            InternalConstants.openXRLibrary,
            EntryPoint = "EXT_spatial_persistence_xrEnumerateSpatialPersistenceScopesEXT_usingContext")]
        public static extern unsafe OpenXRResultStatus xrEnumerateSpatialPersistenceScopesEXT(
            uint persistenceScopeCapacityInput,
            out uint persistenceScopeCountOutput,
            XrSpatialPersistenceScopeEXT* persistenceScopes);

        /// <summary>
        /// Enumerates the types of persistence scopes that are supported by the current OpenXR instance and system ID.
        /// </summary>
        /// <param name="allocator">The allocation strategy to use for <paramref name="persistenceScopes"/>.</param>
        /// <param name="persistenceScopes">Output native array of persistence scopes.</param>
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
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SystemInvalid"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        ///
        /// You are responsible to `Dispose` the output native array if you pass `Allocator.Persistent` as the
        /// <paramref name="allocator"/> value.
        /// </remarks>
        public static unsafe OpenXRResultStatus xrEnumerateSpatialPersistenceScopesEXT(
            Allocator allocator, out NativeArray<XrSpatialPersistenceScopeEXT> persistenceScopes)
        {
            var result = xrEnumerateSpatialPersistenceScopesEXT(0, out var scopeCountOutput, null);
            if (result.IsError())
            {
                persistenceScopes = default;
                return result;
            }

            persistenceScopes = new NativeArray<XrSpatialPersistenceScopeEXT>(
                checked((int)scopeCountOutput), allocator);

            return xrEnumerateSpatialPersistenceScopesEXT(
                scopeCountOutput, out _, (XrSpatialPersistenceScopeEXT*)persistenceScopes.GetUnsafePtr());
        }

        /// <summary>
        /// Completes the asynchronous operation started by
        /// <see cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrCreateSpatialPersistenceContextAsyncEXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialPersistenceContextCreateInfoEXT@,System.UInt64@)"/>.
        /// Provided by `XR_EXT_spatial_persistence`.
        /// </summary>
        /// <param name="session">The session previously passed to `xrCreateSpatialPersistenceContextAsyncEXT`.</param>
        /// <param name="future">The future previously received from `xrCreateSpatialPersistenceContextAsyncEXT`.</param>
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
        /// > <paramref name="completion"/> is only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        public static XrResult xrCreateSpatialPersistenceContextCompleteEXT(
            XrSession session,
            XrFutureEXT future,
            out XrCreateSpatialPersistenceContextCompletionEXT completion)
        {
            completion = XrCreateSpatialPersistenceContextCompletionEXT.defaultValue;
            return xrCreateSpatialPersistenceContextCompleteEXT_native(session, future, ref completion);
        }

        [DllImport(
            InternalConstants.openXRLibrary,
            EntryPoint = "EXT_spatial_persistence_xrCreateSpatialPersistenceContextCompleteEXT")]
        static extern XrResult xrCreateSpatialPersistenceContextCompleteEXT_native(
            XrSession session,
            XrFutureEXT future,
            ref XrCreateSpatialPersistenceContextCompletionEXT completion);

        /// <summary>
        /// Completes the asynchronous operation started by
        /// <see cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrCreateSpatialPersistenceContextAsyncEXT(UnityEngine.XR.OpenXR.NativeTypes.XrSpatialPersistenceContextCreateInfoEXT@,System.UInt64@)"/>.
        /// Provided by `XR_EXT_spatial_persistence`.
        /// </summary>
        /// <param name="future">The future previously received from `xrCreateSpatialPersistenceContextAsyncEXT`.</param>
        /// <param name="completion">The output completion struct.</param>
        /// <returns>The result of the operation.\
        /// \
        /// `nativeStatusCode` success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// `nativeStatusCode` failure codes:
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
        /// > <paramref name="completion"/> is only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        public static OpenXRResultStatus xrCreateSpatialPersistenceContextCompleteEXT(
            XrFutureEXT future,
            out XrCreateSpatialPersistenceContextCompletionEXT completion)
        {
            completion = XrCreateSpatialPersistenceContextCompletionEXT.defaultValue;
            return xrCreateSpatialPersistenceContextCompleteEXT_usingContext(future, ref completion);
        }

        [DllImport(
            InternalConstants.openXRLibrary,
            EntryPoint = "EXT_spatial_persistence_xrCreateSpatialPersistenceContextCompleteEXT_usingContext")]
        static extern OpenXRResultStatus xrCreateSpatialPersistenceContextCompleteEXT_usingContext(
            XrFutureEXT future,
            ref XrCreateSpatialPersistenceContextCompletionEXT completion);

        /// <summary>
        /// Releases the given persistence context handle. Provided by `XR_EXT_spatial_persistence`.
        /// </summary>
        /// <param name="persistenceContext">The persistence context.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!WARNING]
        /// > You are responsible to synchronize this operation with any other threads you created that use this
        /// > persistence context.
        /// </remarks>
        [DllImport(
            InternalConstants.openXRLibrary,
            EntryPoint = "EXT_spatial_persistence_xrDestroySpatialPersistenceContextEXT")]
        public static extern XrResult xrDestroySpatialPersistenceContextEXT(
            XrSpatialPersistenceContextEXT persistenceContext);
    }
}
