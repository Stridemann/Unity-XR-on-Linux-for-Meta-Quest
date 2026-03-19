namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Creation info for a swapchain.
    /// <a href="https://registry.khronos.org/OpenXR/specs/1.0/man/html/XrSwapchainCreateInfo.html">OpenXR Spec</a>
    /// </summary>
    public unsafe struct XrSwapchainCreateInfo
    {
        /// <summary>
        /// The XrStructureType of this structure.
        /// <a href="https://registry.khronos.org/OpenXR/specs/1.0/man/html/XrStructureType.html">OpenXR Spec</a>
        /// </summary>
        public uint Type;

        /// <summary>
        /// Pointer to the next structure in a structure chain. Can be null.
        /// </summary>
        public void* Next;

        /// <summary>
        /// Bitmask of XrSwapchainCreateFlagBits describing additional properties of the swapchain.
        /// <a href="https://registry.khronos.org/OpenXR/specs/1.0/man/html/XrSwapchainCreateFlagBits.html">OpenXR Spec</a>
        /// </summary>
        public ulong CreateFlags;

        /// <summary>
        /// Bitmask of XrSwapchainUsageFlagBits describing the intended usage of the swapchain's images.
        /// The usage flags define how the corresponding graphics API objects are created.
        /// A mismatch may result in swapchain images that do not support the application's usage.
        /// <a href="https://registry.khronos.org/OpenXR/specs/1.0/man/html/XrSwapchainUsageFlagBits.html">OpenXR Spec</a>
        /// </summary>
        public ulong UsageFlags;

        /// <summary>
        /// The graphics API-specific texture format identifier.
        /// Can use OpenXRLayerUtility.GetDefaultColorFormat() to get the default format.
        /// </summary>
        public long Format;

        /// <summary>
        /// The number of sub-data element samples in the image, must not be 0 or greater than the graphics API's maximum limit.
        /// </summary>
        public uint SampleCount;

        /// <summary>
        /// The width of the image, must not be 0 or greater than the graphics API's maximum limit.
        /// </summary>
        public uint Width;

        /// <summary>
        /// The height of the image, must not be 0 or greater than the graphics API's maximum limit.
        /// </summary>
        public uint Height;

        /// <summary>
        /// The number of faces, which can be either 6 (for cubemaps) or 1.
        /// </summary>
        public uint FaceCount;

        /// <summary>
        /// The number of array layers in the image or 1 for a 2D image, must not be 0 or greater than the graphics API's maximum limit.
        /// </summary>
        public uint ArraySize;

        /// <summary>
        /// Describes the number of levels of detail available for minified sampling of the image, must not be 0 or greater than the graphics API's maximum limit.
        /// </summary>
        public uint MipCount;
    }
}
