using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.XR.OpenXR.Features;

namespace UnityEngine.XR.OpenXR
{
    public partial class OpenXRSettings : ISerializationCallbackReceiver
    {
        /// <summary>
        /// Stereo rendering mode.
        /// </summary>
        public enum RenderMode
        {
            /// <summary>
            /// Submit separate draw calls for each eye.
            /// </summary>
            MultiPass,

            /// <summary>
            /// Submit one draw call for both eyes.
            /// </summary>
            SinglePassInstanced,
        };

        /// <summary>
        /// Stereo rendering mode.
        /// </summary>
        [SerializeField]
        RenderMode m_renderMode = RenderMode.SinglePassInstanced;

        /// <summary>
        /// Runtime Stereo rendering mode.
        /// </summary>
        public RenderMode renderMode
        {
            get
            {
                if (OpenXRLoaderBase.Instance != null)
                    return Internal_GetRenderMode();
                else
                    return m_renderMode;
            }
            set
            {
                if (OpenXRLoaderBase.Instance != null)
                    Internal_SetRenderMode(value);
                else
                    m_renderMode = value;
            }
        }

        /// <summary>
        /// Modes describing whether to prioritize minimzing Input polling latency, or rendering latency.
        /// </summary>
        public enum LatencyOptimization
        {
            /// <summary>
            /// Prioritize reducing rendering latency.
            /// </summary>
            PrioritizeRendering,

            /// <summary>
            /// Prioritize reducing input latency.
            /// </summary>
            PrioritizeInputPolling,
        };

        [SerializeField]
        private LatencyOptimization m_latencyOptimization = LatencyOptimization.PrioritizeRendering;

        /// <summary>
        /// Specifies where the xrWaitFrame call should occur to optimize latency.
        /// Note that modifying this value at runtime will not do anything. This property can be modified in the Unity Editor through
        /// the OpenXR settings, or programatically during the build process.
        /// </summary>
        /// <remarks>
        /// When set to <see cref="LatencyOptimization.PrioritizeRendering"/>, the xrWaitFrame call will be moved to before rendering to reduce rendering latency.
        /// When set to <see cref="LatencyOptimization.PrioritizeInputPolling"/>, the xrWaitFrame call will be moved to before input polling to reduce input latency.
        /// </remarks>
        /// <seealso cref="LatencyOptimization"/>
        public LatencyOptimization latencyOptimization
        {
            get
            {
                if (OpenXRLoaderBase.Instance != null)
                    return Internal_GetLatencyOptimization();
                else
                    return m_latencyOptimization;
            }
            set
            {
                m_latencyOptimization = value;
            }
        }

        [SerializeField]
        private bool m_autoColorSubmissionMode = true;

        /// <summary>
        /// Automatically select the default color submission mode.
        /// </summary>
        public bool autoColorSubmissionMode
        {
            get => m_autoColorSubmissionMode;
            set => m_autoColorSubmissionMode = value;
        }

        [SerializeField]
        private ColorSubmissionModeList m_colorSubmissionModes = new ColorSubmissionModeList();

        /// <summary>
        /// Gets or sets the preference for what color format the OpenXR display swapchain should use.
        /// </summary>
        public ColorSubmissionModeGroup[] colorSubmissionModes
        {
            get
            {
                if (m_autoColorSubmissionMode)
                    return new[] { OpenXRSettings.kDefaultColorMode };

                if (OpenXRLoaderBase.Instance != null)
                {
                    int arraySize = Internal_GetColorSubmissionModes(null, 0);
                    int[] intModes = new int[arraySize];
                    Internal_GetColorSubmissionModes(intModes, arraySize);
                    return intModes.Select(i => (ColorSubmissionModeGroup)i).ToArray();
                }
                else
                    return m_colorSubmissionModes.m_List;
            }
            set
            {
                if (OpenXRLoaderBase.Instance != null)
                    Internal_SetColorSubmissionModes(
                        value.Select(e => (int)e).ToArray(),
                        value.Length
                    );
                else
                    m_colorSubmissionModes.m_List = value;
            }
        }

        /// <summary>
        /// Runtime Depth submission mode.
        /// </summary>
        public enum DepthSubmissionMode
        {
            /// <summary>
            /// No depth is submitted to the OpenXR compositor.
            /// </summary>
            None,

            /// <summary>
            /// 16-bit depth is submitted to the OpenXR compositor.
            /// </summary>
            Depth16Bit,

            /// <summary>
            /// 24-bit depth is submitted to the OpenXR compositor.
            /// </summary>
            Depth24Bit,
        }

        /// <summary>
        /// Different Internal Unity APIs to use in the backend.
        /// </summary>
        public enum BackendFovationApi : byte
        {
            /// <summary>
            /// Use the legacy Built-in API for Foveation. This is the only option for Built-in.
            /// </summary>
            Legacy = 0,

            /// <summary>
            /// Use the Foveation for SRP API.
            /// </summary>
            SRPFoveation = 1,
        }

        /// <summary>
        /// Space Warp motion vector texture format.
        /// </summary>
        public enum SpaceWarpMotionVectorTextureFormat
        {
            /// <summary>
            /// RGBA16f texture format
            /// </summary>
            RGBA16f,

            /// <summary>
            /// RG16f texture format
            /// </summary>
            RG16f,
        }

        /// <summary>
        /// Enables XR_KHR_composition_layer_depth if possible and resolves or submits depth to OpenXR runtime.
        /// </summary>
        [SerializeField]
        private DepthSubmissionMode m_depthSubmissionMode = DepthSubmissionMode.None;

        /// <summary>
        /// Enables XR_KHR_composition_layer_depth if possible and resolves or submits depth to OpenXR runtime.
        /// </summary>
        public DepthSubmissionMode depthSubmissionMode
        {
            get
            {
                if (OpenXRLoaderBase.Instance != null)
                    return Internal_GetDepthSubmissionMode();
                else
                    return m_depthSubmissionMode;
            }
            set
            {
                if (OpenXRLoaderBase.Instance != null)
                    Internal_SetDepthSubmissionMode(value);
                else
                    m_depthSubmissionMode = value;
            }
        }


        /// <summary>
        /// Space Warp motion vector texture format (default is RGBA16f).
        /// </summary>
        [SerializeField]
        private SpaceWarpMotionVectorTextureFormat m_spacewarpMotionVectorTextureFormat = SpaceWarpMotionVectorTextureFormat.RGBA16f;

        /// <summary>
        /// Selects the motion vector texture format for Space Warp.
        /// </summary>
        public SpaceWarpMotionVectorTextureFormat spacewarpMotionVectorTextureFormat
        {
            get
            {
                if (OpenXRLoaderBase.Instance != null)
                    return Internal_GetSpaceWarpMotionVectorTextureFormat();
                else
                    return m_spacewarpMotionVectorTextureFormat;
            }
            set
            {
                if (OpenXRLoaderBase.Instance != null)
                    Internal_SetSpaceWarpMotionVectorTextureFormat(value);
                else
                    m_spacewarpMotionVectorTextureFormat = value;
            }
        }

        [SerializeField]
        private bool m_optimizeBufferDiscards = false;

        /// <summary>
        /// Optimization that allows 4x MSAA textures to be memoryless on Vulkan
        /// </summary>
        public bool optimizeBufferDiscards
        {
            get { return m_optimizeBufferDiscards; }
            set
            {
                if (OpenXRLoaderBase.Instance != null)
                    Internal_SetOptimizeBufferDiscards(value);
                else
                    m_optimizeBufferDiscards = value;
            }
        }

        void ApplyRenderSettings()
        {
            Internal_SetSymmetricProjection(m_symmetricProjection);
#if UNITY_6000_1_OR_NEWER
            Internal_SetMultiviewRenderRegionsOptimizationMode(m_multiviewRenderRegionsOptimizationMode);
#endif
#if UNITY_6000_2_OR_NEWER
            Internal_SetUseOpenXRPredictedTime(m_useOpenXRPredictedTime);
#endif
#if UNITY_2023_2_OR_NEWER
            Internal_SetUsedFoveatedRenderingApi(m_foveatedRenderingApi);
#endif
            Internal_SetRenderMode(m_renderMode);
            Internal_SetLatencyOptimization(m_latencyOptimization);
            Internal_SetColorSubmissionModes(
                m_colorSubmissionModes.m_List.Select(e => (int)e).ToArray(),
                m_colorSubmissionModes.m_List.Length
            );
            Internal_SetDepthSubmissionMode(m_depthSubmissionMode);
            Internal_SetSpaceWarpMotionVectorTextureFormat(m_spacewarpMotionVectorTextureFormat);
            Internal_SetOptimizeBufferDiscards(m_optimizeBufferDiscards);
        }

        void LogRendererSettings(ulong diagnosticsSectionHandle)
        {
            DiagnosticReport.AddSectionEntry(diagnosticsSectionHandle, "Symmetric Projection", $"{m_symmetricProjection}");
            DiagnosticReport.AddSectionEntry(diagnosticsSectionHandle, "Stereo Rendering Mode", $"{m_renderMode}");
            DiagnosticReport.AddSectionEntry(diagnosticsSectionHandle, "Latency Optimization", $"{m_latencyOptimization}");
            var colorSubmissionModesStr = string.Join(",", m_colorSubmissionModes.m_List);
            DiagnosticReport.AddSectionEntry(diagnosticsSectionHandle, "Color Submission Modes", $"{colorSubmissionModesStr}");
            DiagnosticReport.AddSectionEntry(diagnosticsSectionHandle, "Depth Submission Mode", $"{m_depthSubmissionMode}");
            DiagnosticReport.AddSectionEntry(diagnosticsSectionHandle, "Space Warp Motion Vector Texture Format", $"{m_spacewarpMotionVectorTextureFormat}");
            DiagnosticReport.AddSectionEntry(diagnosticsSectionHandle, "Optimize Buffer Discards", $"{m_optimizeBufferDiscards}");
#if UNITY_6000_1_OR_NEWER
            DiagnosticReport.AddSectionEntry(diagnosticsSectionHandle, "Multiview Render Regions Optimization Mode", $"{m_multiviewRenderRegionsOptimizationMode}");
#endif
#if UNITY_6000_2_OR_NEWER
            DiagnosticReport.AddSectionEntry(diagnosticsSectionHandle, "Use OpenXR Predicted Time", $"{m_useOpenXRPredictedTime}");
#endif
#if UNITY_2023_2_OR_NEWER
            DiagnosticReport.AddSectionEntry(diagnosticsSectionHandle, "Foveated Rendering API", $"{m_foveatedRenderingApi}");
#endif
        }

        [SerializeField]
        bool m_symmetricProjection;

#if UNITY_6000_1_OR_NEWER
        [SerializeField, HideInInspector]
        [Obsolete("m_optimizeMultiviewRenderRegions is deprecated. Use m_multiviewRenderRegionsOptimizationMode instead.", false)]
        bool m_optimizeMultiviewRenderRegions = false;

        /// <summary>
        /// Multiview Render Regions optimizations modes.
        /// </summary>
        public enum MultiviewRenderRegionsOptimizationMode : byte
        {
            /// Turn off Multiview Render Regions optimizations.
            None = 0,
            /// Turn on Multiview Render Regions Optimizations for final pass only.
            FinalPass = 1,
            /// Turn on Multiview Render Regions Optimizations for all compatible passes.
            AllPasses = 2,
        }

        [SerializeField, HideInInspector]
        MultiviewRenderRegionsOptimizationMode m_multiviewRenderRegionsOptimizationMode = MultiviewRenderRegionsOptimizationMode.None;

        [SerializeField, HideInInspector]
        bool m_hasMigratedMultiviewRenderRegionSetting;
#endif

#if UNITY_2023_2_OR_NEWER
        [SerializeField]
        BackendFovationApi m_foveatedRenderingApi = BackendFovationApi.Legacy;
#endif
        /// <summary>OnBeforeSerialize.</summary>
        public void OnBeforeSerialize()
        {
#if UNITY_6000_1_OR_NEWER
#pragma warning disable CS0618
            // Keep the boolean value for back compatibility.
            m_optimizeMultiviewRenderRegions = m_multiviewRenderRegionsOptimizationMode != MultiviewRenderRegionsOptimizationMode.None;
#pragma warning restore CS0618
#endif
        }
        /// <summary>OnAfterDeserialize.</summary>
        public void OnAfterDeserialize()
        {
#if UNITY_6000_1_OR_NEWER
            if (!m_hasMigratedMultiviewRenderRegionSetting)
            {
#pragma warning disable CS0618
                if (m_optimizeMultiviewRenderRegions)
                {
                    m_multiviewRenderRegionsOptimizationMode = MultiviewRenderRegionsOptimizationMode.FinalPass;
                }
                else
                {
                    m_multiviewRenderRegionsOptimizationMode = MultiviewRenderRegionsOptimizationMode.None;
                }
#pragma warning restore CS0618
                m_hasMigratedMultiviewRenderRegionSetting = true;
            }
#endif
        }

        /// <summary>
        /// If enabled, when the application begins it will create a stereo symmetric view that has the eye buffer resolution change based on the IPD.
        /// Provides a performance benefit across all IPDs.
        /// </summary>
        public bool symmetricProjection
        {
            get => m_symmetricProjection;
            set
            {
                if (OpenXRLoaderBase.Instance != null)
                    Internal_SetSymmetricProjection(value);
                else
                    m_symmetricProjection = value;
            }
        }

#if UNITY_6000_1_OR_NEWER
        /// <summary>
        /// Activates Multiview Render Regions optimizations at application start.
        /// Requires Vulkan as the Graphics API, Render Mode set to Multi-view and Symmetric rendering enabled.
        /// </summary>
        [Obsolete("optimizeMultiviewRenderRegions is deprecated. Use multiviewRenderRegionsMode instead.", false)]
        public bool optimizeMultiviewRenderRegions
        {
            get => m_multiviewRenderRegionsOptimizationMode == MultiviewRenderRegionsOptimizationMode.FinalPass || m_multiviewRenderRegionsOptimizationMode == MultiviewRenderRegionsOptimizationMode.AllPasses;
            set
            {
                MultiviewRenderRegionsOptimizationMode newMode = value
                    ? MultiviewRenderRegionsOptimizationMode.FinalPass
                    : MultiviewRenderRegionsOptimizationMode.None;

                if (OpenXRLoaderBase.Instance != null)
                {
                    Internal_SetMultiviewRenderRegionsOptimizationMode(newMode);
                }
                else
                {
#pragma warning disable CS0618
                    m_optimizeMultiviewRenderRegions = value;
#pragma warning restore CS0618

                    m_multiviewRenderRegionsOptimizationMode = newMode;
                }
            }
        }

        /// <summary>
        /// Activates Multiview Render Regions optimization modes at application start.
        /// Requires Vulkan as the Graphics API, Render Mode set to Multi-view and Symmetric rendering enabled.
        /// </summary>
        public MultiviewRenderRegionsOptimizationMode multiviewRenderRegionsOptimizationMode
        {
            get => m_multiviewRenderRegionsOptimizationMode;
            set
            {
                if (OpenXRLoaderBase.Instance != null)
                    Internal_SetMultiviewRenderRegionsOptimizationMode(value);
                else
                    m_multiviewRenderRegionsOptimizationMode = value;
            }
        }

#endif

        /// <summary>
        /// Different APIs to use in the backend.
        /// On Built-in Render Pipeline, only Legacy will be used.
        /// On Scriptable Render Pipelines, it is highly recommended to use the SRPFoveation API. More textures will use FDM with the SRPFoveation API.
        /// </summary>
#if UNITY_2023_2_OR_NEWER
        public BackendFovationApi foveatedRenderingApi
        {
            get
            {
                if (OpenXRLoaderBase.Instance != null)
                    return Internal_GetUsedFoveatedRenderingApi();
                else
                    return m_foveatedRenderingApi;
            }
            set
            {
                if (OpenXRLoaderBase.Instance != null)
                    Internal_SetUsedFoveatedRenderingApi(value);
                else
                    m_foveatedRenderingApi = value;
            }
        }
#else
        public BackendFovationApi foveatedRenderingApi => BackendFovationApi.Legacy;
#endif

#if UNITY_6000_2_OR_NEWER
        [SerializeField]
        private bool m_useOpenXRPredictedTime = false;
        /// <summary>
        /// Enable OpenXR time prediction which allows the hardware and runtime to set the
        /// display time prediction for the next frame instead of Unity.
        /// </summary>
        public bool useOpenXRPredictedTime
        {
            get
            {
                if (OpenXRLoaderBase.Instance != null)
                    return Internal_GetUseOpenXRPredictedTime();
                else
                    return m_useOpenXRPredictedTime;
            }
            set
            {
                if (OpenXRLoaderBase.Instance != null)
                    Internal_SetUseOpenXRPredictedTime(value);
                else
                    m_useOpenXRPredictedTime = value;
            }
        }
#endif

        const string LibraryName = "UnityOpenXR";

        [DllImport(LibraryName, EntryPoint = "NativeConfig_SetRenderMode")]
        static extern void Internal_SetRenderMode(RenderMode renderMode);

        [DllImport(LibraryName, EntryPoint = "NativeConfig_GetRenderMode")]
        static extern RenderMode Internal_GetRenderMode();

        [DllImport(LibraryName, EntryPoint = "NativeConfig_SetLatencyOptimization")]
        static extern void Internal_SetLatencyOptimization(LatencyOptimization latencyOptimzation);

        [DllImport(LibraryName, EntryPoint = "NativeConfig_GetLatencyOptimization")]
        static extern LatencyOptimization Internal_GetLatencyOptimization();

        [DllImport(LibraryName, EntryPoint = "NativeConfig_SetDepthSubmissionMode")]
        static extern void Internal_SetDepthSubmissionMode(
            DepthSubmissionMode depthSubmissionMode
        );

        [DllImport(LibraryName, EntryPoint = "NativeConfig_GetDepthSubmissionMode")]
        static extern DepthSubmissionMode Internal_GetDepthSubmissionMode();

        [DllImport(LibraryName, EntryPoint = "NativeConfig_SetSpaceWarpMotionVectorTextureFormat")]
        static extern void Internal_SetSpaceWarpMotionVectorTextureFormat(
            SpaceWarpMotionVectorTextureFormat spaceWarpMotionVectorTextureFormat
        );

        [DllImport(LibraryName, EntryPoint = "NativeConfig_GetSpaceWarpMotionVectorTextureFormat")]
        static extern SpaceWarpMotionVectorTextureFormat Internal_GetSpaceWarpMotionVectorTextureFormat();

        [DllImport(LibraryName, EntryPoint = "NativeConfig_SetSymmetricProjection")]
        static extern void Internal_SetSymmetricProjection([MarshalAs(UnmanagedType.I1)] bool enabled);
#if UNITY_6000_1_OR_NEWER
        [DllImport(LibraryName, EntryPoint = "NativeConfig_SetMultiviewRenderRegionsOptimizationMode")]
        static extern void Internal_SetMultiviewRenderRegionsOptimizationMode(MultiviewRenderRegionsOptimizationMode mode);
#endif
        [DllImport(LibraryName, EntryPoint = "NativeConfig_SetOptimizeBufferDiscards")]
        static extern void Internal_SetOptimizeBufferDiscards([MarshalAs(UnmanagedType.I1)] bool enabled);

        [DllImport(LibraryName, EntryPoint = "OculusFoveation_SetUsedApi")]
        static extern void Internal_SetUsedFoveatedRenderingApi(BackendFovationApi api);

        [DllImport(LibraryName, EntryPoint = "OculusFoveation_GetUsedApi")]
        internal static extern BackendFovationApi Internal_GetUsedFoveatedRenderingApi();

        [DllImport(LibraryName, EntryPoint = "NativeConfig_SetColorSubmissionMode")]
        static extern void Internal_SetColorSubmissionMode(
            ColorSubmissionModeGroup[] colorSubmissionMode
        );

        [DllImport(LibraryName, EntryPoint = "NativeConfig_SetColorSubmissionModes")]
        static extern void Internal_SetColorSubmissionModes(
            int[] colorSubmissionMode,
            int arraySize
        );

        [DllImport(LibraryName, EntryPoint = "NativeConfig_GetColorSubmissionModes")]
        static extern int Internal_GetColorSubmissionModes(
            [Out] int[] colorSubmissionMode,
            int arraySize
        );

        [DllImport("UnityOpenXR", EntryPoint = "NativeConfig_GetIsUsingLegacyXRDisplay")]
        [return: MarshalAs(UnmanagedType.U1)]
        static extern bool Internal_GetIsUsingLegacyXRDisplay();

        [DllImport(LibraryName, EntryPoint = "NativeConfig_GetUseOpenXRPredictedTime")]
        [return: MarshalAs(UnmanagedType.U1)]
        static extern bool Internal_GetUseOpenXRPredictedTime();

        [DllImport(LibraryName, EntryPoint = "NativeConfig_SetUseOpenXRPredictedTime")]
        static extern void Internal_SetUseOpenXRPredictedTime([MarshalAs(UnmanagedType.I1)] bool enabled);
    }
}
