using XrFutureEXT = System.UInt64;
using XrSpace = System.UInt64;
using XrTime = System.Int64;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// The completion info for <see cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrCreateSpatialDiscoverySnapshotCompleteEXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrCreateSpatialDiscoverySnapshotCompletionInfoEXT@,UnityEngine.XR.OpenXR.NativeTypes.XrCreateSpatialDiscoverySnapshotCompletionEXT@)"/>.
    /// Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use a constructor with parameters to ensure that <see cref="type"/> is correctly initialized
    /// > to <see cref="XrStructureType.CreateSpatialDiscoverySnapshotCompletionInfoEXT"/>.
    /// </remarks>
    public readonly unsafe struct XrCreateSpatialDiscoverySnapshotCompletionInfoEXT
    {
        /// <summary>
        /// The `XrStructureType` of this struct:
        /// <see cref="XrStructureType.CreateSpatialDiscoverySnapshotCompletionInfoEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        public void* next { get; }

        /// <summary>
        /// The `XrSpace` in which all the locations of the discovery snapshot will be located.
        /// </summary>
        public XrSpace baseSpace { get; }

        /// <summary>
        /// The `XrTime` at which all the locations of the discovery snapshot will be located.
        /// </summary>
        public XrTime time { get; }

        /// <summary>
        /// The future received from <see cref="OpenXRNativeApi.xrCreateSpatialDiscoverySnapshotAsyncEXT"/>.
        /// </summary>
        public XrFutureEXT future { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="baseSpace">The base space.</param>
        /// <param name="time">The time.</param>
        /// <param name="future">The future.</param>
        public XrCreateSpatialDiscoverySnapshotCompletionInfoEXT(
            void* next, XrSpace baseSpace, XrTime time, XrFutureEXT future)
        {
            type = XrStructureType.CreateSpatialDiscoverySnapshotCompletionInfoEXT;
            this.next = next;
            this.baseSpace = baseSpace;
            this.time = time;
            this.future = future;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="baseSpace">The base space.</param>
        /// <param name="time">The time.</param>
        /// <param name="future">The future.</param>
        public XrCreateSpatialDiscoverySnapshotCompletionInfoEXT(XrSpace baseSpace, XrTime time, XrFutureEXT future)
            : this(null, baseSpace, time, future) { }
    }
}
