#if XR_COMPOSITION_LAYERS
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.XR.CompositionLayers;
using Unity.XR.CompositionLayers.Extensions;
using Unity.XR.CompositionLayers.Layers;
using Unity.XR.CompositionLayers.Services;
using UnityEngine.Rendering;
using UnityEngine.XR.OpenXR.NativeTypes;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.XR.OpenXR.CompositionLayers
{
    //Default OpenXR Composition Layer - Projection Layer support
    internal class OpenXRProjectionLayer : OpenXRCustomLayerHandler<XrCompositionLayerProjection>, IDisposable
    {
        class ProjectionData : IDisposable
        {
            public bool isActiveLayer;
            public CompositionLayerManager.LayerInfo layer;
            public TexturesExtension texturesExtension;
            public NativeArray<XrCompositionLayerProjectionView> nativeViews;
            public uint[] renderTextureIds;
            public RenderTexture[] rigTextures;
            public Camera[] rigCameras;

            public void Dispose()
            {
                nativeViews.Dispose();

                if (rigTextures != null)
                {
                    foreach (var renderTexture in rigTextures)
                    {
                        renderTexture.Release();
                    }
                }
            }
        }

        static Dictionary<int, ProjectionData> m_ProjectionData = new Dictionary<int, ProjectionData>();
        bool m_isMainCameraRendered = false;
        bool m_isOnBeforeRender = false;

        /// <summary>
        /// Constructor for OpenXR Projection Layer.
        /// </summary>
        public OpenXRProjectionLayer()
        {
            Application.onBeforeRender += OnBeforeRender;
            RenderPipelineManager.endCameraRendering += OnCameraEndRendering;
            Camera.onPostRender += OnCameraPostRender;
        }

        /// <summary>
        /// Used to clean up.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var projectionData in m_ProjectionData.Values)
                    projectionData.Dispose();
                m_ProjectionData.Clear();
            }

            Application.onBeforeRender -= OnBeforeRender;
            base.Dispose(disposing);
        }

        protected override bool CreateSwapchain(CompositionLayerManager.LayerInfo layer, out SwapchainCreateInfo swapchainCreateInfo)
        {
            bool result = ext_composition_layers_GetViewConfigurationData(out uint width, out uint height, out uint sampleCount);
            if (!result)
            {
                swapchainCreateInfo = default;
                return false;
            }

            bool isProjectionRig = layer.Layer.LayerData.GetType() == typeof(ProjectionLayerRigData);
            if (!isProjectionRig)
            {
                TexturesExtension texturesExtension = layer.Layer.GetComponent<TexturesExtension>();
                if (texturesExtension == null || texturesExtension.enabled == false || texturesExtension.LeftTexture == null || texturesExtension.RightTexture == null)
                {
                    swapchainCreateInfo = default;
                    return false;
                }
            }

            unsafe
            {
                var xrCreateInfo = new XrSwapchainCreateInfo()
                {
                    Type = (uint)XrStructureType.XR_TYPE_SWAPCHAIN_CREATE_INFO,
                    Next = OpenXRLayerUtility.GetExtensionsChain(layer, CompositionLayerExtension.ExtensionTarget.Swapchain),
                    CreateFlags = 0,
                    UsageFlags = (ulong)(XrSwapchainUsageFlags.XR_SWAPCHAIN_USAGE_SAMPLED_BIT | XrSwapchainUsageFlags.XR_SWAPCHAIN_USAGE_COLOR_ATTACHMENT_BIT),
                    Format = OpenXRLayerUtility.GetDefaultColorFormat(),
                    SampleCount = sampleCount,
                    Width = width,
                    Height = height,
                    FaceCount = 1,
                    ArraySize = 1,
                    MipCount = 1,
                };

                swapchainCreateInfo = new SwapchainCreateInfo(xrCreateInfo, isStereo: true);
                return true;
            }
        }

        /// <summary>
        /// Called by the <see cref="OpenXRLayerProvider"/> when a new <see cref="LayerData"/>
        /// object of the type registered to this <c>ILayerHandler</c> instance has been created.
        /// </summary>
        protected override unsafe bool CreateNativeLayer(CompositionLayerManager.LayerInfo layer, SwapchainCreatedOutput swapchainCreatedOutput, out XrCompositionLayerProjection nativeLayer)
        {
            if (layer.Layer == null)
            {
                nativeLayer = default;
                return false;
            }

            bool result = ext_composition_layers_GetViewConfigurationData(out uint width, out uint height, out uint sampleCount);
            if (!result)
            {
                nativeLayer = default;
                return false;
            }

            TexturesExtension texturesExtension = null;
            var isProjectionRig = layer.Layer.LayerData.GetType() == typeof(ProjectionLayerRigData);
            if (!isProjectionRig)
            {
                texturesExtension = layer.Layer.GetComponent<TexturesExtension>();
                if (texturesExtension == null || texturesExtension.enabled == false || texturesExtension.LeftTexture == null || texturesExtension.RightTexture == null)
                {
                    nativeLayer = default;
                    return false;
                }
            }

            XrView xrView1 = default;
            XrView xrView2 = default;
            bool validViews = ext_composition_layers_GetStereoViews(&xrView1, &xrView2);
            XrView[] xrViews = new XrView[2] { xrView1, xrView2 };

            var nativeView1 = new XrCompositionLayerProjectionView()
            {
                Type = (uint)XrStructureType.XR_TYPE_COMPOSITION_LAYER_PROJECTION_VIEW,
                Next = null,
                Pose = validViews ? xrViews[0].Pose : default,
                Fov = validViews ? xrViews[0].Fov : default,
                SubImage = new XrSwapchainSubImage()
                {
                    Swapchain = swapchainCreatedOutput.handle,
                    ImageRect = new XrRect2Di()
                    {
                        Offset = new XrOffset2Di() { X = 0, Y = 0 },
                        Extent = new XrExtent2Di()
                        {
                            Width = (int)width,
                            Height = (int)height
                        }
                    },
                    ImageArrayIndex = 0
                },
            };

            var nativeView2 = new XrCompositionLayerProjectionView()
            {
                Type = (uint)XrStructureType.XR_TYPE_COMPOSITION_LAYER_PROJECTION_VIEW,
                Next = null,
                Pose = validViews ? xrViews[1].Pose : default,
                Fov = validViews ? xrViews[1].Fov : default,
                SubImage = new XrSwapchainSubImage()
                {
                    Swapchain = swapchainCreatedOutput.secondStereoHandle,
                    ImageRect = new XrRect2Di()
                    {
                        Offset = new XrOffset2Di() { X = 0, Y = 0 },
                        Extent = new XrExtent2Di()
                        {
                            Width = (int)width,
                            Height = (int)height
                        }
                    },
                    ImageArrayIndex = 0
                },
            };

            var nativeViews = new NativeArray<XrCompositionLayerProjectionView>(2, Allocator.Persistent);
            nativeViews[0] = nativeView1;
            nativeViews[1] = nativeView2;

            nativeLayer = new XrCompositionLayerProjection()
            {
                Type = (uint)XrStructureType.XR_TYPE_COMPOSITION_LAYER_PROJECTION,
                Next = OpenXRLayerUtility.GetExtensionsChain(layer, CompositionLayerExtension.ExtensionTarget.Layer),
                LayerFlags = XrCompositionLayerFlags.SourceAlpha,
                Space = OpenXRLayerUtility.GetCurrentAppSpace(),
                ViewCount = 2,
                Views = (XrCompositionLayerProjectionView*)nativeViews.GetUnsafePtr()
            };

            if (!isProjectionRig)
            {
                if (!IsTexturesExtensionValid(texturesExtension))
                {
                    nativeLayer = default;
                    return false;
                }

                m_ProjectionData.Add(layer.Id, new ProjectionData
                {
                    layer = layer,
                    nativeViews = nativeViews,
                    texturesExtension = texturesExtension,
                    rigTextures = null,
                    rigCameras = null
                });

                return true;
            }

            // Create render textures to be the target texture of the child cameras.
            Camera leftCamera = null;
            Camera rightCamera = null;
            if (layer.Layer.transform.childCount >= 2)
            {
                leftCamera = layer.Layer.transform.GetChild(0).GetComponent<Camera>();
                rightCamera = layer.Layer.transform.GetChild(1).GetComponent<Camera>();
            }

            String layerName = String.Format("projectLayer_{0}", layer.Id);
            RenderTexture leftRT = new RenderTexture((int)width, (int)height, 0, RenderTextureFormat.ARGB32) { name = layerName + "_left", depthStencilFormat = Experimental.Rendering.GraphicsFormat.D32_SFloat_S8_UInt };
            leftRT.Create();
            RenderTexture rightRT = new RenderTexture((int)width, (int)height, 0, RenderTextureFormat.ARGB32) { name = layerName + "_right", depthStencilFormat = Experimental.Rendering.GraphicsFormat.D32_SFloat_S8_UInt };
            rightRT.Create();

            texturesExtension = layer.Layer.GetComponent<TexturesExtension>();
            texturesExtension.LeftTexture = leftRT;
            texturesExtension.RightTexture = rightRT;

            if (leftCamera)
            {
                leftCamera.targetTexture = leftRT;
                leftCamera.enabled = false;
            }
            if (rightCamera != null)
            {
                rightCamera.targetTexture = rightRT;
                rightCamera.enabled = false;
            }

            m_ProjectionData.Add(layer.Id, new ProjectionData
            {
                layer = layer,
                nativeViews = nativeViews,
                texturesExtension = texturesExtension,
                rigTextures = new RenderTexture[2] { leftRT, rightRT },
                rigCameras = new Camera[2] { leftCamera, rightCamera }
            });

            return true;
        }

        /// <summary>
        /// Called by the <see cref="OpenXRLayerProvider"/> when a <see cref="LayerData"/> object
        /// of the type registered to this <c>ILayerHandler</c> instance has been destroyed or disabled.
        /// </summary>
        public override void RemoveLayer(int id)
        {
            if (m_ProjectionData.ContainsKey(id))
            {
                m_ProjectionData[id].Dispose();
                m_ProjectionData.Remove(id);
            }

            base.RemoveLayer(id);
        }

        /// <summary>
        /// Called by the <see cref="OpenXRLayerProvider"/> when a <see cref="LayerData"/> object
        /// or any attached extension components have had a member modified.
        /// </summary>
        protected override unsafe bool ModifyNativeLayer(CompositionLayerManager.LayerInfo layerInfo, ref XrCompositionLayerProjection nativeLayer)
        {
            if (layerInfo.Layer.LayerData is ProjectionLayerRigData && !m_isOnBeforeRender)
                return false;

            var texturesExtension = layerInfo.Layer.GetComponent<TexturesExtension>();
            if (texturesExtension == null || texturesExtension.enabled == false || texturesExtension.LeftTexture == null || texturesExtension.RightTexture == null)
                return false;

            XrView view1 = default;
            XrView view2 = default;
            bool validViews = ext_composition_layers_GetStereoViews(&view1, &view2);
            if (validViews)
            {
                XrView[] views = new XrView[2] { view1, view2 };
                nativeLayer.Views[0].Pose = views[0].Pose;
                nativeLayer.Views[0].Fov = views[0].Fov;
                nativeLayer.Views[1].Pose = views[1].Pose;
                nativeLayer.Views[1].Fov = views[1].Fov;
            }

            bool isProjectionRig = layerInfo.Layer.LayerData.GetType() == typeof(ProjectionLayerRigData);
            if (!isProjectionRig && !IsTexturesExtensionValid(texturesExtension))
            {
                nativeLayer = default;
                return false;
            }

            nativeLayer.Next = OpenXRLayerUtility.GetExtensionsChain(layerInfo, CompositionLayerExtension.ExtensionTarget.Layer);
            return true;
        }

        /// <summary>
        /// Called every frame by the <see cref="OpenXRLayerProvider"/> for all currently active <see cref="LayerData"/> objects
        /// of the type registered to this <c>ILayerHandler</c> instance.
        /// </summary>
        protected override unsafe bool ActiveNativeLayer(CompositionLayerManager.LayerInfo layerInfo, ref XrCompositionLayerProjection nativeLayer)
        {
            if (layerInfo.Layer.LayerData is ProjectionLayerRigData && !m_isOnBeforeRender)
                return false;

            var texturesExtension = layerInfo.Layer.GetComponent<TexturesExtension>();
            if (texturesExtension != null || texturesExtension.enabled && texturesExtension.CustomRects && texturesExtension.LeftTexture != null || texturesExtension.RightTexture != null)
            {
                nativeLayer.Views[0].SubImage.ImageRect = GetSubImageOffsetAndExtent(texturesExtension.LeftEyeSourceRect, new Vector2(texturesExtension.LeftTexture?.width ?? 0, texturesExtension.LeftTexture?.height ?? 0));
                nativeLayer.Views[1].SubImage.ImageRect = GetSubImageOffsetAndExtent(texturesExtension.RightEyeSourceRect, new Vector2(texturesExtension.RightTexture?.width ?? 0, texturesExtension.RightTexture?.height ?? 0));
            }

            nativeLayer.Space = OpenXRLayerUtility.GetCurrentAppSpace();
            OpenXRLayerUtility.RequestStereoRenderTextureIds(layerInfo.Id, OnStereoRenderTextureIdsCallback);

            return true;
        }

        // Used to hault calls to GetStereoProjectionMatrix before the main camera's projection matrix has been initialized
        void OnCameraPostRender(Camera cam)
        {
            var mainCamera = CompositionLayerManager.mainCameraCache;
            if (m_isMainCameraRendered || mainCamera == null || cam != mainCamera)
                return;

            m_isMainCameraRendered = true;
            Camera.onPostRender -= OnCameraPostRender;
            RenderPipelineManager.endCameraRendering -= OnCameraEndRendering;
        }

        // Used to hault calls to GetStereoProjectionMatrix before the main camera's projection matrix has been initialized
        void OnCameraEndRendering(ScriptableRenderContext cxt, Camera cam) => OnCameraPostRender(cam);

        void OnBeforeRender()
        {
            m_isOnBeforeRender = true;
            foreach (var projectionData in m_ProjectionData.Values)
            {
                // Only projection rig layers will have associated camera data.
                if (projectionData.rigCameras != null && projectionData.layer.Layer != null)
                    UpdateProjectionRig(projectionData);

                WriteToRenderTexture(projectionData);
            }

            m_isOnBeforeRender = false;
        }

        void UpdateProjectionRig(ProjectionData projectionData)
        {
            var mainCamera = CompositionLayerManager.mainCameraCache;
            var leftCamera = projectionData.rigCameras[0];
            var rightCamera = projectionData.rigCameras[1];

            if (m_isMainCameraRendered && leftCamera != null && rightCamera != null)
            {
                leftCamera.projectionMatrix = mainCamera.GetStereoProjectionMatrix(Camera.StereoscopicEye.Left);
                rightCamera.projectionMatrix = mainCamera.GetStereoProjectionMatrix(Camera.StereoscopicEye.Right);

                leftCamera.Render();
                rightCamera.Render();
            }

            this.ModifyLayer(projectionData.layer);
            this.SetActiveLayer(projectionData.layer);
        }

        void WriteToRenderTexture(ProjectionData projectionData)
        {
            if (projectionData.renderTextureIds == null || projectionData.texturesExtension == null)
                return;

            var renderTextureLeft = OpenXRLayerUtility.FindRenderTexture(projectionData.renderTextureIds[0]);
            var renderTextureRight = OpenXRLayerUtility.FindRenderTexture(projectionData.renderTextureIds[1]);
            OpenXRLayerUtility.WriteToRenderTexture(projectionData.texturesExtension.LeftTexture, renderTextureLeft);
            OpenXRLayerUtility.WriteToRenderTexture(projectionData.texturesExtension.RightTexture, renderTextureRight);
        }

        XrRect2Di GetSubImageOffsetAndExtent(Rect sourceRect, Vector2 textureSize)
        {
            var sourceTextureOffset = new XrOffset2Di()
            {
                X = (int)(sourceRect.x * textureSize.x),
                Y = (int)(sourceRect.y * textureSize.y)
            };

            var sourceTextureExtent = new XrExtent2Di()
            {
                Width = (int)(sourceRect.width * textureSize.x),
                Height = (int)(sourceRect.height * textureSize.y)
            };

            return new XrRect2Di { Offset = sourceTextureOffset, Extent = sourceTextureExtent };
        }

        unsafe bool IsTexturesExtensionValid(TexturesExtension texturesExtension)
        {
            if (!ext_composition_layers_GetViewConfigurationData(out uint width, out uint height, out _))
                return false;

            if (!IsTextureSizeValidForView(texturesExtension.LeftTexture, width, height))
            {
                Debug.LogError($"Left Texture size {texturesExtension.LeftTexture.width}x{texturesExtension.LeftTexture.height} is larger than the view configuration size {width}x{height}");
                return false;
            }

            if (!IsTextureSizeValidForView(texturesExtension.RightTexture, width, height))
            {
                Debug.LogError($"Right Texture size {texturesExtension.RightTexture.width}x{texturesExtension.RightTexture.height} is larger than the view configuration size {width}x{height}");
                return false;
            }

            return true;
        }

        bool IsTextureSizeValidForView(Texture texture, uint width, uint height)
        {
            if (texture.width > width || texture.height > height)
                return false;
            return true;
        }

        [AOT.MonoPInvokeCallback(typeof(OpenXRLayerUtility.StereoRenderTextureIdsCallbackDelegate))]
        static void OnStereoRenderTextureIdsCallback(int layerId, uint leftTexId, uint rightTexId)
        {
            if (m_ProjectionData == null)
                return;

            if (m_ProjectionData.TryGetValue(layerId, out var container))
            {
                container.renderTextureIds ??= new uint[2];
                container.renderTextureIds[0] = leftTexId;
                container.renderTextureIds[1] = rightTexId;
            }
        }

        [DllImport("UnityOpenXR")]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool ext_composition_layers_GetViewConfigurationData(out uint width, out uint height, out uint sampleCount);

        [DllImport("UnityOpenXR")]
        [return: MarshalAs(UnmanagedType.U1)]
        internal unsafe static extern bool ext_composition_layers_GetStereoViews(XrView* view1, XrView* view2);
    }
}


#endif
