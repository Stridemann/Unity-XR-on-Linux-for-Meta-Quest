using System;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// The bounded 2D component. Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    public readonly struct XrSpatialBounded2DDataEXT : IEquatable<XrSpatialBounded2DDataEXT>
    {
        /// <summary>
        /// The geometric center of the bounded 2D component.
        /// </summary>
        public XrPosef center { get; }

        /// <summary>
        /// The extents of the bounded 2D component along the x-axis (extents.width), and
        /// y-axis (extents.height), centered on <see cref="center"/>.
        /// </summary>
        /// <remarks>
        /// The extents refer to the entity’s size in the x-y plane of the plane’s coordinate system.
        /// A plane with a position of `{0, 0, 0}`, rotation of `{0, 0, 0, 1}` (no rotation), and an extent of `{1, 1}`
        /// refers to a 1 meter x 1 meter plane centered at `{0, 0, 0}` with its front face normal vector pointing
        /// towards the +Z direction in the component’s space.
        /// </remarks>
        public XrExtent2Df extents { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="center">The center.</param>
        /// <param name="extents">The extents.</param>
        public XrSpatialBounded2DDataEXT(XrPosef center, XrExtent2Df extents)
        {
            this.center = center;
            this.extents = extents;
        }

        /// <summary>
        /// Compares for equality.
        /// Two instances are equal if their bits are exactly identical for the `center` and `extents` properties.
        /// </summary>
        /// <param name="other">The other instance.</param>
        /// <returns>`true` if the instances are equal. Otherwise, `false`.</returns>
        public bool Equals(XrSpatialBounded2DDataEXT other)
        {
            return center.Equals(other.center) && extents.Equals(other.extents);
        }

        /// <summary>
        /// Compares for equality.
        /// Two instances are equal if their bits are exactly identical for the `center` and `extents` properties.
        /// </summary>
        /// <param name="obj">The other object.</param>
        /// <returns>`true` if `obj` is an `XrSpatialBounded2DDataEXT` and equal to this instance.
        /// Otherwise, `false`.</returns>
        public override bool Equals(object obj)
        {
            return obj is XrSpatialBounded2DDataEXT other && Equals(other);
        }

        /// <summary>
        /// Generates a unique hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(center, extents);
        }
    }
}
