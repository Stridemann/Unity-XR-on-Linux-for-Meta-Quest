#region BuildTimeValidationExample
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.XR.OpenXR;
using UnityEngine.XR.OpenXR.Features;

namespace UnityEngine.XR.OpenXR.CodeSamples.Editor.Tests
{
#if UNITY_EDITOR
    public class BuildTimeValidationExample : OpenXRFeature
    {
        protected override void GetValidationChecks(
            List<OpenXRFeature.ValidationRule> results,
            BuildTargetGroup targetGroup)
        {
            if (targetGroup == BuildTargetGroup.WSA)
            {
                results.Add(new ValidationRule(this)
                {
                    message = "Eye Gaze requires the Gaze Input capability.",
                    error = false,
                    checkPredicate = () =>
                        PlayerSettings.WSA.GetCapability(
                            PlayerSettings.WSACapability.GazeInput
                        ),
                    fixIt = () =>
                        PlayerSettings.WSA.SetCapability(
                            PlayerSettings.WSACapability.GazeInput,
                            true
                        )
                });
            }
        }
    }
#endif
}
#endregion
// Used in Documentation~/features.md
// This example demonstrates how to add build time validation checks in an OpenXRFeature.
