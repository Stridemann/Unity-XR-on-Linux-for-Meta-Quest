#if XR_COMPOSITION_LAYERS
using UnityEditor;
using Unity.XR.CompositionLayers.Services;
using UnityEngine.XR.OpenXR.CompositionLayers;
#if UNITY_EDITOR
using UnityEditor.XR.OpenXR.Features;
#endif

namespace UnityEngine.XR.OpenXR.Features.CompositionLayers
{
    /// <summary>
    /// Enables OpenXR Composition Layers.
    /// </summary>
#if UNITY_EDITOR
    [OpenXRFeature(UiName = FeatureName,
        Desc = "Necessary to use OpenXR Composition Layers.",
        Company = "Unity",
        OpenxrExtensionStrings = "XR_KHR_composition_layer_cylinder XR_KHR_composition_layer_equirect XR_KHR_composition_layer_equirect2 XR_KHR_composition_layer_cube XR_KHR_composition_layer_color_scale_bias XR_KHR_android_surface_swapchain",
        Version = "1.0.0",
        BuildTargetGroups = new[] { BuildTargetGroup.Android, BuildTargetGroup.Standalone, BuildTargetGroup.WSA },
        FeatureId = FeatureId
    )]
#endif
    public class OpenXRCompositionLayersFeature : OpenXRFeature
    {
        public const string FeatureId = "com.unity.openxr.feature.compositionlayers";
        internal const string FeatureName = "Composition Layers Support";

        protected internal override void OnSessionBegin(ulong xrSession)
        {
            if (CompositionLayerManager.Instance != null)
            {
                CompositionLayerManager.Instance.LayerProvider ??= new OpenXRLayerProvider();
            }
        }

        protected internal override void OnSessionEnd(ulong xrSession)
        {
            if (CompositionLayerManager.Instance?.LayerProvider is OpenXRLayerProvider)
            {
                ((OpenXRLayerProvider)CompositionLayerManager.Instance.LayerProvider).Dispose();
                CompositionLayerManager.Instance.LayerProvider = null;
            }
        }
    }
}
#endif
