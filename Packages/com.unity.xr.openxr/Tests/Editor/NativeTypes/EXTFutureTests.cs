using NUnit.Framework;
using UnityEngine;
using UnityEngine.XR.OpenXR.NativeTypes;

namespace UnityEditor.XR.OpenXR.Tests.NativeTypes
{
    class EXTFutureTests : MockRuntimeEditorTestBase
    {
        [OneTimeSetUp]
        public override void OneTimeSetUp()
        {
            base.OneTimeSetUp();
            m_Environment.Settings.RequestUseExtension("XR_EXT_future");
            m_Environment.AddSupportedExtension("XR_EXT_future", 1);
        }

        [Test]
        public void xrPollFutureEXT_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor("xrPollFutureEXT", EXTFutureMocks.xrPollFutureEXT_Ready_Ptr);
            m_Environment.Start();

            XrFuturePollInfoEXT pollInfo = new(123456);
            var result = OpenXRNativeApi.xrPollFutureEXT(0, in pollInfo, out var pollResult);

            Assert.AreEqual(XrResult.Success, result);
            Assert.AreEqual(XrFutureStateEXT.Ready, pollResult.state);
        }

        [Test]
        public void xrPollFutureEXT_UsingContext_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor("xrPollFutureEXT", EXTFutureMocks.xrPollFutureEXT_Ready_Ptr);
            m_Environment.Start();

            XrFuturePollInfoEXT pollInfo = new(123456);
            var result = OpenXRNativeApi.xrPollFutureEXT(in pollInfo, out var pollResult);

            Assert.AreEqual(OpenXRResultStatus.unqualifiedSuccess, result);
            Assert.AreEqual(XrFutureStateEXT.Ready, pollResult.state);
        }

        [Test]
        public void xrPollFutureEXT_UsingContextConvenient_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor("xrPollFutureEXT", EXTFutureMocks.xrPollFutureEXT_Ready_Ptr);
            m_Environment.Start();

            var result = OpenXRNativeApi.xrPollFutureEXT(123456, out var pollResult);

            Assert.AreEqual(OpenXRResultStatus.unqualifiedSuccess, result);
            Assert.AreEqual(XrFutureStateEXT.Ready, pollResult.state);
        }

        [Test]
        public void XrCancelFutureEXT_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor("xrCancelFutureEXT", EXTFutureMocks.xrCancelFutureEXT_Ptr);
            m_Environment.Start();

            XrFutureCancelInfoEXT cancelInfo = new(123456);
            var result = OpenXRNativeApi.xrCancelFutureEXT(0, in cancelInfo);

            Assert.AreEqual(XrResult.Success, result);
        }

        [Test]
        public void XrCancelFutureEXT_UsingContext_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor("xrCancelFutureEXT", EXTFutureMocks.xrCancelFutureEXT_Ptr);
            m_Environment.Start();

            XrFutureCancelInfoEXT cancelInfo = new(123456);
            var result = OpenXRNativeApi.xrCancelFutureEXT(in cancelInfo);

            Assert.AreEqual(OpenXRResultStatus.unqualifiedSuccess, result);
        }

        [Test]
        public void XrCancelFutureEXT_UsingContextConvenient_ReturnsRuntimeValues()
        {
            m_Environment.SetFunctionForInterceptor("xrCancelFutureEXT", EXTFutureMocks.xrCancelFutureEXT_Ptr);
            m_Environment.Start();

            var result = OpenXRNativeApi.xrCancelFutureEXT(123456);

            Assert.AreEqual(OpenXRResultStatus.unqualifiedSuccess, result);
        }
    }
}
