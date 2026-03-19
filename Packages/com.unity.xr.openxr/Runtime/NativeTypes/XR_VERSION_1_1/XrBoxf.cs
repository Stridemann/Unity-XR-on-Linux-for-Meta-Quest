using System;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Represents a 3D bounding box. Provided by `XR_VERSION_1_1`.
    /// </summary>
    public readonly struct XrBoxf : IEquatable<XrBoxf>
    {
        /// <summary>
        /// The pose defining the center position and orientation of the bounding box within the
        /// reference frame of the corresponding `XrSpace`.
        /// </summary>
        public XrPosef center { get; }

        /// <summary>
        /// The edge-to-edge length of the box along each dimension with <see cref="center"/> as the center.
        /// </summary>
        public XrExtent3Df extents { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="center">The center.</param>
        /// <param name="extents">The extents.</param>
        public XrBoxf(XrPosef center, XrExtent3Df extents)
        {
            this.center = center;
            this.extents = extents;
        }

        /// <summary>
        /// Compares for equality. Two instances are equal if their bits are exactly
        /// identical for the `center` and `extents` properties.
        /// </summary>
        /// <param name="other">The other instance.</param>
        /// <returns>`true` if the instances are equal. Otherwise, `false`.</returns>
        public bool Equals(XrBoxf other)
        {
            return center.Equals(other.center) && extents.Equals(other.extents);
        }

        /// <summary>
        /// Compares for equality. Two instances are equal if their bits are exactly
        /// identical for the `center` and `extents` properties.
        /// </summary>
        /// <param name="obj">The other object.</param>
        /// <returns>`true` if `obj` is an `XrBoxf` and equal to this instance. Otherwise, `false`.</returns>
        public override bool Equals(object obj)
        {
            return obj is XrBoxf other && Equals(other);
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
