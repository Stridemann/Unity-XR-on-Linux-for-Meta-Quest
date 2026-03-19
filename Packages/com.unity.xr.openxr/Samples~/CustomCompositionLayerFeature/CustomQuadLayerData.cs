#if XR_COMPOSITION_LAYERS
using System;
using UnityEngine;
using Unity.XR.CompositionLayers.Extensions;
using Unity.XR.CompositionLayers.Layers;

/// <summary>
/// Example of defining a layer data script for a custom shape.
/// In this case, we are just creating a custom quad shape.
/// This composition layer type will appear in the type dropdown of the CompositionLayer component with what the Name is defined as.
/// </summary>
[Serializable]
[CompositionLayerData(
    Provider = "Unity",
    Name = "Custom Quad",
    IconPath = "",
    InspectorIcon = "",
    ListViewIcon = "",
    Description = "Custom shape example.",
    SuggestedExtenstionTypes = new[] { typeof(TexturesExtension) }
 )]

public class CustomQuadLayerData : LayerData
{
    [SerializeField]
    Vector2 m_Size = Vector2.one;

    [SerializeField]
    bool m_ApplyTransformScale = true;

    public Vector2 Size
    {
        get => m_Size;
        set => m_Size = UpdateValue(m_Size, value);
    }

    public bool ApplyTransformScale
    {
        get => m_ApplyTransformScale;
        set => m_ApplyTransformScale = UpdateValue(m_ApplyTransformScale, value);
    }

    public Vector2 GetScaledSize(Vector3 scale)
    {
        return m_ApplyTransformScale ? scale * m_Size : m_Size;
    }

    public override void CopyFrom(LayerData layerData)
    {
        if (layerData is CustomQuadLayerData customQuadLayerData)
        {
            m_Size = customQuadLayerData.Size;
            m_ApplyTransformScale = customQuadLayerData.ApplyTransformScale;
        }
    }
}
#endif
