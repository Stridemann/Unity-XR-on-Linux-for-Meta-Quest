using System;
using UnityEngine.Assertions;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// The marker component. Provided by `XR_EXT_spatial_marker_tracking`.
    /// </summary>
    public readonly struct XrSpatialMarkerDataEXT : IEquatable<XrSpatialMarkerDataEXT>
    {
        /// <summary>
        /// The capability that detected the marker.
        /// </summary>
        public XrSpatialCapabilityEXT capability { get; }

        /// <summary>
        /// The ID of the marker. For ArUco markers and AprilTags, this property must be nonzero.
        /// For QR codes, this field must be zero.
        /// </summary>
        public uint markerId { get; }

        /// <summary>
        /// The buffer ID and type of additional information contained in the marker.
        /// ArUco markers and AprilTags do not support additional information, and for those marker types the
        /// `data.bufferId` value must be zero.
        /// </summary>
        public XrSpatialBufferEXT data { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="capability">The capability that detected the marker.</param>
        /// <param name="markerId">The ID of the marker. Must be zero for QR codes.</param>
        /// <param name="data">The buffer ID and type of additional information contained in the marker.</param>
        public XrSpatialMarkerDataEXT(XrSpatialCapabilityEXT capability, uint markerId, XrSpatialBufferEXT data)
        {
#if UNITY_ASSERTIONS
            if (capability is XrSpatialCapabilityEXT.MarkerTrackingQRCode
                or XrSpatialCapabilityEXT.MarkerTrackingMicroQRCode)
            {
                Assert.AreEqual(0, markerId);
                Assert.IsTrue(data.bufferId != 0);
            }
            else
            {
                Assert.IsTrue(markerId != 0);
                Assert.AreEqual(0, data.bufferId);
            }
#endif
            this.capability = capability;
            this.markerId = markerId;
            this.data = data;
        }

        /// <summary>
        /// Compares for equality.
        /// Two instances are equal if their `capability`, `markerId`, and `data` are equal.
        /// </summary>
        /// <param name="other">The other instance.</param>
        /// <returns>`true` if the instances are equal. Otherwise, `false`.</returns>
        public bool Equals(XrSpatialMarkerDataEXT other)
        {
            return capability == other.capability && markerId == other.markerId && data.Equals(other.data);
        }

        /// <summary>
        /// Compares for equality.
        /// Two instances are equal if their `capability`, `markerId`, and `data` are equal.
        /// </summary>
        /// <param name="obj">The other object.</param>
        /// <returns>`true` if `obj` is an `XrSpatialMarkerDataEXT` and equal to this instance.
        /// Otherwise, `false`.</returns>
        public override bool Equals(object obj)
        {
            return obj is XrSpatialMarkerDataEXT other && Equals(other);
        }

        /// <summary>
        /// Generates a unique hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine((int)capability, markerId, data);
        }
    }
}
