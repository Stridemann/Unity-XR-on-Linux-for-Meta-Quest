using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.Rendering;
#if UNITY_RENDER_PIPELINES_UNIVERSAL
using UnityEngine.Rendering.Universal;
#endif // UNITY_RENDER_PIPELINES_UNIVERSAL

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityEngine.XR.OpenXR.Features
{
    /// <summary>
    /// This <see cref="OpenXRFeature"/> enables the use of Automatic Viewport Dynamic Resolution in OpenXR.
    /// </summary>
#if UNITY_EDITOR && UNITY_6000_3_OR_NEWER && UNITY_RENDER_PIPELINES_UNIVERSAL
    [UnityEditor.XR.OpenXR.Features.OpenXRFeature(
        UiName = k_UIName,
        Desc = @"Automatic Viewport Dynamic Resolution",
        Company = "Unity",
        DocumentationLink = "",
        OpenxrExtensionStrings = k_OpenXRRequestedExtensions,
        Version = "1.0.0",
        BuildTargetGroups = new[] { BuildTargetGroup.Android },
        FeatureId = k_DynamicResolutionFeatureId)]
#endif
    public class AutomaticDynamicResolutionFeature : OpenXRFeature
    {
        /// <summary>
        /// The minimum possible value of the resolution scalars
        /// </summary>
        public const float minResolutionScalarLimit = 0.05f;

        /// <summary>
        /// The maximum possible value of the resolution scalars.
        /// </summary>
        public const float maxResolutionScalarLimit = 2.0f;

        /// <summary>
        /// The UI name that shows on the XR Plug-in Management panel.
        /// </summary>
        internal const string k_UIName = "Automatic Viewport Dynamic Resolution";

        /// <summary>
        /// The required OpenXR extensions.
        /// </summary>
        internal const string k_OpenXRRequestedExtensions =
            "XR_META_recommended_layer_resolution XR_ANDROID_recommended_resolution";

        /// <summary>
        /// The feature ID string. This is used to give the feature a well known ID for reference.
        /// </summary>
        internal const string k_DynamicResolutionFeatureId = "com.unity.openxr.feature.ViewportDynamicResolution";

        /// <summary>
        /// The initial value of the minimum resolution scalar
        /// </summary>
        internal const float k_InitialMinResolutionScalar = 0.05f;

        /// <summary>
        /// The initial value of the maximum resolution scalar
        /// </summary>
        internal const float k_InitialMaxResolutionScalar = 1.0f;

        /// <summary>
        /// Stores the current minimum resolution scalar.
        /// </summary>
        [SerializeField]
        [Tooltip("Select the minimum resolution scalar.")]
        internal float m_MinResolutionScalar = k_InitialMinResolutionScalar;

        /// <summary>
        /// Stores the current maximum resolution scalar
        /// </summary>
        [SerializeField]
        [Tooltip("Select the maximum resolution scalar.")]
        internal float m_MaxResolutionScalar = k_InitialMaxResolutionScalar;

        /// <summary>
        /// Stores the current minimum resolution scalar. It is bounded from below
        /// by <see cref="minResolutionScalarLimit"/>, and from above by the current
        /// value of <see cref="maxResolutionScalar"/>.
        /// </summary>
        /// <remarks>
        /// Only the getter is public. You can change this value in:
        /// "Project Settings -> XR Plug-in Management -> OpenXR ->
        /// Automatic Viewport Dynamic Resolution"
        /// </remarks>
        public float minResolutionScalar
        {
            get => m_MinResolutionScalar;
            private set
            {
                if (OpenXRLoaderBase.Instance != null)
                {
                    if (value > m_MaxResolutionScalar)
                        m_MinResolutionScalar = m_MaxResolutionScalar;
                    else
                        m_MinResolutionScalar = value;
                }
            }
        }

        /// <summary>
        /// Stores the current maximum resolution scalar. It is bounded from below
        /// by the current value of <see cref="minResolutionScalar"/>, and from above
        /// by <see cref="maxResolutionScalarLimit"/>.
        /// </summary>
        /// <remarks>
        /// Only the getter is public. You can change this value in:
        /// "Project Settings -> XR Plug-in Management -> OpenXR ->
        /// Automatic Viewport Dynamic Resolution"
        /// </remarks>
        public float maxResolutionScalar
        {
            get => m_MaxResolutionScalar;
            private set
            {
                if (OpenXRLoaderBase.Instance != null)
                {
                    if (value < m_MinResolutionScalar)
                        m_MaxResolutionScalar = m_MinResolutionScalar;
                    else
                        m_MaxResolutionScalar = value;
                }
            }
        }

        /// <summary>
        /// Are we currently using the suggested resolution provided by the extension(s),
        /// or are we disregarding it and not updating resolution automatically.
        /// Default is <see langword="true"/>.
        /// </summary>
        /// <remarks>
        /// Call <see cref="SetUsingSuggestedResolutionScale"/> to change this value.
        /// Only the getter is public.
        /// </remarks>
        public static bool usingSuggestedResolutionScale { get; private set; } = true;

        /// <inheritdoc />
        protected internal override bool OnInstanceCreate(ulong instance)
        {
            if (!enabled || !Internal_IsAutomaticDynamicResolutionScalingSupported())
                return false;

            Internal_SetUsingSuggestedResolutionScale(usingSuggestedResolutionScale);
            Internal_SetMinMaxScalerResolution(minResolutionScalar, maxResolutionScalar);

            var displaySubsystem = GetFirstDisplaySubsystem();
            if (displaySubsystem != null)
                displaySubsystem.scaleOfAllRenderTargets = maxResolutionScalar;
#if UNITY_RENDER_PIPELINES_UNIVERSAL
            RenderPipelineAsset currentRenderPipelineAsset = GraphicsSettings.currentRenderPipeline;
            UniversalRenderPipelineAsset urpAsset = currentRenderPipelineAsset as UniversalRenderPipelineAsset;

            if (urpAsset != null)
                urpAsset.renderScale = maxResolutionScalar;
#endif // UNITY_RENDER_PIPELINES_UNIVERSAL

            return base.OnInstanceCreate(instance);
        }

        /// <summary>
        /// Determines if automatic dynamic resolution scaling is supported.
        /// </summary>
        /// <returns>
        /// Returns <see langword="true"/> if automatic dynamic resolution scaling
        /// is supported, and returns <see langword="false"/> if it is not.
        /// </returns>
        public static bool IsAutomaticDynamicResolutionScalingSupported()
        {
            return Internal_IsAutomaticDynamicResolutionScalingSupported();
        }

        /// <summary>
        /// Set whether or not we are using the suggested resolution provided by
        /// the extensions, rather than disregarding it and not updating resolution
        /// automatically. Default is <see langword="true"/>.
        /// </summary>
        /// <param name="usingSuggestedScale">
        /// The flag whether or not to use the suggested resolution and update the
        /// resolution automatically.
        /// </param>
        public static void SetUsingSuggestedResolutionScale(bool usingSuggestedScale)
        {
            usingSuggestedResolutionScale = usingSuggestedScale;
            Internal_SetUsingSuggestedResolutionScale(usingSuggestedScale);
        }

        /// <summary>
        /// A helper to return the first available XRDisplaySubsystem.
        /// </summary>
        static XRDisplaySubsystem GetFirstDisplaySubsystem()
        {
            List<XRDisplaySubsystem> displays = new List<XRDisplaySubsystem>();
            SubsystemManager.GetSubsystems(displays);
            if (displays.Count == 0)
                return null;

            return displays[0];
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(AutomaticDynamicResolutionFeature))]
        internal class DynamicResolutionSettingsEditor : Editor
        {
            // variables to store the state of the min resolution scalar slider
            private float prevMinResolutionScalar;
            private SerializedProperty minResolutionScalar;
            static GUIContent s_MinResolutionLabel = EditorGUIUtility.TrTextContent("Min Resolution Scalar");

            // variables to store the state of the max resolution scalar slider
            private float prevMaxResolutionScalar;
            private SerializedProperty maxResolutionScalar;
            static GUIContent s_MaxResolutionLabel = EditorGUIUtility.TrTextContent("Max Resolution Scalar");

            void OnEnable()
            {
                minResolutionScalar = serializedObject.FindProperty("m_MinResolutionScalar");
                prevMinResolutionScalar = minResolutionScalar.floatValue;

                maxResolutionScalar = serializedObject.FindProperty("m_MaxResolutionScalar");
                prevMaxResolutionScalar = maxResolutionScalar.floatValue;
            }

            public override void OnInspectorGUI()
            {
                serializedObject.Update();

                EditorGUILayout.Slider(
                    minResolutionScalar,
                    minResolutionScalarLimit,
                    maxResolutionScalarLimit,
                    s_MinResolutionLabel);

                EditorGUILayout.Slider(
                    maxResolutionScalar,
                    minResolutionScalarLimit,
                    maxResolutionScalarLimit,
                    s_MaxResolutionLabel);

                // If min slider is adjusted, prevent min slider value from going above max slider value.
                // Or, if max slider is adjusted, prevent max slider value from going below min slider value.
                if (minResolutionScalar.floatValue != prevMinResolutionScalar
                    && minResolutionScalar.floatValue > maxResolutionScalar.floatValue)
                {
                    minResolutionScalar.floatValue = maxResolutionScalar.floatValue;
                }
                else if (maxResolutionScalar.floatValue != prevMaxResolutionScalar
                    && maxResolutionScalar.floatValue < minResolutionScalar.floatValue)
                {
                    maxResolutionScalar.floatValue = minResolutionScalar.floatValue;
                }

                prevMinResolutionScalar = minResolutionScalar.floatValue;
                prevMaxResolutionScalar = maxResolutionScalar.floatValue;

                serializedObject.ApplyModifiedProperties();
            }
        }
#endif

        // Import functions from the UnityOpenXR dll
        private const string k_Library = "UnityOpenXR";

        [DllImport(k_Library, EntryPoint = "DynamicResolution_IsAutomaticDynamicResolutionScalingSupported")]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool Internal_IsAutomaticDynamicResolutionScalingSupported();

        [DllImport(k_Library, EntryPoint = "DynamicResolution_SetMinMaxScalerResolution")]
        private static extern void Internal_SetMinMaxScalerResolution(float min, float max);

        [DllImport(k_Library, EntryPoint = "DynamicResolution_SetUsingSuggestedResolutionScale")]
        private static extern void Internal_SetUsingSuggestedResolutionScale([MarshalAs(UnmanagedType.I1)] bool usingSuggestedScale);
    }
}
