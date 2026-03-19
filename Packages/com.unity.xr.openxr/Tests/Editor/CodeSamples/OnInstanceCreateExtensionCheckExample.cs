#region OnInstanceCreateExtensionCheckExample
using UnityEngine;
using UnityEngine.XR.OpenXR;
using UnityEngine.XR.OpenXR.Features;

namespace UnityEngine.XR.OpenXR.CodeSamples.Editor.Tests
{
    public class OnInstanceCreateExtensionCheckExample : OpenXRFeature
    {
        protected override bool OnInstanceCreate(ulong xrInstance)
        {
            if (!OpenXRRuntime.IsExtensionEnabled("XR_UNITY_mock_driver"))
            {
                Debug.LogWarning("XR_UNITY_mock_driver is not enabled, " +
                                 "disabling Mock Driver.");
                return false;
            }
            if (OpenXRRuntime.GetExtensionVersion("XR_UNITY_mock_driver") < 100)
                return false;
            return true;
        }
    }
}
#endregion
// Used in Documentation~/features.md
// This example demonstrates how to check for the availability of an OpenXR extension during the instance creation phase.
