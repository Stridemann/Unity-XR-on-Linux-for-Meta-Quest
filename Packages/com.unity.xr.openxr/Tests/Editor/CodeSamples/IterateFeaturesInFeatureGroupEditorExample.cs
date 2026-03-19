#region IterateFeaturesInFeatureGroupEditorExample
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEditor.XR.OpenXR.Features;
using UnityEngine.XR.OpenXR;

namespace UnityEngine.XR.OpenXR.CodeSamples.Editor.Tests
{
    public static class IterateFeaturesInFeatureGroupEditorExample
    {
        [MenuItem("Examples/OpenXR/Iterate Features In Feature Group")]
        public static void IterateFeaturesInFeatureGroup()
        {
            // Example id, update as needed
            string featureSetId = "com.mycompany.myprovider.mynewfeaturegroup";
            var buildTargetGroup = BuildTargetGroup.Standalone;
            var featureSet =
                OpenXRFeatureSetManager.GetFeatureSetWithId(
                    buildTargetGroup,
                    featureSetId
            );
            if (featureSet != null)
            {
                var features =
                    FeatureHelpers.GetFeaturesWithIdsForActiveBuildTarget(
                        featureSet.featureIds
                    );
                foreach (var feature in features)
                {
                    Debug.Log($"Feature: {feature.name}");
                }
            }
            else
            {
                Debug.LogWarning($"Feature set with ID {featureSetId} not found.");
            }
        }
    }
}
#endif
#endregion
// Used in Documentation~/index.md
