namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Runtime XR Session State. <see cref="Features.Mock.MockRuntime.TransitionToState"/>
    /// </summary>
    public enum XrSessionState
    {
        /// <summary>
        /// Session State Unknown.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Session State Idle.
        /// </summary>
        Idle = 1,

        /// <summary>
        /// Session State Ready.
        /// </summary>
        Ready = 2,

        /// <summary>
        /// Session State Synchronized.
        /// </summary>
        Synchronized = 3,

        /// <summary>
        /// Session State Visible.
        /// </summary>
        Visible = 4,

        /// <summary>
        /// Session State Focused.
        /// </summary>
        Focused = 5,

        /// <summary>
        /// Session State Stopping.
        /// </summary>
        Stopping = 6,

        /// <summary>
        /// Session State Loss Pending.
        /// </summary>
        LossPending = 7,

        /// <summary>
        /// Session State Exiting.
        /// </summary>
        Exiting = 8,
    }
}
