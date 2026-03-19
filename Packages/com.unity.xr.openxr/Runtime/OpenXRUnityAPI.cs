/// This file contains the public C# API surface for the OpenXR package.

using System;
using System.Runtime.InteropServices;

using UnityEngine.XR.OpenXR.NativeTypes;

namespace UnityEngine.XR.OpenXR.API
{
    using uint32_t = UInt32;
    using UnityXRRenderTextureID = UInt32;

    // Display scripting types are based off of IUnityXRDisplay v10 types.
    // GUID 0x7dee4aab20644831ULL, 0x92dddc65493b46bfULL

    /// <summary>
    /// Format for color texture.
    /// </summary>
    public enum UnityXRRenderTextureFormat
    {
        /// <summary>
        /// R8 G8 B8 A8
        /// </summary>
        kUnityXRRenderTextureFormatRGBA32,

        /// <summary>
        /// B8 G8 R8 A8
        /// </summary>
        kUnityXRRenderTextureFormatBGRA32,

        /// <summary>
        /// R5 G6 B5
        /// </summary>
        kUnityXRRenderTextureFormatRGB565,

        /// <summary>
        /// R16 G16 B16 A16 signed half-float
        /// </summary>
        kUnityXRRenderTextureFormatR16G16B16A16_SFloat,

        /// <summary>
        /// R10 G10 B10 A2 Unorm
        /// </summary>
        kUnityXRRenderTextureFormatRGBA1010102,

        /// <summary>
        /// B10 G10 R10 A2 Unorm
        /// </summary>
        kUnityXRRenderTextureFormatBGRA1010102,

        /// <summary>
        /// R11 G11 B10 unsigned small floating point
        /// </summary>
        kUnityXRRenderTextureFormatR11G11B10_UFloat,

        /// <summary>
        /// Don't create a color texture, instead create a reference to another color texture that's
        /// already been created.  Must fill out UnityTextureData::referenceTextureId.
        /// </summary>
        kUnityXRRenderTextureFormatReference = 64,

        /// <summary>
        /// Don't create a whole new color texture; soft-alias the MSAA attachment, yet still
        /// construct the MSAA-resolved 1x texture as unique. This allows memory sharing for MSAA
        /// when mobile clients need to both autoresolve yet also share the MSAA textures.
        /// </summary>
        kUnityXRRenderTextureFormatSoftReferenceMSAA,

        /// <summary>
        /// No color texture.
        /// </summary>
        kUnityXRRenderTextureFormatNone,
    };

    /// <summary>
    /// Container for different ways of representing texture data.
    /// - If the format (#UnityXRRenderTextureDesc::colorFormat or
    ///   #UnityXRRenderTextureDesc::depthFormat) is a 'Reference' format, referenceTextureId
    ///   must be set.
    /// - If the format is kUnityXRRenderTextureFormatSoftReferenceMSAA, both fields are used.
    ///   Otherwise, nativePtr is used.
    /// </summary>
    public struct UnityXRTextureData
    {
        /// <summary>
        /// @brief Native texture ID if you've allocated it yourself. The texture ID varies by
        /// graphics API. For example:
        /// - GL: texture name that comes from glGenTextures
        /// - DX11: ID3D11Texture2D*
        /// - Vulkan: vkImage*
        ///
        /// You can pass in #kUnityXRRenderTextureIdDontCare and have Unity allocate one for you.
        /// </summary>
        public IntPtr nativePtr;

        /// <summary>
        /// Texture ID to share color / depth with in the case of reference color / depth format.
        /// </summary>
        public UnityXRRenderTextureID referenceTextureId;
    };

    /// <summary>
    /// Precision of depth texture.
    /// </summary>
    public enum UnityXRDepthTextureFormat
    {
        /// <summary>
        /// 24-bit or greater depth texture.  Unity prefers 32 bit floating point Z buffer if available on the platform.
        /// DX11: DXGI_FORMAT_D32_FLOAT_S8X24_UINT
        /// DX12: DXGI_FORMAT_D32_FLOAT_S8X24_UINT
        /// Vulkan: VK_FORMAT_D24_UNORM_S8_UINT
        /// OpenGL: Unsupported
        /// </summary>
        kUnityXRDepthTextureFormat24bitOrGreater,

        /// <summary>
        /// If possible, use a 16-bit texture format to save bandwidth.
        /// DX11: DXGI_FORMAT_D16_UNORM
        /// DX12: DXGI_FORMAT_D16_UNORM
        /// Vulkan: VK_FORMAT_D16_UNORM
        /// OpenGL: Unsupported
        /// </summary>
        kUnityXRDepthTextureFormat16bit,

        /// <summary>
        /// Don't create a depth texture, instead create a reference to another depth texture that's
        /// already been created.  Must fill out UnityTextureData::referenceTextureId. This is
        /// useful for sharing a single depth texture between double/triple buffered color textures
        /// (of the same width/height).
        /// </summary>
        kUnityXRDepthTextureFormatReference,

        /// <summary>
        /// No depth texture.
        /// </summary>
        kUnityXRDepthTextureFormatNone
    };

    /// <summary>
    /// Format for shading rate texture.
    /// </summary>
    public enum UnityXRShadingRateFormat
    {
        /// <summary>
        /// No shading rate texture.
        /// </summary>
        kUnityXRShadingRateFormatNone,

        /// <summary>
        /// R8G8 shading rate texture format.
        /// </summary>
        kUnityXRShadingRateR8G8
    };

    /// <summary>
    /// Flags that can be set on a UnityXRRenderTextureDesc before creation to modify behavior.
    /// </summary>
    public enum UnityXRRenderTextureFlags
    {
        /// <summary>
        /// By default, Unity expects texture coordinates in OpenGL mapping with (0,0) in lower left
        /// hand corner.  This flag specifies that (0,0) is in the upper left hand corner for this
        /// texture.  Unity will flip the texture at the appropriate time.
        /// </summary>
        kUnityXRRenderTextureFlagsUVDirectionTopToBottom = 1 << 0,

        /// <summary>
        /// This texture can be an unresolved MSAA texture.  Accepting unresolved textures lowers
        /// the bandwidth needed by tile-based architectures.
        /// </summary>
        kUnityXRRenderTextureFlagsMultisampleAutoResolve = 1 << 1,

        /// <summary>
        /// Specifies that the resources backing this texture can't be resized.  No control over
        /// width / height of texture. Unity might render to a separate texture of a more convenient
        /// size, then blit into this one. For Example, HoloLens backbuffer size can't be changed.
        /// </summary>
        kUnityXRRenderTextureFlagsLockedWidthHeight = 1 << 2,

        /// <summary>
        /// Texture can only be written to and can't be read from. Unity needs to create
        /// intermediate textures to do post-processing work.
        /// </summary>
        kUnityXRRenderTextureFlagsWriteOnly = 1 << 3,

        /// <summary>
        /// Use sRGB texture formats if possible.
        /// </summary>
        kUnityXRRenderTextureFlagsSRGB = 1 << 4,

        /// <summary>
        /// Opt-in to always discarding depth and resolving MSAA color to improve performance on
        /// tile-based architectures at the expense of rarely-used effects which require depth
        /// resolve or MSAA color store, such as camera stacking. This only affects Vulkan. Note
        /// that this may break user content - use with care and consider giving the developer a way
        /// to turn it off.
        /// </summary>
        kUnityXRRenderTextureFlagsOptimizeBufferDiscards = 1 << 5,

        /// <summary>
        /// Texture is used for storing motion-vector information.
        /// </summary>
        kUnityXRRenderTextureFlagsMotionVectorTexture = 1 << 6,

        /// <summary>
        /// Texture is a "GFR texture", or more generally one which uses foveation offset
        /// </summary>
        kUnityXRRenderTextureFlagsFoveationOffset = 1 << 7,

        /// <summary>
        /// Renderpass for this texture uses the Viewport Rect to define the Render Area
        /// </summary>
        kUnityXRRenderTextureFlagsViewportAsRenderArea = 1 << 8,

        /// <summary>
        /// Texture is used for storing an HDR output surface
        /// </summary>
        kUnityXRRenderTextureFlagsHDR = 1 << 9,
    };

    /// <summary>
    /// Description of a texture that the plugin can request to be allocated via
    /// IUnityXRDisplayInterface::CreateTexture.  Encapsulates both color and depth surfaces.
    /// </summary>
    public struct UnityXRRenderTextureDesc
    {
        /// <summary>
        /// Color format of the texture.  Format is sRGB if kUnityXRRenderTextureFlagsSRGB is set
        /// and there is an equivalent sRGB native format.
        /// </summary>
        public UnityXRRenderTextureFormat colorFormat;

        /// <summary>
        /// Data for color texture.
        /// </summary>
        public UnityXRTextureData color;

        /// <summary>
        /// Depth format of the texture.
        /// </summary>
        public UnityXRDepthTextureFormat depthFormat;

        /// <summary>
        /// Data for depth texture.
        /// </summary>
        public UnityXRTextureData depth;

        /// <summary>
        /// Shading rate texture format.
        /// </summary>
        public UnityXRShadingRateFormat shadingRateFormat;

        /// <summary>
        /// Data for shading rate texture.
        /// </summary>
        public UnityXRTextureData shadingRate;

        /// <summary>
        /// Width of the texture in pixels.
        /// </summary>
        public uint32_t width;

        /// <summary>
        /// Height of the texture in pixels.
        /// </summary>
        public uint32_t height;

        /// <summary>
        /// If requesting a texture array, the length of the texture array.
        /// </summary>
        public uint32_t textureArrayLength;

        /// <summary>
        /// Combination of #UnityXRRenderTextureFlags.
        /// </summary>
        public uint32_t flags;
    };

    /// <summary>
    /// static container for XRDisplay-related scripting functionality.
    /// </summary>
    public static class UnityXRDisplay
    {
        /// <summary>
        /// Unity will allocate the texture if needed. #kUnityXRRenderTextureIdDontCare can be set
        /// on UnityXRRenderTextureDesc.nativeColorTexPtr or
        /// UnityXRRenderTextureDesc.nativeDepthTexPtr.
        /// </summary>
        public const UnityXRRenderTextureID kUnityXRRenderTextureIdDontCare = 0;

        private const string k_UnityOpenXRLib = "UnityOpenXR";

        /// <summary>
        /// Create a UnityXRRenderTextureId given a UnityXRRenderTextureDesc.
        /// </summary>
        /// <param name="desc">Descriptor of the texture to be created.</param>
        /// <param name="id">Returned Texture ID representing a unique instance of a texture.</param>
        /// <returns>
        /// true Successfully initialized
        /// false Error
        /// </returns>
        [DllImport(k_UnityOpenXRLib, EntryPoint = "Display_CreateTexture")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool CreateTexture(UnityXRRenderTextureDesc desc, out uint32_t id);

        /// <summary>
        /// Destroy a texture given a UnityXRRenderTextureId.
        /// </summary>
        /// <param name="textureId"> Texture ID represents the instance of a texture to be destroyed.</param>
        /// <returns>
        /// true Successfully destroyed.
        /// false Error
        /// </returns>
        [DllImport(k_UnityOpenXRLib, EntryPoint = "Display_DestroyTexture")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool DestroyTexture(uint32_t textureId);
    }
} // namespace
