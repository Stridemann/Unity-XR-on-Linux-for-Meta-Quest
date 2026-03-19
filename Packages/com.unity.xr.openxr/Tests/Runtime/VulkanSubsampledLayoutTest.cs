using System;
using System.Collections;
using System.Runtime.InteropServices;
using AOT;
using NUnit.Framework;
using UnityEngine.Rendering;
using UnityEngine.TestTools;
using UnityEngine.XR.OpenXR.Features;
using UnityEngine.XR.OpenXR.Features.Mock;
using UnityEngine.XR.OpenXR.NativeTypes;
using XrSession = System.UIntPtr;
using XrSwapchain = System.UIntPtr;
using XrSwapchainCreateFlags = System.UInt64;
using XrSwapchainUsageFlags = System.UInt64;
using VkImageCreateFlags = System.UInt32;
using VkImageUsageFlags = System.UInt32;

namespace UnityEngine.XR.OpenXR.Tests
{
    class VulkanSubsampledLayoutTest : MockRuntimeTestBase
    {
        const uint s_VulkanSubsampledBit = 0x00004000;

        // used to test whether the last call to xrCreateSwapchain was flagged for subsampling
        static bool s_HasSubsamplingFlag;

        protected override void CustomOneTimeSetup()
        {
            MockRuntime.Instance.openxrExtensionStrings = "";
            m_Environment.Settings.RequestUseExtension("XR_META_vulkan_swapchain_create_info");
            m_Environment.AddSupportedExtension("XR_META_vulkan_swapchain_create_info", 1);

            m_Environment.SetFunctionForInterceptor(
                "xrCreateSwapchain",
                CreateSwapchain);
        }

        protected override void CustomTearDown()
        {
            s_HasSubsamplingFlag = false;
            FoveatedRenderingFeature.TrySetSubsampledLayoutEnabled(false);
        }

        [UnityTest]
        [UnityPlatform(RuntimePlatform.WindowsEditor, RuntimePlatform.WindowsPlayer)]
        [Category("VulkanTest")]
        public IEnumerator VulkanSubsampledLayout_InitiallyDisabled_ShouldNotContainFlag()
        {
            if (SystemInfo.graphicsDeviceType != GraphicsDeviceType.Vulkan)
            {
                Assert.Ignore("This test requires the Vulkan graphics API and was skipped.");
            }

            m_Environment.Start();

            yield return new WaitForXrFrame();
            Assert.IsFalse(s_HasSubsamplingFlag);
        }

        [UnityTest]
        [UnityPlatform(RuntimePlatform.WindowsEditor, RuntimePlatform.WindowsPlayer)]
        [Category("VulkanTest")]
        public IEnumerator VulkanSubsampledLayout_EnabledAtRuntime_ShouldContainFlag()
        {
            if (SystemInfo.graphicsDeviceType != GraphicsDeviceType.Vulkan)
            {
                Assert.Ignore("This test requires the Vulkan graphics API and was skipped.");
            }

            m_Environment.Start();

            yield return new WaitForXrFrame();
            Assert.IsFalse(s_HasSubsamplingFlag);
            Assert.IsFalse(FoveatedRenderingFeature.isSubsampledLayoutEnabled);

            FoveatedRenderingFeature.TrySetSubsampledLayoutEnabled(true);

            yield return new WaitForXrFrame();
            Assert.IsTrue(s_HasSubsamplingFlag);
            Assert.IsTrue(FoveatedRenderingFeature.isSubsampledLayoutEnabled);
        }

        [UnityTest]
        [UnityPlatform(RuntimePlatform.WindowsEditor, RuntimePlatform.WindowsPlayer)]
        [Category("VulkanTest")]
        public IEnumerator VulkanSubsampledLayout_InitiallyEnabled_ShouldContainFlag()
        {
            if (SystemInfo.graphicsDeviceType != GraphicsDeviceType.Vulkan)
            {
                Assert.Ignore("This test requires the Vulkan graphics API and was skipped.");
            }

            m_Environment.Start();

            FoveatedRenderingFeature.TrySetSubsampledLayoutEnabled(true);

            yield return new WaitForXrFrame();
            Assert.IsTrue(s_HasSubsamplingFlag);
        }

        [UnityTest]
        [UnityPlatform(RuntimePlatform.WindowsEditor, RuntimePlatform.WindowsPlayer)]
        [Category("VulkanTest")]
        public IEnumerator VulkanSubsampledLayout_DisabledAtRuntime_ShouldNotContainFlag()
        {
            if (SystemInfo.graphicsDeviceType != GraphicsDeviceType.Vulkan)
            {
                Assert.Ignore("This test requires the Vulkan graphics API and was skipped.");
            }

            m_Environment.Start();

            FoveatedRenderingFeature.TrySetSubsampledLayoutEnabled(true);

            yield return new WaitForXrFrame();
            Assert.IsTrue(s_HasSubsamplingFlag);
            Assert.IsTrue(FoveatedRenderingFeature.isSubsampledLayoutEnabled);

            FoveatedRenderingFeature.TrySetSubsampledLayoutEnabled(false);

            yield return new WaitForXrFrame();
            Assert.IsFalse(s_HasSubsamplingFlag);
            Assert.IsFalse(FoveatedRenderingFeature.isSubsampledLayoutEnabled);
        }

        [Test]
        public void VulkanSubsampledLayout_VulkanDisabled_SetSubsamplingShouldFail()
        {
            if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.Vulkan)
            {
                Assert.Ignore("This test requires the absence of the Vulkan graphics API and was skipped.");
            }

            m_Environment.Start();

            LogAssert.Expect(LogType.Error, "Could not enable subsampling. Subsampled Layout is only supported on Vulkan graphics API.");
            FoveatedRenderingFeature.TrySetSubsampledLayoutEnabled(true);
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct XrSwapchainCreateInfo
        {
            internal XrStructureType type;
            internal IntPtr next;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct XrVulkanSwapchainCreateInfoMETA
        {
            internal XrStructureType type;
            internal IntPtr next;
            internal VkImageCreateFlags additionalCreateFlags;
        }

        internal static readonly unsafe IntPtr CreateSwapchain =
            Marshal.GetFunctionPointerForDelegate(
                new CreateSwapchain_Delegate(CreateSwapchain_MockCallback));

        internal unsafe delegate int CreateSwapchain_Delegate(
            XrSession session,
            XrSwapchainCreateInfo* info,
            XrSwapchain* swapchain);

        [MonoPInvokeCallback(typeof(CreateSwapchain_Delegate))]
        internal static unsafe int CreateSwapchain_MockCallback(
            XrSession session,
            XrSwapchainCreateInfo* info,
            XrSwapchain* swapchain)
        {
            s_HasSubsamplingFlag = false;

            var currentInfo = info->next;

            while (currentInfo != IntPtr.Zero)
            {
                if (Marshal.PtrToStructure<XrStructureType>(currentInfo) == XrStructureType.VulkanSwapchainCreateInfoMETA)
                {
                    var vulkanCreateInfo = Marshal.PtrToStructure<XrVulkanSwapchainCreateInfoMETA>(currentInfo);
                    s_HasSubsamplingFlag = (vulkanCreateInfo.additionalCreateFlags & s_VulkanSubsampledBit) != 0;
                }

                currentInfo = Marshal.ReadIntPtr(currentInfo, IntPtr.Size);
            }

            return 0;
        }
    }
}
