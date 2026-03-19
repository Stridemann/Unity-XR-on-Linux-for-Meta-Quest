using System;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// A construct representing a position and orientation within a space, with position expressed in meters, and orientation represented as a unit quaternion.
    /// <a href="https://registry.khronos.org/OpenXR/specs/1.0/man/html/XrPosef.html">OpenXR Spec</a>
    /// </summary>
    public struct XrPosef : IEquatable<XrPosef>
    {
        /// <summary>
        /// The orientation/rotation of the pose.
        /// </summary>
        public XrQuaternionf Orientation;

        /// <summary>
        /// The position of the pose.
        /// </summary>
        public XrVector3f Position;

        /// <summary>
        /// Construct an instance from a position and rotation in Unity space relative to your XR Origin.
        /// </summary>
        /// <param name="vec3">The position in Unity space relative to your XR Origin.</param>
        /// <param name="quaternion">The rotation in Unity space relative to your XR Origin.</param>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > This constructor negates the position `z` value and the rotation `x` and `y` values to convert from
        /// Unity's left-handed coordinate system to the right-handed system used by OpenXR.
        /// </remarks>
        public XrPosef(Vector3 vec3, Quaternion quaternion)
        {
            Position = new XrVector3f(vec3);
            Orientation = new XrQuaternionf(quaternion);
        }

        /// <summary>
        /// Construct an instance from a `pose` in Unity space relative to your XR Origin.
        /// </summary>
        /// <param name="pose">The pose in Unity space relative to your XR Origin.</param>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > This constructor negates the position `z` value and the rotation `x` and `y` values to convert from
        /// Unity's left-handed coordinate system to the right-handed system used by OpenXR.
        /// </remarks>
        public XrPosef(Pose pose)
        {
            Position = new XrVector3f(pose.position);
            Orientation = new XrQuaternionf(pose.rotation);
        }

        /// <summary>
        /// Construct an instance from a position and rotation in session space.
        /// This method applies no transformations to the input values.
        /// </summary>
        /// <param name="position">The position in session space.</param>
        /// <param name="rotation">The rotation in session space.</param>
        /// <returns>The instance.</returns>
        public static XrPosef FromSessionSpaceCoordinates(Vector3 position, Quaternion rotation)
        {
            return new XrPosef
            {
                Position = XrVector3f.FromSessionSpaceCoordinates(position),
                Orientation = XrQuaternionf.FromSessionSpaceCoordinates(rotation)
            };
        }

        /// <summary>
        /// Construct an instance from a pose in session space.
        /// This method applies no transformations to the input values.
        /// </summary>
        /// <param name="pose">The pose in session space.</param>
        /// <returns>The instance.</returns>
        public static XrPosef FromSessionSpaceCoordinates(Pose pose)
        {
            return new XrPosef
            {
                Position = XrVector3f.FromSessionSpaceCoordinates(pose.position),
                Orientation = XrQuaternionf.FromSessionSpaceCoordinates(pose.rotation)
            };
        }

        /// <summary>
        /// Convert this instance to a Unity `Pose`. This method applies no transformation to
        /// <see cref="Position"/> or <see cref="Orientation"/>.
        /// </summary>
        /// <returns>The `Pose`.</returns>
        public Pose ToSessionSpacePose()
        {
            return new Pose(Position.ToSessionSpaceVector3(), Orientation.ToSessionSpaceQuaternion());
        }

        /// <summary>
        /// Compares for equality.
        /// Two instances are equal if their bits are exactly identical for the `Orientation` and `Position` fields.
        /// </summary>
        /// <param name="other">The other instance.</param>
        /// <returns>`true` if the instances are equal. Otherwise, `false`.</returns>
        public bool Equals(XrPosef other)
        {
            return Orientation.Equals(other.Orientation) && Position.Equals(other.Position);
        }

        /// <summary>
        /// Compares for equality.
        /// Two instances are equal if their bits are exactly identical for the `Orientation` and `Position` fields.
        /// </summary>
        /// <param name="obj">The other object.</param>
        /// <returns>`true` if `obj` is an `XrPosef` and equal to this instance. Otherwise, `false`.</returns>
        public override bool Equals(object obj)
        {
            return obj is XrPosef other && Equals(other);
        }

        /// <summary>
        /// Generates a unique hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Orientation, Position);
        }
    }
}
