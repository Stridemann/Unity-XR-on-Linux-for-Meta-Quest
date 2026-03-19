//#define VERBOSE_LOGGING
#if XR_COMPOSITION_LAYERS
#if VERBOSE_LOGGING
using System.Runtime.CompilerServices;
#endif
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.XR.CompositionLayers.Extensions;
using Unity.XR.CompositionLayers.Services;
using UnityEngine.XR.OpenXR.NativeTypes;
#if UNITY_VIDEO
using UnityEngine.Video;
#endif

namespace UnityEngine.XR.OpenXR.CompositionLayers
{
    /// <summary>
    /// Provides a base implementation for the <see cref="OpenXRLayerProvider.ILayerHandler"/> interface.
    /// You can implement the required methods of this abstract class to create a concrete layer handler.
    /// </summary>
    /// <remarks>
    /// The <see cref="OpenXRLayerProvider.ILayerHandler"/> methods that this class implements handle adding
    /// and removing composition layers from native arrays, swap chain creation dispatching and other tasks
    /// required by the Unity side of the API.
    ///
    /// The abstract methods that you must implement handle the custom aspects of your layer. These methods include:
    ///
    /// * <see cref="CreateSwapchain(CompositionLayerManager.LayerInfo, out SwapchainCreateInfo)"/>
    /// * <see cref="CreateNativeLayer(CompositionLayerManager.LayerInfo, OpenXRCustomLayerHandler{T}.SwapchainCreatedOutput, out T)"/>
    /// * <see cref="ModifyNativeLayer(CompositionLayerManager.LayerInfo, ref T)"/>
    ///
    /// You are not required to implement a custom layer handler based on this abstract class, but doing so should be
    /// easier than implementing the <see cref="OpenXRLayerProvider.ILayerHandler"/> interface in its entirety.
    ///
    /// You must register your concrete layer handler object with
    /// <see cref="OpenXRLayerProvider.RegisterLayerHandler(Type, OpenXRLayerProvider.ILayerHandler)"/>.
    /// </remarks>
    /// <typeparam name="T">The native OpenXR structure of the composition layer to handle.</typeparam>
    public abstract class OpenXRCustomLayerHandler<T> : OpenXRLayerProvider.ILayerHandler, IDisposable where T : struct
    {
        /// <summary>
        /// Container for swapchain related information that may be needed during the creation of the native OpenXR composition layer struct.
        /// </summary>
        protected struct SwapchainCreateInfo
        {
            /// <summary>
            /// Native structure for the swapchain creation info.
            /// </summary>
            public XrSwapchainCreateInfo nativeStruct;

            /// <summary>
            /// Tells if swapchain is using an external surface.
            /// </summary>
            [MarshalAs(UnmanagedType.I1)]
            public bool isExternalSurface;

            /// <summary>
            /// Tells if swapchain should be stereo.
            /// </summary>
            public bool isStereo;

            /// <summary>
            /// Initializes and returns an instance of SwapchainCreateInfo with the provided parameters.
            /// </summary>
            /// <param name="xrSwapchainCreateInfo">Native structure for the swapchain creation info.</param>
            /// <param name="isExternalSurface">Tells if swapchain is using an external surface.</param>
            /// <param name="isStereo">Tells if swapchain should be stereo.</param>
            public SwapchainCreateInfo(
                XrSwapchainCreateInfo xrSwapchainCreateInfo, bool isExternalSurface = false, bool isStereo = false)
            {
                this.nativeStruct = xrSwapchainCreateInfo;
                this.isExternalSurface = isExternalSurface;
                this.isStereo = isStereo;
            }

            /// <summary>
            /// Implicit conversion with just a native XrSwapchainCreateInfo struct.
            /// </summary>
            /// <param name="createInfo">The native struct to convert.</param>
            public static implicit operator SwapchainCreateInfo(XrSwapchainCreateInfo createInfo) => new(createInfo);
        }

        /// <summary>
        /// Container for swapchain related information that may be needed during the creation of the native OpenXR composition layer struct.
        /// </summary>
        protected struct SwapchainCreatedOutput
        {
            /// <summary>
            /// The handle of the created swapchain.
            /// Can be used to initialize the swapchain member of a native OpenXR composition layer struct.
            /// </summary>
            public ulong handle;

            /// <summary>
            /// The second handle of the created stereo swapchain.
            /// Can be used to initialize the swapchain member of a native OpenXR composition layer struct.
            /// </summary>
            public ulong secondStereoHandle;
        }

        /// <summary>
        /// Container for grouping render information for each composition layer.
        /// </summary>
        class LayerRenderInfo
        {
            public Dictionary<uint, SwapchainImageInfo> SwapchainImageInfos = new();
            public Texture Texture;
#if UNITY_VIDEO
            public VideoPlayer VideoPlayer;
#endif
            public MeshCollider MeshCollider;

            public uint RenderTextureId;

            public bool IsActiveLayer;

            public bool IsNewTexture;

            public class SwapchainImageInfo
            {
                public RenderTexture RenderTexture;
                public bool IsWritten;
            }
        }

        /// <summary>
        /// Initializes and returns an instance of this <c>OpenXRCustomLayerHandler&lt;T&gt;</c> while also setting the singleton instance member.
        /// </summary>
        protected OpenXRCustomLayerHandler() => Instance = this;

        /// <summary>
        /// Singleton instance of this specific handler.
        /// </summary>
        protected static OpenXRCustomLayerHandler<T> Instance;

        /// <summary>
        /// Deinitializes this instance of <c>OpenXRCustomLayerHandler&lt;T&gt;</c>.
        /// </summary>
        ~OpenXRCustomLayerHandler() => Dispose(false);

        /// <summary>
        /// Override this method to create the <see cref="XrSwapchainCreateInfo"/> struct that is passed to OpenXR
        /// to create a swapchain.
        /// </summary>
        /// <remarks>
        /// To add extensions when constructing the <see cref="XrSwapchainCreateInfo"/> struct, initialize
        /// the <c>Next</c> pointer with
        /// <see cref="OpenXRLayerUtility.GetExtensionsChain(CompositionLayerManager.LayerInfo, Unity.XR.CompositionLayers.CompositionLayerExtension.ExtensionTarget)"/>.
        /// </remarks>
        /// <example>
        /// Constructs an XrSwapchainCreateInfo struct with some members using component data.
        /// <code>
        /// <![CDATA[
        /// protected override bool CreateSwapchain(CompositionLayerManager.LayerInfo layerInfo, out SwapchainCreateInfo swapchainCreateInfo)
        /// {
        ///    var texturesExtension = layerInfo.Layer.GetComponent<TexturesExtension>();
        ///    unsafe
        ///    {
        ///        swapchainCreateInfo = new XrSwapchainCreateInfo()
        ///        {
        ///             Type = (uint)XrStructureType.XR_TYPE_SWAPCHAIN_CREATE_INFO,
        ///             Next = OpenXRLayerUtility.GetExtensionsChain(layerInfo, CompositionLayerExtension.ExtensionTarget.Swapchain),
        ///             CreateFlags = 0,
        ///             UsageFlags = (ulong)(XrSwapchainUsageFlags.XR_SWAPCHAIN_USAGE_SAMPLED_BIT | XrSwapchainUsageFlags.XR_SWAPCHAIN_USAGE_COLOR_ATTACHMENT_BIT),
        ///             Format = OpenXRLayerUtility.GetDefaultColorFormat(),
        ///             SampleCount = 1,
        ///             Width = (uint)texturesExtension.LeftTexture.width,
        ///             Height = (uint)texturesExtension.LeftTexture.height,
        ///             FaceCount = 1,
        ///             ArraySize = 1,
        ///             MipCount = (uint)texturesExtension.LeftTexture.mipmapCount,
        ///         };
        ///     }
        ///     return true;
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// <param name="layerInfo"> Container for the instance id and CompositionLayer component of the composition layer
        /// that was just created.</param>
        /// <param name="swapchainCreateInfo"> An <c>XrSwapchainCreateInfo</c> object created and initialized by the concrete implementation of this method.</param>
        /// <returns> A bool indicating success or failure.</returns>
        protected abstract bool CreateSwapchain(
            CompositionLayerManager.LayerInfo layerInfo, out SwapchainCreateInfo swapchainCreateInfo);

        /// <summary>
        /// Override this method to create the native composition layer struct of type T that is passed to OpenXR.
        /// A swapchain info struct is provided so your layer handler has access to any needed swapchain information.
        /// </summary>
        /// <remarks>
        /// To add extensions when constructing the <see cref="XrSwapchainCreateInfo"/> struct, initialize
        /// the <c>Next</c> pointer with <see cref="OpenXRLayerUtility.GetExtensionsChain(CompositionLayerManager.LayerInfo, Unity.XR.CompositionLayers.CompositionLayerExtension.ExtensionTarget)"/>.
        ///
        /// If your struct needs any XrSpace relative info you can use <see cref="OpenXRLayerUtility.GetCurrentAppSpace"/>
        /// to get the current app space.
        /// </remarks>
        /// <example>
        /// Constructs an XrCompositionLayerQuad struct with some members using component data.
        /// <code>
        /// <![CDATA[
        /// protected override bool CreateNativeLayer(CompositionLayerManager.LayerInfo layerInfo, SwapchainCreatedOutput swapchainOutput, out XrCompositionLayerQuad nativeLayer)
        /// {
        ///     var texturesExtension = layerInfo.Layer.GetComponent<TexturesExtension>();
        ///     var transform = layerInfo.Layer.GetComponent<Transform>();
        ///
        ///     unsafe
        ///     {
        ///         var data = layerInfo.Layer.LayerData as QuadLayerData;
        ///
        ///         nativeLayer = new XrCompositionLayerQuad()
        ///         {
        ///             Type = 36,
        ///             Next = OpenXRLayerUtility.GetExtensionsChain(layerInfo, CompositionLayerExtension.ExtensionTarget.Layer),
        ///             LayerFlags = 0x00000002,
        ///             Space = OpenXRLayerUtility.GetCurrentAppSpace(),
        ///             EyeVisibility = 0,
        ///             SubImage = new SwapchainSubImage()
        ///             {
        ///                 Swapchain = swapchainOutput.handle,
        ///                 ImageRect = new XrRect2Di()
        ///                 {
        ///                     offset = new XrOffset2Di() { x = 0, y = 0 },
        ///                     extent = new XrExtent2Di()
        ///                     {
        ///                         width = texturesExtension.LeftTexture.width,
        ///                         height = texturesExtension.LeftTexture.height
        ///                     }
        ///                 },
        ///                 ImageArrayIndex = 0
        ///             },
        ///             Pose = new XrPosef(transform.position, transform.rotation),
        ///             Size = new XrExtend2Df()
        ///             {
        ///                 width = data.GetScaledSize(transform.lossyScale).x,
        ///                 height = data.GetScaledSize(transform.lossyScale).y
        ///             }
        ///         };
        ///     }
        ///     return true;
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// <param name="layerInfo"> Container for the instance id and CompositionLayer component of the composition layer
        /// that was just created.</param>
        /// <param name="swapchainOutput"> Information regarding the swapchain that was created for this layer,
        /// such as the associated swapchain handle.</param>
        /// <param name="nativeLayer"> An object of type T that is created and initialized by the concrete implementation of this method.</param>
        /// <returns> A bool indicating success or failure.</returns>
        protected abstract bool CreateNativeLayer(
            CompositionLayerManager.LayerInfo layerInfo, SwapchainCreatedOutput swapchainOutput, out T nativeLayer);

        /// <summary>
        /// Override this method to modify a native composition layer struct in response to changes on the associated
        /// <see cref="Unity.XR.CompositionLayers.Layers.LayerData"/> object or any extension components on the
        /// <see cref="Unity.XR.CompositionLayers.CompositionLayer"/> GameObject.
        /// </summary>
        /// <remarks>
        /// You must reinitialize the Next pointer with
        /// <see cref="OpenXRLayerUtility.GetExtensionsChain(CompositionLayerManager.LayerInfo, Unity.XR.CompositionLayers.CompositionLayerExtension.ExtensionTarget)"/>
        /// to get any potential updates from extension components.
        /// </remarks>
        /// <example>
        /// Modifies an XrCompositionLayerQuad struct with new transform and extension information.
        /// <code>
        /// <![CDATA[
        /// protected override bool ModifyNativeLayer(CompositionLayerManager.LayerInfo layerInfo, ref XrCompositionLayerQuad nativeLayer)
        /// {
        ///     var data = layerInfo.Layer.LayerData as QuadLayerData;
        ///     var transform = layerInfo.Layer.GetComponent<Transform>();
        ///
        ///     nativeLayer.Pose = new XrPosef(transform.position, transform.rotation);
        ///     nativeLayer.Size = new XrExtend2Df()
        ///     {
        ///         width = data.GetScaledSize(transform.lossyScale).x,
        ///         height = data.GetScaledSize(transform.lossyScale).y
        ///     };
        ///
        ///     unsafe
        ///     {
        ///         nativeLayer.Next = OpenXRLayerUtility.GetExtensionsChain(layerInfo, CompositionLayerExtension.ExtensionTarget.Layer);
        ///     }
        ///     return true;
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// <param name="layerInfo"> Container for the instance id and CompositionLayer component of the composition
        /// layer that was modified.</param>
        /// <param name="nativeLayer"> A reference to the native OpenXR structure of the composition layer that was modified.
        /// The concrete implementation of this method should update the values of the structure as appropriate.</param>
        /// <returns> A bool indicating success or failure.</returns>
        protected abstract bool ModifyNativeLayer(CompositionLayerManager.LayerInfo layerInfo, ref T nativeLayer);

        /// <summary>
        /// Mapping of instance ids and native layer structs to help determine what layers are currently set to be active.
        /// </summary>
        protected Dictionary<int, T> m_nativeLayers = new();

        /// <summary>
        /// Thread safe queue used to dispatch callbacks that may come from other threads such as the swapchain creation
        /// on the graphics thread.
        /// </summary>
        protected ConcurrentQueue<Action> actionsForMainThread = new();

        bool isRegisteredOnBeforeRender;
        Dictionary<int, LayerRenderInfo> m_RenderInfos = new();
        Dictionary<int, CompositionLayerManager.LayerInfo> m_LayerInfos = new();
        NativeArray<T> m_ActiveNativeLayers;
        NativeArray<int> m_ActiveNativeLayerOrders;
        int m_ActiveNativeLayerCount;

        /// <summary>
        /// Implements the <see cref="OpenXRLayerProvider.ILayerHandler"/> method that is called by the
        /// <see cref="OpenXRLayerProvider"/> during the Unity update loop.
        /// </summary>
        /// <remarks>
        /// This implementation carries out two tasks. It dequeues actions for the main thread like dispatch when
        /// the swapchain has been
        /// created, and it adds all the active layers to the <c>endFrameInfo</c> struct in the native UnityOpenXR lib.
        /// </remarks>
        public virtual void OnUpdate()
        {
            while (actionsForMainThread.Count > 0)
            {
                if (actionsForMainThread.TryDequeue(out var action))
                    action();
            }

            unsafe
            {
                if (m_ActiveNativeLayerCount > 0 && CompositionLayerManager.Instance != null)
                    OpenXRLayerUtility.AddActiveLayersToEndFrame(
                        m_ActiveNativeLayers.GetUnsafePtr(),
                        m_ActiveNativeLayerOrders.GetUnsafePtr(),
                        m_ActiveNativeLayerCount,
                        UnsafeUtility.SizeOf<T>());
            }

            m_ActiveNativeLayerCount = 0;
        }

        /// <summary>
        /// Implements the <see cref="OpenXRLayerProvider.ILayerHandler"/> method that is called by the
        /// <see cref="OpenXRLayerProvider"/> when a new layer has been created.
        /// This implementation triggers the creation of a swapchain before the actual native layer struct is created.
        /// </summary>
        /// <param name="layerInfo"> Container for the instance id and CompositionLayer component of the composition layer
        /// being created.</param>
        public void CreateLayer(CompositionLayerManager.LayerInfo layerInfo)
        {
            if (!isRegisteredOnBeforeRender)
            {
                Application.onBeforeRender += OnBeforeRender;
                isRegisteredOnBeforeRender = true;
            }

            CreateSwapchainAsync(layerInfo);
        }

        void OnBeforeRender()
        {
            foreach(var container in m_RenderInfos.Values)
            {
                if (!container.IsActiveLayer)
                    continue;

                var renderTexture = OpenXRLayerUtility.FindRenderTexture(container.RenderTextureId);

                LayerRenderInfo.SwapchainImageInfo swapchainImageInfo;
                if (!container.SwapchainImageInfos.ContainsKey(container.RenderTextureId))
                {
                    swapchainImageInfo = new LayerRenderInfo.SwapchainImageInfo
                    {
                        RenderTexture = renderTexture, IsWritten = false
                    };
                    container.SwapchainImageInfos.Add(container.RenderTextureId, swapchainImageInfo);
                }

                swapchainImageInfo = container.SwapchainImageInfos[container.RenderTextureId];

                var isVideo = false;
#if UNITY_VIDEO
                isVideo = container.VideoPlayer != null && container.VideoPlayer.enabled;
#endif
                var isRenderTexture = container.Texture is RenderTexture;

                // Layers that have a new texture or have video or are a RenderTexture must always have their swapchain image written to.
                if (container.IsNewTexture || isVideo || isRenderTexture)
                {
                    OpenXRLayerUtility.WriteToRenderTexture(container.Texture, renderTexture);
                    swapchainImageInfo.IsWritten = true;
                }

                // For all other layers only write to the swapchain image if it has not already been written.
                else if (!swapchainImageInfo.IsWritten)
                {
                    OpenXRLayerUtility.WriteToRenderTexture(container.Texture, renderTexture);
                    swapchainImageInfo.IsWritten = true;
                }
            }
        }

        /// <summary>
        /// Implements the <see cref="OpenXRLayerProvider.ILayerHandler"/> method that is called by the
        /// <see cref="OpenXRLayerProvider"/> when a layer or attached extension has been modified.
        /// This implementation asks the subclass for any changes that must be made to the layer via
        /// <see cref="ModifyNativeLayer(CompositionLayerManager.LayerInfo, ref T)"/>
        /// by sending a reference to the native layer struct.
        /// </summary>
        /// <param name="layerInfo"> Container for the instance id and CompositionLayer component of the composition layer
        /// that was modified.</param>
        public virtual void ModifyLayer(CompositionLayerManager.LayerInfo layerInfo)
        {
            var texturesExtension = layerInfo.Layer.GetComponent<TexturesExtension>();

            if (!m_nativeLayers.TryGetValue(layerInfo.Id, out var nativeLayer))
            {
                if (texturesExtension != null && texturesExtension.TextureAdded)
                {
                    texturesExtension.TextureAdded = false;
                    CreateLayer(layerInfo);
                }
                return;
            }

            var success = ModifyNativeLayer(layerInfo, ref nativeLayer);
            if (success)
                m_nativeLayers[layerInfo.Id] = nativeLayer;
        }

        /// <summary>
        /// Implements the <see cref="OpenXRLayerProvider.ILayerHandler"/> method that is called by the
        /// <see cref="OpenXRLayerProvider"/> when a layer is destroyed or disabled.
        /// </summary>
        /// <param name="removedLayerId"> The instance id of the CompositionLayer component that was removed.</param>
        public virtual void RemoveLayer(int removedLayerId)
        {
            OpenXRLayerUtility.ReleaseSwapchain(removedLayerId);
            m_nativeLayers.Remove(removedLayerId);
            m_LayerInfos.Remove(removedLayerId);
            m_RenderInfos.Remove(removedLayerId);
        }

        /// <summary>
        /// Implements the <see cref="OpenXRLayerProvider.ILayerHandler"/> method that is called by the
        /// <see cref="OpenXRLayerProvider"/> when a layer is considered to be currently active.
        /// </summary>
        /// <param name="layerInfo"> Container for the instance id and CompositionLayer component of the composition layer
        /// being set to active.</param>
        public virtual void SetActiveLayer(CompositionLayerManager.LayerInfo layerInfo)
        {
            if (!m_nativeLayers.TryGetValue(layerInfo.Id, out var nativeLayer))
                return;

            var success = ActiveNativeLayer(layerInfo, ref nativeLayer);
            if (!success)
                return;

            m_nativeLayers[layerInfo.Id] = nativeLayer;
            ResizeNativeArrays();
            m_ActiveNativeLayers[m_ActiveNativeLayerCount] = m_nativeLayers[layerInfo.Id];
            m_ActiveNativeLayerOrders[m_ActiveNativeLayerCount] = layerInfo.Layer.Order;
            ++m_ActiveNativeLayerCount;
        }

        /// <summary>
        /// Implements method from <see cref="IDisposable"/> that is called by the <see cref="OpenXRLayerProvider"/>
        /// when this custom layer handler instance is disposed.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Clears all maps and disposes any created native arrays.
        /// </summary>
        /// <param name="disposing">`true` if this method was called by `Dispose`.
        /// Otherwise, this method was called by the class finalizer.</param>
        protected virtual void Dispose(bool disposing)
        {
#if VERBOSE_LOGGING
            Debug.Log($"Disposing {GetType().Name} with hash: {RuntimeHelpers.GetHashCode(this)}");
#endif
            if (disposing)
            {
                m_LayerInfos.Clear();
                m_nativeLayers.Clear();
                m_RenderInfos.Clear();
            }

            if (m_ActiveNativeLayers.IsCreated)
                m_ActiveNativeLayers.Dispose();
            if (m_ActiveNativeLayerOrders.IsCreated)
                m_ActiveNativeLayerOrders.Dispose();

            if (isRegisteredOnBeforeRender)
            {
                Application.onBeforeRender -= OnBeforeRender;
                isRegisteredOnBeforeRender = false;
            }
        }

        /// <summary>
        /// Calls <see cref="CreateSwapchain(CompositionLayerManager.LayerInfo, out SwapchainCreateInfo)"/> to create a
        /// <see cref="SwapchainCreateInfo"/> struct that is then passed to the
        /// UnityOpenXR lib to actually create the swapchain on the graphics thread.
        /// The static <see cref="OnCreatedSwapchainCallback(int, ulong)"/> method is passed as a callback and invoked when
        /// the swapchain has been created.
        /// </summary>
        /// <param name="layerInfo"> Container for the instance id and CompositionLayer component of the composition layer
        /// that was just created.</param>
        protected virtual void CreateSwapchainAsync(CompositionLayerManager.LayerInfo layerInfo)
        {
            m_LayerInfos[layerInfo.Id] = layerInfo;
            var success = CreateSwapchain(layerInfo, out var swapChainInfo);
            if (!success)
                return;

            if (swapChainInfo.isStereo)
                OpenXRLayerUtility.CreateStereoSwapchain(
                    layerInfo.Id, swapChainInfo.nativeStruct, OnCreatedStereoSwapchainCallback);
            else
                OpenXRLayerUtility.CreateSwapchain(
                    layerInfo.Id,
                    swapChainInfo.nativeStruct,
                    swapChainInfo.isExternalSurface,
                    OnCreatedSwapchainCallback);
        }

        /// <summary>
        /// This method is dispatched to the main thread inside <see cref="OnCreatedSwapchainCallback(int, ulong)"/>
        /// and asks this subclass to create the native layer struct by invoking
        /// <see cref="CreateNativeLayer(CompositionLayerManager.LayerInfo, SwapchainCreatedOutput, out T)"/>.
        /// </summary>
        /// <param name="layerInfo"> Container for the instance id and CompositionLayer component of the composition layer
        /// that was just created.</param>
        /// <param name="swapchainOutput"> Information regarding the swapchain that was created for this layer, such as
        /// the associated swapchain handle.</param>
        protected virtual void OnCreatedSwapchain(
            CompositionLayerManager.LayerInfo layerInfo, SwapchainCreatedOutput swapchainOutput)
        {
            var success = CreateNativeLayer(layerInfo, swapchainOutput, out var nativeLayer);
            if (success)
                m_nativeLayers[layerInfo.Id] = nativeLayer;
#if VERBOSE_LOGGING
            else
                Debug.LogError("CreateNativeLayer failed");
#endif
        }

        /// <summary>
        /// Ensures that the native arrays are of the same size as the m_nativeLayers map.
        /// </summary>
        protected virtual void ResizeNativeArrays()
        {
            if (!m_ActiveNativeLayers.IsCreated && !m_ActiveNativeLayerOrders.IsCreated)
            {
                m_ActiveNativeLayers = new NativeArray<T>(m_nativeLayers.Count, Allocator.Persistent);
                m_ActiveNativeLayerOrders = new NativeArray<int>(m_nativeLayers.Count, Allocator.Persistent);
                return;
            }

            Assertions.Assert.AreEqual(m_ActiveNativeLayers.Length, m_ActiveNativeLayerOrders.Length);

            if (m_ActiveNativeLayers.Length < m_nativeLayers.Count)
            {
                var newLayerArray = new NativeArray<T>(m_nativeLayers.Count, Allocator.Persistent);
                NativeArray<T>.Copy(m_ActiveNativeLayers, newLayerArray, m_ActiveNativeLayers.Length);
                m_ActiveNativeLayers.Dispose();
                m_ActiveNativeLayers = newLayerArray;

                var newOrderArray = new NativeArray<int>(m_nativeLayers.Count, Allocator.Persistent);
                NativeArray<int>.Copy(m_ActiveNativeLayerOrders, newOrderArray, m_ActiveNativeLayerOrders.Length);
                m_ActiveNativeLayerOrders.Dispose();
                m_ActiveNativeLayerOrders = newOrderArray;
            }
        }

        /// <summary>
        /// Override this method to modify a native composition layer struct in response to when it is active.
        /// An active composition layer will invoke this every frame.
        /// </summary>
        /// <param name="layerInfo">Container for the instance id and CompositionLayer component of the composition
        /// layer that is active.</param>
        /// <param name="nativeLayer">A reference to the native OpenXR structure of the composition layer that is active.</param>
        /// <returns>Bool indicating success or failure.
        /// A failure case will result in the native composition layer struct not being added into the final XrFrameEndInfo struct.</returns>
        protected virtual bool ActiveNativeLayer(CompositionLayerManager.LayerInfo layerInfo, ref T nativeLayer)
        {
            var texturesExtension = layerInfo.Layer.GetComponent<TexturesExtension>();
            if (texturesExtension == null
                || texturesExtension.LeftTexture == null
                || texturesExtension.sourceTexture == TexturesExtension.SourceTextureEnum.AndroidSurface)
                return true;

            if (m_RenderInfos.TryGetValue(layerInfo.Id, out var container))
            {
                container.IsNewTexture = container.Texture != texturesExtension.LeftTexture;

                if (container.IsNewTexture)
                {
                    // If we have a new texture with different dimensions then we need to release the current swapchain and create another.
                    // This is an async procedure that also creates a new native layer object.
                    if (container.Texture.width != texturesExtension.LeftTexture.width
                        || container.Texture.height != texturesExtension.LeftTexture.height)
                    {
                        RemoveLayer(layerInfo.Id);
                        CreateSwapchainAsync(layerInfo);
                        return false;
                    }
                    else
                    {
                        container.Texture = texturesExtension.LeftTexture;
                    }
#if UNITY_VIDEO
                    container.VideoPlayer = layerInfo.Layer.GetComponent<VideoPlayer>();
#endif
                    container.MeshCollider = layerInfo.Layer.GetComponent<MeshCollider>();
                    container.SwapchainImageInfos.Clear();
                }
            }
            else
            {
                var layerRenderInfo = new LayerRenderInfo
                {
                    Texture = texturesExtension.LeftTexture,
#if UNITY_VIDEO
                    VideoPlayer = layerInfo.Layer.GetComponent<VideoPlayer>(),
#endif
                    MeshCollider = layerInfo.Layer.GetComponent<MeshCollider>()
                };

                m_RenderInfos.Add(layerInfo.Id, layerRenderInfo);
            }

            m_RenderInfos[layerInfo.Id].IsActiveLayer = true;

            OpenXRLayerUtility.RequestRenderTextureId(layerInfo.Id, OnRenderTextureIdIdCallback);

            return true;
        }

        [AOT.MonoPInvokeCallback(typeof(OpenXRLayerUtility.RenderTextureIdCallbackDelegate))]
        static void OnRenderTextureIdIdCallback(int layerId, uint texId)
        {
            if (Instance == null)
                return;

            if (Instance.m_RenderInfos.TryGetValue(layerId, out _))
            {
                Instance.m_RenderInfos[layerId].RenderTextureId = texId;
            }
        }

        [AOT.MonoPInvokeCallback(typeof(OpenXRLayerUtility.SwapchainCallbackDelegate))]
        static void OnCreatedSwapchainCallback(int layerId, ulong swapchainHandle)
        {
            Instance?.actionsForMainThread.Enqueue(() =>
            {
                Instance.OnCreatedSwapchain(
                    Instance.m_LayerInfos[layerId],
                    new SwapchainCreatedOutput { handle = swapchainHandle });
            });
        }

        [AOT.MonoPInvokeCallback(typeof(OpenXRLayerUtility.StereoSwapchainCallbackDelegate))]
        static void OnCreatedStereoSwapchainCallback(int layerId, ulong swapchainHandleLeft, ulong swapchainHandleRight)
        {
            Instance?.actionsForMainThread.Enqueue(() =>
            {
                Instance.OnCreatedSwapchain(
                    Instance.m_LayerInfos[layerId],
                    new SwapchainCreatedOutput
                    {
                        handle = swapchainHandleLeft, secondStereoHandle = swapchainHandleRight
                    });
            });
        }
    }
}
#endif
