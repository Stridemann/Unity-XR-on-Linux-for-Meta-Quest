#if XR_COMPOSITION_LAYERS
namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Rect in two dimensions, integer values.
    /// </summary>
    public struct XrRect2Di
    {
        /// <summary>
        /// The XrOffset2Di specifying the integer rectangle offset.
        /// </summary>
        public XrOffset2Di Offset;

        /// <summary>
        /// The XrExtent2Di specifying the integer rectangle extent.
        /// </summary>
        public XrExtent2Di Extent;
    }
}
#endif
