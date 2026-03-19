#if XR_COMPOSITION_LAYERS
using System.Collections.Generic;
using Unity.XR.CompositionLayers;
using Unity.XR.CompositionLayers.Extensions;
using Unity.XR.CompositionLayers.Layers;
using Unity.XR.CompositionLayers.Services;
using UnityEngine.XR.OpenXR.NativeTypes;

namespace UnityEngine.XR.OpenXR.CompositionLayers
{
    internal class OpenXRCubeLayer : OpenXRCustomLayerHandler<XrCompositionLayerCubeKHR>
    {
        public static bool ExtensionEnabled = OpenXRRuntime.IsExtensionEnabled("XR_KHR_composition_layer_cube");

        protected override unsafe bool CreateSwapchain(CompositionLayerManager.LayerInfo layerInfo, out SwapchainCreateInfo swapchainCreateInfo)
        {
            TexturesExtension texture = layerInfo.Layer.GetComponent<TexturesExtension>();
            if (texture == null || texture.enabled == false || texture.LeftTexture == null)
            {
                swapchainCreateInfo = default;
                return false;
            }

            swapchainCreateInfo = new XrSwapchainCreateInfo()
            {
                Type = (uint)XrStructureType.XR_TYPE_SWAPCHAIN_CREATE_INFO,
                Next = OpenXRLayerUtility.GetExtensionsChain(layerInfo, CompositionLayerExtension.ExtensionTarget.Swapchain),
                CreateFlags = 0,
                UsageFlags = (ulong)(XrSwapchainUsageFlags.XR_SWAPCHAIN_USAGE_SAMPLED_BIT | XrSwapchainUsageFlags.XR_SWAPCHAIN_USAGE_COLOR_ATTACHMENT_BIT),
                Format = OpenXRLayerUtility.GetDefaultColorFormat(),
                SampleCount = 1,
                Width = (uint)(texture.LeftTexture.width),
                Height = (uint)(texture.LeftTexture.height),
                FaceCount = 6,
                ArraySize = 1,
                MipCount = (uint)texture.LeftTexture.mipmapCount,
            };
            return true;
        }

        protected override unsafe bool CreateNativeLayer(CompositionLayerManager.LayerInfo layerInfo, SwapchainCreatedOutput swapchainOutput, out XrCompositionLayerCubeKHR nativeLayer)
        {
            var data = layerInfo.Layer.LayerData as CubeProjectionLayerData;
            var transform = layerInfo.Layer.GetComponent<Transform>();

            nativeLayer = new XrCompositionLayerCubeKHR()
            {
                Type = (uint)XrStructureType.XR_TYPE_COMPOSITION_LAYER_CUBE_KHR,
                Next = OpenXRLayerUtility.GetExtensionsChain(layerInfo, CompositionLayerExtension.ExtensionTarget.Layer),
                LayerFlags = data.BlendType == BlendType.Premultiply ? XrCompositionLayerFlags.SourceAlpha : XrCompositionLayerFlags.SourceAlpha | XrCompositionLayerFlags.UnPremultipliedAlpha,
                Space = OpenXRLayerUtility.GetCurrentAppSpace(),
                EyeVisibility = 0,
                Swapchain = swapchainOutput.handle,
                ImageArrayIndex = 0,
                Orientation = new XrQuaternionf(OpenXRUtility.ComputePoseToWorldSpace(transform, CompositionLayerManager.mainCameraCache).rotation)
            };

            return true;
        }

        protected override bool ModifyNativeLayer(CompositionLayerManager.LayerInfo layerInfo, ref XrCompositionLayerCubeKHR nativeLayer)
        {
            var texturesExtension = layerInfo.Layer.GetComponent<TexturesExtension>();
            if (texturesExtension == null || texturesExtension.enabled == false || texturesExtension.LeftTexture == null)
                return false;

            var transform = layerInfo.Layer.GetComponent<Transform>();
            nativeLayer.Space = OpenXRLayerUtility.GetCurrentAppSpace();
            nativeLayer.Orientation = new XrQuaternionf(OpenXRUtility.ComputePoseToWorldSpace(transform, CompositionLayerManager.mainCameraCache).rotation);

            return true;
        }

        protected override bool ActiveNativeLayer(CompositionLayerManager.LayerInfo layerInfo, ref XrCompositionLayerCubeKHR nativeLayer)
        {
            nativeLayer.Space = OpenXRLayerUtility.GetCurrentAppSpace();
            return base.ActiveNativeLayer(layerInfo, ref nativeLayer);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
#endif
