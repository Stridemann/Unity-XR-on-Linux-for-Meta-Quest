#if XR_COMPOSITION_LAYERS
namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Composition layer base header.
    /// </summary>
    public unsafe struct XrCompositionLayerBaseHeader
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
    }
}
#endif
