namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Defines the types of result codes for a persistence operation.
    /// Provided by `XR_EXT_spatial_persistence`.
    /// </summary>
    public enum XrSpatialPersistenceContextResultEXT
    {
        /// <summary>
        /// The persistence context operation was a success.
        /// </summary>
        Success = 0,

        /// <summary>
        /// The persistence operation failed because the entity could not be tracked by the runtime.
        /// (Added by the `XR_EXT_spatial_persistence_operations` extension)
        /// </summary>
        EntityNotTracking = -1000781001,

        /// <summary>
        /// The provided persist UUID was not found in storage.
        /// (Added by the `XR_EXT_spatial_persistence_operations` extension)
        /// </summary>
        PersistUuidNotFound = -1000781002
    }

    /// <summary>
    /// Extension methods for <see cref="XrSpatialPersistenceContextResultEXT"/>.
    /// </summary>
    public static class XrSpatialPersistenceContextResultEXTExtensions
    {
        /// <summary>
        /// Indicates whether the persistence context result represents a success code.
        /// </summary>
        /// <param name="result">The persistence context result.</param>
        /// <returns>`true` if the result represents a success code. Otherwise, `false`.</returns>
        public static bool IsSuccess(this XrSpatialPersistenceContextResultEXT result)
        {
            return result >= 0;
        }

        /// <summary>
        /// Indicates whether the persistence context result represents an error code.
        /// Equivalent to `!IsSuccess()`.
        /// </summary>
        /// <param name="result">The persistence context result.</param>
        /// <returns>`true` if the result represents an error code. Otherwise, `false`.</returns>
        public static bool IsError(this XrSpatialPersistenceContextResultEXT result)
        {
            return result < 0;
        }
    }
}
