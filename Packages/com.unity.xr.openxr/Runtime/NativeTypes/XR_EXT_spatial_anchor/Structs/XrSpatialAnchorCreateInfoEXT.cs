using XrSpace = System.UInt64;
using XrTime = System.Int64;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Creation info struct used by
    /// <see cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrCreateSpatialAnchorEXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialAnchorCreateInfoEXT@,System.UInt64@,System.UInt64@)"/>.
    /// Provided by `XR_EXT_spatial_anchor`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/> is correctly initialized
    /// > to <see cref="XrStructureType.SpatialAnchorCreateInfoEXT"/>.
    /// </remarks>
    public readonly unsafe struct XrSpatialAnchorCreateInfoEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.SpatialAnchorCreateInfoEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        public void* next { get; }

        /// <summary>
        /// The `XrSpace` in which <see cref="pose"/> is applied.
        /// </summary>
        public XrSpace baseSpace { get; }

        /// <summary>
        /// The `XrTime` at which <see cref="baseSpace"/> is located (and <see cref="pose"/> is applied).
        /// </summary>
        public XrTime time { get; }

        /// <summary>
        /// The location for the anchor entity, in OpenXR coordinate space.
        /// </summary>
        public XrPosef pose { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="baseSpace">The base space.</param>
        /// <param name="time">The time.</param>
        /// <param name="pose">The pose, in OpenXR coordinates.</param>
        public XrSpatialAnchorCreateInfoEXT(void* next, XrSpace baseSpace, XrTime time, XrPosef pose)
        {
            type = XrStructureType.SpatialAnchorCreateInfoEXT;
            this.next = next;
            this.baseSpace = baseSpace;
            this.time = time;
            this.pose = pose;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="baseSpace">The base space.</param>
        /// <param name="time">The time.</param>
        /// <param name="pose">The pose, in OpenXR coordinates.</param>
        public XrSpatialAnchorCreateInfoEXT(XrSpace baseSpace, XrTime time, XrPosef pose)
            : this(null, baseSpace, time, pose) { }

        /// <summary>
        /// Construct an instance from Unity coordinates.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="baseSpace">The base space.</param>
        /// <param name="time">The time.</param>
        /// <param name="position">The position, in Unity coordinates relative to your XR Origin.</param>
        /// <param name="rotation">The rotation, in Unity coordinates relative to your XR Origin.</param>
        public XrSpatialAnchorCreateInfoEXT(
            void* next, XrSpace baseSpace, XrTime time, Vector3 position, Quaternion rotation)
            : this(next, baseSpace, time, new XrPosef(position, rotation)) { }

        /// <summary>
        /// Construct an instance with a `null` next pointer from Unity coordinates.
        /// </summary>
        /// <param name="baseSpace">The base space.</param>
        /// <param name="time">The time.</param>
        /// <param name="position">The position, in Unity coordinates relative to your XR Origin.</param>
        /// <param name="rotation">The rotation, in Unity coordinates relative to your XR Origin.</param>
        public XrSpatialAnchorCreateInfoEXT(XrSpace baseSpace, XrTime time, Vector3 position, Quaternion rotation)
            : this(null, baseSpace, time, new XrPosef(position, rotation)) { }
    }
}
