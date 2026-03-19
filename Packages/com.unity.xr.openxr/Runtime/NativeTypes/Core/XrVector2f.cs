using System;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Two-dimensional vector.
    /// </summary>
    public struct XrVector2f : IEquatable<XrVector2f>
    {
        /// <summary>
        /// The x coordinate of the vector.
        /// </summary>
        public float X;

        /// <summary>
        /// The y coordinate of the vector.
        /// </summary>
        public float Y;

        /// <summary>
        /// Constructor for two float values.
        /// </summary>
        /// <param name="x">The x coordinate of the vector.</param>
        /// <param name="y">The y coordinate of the vector.</param>
        public XrVector2f(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Initializes and returns an instance of XrVector2f with the provided parameters.
        /// </summary>
        /// <param name="value">Vector2 struct coming from Unity that is translated into the OpenXR XrVector2f struct.</param>
        public XrVector2f(Vector2 value)
        {
            X = value.x;
            Y = value.y;
        }

        /// <summary>
        /// Convert this instance to a `Vector2`.
        /// </summary>
        /// <returns>The output `Vector2`.</returns>
        public Vector2 AsVector2()
        {
            return new Vector2(X, Y);
        }

        /// <summary>
        /// Compares for equality.
        /// Two instances are equal if their bits are exactly identical for the `X` and `Y` fields.
        /// </summary>
        /// <param name="other">The other instance.</param>
        /// <returns>`true` if the instances are equal. Otherwise, `false`.</returns>
        public bool Equals(XrVector2f other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        /// <summary>
        /// Compares for equality.
        /// Two instances are equal if their bits are exactly identical for the `X` and `Y` fields.
        /// </summary>
        /// <param name="obj">The other object.</param>
        /// <returns>`true` if `obj` is an `XrVector2f` and equal to this instance. Otherwise, `false`.</returns>
        public override bool Equals(object obj)
        {
            return obj is XrVector2f other && Equals(other);
        }

        /// <summary>
        /// Generates a unique hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
