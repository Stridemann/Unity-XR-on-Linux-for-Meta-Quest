using System;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Extent in two dimensions.
    /// </summary>
    public struct XrExtent2Df : IEquatable<XrExtent2Df>
    {
        /// <summary>
        /// The floating-point width of the extent.
        /// </summary>
        public float Width;

        /// <summary>
        /// The floating-point height of the extent.
        /// </summary>
        public float Height;

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="width">The width value.</param>
        /// <param name="height">The height value.</param>
        public XrExtent2Df(float width, float height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Compares for equality.
        /// Two instances are equal if their bits are exactly identical for the `Width` and `Height` fields.
        /// </summary>
        /// <param name="other">The other instance.</param>
        /// <returns>`true` if the instances are equal. Otherwise, `false.</returns>
        public bool Equals(XrExtent2Df other)
        {
            return Width.Equals(other.Width) && Height.Equals(other.Height);
        }

        /// <summary>
        /// Compares for equality.
        /// Two instances are equal if their bits are exactly identical for the `Width` and `Height` fields.
        /// </summary>
        /// <param name="obj">The other object.</param>
        /// <returns>`true` if `obj` is an `XrExtent2Df` and equal to this instance. Otherwise, `false`.</returns>
        public override bool Equals(object obj)
        {
            return obj is XrExtent2Df other && Equals(other);
        }

        /// <summary>
        /// Generate a unique hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Width, Height);
        }
    }
}
