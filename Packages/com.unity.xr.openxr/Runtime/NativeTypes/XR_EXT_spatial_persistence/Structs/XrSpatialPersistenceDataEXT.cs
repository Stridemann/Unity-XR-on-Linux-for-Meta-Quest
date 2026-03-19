using System;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// The persistence component. Provided by `XR_EXT_spatial_persistence`.
    /// </summary>
    public readonly struct XrSpatialPersistenceDataEXT : IEquatable<XrSpatialPersistenceDataEXT>
    {
        /// <summary>
        /// The unique identifier of the persisted entity.
        /// </summary>
        public XrUuid persistUuid { get; }

        /// <summary>
        /// The persistence state of the entity identified by <see cref="persistUuid"/>.
        /// </summary>
        public XrSpatialPersistenceStateEXT persistState { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="persistUuid">The unique identifier of the persisted entity.</param>
        /// <param name="persistState">The persistence state.</param>
        public XrSpatialPersistenceDataEXT(XrUuid persistUuid, XrSpatialPersistenceStateEXT persistState)
        {
            this.persistUuid = persistUuid;
            this.persistState = persistState;
        }

        /// <summary>
        /// Compares for equality.
        /// Two instances are equal if their `persistId` and `persistState` properties are equal.
        /// </summary>
        /// <param name="other">The other instance.</param>
        /// <returns>`true` if the instances are equal. Otherwise, `false.</returns>
        public bool Equals(XrSpatialPersistenceDataEXT other)
        {
            return persistUuid.Equals(other.persistUuid) && persistState == other.persistState;
        }

        /// <summary>
        /// Compares for equality.
        /// Two instances are equal if their `persistId` and `persistState` properties are equal.
        /// </summary>
        /// <param name="obj">The other instance.</param>
        /// <returns>`true` if `obj` is an `XrSpatialPersistenceDataEXT` and equal to this instance.
        /// Otherwise, `false`.</returns>
        public override bool Equals(object obj)
        {
            return obj is XrSpatialPersistenceDataEXT other && Equals(other);
        }

        /// <summary>
        /// Generates a unique hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(persistUuid, (int)persistState);
        }
    }
}
