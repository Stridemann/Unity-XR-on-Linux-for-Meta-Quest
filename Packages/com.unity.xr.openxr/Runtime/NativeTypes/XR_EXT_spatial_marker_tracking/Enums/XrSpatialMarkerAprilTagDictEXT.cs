namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Represents the supported configurations of predefined AprilTag dictionaries.
    /// Provided by `XR_spatial_marker_tracking`.
    /// </summary>
    public enum XrSpatialMarkerAprilTagDictEXT
    {
        /// <summary>
        /// 4 by 4 bit dictionary where the minimum Hamming distance between any two codes is 5. 30 codes.
        /// Equivalent to the OpenXR value `XR_SPATIAL_MARKER_APRIL_TAG_DICT_16H5_EXT`.
        /// </summary>
        Dict_16h5 = 1,

        /// <summary>
        /// 5 by 5 bit dictionary where the minimum Hamming distances between any two codes is 9. 35 codes.
        /// Equivalent to the OpenXR value `XR_SPATIAL_MARKER_APRIL_TAG_DICT_25H9_EXT`.
        /// </summary>
        Dict_25h9 = 2,

        /// <summary>
        /// 6 by 6 bit dictionary where the minimum Hamming distance between any two codes is 10. 2320 codes.
        /// Equivalent to the OpenXR value `XR_SPATIAL_MARKER_APRIL_TAG_DICT_36H10_EXT`.
        /// </summary>
        Dict_36h10 = 3,

        /// <summary>
        /// 6 by 6 bit dictionary where the minimum Hamming distance between any two codes is 11. 587 codes.
        /// Equivalent to the OpenXR value `XR_SPATIAL_MARKER_APRIL_TAG_DICT_36H11_EXT`.
        /// </summary>
        Dict_36h11 = 4,
    }
}
