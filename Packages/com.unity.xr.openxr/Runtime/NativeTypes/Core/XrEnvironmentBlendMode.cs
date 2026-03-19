namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Runtime XR Environment Blend Mode. <see cref="Features.OpenXRFeature.SetEnvironmentBlendMode"/>
    /// </summary>
    public enum XrEnvironmentBlendMode
    {
        /// <summary>
        /// Select XR_ENVIRONMENT_BLEND_MODE_OPAQUE for runtime.
        /// </summary>
        Opaque = 1,
        /// <summary>
        /// Select XR_ENVIRONMENT_BLEND_MODE_ADDITIVE for runtime.
        /// </summary>
        Additive = 2,
        /// <summary>
        /// Select XR_ENVIRONMENT_BLEND_MODE_ALPHA_BLEND for runtime.
        /// </summary>
        AlphaBlend = 3
    }
}
