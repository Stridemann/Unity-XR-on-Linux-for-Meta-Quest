#if UNITY_6000_0_OR_NEWER
// The FoveatedRendering API is only available in Unity 6.0.0 or newer.
#region FoveationSrpApiExample
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace UnityEngine.XR.OpenXR.CodeSamples.Editor.Tests
{
    // Requires Unity 6.0.0 or newer.
    public class FoveationStarter : MonoBehaviour
    {
        List<XRDisplaySubsystem> xrDisplays = new List<XRDisplaySubsystem>();

        void Start()
        {
            SubsystemManager.GetSubsystems(xrDisplays);
            if (xrDisplays.Count == 1)
            {
                xrDisplays[0].foveatedRenderingLevel = 1.0f; // Full strength
                xrDisplays[0].foveatedRenderingFlags =
                    XRDisplaySubsystem.FoveatedRenderingFlags.GazeAllowed;
            }
        }
    }
}
#endregion
#endif
// Used in Documentation~/features/foveatedrendering.md
// This example demonstrates how to enable foveated rendering using the SRP Foveation API.
