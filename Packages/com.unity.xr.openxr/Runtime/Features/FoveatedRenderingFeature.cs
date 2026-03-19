using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.Rendering;
using UnityEngine.XR.OpenXR.NativeTypes;

namespace UnityEngine.XR.OpenXR.Features
{
    /// <summary>
    /// This <see cref="OpenXRFeature"/> enables the use of foveated rendering in OpenXR.
    /// </summary>
#if UNITY_EDITOR && UNITY_2023_2_OR_NEWER
    [UnityEditor.XR.OpenXR.Features.OpenXRFeature(UiName = "Foveated Rendering",
        BuildTargetGroups = new []{BuildTargetGroup.Standalone, BuildTargetGroup.WSA, BuildTargetGroup.Android},
        Company = "Unity",
        Desc = "Add foveated rendering.",
        DocumentationLink = Constants.k_DocumentationManualURL + "features/foveatedrendering.html",
        OpenxrExtensionStrings = "XR_UNITY_foveation XR_FB_foveation XR_FB_foveation_configuration XR_FB_swapchain_update_state XR_FB_foveation_vulkan XR_META_foveation_eye_tracked XR_META_vulkan_swapchain_create_info",
        Version = "1",
        Category = UnityEditor.XR.OpenXR.Features.FeatureCategory.Feature,
        FeatureId = featureId)]
#endif
    public class FoveatedRenderingFeature : OpenXRFeature
    {
        /// <summary>
        /// The feature id string. This is used to give the feature a well known id for reference.
        /// </summary>
        public const string featureId = "com.unity.openxr.feature.foveatedrendering";

        /// <summary>
        /// Get whether Vulkan subsampled layout is currently enabled.
        /// </summary>
        public static bool isSubsampledLayoutEnabled { get; private set; }

        [SerializeField]
        bool enableSubsampledLayout;

#if UNITY_EDITOR
        private bool SettingsUseVulkan()
        {
            if (!PlayerSettings.GetUseDefaultGraphicsAPIs(BuildTarget.Android))
            {
                GraphicsDeviceType[] apis = PlayerSettings.GetGraphicsAPIs(BuildTarget.Android);
                if (apis.Length >= 1 && apis[0] == GraphicsDeviceType.Vulkan)
                {
                    return true;
                }
                return false;
            }

            return true;
        }

        protected internal override void GetValidationChecks(List<ValidationRule> rules, BuildTargetGroup targetGroup)
        {
#if UNITY_ANDROID
            rules.Add(new ValidationRule(this)
            {
                message = "Subsampled Layout is only supported on Vulkan graphics API",
                checkPredicate = () =>
                {
                    if (enableSubsampledLayout && !SettingsUseVulkan())
                    {
                        return false;
                    }
                    return true;
                },
                fixIt = () =>
                {
                    PlayerSettings.SetGraphicsAPIs(BuildTarget.Android, new[] { GraphicsDeviceType.Vulkan });
                },
                error = true,
                fixItAutomatic = true,
                fixItMessage = "Set Vulkan as Graphics API"
            });
#endif
        }

        [CustomEditor(typeof(FoveatedRenderingFeature))]
        internal class FoveatedRenderingFeatureEditor : Editor
        {
            private SerializedProperty subsampledLayout;
            static GUIContent s_SubsampledLayout = EditorGUIUtility.TrTextContent("Subsampled Layout (Vulkan)");

            void OnEnable()
            {
                subsampledLayout = serializedObject.FindProperty("enableSubsampledLayout");
            }

            public override void OnInspectorGUI()
            {
                EditorGUIUtility.labelWidth = 300.0f;
                serializedObject.Update();
                EditorGUILayout.PropertyField(subsampledLayout, s_SubsampledLayout);
                serializedObject.ApplyModifiedProperties();
                EditorGUIUtility.labelWidth = 0.0f;
            }
        }
#endif

        /// <summary>
        /// Attempts to set whether subsampled layout is enabled.
        /// </summary>
        /// <param name="enableSubsampling">Indicates whether to enable subsampling.</param>
        /// <returns>
        /// Returns <see langword="true"/> if subsampling state was updated
        /// and returns <see langword="false"/> otherwise.
        /// </returns>
        public static bool TrySetSubsampledLayoutEnabled(bool enableSubsampling)
        {
            if (enableSubsampling)
            {
                if (SystemInfo.graphicsDeviceType != GraphicsDeviceType.Vulkan)
                {
                    Debug.LogError("Could not enable subsampling. Subsampled Layout is only supported on Vulkan graphics API.");
                    return false;
                }

                if (!OpenXRRuntime.IsExtensionEnabled("XR_META_vulkan_swapchain_create_info"))
                {
                    Debug.LogError("$Could not enable Vulkan subsampling. OpenXR extension XR_META_vulkan_swapchain_create_info is not available on the current runtime.");
                    return false;
                }
            }

            var wasSuccessful = Internal_Unity_MetaSetSubsampledLayout(enableSubsampling) == XrResult.Success;

            if (wasSuccessful)
            {
                isSubsampledLayoutEnabled = enableSubsampling;
            }

            return wasSuccessful;
        }

        /// <inheritdoc />
        protected internal override bool OnInstanceCreate(ulong instance)
        {
            // If using BiRP, the feature must know not to use the newer API for FSR/FDM
            Internal_Unity_SetUseFoveatedRenderingLegacyMode(GraphicsSettings.defaultRenderPipeline == null);

            TrySetSubsampledLayoutEnabled(enableSubsampledLayout);

            return base.OnInstanceCreate(instance);
        }

        /// <inheritdoc />
        protected internal override IntPtr HookGetInstanceProcAddr(IntPtr func)
        {
            return Internal_Unity_intercept_xrGetInstanceProcAddr(func);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        private const string Library = "UnityOpenXR";

        [DllImport(Library, EntryPoint = "UnityFoveation_intercept_xrGetInstanceProcAddr")]
        private static extern IntPtr Internal_Unity_intercept_xrGetInstanceProcAddr(IntPtr func);

        [DllImport(Library, EntryPoint = "UnityFoveation_SetUseFoveatedRenderingLegacyMode")]
        private static extern void Internal_Unity_SetUseFoveatedRenderingLegacyMode([MarshalAs(UnmanagedType.I1)] bool value);

        [DllImport(Library, EntryPoint = "UnityFoveation_GetUseFoveatedRenderingLegacyMode")]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool Internal_Unity_GetUseFoveatedRenderingLegacyMode();

        [DllImport(Library, EntryPoint = "MetaSetSubsampledLayout")]
        private static extern XrResult Internal_Unity_MetaSetSubsampledLayout([MarshalAs(UnmanagedType.U1)] bool enableSubsampling);
    }
}
