#if XR_COMPOSITION_LAYERS
namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Quad composition layer.
    /// </summary>
    public unsafe struct XrCompositionLayerQuad
    {
        /// <summary>
        /// The XrStructureType of this structure.
        /// </summary>
        public uint Type;

        /// <summary>
        /// Pointer to the next structure in a structure chain.
        /// </summary>
        public void* Next;

        /// <summary>
        /// Bitmask of XrCompositionLayerFlags describing flags to apply to the layer.
        /// </summary>
        public XrCompositionLayerFlags LayerFlags;

        /// <summary>
        /// The XrSpace in which the layer will be kept stable over time.
        /// </summary>
        public ulong Space;

        /// <summary>
        /// The XrEyeVisibility for this layer.
        /// </summary>
        public uint EyeVisibility;

        /// <summary>
        /// The image layer XrSwapchainSubImage to use. The swapchain must have been created with a XrSwapchainCreateInfo.faceCount of 1.
        /// </summary>
        public XrSwapchainSubImage SubImage;

        /// <summary>
        /// An XrPosef defining the position and orientation of the quad in the reference frame of the space.
        /// </summary>
        public XrPosef Pose;

        /// <summary>
        /// The width and height of the quad in meters.
        /// </summary>
        public XrExtent2Df Size;
    }
}
#endif
