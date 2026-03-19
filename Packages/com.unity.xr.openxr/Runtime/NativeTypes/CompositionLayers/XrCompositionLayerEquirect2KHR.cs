#if XR_COMPOSITION_LAYERS
namespace UnityEngine.XR.OpenXR.NativeTypes
{
    public unsafe struct XrCompositionLayerEquirect2KHR
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
        /// Defines the visible horizontal angle of the sphere, based at 0 radians, in the range of [0, 2π]. It grows symmetrically around the 0 radian angle.
        /// </summary>
        public float CentralHorizontalAngle;

        /// <summary>
        /// Defines the upper vertical angle of the visible portion of the sphere, in the range of [-π/2, π/2].
        /// </summary>
        public float UpperVerticalAngle;

        /// <summary>
        /// Defines the lower vertical angle of the visible portion of the sphere, in the range of [-π/2, π/2].
        /// </summary>
        public float LowerVerticalAngle;
    }
}
#endif
