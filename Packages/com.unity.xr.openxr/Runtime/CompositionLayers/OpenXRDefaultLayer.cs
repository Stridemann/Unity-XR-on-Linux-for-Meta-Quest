#if XR_COMPOSITION_LAYERS

using Unity.XR.CompositionLayers;
using Unity.XR.CompositionLayers.Layers;
using Unity.XR.CompositionLayers.Services;
using UnityEngine.XR.OpenXR.NativeTypes;

namespace UnityEngine.XR.OpenXR.CompositionLayers
{
    internal class OpenXRDefaultLayer : OpenXRLayerProvider.ILayerHandler
    {
        unsafe void SetDefaultLayerAttributes(CompositionLayerManager.LayerInfo layerInfo)
        {
            var extensions = OpenXRLayerUtility.GetExtensionsChain(layerInfo, CompositionLayerExtension.ExtensionTarget.Layer);
            OpenXRLayerUtility.SetDefaultSceneLayerExtensions(extensions);

            var flags = layerInfo.Layer.LayerData.BlendType == BlendType.Premultiply ? XrCompositionLayerFlags.SourceAlpha : XrCompositionLayerFlags.SourceAlpha | XrCompositionLayerFlags.UnPremultipliedAlpha;
            OpenXRLayerUtility.SetDefaultLayerFlags(flags);
        }

        public void CreateLayer(CompositionLayerManager.LayerInfo layerInfo) => SetDefaultLayerAttributes(layerInfo);

        public void ModifyLayer(CompositionLayerManager.LayerInfo layerInfo) => SetDefaultLayerAttributes(layerInfo);

        public void OnUpdate()
        {
            return;
        }

        public void RemoveLayer(int id)
        {
            ClearDefaultLayerFlags();
            ClearDefaultSceneLayerExtensions();
        }

        public void SetActiveLayer(CompositionLayerManager.LayerInfo layerInfo)
        {
            return;
        }

        unsafe void ClearDefaultSceneLayerExtensions() => OpenXRLayerUtility.SetDefaultSceneLayerExtensions(null);
        void ClearDefaultLayerFlags() => OpenXRLayerUtility.ClearDefaultLayerFlags();
    }
}
#endif
