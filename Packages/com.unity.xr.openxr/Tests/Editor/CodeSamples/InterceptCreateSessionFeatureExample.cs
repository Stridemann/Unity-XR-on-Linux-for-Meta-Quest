#region InterceptCreateSessionFeatureExample
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.OpenXR;
using UnityEngine.XR.OpenXR.Features;

namespace UnityEngine.XR.OpenXR.CodeSamples.Editor.Tests
{
    public class InterceptCreateSessionFeatureExample : OpenXRFeature
    {
        /// <summary>
        /// The feature id string. Provides a well known id for
        /// referencing the feature.
        /// </summary>
        public const string featureId =
            "com.unity.openxr.feature.example.intercept";
    }
}
#endif
#endregion
// Used in Documentation~/features.md
