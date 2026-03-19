using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine.XR.OpenXR.NativeTypes;
using XrSpatialContextEXT = System.UInt64;
using XrSpatialEntityEXT = System.UInt64;
using XrSpatialEntityIdEXT = System.UInt64;

namespace UnityEditor.XR.OpenXR.Tests.NativeTypes
{
    /// <summary>
    /// Delegate signature for `xrCreateSpatialAnchorEXT`.
    /// Provided by `XR_EXT_spatial_anchor`.
    /// </summary>
    /// <param name="spatialContext">The spatial context.</param>
    /// <param name="createInfo">The create info.</param>
    /// <param name="anchorEntityId">The output anchor entity ID.</param>
    /// <param name="anchorEntity">The output anchor entity.</param>
    /// <returns>The result of the operation.</returns>
    public delegate XrResult xrCreateSpatialAnchorEXT_delegate(
        XrSpatialContextEXT spatialContext,
        in XrSpatialAnchorCreateInfoEXT createInfo,
        out XrSpatialEntityIdEXT anchorEntityId,
        out XrSpatialEntityEXT anchorEntity);

    static class EXTSpatialAnchorMocks
    {
        [MonoPInvokeCallback(typeof(xrCreateSpatialAnchorEXT_delegate))]
        internal static XrResult xrCreateSpatialAnchorEXT(
            XrSpatialContextEXT spatialContext,
            in XrSpatialAnchorCreateInfoEXT createInfo,
            out XrSpatialEntityIdEXT anchorEntityId,
            out XrSpatialEntityEXT anchorEntity)
        {
            anchorEntityId = 123;
            anchorEntity = 456;
            return XrResult.Success;
        }

        internal static IntPtr xrCreateSpatialAnchorEXT_Ptr =
            Marshal.GetFunctionPointerForDelegate(
                (xrCreateSpatialAnchorEXT_delegate)xrCreateSpatialAnchorEXT);
    }
}
