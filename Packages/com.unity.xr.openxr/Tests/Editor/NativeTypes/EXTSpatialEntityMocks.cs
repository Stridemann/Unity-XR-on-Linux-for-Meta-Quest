using System;
using System.Runtime.InteropServices;
using System.Text;
using AOT;
using UnityEngine;
using UnityEngine.XR.OpenXR.NativeTypes;
using XrFutureEXT = System.UInt64;
using XrInstance = System.UInt64;
using XrSession = System.UInt64;
using XrSpatialContextEXT = System.UInt64;
using XrSpatialEntityEXT = System.UInt64;
using XrSpatialSnapshotEXT = System.UInt64;
using XrSystemId = System.UInt64;

namespace UnityEditor.XR.OpenXR.Tests.NativeTypes
{
    /// <summary>
    /// Delegate signature for `xrEnumerateSpatialCapabilitiesEXT`.
    /// Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <param name="instance">The `XrInstance`.</param>
    /// <param name="systemId">The system ID.</param>
    /// <param name="capabilityCapacityInput">The capability capacity input.</param>
    /// <param name="capabilityCountOutput">The capability count output.</param>
    /// <param name="capabilities">Pointer to an array of capabilities.</param>
    /// <returns>The result of the operation.</returns>
    public unsafe delegate XrResult xrEnumerateSpatialCapabilitiesEXT_delegate(
        XrInstance instance,
        XrSystemId systemId,
        uint capabilityCapacityInput,
        out uint capabilityCountOutput,
        XrSpatialCapabilityEXT* capabilities);

    /// <summary>
    /// Delegate signature for `xrEnumerateSpatialCapabilityComponentTypesEXT`.
    /// Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <param name="instance">The `XrInstance`.</param>
    /// <param name="systemId">The system ID.</param>
    /// <param name="capability">The capability.</param>
    /// <param name="capabilityComponents">The component types.</param>
    /// <returns>The result of the operation.</returns>
    public delegate XrResult xrEnumerateSpatialCapabilityComponentTypesEXT_delegate(
        XrInstance instance,
        XrSystemId systemId,
        XrSpatialCapabilityEXT capability,
        ref XrSpatialCapabilityComponentTypesEXT capabilityComponents);

    /// <summary>
    /// Delegate signature for `xrEnumerateSpatialCapabilityFeaturesEXT`.
    /// Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <param name="instance">The `XrInstance`.</param>
    /// <param name="systemId">The system ID.</param>
    /// <param name="capability">The capability.</param>
    /// <param name="capabilityFeatureCapacityInput">The capability feature capacity input.</param>
    /// <param name="capabilityFeatureCountOutput">The capability feature count output.</param>
    /// <param name="capabilityFeatures">Pointer to an array of capability features.</param>
    /// <returns>The result of the operation.</returns>
    public unsafe delegate XrResult xrEnumerateSpatialCapabilityFeaturesEXT_delegate(
        XrInstance instance,
        XrSystemId systemId,
        XrSpatialCapabilityEXT capability,
        uint capabilityFeatureCapacityInput,
        out uint capabilityFeatureCountOutput,
        XrSpatialCapabilityFeatureEXT* capabilityFeatures);

    /// <summary>
    /// Delegate signature for `xrCreateSpatialContextCompleteEXT`.
    /// Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <param name="session">The `XrSession`.</param>
    /// <param name="future">The future.</param>
    /// <param name="completion">The completion.</param>
    /// <returns>The result of the operation.</returns>
    public delegate XrResult xrCreateSpatialContextCompleteEXT_delegate(
        XrSession session, XrFutureEXT future, ref XrCreateSpatialContextCompletionEXT completion);

    /// <summary>
    /// Delegate signature for `xrCreateSpatialContextAsyncEXT`.
    /// Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <param name="session">The `XrSession`.</param>
    /// <param name="createInfo">The create info.</param>
    /// <param name="future">The output future.</param>
    /// <returns>The result of the operation.</returns>
    public delegate XrResult xrCreateSpatialContextAsyncEXT_delegate(
        XrSession session, in XrSpatialContextCreateInfoEXT createInfo, out XrFutureEXT future);

    /// <summary>
    /// Delegate signature for `xrDestroySpatialContextEXT`.
    /// Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <param name="spatialContext">The spatial context.</param>
    /// <returns>The result of the operation.</returns>
    public delegate XrResult xrDestroySpatialContextEXT_delegate(XrSpatialContextEXT spatialContext);

    /// <summary>
    /// Delegate signature for `xrCreateSpatialEntityFromIdEXT`.
    /// Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <param name="spatialContext">The spatial context.</param>
    /// <param name="createInfo">The create info.</param>
    /// <param name="spatialEntity">The spatial entity.</param>
    /// <returns>The result of the operation.</returns>
    public delegate XrResult xrCreateSpatialEntityFromIdEXT_delegate(
        XrSpatialContextEXT spatialContext,
        in XrSpatialEntityFromIdCreateInfoEXT createInfo,
        out XrSpatialEntityEXT spatialEntity);

    /// <summary>
    /// Delegate signature for `xrDestroySpatialEntityEXT`.
    /// Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <param name="spatialEntity">The spatial entity.</param>
    /// <returns>The result of the operation.</returns>
    public delegate XrResult xrDestroySpatialEntityEXT_delegate(XrSpatialEntityEXT spatialEntity);

    /// <summary>
    /// Delegate signature for `xrCreateSpatialDiscoverySnapshotAsyncEXT`.
    /// Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <param name="spatialContext">The spatial context.</param>
    /// <param name="createInfo">The create info.</param>
    /// <param name="future">The output future.</param>
    /// <returns>The result of the operation.</returns>
    public delegate XrResult xrCreateSpatialDiscoverySnapshotAsyncEXT_delegate(
        XrSpatialContextEXT spatialContext,
        in XrSpatialDiscoverySnapshotCreateInfoEXT createInfo,
        out XrFutureEXT future);

    /// <summary>
    /// Delegate signature for `xrCreateSpatialDiscoverySnapshotCompleteEXT`.
    /// Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <param name="spatialContext">The spatial context.</param>
    /// <param name="createSnapshotCompletionInfo">The completion info.</param>
    /// <param name="completion">The completion.</param>
    /// <returns>The result of the operation.</returns>
    public delegate XrResult xrCreateSpatialDiscoverySnapshotCompleteEXT_delegate(
        XrSpatialContextEXT spatialContext,
        in XrCreateSpatialDiscoverySnapshotCompletionInfoEXT createSnapshotCompletionInfo,
        ref XrCreateSpatialDiscoverySnapshotCompletionEXT completion);

    /// <summary>
    /// Delegate signature for `xrQuerySpatialComponentDataEXT`.
    /// Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <param name="snapshot">The snapshot.</param>
    /// <param name="queryCondition">The query condition.</param>
    /// <param name="queryResult">The query result.</param>
    /// <returns>The result of the operation.</returns>
    public delegate XrResult xrQuerySpatialComponentDataEXT_delegate(
        XrSpatialSnapshotEXT snapshot,
        in XrSpatialComponentDataQueryConditionEXT queryCondition,
        ref XrSpatialComponentDataQueryResultEXT queryResult);

    /// <summary>
    /// Delegate signature for `xrGetSpatialBufferStringEXT`.
    /// Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <param name="snapshot">The snapshot.</param>
    /// <param name="getInfo">The get info.</param>
    /// <param name="bufferCapacityInput">The buffer capacity input.</param>
    /// <param name="bufferCountOutput">The buffer count output.</param>
    /// <param name="buffer">Pointer to an array of bytes.</param>
    /// <returns>The result of the operation.</returns>
    public unsafe delegate XrResult xrGetSpatialBufferStringEXT_delegate(
        XrSpatialSnapshotEXT snapshot,
        in XrSpatialBufferGetInfoEXT getInfo,
        uint bufferCapacityInput,
        out uint bufferCountOutput,
        byte* buffer);

    /// <summary>
    /// Delegate signature for `xrGetSpatialBufferUint8EXT`.
    /// Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <param name="snapshot">The snapshot.</param>
    /// <param name="getInfo">The get info.</param>
    /// <param name="bufferCapacityInput">The buffer capacity input.</param>
    /// <param name="bufferCountOutput">The buffer count output.</param>
    /// <param name="buffer">Pointer to an array of bytes.</param>
    /// <returns>The result of the operation.</returns>
    public unsafe delegate XrResult xrGetSpatialBufferUint8EXT_delegate(
        XrSpatialSnapshotEXT snapshot,
        in XrSpatialBufferGetInfoEXT getInfo,
        uint bufferCapacityInput,
        out uint bufferCountOutput,
        byte* buffer);

    /// <summary>
    /// Delegate signature for `xrGetSpatialBufferUint16EXT`.
    /// Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <param name="snapshot">The snapshot.</param>
    /// <param name="getInfo">The get info.</param>
    /// <param name="bufferCapacityInput">The buffer capacity input.</param>
    /// <param name="bufferCountOutput">The buffer count output.</param>
    /// <param name="buffer">Pointer to an array of shorts.</param>
    /// <returns>The result of the operation.</returns>
    public unsafe delegate XrResult xrGetSpatialBufferUint16EXT_delegate(
        XrSpatialSnapshotEXT snapshot,
        in XrSpatialBufferGetInfoEXT getInfo,
        uint bufferCapacityInput,
        out uint bufferCountOutput,
        short* buffer);

    /// <summary>
    /// Delegate signature for `xrGetSpatialBufferUint32EXT`.
    /// Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <param name="snapshot">The snapshot.</param>
    /// <param name="getInfo">The get info.</param>
    /// <param name="bufferCapacityInput">The buffer capacity input.</param>
    /// <param name="bufferCountOutput">The buffer count output.</param>
    /// <param name="buffer">Pointer to an array of unsigned ints.</param>
    /// <returns>The result of the operation.</returns>
    public unsafe delegate XrResult xrGetSpatialBufferUint32EXT_delegate(
        XrSpatialSnapshotEXT snapshot,
        in XrSpatialBufferGetInfoEXT getInfo,
        uint bufferCapacityInput,
        out uint bufferCountOutput,
        uint* buffer);

    /// <summary>
    /// Delegate signature for `xrGetSpatialBufferFloatEXT`.
    /// Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <param name="snapshot">The snapshot.</param>
    /// <param name="getInfo">The get info.</param>
    /// <param name="bufferCapacityInput">The buffer capacity input.</param>
    /// <param name="bufferCountOutput">The buffer count output.</param>
    /// <param name="buffer">Pointer to an array of floats.</param>
    /// <returns>The result of the operation.</returns>
    public unsafe delegate XrResult xrGetSpatialBufferFloatEXT_delegate(
        XrSpatialSnapshotEXT snapshot,
        in XrSpatialBufferGetInfoEXT getInfo,
        uint bufferCapacityInput,
        out uint bufferCountOutput,
        float* buffer);

    /// <summary>
    /// Delegate signature for `xrGetSpatialBufferVector2fEXT`.
    /// Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <param name="snapshot">The snapshot.</param>
    /// <param name="getInfo">The get info.</param>
    /// <param name="bufferCapacityInput">The buffer capacity input.</param>
    /// <param name="bufferCountOutput">The buffer count output.</param>
    /// <param name="buffer">Pointer to an array of `XrVector2f`.</param>
    /// <returns>The result of the operation.</returns>
    public unsafe delegate XrResult xrGetSpatialBufferVector2fEXT_delegate(
        XrSpatialSnapshotEXT snapshot,
        in XrSpatialBufferGetInfoEXT getInfo,
        uint bufferCapacityInput,
        out uint bufferCountOutput,
        XrVector2f* buffer);

    /// <summary>
    /// Delegate signature for `xrGetSpatialBufferVector3fEXT`.
    /// Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <param name="snapshot">The snapshot.</param>
    /// <param name="getInfo">The get info.</param>
    /// <param name="bufferCapacityInput">The buffer capacity input.</param>
    /// <param name="bufferCountOutput">The buffer count output.</param>
    /// <param name="buffer">Pointer to an array of `XrVector3f`.</param>
    /// <returns>The result of the operation.</returns>
    public unsafe delegate XrResult xrGetSpatialBufferVector3fEXT_delegate(
        XrSpatialSnapshotEXT snapshot,
        in XrSpatialBufferGetInfoEXT getInfo,
        uint bufferCapacityInput,
        out uint bufferCountOutput,
        XrVector3f* buffer);

    /// <summary>
    /// Delegate signature for `xrCreateSpatialUpdateSnapshotEXT`.
    /// Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <param name="spatialContext">The spatial context.</param>
    /// <param name="createInfo">The create info.</param>
    /// <param name="snapshot">The snapshot.</param>
    /// <returns>The result of the operation.</returns>
    public delegate XrResult xrCreateSpatialUpdateSnapshotEXT_delegate(
        XrSpatialContextEXT spatialContext,
        in XrSpatialUpdateSnapshotCreateInfoEXT createInfo,
        out XrSpatialSnapshotEXT snapshot);

    /// <summary>
    /// Delegate signature for `xrDestroySpatialSnapshotEXT`.
    /// Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <param name="snapshot">The snapshot.</param>
    /// <returns>The result of the operation.</returns>
    public delegate XrResult xrDestroySpatialSnapshotEXT_delegate(XrSpatialSnapshotEXT snapshot);

    static class EXTSpatialEntityMocks
    {
        [MonoPInvokeCallback(typeof(xrEnumerateSpatialCapabilitiesEXT_delegate))]
        internal static unsafe XrResult xrEnumerateSpatialCapabilitiesEXT(
            XrInstance instance,
            XrSystemId systemId,
            uint capabilityCountInput,
            out uint capabilityCountOutput,
            XrSpatialCapabilityEXT* capabilities)
        {
            capabilityCountOutput = 3;
            if (capabilityCountInput < 3)
                return XrResult.Success;

            capabilities[0] = XrSpatialCapabilityEXT.PlaneTracking;
            capabilities[1] = XrSpatialCapabilityEXT.MarkerTrackingQRCode;
            capabilities[2] = XrSpatialCapabilityEXT.Anchor;
            return XrResult.Success;
        }

        internal static unsafe IntPtr xrEnumerateSpatialCapabilitiesEXT_Ptr =
            Marshal.GetFunctionPointerForDelegate(
                (xrEnumerateSpatialCapabilitiesEXT_delegate)xrEnumerateSpatialCapabilitiesEXT);

        [MonoPInvokeCallback(typeof(xrEnumerateSpatialCapabilityComponentTypesEXT_delegate))]
        internal static unsafe XrResult xrEnumerateSpatialCapabilityComponentTypesEXT(
            XrInstance instance,
            XrSystemId systemId,
            XrSpatialCapabilityEXT capability,
            ref XrSpatialCapabilityComponentTypesEXT capabilityComponents)
        {
            switch (capability)
            {
                case XrSpatialCapabilityEXT.PlaneTracking:
                    capabilityComponents.componentTypeCountOutput = 2;
                    if (capabilityComponents.componentTypeCapacityInput < 2)
                        return XrResult.Success;

                    capabilityComponents.componentTypes[0] = XrSpatialComponentTypeEXT.Bounded2D;
                    capabilityComponents.componentTypes[1] = XrSpatialComponentTypeEXT.PlaneAlignment;
                    return XrResult.Success;
                case XrSpatialCapabilityEXT.MarkerTrackingQRCode:
                case XrSpatialCapabilityEXT.MarkerTrackingMicroQRCode:
                case XrSpatialCapabilityEXT.MarkerTrackingArucoMarker:
                case XrSpatialCapabilityEXT.MarkerTrackingAprilTag:
                case XrSpatialCapabilityEXT.Anchor:
                default:
                    throw new NotImplementedException();
            }
        }

        internal static IntPtr xrEnumerateSpatialCapabilityComponentTypesEXT_Ptr =
            Marshal.GetFunctionPointerForDelegate(
                (xrEnumerateSpatialCapabilityComponentTypesEXT_delegate)xrEnumerateSpatialCapabilityComponentTypesEXT);

        [MonoPInvokeCallback(typeof(xrEnumerateSpatialCapabilityFeaturesEXT_delegate))]
        internal static unsafe XrResult xrEnumerateSpatialCapabilityFeaturesEXT(
            XrInstance instance,
            XrSystemId systemId,
            XrSpatialCapabilityEXT capability,
            uint capabilityFeatureCapacityInput,
            out uint capabilityFeatureCountOutput,
            XrSpatialCapabilityFeatureEXT* capabilityFeatures)
        {
            switch (capability)
            {
                case XrSpatialCapabilityEXT.MarkerTrackingAprilTag:
                case XrSpatialCapabilityEXT.MarkerTrackingArucoMarker:
                case XrSpatialCapabilityEXT.MarkerTrackingMicroQRCode:
                case XrSpatialCapabilityEXT.MarkerTrackingQRCode:
                    capabilityFeatureCountOutput = 2;
                    if (capabilityFeatureCapacityInput < 2)
                        return XrResult.Success;

                    capabilityFeatures[0] = XrSpatialCapabilityFeatureEXT.MarkerTrackingFixedSizeMarkers;
                    capabilityFeatures[1] = XrSpatialCapabilityFeatureEXT.MarkerTrackingStaticMarkers;
                    return XrResult.Success;
                case XrSpatialCapabilityEXT.PlaneTracking:
                case XrSpatialCapabilityEXT.Anchor:
                default:
                    capabilityFeatureCountOutput = 0;
                    return XrResult.Success;
            }
        }

        internal static unsafe IntPtr xrEnumerateSpatialCapabilityFeaturesEXT_Ptr =
            Marshal.GetFunctionPointerForDelegate(
                (xrEnumerateSpatialCapabilityFeaturesEXT_delegate)xrEnumerateSpatialCapabilityFeaturesEXT);

        [MonoPInvokeCallback(typeof(xrCreateSpatialContextAsyncEXT_delegate))]
        internal static XrResult xrCreateSpatialContextAsyncEXT(
            XrSession session, in XrSpatialContextCreateInfoEXT createInfo, out XrFutureEXT future)
        {
            future = 123456;
            return XrResult.Success;
        }

        internal static IntPtr xrCreateSpatialContextAsyncEXT_Ptr =
            Marshal.GetFunctionPointerForDelegate(
                (xrCreateSpatialContextAsyncEXT_delegate)xrCreateSpatialContextAsyncEXT);

        [MonoPInvokeCallback(typeof(xrCreateSpatialContextCompleteEXT_delegate))]
        internal static XrResult xrCreateSpatialContextCompleteEXT(
            XrSession session, XrFutureEXT future, ref XrCreateSpatialContextCompletionEXT completion)
        {
            completion.futureResult = XrResult.Success;
            completion.spatialContext = 123456;
            return XrResult.Success;
        }

        internal static IntPtr xrCreateSpatialContextCompleteEXT_Ptr =
            Marshal.GetFunctionPointerForDelegate(
                (xrCreateSpatialContextCompleteEXT_delegate)xrCreateSpatialContextCompleteEXT);

        [MonoPInvokeCallback(typeof(xrDestroySpatialContextEXT_delegate))]
        internal static XrResult xrDestroySpatialContextEXT(XrSpatialContextEXT spatialContext)
        {
            return XrResult.Success;
        }

        internal static IntPtr xrDestroySpatialContextEXT_Ptr =
            Marshal.GetFunctionPointerForDelegate((xrDestroySpatialContextEXT_delegate)xrDestroySpatialContextEXT);

        [MonoPInvokeCallback(typeof(xrCreateSpatialEntityFromIdEXT_delegate))]
        internal static XrResult xrCreateSpatialEntityFromIdEXT(
            XrSpatialContextEXT spatialContext,
            in XrSpatialEntityFromIdCreateInfoEXT createInfo,
            out XrSpatialEntityEXT spatialEntity)
        {
            spatialEntity = 456;
            return XrResult.Success;
        }

        internal static IntPtr xrCreateSpatialEntityFromIdEXT_Ptr =
            Marshal.GetFunctionPointerForDelegate(
                (xrCreateSpatialEntityFromIdEXT_delegate)xrCreateSpatialEntityFromIdEXT);

        [MonoPInvokeCallback(typeof(xrDestroySpatialEntityEXT_delegate))]
        internal static XrResult xrDestroySpatialEntityEXT(XrSpatialEntityEXT spatialEntity)
        {
            return XrResult.Success;
        }

        internal static IntPtr xrDestroySpatialEntityEXT_Ptr =
            Marshal.GetFunctionPointerForDelegate((xrDestroySpatialEntityEXT_delegate)xrDestroySpatialEntityEXT);

        [MonoPInvokeCallback(typeof(xrCreateSpatialDiscoverySnapshotAsyncEXT_delegate))]
        internal static XrResult xrCreateSpatialDiscoverySnapshotAsyncEXT(
            XrSpatialContextEXT spatialContext,
            in XrSpatialDiscoverySnapshotCreateInfoEXT createInfo,
            out XrFutureEXT future)
        {
            future = 123456;
            return XrResult.Success;
        }

        internal static IntPtr xrCreateSpatialDiscoverySnapshotAsyncEXT_Ptr =
            Marshal.GetFunctionPointerForDelegate(
                (xrCreateSpatialDiscoverySnapshotAsyncEXT_delegate)xrCreateSpatialDiscoverySnapshotAsyncEXT);

        [MonoPInvokeCallback(typeof(xrCreateSpatialDiscoverySnapshotCompleteEXT_delegate))]
        internal static XrResult xrCreateSpatialDiscoverySnapshotCompleteEXT(
            XrSpatialContextEXT spatialContext,
            in XrCreateSpatialDiscoverySnapshotCompletionInfoEXT createSnapshotCompletionInfo,
            ref XrCreateSpatialDiscoverySnapshotCompletionEXT completion)
        {
            completion.futureResult = XrResult.Success;
            completion.snapshot = 123456;
            return XrResult.Success;
        }

        internal static IntPtr xrCreateSpatialDiscoverySnapshotCompleteEXT_Ptr =
            Marshal.GetFunctionPointerForDelegate(
                (xrCreateSpatialDiscoverySnapshotCompleteEXT_delegate)xrCreateSpatialDiscoverySnapshotCompleteEXT);

        [MonoPInvokeCallback(typeof(xrQuerySpatialComponentDataEXT_delegate))]
        internal static unsafe XrResult xrQuerySpatialComponentDataEXT(
            XrSpatialSnapshotEXT snapshot,
            in XrSpatialComponentDataQueryConditionEXT queryCondition,
            ref XrSpatialComponentDataQueryResultEXT queryResult)
        {
            // Mock implementation assumes that the query condition is for plane entities
            queryResult.entityIdCountOutput = 1;
            queryResult.entityStateCountOutput = 1;

            if (queryResult.entityIdCapacityInput >= 1)
                queryResult.entityIds[0] = 5555;
            if (queryResult.entityStateCapacityInput >= 1)
                queryResult.entityStates[0] = XrSpatialEntityTrackingStateEXT.Tracking;

            return XrResult.Success;
        }

        internal static IntPtr xrQuerySpatialComponentDataEXT_Ptr =
            Marshal.GetFunctionPointerForDelegate(
                (xrQuerySpatialComponentDataEXT_delegate)xrQuerySpatialComponentDataEXT);

        [MonoPInvokeCallback(typeof(xrGetSpatialBufferStringEXT_delegate))]
        internal static unsafe XrResult xrGetSpatialBufferStringEXT(
            XrSpatialSnapshotEXT snapshot,
            in XrSpatialBufferGetInfoEXT info,
            uint bufferCapacityInput,
            out uint bufferCountOutput,
            byte* buffer)
        {
            var utf16bytes = Encoding.Default.GetBytes("Hello, world.");
            var utf8bytes = Encoding.Convert(Encoding.Default, Encoding.UTF8, utf16bytes);
            bufferCountOutput = (uint)utf8bytes.Length;

            if (bufferCapacityInput == 0)
                return XrResult.Success;

            if (bufferCapacityInput < bufferCountOutput)
                return XrResult.SizeInsufficient;

            for (var i = 0; i < bufferCountOutput; ++i)
            {
                buffer[i] = utf8bytes[i];
            }

            return XrResult.Success;
        }

        internal static unsafe IntPtr xrGetSpatialBufferStringEXT_Ptr =
            Marshal.GetFunctionPointerForDelegate(
                (xrGetSpatialBufferStringEXT_delegate)xrGetSpatialBufferStringEXT);

        [MonoPInvokeCallback(typeof(xrGetSpatialBufferUint8EXT_delegate))]
        internal static unsafe XrResult xrGetSpatialBufferUint8EXT(
            XrSpatialSnapshotEXT snapshot,
            in XrSpatialBufferGetInfoEXT info,
            uint bufferCapacityInput,
            out uint bufferCountOutput,
            byte* buffer)
        {
            var utf16bytes = Encoding.Default.GetBytes("Hello, world.");
            var utf8bytes = Encoding.Convert(Encoding.Default, Encoding.UTF8, utf16bytes);
            bufferCountOutput = (uint)utf8bytes.Length;

            if (bufferCapacityInput == 0)
                return XrResult.Success;

            if (bufferCapacityInput < bufferCountOutput)
                return XrResult.SizeInsufficient;

            for (var i = 0; i < bufferCountOutput; ++i)
            {
                buffer[i] = utf8bytes[i];
            }

            return XrResult.Success;
        }

        internal static unsafe IntPtr xrGetSpatialBufferUint8EXT_Ptr =
            Marshal.GetFunctionPointerForDelegate(
                (xrGetSpatialBufferUint8EXT_delegate)xrGetSpatialBufferUint8EXT);

        [MonoPInvokeCallback(typeof(xrGetSpatialBufferUint16EXT_delegate))]
        internal static unsafe XrResult xrGetSpatialBufferUint16EXT(
            XrSpatialSnapshotEXT snapshot,
            in XrSpatialBufferGetInfoEXT info,
            uint bufferCapacityInput,
            out uint bufferCountOutput,
            short* buffer)
        {
            bufferCountOutput = 5;
            if (bufferCapacityInput == 0)
                return XrResult.Success;

            if (bufferCapacityInput < bufferCountOutput)
                return XrResult.SizeInsufficient;

            buffer[0] = 1;
            buffer[1] = 2;
            buffer[2] = 3;
            buffer[3] = 4;
            buffer[4] = 5;

            return XrResult.Success;
        }

        internal static unsafe IntPtr xrGetSpatialBufferUint16EXT_Ptr =
            Marshal.GetFunctionPointerForDelegate(
                (xrGetSpatialBufferUint16EXT_delegate)xrGetSpatialBufferUint16EXT);

        [MonoPInvokeCallback(typeof(xrGetSpatialBufferUint32EXT_delegate))]
        internal static unsafe XrResult xrGetSpatialBufferUint32EXT(
            XrSpatialSnapshotEXT snapshot,
            in XrSpatialBufferGetInfoEXT info,
            uint bufferCapacityInput,
            out uint bufferCountOutput,
            uint* buffer)
        {
            bufferCountOutput = 2;
            if (bufferCapacityInput == 0)
                return XrResult.Success;

            if (bufferCapacityInput < bufferCountOutput)
                return XrResult.SizeInsufficient;

            buffer[0] = 12345;
            buffer[1] = 67890;

            return XrResult.Success;
        }

        internal static unsafe IntPtr xrGetSpatialBufferUint32EXT_Ptr =
            Marshal.GetFunctionPointerForDelegate(
                (xrGetSpatialBufferUint32EXT_delegate)xrGetSpatialBufferUint32EXT);

        [MonoPInvokeCallback(typeof(xrGetSpatialBufferFloatEXT_delegate))]
        internal static unsafe XrResult xrGetSpatialBufferFloatEXT(
            XrSpatialSnapshotEXT snapshot,
            in XrSpatialBufferGetInfoEXT info,
            uint bufferCapacityInput,
            out uint bufferCountOutput,
            float* buffer)
        {
            bufferCountOutput = 2;
            if (bufferCapacityInput == 0)
                return XrResult.Success;

            if (bufferCapacityInput < bufferCountOutput)
                return XrResult.SizeInsufficient;

            buffer[0] = .333f;
            buffer[1] = .12345f;

            return XrResult.Success;
        }

        internal static unsafe IntPtr xrGetSpatialBufferFloatEXT_Ptr =
            Marshal.GetFunctionPointerForDelegate(
                (xrGetSpatialBufferFloatEXT_delegate)xrGetSpatialBufferFloatEXT);

        [MonoPInvokeCallback(typeof(xrGetSpatialBufferVector2fEXT_delegate))]
        internal static unsafe XrResult xrGetSpatialBufferVector2fEXT(
            XrSpatialSnapshotEXT snapshot,
            in XrSpatialBufferGetInfoEXT info,
            uint bufferCapacityInput,
            out uint bufferCountOutput,
            XrVector2f* buffer)
        {
            bufferCountOutput = 2;
            if (bufferCapacityInput == 0)
                return XrResult.Success;

            if (bufferCapacityInput < bufferCountOutput)
                return XrResult.SizeInsufficient;

            buffer[0] = new(1.5f, 2.5f);
            buffer[1] = new(3.5f, 4.5f);

            return XrResult.Success;
        }

        internal static unsafe IntPtr xrGetSpatialBufferVector2fEXT_Ptr =
            Marshal.GetFunctionPointerForDelegate(
                (xrGetSpatialBufferVector2fEXT_delegate)xrGetSpatialBufferVector2fEXT);

        [MonoPInvokeCallback(typeof(xrGetSpatialBufferVector3fEXT_delegate))]
        internal static unsafe XrResult xrGetSpatialBufferVector3fEXT(
            XrSpatialSnapshotEXT snapshot,
            in XrSpatialBufferGetInfoEXT info,
            uint bufferCapacityInput,
            out uint bufferCountOutput,
            XrVector3f* buffer)
        {
            bufferCountOutput = 2;
            if (bufferCapacityInput == 0)
                return XrResult.Success;

            if (bufferCapacityInput < bufferCountOutput)
                return XrResult.SizeInsufficient;

            buffer[0] = new(1.5f, 2.5f, 3.5f);
            buffer[1] = new(4.5f, 5.5f, 6.5f);

            return XrResult.Success;
        }

        internal static unsafe IntPtr xrGetSpatialBufferVector3fEXT_Ptr =
            Marshal.GetFunctionPointerForDelegate(
                (xrGetSpatialBufferVector3fEXT_delegate)xrGetSpatialBufferVector3fEXT);

        internal static XrResult xrCreateSpatialUpdateSnapshotEXT(
            XrSpatialContextEXT spatialContext,
            in XrSpatialUpdateSnapshotCreateInfoEXT createInfo,
            out XrSpatialSnapshotEXT snapshot)
        {
            snapshot = 123456;
            return XrResult.Success;
        }

        internal static IntPtr xrCreateSpatialUpdateSnapshotEXT_Ptr =
            Marshal.GetFunctionPointerForDelegate(
                (xrCreateSpatialUpdateSnapshotEXT_delegate)xrCreateSpatialUpdateSnapshotEXT);

        internal static XrResult xrDestroySpatialSnapshotEXT(XrSpatialSnapshotEXT snapshot)
        {
            return XrResult.Success;
        }

        internal static IntPtr xrDestroySpatialSnapshotEXT_Ptr =
            Marshal.GetFunctionPointerForDelegate((xrDestroySpatialSnapshotEXT_delegate)xrDestroySpatialSnapshotEXT);
    }
}
