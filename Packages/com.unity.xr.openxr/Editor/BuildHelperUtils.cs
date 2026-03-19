using System.Linq;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.XR.Management;
using UnityEngine.XR.Management;
using UnityEngine.XR.OpenXR;

namespace UnityEditor.XR.OpenXR
{
    [InitializeOnLoad]
    internal class BuildHelperUtils : IPreprocessBuildWithReport
    {
        public static bool HasActiveLoader(BuildTargetGroup targetGroup, System.Type loader)
        {
            var settings = XRGeneralSettingsPerBuildTarget.XRGeneralSettingsForBuildTarget(targetGroup);

            if (settings)
            {
                return settings.Manager.activeLoaders.Any(loader.IsInstanceOfType);
            }

            return false;
        }

        public int callbackOrder => -100;

        public void OnPreprocessBuild(BuildReport report)
        {
            MakeSureXRGeneralSettingsExists(report.summary.platformGroup);
        }

        public static XRGeneralSettings MakeSureXRGeneralSettingsExists(BuildTargetGroup targetGroup)
        {
            // If we don't have XRGeneralSettings in EditorBuildSettings, check if we have one in the project and set it.
            var settings = XRGeneralSettingsPerBuildTarget.XRGeneralSettingsForBuildTarget(targetGroup);
            if (!settings)
            {
                string searchText = "t:XRGeneralSettings";
                string[] assets = AssetDatabase.FindAssets(searchText);
                for (int i = 0; i < assets.Length; ++i)
                {
                    string path = AssetDatabase.GUIDToAssetPath(assets[i]);
                    var allSettings = AssetDatabase.LoadAssetAtPath(path, typeof(XRGeneralSettingsPerBuildTarget)) as XRGeneralSettingsPerBuildTarget;
                    if (allSettings != null)
                    {
                        EditorBuildSettings.AddConfigObject(XRGeneralSettings.k_SettingsKey, allSettings, true);
                        break;
                    }
                }
            }

            return settings;
        }
    }
}
