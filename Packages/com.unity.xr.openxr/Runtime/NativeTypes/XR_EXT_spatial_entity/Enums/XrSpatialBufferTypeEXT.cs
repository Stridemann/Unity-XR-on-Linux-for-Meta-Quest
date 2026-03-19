namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Represents the possible types of <see cref="XrSpatialBufferEXT"/>. Provided by `XR_EXT_spatial_entity`.
    /// </summary>
    public enum XrSpatialBufferTypeEXT
    {
        /// <summary>
        /// Unknown buffer type. Equivalent to the OpenXR value `XR_SPATIAL_BUFFER_TYPE_UNKNOWN_EXT`.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// The buffer ID can be passed to `xrGetSpatialBufferStringEXT` to retrieve a string buffer.
        /// Equivalent to the OpenXR value `XR_SPATIAL_BUFFER_TYPE_STRING_EXT`.
        /// </summary>
        /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrGetSpatialBufferStringEXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialBufferGetInfoEXT@,System.UInt32@,System.Byte*@)"/>
        /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrGetSpatialBufferStringEXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialBufferGetInfoEXT@,System.UInt32@,System.String@)"/>
        String = 1,

        /// <summary>
        /// The buffer ID can be passed to `OpenXRNativeApi.xrGetSpatialBufferUint8EXT` to retrieve a `byte` buffer.
        /// Equivalent to the OpenXR value `XR_SPATIAL_BUFFER_TYPE_UINT8_EXT`.
        /// </summary>
        /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrGetSpatialBufferUint8EXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialBufferGetInfoEXT@,System.UInt32,System.UInt32@,System.Byte*)"/>
        /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrGetSpatialBufferUint8EXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialBufferGetInfoEXT@,Unity.Collections.Allocator,Unity.Collections.NativeArray{System.Byte}@)"/>
        Uint8 = 2,

        /// <summary>
        /// The buffer ID can be passed to `OpenXRNativeApi.xrGetSpatialBufferUint16EXT` to retrieve a `ushort` buffer.
        /// Equivalent to the OpenXR value `XR_SPATIAL_BUFFER_TYPE_UINT16_EXT`.
        /// </summary>
        /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrGetSpatialBufferUint16EXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialBufferGetInfoEXT@,System.UInt32,System.UInt32@,System.UInt16*)"/>
        /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrGetSpatialBufferUint16EXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialBufferGetInfoEXT@,Unity.Collections.Allocator,Unity.Collections.NativeArray{System.UInt16}@)"/>
        Uint16 = 3,

        /// <summary>
        /// The buffer ID can be passed to `OpenXRNativeApi.xrGetSpatialBufferUint32EXT` to retrieve a `uint` buffer.
        /// Equivalent to the OpenXR value `XR_SPATIAL_BUFFER_TYPE_UINT32_EXT`.
        /// </summary>
        /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrGetSpatialBufferUint32EXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialBufferGetInfoEXT@,System.UInt32,System.UInt32@,System.UInt32*)"/>
        /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrGetSpatialBufferUint32EXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialBufferGetInfoEXT@,Unity.Collections.Allocator,Unity.Collections.NativeArray{System.UInt32}@)"/>
        Uint32 = 4,

        /// <summary>
        /// The buffer ID can be passed to `OpenXRNativeApi.xrGetSpatialBufferFloatEXT` to retrieve a `float` buffer.
        /// Equivalent to the OpenXR value `XR_SPATIAL_BUFFER_TYPE_FLOAT_EXT`.
        /// </summary>
        /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrGetSpatialBufferFloatEXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialBufferGetInfoEXT@,System.UInt32,System.UInt32@,System.Single*)"/>
        /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrGetSpatialBufferFloatEXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialBufferGetInfoEXT@,Unity.Collections.Allocator,Unity.Collections.NativeArray{System.Single}@)"/>
        Float = 5,

        /// <summary>
        /// The buffer ID can be passed to `OpenXRNativeApi.xrGetSpatialBufferVector2fEXT` to retrieve an `XrVector2f`
        /// buffer. Equivalent to the OpenXR value `XR_SPATIAL_BUFFER_TYPE_VECTOR2F_EXT`.
        /// </summary>
        /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrGetSpatialBufferVector2fEXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialBufferGetInfoEXT@,System.UInt32,System.UInt32@,UnityEngine.XR.OpenXR.NativeTypes.XrVector2f*)"/>
        /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrGetSpatialBufferVector2fEXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialBufferGetInfoEXT@,Unity.Collections.Allocator,Unity.Collections.NativeArray{UnityEngine.XR.OpenXR.NativeTypes.XrVector2f}@)"/>
        Vector2f = 6,

        /// <summary>
        /// The buffer ID can be passed to `OpenXRNativeApi.xrGetSpatialBufferVector3fEXT` to retrieve an `XrVector3f`
        /// buffer. Equivalent to the OpenXR value `XR_SPATIAL_BUFFER_TYPE_VECTOR3F_EXT`.
        /// </summary>
        /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrGetSpatialBufferVector3fEXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialBufferGetInfoEXT@,System.UInt32,System.UInt32@,UnityEngine.XR.OpenXR.NativeTypes.XrVector3f*)"/>
        /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrGetSpatialBufferVector3fEXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialBufferGetInfoEXT@,Unity.Collections.Allocator,Unity.Collections.NativeArray{UnityEngine.XR.OpenXR.NativeTypes.XrVector3f}@)"/>
        Vector3f = 7,
    }
}
