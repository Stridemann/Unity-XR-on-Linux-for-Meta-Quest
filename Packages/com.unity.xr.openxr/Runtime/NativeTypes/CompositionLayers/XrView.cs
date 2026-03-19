#if XR_COMPOSITION_LAYERS
namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Struct containing view projection state.
    /// </summary>
    public unsafe struct XrView
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
        /// An XrPosef defining the location and orientation of the view in the space specified by the xrLocateViews function.
        /// </summary>
        public XrPosef Pose;

        /// <summary>
        /// The XrFovf for the four sides of the projection.
        /// </summary>
        public XrFovf Fov;
    }
}
#endif
