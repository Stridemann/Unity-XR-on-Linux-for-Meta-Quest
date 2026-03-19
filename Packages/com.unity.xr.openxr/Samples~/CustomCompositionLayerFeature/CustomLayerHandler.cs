#if XR_COMPOSITION_LAYERS
using Unity.XR.CompositionLayers.Extensions;
using Unity.XR.CompositionLayers.Services;
using Unity.XR.CompositionLayers;
using Unity.XR.CompositionLayers.Layers;
using UnityEngine.XR.OpenXR.NativeTypes;
using System.Runtime.InteropServices;

namespace UnityEngine.XR.OpenXR.CompositionLayers
{
    /// <summary>
    /// Demonstrates custom handler for the custom quad layer.
    /// </summary>
    public class CustomLayerHandler : OpenXRCustomLayerHandler<CustomNativeCompositionLayerQuad>
    {
        protected override bool CreateSwapchain(CompositionLayerManager.LayerInfo layer, out SwapchainCreateInfo swapchainCreateInfo)
        {
            unsafe
            {
                var texturesExtension = layer.Layer.GetComponent<TexturesExtension>();
                if (texturesExtension == null || texturesExtension.enabled == false)
                {
                    swapchainCreateInfo = default;
                    return false;
                }

                if (texturesExtension.sourceTexture == TexturesExtension.SourceTextureEnum.LocalTexture && texturesExtension.LeftTexture != null)
                {
                    var xrCreateInfo = new XrSwapchainCreateInfo()
                    {
                        Type = (uint)XrStructureType.XR_TYPE_SWAPCHAIN_CREATE_INFO,
                        Next = OpenXRLayerUtility.GetExtensionsChain(layer, CompositionLayerExtension.ExtensionTarget.Swapchain),
                        CreateFlags = 0,
                        UsageFlags = (ulong)(XrSwapchainUsageFlags.XR_SWAPCHAIN_USAGE_SAMPLED_BIT | XrSwapchainUsageFlags.XR_SWAPCHAIN_USAGE_COLOR_ATTACHMENT_BIT),
                        Format = OpenXRLayerUtility.GetDefaultColorFormat(),
                        SampleCount = 1,
                        Width = (uint)texturesExtension.LeftTexture.width,
                        Height = (uint)texturesExtension.LeftTexture.height,
                        FaceCount = 1,
                        ArraySize = 1,
                        MipCount = (uint)texturesExtension.LeftTexture.mipmapCount,
                    };

                    swapchainCreateInfo = new SwapchainCreateInfo(xrCreateInfo, isExternalSurface: false);
                    return true;
                }

                swapchainCreateInfo = default;
                return false;
            }
        }

        protected override bool CreateNativeLayer(CompositionLayerManager.LayerInfo layer, SwapchainCreatedOutput swapchainOutput, out CustomNativeCompositionLayerQuad nativeLayer)
        {
            unsafe
            {
                var texturesExtension = layer.Layer.GetComponent<TexturesExtension>();
                if (texturesExtension == null || texturesExtension.enabled == false)
                {
                    nativeLayer = default;
                    return false;
                }

                var data = layer.Layer.LayerData as CustomQuadLayerData;
                var transform = layer.Layer.GetComponent<Transform>();
                int subImageWidth = 0;
                int subImageHeight = 0;
                if (texturesExtension.sourceTexture == TexturesExtension.SourceTextureEnum.LocalTexture && texturesExtension.LeftTexture != null)
                {
                    subImageWidth = texturesExtension.LeftTexture.width;
                    subImageHeight = texturesExtension.LeftTexture.height;
                }

                var size = data.GetScaledSize(transform.lossyScale);
                nativeLayer = new CustomNativeCompositionLayerQuad()
                {
                    Type = (uint)XrStructureType.XR_TYPE_COMPOSITION_LAYER_QUAD,
                    Next = OpenXRLayerUtility.GetExtensionsChain(layer, CompositionLayerExtension.ExtensionTarget.Layer),
                    LayerFlags = data.BlendType == BlendType.Premultiply ? XrCompositionLayerFlags.SourceAlpha : XrCompositionLayerFlags.SourceAlpha | XrCompositionLayerFlags.UnPremultipliedAlpha,
                    Space = OpenXRLayerUtility.GetCurrentAppSpace(),
                    EyeVisibility = 0,
                    SubImage = new XrSwapchainSubImage()
                    {
                        Swapchain = swapchainOutput.handle,
                        ImageRect = new XrRect2Di()
                        {
                            Offset = new XrOffset2Di() { X = 0, Y = 0 },
                            Extent = new XrExtent2Di()
                            {
                                Width = subImageWidth,
                                Height = subImageHeight
                            }
                        },
                        ImageArrayIndex = 0
                    },
                    Pose = new XrPosef(OpenXRUtility.ComputePoseToWorldSpace(transform, CompositionLayerManager.mainCameraCache).position, OpenXRUtility.ComputePoseToWorldSpace(transform, CompositionLayerManager.mainCameraCache).rotation),
                    Size = new XrExtent2Df()
                    {
                        Width = size.x,
                        Height = size.y
                    }
                };
                return true;
            }
        }

        protected override bool ModifyNativeLayer(CompositionLayerManager.LayerInfo layerInfo, ref CustomNativeCompositionLayerQuad nativeLayer)
        {
            var texturesExtension = layerInfo.Layer.GetComponent<TexturesExtension>();
            if (texturesExtension == null || texturesExtension.enabled == false)
                return false;

            var data = layerInfo.Layer.LayerData as CustomQuadLayerData;
            var transform = layerInfo.Layer.GetComponent<Transform>();

            nativeLayer.Pose = new XrPosef(OpenXRUtility.ComputePoseToWorldSpace(transform, CompositionLayerManager.mainCameraCache).position, OpenXRUtility.ComputePoseToWorldSpace(transform, CompositionLayerManager.mainCameraCache).rotation);
            nativeLayer.Size = new XrExtent2Df()
            {
                Width = data.GetScaledSize(transform.lossyScale).x,
                Height = data.GetScaledSize(transform.lossyScale).y
            };

            unsafe
            {
                nativeLayer.Next = OpenXRLayerUtility.GetExtensionsChain(layerInfo, CompositionLayerExtension.ExtensionTarget.Layer);
            }

            return true;
        }

        protected override bool ActiveNativeLayer(CompositionLayerManager.LayerInfo layerInfo, ref CustomNativeCompositionLayerQuad nativeLayer)
        {
            var texturesExtension = layerInfo.Layer.GetComponent<TexturesExtension>();
            if (texturesExtension == null || texturesExtension.enabled == false)
                return false;

            var transform = layerInfo.Layer.GetComponent<Transform>();
            nativeLayer.Pose = new XrPosef(OpenXRUtility.ComputePoseToWorldSpace(transform, CompositionLayerManager.mainCameraCache).position, OpenXRUtility.ComputePoseToWorldSpace(transform, CompositionLayerManager.mainCameraCache).rotation);
            nativeLayer.Space = OpenXRLayerUtility.GetCurrentAppSpace();
            return base.ActiveNativeLayer(layerInfo, ref nativeLayer);
        }
    }
}

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct CustomNativeCompositionLayerQuad
    {
        public uint Type;
        public void* Next;
        public XrCompositionLayerFlags LayerFlags;
        public ulong Space;
        public uint EyeVisibility;
        public XrSwapchainSubImage SubImage;
        public XrPosef Pose;
        public XrExtent2Df Size;
    }
}
#endif
