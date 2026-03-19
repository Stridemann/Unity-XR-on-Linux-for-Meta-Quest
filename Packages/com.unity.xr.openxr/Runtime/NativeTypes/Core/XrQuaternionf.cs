using System;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Represents a quaternion in OpenXR coordinates.
    /// </summary>
    public struct XrQuaternionf : IEquatable<XrQuaternionf>
    {
        /// <summary>
        /// The x-coordinate of the quaternion, in OpenXR coordinates.
        /// </summary>
        public float X;

        /// <summary>
        /// The y-coordinate of the quaternion, in OpenXR coordinates.
        /// </summary>
        public float Y;

        /// <summary>
        /// The z-coordinate of the quaternion, in OpenXR coordinates.
        /// </summary>
        public float Z;

        /// <summary>
        /// The w-coordinate of the quaternion, in OpenXR coordinates.
        /// </summary>
        public float W;

        /// <summary>
        /// Construct an instance from the given Unity coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate from Unity's coordinate system.</param>
        /// <param name="y">The y-coordinate from Unity's coordinate system.</param>
        /// <param name="z">The z-coordinate from Unity's coordinate system.</param>
        /// <param name="w">The w-coordinate from Unity's coordinate system.</param>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > This constructor negates the <paramref name="x"/> and <paramref name="y"/> values to convert from
        /// > Unity's left-handed coordinate system to the right-handed system used by OpenXR.
        /// </remarks>
        public XrQuaternionf(float x, float y, float z, float w)
        {
            X = -x;
            Y = -y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Construct an instance from the given Unity quaternion.
        /// </summary>
        /// <param name="quaternion">The quaternion in Unity coordinates.</param>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > This constructor negates the `x` and `y` values of the input quaternion to convert from
        /// > Unity's left-handed coordinate system to the right-handed system used by OpenXR.
        /// </remarks>
        public XrQuaternionf(Quaternion quaternion)
        {
            X = -quaternion.x;
            Y = -quaternion.y;
            Z = quaternion.z;
            W = quaternion.w;
        }

        /// <summary>
        /// Construct an instance from the given session-space coordinates.
        /// This method applies no transformations to the input values.
        /// </summary>
        /// <param name="x">The x-coordinate in session space.</param>
        /// <param name="y">The y-coordinate in session space.</param>
        /// <param name="z">The z-coordinate in session space.</param>
        /// <param name="w">The w-coordinate in session space.</param>
        /// <returns>The instance.</returns>
        public static XrQuaternionf FromSessionSpaceCoordinates(float x, float y, float z, float w)
        {
            return new XrQuaternionf
            {
                X = x,
                Y = y,
                Z = z,
                W = w
            };
        }

        /// <summary>
        /// Construct an instance from the given session-space coordinates.
        /// This method applies no transformations to the input values.
        /// </summary>
        /// <param name="quaternion">The quaternion in session space.</param>
        /// <returns>The instance.</returns>
        public static XrQuaternionf FromSessionSpaceCoordinates(Quaternion quaternion)
        {
            return new XrQuaternionf
            {
                X = quaternion.x,
                Y = quaternion.y,
                Z = quaternion.z,
                W = quaternion.w
            };
        }

        /// <summary>
        /// Convert this instance in OpenXR coordinates to a `Quaternion` in Unity coordinates.
        /// </summary>
        /// <returns>The output `Quaternion`.</returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > This method negates the <see cref="X"/> and <see cref="Y"/> values to convert from OpenXR's
        /// > right-handed coordinate system to the left-handed system used by Unity.
        /// </remarks>
        public Quaternion AsQuaternion()
        {
            return new Quaternion(-X, -Y, Z, W);
        }

        /// <summary>
        /// Convert this instance to a Unity `Quaternion`. This method applies no transformation to
        /// <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, or <see cref="W"/>.
        /// </summary>
        /// <returns>The `Quaternion`.</returns>
        public Quaternion ToSessionSpaceQuaternion()
        {
            return new Quaternion(X, Y, Z, W);
        }

        /// <summary>
        /// Compares for equality.
        /// Two instances are equal if their bits are exactly identical for the `X`, `Y`, `Z`, and `W` fields.
        /// </summary>
        /// <param name="other">The other instance.</param>
        /// <returns>`true` if the instances are equal. Otherwise, `false`.</returns>
        public bool Equals(XrQuaternionf other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z) && W.Equals(other.W);
        }

        /// <summary>
        /// Compares for equality.
        /// Two instances are equal if their bits are exactly identical for the `X`, `Y`, `Z`, and `W` fields.
        /// </summary>
        /// <param name="obj">The other `object`.</param>
        /// <returns>`true` if `obj` is an `XrQuaternionf` and equal to this instance. Otherwise, `false`.</returns>
        public override bool Equals(object obj)
        {
            return obj is XrQuaternionf other && Equals(other);
        }

        /// <summary>
        /// Generates a unique hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z, W);
        }
    }
}
