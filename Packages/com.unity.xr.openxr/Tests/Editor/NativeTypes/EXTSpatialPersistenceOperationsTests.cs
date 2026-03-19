using NUnit.Framework;
using UnityEngine.XR.OpenXR.NativeTypes;

namespace UnityEditor.XR.OpenXR.Tests.NativeTypes
{
    class EXTSpatialPersistenceOperationsTests : MockRuntimeEditorTestBase
    {
        public override void OneTimeSetUp()
        {
            base.OneTimeSetUp();
            m_Environment.Settings.RequestUseExtension("XR_EXT_spatial_persistence_operations");
            m_Environment.Settings.RequestUseExtension("XR_EXT_spatial_persistence");
            m_Environment.Settings.RequestUseExtension("XR_EXT_spatial_entity");
            m_Environment.Settings.RequestUseExtension("XR_EXT_future");
            m_Environment.AddSupportedExtension("XR_EXT_spatial_persistence_operations", 1);
            m_Environment.AddSupportedExtension("XR_EXT_spatial_persistence", 1);
            m_Environment.AddSupportedExtension("XR_EXT_spatial_entity", 1);
            m_Environment.AddSupportedExtension("XR_EXT_future", 1);
        }

        [Test]
        public void xrPersistSpatialEntityAsyncEXT_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrPersistSpatialEntityAsyncEXT",
                EXTSpatialPersistenceOperationsMocks.xrPersistSpatialEntityAsyncEXT_Ptr);
            m_Environment.Start();

            var persistInfo = new XrSpatialEntityPersistInfoEXT(0, 123);
            var result = OpenXRNativeApi.xrPersistSpatialEntityAsyncEXT(0, persistInfo, out var future);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(123456, future);
        }

        [Test]
        public void xrPersistSpatialEntityCompleteEXT_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrPersistSpatialEntityCompleteEXT",
                EXTSpatialPersistenceOperationsMocks.xrPersistSpatialEntityCompleteEXT_Ptr);
            m_Environment.Start();

            var result = OpenXRNativeApi.xrPersistSpatialEntityCompleteEXT(0, 0, out var completion);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(XrResult.Success, completion.futureResult);
            Assert.AreEqual(XrSpatialPersistenceContextResultEXT.Success, completion.persistResult);
            Assert.AreEqual(new XrUuid(123, 456), completion.persistUuid);
        }

        [Test]
        public void xrUnpersistSpatialEntityAsyncEXT_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrUnpersistSpatialEntityAsyncEXT",
                EXTSpatialPersistenceOperationsMocks.xrUnpersistSpatialEntityAsyncEXT_Ptr);
            m_Environment.Start();

            var unpersistInfo = new XrSpatialEntityUnpersistInfoEXT(new(123, 456));
            var result = OpenXRNativeApi.xrUnpersistSpatialEntityAsyncEXT(0, unpersistInfo, out var future);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(123456, future);
        }

        [Test]
        public void xrUnpersistSpatialEntityCompleteEXT_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrUnpersistSpatialEntityCompleteEXT",
                EXTSpatialPersistenceOperationsMocks.xrUnpersistSpatialEntityCompleteEXT_Ptr);
            m_Environment.Start();

            var result = OpenXRNativeApi.xrUnpersistSpatialEntityCompleteEXT(0, 0, out var completion);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(XrResult.Success, completion.futureResult);
            Assert.AreEqual(XrSpatialPersistenceContextResultEXT.Success, completion.unpersistResult);
        }
    }
}
