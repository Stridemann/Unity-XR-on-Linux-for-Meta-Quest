#if XR_COMPOSITION_LAYERS
namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Composition layer data describing the associated swapchain.
    /// </summary>
    public struct XrSwapchainSubImage
    {
        /// <summary>
        /// The XrSwapchain to be displayed.
        /// </summary>
        public ulong Swapchain;

        /// <summary>
        /// An XrRect2Di representing the valid portion of the image to use, in pixels.
        /// It also implicitly defines the transform from normalized image coordinates into pixel coordinates.
        /// The coordinate origin depends on which graphics API is being used.
        /// See the graphics API extension details for more information on the coordinate origin definition. Note that the compositor may bleed in pixels from outside the bounds in some cases, for instance due to mipmapping.
        /// </summary>
        public XrRect2Di ImageRect;

        /// <summary>
        /// Composition layer data describing the associated swapchain.
        /// </summary>
        public uint ImageArrayIndex;
    }
}
#endif
