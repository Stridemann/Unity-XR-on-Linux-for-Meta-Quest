using System;
using XrSpatialBufferIdEXT = System.UInt64;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Represents of buffer of variable-sized data contained in spatial component. Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    public readonly struct XrSpatialBufferEXT : IEquatable<XrSpatialBufferEXT>
    {
        /// <summary>
        /// The ID of the buffer.
        /// </summary>
        public XrSpatialBufferIdEXT bufferId { get; }

        /// <summary>
        /// The type of data contained in the buffer, used to determine which method to call to retrieve the data.
        /// </summary>
        public XrSpatialBufferTypeEXT bufferType { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="bufferId">The buffer ID.</param>
        /// <param name="bufferType">The buffer type.</param>
        public XrSpatialBufferEXT(XrSpatialBufferIdEXT bufferId, XrSpatialBufferTypeEXT bufferType)
        {
            this.bufferId = bufferId;
            this.bufferType = bufferType;
        }

        /// <summary>
        /// Compares for equality.
        /// Two instances are equal if they share the same buffer ID and buffer type.
        /// </summary>
        /// <param name="other">The other instance.</param>
        /// <returns>`true` if the instances are equal. Otherwise, `false`.</returns>
        public bool Equals(XrSpatialBufferEXT other)
        {
            return bufferId == other.bufferId && bufferType == other.bufferType;
        }

        /// <summary>
        /// Compares for equality.
        /// Two instances are equal if they share the same buffer ID and buffer type.
        /// </summary>
        /// <param name="obj">The other object.</param>
        /// <returns>`true` if `obj` is an `XrSpatialBufferEXT` and equal to this instance. Otherwise, `false`.</returns>
        public override bool Equals(object obj)
        {
            return obj is XrSpatialBufferEXT other && Equals(other);
        }

        /// <summary>
        /// Generate a unique hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(bufferId, (int)bufferType);
        }
    }
}
