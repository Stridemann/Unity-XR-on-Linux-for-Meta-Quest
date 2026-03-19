#region IterateAllFeaturesExample
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.OpenXR;

namespace UnityEngine.XR.OpenXR.CodeSamples.Editor.Tests
{
    public static class IterateAllFeaturesExample
    {
        [MenuItem("Examples/OpenXR/Iterate All Features")]
        public static void IterateAllFeatures()
        {
            var buildTargetGroup = UnityEditor.BuildTargetGroup.Standalone;
            UnityEditor.XR.OpenXR.Features.FeatureHelpers.RefreshFeatures(
                buildTargetGroup
            );
            var features = OpenXRSettings.Instance.GetFeatures();
            foreach (var feature in features)
            {
                Debug.Log($"Feature: {feature.name}, Enabled: {feature.enabled}");
            }
        }
    }
}
#endif
#endregion
// Used in Documentation~/index.md
// This example demonstrates how to iterate through all features defined in OpenXR for the Standalone build target.
