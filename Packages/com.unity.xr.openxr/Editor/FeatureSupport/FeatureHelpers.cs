using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.XR.OpenXR.Features;
using UnityEngine;
using UnityEngine.XR.OpenXR;
using Object = UnityEngine.Object;

[assembly: InternalsVisibleTo("Unity.XR.OpenXR.Editor.Tests")]
[assembly: InternalsVisibleTo("Unity.XR.OpenXR.Tests")]
[assembly: InternalsVisibleTo("Unity.XR.OpenXR.TestTooling")]
namespace UnityEditor.XR.OpenXR.Features
{
    /// <summary>
    /// Editor OpenXR Feature helpers.
    /// </summary>
    public static class FeatureHelpers
    {
        /// <summary>
        /// Discovers all features in project and ensures that OpenXRSettings.Instance.features is up to date
        /// for selected build target group.
        /// </summary>
        /// <param name="group">build target group to refresh</param>
        public static void RefreshFeatures(BuildTargetGroup group)
        {
            FeatureHelpersInternal.GetAllFeatureInfo(group);
        }

        /// <summary>
        /// Given a feature id, returns the first instance of <see cref="OpenXRFeature" /> associated with that id.
        /// </summary>
        /// <param name="featureId">The unique id identifying the feature</param>
        /// <returns>The instance of the feature matching thd id, or null.</returns>
        public static OpenXRFeature GetFeatureWithIdForActiveBuildTarget(string featureId)
        {
            return GetFeatureWithIdForBuildTarget(BuildPipeline.GetBuildTargetGroup(EditorUserBuildSettings.activeBuildTarget), featureId);
        }

        /// <summary>
        /// Given an array of feature ids, returns an array of matching <see cref="OpenXRFeature" /> instances.
        /// </summary>
        /// <param name="featureIds">Array of feature ids to match against.</param>
        /// <returns>An array of all matching features.</returns>
        public static OpenXRFeature[] GetFeaturesWithIdsForActiveBuildTarget(string[] featureIds)
        {
            return GetFeaturesWithIdsForBuildTarget(BuildPipeline.GetBuildTargetGroup(EditorUserBuildSettings.activeBuildTarget), featureIds);
        }

        /// <summary>
        /// Given a feature id, returns the first <see cref="OpenXRFeature" /> associated with that id.
        /// </summary>
        /// <param name="buildTargetGroup">The build target group to get the feature from.</param>
        /// <param name="featureId">The unique id identifying the feature</param>
        /// <returns>The instance of the feature matching thd id, or null.</returns>
        public static OpenXRFeature GetFeatureWithIdForBuildTarget(BuildTargetGroup buildTargetGroup, string featureId)
        {
            if (string.IsNullOrEmpty(featureId))
                return null;

            var settings = OpenXRSettings.GetSettingsForBuildTargetGroup(buildTargetGroup);
            if (settings == null)
                return null;

            foreach (var feature in settings.features)
            {
                if (string.Compare(featureId, feature.featureIdInternal, true) == 0)
                    return feature;
            }

            return null;
        }

        /// <summary>
        /// Given an array of feature ids, returns an array of matching <see cref="OpenXRFeature" /> instances that match.
        /// </summary>
        /// <param name="buildTargetGroup">The build target group to get the feature from.</param>
        /// <param name="featureIds">Array of feature ids to match against.</param>
        /// <returns>An array of all matching features.</returns>
        public static OpenXRFeature[] GetFeaturesWithIdsForBuildTarget(BuildTargetGroup buildTargetGroup, string[] featureIds)
        {
            List<OpenXRFeature> ret = new List<OpenXRFeature>();

            if (featureIds == null || featureIds.Length == 0)
                return ret.ToArray();

            foreach (var featureId in featureIds)
            {
                var feature = GetFeatureWithIdForBuildTarget(buildTargetGroup, featureId);
                if (feature != null)
                    ret.Add(feature);
            }

            return ret.ToArray();
        }
    }

    static class FeatureHelpersInternal
    {
        public class AllFeatureInfo
        {
            public List<FeatureInfo> Features;
            public FeatureInfo? ActiveCustomLoaderFeature;
        }

        public enum FeatureInfoCategory
        {
            Feature,
            Interaction
        }

        public struct FeatureInfo
        {
            public string PluginPath;
            public OpenXRFeatureAttribute Attribute;
            public OpenXRFeature Feature;
            public FeatureInfoCategory Category;
            public OpenXRApiVersion LoaderVersion;
            public bool HasLoaderForBuildTarget;
            public string CustomLoaderName;
        }

        static FeatureInfoCategory DetermineFeatureCategory(string featureCategoryString)
        {
            return string.Compare(featureCategoryString, FeatureCategory.Interaction) == 0
                ? FeatureInfoCategory.Interaction
                : FeatureInfoCategory.Feature;
        }

        /// <summary>
        /// Gets all features for group. If serialized feature instances do not exist, creates them.
        /// </summary>
        /// <param name="group">BuildTargetGroup to get feature information for.</param>
        /// <returns>feature info</returns>
        public static AllFeatureInfo GetAllFeatureInfo(BuildTargetGroup group)
        {
            AllFeatureInfo ret = new()
            {
                Features = new List<FeatureInfo>(),
                ActiveCustomLoaderFeature = null
            };
            var openXrPackageSettings = OpenXRPackageSettings.GetOrCreateInstance();
            var isOpenXrSettingsAMockInstance = ((IPackageSettings2)openXrPackageSettings).IsSettingsLocatorFuncOverriden();
            var openXrSettings = openXrPackageSettings.GetSettingsForBuildTargetGroup(group);
            if (openXrSettings == null)
                return ret;

            // Find any OpenXRFeatures that are already serialized
            IEnumerable<Object> featureAssets = isOpenXrSettingsAMockInstance
                ? openXrSettings.features
                : GetPackageSettingsFeatureAssets(openXrPackageSettings);

            var featuresOnDisk = new Dictionary<OpenXRFeatureAttribute, OpenXRFeature>();
            string buildGroupName = isOpenXrSettingsAMockInstance ? "MockRuntime" : group.ToString();
            foreach (var featureAsset in featureAssets)
            {
                if (featureAsset == null || !featureAsset.name.Contains(buildGroupName))
                    continue;

                foreach (var attr in Attribute.GetCustomAttributes(featureAsset.GetType()))
                {
                    if (attr is OpenXRFeatureAttribute featureAttr)
                    {
                        featuresOnDisk[featureAttr] = (OpenXRFeature)featureAsset;
                        break;
                    }
                }
            }

            // Find any features that haven't yet been added to the feature list and create instances of them
            var all = new List<OpenXRFeature>();
            var mockRuntimeIsAlreadyInitialized = isOpenXrSettingsAMockInstance && featuresOnDisk.Any();
            foreach (var featureType in TypeCache.GetTypesWithAttribute<OpenXRFeatureAttribute>())
            {
                foreach (Attribute attr in Attribute.GetCustomAttributes(featureType))
                {
                    if (attr is not OpenXRFeatureAttribute featureAttr)
                        continue;

                    if (featureAttr.BuildTargetGroups != null
                        && !((IList)featureAttr.BuildTargetGroups).Contains(group))
                        break;

                    if (!featuresOnDisk.TryGetValue(featureAttr, out var featureAsset))
                    {
                        // Create a new one
                        featureAsset = (OpenXRFeature)ScriptableObject.CreateInstance(featureType);
                        featureAsset.name = featureType.Name + " " + buildGroupName;
                        AssetDatabase.AddObjectToAsset(featureAsset, openXrSettings);
                        AssetDatabase.SaveAssets();
                    }
                    else
                    {
                        // This line of code is required for changes to interaction profiles to be saved
                        // when closing and re-opening the Editor.
                        featureAsset.name = featureType.Name + " " + buildGroupName;
                    }

                    if (featureAsset == null)
                        break;

                    var ms = MonoScript.FromScriptableObject(featureAsset);
                    var path = AssetDatabase.GetAssetPath(ms);
                    var dir = "";
                    if (!string.IsNullOrEmpty(path))
                        dir = Path.GetDirectoryName(path);

                    var featureInfo = new FeatureInfo
                    {
                        PluginPath = dir,
                        Attribute = featureAttr,
                        Feature = featureAsset,
                        Category = DetermineFeatureCategory(featureAttr.Category)
                    };

                    if (featureAttr.CustomRuntimeLoaderBuildTargets?.Length > 0)
                    {
                        featureInfo.HasLoaderForBuildTarget = featureAttr.CustomRuntimeLoaderBuildTargets
                            .Select(BuildPipeline.GetBuildTargetGroup)
                            .Any(targetGroup => targetGroup == group);

                        if (featureInfo.HasLoaderForBuildTarget)
                        {
                            featureInfo.LoaderVersion =
                                OpenXRApiVersion.TryParse(featureAttr.CustomRuntimeLoaderVersion, out var version)
                                    ? version
                                    : null;
                            featureInfo.CustomLoaderName = featureAttr.CustomRuntimeLoaderName;
                        }
                    }

                    ret.Features.Add(featureInfo);

                    if (!mockRuntimeIsAlreadyInitialized)
                        all.Add(featureAsset);

                    break;
                }
            }

            if (!mockRuntimeIsAlreadyInitialized)
            {
                // Update the feature list
                var originalFeatures = openXrSettings.features;
                var newFeatures = all
                    .Where(f => f != null)
                    .OrderByDescending(f => f.priority)
                    .ThenBy(f => f.nameUi)
                    .ToArray();

                // Populate the internal feature variables for all features
                bool fieldChanged = false;
                foreach (var feature in newFeatures)
                {
                    if (feature.internalFieldsUpdated)
                        continue;

                    feature.internalFieldsUpdated = true;
                    foreach (var attr in feature.GetType().GetCustomAttributes<OpenXRFeatureAttribute>())
                    {
                        foreach (var sourceField in attr.GetType().GetFields())
                        {
                            var copyField = sourceField.GetCustomAttribute<OpenXRFeatureAttribute.CopyFieldAttribute>();
                            if (copyField == null)
                                continue;

                            var targetField = feature.GetType().GetField(
                                copyField.FieldName,
                                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                            if (targetField == null)
                                continue;

                            // Only set value if value is different
                            if (targetField.GetValue(feature) == null
                                || !targetField.GetValue(feature).Equals(sourceField.GetValue(attr)))
                            {
                                targetField.SetValue(feature, sourceField.GetValue(attr));
                                fieldChanged = true;
                            }
                        }
                    }
                }

                // Ensure the settings are saved after the features are populated
                if (fieldChanged || originalFeatures == null || !originalFeatures.SequenceEqual(newFeatures))
                {
                    openXrSettings.features = newFeatures;
                    EditorUtility.SetDirty(openXrSettings);
                }
            }

            // Decide which loader to use and API version to request
            if (TryFindCustomLoaderWithHighestPriority(ret.Features, out var customLoader))
                ret.ActiveCustomLoaderFeature = customLoader;

            // Save custom loader name, if any
            if (ret.ActiveCustomLoaderFeature.HasValue &&
                !string.IsNullOrWhiteSpace(ret.ActiveCustomLoaderFeature.Value.CustomLoaderName))
            {
                openXrSettings.customLoaderName = ret.ActiveCustomLoaderFeature.Value.CustomLoaderName;
                EditorUtility.SetDirty(openXrSettings);
            }
            else if (!string.IsNullOrEmpty(openXrSettings.customLoaderName))
            {
                // Clear active custom loader feature name
                openXrSettings.customLoaderName = string.Empty;
                EditorUtility.SetDirty(openXrSettings);
            }

            if (EditorUtility.IsDirty(openXrSettings))
                AssetDatabase.SaveAssetIfDirty(openXrSettings);

            return ret;
        }

        static IEnumerable<Object> GetPackageSettingsFeatureAssets(OpenXRPackageSettings openXrPackageSettings)
        {
            var assetPath = Path.Combine(OpenXRPackageSettings.GetAssetPathForComponents(
                OpenXRPackageSettings.s_PackageSettingsDefaultSettingsPath), openXrPackageSettings.name + ".asset");
            var featureAssets = AssetDatabase.LoadAllAssetsAtPath(assetPath);

            if (featureAssets == null || featureAssets.Length == 0)
            {
                string[] guids = AssetDatabase.FindAssets("t:OpenXRSettings");

                foreach (string guid in guids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    var packageSettingsAssets = AssetDatabase.LoadAllAssetsAtPath(path);

                    if (packageSettingsAssets.Any(obj => obj != null && obj.name == openXrPackageSettings.name))
                    {
                        return packageSettingsAssets;
                    }
                }
            }

            return featureAssets;
        }

        internal static bool TryFindCustomLoaderWithHighestPriority(
            IEnumerable<FeatureInfo> features, out FeatureInfo loaderFeatureInfo)
        {
            var activeCustomLoaderFeatures = features
                .Where(feature => feature.Feature.enabled && feature.HasLoaderForBuildTarget);

            if (!activeCustomLoaderFeatures.Any())
            {
                loaderFeatureInfo = default;
                return false;
            }

            if (TryGetForcedLoaderOverride(activeCustomLoaderFeatures, out var loader))
            {
                // Using the forced custom loader override, when a feature doesn't specify an API version
                var versionString = loader.LoaderVersion?.ToString() ?? "null";
                Debug.Log($"Using forced custom loader override provided by the OpenXR Feature {loader.Feature.nameUi}, with version {versionString}");
                loaderFeatureInfo = loader;
                return true;
            }

            // Find loader with highest version
            var loaderFeatureWithHighestApiVersion = activeCustomLoaderFeatures
                .Where(feature => feature.LoaderVersion > OpenXRApiVersion.Current); // Ignore custom loaders with lower version than default loader

            if (loaderFeatureWithHighestApiVersion.Any())
            {
                // Pick loader with highest version and highest Feature order priority
                loaderFeatureInfo = loaderFeatureWithHighestApiVersion
                    .GroupBy(feature => feature.LoaderVersion)
                    .OrderByDescending(group => group.First().LoaderVersion)
                    .First()
                    .OrderByDescending(feature => feature.Attribute.Priority)
                    .First();
                return true;
            }

            // Return default loader
            loaderFeatureInfo = default;
            return false;
        }

        static bool TryGetForcedLoaderOverride(
            IEnumerable<FeatureInfo> customLoaderFeatures, out FeatureInfo overrideLoaderFeature)
        {
            var overrideLoaderFeatures = customLoaderFeatures
                .Where(feature => feature.LoaderVersion == null);
            if (overrideLoaderFeatures.Count() > 1)
            {
                Debug.LogError(
                    "Only one OpenXR feature may force a custom runtime loader override per platform." +
                    "Verify that only one of the following extensions doesn't specify a custom loader OpenXR API version:" +
                    $"{string.Join(",", overrideLoaderFeatures.Select(features => features.Attribute.UiName))}.");
            }

            if (overrideLoaderFeatures.Any())
            {
                overrideLoaderFeature = overrideLoaderFeatures.First();
                return true;
            }

            overrideLoaderFeature = default;
            return false;
        }
    }
}
