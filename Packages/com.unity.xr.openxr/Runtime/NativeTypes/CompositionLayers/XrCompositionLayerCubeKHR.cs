#if XR_COMPOSITION_LAYERS
namespace UnityEngine.XR.OpenXR.NativeTypes
{
    public unsafe struct XrCompositionLayerCubeKHR
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
        /// The XrEyeVisibility for this layer.
        /// </summary>
        public uint EyeVisibility;

        /// <summary>
        /// The swapchain, which must have been created with a XrSwapchainCreateInfo.faceCount of 6.
        /// </summary>
        public ulong Swapchain;

        /// <summary>
        /// The image array index, with 0 meaning the first or only array element.
        /// </summary>
        public uint ImageArrayIndex;

        /// <summary>
        /// The orientation of the environment map in the space.
        /// </summary>
        public XrQuaternionf Orientation;
    }
}
#endif
