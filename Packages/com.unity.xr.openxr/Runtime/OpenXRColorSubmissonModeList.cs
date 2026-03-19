using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif // UNITY_EDITOR
using UnityEngine;

namespace UnityEngine.XR.OpenXR
{
#if UNITY_EDITOR
    static class SerializedPropertyExt
    {
        public static void Update(this SerializedProperty property)
        {
            property.serializedObject.Update();
        }

        public static void ApplyModifiedProperties(this SerializedProperty property)
        {
            property.serializedObject.ApplyModifiedProperties();
        }

        public static SerializedProperty GetArrayElementAtReverseIndex(
            this SerializedProperty property,
            int i
        )
        {
            return property.GetArrayElementAtIndex(property.arraySize - 1 - i);
        }

        public static SerializedProperty GetLastArrayElement(this SerializedProperty property)
        {
            return property.GetArrayElementAtReverseIndex(0);
        }
    }
#endif // UNITY_EDITOR

    public partial class OpenXRSettings
    {
        /// <summary>
        /// Runtime color format - keep in sync with NativePlugin\Source\unity_types.h
        /// </summary>
        public enum ColorSubmissionModeGroup
        {
            /// <summary>
            /// Color render texture format, 8 bits per channel. This is the default swapchain
            /// format.
            /// Analogous to kUnityXRRenderTextureFormatRGBA32/BGRA32, auto-selecting the preferred
            /// color order for the platform.
            /// </summary>
            [InspectorName("8 bits per channel (LDR, default)")]
            kRenderTextureFormatGroup8888 = 0,

            /// <summary>
            /// Color render texture format. 10 bits for colors, 2 bits for alpha.
            /// Analogous to kUnityXRRenderTextureFormatRGBA1010102/BGRA1010102, auto-selecting the preferred
            /// color order for the platform.
            /// </summary>
            [InspectorName("10 bits floating-point per color channel, 2 bit alpha (HDR)")]
            kRenderTextureFormatGroup1010102_Float,

            /// <summary>
            /// Color render texture format, 16 bit floating point per channel, auto-selecting the
            /// preferred color order for the platform.
            /// </summary>
            [InspectorName("16 bits floating-point per channel (HDR)")]
            kRenderTextureFormatGroup16161616_Float,

            /// <summary>
            /// Color render texture format. 5 bits for Red channel, 6 bits for Green channel, 5
            /// bits for Blue channel, auto-selecting the preferred color order for the platform.
            /// </summary>
            [InspectorName("5,6,5 bit packed (LDR, mobile)")]
            kRenderTextureFormatGroup565,

            /// <summary>
            /// Color render texture format. R and G channels are 11 bit floating point, B channel is 10
            /// bit floating point.
            /// </summary>
            [InspectorName("11,11,10 bit packed floating-point (HDR)")]
            kRenderTextureFormatGroup111110_Float,
        }

        /// <summary>
        /// The default color mode for color render targets.
        /// </summary>
        public static readonly ColorSubmissionModeGroup kDefaultColorMode =
            ColorSubmissionModeGroup.kRenderTextureFormatGroup8888;

        /// <summary>
        /// Container class for the list of color render target color modes, used to enable a CustomPropertyDrawer.
        /// </summary>
        [Serializable]
        public class ColorSubmissionModeList
        {
            /// <summary>
            /// The priority list of color render target color modes. The first _supported_ format is selected.
            /// </summary>
            public ColorSubmissionModeGroup[] m_List =
            {
                ColorSubmissionModeGroup.kRenderTextureFormatGroup8888
            };
        }

#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(ColorSubmissionModeList))]
        internal class ColorSubmissionModeListEditor : UnityEditor.PropertyDrawer
        {
            private struct FormatGroupInfo
            {
                public ColorSubmissionModeGroup Value;
                public string Name;
                public string DisplayName;
            }

            private List<FormatGroupInfo> m_GroupInfo;
            private ReorderableList m_ListGUI;
            private HashSet<int> m_FormatIndicesInUse = new HashSet<int>();
            private HashSet<ColorSubmissionModeGroup> m_FormatValuesInUse =
                new HashSet<ColorSubmissionModeGroup>();

            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                property = property?.FindPropertyRelative("m_List");
                if (property == null || !property.isArray)
                    return;

                property.Update();

                bool hasDefault = false;
                for (int i = 0; i < property.arraySize; i++)
                {
                    var element = property.GetArrayElementAtIndex(i);
                    if (element.intValue == (int)kDefaultColorMode)
                    {
                        hasDefault = true;
                        break;
                    }
                }
                if (!hasDefault)
                {
                    property.arraySize++;
                    property.GetLastArrayElement().intValue = (int)kDefaultColorMode;
                }

                var listSize = property.arraySize;

                GUILayout.BeginHorizontal();
                EditorGUI.BeginProperty(position, label, property);

                GUILayout.Label("Color Submission Modes");

                if (m_ListGUI == null)
                {
                    m_ListGUI = new ReorderableList(null, property);
                    m_ListGUI.onAddDropdownCallback = (position, listGUI) =>
                        AddDropdownCallback(property, position, listGUI);
                    m_ListGUI.onCanRemoveCallback = (listGUI) =>
                    {
                        property.Update();
                        return property.arraySize > 1
                            && property.GetArrayElementAtIndex(listGUI.index).intValue
                                != (int)kDefaultColorMode;
                    };
                    m_ListGUI.onRemoveCallback = (listGUI) =>
                    {
                        using (new PropertyScope(property))
                            property.DeleteArrayElementAtIndex(listGUI.index);
                    };
                    m_ListGUI.onReorderCallbackWithDetails = (listGUI, oldIndex, newIndex) =>
                        OnReorderCallbackWithDetails(property, oldIndex, newIndex);
                    m_ListGUI.drawElementCallback = (position, index, isActive, isFocused) =>
                        DrawElementCallback(property, position, index);
                    m_ListGUI.drawHeaderCallback = (position) =>
                        GUI.Label(position, "Color Formats");
                    m_ListGUI.elementHeight = 16;
                    m_ListGUI.multiSelect = false;
                }
                m_ListGUI.DoLayoutList();

                GUILayout.EndHorizontal();

                EditorGUI.EndProperty();
                property.ApplyModifiedProperties();
            }

            private void OnReorderCallbackWithDetails(
                SerializedProperty property,
                int oldIndex,
                int newIndex
            )
            {
                if (property == null || !property.isArray)
                    return;

                using (new PropertyScope(property))
                {
                    SerializedProperty oldLocation = property.GetArrayElementAtIndex(oldIndex);
                    SerializedProperty newLocation = property.GetArrayElementAtIndex(newIndex);
                    int tempValue = newLocation.enumValueIndex;
                    newLocation.enumValueIndex = oldLocation.enumValueIndex;
                    oldLocation.enumValueIndex = tempValue;
                }
            }

            private void DrawElementCallback(SerializedProperty property, Rect position, int index)
            {
                var element = property.GetArrayElementAtIndex(index);
                if (property == null)
                    return;

                // Don't allow removing RGBA32 - default.
                if (element.intValue == (int)kDefaultColorMode)
                {
                    GUI.Label(position, element.enumDisplayNames[element.enumValueIndex]);
                    return;
                }

                if (
                    EditorGUI.DropdownButton(
                        position: position,
                        content: new GUIContent(element.enumDisplayNames[element.enumValueIndex]),
                        focusType: FocusType.Keyboard
                    )
                )
                {
                    var menu = new GenericMenu();

                    var inUse = FormatValuesInUse(property);
                    AvailableFormatGroups(property)
                        .ForEach(gi =>
                        {
                            if (inUse.Contains(gi.Value))
                                menu.AddDisabledItem(new GUIContent(gi.DisplayName));
                            else
                                menu.AddItem(
                                    new GUIContent(gi.DisplayName),
                                    on: false,
                                    func: () =>
                                    {
                                        using (new PropertyScope(property))
                                        {
                                            element.intValue = (int)gi.Value;
                                        }
                                    }
                                );
                        });

                    menu.DropDown(position);
                }
            }

            private HashSet<int> FormatIndicesInUse(SerializedProperty property)
            {
                m_FormatIndicesInUse.Clear();
                if (property == null || !property.isArray)
                    return m_FormatIndicesInUse;

                for (int i = 0; i < property.arraySize; i++)
                {
                    m_FormatIndicesInUse.Add(property.GetArrayElementAtIndex(i).enumValueIndex);
                }
                return m_FormatIndicesInUse;
            }

            private HashSet<ColorSubmissionModeGroup> FormatValuesInUse(SerializedProperty property)
            {
                m_FormatValuesInUse.Clear();
                if (property == null || !property.isArray)
                    return m_FormatValuesInUse;

                for (int i = 0; i < property.arraySize; i++)
                {
                    m_FormatValuesInUse.Add(
                        (ColorSubmissionModeGroup)property.GetArrayElementAtIndex(i).intValue
                    );
                }
                return m_FormatValuesInUse;
            }

            private List<FormatGroupInfo> AvailableFormatGroups(SerializedProperty property)
            {
                if (m_GroupInfo == null)
                {
                    m_GroupInfo = new List<FormatGroupInfo>();

                    var values = Enum.GetValues(typeof(ColorSubmissionModeGroup))
                        .Cast<ColorSubmissionModeGroup>();
                    if (Internal_GetIsUsingLegacyXRDisplay())
                    {
                        values = new List<ColorSubmissionModeGroup>
                        {
                            ColorSubmissionModeGroup.kRenderTextureFormatGroup8888,
                            ColorSubmissionModeGroup.kRenderTextureFormatGroup565
                        };
                    }

                    var element0 = property.GetArrayElementAtIndex(0);
                    var displayNames = element0.enumDisplayNames;
                    var names = element0.enumNames;

                    foreach (var v in values)
                    {
                        var index = names
                            .Select((n, i) => new { name = n, index = i })
                            .First(s => s.name == Enum.GetName(typeof(ColorSubmissionModeGroup), v))
                            .index;
                        m_GroupInfo.Add(
                            new FormatGroupInfo
                            {
                                Value = v,
                                Name = names[index],
                                DisplayName = displayNames[index]
                            }
                        );
                    }
                }

                return m_GroupInfo;
            }

            private void AddDropdownCallback(
                SerializedProperty property,
                Rect position,
                ReorderableList listGUI
            )
            {
                using (new PropertyScope(property))
                    EditorUtility.DisplayCustomMenu(
                        position,
                        AvailableFormatGroups(property)
                            .Select(gi => new GUIContent(gi.DisplayName))
                            .ToArray(),
                        (i => !FormatIndicesInUse(property).Contains(i)),
                        -1,
                        MenuSelectCallback,
                        property
                    );
            }

            private void MenuSelectCallback(object userData, string[] options, int selected)
            {
                SerializedProperty property = (SerializedProperty)userData;
                if (property == null || !property.isArray)
                    return;

                using (new PropertyScope(property))
                {
                    property.arraySize++;
                    property.GetLastArrayElement().intValue = (int)
                        AvailableFormatGroups(property)[selected].Value;
                }
            }

            private sealed class PropertyScope : IDisposable
            {
                SerializedProperty m_Property;

                public PropertyScope(SerializedProperty property)
                {
                    m_Property = property;
                    m_Property.Update();
                }

                public void Dispose()
                {
                    m_Property.ApplyModifiedProperties();
                }
            }
        }
#endif // UNITY_EDITOR
    }
}
