using System;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Runtime view state flags. <see cref="Features.Mock.MockRuntime.SetViewState"/>
    /// </summary>
    [Flags]
    public enum XrViewStateFlags
    {
        /// <summary>
        /// Default view state flag.
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
        /// Oriention Tracked bit.
        /// </summary>
        OrientationTracked = 4,

        /// <summary>
        /// Position Tracked bit.
        /// </summary>
        PositionTracked = 8
    }
}
