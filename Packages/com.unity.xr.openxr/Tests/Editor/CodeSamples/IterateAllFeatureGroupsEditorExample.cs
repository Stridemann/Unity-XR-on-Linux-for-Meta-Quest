#region IterateAllFeatureGroupsEditorExample
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEditor.XR.OpenXR.Features;
using UnityEngine.XR.OpenXR;

namespace UnityEngine.XR.OpenXR.CodeSamples.Editor.Tests
{
    public static class IterateAllFeatureGroupsEditorExample
    {
        [MenuItem("Examples/OpenXR/Iterate All Feature Groups")]
        public static void IterateAllFeatureGroups()
        {
            var buildTargetGroup = BuildTargetGroup.Standalone;
            var featureSets = OpenXRFeatureSetManager.FeatureSetsForBuildTarget(
                buildTargetGroup
            );
            foreach (var featureSet in featureSets)
            {
                var featureSetId = featureSet.featureSetId;
                Debug.Log($"Feature Set ID: {featureSetId}");
            }
        }
    }
}
#endif
#endregion
// Used in Documentation~/index.md
// This code iterates through all feature groups available in the OpenXR settings for the Standalone build target.
