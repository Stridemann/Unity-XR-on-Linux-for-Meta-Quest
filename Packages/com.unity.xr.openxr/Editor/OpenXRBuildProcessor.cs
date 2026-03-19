using UnityEditor.Build.Reporting;
using UnityEditor.XR.Management;
using UnityEditor.XR.OpenXR.Features;
using UnityEngine.XR.OpenXR;

namespace UnityEditor.XR.OpenXR
{
    internal class OpenXRBuildProcessor : XRBuildHelper<OpenXRSettings>
    {
        private const string kRequestAdditionalVulkanGraphicsQueue = "xr-request-additional-vulkan-graphics-queue";
        private const string kVulkanOffscreenSwapchainNoMainDisplay = "xr-use-vulkan-offscreen-swapchain-no-main-display-buffer";

        public override string BuildSettingsKey => Constants.k_SettingsKey;

        private readonly BootConfigBuilder _bootConfigBuilder = new BootConfigBuilder();

        public override UnityEngine.Object SettingsForBuildTargetGroup(BuildTargetGroup buildTargetGroup)
        {
            EditorBuildSettings.TryGetConfigObject(Constants.k_SettingsKey, out OpenXRPackageSettings packageSettings);
            if (packageSettings == null)
                return null;
            return packageSettings.GetSettingsForBuildTargetGroup(buildTargetGroup);
        }

        public override void OnPreprocessBuild(BuildReport report)
        {
            base.OnPreprocessBuild(report);

            if (!BuildHelperUtils.HasActiveLoader(BuildPipeline.GetBuildTargetGroup(report.summary.platform), typeof(OpenXRLoaderBase)))
                return;

            _bootConfigBuilder.ReadBootConfig(report);

#if UNITY_STANDALONE_WIN || UNITY_ANDROID
            var settings = OpenXREditorSettings.Instance;
            if (settings != null)
            {
                _bootConfigBuilder.SetBootConfigBoolean(kRequestAdditionalVulkanGraphicsQueue, settings.VulkanAdditionalGraphicsQueue);

                var offscreenRenderingOnly = false;
#if UNITY_ANDROID
                offscreenRenderingOnly = settings.VulkanOffscreenSwapchainNoMainDisplay;
#endif
                _bootConfigBuilder.SetBootConfigBoolean(kVulkanOffscreenSwapchainNoMainDisplay, offscreenRenderingOnly);
                _bootConfigBuilder.WriteBootConfig(report);
            }
#endif
        }

        public override void OnPostprocessBuild(BuildReport report)
        {
            base.OnPostprocessBuild(report);

#if UNITY_STANDALONE_WIN || UNITY_ANDROID
            var settings = OpenXREditorSettings.Instance;
            if (settings != null)
            {
                _bootConfigBuilder.ClearAndWriteBootConfig(report);
            }
#endif
        }
    }
}
