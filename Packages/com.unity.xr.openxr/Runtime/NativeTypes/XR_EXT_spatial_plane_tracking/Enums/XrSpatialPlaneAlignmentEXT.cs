namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Plane alignment component type. Describes the orientation of a plane associated with a spatial entity.
    /// Provided by `XR_EXT_spatial_plane_tracking`.
    /// </summary>
    public enum XrSpatialPlaneAlignmentEXT
    {
        /// <summary>
        /// The entity is horizontally aligned and faces upward, such as a floor.
        /// Equivalent to the OpenXR value `XR_SPATIAL_PLANE_ALIGNMENT_HORIZONTAL_UPWARD_EXT`.
        /// </summary>
        HorizontalUpward = 0,

        /// <summary>
        /// The entity is horizontally aligned and faces downward, such as a ceiling.
        /// Equivalent to the OpenXR value `XR_SPATIAL_PLANE_ALIGNMENT_HORIZONTAL_DOWNWARD_EXT`.
        /// </summary>
        HorizontalDownward = 1,

        /// <summary>
        /// The entity is vertically aligned, such as a wall.
        /// Equivalent to the OpenXR value `XR_SPATIAL_PLANE_ALIGNMENT_VERTICAL_EXT`.
        /// </summary>
        Vertical = 2,

        /// <summary>
        /// The entity has an arbitrary, non-vertical and non-horizontal alignment.
        /// Equivalent to the OpenXR value `XR_SPATIAL_PLANE_ALIGNMENT_ARBITRARY_EXT`.
        /// </summary>
        Arbitrary = 3
    }
}
