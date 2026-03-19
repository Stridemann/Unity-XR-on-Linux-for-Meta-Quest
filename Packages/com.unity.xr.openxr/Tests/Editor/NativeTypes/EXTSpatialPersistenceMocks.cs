using System;
using System.Runtime.InteropServices;
using UnityEngine.XR.OpenXR.NativeTypes;
using XrFutureEXT = System.UInt64;
using XrInstance = System.UInt64;
using XrSession = System.UInt64;
using XrSpatialPersistenceContextEXT = System.UInt64;
using XrSystemId = System.UInt64;

namespace UnityEditor.XR.OpenXR.Tests.NativeTypes
{
    /// <summary>
    /// Delegate signature for `xrCreateSpatialPersistenceContextAsyncEXT`.
    /// Provided by `XR_EXT_spatial_persistence`.
    /// </summary>
    /// <param name="session">The `XrSession`.</param>
    /// <param name="createInfo">The create info.</param>
    /// <param name="future">The output future.</param>
    /// <returns>The result of the operation.</returns>
    public delegate XrResult xrCreateSpatialPersistenceContextAsyncEXT_delegate(
        XrSession session, in XrSpatialPersistenceContextCreateInfoEXT createInfo, out XrFutureEXT future);

    /// <summary>
    /// Delegate signature for `xrEnumerateSpatialPersistenceScopesEXT`.
    /// Provided by `XR_EXT_spatial_persistence`.
    /// </summary>
    /// <param name="instance">The `XrInstance`.</param>
    /// <param name="systemId">The system ID.</param>
    /// <param name="persistenceScopeCapacityInput">The persistence scope capacity input.</param>
    /// <param name="persistenceScopeCountOutput">The persistence scope count output.</param>
    /// <param name="persistenceScopes">Pointer to an array of persistence scopes.</param>
    /// <returns>The result of the operation.</returns>
    public unsafe delegate XrResult xrEnumerateSpatialPersistenceScopesEXT_delegate(
        XrInstance instance,
        XrSystemId systemId,
        uint persistenceScopeCapacityInput,
        out uint persistenceScopeCountOutput,
        XrSpatialPersistenceScopeEXT* persistenceScopes);

    /// <summary>
    /// Delegate signature for `xrCreateSpatialPersistenceContextCompleteEXT`.
    /// Provided by `XR_EXT_spatial_persistence`.
    /// </summary>
    /// <param name="session">The `XrSession`.</param>
    /// <param name="future">The future.</param>
    /// <param name="completion">The completion.</param>
    /// <returns>The result of the operation.</returns>
    public delegate XrResult xrCreateSpatialPersistenceContextCompleteEXT_delegate(
        XrSession session,
        XrFutureEXT future,
        ref XrCreateSpatialPersistenceContextCompletionEXT completion);

    /// <summary>
    /// Delegate signature for `xrDestroySpatialPersistenceContextEXT`.
    /// Provided by `XR_EXT_spatial_persistence`.
    /// </summary>
    /// <param name="persistenceContext">The persistence context.</param>
    /// <returns>The result of the operation.</returns>
    public delegate XrResult xrDestroySpatialPersistenceContextEXT_delegate(
        XrSpatialPersistenceContextEXT persistenceContext);

    static class EXTSpatialPersistenceMocks
    {
        internal static XrResult xrCreateSpatialPersistenceContextAsyncEXT(
            XrSession session, in XrSpatialPersistenceContextCreateInfoEXT createInfo, out XrFutureEXT future)
        {
            future = 123456;
            return XrResult.Success;
        }

        internal static IntPtr xrCreateSpatialPersistenceContextAsyncEXT_Ptr =
            Marshal.GetFunctionPointerForDelegate(
                (xrCreateSpatialPersistenceContextAsyncEXT_delegate)xrCreateSpatialPersistenceContextAsyncEXT);

        internal static unsafe XrResult xrEnumerateSpatialPersistenceScopesEXT(
            XrInstance instance,
            XrSystemId systemId,
            uint persistenceScopeCapacityInput,
            out uint persistenceScopeCountOutput,
            XrSpatialPersistenceScopeEXT* persistenceScopes)
        {
            persistenceScopeCountOutput = 2;
            if (persistenceScopeCapacityInput < 2)
                return XrResult.Success;

            persistenceScopes[0] = XrSpatialPersistenceScopeEXT.SystemManaged;
            persistenceScopes[1] = XrSpatialPersistenceScopeEXT.LocalAnchors;
            return XrResult.Success;
        }

        internal static unsafe IntPtr xrEnumerateSpatialPersistenceScopesEXT_Ptr =
            Marshal.GetFunctionPointerForDelegate(
                (xrEnumerateSpatialPersistenceScopesEXT_delegate)xrEnumerateSpatialPersistenceScopesEXT);

        internal static XrResult xrCreateSpatialPersistenceContextCompleteEXT(
            XrSession session,
            XrFutureEXT future,
            ref XrCreateSpatialPersistenceContextCompletionEXT completion)
        {
            completion.futureResult = XrResult.Success;
            completion.createResult = XrSpatialPersistenceContextResultEXT.Success;
            completion.persistenceContext = 123456;
            return XrResult.Success;
        }

        internal static IntPtr xrCreateSpatialPersistenceContextCompleteEXT_Ptr =
            Marshal.GetFunctionPointerForDelegate(
                (xrCreateSpatialPersistenceContextCompleteEXT_delegate)xrCreateSpatialPersistenceContextCompleteEXT);

        internal static XrResult xrDestroySpatialPersistenceContextEXT(
            XrSpatialPersistenceContextEXT persistenceContext)
        {
            return XrResult.Success;
        }

        internal static IntPtr xrDestroySpatialPersistenceContextEXT_Ptr =
            Marshal.GetFunctionPointerForDelegate(
                (xrDestroySpatialPersistenceContextEXT_delegate)xrDestroySpatialPersistenceContextEXT);
    }
}
