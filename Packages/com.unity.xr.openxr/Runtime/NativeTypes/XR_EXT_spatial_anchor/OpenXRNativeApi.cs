using System.Runtime.InteropServices;
using XrSpatialContextEXT = System.UInt64;
using XrSpatialEntityEXT = System.UInt64;
using XrSpatialEntityIdEXT = System.UInt64;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    public static partial class OpenXRNativeApi
    {
        /// <summary>
        /// Creates an anchor, allowing you to track the given position and rotation within the given spatial context.
        /// Provided by `XR_EXT_spatial_anchor`.
        /// </summary>
        /// <param name="spatialContext">A spatial context previously created using
        /// `OpenXRNativeApi.xrCreateSpatialContextAsyncEXT`.</param>
        /// <param name="createInfo">The creation info.</param>
        /// <param name="anchorEntityId">The ID of the created anchor entity.</param>
        /// <param name="anchorEntity">The spatial entity handle of the created anchor entity.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.LimitReached"/></description></item>
        ///   <item><description><see cref="XrResult.TimeInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.PoseInvalid"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        [DllImport(InternalConstants.openXRLibrary, EntryPoint = "EXT_spatial_anchor_xrCreateSpatialAnchorEXT")]
        public static extern XrResult xrCreateSpatialAnchorEXT(
            XrSpatialContextEXT spatialContext,
            in XrSpatialAnchorCreateInfoEXT createInfo,
            out XrSpatialEntityIdEXT anchorEntityId,
            out XrSpatialEntityEXT anchorEntity);

        /// <summary>
        /// Creates an anchor in your app space at the next frame's predicted display time,
        /// allowing you to track the given position and rotation within the given spatial context.
        /// Provided by `XR_EXT_spatial_anchor`.
        /// </summary>
        /// <param name="spatialContext">A spatial context previously created using
        /// `OpenXRNativeApi.xrCreateSpatialContextAsyncEXT`.</param>
        /// <param name="pose">The pose, in OpenXR coordinates, at which to create the anchor.</param>
        /// <param name="anchorEntityId">The ID of the created anchor entity.</param>
        /// <param name="anchorEntity">The spatial entity handle of the created anchor entity.</param>
        /// <returns>The result of the operation.\
        /// \
        /// `nativeStatusCode` success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// `nativeStatusCode` failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.LimitReached"/></description></item>
        ///   <item><description><see cref="XrResult.TimeInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.PoseInvalid"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        [DllImport(
            InternalConstants.openXRLibrary, EntryPoint = "EXT_spatial_anchor_xrCreateSpatialAnchorEXT_usingContext")]
        public static extern OpenXRResultStatus xrCreateSpatialAnchorEXT(
            XrSpatialContextEXT spatialContext,
            in XrPosef pose,
            out XrSpatialEntityIdEXT anchorEntityId,
            out XrSpatialEntityEXT anchorEntity);

        /// <summary>
        /// Creates an anchor in your app space at the next frame's predicted display time,
        /// allowing you to track the given position and rotation within the given spatial context.
        /// Provided by `XR_EXT_spatial_anchor`.
        /// </summary>
        /// <param name="spatialContext">A spatial context previously created using
        /// `OpenXRNativeApi.xrCreateSpatialContextAsyncEXT`.</param>
        /// <param name="position">The position, in Unity coordinates relative to your XR Origin, at which to create the
        /// anchor.</param>
        /// <param name="rotation">The rotation, in Unity coordinates relative to your XR Origin, to use for the
        /// anchor.</param>
        /// <param name="anchorEntityId">The ID of the created anchor entity.</param>
        /// <param name="anchorEntity">The spatial entity handle of the created anchor entity.</param>
        /// <returns>The result of the operation.\
        /// \
        /// `nativeStatusCode` success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// `nativeStatusCode` failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.LimitReached"/></description></item>
        ///   <item><description><see cref="XrResult.TimeInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.PoseInvalid"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        public static OpenXRResultStatus xrCreateSpatialAnchorEXT(
            XrSpatialContextEXT spatialContext,
            Vector3 position,
            Quaternion rotation,
            out XrSpatialEntityIdEXT anchorEntityId,
            out XrSpatialEntityEXT anchorEntity)
            => xrCreateSpatialAnchorEXT(
                spatialContext, new XrPosef(position, rotation), out anchorEntityId, out anchorEntity);
    }
}
