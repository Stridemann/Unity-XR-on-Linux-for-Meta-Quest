using System;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Space Location bit flags. <see cref="Features.Mock.MockRuntime.SetSpace"/>
    /// </summary>
    [Flags]
    public enum XrSpaceLocationFlags
    {
        /// <summary>
        /// Default space location flag.
        /// </summary>
        None = 0,

        /// <summary>
        /// Orientation Valid bit.
        /// </summary>
        OrientationValid = 1,

        /// <summary>
        /// Position Valid bit.
        /// </summary>
        PositionValid = 2,

        /// <summary>
        /// Orientation Tracked bit.
        /// </summary>
        OrientationTracked = 4,

        /// <summary>
        /// Position Tracked bit.
        /// </summary>
        PositionTracked = 8
    }
}
