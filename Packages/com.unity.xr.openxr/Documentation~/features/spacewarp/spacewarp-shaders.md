---
uid: openxr-spacewarp-shaders
---

# Shaders and SpaceWarp

Application SpaceWarp requires shaders that record XR motion vectors, which are used to predict the position of pixels in the extrapolated frames. If you use a shader that doesn't record XR motion vectors, rendered objects that use it won't update in the synthesized frames. These objects might appear to stutter because they are effectively rendered at half the framerate. Refer to [Understand Application SpaceWarp](xref:openxr-spacewarp-overview) for information about how SpaceWarp works.

## Compatible URP shaders {#compatible-urp-shaders}

The following URP base shaders are SpaceWarp compatible:

* [Lit](xref:urp-lit-shader)
* [Unlit](xref:urp-unlit-shader)
* [Complex Lit](xref:urp-shader-complex-lit)
* [Simple Lit](xref:urp-simple-lit-shader)
* [Baked Lit](xref:urp-baked-lit-shader)

For Shadergraph, the following **Material** options for the **Universal** [Graph Target](https://docs.unity3d.com/Packages/com.unity.shadergraph@17.0/manual/Graph-Target.html) are SpaceWarp compatible:

* Lit
* Unlit

## Modify custom shaders {#modify-custom-shaders}

To enable your custom shaders to work with Application SpaceWarp, you must add support for the XRMotionVectors render pass.

First, add the `_XRMotionVectorsPass` property to your shader. This property allows you to enable SpaceWarp on a per-material basis. (Refer to [Configure Materials for SpaceWarp](xref:openxr-spacewarp-materials) for information about how it works with the URP shaders that support SpaceWarp.)

``` lang-hlsl
[HideInInspector] _XRMotionVectorsPass("_XRMotionVectorsPass", Float) = 1.0
```

Next, add the following shader subpass declaration:

``` lang-hlsl
Pass
{
        Name "XRMotionVectors"
        Tags { "LightMode" = "XRMotionVectors" }
        ColorMask RGBA

        // Stencil write for obj motion pixels
        Stencil
        {
        WriteMask 1
                Ref 1
                Comp Always
                Pass Replace
        }

        HLSLPROGRAM
        #pragma shader_feature_local _ALPHATEST_ON
        #pragma multi_compile _ LOD_FADE_CROSSFADE
        #pragma shader_feature_local_vertex _ADD_PRECOMPUTED_VELOCITY
        #define APPLICATION_SPACE_WARP_MOTION 1

        #include "Packages/com.unity.render-pipelines.universal/Shaders/BakedLitInput.hlsl"
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ObjectMotionVectors.hlsl"
        ENDHLSL
}
```
