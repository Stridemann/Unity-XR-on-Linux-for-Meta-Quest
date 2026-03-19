using NUnit.Framework;
using UnityEngine.XR.OpenXR.NativeTypes;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace UnityEditor.XR.OpenXR.Tests.NativeTypes
{
    class EXTSpatialAnchorTests : MockRuntimeEditorTestBase
    {
        public override void OneTimeSetUp()
        {
            base.OneTimeSetUp();
            m_Environment.Settings.RequestUseExtension("XR_EXT_spatial_anchor");
            m_Environment.Settings.RequestUseExtension("XR_EXT_spatial_entity");
            m_Environment.Settings.RequestUseExtension("XR_EXT_future");
            m_Environment.AddSupportedExtension("XR_EXT_spatial_anchor", 1);
            m_Environment.AddSupportedExtension("XR_EXT_spatial_entity", 1);
            m_Environment.AddSupportedExtension("XR_EXT_future", 1);
        }

        [Test]
        public void xrCreateSpatialAnchorEXT_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrCreateSpatialAnchorEXT", EXTSpatialAnchorMocks.xrCreateSpatialAnchorEXT_Ptr);
            m_Environment.Start();

            var createInfo = new XrSpatialAnchorCreateInfoEXT(0, 0, new XrPosef());
            var result = OpenXRNativeApi.xrCreateSpatialAnchorEXT(0, createInfo, out var id, out var entity);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(123, id);
            Assert.AreEqual(456, entity);
        }

        [Test]
        public void xrCreateSpatialAnchorEXT_UsingContext_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrCreateSpatialAnchorEXT", EXTSpatialAnchorMocks.xrCreateSpatialAnchorEXT_Ptr);
            m_Environment.Start();

            var result = OpenXRNativeApi.xrCreateSpatialAnchorEXT(0, new XrPosef(), out var id, out var entity);

            Assert.AreEqual(OpenXRResultStatus.unqualifiedSuccess, result);
            Assert.AreEqual(123, id);
            Assert.AreEqual(456, entity);
        }

        [Test]
        public void xrCreateSpatialAnchorEXT_UsingContext_WithUnityInputs_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor(
                "xrCreateSpatialAnchorEXT", EXTSpatialAnchorMocks.xrCreateSpatialAnchorEXT_Ptr);
            m_Environment.Start();

            var result = OpenXRNativeApi.xrCreateSpatialAnchorEXT(
                0, Vector3.zero, Quaternion.identity, out var id, out var entity);

            Assert.AreEqual(OpenXRResultStatus.unqualifiedSuccess, result);
            Assert.AreEqual(123, id);
            Assert.AreEqual(456, entity);
        }
    }
}
