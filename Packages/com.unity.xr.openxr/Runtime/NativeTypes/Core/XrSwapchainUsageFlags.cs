using System;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Values to specify the intended usage of swapchain images.
    /// </summary>
    [Flags]
    public enum XrSwapchainUsageFlags : ulong
    {
        /// <summary>
        /// Specifies that the image may be a color rendering target.
        /// </summary>
        XR_SWAPCHAIN_USAGE_COLOR_ATTACHMENT_BIT = 0x00000001,

        /// <summary>
        ///  Specifies that the image may be a depth/stencil rendering target.
        /// </summary>
        XR_SWAPCHAIN_USAGE_DEPTH_STENCIL_ATTACHMENT_BIT = 0x00000002,

        /// <summary>
        /// Specifies that the image may be accessed out of order and that access may be via atomic operations.
        /// </summary>
        XR_SWAPCHAIN_USAGE_UNORDERED_ACCESS_BIT = 0x00000004,

        /// <summary>
        /// Specifies that the image may be used as the source of a transfer operation.
        /// </summary>
        XR_SWAPCHAIN_USAGE_TRANSFER_SRC_BIT = 0x00000008,

        /// <summary>
        /// Specifies that the image may be used as the destination of a transfer operation.
        /// </summary>
        XR_SWAPCHAIN_USAGE_TRANSFER_DST_BIT = 0x00000010,

        /// <summary>
        /// Specifies that the image may be sampled by a shader.
        /// </summary>
        XR_SWAPCHAIN_USAGE_SAMPLED_BIT = 0x00000020,

        /// <summary>
        /// Specifies that the image may be reinterpreted as another image format
        /// </summary>
        XR_SWAPCHAIN_USAGE_MUTABLE_FORMAT_BIT = 0x00000040,

        /// <summary>
        /// Specifies that the image may be used as a input attachment. (Added by the XR_MND_swapchain_usage_input_attachment_bit extension)
        /// </summary>
        XR_SWAPCHAIN_USAGE_INPUT_ATTACHMENT_BIT_MND = 0x00000080,

        /// <summary>
        ///  Specifies that the image may be used as a input attachment. (Added by the XR_KHR_swapchain_usage_input_attachment_bit extension)
        /// </summary>
        XR_SWAPCHAIN_USAGE_INPUT_ATTACHMENT_BIT_KHR = 0x00000080
    }
}
