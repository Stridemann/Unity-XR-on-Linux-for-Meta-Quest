#if XR_COMPOSITION_LAYERS
namespace UnityEngine.XR.OpenXR.NativeTypes
{
    public unsafe struct XrCompositionLayerEquirectKHR
    {
        /// <summary>
        /// The XrStructureType of this structure. This base structure itself has no associated XrStructureType value
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
        /// The eye represented by this layer.
        /// </summary>
        public uint EyeVisibility;

        /// <summary>
        /// Identifies the image XrSwapchainSubImage to use. The swapchain must have been created with a XrSwapchainCreateInfo.faceCount of 1.
        /// </summary>
        public XrSwapchainSubImage SubImage;

        /// <summary>
        /// An XrPosef defining the position and orientation of the center point of the sphere onto which the equirect image data is mapped, relative to the reference frame of the space.
        /// </summary>
        public XrPosef Pose;

        /// <summary>
        /// The non-negative radius of the sphere onto which the equirect image data is mapped. Values of zero or floating point positive infinity are treated as an infinite sphere.
        /// </summary>
        public float Radius;

        /// <summary>
        /// An XrVector2f indicating a scale of the texture coordinates after the mapping to 2D.
        /// </summary>
        public XrVector2f Scale;

        /// <summary>
        /// An XrVector2f indicating a bias of the texture coordinates after the mapping to 2D.
        /// </summary>
        public XrVector2f Bias;
    }
}
#endif
