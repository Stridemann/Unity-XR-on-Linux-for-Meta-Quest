#if UNITY_EDITOR
using UnityEditor;

namespace UnityEngine.XR.OpenXR
{
    static class PackageSettingsLocator
    {
        internal static IPackageSettings2 GetPackageSettings()
        {
            if (EditorBuildSettings.TryGetConfigObject<Object>(Constants.k_SettingsKey, out var obj)
                && obj is IPackageSettings2 packageSettings)
                return packageSettings;

            return null;
        }
    }
}
#endif // UNITY_EDITOR
