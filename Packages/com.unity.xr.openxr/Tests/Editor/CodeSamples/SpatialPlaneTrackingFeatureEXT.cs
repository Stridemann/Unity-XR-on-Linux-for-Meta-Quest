//#define ENABLE_CODE_SAMPLE_COMPILATION
#if ENABLE_CODE_SAMPLE_COMPILATION
#region SpatialPlaneTrackingFeatureEXT
using UnityEngine.XR.OpenXR;
using UnityEngine.XR.OpenXR.Features;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.XR.OpenXR.Features;
#endif

#if UNITY_EDITOR
[OpenXRFeature(UiName = k_DisplayName,
    BuildTargetGroups = new[]
        { BuildTargetGroup.Android, BuildTargetGroup.Standalone },
    Company = "Your company name",
    Desc = "Enables XR_EXT_spatial_plane_tracking and dependencies.",
    DocumentationLink = "https://yourcompany.com/documentation/thisfeature.html",
    OpenxrExtensionStrings = k_OpenXRRequestedExtensions,
    Category = FeatureCategory.Feature,
    FeatureId = "com.yourcompany.featurename",
    Version = "0.1.0")]
#endif
class SpatialPlaneTrackingFeatureEXT : OpenXRFeature
{
    const string k_DisplayName = "Spatial Entities EXT: Plane Tracking";
    const string k_OpenXRRequestedExtensions =
        "XR_EXT_future"
        + " XR_EXT_spatial_entity"
        + " XR_EXT_spatial_plane_tracking";

    protected override bool OnInstanceCreate(ulong xrInstance)
    {
        return OpenXRRuntime.IsExtensionEnabled("XR_EXT_future")
            && OpenXRRuntime.IsExtensionEnabled("XR_EXT_spatial_entity")
            && OpenXRRuntime.IsExtensionEnabled("XR_EXT_spatial_plane_tracking");
    }
}
#endregion
#endif
