namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Plane semantic label component type. Describes a set of semantic labels for planes.
    /// Provided by `XR_EXT_spatial_plane_tracking`.
    /// </summary>
    public enum XrSpatialPlaneSemanticLabelEXT
    {
        /// <summary>
        /// The runtime was unable to classify this entity.
        /// Equivalent to the OpenXR value `XR_SPATIAL_PLANE_SEMANTIC_LABEL_UNCATEGORIZED_EXT`.
        /// </summary>
        Uncategorized = 1,

        /// <summary>
        /// The entity is a floor.
        /// Equivalent to the OpenXR value `XR_SPATIAL_PLANE_SEMANTIC_LABEL_FLOOR_EXT`.
        /// </summary>
        Floor = 2,

        /// <summary>
        /// The entity is a wall.
        /// Equivalent to the OpenXR value `XR_SPATIAL_PLANE_SEMANTIC_LABEL_WALL_EXT`.
        /// </summary>
        Wall = 3,

        /// <summary>
        /// The entity is a ceiling.
        /// Equivalent to the OpenXR value `XR_SPATIAL_PLANE_SEMANTIC_LABEL_CEILING_EXT`.
        /// </summary>
        Ceiling = 4,

        /// <summary>
        /// The entity is a table.
        /// Equivalent to the OpenXR value `XR_SPATIAL_PLANE_SEMANTIC_LABEL_TABLE_EXT`.
        /// </summary>
        Table = 5
    }
}
