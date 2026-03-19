using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.XR.OpenXR;
using static UnityEditor.XR.OpenXR.Features.FeatureHelpersInternal;

namespace UnityEditor.XR.OpenXR.Features
{
    internal class OpenXRChooseRuntimeLibraries : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        private const string K_openXrLoaderLibName = "openxr_loader";

        public static string DefineEditorLoaderLibraryPath()
        {
            var allFeatureInfo = FeatureHelpersInternal.GetAllFeatureInfo(BuildTargetGroup.Standalone);
            var editorLoaderImporters = PluginImporter.GetAllImporters()
#if UNITY_EDITOR_WIN
                .Where(importer => importer.GetCompatibleWithPlatform(BuildTarget.StandaloneWindows64) && importer.assetPath.EndsWith(".dll"))
#elif UNITY_EDITOR_OSX
                .Where(importer => importer.GetCompatibleWithPlatform(BuildTarget.StandaloneOSX) && importer.assetPath.EndsWith(".dylib"))
#endif
                ;

            Debug.Assert(editorLoaderImporters.Any(), "No Editor-compatible OpenXR loader library found");

            var openXrSettings = OpenXRSettings.Instance;
            IEnumerable<PluginImporter> loaderImporters = null;
            if (allFeatureInfo.ActiveCustomLoaderFeature.HasValue)
            {
                var activeCustomLoaderFeature = allFeatureInfo.ActiveCustomLoaderFeature.Value;
                loaderImporters = FindImportersForLoader(editorLoaderImporters, activeCustomLoaderFeature);
            }
            else
            {
                loaderImporters = FindDefaultOpenXRLoaderImporters(editorLoaderImporters, allFeatureInfo.Features);
            }
            return AssetPathToAbsolutePath(loaderImporters.First().assetPath);
        }

        private static string AssetPathToAbsolutePath(string assetPath)
        {
            var path = assetPath.Replace('/', Path.DirectorySeparatorChar);
            if (assetPath.StartsWith("Packages"))
            {
                path = String.Join("" + Path.DirectorySeparatorChar, path.Split(Path.DirectorySeparatorChar).Skip(2));

                return Path.Combine(PackageManager.PackageInfo.FindForAssetPath(assetPath).resolvedPath, path);
            }

            return path;
        }

        public void OnPreprocessBuild(BuildReport report)
        {
            var openXRLoaderActive = BuildHelperUtils.HasActiveLoader(report.summary.platformGroup, typeof(OpenXRLoaderBase));

            var extensions = FeatureHelpersInternal.GetAllFeatureInfo(report.summary.platformGroup);

            // Keep set of seen plugins, only disable plugins that haven't been seen.
            HashSet<string> seenPlugins = new HashSet<string>();

            // Acquire native plugin importers enabled for the active build.
            var buildImporters = PluginImporter.GetAllImporters()
                .Where(importer => importer.GetCompatibleWithPlatform(report.summary.platform));

            // By default, plugins with a name containing "openxr_library" are considered loaders
            var defaultOpenXrPluginImporters = buildImporters
                .Where(importer => importer.assetPath.Contains(K_openXrLoaderLibName));

            // Ensure we also remove custom loaders with custom name, only if these exist in the same path as their feature
            var customLoaderPathNameTuples = extensions.Features
                .Where(feature => !string.IsNullOrWhiteSpace(feature.CustomLoaderName))
                .Select(feature => new Tuple<string, string>(feature.PluginPath, feature.CustomLoaderName))
                .ToList();
            var otherLoaderImporters = buildImporters
                .Where(importer => customLoaderPathNameTuples
                    .Where(tuple => Path.GetDirectoryName(importer.assetPath).Contains(tuple.Item1) && importer.assetPath.Contains(tuple.Item2))
                    .Any());

            var allOpenXrPluginImporters = defaultOpenXrPluginImporters.Concat(otherLoaderImporters);

            // By default, disable all OpenXR Loader plugins from auto including in build.
            // We do this to prevent custom loaders from conflicting with the default OpenXR loader.
            RemoveLoadersFromBuild(allOpenXrPluginImporters);

            if (openXRLoaderActive)
            {
                EnableOpenXRLoaderImportersInBuild(allOpenXrPluginImporters, extensions);
            }

            foreach (var importer in buildImporters)
            {
                if (allOpenXrPluginImporters.Contains(importer))
                {
                    // Ignore OpenXR loaders (including those with custom name), these were processed above
                    continue;
                }

                if (importer.assetPath.Contains("UnityOpenXR"))
                {
                    importer.SetIncludeInBuildDelegate(path => openXRLoaderActive);
                }

                var root = Path.GetDirectoryName(importer.assetPath);
                foreach (var extInfo in extensions.Features)
                {
                    if (root != null && root.Contains(extInfo.PluginPath))
                    {
                        if (extInfo.Feature.enabled)
                        {
                            importer.SetIncludeInBuildDelegate(path => openXRLoaderActive);
                        }
                        else if (!seenPlugins.Contains(importer.assetPath))
                        {
                            importer.SetIncludeInBuildDelegate(path => false);
                        }
                        seenPlugins.Add(importer.assetPath);
                    }
                }
            }
        }

        [InitializeOnLoadMethod]
        static void InitializeOnLoad()
        {
            var importers = PluginImporter.GetAllImporters();

            // fixes asset bundle building since IPreProcessBuildWithReport isn't called
            foreach (var importer in importers)
            {
                if (importer.assetPath.Contains(K_openXrLoaderLibName))
                {
                    importer.SetIncludeInBuildDelegate(path => false);
                }
            }
        }

        private void EnableOpenXRLoaderImportersInBuild(
            IEnumerable<PluginImporter> allOpenXRImporters,
            AllFeatureInfo allFeatureInfo)
        {
            IEnumerable<PluginImporter> loaderImporters;
            if (allFeatureInfo.ActiveCustomLoaderFeature.HasValue)
            {
                loaderImporters = FindImportersForLoader(allOpenXRImporters, allFeatureInfo.ActiveCustomLoaderFeature.Value);
            }
            else
            {
                loaderImporters = FindDefaultOpenXRLoaderImporters(allOpenXRImporters, allFeatureInfo.Features);
            }

            foreach (var loader in loaderImporters)
            {
                // Include all plugins from features that override multiple architectures (ie Android arm64 and x64)
                loader.SetIncludeInBuildDelegate(path => true);
            }
        }

        private static IEnumerable<PluginImporter> FindDefaultOpenXRLoaderImporters(
            IEnumerable<PluginImporter> allPluginImporters,
            IEnumerable<FeatureHelpersInternal.FeatureInfo> customLoaderFeatures)
        {
            var importers = allPluginImporters
                .Where(importer => importer.assetPath.Contains(K_openXrLoaderLibName))
                .Where(importer => !customLoaderFeatures // Reject any importer that belongs to a feature
                    .Select(feature => feature.PluginPath)
                    .Where(path =>
                    {
                        return Path.GetDirectoryName(importer.assetPath).Contains(path);
                    })
                    .Any());

            if (!importers.Any())
            {
                throw new BuildFailedException("No OpenXR loader library found. Make sure the Unity OpenXR plug-in package is installed properly.");
            }

            return importers;
        }

        static IEnumerable<PluginImporter> FindImportersForLoader(IEnumerable<PluginImporter> allPluginImporters, FeatureInfo customLoaderFeature)
        {
            var loaderLibName = !string.IsNullOrWhiteSpace(customLoaderFeature.CustomLoaderName) ? customLoaderFeature.CustomLoaderName : K_openXrLoaderLibName;
            var importers = allPluginImporters
                .Where(importer => Path.GetDirectoryName(importer.assetPath).Contains(customLoaderFeature.PluginPath) && importer.assetPath.Contains(loaderLibName));

            if (!importers.Any())
            {
                throw new Exception(
                    $"Build process couldn't find the Plugin Importer for the OpenXR Loader library of the feature \"{customLoaderFeature.Attribute.UiName}\", " +
                    $"the file was expected to be in path: {customLoaderFeature.PluginPath}");
            }

            return importers;
        }

        void RemoveLoadersFromBuild(IEnumerable<PluginImporter> importers)
        {
            foreach (var pluginImporter in importers)
            {
                pluginImporter.SetIncludeInBuildDelegate(path => false);
            }
        }
    }
}
