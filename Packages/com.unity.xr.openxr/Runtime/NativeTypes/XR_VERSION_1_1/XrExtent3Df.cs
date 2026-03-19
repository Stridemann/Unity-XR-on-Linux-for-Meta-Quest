using System;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Represents the extents of a 3D bounding box. The `width`, `height`, and `depth` values must be non-negative.
    /// Provided by `XR_VERSION_1_1`.
    /// </summary>
    public readonly struct XrExtent3Df : IEquatable<XrExtent3Df>
    {
        /// <summary>
        /// The floating-point width of the extent (x).
        /// </summary>
        public float width { get; }

        /// <summary>
        /// The floating-point height of the extent (y).
        /// </summary>
        public float height { get; }

        /// <summary>
        /// The floating-point depth of the extent (z).
        /// </summary>
        public float depth { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="width">The width value.</param>
        /// <param name="height">The height value.</param>
        /// <param name="depth">The depth value.</param>
        public XrExtent3Df(float width, float height, float depth)
        {
            this.width = width;
            this.height = height;
            this.depth = depth;
        }

        /// <summary>
        /// Compares for equality. Two instances are equal if their bits are exactly
        /// identical for the `width`, `height`, and `depth` properties.
        /// </summary>
        /// <param name="other">The other instance.</param>
        /// <returns>`true` if the instances are equal. Otherwise, `false`.</returns>
        public bool Equals(XrExtent3Df other)
        {
            return width.Equals(other.width) && height.Equals(other.height) && depth.Equals(other.depth);
        }

        /// <summary>
        /// Compares for equality. Two instances are equal if their bits are exactly
        /// identical for the `width`, `height`, and `depth` properties.
        /// </summary>
        /// <param name="obj">The other object.</param>
        /// <returns>`true` if `obj` is an `XrExtent3Df` and equal to this instance.. Otherwise, `false`.</returns>
        public override bool Equals(object obj)
        {
            return obj is XrExtent3Df other && Equals(other);
        }

        /// <summary>
        /// Generates a unique hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(width, height, depth);
        }
    }
}
