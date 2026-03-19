---
uid: openxr-spacewarp
---

# Application SpaceWarp in OpenXR

Application SpaceWarp is an optimization for OpenXR that helps applications maintain a high frame rate. SpaceWarp synthesizes every other frame, which can reduce computational power and energy use considerably. The technique uses reprojection when synthesizing frames to reduce latency between the user's movements and display updates.

SpaceWarp can cause display artifacts and might require changes to your application for acceptable results.

Refer to the following topics for information about how to use SpaceWarp in your Unity project:

| Topics | Description |
| :----- | :---------- |
| [Prerequisites](xref:openxr-spacewarp-prerequisites) | Lists the technical requirements needed to utilize SpaceWarp in Unity projects, including specific versions of Unity, OpenXR, URP, and other necessary components.|
| [Understand Application SpaceWarp](xref:openxr-spacewarp-overview) | Explains how SpaceWarp works by interpolating frames and describes associated buffers, limitations, and potential visual artifacts you might encounter.|
| [Enable and use Application SpaceWarp](xref:openxr-spacewarp-workflow) | Provides an overview of how to enable and configure SpaceWarp in Unity projects. |
| [Shaders and SpaceWarp](xref:openxr-spacewarp-shaders) | Lists the URP shaders that are compatible with SpaceWarp. Explains how to update custom shaders to support SpaceWarp. |
| [Configure Materials for SpaceWarp](xref:openxr-spacewarp-materials) | How to apply the SpaceWarp feature to specific Materials by toggling the **XR Motion Vectors Pass (Space Warp)** property. |

## Additional resources

* [Rendergraph](xref:urp-render-graph)
* [URP compatibility mode](xref:urp-compatibility-mode)
* [Using the Universal Render Pipeline](xref:um-universal-render-pipeline)
* [Shader Graph](xref:um-shader-graph)
* [OpenXR XR\_FB\_space\_warp extension](https://registry.khronos.org/OpenXR/specs/1.0/html/xrspec.html#XR_FB_space_warp)
* [Application SpaceWarp Developer Guide (Meta)](https://developers.meta.com/horizon/documentation/unity/unity-asw/)
