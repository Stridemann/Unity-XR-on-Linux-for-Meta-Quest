using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityEngine.XR.OpenXR
{
    /// <summary>
    /// Build time settings for OpenXR. These are serialized and available at runtime.
    /// </summary>
    [Serializable]
    public partial class OpenXRSettings : ScriptableObject
    {
#if UNITY_EDITOR
        internal bool versionChanged = false;
#else
        static OpenXRSettings s_RuntimeInstance = null;

        void Awake()
        {
            s_RuntimeInstance = this;
        }

#endif
        internal void ApplySettings(bool logSettings = false)
        {
            if (logSettings)
            {
                var diagnosticsSectionHandle = DiagnosticReport.GetSection("OpenXR Settings");
                DiagnosticReport.AddSectionBreak(diagnosticsSectionHandle);
                LogRendererSettings(diagnosticsSectionHandle);
            }
            ApplyRenderSettings();
            ApplyPermissionSettings();
        }

#if UNITY_EDITOR
        void OnValidate()
        {
            if (Application.isPlaying)
                ApplySettings();
        }
#endif

        static OpenXRSettings GetInstance(bool useActiveBuildTarget)
        {
            // When running in the Unity Editor, we have to load user's customization of configuration data directly from
            // EditorBuildSettings. At runtime, we need to grab it from the static instance field instead.
#if UNITY_EDITOR
            var settings = GetSettingsForBuildTargetGroup(useActiveBuildTarget
                ? BuildPipeline.GetBuildTargetGroup(EditorUserBuildSettings.activeBuildTarget)
                : BuildTargetGroup.Standalone);
#else
            var settings = s_RuntimeInstance;
            if (settings == null)
                settings = ScriptableObject.CreateInstance<OpenXRSettings>();
#endif

            return settings;
        }

#if UNITY_EDITOR
        /// <summary>
        /// Returns the Settings object for the given build target group.
        /// </summary>
        /// <param name="buildTargetGroup">The build target group whose settings will be retrieved.</param>
        /// <returns>OpenXRSettings object for the given build target group.</returns>
        public static OpenXRSettings GetSettingsForBuildTargetGroup(BuildTargetGroup buildTargetGroup)
            => PackageSettingsLocator.GetPackageSettings()?.GetSettingsForBuildTargetGroup(buildTargetGroup);
#endif

        /// <summary>
        /// Accessor to OpenXR build time settings. In the Unity Editor, this returns the settings for the
        /// active build target group.
        /// </summary>
        public static OpenXRSettings ActiveBuildTargetInstance => GetInstance(true);

        /// <summary>
        /// Accessor to OpenXR build time settings. In the Unity Editor, this returns the settings for the
        /// Standalone build target group.
        /// </summary>
        public static OpenXRSettings Instance => GetInstance(false);
    }
}
