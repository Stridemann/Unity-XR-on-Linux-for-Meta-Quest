using System;
using System.Runtime.InteropServices;
using System.Text;
using NUnit.Framework;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.XR.OpenXR.NativeTypes;
using XrSpatialEntityIdEXT = System.UInt64;

namespace UnityEditor.XR.OpenXR.Tests.NativeTypes
{
    class EXTSpatialEntityTests : MockRuntimeEditorTestBase
    {
        public override void OneTimeSetUp()
        {
            base.OneTimeSetUp();
            m_Environment.Settings.RequestUseExtension("XR_EXT_spatial_entity");
            m_Environment.Settings.RequestUseExtension("XR_EXT_future");
            m_Environment.AddSupportedExtension("XR_EXT_spatial_entity", 1);
            m_Environment.AddSupportedExtension("XR_EXT_future", 1);
        }

        [Test]
        public unsafe void xrEnumerateSpatialCapabilities_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrEnumerateSpatialCapabilitiesEXT", EXTSpatialEntityMocks.xrEnumerateSpatialCapabilitiesEXT_Ptr);
            m_Environment.Start();

            var result = OpenXRNativeApi.xrEnumerateSpatialCapabilitiesEXT(0, 0, 0, out var countOutput, null);
            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(3, countOutput);

            var array = new NativeArray<XrSpatialCapabilityEXT>((int)countOutput, Allocator.Temp);
            result = OpenXRNativeApi.xrEnumerateSpatialCapabilitiesEXT(
                0, 0, countOutput, out countOutput, (XrSpatialCapabilityEXT*)array.GetUnsafePtr());

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(3, countOutput);
            Assert.AreEqual(XrSpatialCapabilityEXT.PlaneTracking, array[0]);
            Assert.AreEqual(XrSpatialCapabilityEXT.MarkerTrackingQRCode, array[1]);
            Assert.AreEqual(XrSpatialCapabilityEXT.Anchor, array[2]);
        }

        [Test]
        public void xrEnumerateSpatialCapabilities_Array_AllocatesCorrectly()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrEnumerateSpatialCapabilitiesEXT", EXTSpatialEntityMocks.xrEnumerateSpatialCapabilitiesEXT_Ptr);
            m_Environment.Start();

            var result = OpenXRNativeApi.xrEnumerateSpatialCapabilitiesEXT(0, 0, Allocator.Temp, out var array);

            Assert.AreEqual(XrResult.Success, result);
            Assert.IsTrue(array.IsCreated);
            Assert.AreEqual(3, array.Length);
            Assert.AreEqual(XrSpatialCapabilityEXT.PlaneTracking, array[0]);
            Assert.AreEqual(XrSpatialCapabilityEXT.MarkerTrackingQRCode, array[1]);
            Assert.AreEqual(XrSpatialCapabilityEXT.Anchor, array[2]);
        }

        [Test]
        public unsafe void xrEnumerateSpatialCapabilities_UsingContext_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrEnumerateSpatialCapabilitiesEXT", EXTSpatialEntityMocks.xrEnumerateSpatialCapabilitiesEXT_Ptr);
            m_Environment.Start();

            var result = OpenXRNativeApi.xrEnumerateSpatialCapabilitiesEXT(0, out var countOutput, null);
            Assert.AreEqual(OpenXRResultStatus.unqualifiedSuccess, result);
            Assert.AreEqual(3, countOutput);

            var array = new NativeArray<XrSpatialCapabilityEXT>((int)countOutput, Allocator.Temp);
            result = OpenXRNativeApi.xrEnumerateSpatialCapabilitiesEXT(
                countOutput, out countOutput, (XrSpatialCapabilityEXT*)array.GetUnsafePtr());

            Assert.AreEqual(OpenXRResultStatus.unqualifiedSuccess, result);
            Assert.AreEqual(3, countOutput);
            Assert.AreEqual(XrSpatialCapabilityEXT.PlaneTracking, array[0]);
            Assert.AreEqual(XrSpatialCapabilityEXT.MarkerTrackingQRCode, array[1]);
            Assert.AreEqual(XrSpatialCapabilityEXT.Anchor, array[2]);
        }

        [Test]
        public void xrEnumerateSpatialCapabilities_UsingContext_Array_AllocatesCorrectly()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrEnumerateSpatialCapabilitiesEXT", EXTSpatialEntityMocks.xrEnumerateSpatialCapabilitiesEXT_Ptr);
            m_Environment.Start();

            var result = OpenXRNativeApi.xrEnumerateSpatialCapabilitiesEXT(Allocator.Temp, out var array);

            Assert.AreEqual(OpenXRResultStatus.unqualifiedSuccess, result);
            Assert.IsTrue(array.IsCreated);
            Assert.AreEqual(3, array.Length);
            Assert.AreEqual(XrSpatialCapabilityEXT.PlaneTracking, array[0]);
            Assert.AreEqual(XrSpatialCapabilityEXT.MarkerTrackingQRCode, array[1]);
            Assert.AreEqual(XrSpatialCapabilityEXT.Anchor, array[2]);
        }

        [Test]
        public unsafe void xrEnumerateSpatialCapabilityComponentTypesEXT_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrEnumerateSpatialCapabilityComponentTypesEXT",
                EXTSpatialEntityMocks.xrEnumerateSpatialCapabilityComponentTypesEXT_Ptr);
            m_Environment.Start();

            var types = XrSpatialCapabilityComponentTypesEXT.defaultValue;
            var result = OpenXRNativeApi.xrEnumerateSpatialCapabilityComponentTypesEXT(
                0, 0, XrSpatialCapabilityEXT.PlaneTracking, ref types);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(2, types.componentTypeCountOutput);

            types.componentTypeCapacityInput = types.componentTypeCountOutput;
            var array = new NativeArray<XrSpatialComponentTypeEXT>((int)types.componentTypeCountOutput, Allocator.Temp);
            types.componentTypes = (XrSpatialComponentTypeEXT*)array.GetUnsafePtr();
            result = OpenXRNativeApi.xrEnumerateSpatialCapabilityComponentTypesEXT(
                0, 0, XrSpatialCapabilityEXT.PlaneTracking, ref types);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(2, types.componentTypeCountOutput);
            Assert.AreEqual(XrSpatialComponentTypeEXT.Bounded2D, types.componentTypes[0]);
            Assert.AreEqual(XrSpatialComponentTypeEXT.PlaneAlignment, types.componentTypes[1]);
        }

        [Test]
        public void xrEnumerateSpatialCapabilityComponentTypesEXT_Array_AllocatesCorrectly()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrEnumerateSpatialCapabilityComponentTypesEXT",
                EXTSpatialEntityMocks.xrEnumerateSpatialCapabilityComponentTypesEXT_Ptr);
            m_Environment.Start();

            var result = OpenXRNativeApi.xrEnumerateSpatialCapabilityComponentTypesEXT(
                0, 0, XrSpatialCapabilityEXT.PlaneTracking, Allocator.Temp, out var array);

            Assert.AreEqual(XrResult.Success, result);
            Assert.IsTrue(array.IsCreated);
            Assert.AreEqual(2, array.Length);
            Assert.AreEqual(XrSpatialComponentTypeEXT.Bounded2D, array[0]);
            Assert.AreEqual(XrSpatialComponentTypeEXT.PlaneAlignment, array[1]);
        }

        [Test]
        public unsafe void xrEnumerateSpatialCapabilityComponentTypesEXT_UsingContext_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrEnumerateSpatialCapabilityComponentTypesEXT",
                EXTSpatialEntityMocks.xrEnumerateSpatialCapabilityComponentTypesEXT_Ptr);
            m_Environment.Start();

            var types = XrSpatialCapabilityComponentTypesEXT.defaultValue;
            var result = OpenXRNativeApi.xrEnumerateSpatialCapabilityComponentTypesEXT(
                XrSpatialCapabilityEXT.PlaneTracking, ref types);

            Assert.AreEqual(OpenXRResultStatus.unqualifiedSuccess, result);
            Assert.AreEqual(2, types.componentTypeCountOutput);

            types.componentTypeCapacityInput = types.componentTypeCountOutput;
            var array = new NativeArray<XrSpatialComponentTypeEXT>((int)types.componentTypeCountOutput, Allocator.Temp);
            types.componentTypes = (XrSpatialComponentTypeEXT*)array.GetUnsafePtr();
            result = OpenXRNativeApi.xrEnumerateSpatialCapabilityComponentTypesEXT(
                XrSpatialCapabilityEXT.PlaneTracking, ref types);

            Assert.AreEqual(OpenXRResultStatus.unqualifiedSuccess, result);
            Assert.AreEqual(2, types.componentTypeCountOutput);
            Assert.AreEqual(XrSpatialComponentTypeEXT.Bounded2D, types.componentTypes[0]);
            Assert.AreEqual(XrSpatialComponentTypeEXT.PlaneAlignment, types.componentTypes[1]);
        }

        [Test]
        public void xrEnumerateSpatialCapabilityComponentTypesEXT_UsingContext_Array_AllocatesCorrectly()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrEnumerateSpatialCapabilityComponentTypesEXT",
                EXTSpatialEntityMocks.xrEnumerateSpatialCapabilityComponentTypesEXT_Ptr);
            m_Environment.Start();

            var result = OpenXRNativeApi.xrEnumerateSpatialCapabilityComponentTypesEXT(
                XrSpatialCapabilityEXT.PlaneTracking, Allocator.Temp, out var array);

            Assert.AreEqual(OpenXRResultStatus.unqualifiedSuccess, result);
            Assert.IsTrue(array.IsCreated);
            Assert.AreEqual(2, array.Length);
            Assert.AreEqual(XrSpatialComponentTypeEXT.Bounded2D, array[0]);
            Assert.AreEqual(XrSpatialComponentTypeEXT.PlaneAlignment, array[1]);
        }

        [Test]
        public unsafe void xrEnumerateSpatialCapabilityFeaturesEXT_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrEnumerateSpatialCapabilityFeaturesEXT",
                EXTSpatialEntityMocks.xrEnumerateSpatialCapabilityFeaturesEXT_Ptr);
            m_Environment.Start();

            var result = OpenXRNativeApi.xrEnumerateSpatialCapabilityFeaturesEXT(
                0, 0, XrSpatialCapabilityEXT.MarkerTrackingQRCode, 0, out var featureCount, null);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(2, featureCount);

            var array = new NativeArray<XrSpatialCapabilityFeatureEXT>((int)featureCount, Allocator.Temp);
            result = OpenXRNativeApi.xrEnumerateSpatialCapabilityFeaturesEXT(
                0,
                0,
                XrSpatialCapabilityEXT.MarkerTrackingQRCode,
                featureCount,
                out featureCount,
                (XrSpatialCapabilityFeatureEXT*)array.GetUnsafePtr());

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(2, featureCount);
            Assert.AreEqual(XrSpatialCapabilityFeatureEXT.MarkerTrackingFixedSizeMarkers, array[0]);
            Assert.AreEqual(XrSpatialCapabilityFeatureEXT.MarkerTrackingStaticMarkers, array[1]);
        }

        [Test]
        public void xrEnumerateSpatialCapabilityFeaturesEXT_Array_AllocatesCorrectly()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrEnumerateSpatialCapabilityFeaturesEXT",
                EXTSpatialEntityMocks.xrEnumerateSpatialCapabilityFeaturesEXT_Ptr);
            m_Environment.Start();

            var result = OpenXRNativeApi.xrEnumerateSpatialCapabilityFeaturesEXT(
                0, 0, XrSpatialCapabilityEXT.MarkerTrackingQRCode, Allocator.Temp, out var array);

            Assert.AreEqual(XrResult.Success, result);
            Assert.IsTrue(array.IsCreated);
            Assert.AreEqual(2, array.Length);
            Assert.AreEqual(XrSpatialCapabilityFeatureEXT.MarkerTrackingFixedSizeMarkers, array[0]);
            Assert.AreEqual(XrSpatialCapabilityFeatureEXT.MarkerTrackingStaticMarkers, array[1]);
        }

        [Test]
        public unsafe void xrEnumerateSpatialCapabilityFeaturesEXT_UsingContext_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrEnumerateSpatialCapabilityFeaturesEXT",
                EXTSpatialEntityMocks.xrEnumerateSpatialCapabilityFeaturesEXT_Ptr);
            m_Environment.Start();

            var result = OpenXRNativeApi.xrEnumerateSpatialCapabilityFeaturesEXT(
                XrSpatialCapabilityEXT.MarkerTrackingQRCode, 0, out var featureCount, null);

            Assert.AreEqual(OpenXRResultStatus.unqualifiedSuccess, result);
            Assert.AreEqual(2, featureCount);

            var array = new NativeArray<XrSpatialCapabilityFeatureEXT>((int)featureCount, Allocator.Temp);
            result = OpenXRNativeApi.xrEnumerateSpatialCapabilityFeaturesEXT(
                XrSpatialCapabilityEXT.MarkerTrackingQRCode,
                featureCount,
                out featureCount,
                (XrSpatialCapabilityFeatureEXT*)array.GetUnsafePtr());

            Assert.AreEqual(OpenXRResultStatus.unqualifiedSuccess, result);
            Assert.AreEqual(2, featureCount);
            Assert.AreEqual(XrSpatialCapabilityFeatureEXT.MarkerTrackingFixedSizeMarkers, array[0]);
            Assert.AreEqual(XrSpatialCapabilityFeatureEXT.MarkerTrackingStaticMarkers, array[1]);
        }

        [Test]
        public void xrEnumerateSpatialCapabilityFeaturesEXT_UsingContext_Array_AllocatesCorrectly()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrEnumerateSpatialCapabilityFeaturesEXT",
                EXTSpatialEntityMocks.xrEnumerateSpatialCapabilityFeaturesEXT_Ptr);
            m_Environment.Start();

            var result = OpenXRNativeApi.xrEnumerateSpatialCapabilityFeaturesEXT(
                XrSpatialCapabilityEXT.MarkerTrackingQRCode, Allocator.Temp, out var array);

            Assert.AreEqual(OpenXRResultStatus.unqualifiedSuccess, result);
            Assert.IsTrue(array.IsCreated);
            Assert.AreEqual(2, array.Length);
            Assert.AreEqual(XrSpatialCapabilityFeatureEXT.MarkerTrackingFixedSizeMarkers, array[0]);
            Assert.AreEqual(XrSpatialCapabilityFeatureEXT.MarkerTrackingStaticMarkers, array[1]);
        }

        [Test]
        public void xrCreateSpatialContextAsyncEXT_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrCreateSpatialContextAsyncEXT", EXTSpatialEntityMocks.xrCreateSpatialContextAsyncEXT_Ptr);
            m_Environment.Start();

            // Can't create a valid SpatialContextCreateInfo because there are no capabilities in EXT_spatial_entity
            var createInfo = new XrSpatialContextCreateInfoEXT(
                new NativeArray<IntPtr>(1, Allocator.Temp));

            var result = OpenXRNativeApi.xrCreateSpatialContextAsyncEXT(0, in createInfo, out var future);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(123456, future);
        }

        [Test]
        public void xrCreateSpatialContextAsyncEXT_UsingContext_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrCreateSpatialContextAsyncEXT", EXTSpatialEntityMocks.xrCreateSpatialContextAsyncEXT_Ptr);
            m_Environment.Start();

            // Can't create a valid SpatialContextCreateInfo because there are no capabilities in EXT_spatial_entity
            var createInfo = new XrSpatialContextCreateInfoEXT(
                new NativeArray<IntPtr>(1, Allocator.Temp));
            var result = OpenXRNativeApi.xrCreateSpatialContextAsyncEXT(in createInfo, out var future);

            Assert.AreEqual(OpenXRResultStatus.unqualifiedSuccess, result);
            Assert.AreEqual(123456, future);
        }

        [Test]
        public void xrCreateSpatialContextCompleteEXT_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrCreateSpatialContextCompleteEXT", EXTSpatialEntityMocks.xrCreateSpatialContextCompleteEXT_Ptr);
            m_Environment.Start();

            var result = OpenXRNativeApi.xrCreateSpatialContextCompleteEXT(0, 123, out var completion);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(XrResult.Success, completion.futureResult);
            Assert.AreEqual(123456, completion.spatialContext);
        }

        [Test]
        public void xrCreateSpatialContextCompleteEXT_UsingContext_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrCreateSpatialContextCompleteEXT", EXTSpatialEntityMocks.xrCreateSpatialContextCompleteEXT_Ptr);
            m_Environment.Start();

            var result = OpenXRNativeApi.xrCreateSpatialContextCompleteEXT(0, out var completion);

            Assert.AreEqual(OpenXRResultStatus.unqualifiedSuccess, result);
            Assert.AreEqual(XrResult.Success, completion.futureResult);
            Assert.AreEqual(123456, completion.spatialContext);
        }

        [Test]
        public void xrDestroySpatialContextEXT_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrDestroySpatialContextEXT", EXTSpatialEntityMocks.xrDestroySpatialContextEXT_Ptr);
            m_Environment.Start();

            var result = OpenXRNativeApi.xrDestroySpatialContextEXT(0);

            Assert.AreEqual(XrResult.Success, result);
        }

        [Test]
        public void xrCreateSpatialEntityFromIdEXT_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrCreateSpatialEntityFromIdEXT", EXTSpatialEntityMocks.xrCreateSpatialEntityFromIdEXT_Ptr);
            m_Environment.Start();

            var createInfo = new XrSpatialEntityFromIdCreateInfoEXT(123);
            var result = OpenXRNativeApi.xrCreateSpatialEntityFromIdEXT(0, in createInfo, out var entity);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(456, entity);
        }

        [Test]
        public void xrDestroySpatialEntityEXT_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrDestroySpatialEntityEXT", EXTSpatialEntityMocks.xrDestroySpatialEntityEXT_Ptr);
            m_Environment.Start();

            var result = OpenXRNativeApi.xrDestroySpatialEntityEXT(123);

            Assert.AreEqual(XrResult.Success, result);
        }

        [Test]
        public unsafe void xrCreateSpatialDiscoverySnapshotAsyncEXT_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrCreateSpatialDiscoverySnapshotAsyncEXT",
                EXTSpatialEntityMocks.xrCreateSpatialDiscoverySnapshotAsyncEXT_Ptr);
            m_Environment.Start();

            var componentTypes = new NativeArray<XrSpatialComponentTypeEXT>(2, Allocator.Temp);
            componentTypes[0] = XrSpatialComponentTypeEXT.Bounded2D;
            componentTypes[1] = XrSpatialComponentTypeEXT.PlaneAlignment;

            var createInfo = new XrSpatialDiscoverySnapshotCreateInfoEXT(
                2, (XrSpatialComponentTypeEXT*)componentTypes.GetUnsafePtr());

            var result = OpenXRNativeApi.xrCreateSpatialDiscoverySnapshotAsyncEXT(0, in createInfo, out var future);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(123456, future);
        }

        [Test]
        public void xrCreateSpatialDiscoverySnapshotCompleteEXT_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrCreateSpatialDiscoverySnapshotCompleteEXT",
                EXTSpatialEntityMocks.xrCreateSpatialDiscoverySnapshotCompleteEXT_Ptr);
            m_Environment.Start();

            var completionInfo = new XrCreateSpatialDiscoverySnapshotCompletionInfoEXT(0, 0, 0);
            var result = OpenXRNativeApi.xrCreateSpatialDiscoverySnapshotCompleteEXT(
                123, in completionInfo, out var completion);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(XrResult.Success, completion.futureResult);
            Assert.AreEqual(123456, completion.snapshot);
        }

        [Test]
        public void xrCreateSpatialDiscoverySnapshotCompleteEXT_UsingContext_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrCreateSpatialDiscoverySnapshotCompleteEXT",
                EXTSpatialEntityMocks.xrCreateSpatialDiscoverySnapshotCompleteEXT_Ptr);
            m_Environment.Start();

            var result = OpenXRNativeApi.xrCreateSpatialDiscoverySnapshotCompleteEXT(
                123, 0, out var completion);

            Assert.AreEqual(OpenXRResultStatus.unqualifiedSuccess, result);
            Assert.AreEqual(XrResult.Success, completion.futureResult);
            Assert.AreEqual(123456, completion.snapshot);
        }

        [Test]
        public unsafe void xrQuerySpatialComponentDataEXT_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrQuerySpatialComponentDataEXT",
                EXTSpatialEntityMocks.xrQuerySpatialComponentDataEXT_Ptr);
            m_Environment.Start();

            var componentTypes = new NativeArray<XrSpatialComponentTypeEXT>(2, Allocator.Temp);
            componentTypes[0] = XrSpatialComponentTypeEXT.Bounded2D;
            componentTypes[1] = XrSpatialComponentTypeEXT.PlaneAlignment;

            var queryCondition = new XrSpatialComponentDataQueryConditionEXT(componentTypes);
            var queryResult = XrSpatialComponentDataQueryResultEXT.defaultValue;
            var result = OpenXRNativeApi.xrQuerySpatialComponentDataEXT(123, queryCondition, ref queryResult);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(1, queryResult.entityIdCountOutput);
            Assert.AreEqual(1, queryResult.entityStateCountOutput);

            var entityIds = new NativeArray<XrSpatialEntityIdEXT>(
                checked((int)queryResult.entityIdCountOutput), Allocator.Temp);

            var entityStates = new NativeArray<XrSpatialEntityTrackingStateEXT>(
                checked((int)queryResult.entityStateCountOutput), Allocator.Temp);

            queryResult.entityIdCapacityInput = queryResult.entityIdCountOutput;
            queryResult.entityStateCapacityInput = queryResult.entityStateCountOutput;
            queryResult.entityIds = (XrSpatialEntityIdEXT*)entityIds.GetUnsafePtr();
            queryResult.entityStates = (XrSpatialEntityTrackingStateEXT*)entityStates.GetUnsafePtr();
            result = OpenXRNativeApi.xrQuerySpatialComponentDataEXT(123, queryCondition, ref queryResult);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(1, queryResult.entityIdCountOutput);
            Assert.AreEqual(1, queryResult.entityStateCountOutput);
            Assert.AreEqual(5555, queryResult.entityIds[0]);
            Assert.AreEqual(XrSpatialEntityTrackingStateEXT.Tracking, queryResult.entityStates[0]);
        }

        [Test]
        public unsafe void xrGetSpatialBufferStringEXT_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrGetSpatialBufferStringEXT",
                EXTSpatialEntityMocks.xrGetSpatialBufferStringEXT_Ptr);
            m_Environment.Start();

            var getInfo = new XrSpatialBufferGetInfoEXT(123);
            var result = OpenXRNativeApi.xrGetSpatialBufferStringEXT(0, getInfo, 0, out var bufferCountOutput, null);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(13, bufferCountOutput);

            var array = new NativeArray<byte>(13, Allocator.Temp);
            result = OpenXRNativeApi.xrGetSpatialBufferStringEXT(
                0, getInfo, bufferCountOutput, out bufferCountOutput, (byte*)array.GetUnsafePtr());

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(13, bufferCountOutput);
            Assert.AreEqual(
                "Hello, world.", Encoding.UTF8.GetString((byte*)array.GetUnsafePtr(), (int)bufferCountOutput));
        }

        [Test]
        public void xrGetSpatialBufferStringEXT_NativeArray_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrGetSpatialBufferStringEXT",
                EXTSpatialEntityMocks.xrGetSpatialBufferStringEXT_Ptr);
            m_Environment.Start();

            var getInfo = new XrSpatialBufferGetInfoEXT(123);
            var result = OpenXRNativeApi.xrGetSpatialBufferStringEXT(0, getInfo, Allocator.Temp, out var array);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(13, array.Length);
            Assert.AreEqual("Hello, world.", Encoding.UTF8.GetString(array.AsReadOnlySpan()));
        }

        [Test]
        public unsafe void xrGetSpatialBufferUint8EXT_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrGetSpatialBufferUint8EXT",
                EXTSpatialEntityMocks.xrGetSpatialBufferUint8EXT_Ptr);
            m_Environment.Start();

            var getInfo = new XrSpatialBufferGetInfoEXT(123);
            var result = OpenXRNativeApi.xrGetSpatialBufferUint8EXT(0, getInfo, 0, out var bufferCountOutput, null);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(13, bufferCountOutput);

            var array = new NativeArray<byte>(13, Allocator.Temp);
            result = OpenXRNativeApi.xrGetSpatialBufferUint8EXT(
                0, getInfo, bufferCountOutput, out bufferCountOutput, (byte*)array.GetUnsafePtr());

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(13, bufferCountOutput);
            Assert.AreEqual(
                "Hello, world.", Encoding.UTF8.GetString((byte*)array.GetUnsafePtr(), (int)bufferCountOutput));
        }

        [Test]
        public void xrGetSpatialBufferUint8EXT_NativeArray_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrGetSpatialBufferUint8EXT",
                EXTSpatialEntityMocks.xrGetSpatialBufferUint8EXT_Ptr);
            m_Environment.Start();

            var getInfo = new XrSpatialBufferGetInfoEXT(123);
            var result = OpenXRNativeApi.xrGetSpatialBufferUint8EXT(0, getInfo, Allocator.Temp, out var array);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(13, array.Length);
            Assert.AreEqual("Hello, world.", Encoding.UTF8.GetString(array.AsReadOnlySpan()));
        }

        [Test]
        public unsafe void xrGetSpatialBufferUint16EXT_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrGetSpatialBufferUint16EXT",
                EXTSpatialEntityMocks.xrGetSpatialBufferUint16EXT_Ptr);
            m_Environment.Start();

            var getInfo = new XrSpatialBufferGetInfoEXT(123);
            var result = OpenXRNativeApi.xrGetSpatialBufferUint16EXT(0, getInfo, 0, out var bufferCountOutput, null);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(5, bufferCountOutput);

            var array = new NativeArray<ushort>(5, Allocator.Temp);
            result = OpenXRNativeApi.xrGetSpatialBufferUint16EXT(
                0, getInfo, bufferCountOutput, out bufferCountOutput, (ushort*)array.GetUnsafePtr());

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(5, bufferCountOutput);
            Assert.AreEqual(1, array[0]);
            Assert.AreEqual(2, array[1]);
            Assert.AreEqual(3, array[2]);
            Assert.AreEqual(4, array[3]);
            Assert.AreEqual(5, array[4]);
        }

        [Test]
        public void xrGetSpatialBufferUint16EXT_NativeArray_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrGetSpatialBufferUint16EXT",
                EXTSpatialEntityMocks.xrGetSpatialBufferUint16EXT_Ptr);
            m_Environment.Start();

            var getInfo = new XrSpatialBufferGetInfoEXT(123);
            var result = OpenXRNativeApi.xrGetSpatialBufferUint16EXT(0, getInfo, Allocator.Temp, out var array);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(5, array.Length);
            Assert.AreEqual(1, array[0]);
            Assert.AreEqual(2, array[1]);
            Assert.AreEqual(3, array[2]);
            Assert.AreEqual(4, array[3]);
            Assert.AreEqual(5, array[4]);
        }

        [Test]
        public unsafe void xrGetSpatialBufferUint32EXT_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrGetSpatialBufferUint32EXT",
                EXTSpatialEntityMocks.xrGetSpatialBufferUint32EXT_Ptr);
            m_Environment.Start();

            var getInfo = new XrSpatialBufferGetInfoEXT(123);
            var result = OpenXRNativeApi.xrGetSpatialBufferUint32EXT(0, getInfo, 0, out var bufferCountOutput, null);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(2, bufferCountOutput);

            var array = new NativeArray<uint>(2, Allocator.Temp);
            result = OpenXRNativeApi.xrGetSpatialBufferUint32EXT(
                0, getInfo, bufferCountOutput, out bufferCountOutput, (uint*)array.GetUnsafePtr());

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(2, bufferCountOutput);
            Assert.AreEqual(12345, array[0]);
            Assert.AreEqual(67890, array[1]);
        }

        [Test]
        public void xrGetSpatialBufferUint32EXT_NativeArray_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrGetSpatialBufferUint32EXT",
                EXTSpatialEntityMocks.xrGetSpatialBufferUint32EXT_Ptr);
            m_Environment.Start();

            var getInfo = new XrSpatialBufferGetInfoEXT(123);
            var result = OpenXRNativeApi.xrGetSpatialBufferUint32EXT(0, getInfo, Allocator.Temp, out var array);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(2, array.Length);
            Assert.AreEqual(12345, array[0]);
            Assert.AreEqual(67890, array[1]);
        }

        [Test]
        public unsafe void xrGetSpatialBufferFloatEXT_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrGetSpatialBufferFloatEXT",
                EXTSpatialEntityMocks.xrGetSpatialBufferFloatEXT_Ptr);
            m_Environment.Start();

            var getInfo = new XrSpatialBufferGetInfoEXT(123);
            var result = OpenXRNativeApi.xrGetSpatialBufferFloatEXT(0, getInfo, 0, out var bufferCountOutput, null);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(2, bufferCountOutput);

            var array = new NativeArray<float>(2, Allocator.Temp);
            result = OpenXRNativeApi.xrGetSpatialBufferFloatEXT(
                0, getInfo, bufferCountOutput, out bufferCountOutput, (float*)array.GetUnsafePtr());

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(2, bufferCountOutput);
            Assert.AreEqual(.333f, array[0]);
            Assert.AreEqual(.12345f, array[1]);
        }

        [Test]
        public void xrGetSpatialBufferFloatEXT_NativeArray_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrGetSpatialBufferFloatEXT",
                EXTSpatialEntityMocks.xrGetSpatialBufferFloatEXT_Ptr);
            m_Environment.Start();

            var getInfo = new XrSpatialBufferGetInfoEXT(123);
            var result = OpenXRNativeApi.xrGetSpatialBufferFloatEXT(0, getInfo, Allocator.Temp, out var array);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(2, array.Length);
            Assert.AreEqual(.333f, array[0]);
            Assert.AreEqual(.12345f, array[1]);
        }

        [Test]
        public unsafe void xrGetSpatialBufferVector2fEXT_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrGetSpatialBufferVector2fEXT",
                EXTSpatialEntityMocks.xrGetSpatialBufferVector2fEXT_Ptr);
            m_Environment.Start();

            var getInfo = new XrSpatialBufferGetInfoEXT(123);
            var result = OpenXRNativeApi.xrGetSpatialBufferVector2fEXT(0, getInfo, 0, out var bufferCountOutput, null);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(2, bufferCountOutput);

            var array = new NativeArray<XrVector2f>(2, Allocator.Temp);
            result = OpenXRNativeApi.xrGetSpatialBufferVector2fEXT(
                0, getInfo, bufferCountOutput, out bufferCountOutput, (XrVector2f*)array.GetUnsafePtr());

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(2, bufferCountOutput);
            Assert.IsTrue(new XrVector2f(1.5f, 2.5f).Equals(array[0]));
            Assert.IsTrue(new XrVector2f(3.5f, 4.5f).Equals(array[1]));
        }

        [Test]
        public void xrGetSpatialBufferVector2fEXT_NativeArray_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrGetSpatialBufferVector2fEXT",
                EXTSpatialEntityMocks.xrGetSpatialBufferVector2fEXT_Ptr);
            m_Environment.Start();

            var getInfo = new XrSpatialBufferGetInfoEXT(123);
            var result = OpenXRNativeApi.xrGetSpatialBufferVector2fEXT(0, getInfo, Allocator.Temp, out var array);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(2, array.Length);
            Assert.IsTrue(new XrVector2f(1.5f, 2.5f).Equals(array[0]));
            Assert.IsTrue(new XrVector2f(3.5f, 4.5f).Equals(array[1]));
        }

        [Test]
        public unsafe void xrGetSpatialBufferVector3fEXT_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrGetSpatialBufferVector3fEXT",
                EXTSpatialEntityMocks.xrGetSpatialBufferVector3fEXT_Ptr);
            m_Environment.Start();

            var getInfo = new XrSpatialBufferGetInfoEXT(123);
            var result = OpenXRNativeApi.xrGetSpatialBufferVector3fEXT(0, getInfo, 0, out var bufferCountOutput, null);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(2, bufferCountOutput);

            var array = new NativeArray<XrVector3f>(2, Allocator.Temp);
            result = OpenXRNativeApi.xrGetSpatialBufferVector3fEXT(
                0, getInfo, bufferCountOutput, out bufferCountOutput, (XrVector3f*)array.GetUnsafePtr());

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(2, bufferCountOutput);
            Assert.IsTrue(new XrVector3f(1.5f, 2.5f, 3.5f).Equals(array[0]));
            Assert.IsTrue(new XrVector3f(4.5f, 5.5f, 6.5f).Equals(array[1]));
        }

        [Test]
        public void xrGetSpatialBufferVector3fEXT_NativeArray_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrGetSpatialBufferVector3fEXT",
                EXTSpatialEntityMocks.xrGetSpatialBufferVector3fEXT_Ptr);
            m_Environment.Start();

            var getInfo = new XrSpatialBufferGetInfoEXT(123);
            var result = OpenXRNativeApi.xrGetSpatialBufferVector3fEXT(0, getInfo, Allocator.Temp, out var array);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(2, array.Length);
            Assert.IsTrue(new XrVector3f(1.5f, 2.5f, 3.5f).Equals(array[0]));
            Assert.IsTrue(new XrVector3f(4.5f, 5.5f, 6.5f).Equals(array[1]));
        }

        [Test]
        public unsafe void xrCreateSpatialUpdateSnapshotEXT_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrCreateSpatialUpdateSnapshotEXT",
                EXTSpatialEntityMocks.xrCreateSpatialUpdateSnapshotEXT_Ptr);
            m_Environment.Start();

            var createInfo = new XrSpatialUpdateSnapshotCreateInfoEXT(0, null, 0, null, 0, 0);
            var result = OpenXRNativeApi.xrCreateSpatialUpdateSnapshotEXT(0, createInfo, out var snapshot);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(123456, snapshot);
        }

        [Test]
        public unsafe void xrCreateSpatialUpdateSnapshotEXT_UsingContext_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrCreateSpatialUpdateSnapshotEXT",
                EXTSpatialEntityMocks.xrCreateSpatialUpdateSnapshotEXT_Ptr);
            m_Environment.Start();

            var result = OpenXRNativeApi.xrCreateSpatialUpdateSnapshotEXT(0, 0, null, 0, null, out var snapshot);

            Assert.AreEqual(OpenXRResultStatus.unqualifiedSuccess, result);
            Assert.AreEqual(123456, snapshot);
        }

        [Test]
        public void xrCreateSpatialUpdateSnapshotEXT_NativeArray_UsingContext_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrCreateSpatialUpdateSnapshotEXT",
                EXTSpatialEntityMocks.xrCreateSpatialUpdateSnapshotEXT_Ptr);
            m_Environment.Start();

            var entities = new NativeArray<XrSpatialEntityIdEXT>(1, Allocator.Temp);
            var componentTypes = new NativeArray<XrSpatialComponentTypeEXT>(1, Allocator.Temp);
            var result = OpenXRNativeApi.xrCreateSpatialUpdateSnapshotEXT(0, entities, componentTypes, out var snapshot);

            Assert.AreEqual(OpenXRResultStatus.unqualifiedSuccess, result);
            Assert.AreEqual(123456, snapshot);
        }

        [Test]
        public void xrDestroySpatialSnapshotEXT_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrDestroySpatialSnapshotEXT",
                EXTSpatialEntityMocks.xrDestroySpatialSnapshotEXT_Ptr);
            m_Environment.Start();

            var result = OpenXRNativeApi.xrDestroySpatialSnapshotEXT(0);

            Assert.AreEqual(XrResult.Success, result);
        }

        [DllImport("UnityOpenXR", EntryPoint = "EXT_spatial_entity_ValidateStruct")]
        [return: MarshalAs(UnmanagedType.U1)]
        static extern unsafe bool ValidateStruct(XrBaseInStructure* structPtr);

        [Test]
        public unsafe void ValidateStruct_XrSpatialContextCreateInfoEXT()
        {
            var planeComponents = new NativeArray<XrSpatialComponentTypeEXT>(4, Allocator.Temp);
            planeComponents[0] = XrSpatialComponentTypeEXT.Bounded2D;
            planeComponents[1] = XrSpatialComponentTypeEXT.PlaneAlignment;
            planeComponents[2] = XrSpatialComponentTypeEXT.Polygon2D;
            planeComponents[3] = XrSpatialComponentTypeEXT.PlaneSemanticLabel;

            var planeConfig = new XrSpatialCapabilityConfigurationPlaneTrackingEXT(planeComponents);
            var planeConfigPtr = Marshal.AllocHGlobal(Marshal.SizeOf<XrSpatialCapabilityConfigurationPlaneTrackingEXT>());
            Marshal.StructureToPtr(planeConfig, planeConfigPtr, false);

            var anchorComponents = new NativeArray<XrSpatialComponentTypeEXT>(2, Allocator.Temp);
            anchorComponents[0] = XrSpatialComponentTypeEXT.Anchor;
            anchorComponents[1] = XrSpatialComponentTypeEXT.Persistence;

            var anchorConfig = new XrSpatialCapabilityConfigurationAnchorEXT(anchorComponents);
            var anchorConfigPtr = Marshal.AllocHGlobal(Marshal.SizeOf<XrSpatialCapabilityConfigurationAnchorEXT>());
            Marshal.StructureToPtr(anchorConfig, anchorConfigPtr, false);

            var markerComponents = new NativeArray<XrSpatialComponentTypeEXT>(2, Allocator.Temp);
            markerComponents[0] = XrSpatialComponentTypeEXT.Marker;
            markerComponents[1] = XrSpatialComponentTypeEXT.Bounded2D;

            var arucoConfig = new XrSpatialCapabilityConfigurationArucoMarkerEXT(
                markerComponents, XrSpatialMarkerArucoDictEXT.Dict_4x4_50);
            var arucoConfigPtr = Marshal.AllocHGlobal(Marshal.SizeOf<XrSpatialCapabilityConfigurationArucoMarkerEXT>());
            Marshal.StructureToPtr(arucoConfig, arucoConfigPtr, false);

            var aprilConfig = new XrSpatialCapabilityConfigurationAprilTagEXT(
                markerComponents, XrSpatialMarkerAprilTagDictEXT.Dict_16h5);
            var aprilConfigPtr = Marshal.AllocHGlobal(Marshal.SizeOf<XrSpatialCapabilityConfigurationAprilTagEXT>());
            Marshal.StructureToPtr(aprilConfig, aprilConfigPtr, false);

            var qrConfig = new XrSpatialCapabilityConfigurationQrCodeEXT(markerComponents);
            var qrConfigPtr = Marshal.AllocHGlobal(Marshal.SizeOf<XrSpatialCapabilityConfigurationQrCodeEXT>());
            Marshal.StructureToPtr(qrConfig, qrConfigPtr, false);

            var microQrConfig = new XrSpatialCapabilityConfigurationMicroQrCodeEXT(markerComponents);
            var microQrConfigPtr = Marshal.AllocHGlobal(
                Marshal.SizeOf<XrSpatialCapabilityConfigurationMicroQrCodeEXT>());
            Marshal.StructureToPtr(microQrConfig, microQrConfigPtr, false);

            var configPtrs = new NativeArray<IntPtr>(6, Allocator.Temp);
            configPtrs[0] = planeConfigPtr;
            configPtrs[1] = anchorConfigPtr;
            configPtrs[2] = arucoConfigPtr;
            configPtrs[3] = aprilConfigPtr;
            configPtrs[4] = qrConfigPtr;
            configPtrs[5] = microQrConfigPtr;

            var contextCreateInfo = new XrSpatialContextCreateInfoEXT(configPtrs);

            try
            {
                Assert.IsTrue(ValidateStruct((XrBaseInStructure*)&contextCreateInfo));
            }
            finally
            {
                Marshal.FreeHGlobal(planeConfigPtr);
                Marshal.FreeHGlobal(anchorConfigPtr);
                Marshal.FreeHGlobal(arucoConfigPtr);
                Marshal.FreeHGlobal(aprilConfigPtr);
                Marshal.FreeHGlobal(qrConfigPtr);
                Marshal.FreeHGlobal(microQrConfigPtr);
            }
        }
    }
}
