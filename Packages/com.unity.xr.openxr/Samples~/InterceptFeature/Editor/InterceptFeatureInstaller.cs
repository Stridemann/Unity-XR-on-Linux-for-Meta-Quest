using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.OpenXR;
using UnityEditor.XR.OpenXR.Features;
using UnityEngine.XR.OpenXR.Samples.InterceptFeature;

namespace UnityEditor.XR.OpenXR.Samples.InterceptFeature
{
    /// <summary>
    /// Automatically enables the InterceptCreateSessionFeature feature and then self-destructs
    /// </summary>
    [InitializeOnLoad]
    public class InterceptFeatureInstaller
    {
#if !UNITY_SAMPLE_DEV
        private const string k_SamplePath = "Intercept Feature/Editor/InterceptFeatureInstaller.cs";

        static InterceptFeatureInstaller()
        {
            EditorApplication.update += Install;
        }

        private static void Install()
        {
            EditorApplication.update -= Install;

            // Automatically enable the feature
            FeatureHelpers.RefreshFeatures(BuildTargetGroup.Standalone);
            FeatureHelpers.RefreshFeatures(BuildTargetGroup.WSA);
            FeatureHelpers.RefreshFeatures(BuildTargetGroup.Android);

            var settings = OpenXRSettings.GetSettingsForBuildTargetGroup(BuildTargetGroup.Standalone);
            foreach (var feature in settings.GetFeatures<InterceptCreateSessionFeature>())
                feature.enabled = true;

            settings = OpenXRSettings.GetSettingsForBuildTargetGroup(BuildTargetGroup.WSA);
            foreach (var feature in settings.GetFeatures<InterceptCreateSessionFeature>())
                feature.enabled = true;

            settings = OpenXRSettings.GetSettingsForBuildTargetGroup(BuildTargetGroup.Android);
            foreach (var feature in settings.GetFeatures<InterceptCreateSessionFeature>())
                feature.enabled = true;

            // Find this script in the asset database
            var source = AssetDatabase
                .FindAssets(Path.GetFileNameWithoutExtension(k_SamplePath))
                .Select(AssetDatabase.GUIDToAssetPath)
                .FirstOrDefault(r => r.Contains(k_SamplePath));

            // Self destruct
            if (!string.IsNullOrEmpty(source))
                AssetDatabase.DeleteAsset(source);
        }

#endif
    }
}
