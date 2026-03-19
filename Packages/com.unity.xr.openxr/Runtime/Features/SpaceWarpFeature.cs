using System.Runtime.InteropServices;
using UnityEngine.XR.OpenXR.NativeTypes;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityEngine.XR.OpenXR.Features
{
    /// <summary>
    /// This <see cref="OpenXRFeature"/> enables the use of SpaceWarp in OpenXR without
    /// the need for other 3rd party packages.
    /// </summary>
#if UNITY_EDITOR && UNITY_6000_1_OR_NEWER
    [UnityEditor.XR.OpenXR.Features.OpenXRFeature(
        UiName = k_UiName,
        Desc = @"Application SpaceWarp feature",
        Company = "Unity",
        DocumentationLink = "",
        OpenxrExtensionStrings = k_OpenXRRequestedExtensions,
        Version = "1.0.0",
        BuildTargetGroups = new[] { BuildTargetGroup.Android },
        FeatureId = k_SpaceWarpFeatureId)]
#endif
    public class SpaceWarpFeature : OpenXRFeature
    {
        /// <summary>
        /// The UI name that shows on the XR Plug-in Management panel.
        /// </summary>
        public const string k_UiName = "Application SpaceWarp";

        /// <summary>
        /// The required OpenXR extension.
        /// </summary>
        public const string k_OpenXRRequestedExtensions = "XR_FB_space_warp";

        /// <summary>
        /// The feature ID string. This is used to give the feature a well known ID for reference.
        /// </summary>
        public const string k_SpaceWarpFeatureId = "com.unity.openxr.feature.spacewarp";

#if SPACEWARP_RIGHT_HAND_NDC_SUPPORTED
        /// <summary>
        /// SpaceWarp NDC space handedness (default is false, for left-handed NDC space).
        /// </summary>
        [SerializeField]
        [Tooltip("Check this box if motion vector uses right-handed normalized device coordinates")]
        internal bool m_UseRightHandedNDC = false;

        /// <summary>
        /// Selects the NDC space handedness for SpaceWarp.
        /// If <c>true</c>, motion vector data must use the right-handed NDC space.
        /// If <c>false</c>, motion vector data must use the left-handed NDC space.
        /// </summary>
        public bool useRightHandedNDC
        {
            get => m_UseRightHandedNDC;
            set
            {
                if (OpenXRLoaderBase.Instance != null)
                {
                    bool success = Internal_SetSpaceWarpRightHandedNDC(value);
                    if (success)
                        m_UseRightHandedNDC = value;
                }
            }
        }
#endif // SPACEWARP_RIGHT_HAND_NDC_SUPPORTED

        /// <inheritdoc />
        protected internal override bool OnInstanceCreate(ulong xrInstance)
        {
            return OpenXRRuntime.IsExtensionEnabled(k_OpenXRRequestedExtensions)
#if SPACEWARP_RIGHT_HAND_NDC_SUPPORTED
                && Internal_SetSpaceWarpRightHandedNDC(m_UseRightHandedNDC)
#endif // SPACEWARP_RIGHT_HAND_NDC_SUPPORTED
                && base.OnInstanceCreate(xrInstance);
        }

        /// <summary>
        /// Enable or disable SpaceWarp.
        /// </summary>
        /// <param name="enabled">The flag to enable or disable SpaceWarp.</param>
        /// <returns>Always returns <see langword="true"/>.</returns>
        public static bool SetSpaceWarp(bool enabled)
        {
            return MetaSetSpaceWarp(enabled) == XrResult.Success;
        }

        /// <summary>
        /// Update SpaceWarp with the camera's position.
        /// </summary>
        /// <param name="position">Camera's position as a <see cref="Vector3"/>.</param>
        /// <returns>Always returns <see langword="true"/>.</returns>
        /// <remarks>
        /// Depending on the headset, SpaceWarp may not need to be updated with the main camera’s position.
        /// Refer to the headset’s specifications to determine if it’s required.
        /// </remarks>
        public static bool SetAppSpacePosition(Vector3 position)
        {
            return MetaSetAppSpacePosition(position.x, position.y, position.z) == XrResult.Success;
        }

        /// <summary>
        /// Update SpaceWarp with the camera's rotation.
        /// </summary>
        /// <param name="rotation">Camera's rotation as a <see cref="Quaternion"/>.</param>
        /// <returns>Always returns <see langword="true"/>.</returns>
        /// <remarks>
        /// Depending on the headset, SpaceWarp may not need to be updated with the main camera’s rotation.
        /// Refer to the headset’s specifications to determine if it’s required.
        /// </remarks>
        public static bool SetAppSpaceRotation(Quaternion rotation)
        {
            return MetaSetAppSpaceRotation(rotation.x, rotation.y, rotation.z, rotation.w) == XrResult.Success;
        }

#if UNITY_EDITOR && SPACEWARP_RIGHT_HAND_NDC_SUPPORTED
        [CustomEditor(typeof(SpaceWarpFeature))]
        internal class SpaceWarpFeatureSettingsEditor : Editor
        {
            private SerializedProperty useRightHandedNDC;
            static GUIContent s_UseRightHandedNDC = EditorGUIUtility.TrTextContent("Use Right Handed NDC");

            void OnEnable()
            {
                useRightHandedNDC = serializedObject.FindProperty("m_UseRightHandedNDC");
            }

            public override void OnInspectorGUI()
            {
                serializedObject.Update();
                EditorGUILayout.PropertyField(useRightHandedNDC, s_UseRightHandedNDC);
                serializedObject.ApplyModifiedProperties();
            }
        }
#endif // UNITY_EDITOR && SPACEWARP_RIGHT_HAND_NDC_SUPPORTED

        // Import functions from the UnityOpenXR dll
        private const string k_Library = "UnityOpenXR";

        [DllImport(k_Library, EntryPoint = "MetaSetSpaceWarp")]
        private static extern XrResult MetaSetSpaceWarp([MarshalAs(UnmanagedType.I1)] bool enabled);

        [DllImport(k_Library, EntryPoint = "MetaSetAppSpacePosition")]
        private static extern XrResult MetaSetAppSpacePosition(float x, float y, float z);

        [DllImport(k_Library, EntryPoint = "MetaSetAppSpaceRotation")]
        private static extern XrResult MetaSetAppSpaceRotation(float x, float y, float z, float w);

#if SPACEWARP_RIGHT_HAND_NDC_SUPPORTED
        [DllImport(k_Library, EntryPoint = "NativeConfig_SetSpaceWarpRightHandedNDC")]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool Internal_SetSpaceWarpRightHandedNDC([MarshalAs(UnmanagedType.I1)] bool useRightHandedNDC);
#endif // SPACEWARP_RIGHT_HAND_NDC_SUPPORTED
    }
}
