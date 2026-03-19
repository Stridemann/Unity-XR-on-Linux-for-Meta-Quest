#if XR_COMPOSITION_LAYERS
using System;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Specifies options for individual composition layers, and contains a bitwise-OR of zero or more of the bits.
    /// </summary>
    [Flags]
    public enum XrCompositionLayerFlags : ulong
    {
        /// <summary>
        /// Enables chromatic aberration correction when not done by default. This flag has no effect on any known conformant runtime, and is planned for deprecation for OpenXR 1.1
        /// </summary>
        CorrectChromaticAberration = 1,

        /// <summary>
        /// Enables the layer texture alpha channel.
        /// </summary>
        SourceAlpha = 2,

        /// <summary>
        /// Indicates the texture color channels have not been premultiplied by the texture alpha channel.
        /// </summary>
        UnPremultipliedAlpha = 4
    }
}
#endif
