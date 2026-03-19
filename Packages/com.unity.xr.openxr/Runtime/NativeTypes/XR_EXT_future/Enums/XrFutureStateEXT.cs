namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Represents the possible states of a future.
    /// </summary>
    public enum XrFutureStateEXT
    {
        /// <summary>
        /// The state of a future that is waiting for the async operation to conclude.
        /// This is typically the initial state of a future returned from an async function.
        /// </summary>
        Pending = 1,

        /// <summary>
        /// The state of a future when the result of the async operation is ready.
        /// You can retrieve the result by calling the associated completion function.
        /// </summary>
        Ready = 2
    }
}
