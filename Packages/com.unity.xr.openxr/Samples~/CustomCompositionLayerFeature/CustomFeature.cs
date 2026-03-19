#if XR_COMPOSITION_LAYERS
using System.Runtime.CompilerServices;
using Unity.XR.CompositionLayers.Layers;
using UnityEditor;
using UnityEngine.XR.OpenXR.CompositionLayers;
using UnityEngine.XR.OpenXR.Features;

#if UNITY_EDITOR
[UnityEditor.XR.OpenXR.Features.OpenXRFeature(UiName = "OpenXR Custom Layer Handler Example",
    BuildTargetGroups = new[] { BuildTargetGroup.Standalone, BuildTargetGroup.WSA, BuildTargetGroup.Android },
    Company = "Unity",
    Desc = "An example to demonstrate how to enable a handler for a customized composition layer type.",
    DocumentationLink = "",
    FeatureId = "com.unity.openxr.features.customlayerexample",
    OpenxrExtensionStrings = "",
    Version = "1")]
#endif
public class CustomFeature : OpenXRFeature
{
    bool isSubscribed;

    protected override void OnEnable()
    {
        if (OpenXRLayerProvider.isStarted)
            CreateAndRegisterLayerHandler();
        else
        {
            OpenXRLayerProvider.Started += CreateAndRegisterLayerHandler;
            isSubscribed = true;
        }
    }

    protected override void OnDisable()
    {
        if (isSubscribed)
        {
            OpenXRLayerProvider.Started -= CreateAndRegisterLayerHandler;
            isSubscribed = false;
        }
    }

    protected void CreateAndRegisterLayerHandler()
    {
        if (enabled)
        {
            var layerHandler = new CustomLayerHandler();
            OpenXRLayerProvider.RegisterLayerHandler(typeof(CustomQuadLayerData), layerHandler);
        }
    }
}
#endif
