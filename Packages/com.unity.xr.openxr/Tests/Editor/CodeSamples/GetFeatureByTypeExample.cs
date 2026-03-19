#region GetFeatureByTypeExample
using UnityEngine;
using UnityEngine.XR.OpenXR;
using UnityEngine.XR.OpenXR.Features.Mock;

namespace UnityEngine.XR.OpenXR.CodeSamples.Editor.Tests
{
    public static class GetFeatureByTypeExample
    {
        [UnityEditor.MenuItem("Examples/OpenXR/Get Feature By Type")]
        public static void GetFeatureByType()
        {
            var feature = OpenXRSettings.Instance.GetFeature<MockRuntime>();
            if (feature != null)
            {
                Debug.Log($"MockRuntime Feature Enabled: {feature.enabled}");
            }
            else
            {
                Debug.LogWarning("MockRuntime feature not found.");
            }
        }
    }
}
#endregion
// Used in Documentation~/index.md
// This example demonstrates how to retrieve a specific feature type from OpenXRSettings.
