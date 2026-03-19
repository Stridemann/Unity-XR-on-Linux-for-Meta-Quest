#if XR_COMPOSITION_LAYERS
namespace UnityEngine.XR.OpenXR.NativeTypes
{
    public unsafe struct XrCompositionLayerProjection
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
        /// The count of views in the views array. This must be equal to the number of view poses returned by xrLocateViews.
        /// </summary>
        public uint ViewCount;

        /// <summary>
        /// The array of type XrCompositionLayerProjectionView containing each projection layer view.
        /// </summary>
        public XrCompositionLayerProjectionView* Views;
    }
}
#endif
