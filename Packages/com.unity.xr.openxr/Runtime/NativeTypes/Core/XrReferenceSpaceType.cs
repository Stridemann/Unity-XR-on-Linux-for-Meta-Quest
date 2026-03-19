using System;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Runtime XR Reference Space. <see cref="Features.Mock.MockRuntime.SetSpace"/>
    /// </summary>
    [Flags]
    public enum XrReferenceSpaceType
    {
        /// <summary>
        /// View space.
        /// </summary>
        View = 1,

        /// <summary>
        /// Local space.
        /// </summary>
        Local = 2,

        /// <summary>
        /// Stage space.
        /// </summary>
        Stage = 3,

        /// <summary>
        /// Unbounded (Microsoft specific).
        /// </summary>
        UnboundedMsft = 1000038000,

        /// <summary>
        /// Combined Eye (Varjo specific).
        /// </summary>
        CombinedEyeVarjo = 1000121000
    }
}
