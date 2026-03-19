using System;
using System.Collections.Generic;

using UnityEditor;
using UnityEditor.Build;
using UnityEditor.XR.OpenXR;

using UnityEngine;
using UnityEngine.XR.OpenXR;

namespace UnityEditor.XR.OpenXR.Features
{
    internal static class KnownFeatureSets
    {
        internal static Dictionary<BuildTargetGroup, OpenXRFeatureSetManager.FeatureSet[]> k_KnownFeatureSets =
            new Dictionary<BuildTargetGroup, OpenXRFeatureSetManager.FeatureSet[]>() { };
    }
}
