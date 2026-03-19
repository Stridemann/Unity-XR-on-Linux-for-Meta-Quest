using System;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Polygon 2D component. Provided by `XR_EXT_spatial_plane_tracking`.
    /// </summary>
    public readonly struct XrSpatialPolygon2DDataEXT : IEquatable<XrSpatialPolygon2DDataEXT>
    {
        /// <summary>
        /// The pose defining the origin of the polygon. All vertices of the polygon are relative to this origin
        /// in the X-Y plane.
        /// </summary>
        public XrPosef origin { get; }

        /// <summary>
        /// Vertex buffer of the entity that contains this component. Vertices must be in counter-clockwise order.
        /// The polygon represented by these vertices must not be self-intersecting, and may be concave.
        /// </summary>
        public XrSpatialBufferEXT vertexBuffer { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="origin">The origin.</param>
        /// <param name="vertexBuffer">The vertex buffer.</param>
        public XrSpatialPolygon2DDataEXT(XrPosef origin, XrSpatialBufferEXT vertexBuffer)
        {
            this.origin = origin;
            this.vertexBuffer = vertexBuffer;
        }

        /// <summary>
        /// Compares for equality.
        /// Two instances are equal if their `origin` and `vertexBuffer` properties are exactly equal.
        /// </summary>
        /// <param name="other">The other instance.</param>
        /// <returns>`true` if the instances are equal, otherwise `false`.</returns>
        public bool Equals(XrSpatialPolygon2DDataEXT other)
        {
            return origin.Equals(other.origin) && vertexBuffer.Equals(other.vertexBuffer);
        }

        /// <summary>
        /// Compares for equality.
        /// Two instances are equal if their `origin` and `vertexBuffer` properties are exactly equal.
        /// </summary>
        /// <param name="obj">The other object.</param>
        /// <returns>`true` if `obj` is an `XrSpatialPolygon2DDataEXT` and equal to this instance.
        /// Otherwise, `false`.</returns>
        public override bool Equals(object obj)
        {
            return obj is XrSpatialPolygon2DDataEXT other && Equals(other);
        }

        /// <summary>
        /// Generates a unique hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(origin, vertexBuffer);
        }
    }
}
