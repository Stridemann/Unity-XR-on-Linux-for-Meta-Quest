using NUnit.Framework;
using System.Runtime.InteropServices;

namespace UnityEngine.XR.OpenXR.Tests
{
    /// <summary>
    /// Custom yield instruction that waits for the tracking origin to regenerate.
    /// </summary>
    internal sealed class WaitForTrackingOriginRegeneration : CustomYieldInstruction
    {
        [DllImport("UnityOpenXR", EntryPoint = "unity_ext_GetRegenerateTrackingOriginFlag")]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool GetRegenerateTrackingOriginFlag();

        private float m_Timeout = 0;

        public WaitForTrackingOriginRegeneration(float timeout = 5.0f)
        {
            m_Timeout = Time.realtimeSinceStartup + timeout;
        }

        public override bool keepWaiting
        {
            get
            {
                if (!GetRegenerateTrackingOriginFlag())
                {
                    return false;
                }

                // Did we time out waiting?
                if (Time.realtimeSinceStartup > m_Timeout)
                {
                    Assert.Fail("WaitForTrackingOriginRegeneration: Timeout");
                    return false;
                }

                return true;
            }
        }
    }
}
