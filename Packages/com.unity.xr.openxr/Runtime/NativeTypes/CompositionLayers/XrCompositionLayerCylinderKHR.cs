#if XR_COMPOSITION_LAYERS
namespace UnityEngine.XR.OpenXR.NativeTypes
{
    public unsafe struct XrCompositionLayerCylinderKHR
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
        /// An XrPosef defining the position and orientation of the center point of the view of the cylinder within the reference frame of the space.
        /// </summary>
        public XrPosef Pose;

        /// <summary>
        /// The non-negative radius of the cylinder. Values of zero or floating point positive infinity are treated as an infinite cylinder.
        /// </summary>
        public float Radius;

        /// <summary>
        /// The angle of the visible section of the cylinder, based at 0 radians, in the range of [0, 2π). It grows symmetrically around the 0 radian angle.
        /// </summary>
        public float CentralAngle;

        /// <summary>
        /// The ratio of the visible cylinder section width / height. The height of the cylinder is given by: (cylinder radius × cylinder angle) / aspectRatio.
        /// </summary>
        public float AspectRatio;
    }
}
#endif
