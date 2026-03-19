namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Represents the types of components that can exist on a spatial entity. Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    public enum XrSpatialComponentTypeEXT
    {
        /// <summary>
        /// Represents the Bounded 2D component type.
        /// Equivalent to the OpenXR value `XR_SPATIAL_COMPONENT_TYPE_BOUNDED_2D_EXT`.
        /// </summary>
        Bounded2D = 1,

        /// <summary>
        /// Represents the Bounded 3D component type.
        /// Equivalent to the OpenXR value `XR_SPATIAL_COMPONENT_TYPE_BOUNDED_3D_EXT`.
        /// </summary>
        Bounded3D = 2,

        /// <summary>
        /// Represents the Parent component type.
        /// Equivalent to the OpenXR value `XR_SPATIAL_COMPONENT_TYPE_PARENT_EXT`.
        /// </summary>
        Parent = 3,

        /// <summary>
        /// Represents the Mesh 3D component type.
        /// Equivalent to the OpenXR value `XR_SPATIAL_COMPONENT_TYPE_MESH_3D_EXT`.
        /// </summary>
        Mesh3D = 4,

        /// <summary>
        /// Represents the Plane Alignment component type provided by `XR_EXT_spatial_plane_tracking`.
        /// Equivalent to the OpenXR value `XR_SPATIAL_COMPONENT_TYPE_PLANE_ALIGNMENT_EXT`.
        /// </summary>
        PlaneAlignment = 1000741000,

        /// <summary>
        /// Represents the Mesh 2D component type provided by `XR_EXT_spatial_plane_tracking`.
        /// Equivalent to the OpenXR value `XR_SPATIAL_COMPONENT_TYPE_MESH_2D_EXT`.
        /// </summary>
        Mesh2D = 1000741001,

        /// <summary>
        /// Represents the Polygon 2D component type provided by `XR_EXT_spatial_plane_tracking`.
        /// Equivalent to the OpenXR value `XR_SPATIAL_COMPONENT_TYPE_POLYGON_2D_EXT`.
        /// </summary>
        Polygon2D = 1000741002,

        /// <summary>
        /// Represents the Plane Semantic Label component type provided by `XR_EXT_spatial_plane_tracking`.
        /// Equivalent to the OpenXR value `XR_SPATIAL_COMPONENT_TYPE_PLANE_SEMANTIC_LABEL_EXT`.
        /// </summary>
        PlaneSemanticLabel = 1000741003,

        /// <summary>
        /// Represents the Marker component type provided by `XR_EXT_spatial_marker_tracking`.
        /// Equivalent to the OpenXR value `XR_SPATIAL_COMPONENT_TYPE_MARKER_EXT`.
        /// </summary>
        Marker = 1000743000,

        /// <summary>
        /// Represents the Anchor component type provided by `XR_EXT_spatial_anchor`.
        /// Equivalent to the OpenXR value `XR_SPATIAL_COMPONENT_TYPE_ANCHOR_EXT`.
        /// </summary>
        Anchor = 1000762000,

        /// <summary>
        /// Represents the Persistence component type provided by `XR_EXT_spatial_persistence`.
        /// Equivalent to the OpenXR value `XR_SPATIAL_COMPONENT_TYPE_PERSISTENCE_EXT`.
        /// </summary>
        Persistence = 1000763000,
    }
}
