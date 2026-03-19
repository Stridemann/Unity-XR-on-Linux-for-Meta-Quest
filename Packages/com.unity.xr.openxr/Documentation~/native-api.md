---
uid: openxr-native-api
---
# OpenXR native API

For users familiar with the [OpenXR specification](https://registry.khronos.org/OpenXR/specs/1.1/html/xrspec.html), the class [OpenXRNativeApi](xref:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi) contains a partial public C# port of OpenXR. You can use the OpenXR native API to write low-level OpenXR code if you prefer a lower-level abstraction.

Refer to the OpenXR specification for more detailed information about each extension.

## Supported extensions

The OpenXR native API currently supports the following OpenXR extensions:

* [XR_EXT_future](https://registry.khronos.org/OpenXR/specs/1.1/html/xrspec.html#XR_EXT_future)
* [XR_EXT_spatial_anchor](https://registry.khronos.org/OpenXR/specs/1.1/html/xrspec.html#XR_EXT_spatial_anchor)
* [XR_EXT_spatial_entity](https://registry.khronos.org/OpenXR/specs/1.1/html/xrspec.html#XR_EXT_spatial_entity)
* [XR_EXT_spatial_marker_tracking](https://registry.khronos.org/OpenXR/specs/1.1/html/xrspec.html#XR_EXT_spatial_marker_tracking)
* [XR_EXT_spatial_persistence](https://registry.khronos.org/OpenXR/specs/1.1/html/xrspec.html#XR_EXT_spatial_persistence)
* [XR_EXT_spatial_persistence_operations](https://registry.khronos.org/OpenXR/specs/1.1/html/xrspec.html#XR_EXT_spatial_persistence_operations)
* [XR_EXT_spatial_plane_tracking](https://registry.khronos.org/OpenXR/specs/1.1/html/xrspec.html#XR_EXT_spatial_plane_tracking)

### Enable extensions

The OpenXR native API is a static class, so C# allows you to call its methods at any time from any C# script. However, methods provided by an extension will return the `XrResult.FunctionUnsupported` error code unless the required extension is enabled.

The OpenXR Plug-in supports enabling extensions via [OpenXR features](xref:openxr-features).

To enable the required extensions for the native API:

1. Create a class that inherits `OpenXRFeature`.
2. Fill out the required information for the `OpenXRFeatureAttribute` constructor, including the extensions you wish to enable.
3. Implement [OnInstanceCreate](xref:UnityEngine.XR.OpenXR.Features.OpenXRFeature.OnInstanceCreate*). Return `false` if any requirements to enable your feature are not met, such as if runtime failed to enable the required extensions.
4. Go to **Project Settings** > **XR Plug-in Management** and enable your feature in the **OpenXR** tab.
5. At runtime, use `OpenXRSettings.Instance.GetFeature<T>` to get your feature. If `feature != null && feature.enabled`, your app can use the native API methods provided by the enabled extensions.

#### Example code

The following example code demonstrates how to implement a simple OpenXR feature that enables `XR_EXT_spatial_plane_tracking` and its required dependencies:

[!code-cs[SpatialPlaneTrackingFeatureEXT](../Tests/Editor/CodeSamples/SpatialPlaneTrackingFeatureEXT.cs#SpatialPlaneTrackingFeatureEXT)]
