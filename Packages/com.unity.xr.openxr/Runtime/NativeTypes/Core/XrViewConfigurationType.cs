namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Runtime View Configuration. <see cref="Features.Mock.MockRuntime.SetViewState"/>
    /// </summary>
    public enum XrViewConfigurationType
    {
        /// <summary>
        /// Select Mono view for runtime.
        /// </summary>
        PrimaryMono = 1,

        /// <summary>
        /// Select Stereo view for runtime.
        /// </summary>
        PrimaryStereo = 2,

        /// <summary>
        /// Select Quad view for runtime.
        /// </summary>
        PrimaryQuadVarjo = 1000037000,

        /// <summary>
        /// Select first person mono view for runtime.
        /// </summary>
        SecondaryMonoFirstPersonObserver = 1000054000,

        /// <summary>
        /// Select third person mono view for runtime.
        /// </summary>
        SecondaryMonoThirdPersonObserver = 1000145000
    }
}
