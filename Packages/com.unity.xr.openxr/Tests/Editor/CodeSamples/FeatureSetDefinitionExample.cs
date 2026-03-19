#region FeatureSetDefinitionExample
#if UNITY_EDITOR
using UnityEditor.XR.OpenXR.Features;
using UnityEngine;
using UnityEngine.XR.OpenXR;

namespace UnityEngine.XR.OpenXR.CodeSamples.Editor.Tests
{
    [OpenXRFeatureSet(
        FeatureIds = new string[] {
            "EyeGazeInteraction.featureId",
            "KHRSimpleControllerProfile.featureId",
            "com.mycompany.myprovider.mynewfeature",
        },
        UiName = "Feature_Set_Name",
        Description = "Feature group that allows for setting up the best " +
                      "environment for My Company's hardware.",
        FeatureSetId = "com.mycompany.myprovider.mynewfeaturegroup",
        SupportedBuildTargets = new UnityEditor.BuildTargetGroup[] {
            UnityEditor.BuildTargetGroup.Standalone,
            UnityEditor.BuildTargetGroup.Android
        }
    )]
    public class FeatureSetDefinitionExample {}
}
#endif
#endregion
// Used in Documentation~/features.md
// This example shows how to define a feature set in OpenXR, which groups multiple features together for easier management and configuration.
