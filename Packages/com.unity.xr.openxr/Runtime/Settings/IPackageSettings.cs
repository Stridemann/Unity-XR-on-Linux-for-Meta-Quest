#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.XR.OpenXR.Features;

namespace UnityEngine.XR.OpenXR
{
    public interface IPackageSettings
    {
        OpenXRSettings GetSettingsForBuildTargetGroup(BuildTargetGroup buildTargetGroup);
        string GetActiveLoaderLibraryPath();

        /// <summary>
        /// Returns all features of a given type from all existing build target groups.
        /// </summary>
        /// <typeparam name="T">Feature type</typeparam>
        /// <returns>All known features of the given type within the package settings</returns>
        IEnumerable<(BuildTargetGroup buildTargetGroup, T feature)> GetFeatures<T>() where T : OpenXRFeature;

        internal void RefreshFeatureSets();

        internal string PackageSettingsAssetPath();
    }

    interface IPackageSettings2 : IPackageSettings
    {
        internal void OverrideSettingsLocatorFunc(Func<BuildTargetGroup, OpenXRSettings> locatorFunc);

        internal void RestoreDefaultSettingsLocatorFunc();

        internal bool IsSettingsLocatorFuncOverriden();

    }
}
#endif // UNITY_EDITOR
