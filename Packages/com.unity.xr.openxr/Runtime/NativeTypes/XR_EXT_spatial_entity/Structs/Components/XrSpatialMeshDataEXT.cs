using System;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// The mesh component. Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    public readonly struct XrSpatialMeshDataEXT : IEquatable<XrSpatialMeshDataEXT>
    {
        /// <summary>
        /// A pose defining the origin of the mesh. All vertices of the mesh must be relative to this origin.
        /// </summary>
        public XrPosef origin { get; }

        /// <summary>
        /// Buffer that contains the vertices of the mesh.
        /// </summary>
        public XrSpatialBufferEXT vertexBuffer { get; }

        /// <summary>
        /// Buffer that specifies the triangles of the mesh using the indices of <see cref="vertexBuffer"/>.
        /// The indices must be returned in counter-clockwise winding order with three indices per triangle.
        /// </summary>
        public XrSpatialBufferEXT indexBuffer { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="origin">The origin.</param>
        /// <param name="vertexBuffer">The vertex buffer.</param>
        /// <param name="indexBuffer">The index buffer.</param>
        public XrSpatialMeshDataEXT(XrPosef origin, XrSpatialBufferEXT vertexBuffer, XrSpatialBufferEXT indexBuffer)
        {
            this.origin = origin;
            this.vertexBuffer = vertexBuffer;
            this.indexBuffer = indexBuffer;
        }

        /// <summary>
        /// Compares for equality. Two instances are equal if their bits are exactly
        /// identical for the `origin`, `vertexBuffer`, and `indexBuffer` properties.
        /// </summary>
        /// <param name="other">The other instance.</param>
        /// <returns>`true` if the instances are equal. Otherwise, `false`.</returns>
        public bool Equals(XrSpatialMeshDataEXT other)
        {
            return origin.Equals(other.origin)
                && vertexBuffer.Equals(other.vertexBuffer)
                && indexBuffer.Equals(other.indexBuffer);
        }

        /// <summary>
        /// Compares for equality. Two instances are equal if their bits are exactly
        /// identical for the `origin`, `vertexBuffer`, and `indexBuffer` properties.
        /// </summary>
        /// <param name="obj">The other object.</param>
        /// <returns>`true` if `obj` is an `XrSpatialMeshDataEXT` and equal to this instance.
        /// Otherwise, `false`.</returns>
        public override bool Equals(object obj)
        {
            return obj is XrSpatialMeshDataEXT other && Equals(other);
        }

        /// <summary>
        /// Generates a unique hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(origin, vertexBuffer, indexBuffer);
        }
    }
}
