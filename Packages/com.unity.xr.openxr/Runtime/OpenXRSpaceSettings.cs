using System;
using System.Runtime.InteropServices;

namespace UnityEngine.XR.OpenXR
{
    public partial class OpenXRSettings
    {
        /// <summary>
        /// Activates reference space recentering when using floor-based tracking origin.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When a recentering event is performed, OpenXR will attempt to recenter the world space origin based on the local-floor reference space, if supported by the platform's hardware.
        /// </para>
        /// <para>
        /// If that reference space isn't supported, OpenXR will then attempt to approximate it using stage space or local space.
        /// </para>
        /// <para>
        /// Calling this method won't trigger a recenter event. This event will be sent from the platform's runtime.
        /// </para>
        /// </remarks>
        /// <param name="allowRecentering">Boolean value that activates the recentering feature.</param>
        /// <param name="floorOffset">Estimated height used when approximating the floor-based position when recentering the space. By default, this value is 1.5f</param>
        public static void SetAllowRecentering(bool allowRecentering, float floorOffset = 1.5f)
        {
            Internal_SetAllowRecentering(allowRecentering, floorOffset);
        }

        /// <summary>
        /// Regenerates the internal XR space used for recentering, updating the tracking origin to the most recent floor-based position from the runtime.
        /// </summary>
        /// <remarks>
        /// This method needs that <see cref="AllowRecentering"/> is turned on using <see cref="SetAllowRecentering"/>, otherwise it will do nothing.
        ///
        /// This method doesn't trigger a recenter event, as this event has to be initiated from the platform's runtime.
        ///
        /// See <see cref="SetAllowRecentering"/> for more information.
        /// </remarks>
        public static void RefreshRecenterSpace()
        {
            Internal_RegenerateTrackingOrigin();
        }

        /// <summary>
        /// Returns the current state of the recentering feature.
        /// </summary>
        public static bool AllowRecentering
        {
            get
            {
                return Internal_GetAllowRecentering();
            }
        }

        /// <summary>
        /// Returns the current floor offset value used when approximating the floor-based position when recentering the space.
        /// </summary>
        public static float FloorOffset
        {
            get
            {
                return Internal_GetFloorOffset();
            }
        }

        [DllImport("UnityOpenXR", EntryPoint = "NativeConfig_SetAllowRecentering")]
        private static extern void Internal_SetAllowRecentering([MarshalAs(UnmanagedType.U1)] bool active, float height);

        [DllImport("UnityOpenXR", EntryPoint = "NativeConfig_RegenerateTrackingOrigin")]
        private static extern void Internal_RegenerateTrackingOrigin();

        [DllImport("UnityOpenXR", EntryPoint = "NativeConfig_GetAllowRecentering")]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool Internal_GetAllowRecentering();

        [DllImport("UnityOpenXR", EntryPoint = "NativeConfig_GetFloorOffsetHeight")]
        private static extern float Internal_GetFloorOffset();
    }
}
