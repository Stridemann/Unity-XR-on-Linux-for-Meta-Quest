#if XR_COMPOSITION_LAYERS
namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Projection layer element
    /// </summary>
    public unsafe struct XrCompositionLayerProjectionView
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
        /// An XrPosef defining the location and orientation of this projection element in the space of the corresponding XrCompositionLayerProjectionView.
        /// </summary>
        public XrPosef Pose;

        /// <summary>
        /// The XrFovf for this projection element.
        /// </summary>
        public XrFovf Fov;

        /// <summary>
        /// The image layer XrSwapchainSubImage to use. The swapchain must have been created with a XrSwapchainCreateInfo.faceCount of 1.
        /// </summary>
        public XrSwapchainSubImage SubImage;
    }
}
#endif
