using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using XrSpatialEntityIdEXT = System.UInt64;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Represents the result of the `OpenXRNativeApi.xrQuerySpatialComponentDataEXT` operation.
    /// Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    /// <remarks>
    /// > [!WARNING]
    /// > Don't initialize this struct with the default parameterless constructor.
    /// > Use either <see cref="defaultValue"/> or a constructor with parameters to ensure that <see cref="type"/>
    /// > is correctly initialized to <see cref="XrStructureType.SpatialComponentDataQueryResultEXT"/>.
    /// </remarks>
    /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrQuerySpatialComponentDataEXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialComponentDataQueryConditionEXT@,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialComponentDataQueryResultEXT@)"/>
    /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrQuerySpatialComponentDataEXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialComponentDataQueryConditionEXT@,Unity.Collections.Allocator,Unity.Collections.NativeArray{System.UInt64}@,Unity.Collections.NativeArray{UnityEngine.XR.OpenXR.NativeTypes.XrSpatialEntityTrackingStateEXT}@)"/>
    public unsafe struct XrSpatialComponentDataQueryResultEXT
    {
        /// <summary>
        /// Get a default instance with an initialized <see cref="type"/> property.
        /// </summary>
        public static XrSpatialComponentDataQueryResultEXT defaultValue => new(0, 0, null, 0, 0, null);

        /// <summary>
        /// The `XrStructureType` of this struct: <see cref="XrStructureType.SpatialComponentDataQueryResultEXT"/>.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        /// <seealso cref="XrSpatialComponentAnchorListEXT"/>
        /// <seealso cref="XrSpatialComponentBounded2DListEXT"/>
        /// <seealso cref="XrSpatialComponentBounded3DListEXT"/>
        /// <seealso cref="XrSpatialComponentMarkerListEXT"/>
        /// <seealso cref="XrSpatialComponentMesh2DListEXT"/>
        /// <seealso cref="XrSpatialComponentMesh3DListEXT"/>
        /// <seealso cref="XrSpatialComponentParentListEXT"/>
        /// <seealso cref="XrSpatialComponentPersistenceListEXT"/>
        /// <seealso cref="XrSpatialComponentPlaneAlignmentListEXT"/>
        /// <seealso cref="XrSpatialComponentPlaneSemanticLabelListEXT"/>
        /// <seealso cref="XrSpatialComponentPolygon2DListEXT"/>
        public void* next { get; set; }

        /// <summary>
        /// The capacity of the <see cref="entityIds"/> array.
        /// </summary>
        public uint entityIdCapacityInput { get; set; }

        /// <summary>
        /// The count of elements in <see cref="entityIds"/>, or the required capacity if
        /// <see cref="entityIdCapacityInput"/> is insufficient.
        /// </summary>
        public uint entityIdCountOutput { get; set; }

        /// <summary>
        /// Pointer to an array of spatial entity IDs. Can be `null` if <see cref="entityIdCapacityInput"/> is `0`.
        /// </summary>
        public XrSpatialEntityIdEXT* entityIds { get; set; }

        /// <summary>
        /// The capacity of the <see cref="entityStates"/> array.
        /// </summary>
        public uint entityStateCapacityInput { get; set; }

        /// <summary>
        /// The count of elements in <see cref="entityStates"/>, or the required capacity if
        /// <see cref="entityIdCapacityInput"/> is insufficient.
        /// </summary>
        public uint entityStateCountOutput { get; set; }

        /// <summary>
        /// Pointer to an array of spatial entity tracking states.
        /// Can be `null` if <see cref="entityStateCapacityInput"/> is `0`.
        /// </summary>
        public XrSpatialEntityTrackingStateEXT* entityStates { get; set; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        /// <param name="entityIdCapacityInput">The entity ID capacity input.</param>
        /// <param name="entityIdCountOutput">The entity ID count output.</param>
        /// <param name="entityIds">The entity IDs.</param>
        /// <param name="entityStateCapacityInput">The entity state capacity input.</param>
        /// <param name="entityStateCountOutput">The entity state capacity output.</param>
        /// <param name="entityStates">The entity states.</param>
        public XrSpatialComponentDataQueryResultEXT(
            void* next,
            uint entityIdCapacityInput,
            uint entityIdCountOutput,
            XrSpatialEntityIdEXT* entityIds,
            uint entityStateCapacityInput,
            uint entityStateCountOutput,
            XrSpatialEntityTrackingStateEXT* entityStates)
        {
            type = XrStructureType.SpatialComponentDataQueryResultEXT;
            this.next = next;
            this.entityIdCapacityInput = entityIdCapacityInput;
            this.entityIdCountOutput = entityIdCountOutput;
            this.entityIds = entityIds;
            this.entityStateCapacityInput = entityStateCapacityInput;
            this.entityStateCountOutput = entityStateCountOutput;
            this.entityStates = entityStates;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="entityIdCapacityInput">The entity ID capacity input.</param>
        /// <param name="entityIdCountOutput">The entity ID count output.</param>
        /// <param name="entityIds">The entity IDs.</param>
        /// <param name="entityStateCapacityInput">The entity state capacity input.</param>
        /// <param name="entityStateCountOutput">The entity state capacity output.</param>
        /// <param name="entityStates">The entity states.</param>
        public XrSpatialComponentDataQueryResultEXT(
            uint entityIdCapacityInput,
            uint entityIdCountOutput,
            XrSpatialEntityIdEXT* entityIds,
            uint entityStateCapacityInput,
            uint entityStateCountOutput,
            XrSpatialEntityTrackingStateEXT* entityStates)
            : this(
                null,
                entityIdCapacityInput,
                entityIdCountOutput,
                entityIds,
                entityStateCapacityInput,
                entityStateCountOutput,
                entityStates)
        { }

        /// <summary>
        /// Construct an instance with default values for all members except the next pointer.
        /// </summary>
        /// <param name="next">The next pointer.</param>
        public XrSpatialComponentDataQueryResultEXT(void* next)
        {
            type = XrStructureType.SpatialComponentDataQueryResultEXT;
            this.next = next;
            entityIdCapacityInput = 0;
            entityIdCountOutput = 0;
            entityIds = null;
            entityStateCapacityInput = 0;
            entityStateCountOutput = 0;
            entityStates = null;
        }

        /// <summary>
        /// Set <see cref="entityIds"/> using the unsafe pointer of a native array.
        /// </summary>
        /// <param name="ids">The native array.</param>
        public void SetEntityIds(NativeArray<XrSpatialEntityIdEXT> ids)
        {
            entityIds = (XrSpatialEntityIdEXT*)ids.GetUnsafePtr();
        }

        /// <summary>
        /// Set <see cref="entityStates"/> using the unsafe pointer of a native array.
        /// </summary>
        /// <param name="states">The native array.</param>
        public void SetEntityStates(NativeArray<XrSpatialEntityTrackingStateEXT> states)
        {
            entityStates = (XrSpatialEntityTrackingStateEXT*)states.GetUnsafePtr();
        }
    }
}
