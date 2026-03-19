namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Values for type members of structs.
    /// </summary>
    public enum XrStructureType : uint
    {
        /// <summary>
        /// Unknown struct type.
        /// </summary>
        XR_TYPE_UNKNOWN = Unknown,

        /// <summary>
        /// Unknown struct type. Equivalent to the OpenXR value `XR_TYPE_UNKNOWN`.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Struct is of type `XrApiLayerProperties`. Equivalent to the OpenXR value `XR_TYPE_API_LAYER_PROPERTIES`.
        /// </summary>
        ApiLayerProperties = 1,

        /// <summary>
        /// Struct is of type `XrExtensionProperties`. Equivalent to the OpenXR value `XR_TYPE_EXTENSION_PROPERTIES`.
        /// </summary>
        ExtensionProperties = 2,

        /// <summary>
        /// Struct is of type `XrInstanceCreateInfo`. Equivalent to the OpenXR value `XR_TYPE_INSTANCE_CREATE_INFO`.
        /// </summary>
        InstanceCreateInfo = 3,

        /// <summary>
        /// Struct is of type `XrSystemGetInfo`. Equivalent to the OpenXR value `XR_TYPE_SYSTEM_GET_INFO`.
        /// </summary>
        SystemGetInfo = 4,

        /// <summary>
        /// Struct is of type `XrSystemProperties`. Equivalent to the OpenXR value `XR_TYPE_SYSTEM_PROPERTIES`.
        /// </summary>
        SystemProperties = 5,

        /// <summary>
        /// Struct is of type `XrViewLocateInfo`. Equivalent to the OpenXR value `XR_TYPE_VIEW_LOCATE_INFO`.
        /// </summary>
        ViewLocateInfo = 6,

        /// <summary>
        /// Struct is of type `XrView`. Equivalent to the OpenXR value `XR_TYPE_VIEW`.
        /// </summary>
        XrView = 7,

        /// <summary>
        /// Struct is of type `XrSessionCreateInfo`. Equivalent to the OpenXR value `XR_TYPE_SESSION_CREATE_INFO`.
        /// </summary>
        SessionCreateInfo = 8,

        /// <summary>
        /// Struct is of type <see cref="XrSwapchainCreateInfo"/>.
        /// </summary>
        XR_TYPE_SWAPCHAIN_CREATE_INFO = SwapchainCreateInfo,

        /// <summary>
        /// Struct is of type <see cref="XrSwapchainCreateInfo"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_SWAPCHAIN_CREATE_INFO`.
        /// </summary>
        SwapchainCreateInfo = 9,

        /// <summary>
        /// Struct is of type `XrSessionBeginInfo`. Equivalent to the OpenXR value `XR_TYPE_SESSION_BEGIN_INFO`.
        /// </summary>
        SessionBeginInfo = 10,

        /// <summary>
        /// Struct is of type `XrViewState`. Equivalent to the OpenXR value `XR_TYPE_VIEW_STATE`.
        /// </summary>
        ViewState = 11,

        /// <summary>
        /// Struct is of type `XrFrameEndInfo`. Equivalent to the OpenXR value `XR_TYPE_FRAME_END_INFO`.
        /// </summary>
        FrameEndInfo = 12,

        /// <summary>
        /// Struct is of type `XrHapticVibration`. Equivalent to the OpenXR value `XR_TYPE_HAPTIC_VIBRATION`.
        /// </summary>
        HapticVibration = 13,

        /// <summary>
        /// Struct is of type `XrEventDataBuffer`. Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_BUFFER`.
        /// </summary>
        EventDataBuffer = 16,

        /// <summary>
        /// Struct is of type `XrEventDataInstanceLossPending`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_INSTANCE_LOSS_PENDING`.
        /// </summary>
        EventDataInstanceLossPending = 17,

        /// <summary>
        /// Struct is of type `XrEventDataSessionStateChanged`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_SESSION_STATE_CHANGED`.
        /// </summary>
        EventDataSessionStateChanged = 18,

        /// <summary>
        /// Struct is of type `XrActionStateBoolean`. Equivalent to the OpenXR value `XR_TYPE_ACTION_STATE_BOOLEAN`.
        /// </summary>
        ActionStateBoolean = 23,

        /// <summary>
        /// Struct is of type `XrActionStateFloat`. Equivalent to the OpenXR value `XR_TYPE_ACTION_STATE_FLOAT`.
        /// </summary>
        ActionStateFloat = 24,

        /// <summary>
        /// Struct is of type `XrActionStateVector2f`. Equivalent to the OpenXR value `XR_TYPE_ACTION_STATE_VECTOR2F`.
        /// </summary>
        ActionStateVector2f = 25,

        /// <summary>
        /// Struct is of type `XrActionStatePose`. Equivalent to the OpenXR value `XR_TYPE_ACTION_STATE_POSE`.
        /// </summary>
        ActionStatePose = 27,

        /// <summary>
        /// Struct is of type `XrActionSetCreateInfo`. Equivalent to the OpenXR value `XR_TYPE_ACTION_SET_CREATE_INFO`.
        /// </summary>
        ActionSetCreateInfo = 28,

        /// <summary>
        /// Struct is of type `XrActionCreateInfo`. Equivalent to the OpenXR value `XR_TYPE_ACTION_CREATE_INFO`.
        /// </summary>
        ActionCreateInfo = 29,

        /// <summary>
        /// Struct is of type `XrInstanceProperties`. Equivalent to the OpenXR value `XR_TYPE_INSTANCE_PROPERTIES`.
        /// </summary>
        InstanceProperties = 32,

        /// <summary>
        /// Struct is of type `XrFrameWaitInfo`. Equivalent to the OpenXR value `XR_TYPE_FRAME_WAIT_INFO`.
        /// </summary>
        FrameWaitInfo = 33,

        /// <summary>
        /// Struct is of type <see cref="XrCompositionLayerProjection"/>.
        /// </summary>
        XR_TYPE_COMPOSITION_LAYER_PROJECTION = CompositionLayerProjection,

        /// <summary>
        /// Struct is of type <see cref="XrCompositionLayerProjection"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_COMPOSITION_LAYER_PROJECTION`.
        /// </summary>
        CompositionLayerProjection = 35,

        /// <summary>
        /// Struct is of type <see cref="XrCompositionLayerQuad"/>.
        /// </summary>
        XR_TYPE_COMPOSITION_LAYER_QUAD = 36,

        /// <summary>
        /// Struct is of type <see cref="XrCompositionLayerQuad"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_COMPOSITION_LAYER_QUAD`.
        /// </summary>
        CompositionLayerQuad = 36,

        /// <summary>
        /// Struct is of type `XrReferenceSpaceCreateInfo`.
        /// Equivalent to the OpenXR value `XR_TYPE_REFERENCE_SPACE_CREATE_INFO`.
        /// </summary>
        ReferenceSpaceCreateInfo = 37,

        /// <summary>
        /// Struct is of type `XrActionSpaceCreateInfo`.
        /// Equivalent to the OpenXR value `XR_TYPE_ACTION_SPACE_CREATE_INFO`.
        /// </summary>
        ActionSpaceCreateInfo = 38,

        /// <summary>
        /// Struct is of type `XrEventDataReferenceSpaceChangePending`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_REFERENCE_SPACE_CHANGE_PENDING`.
        /// </summary>
        EventDataReferenceSpaceChangePending = 40,

        /// <summary>
        /// Struct is of type `XrViewConfigurationView`.
        /// Equivalent to the OpenXR value `XR_TYPE_VIEW_CONFIGURATION_VIEW`.
        /// </summary>
        ViewConfigurationView = 41,

        /// <summary>
        /// Struct is of type `XrSpaceLocation`. Equivalent to the OpenXR value `XR_TYPE_SPACE_LOCATION`.
        /// </summary>
        SpaceLocation = 42,

        /// <summary>
        /// Struct is of type `XrSpaceVelocity`. Equivalent to the OpenXR value `XR_TYPE_SPACE_VELOCITY`.
        /// </summary>
        SpaceVelocity = 43,

        /// <summary>
        /// Struct is of type `XrFrameState`. Equivalent to the OpenXR value `XR_TYPE_FRAME_STATE`.
        /// </summary>
        FrameState = 44,

        /// <summary>
        /// Struct is of type `XrViewConfigurationProperties`.
        /// Equivalent to the OpenXR value `XR_TYPE_VIEW_CONFIGURATION_PROPERTIES`.
        /// </summary>
        ViewConfigurationProperties = 45,

        /// <summary>
        /// Struct is of type `XrFrameBeginInfo`. Equivalent to the OpenXR value `XR_TYPE_FRAME_BEGIN_INFO`.
        /// </summary>
        FrameBeginInfo = 46,

        /// <summary>
        /// Struct is of type <see cref="XrCompositionLayerProjectionView"/>.
        /// </summary>
        XR_TYPE_COMPOSITION_LAYER_PROJECTION_VIEW = CompositionLayerProjectionView,

        /// <summary>
        /// Struct is of type <see cref="XrCompositionLayerProjectionView"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_COMPOSITION_LAYER_PROJECTION_VIEW`.
        /// </summary>
        CompositionLayerProjectionView = 48,

        /// <summary>
        /// Struct is of type `XrEventDataEventsLost`. Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_EVENTS_LOST`.
        /// </summary>
        EventDataEventsLost = 49,

        /// <summary>
        /// Struct is of type `XrInteractionProfileSuggestedBinding`.
        /// Equivalent to the OpenXR value `XR_TYPE_INTERACTION_PROFILE_SUGGESTED_BINDING`.
        /// </summary>
        InteractionProfileSuggestedBinding = 51,

        /// <summary>
        /// Struct is of type `XrEventDataInteractionProfileChanged`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_INTERACTION_PROFILE_CHANGED`.
        /// </summary>
        EventDataInteractionProfileChanged = 52,

        /// <summary>
        /// Struct is of type `XrInteractionProfileState`.
        /// Equivalent to the OpenXR value `XR_TYPE_INTERACTION_PROFILE_STATE`.
        /// </summary>
        InteractionProfileState = 53,

        /// <summary>
        /// Struct is of type `XrSwapchainImageAcquireInfo`.
        /// Equivalent to the OpenXR value `XR_TYPE_SWAPCHAIN_IMAGE_ACQUIRE_INFO`.
        /// </summary>
        SwapchainImageAcquireInfo = 55,

        /// <summary>
        /// Struct is of type `XrSwapchainImageWaitInfo`.
        /// Equivalent to the OpenXR value `XR_TYPE_SWAPCHAIN_IMAGE_WAIT_INFO`.
        /// </summary>
        SwapchainImageWaitInfo = 56,

        /// <summary>
        /// Struct is of type `XrSwapchainImageReleaseInfo`.
        /// Equivalent to the OpenXR value `XR_TYPE_SWAPCHAIN_IMAGE_RELEASE_INFO`.
        /// </summary>
        SwapchainImageReleaseInfo = 57,

        /// <summary>
        /// Struct is of type `XrActionStateGetInfo`. Equivalent to the OpenXR value `XR_TYPE_ACTION_STATE_GET_INFO`.
        /// </summary>
        ActionStateGetInfo = 58,

        /// <summary>
        /// Struct is of type `XrHapticActionInfo`. Equivalent to the OpenXR value `XR_TYPE_HAPTIC_ACTION_INFO`.
        /// </summary>
        HapticActionInfo = 59,

        /// <summary>
        /// Struct is of type `XrSessionActionSetsAttachInfo`.
        /// Equivalent to the OpenXR value `XR_TYPE_SESSION_ACTION_SETS_ATTACH_INFO`.
        /// </summary>
        SessionActionSetsAttachInfo = 60,

        /// <summary>
        /// Struct is of type `XrActionsSyncInfo`. Equivalent to the OpenXR value `XR_TYPE_ACTION_SYNC_INFO`.
        /// </summary>
        ActionsSyncInfo = 61,

        /// <summary>
        /// Struct is of type `XrBoundSourcesForActionEnumerateInfo`.
        /// Equivalent to the OpenXR value `XR_TYPE_BOUND_SOURCES_FOR_ACTION_ENUMERATE_INFO`.
        /// </summary>
        BoundSourcesForActionEnumerateInfo = 62,

        /// <summary>
        /// Struct is of type `XrInputSourceLocalizedNameGetInfo`.
        /// Equivalent to the OpenXR value `XR_TYPE_INPUT_SOURCE_LOCALIZED_NAME_GET_INFO`.
        /// </summary>
        InputSourceLocalizedNameGetInfo = 63,

        /// <summary>
        /// Struct is of type `XrSpacesLocateInfo`. Equivalent to the OpenXR value `XR_TYPE_SPACES_LOCATE_INFO`.
        /// Provided by `XR_VERSION_1_1`.
        /// </summary>
        SpacesLocateInfo = 1000471000,

        /// <summary>
        /// Struct is of type `XrSpaceLocations`. Equivalent to the OpenXR value `XR_TYPE_SPACE_LOCATIONS`.
        /// Provided by `XR_VERSION_1_1`.
        /// </summary>
        SpaceLocations = 1000471001,

        /// <summary>
        /// Struct is of type `XrSpaceVelocities`. Equivalent to the OpenXR value `XR_TYPE_SPACE_VELOCITIES`.
        /// Provided by `XR_VERSION_1_1`.
        /// </summary>
        SpaceVelocities = 1000471002,

        /// <summary>
        /// Struct is of type <see cref="XrCompositionLayerCubeKHR"/>. Provided by `XR_KHR_composition_layer_cube`.
        /// </summary>
        XR_TYPE_COMPOSITION_LAYER_CUBE_KHR = CompositionLayerCubeKHR,

        /// <summary>
        /// Struct is of type <see cref="XrCompositionLayerCubeKHR"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_COMPOSITION_LAYER_CUBE_KHR`.
        /// Provided by `XR_KHR_composition_layer_cube`.
        /// </summary>
        CompositionLayerCubeKHR = 1000006000,

        /// <summary>
        /// Struct is of type `XrInstanceCreateInfoAndroidKHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_INSTANCE_CREATE_INFO_ANDROID_KHR`.
        /// Provided by `XR_KHR_android_create_instance`.
        /// </summary>
        InstanceCreateInfoAndroidKHR = 1000008000,

        /// <summary>
        /// Struct is of type `XrCompositionLayerDepthInfoKHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_COMPOSITION_LAYER_DEPTH_INFO_KHR`.
        /// Provided by `XR_KHR_composition_layer_depth`.
        /// </summary>
        CompositionLayerDepthInfoKHR = 1000010000,

        /// <summary>
        /// Struct is of type `XrVulkanSwapchainFormatListCreateInfoKHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_VULKAN_SWAPCHAIN_FORMAT_LIST_CREATE_INFO_KHR`.
        /// Provided by `XR_KHR_vulkan_swapchain_format_list`.
        /// </summary>
        VulkanSwapchainFormatListCreateInfoKHR = 1000014000,

        /// <summary>
        /// Struct is of type `XrEventDataPerfSettingsEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_PERF_SETTINGS_EXT`.
        /// Provided by `XR_EXT_performance_settings`.
        /// </summary>
        EventDataPerfSettingsEXT = 1000015000,

        /// <summary>
        /// Struct is of type <see cref="XrCompositionLayerCylinderKHR"/>.
        /// Provided by `XR_KHR_composition_layer_cylinder`.
        /// </summary>
        XR_TYPE_COMPOSITION_LAYER_CYLINDER_KHR = CompositionLayerCylinderKHR,

        /// <summary>
        /// Struct is of type <see cref="XrCompositionLayerCylinderKHR"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_COMPOSITION_LAYER_CYLINDER_KHR`.
        /// Provided by `XR_KHR_composition_layer_cylinder`.
        /// </summary>
        CompositionLayerCylinderKHR = 1000017000,

        /// <summary>
        /// Struct is of type <see cref="XrCompositionLayerEquirectKHR"/>.
        /// Provided by `XR_KHR_composition_layer_equirect`.
        /// </summary>
        XR_TYPE_COMPOSITION_LAYER_EQUIRECT_KHR = CompositionLayerEquirectKHR,

        /// <summary>
        /// Struct is of type <see cref="XrCompositionLayerEquirectKHR"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_COMPOSITION_LAYER_EQUIRECT_KHR`.
        /// Provided by `XR_KHR_composition_layer_equirect`.
        /// </summary>
        CompositionLayerEquirectKHR = 1000018000,

        /// <summary>
        /// Struct is of type `XrDebugUtilsObjectNameInfoEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_DEBUG_UTILS_OBJECT_NAME_INFO_EXT`.
        /// Provided by `XR_EXT_debug_utils`.
        /// </summary>
        DebugUtilsObjectNameInfoEXT = 1000019000,

        /// <summary>
        /// Struct is of type `XrDebugUtilsMessengerCallbackDataEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_DEBUG_UTILS_MESSENGER_CALLBACK_DATA_EXT`.
        /// Provided by `XR_EXT_debug_utils`.
        /// </summary>
        DebugUtilsMessengerCallbackDataEXT = 1000019001,

        /// <summary>
        /// Struct is of type `XrDebugUtilsMessengerCreateInfoEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_DEBUG_UTILS_MESSENGER_CREATE_INFO_EXT`.
        /// Provided by `XR_EXT_debug_utils`.
        /// </summary>
        DebugUtilsMessengerCreateInfoEXT = 1000019002,

        /// <summary>
        /// Struct is of type `XrDebugUtilsLabelEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_DEBUG_UTILS_LABEL_EXT`.
        /// Provided by `XR_EXT_debug_utils`.
        /// </summary>
        DebugUtilsLabelEXT = 1000019003,

        /// <summary>
        /// Struct is of type `XrGraphicsBindingOpenGLWin32KHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_GRAPHICS_BINDING_OPENGL_WIN32_KHR`.
        /// Provided by `XR_KHR_opengl_enable`.
        /// </summary>
        GraphicsBindingOpenGLWin32KHR = 1000023000,

        /// <summary>
        /// Struct is of type `XrGraphicsBindingOpenGLXlibKHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_GRAPHICS_BINDING_OPENGL_XLIB_KHR`.
        /// Provided by `XR_KHR_opengl_enable`.
        /// </summary>
        GraphicsBindingOpenGLXlibKHR = 1000023001,

        /// <summary>
        /// Struct is of type `XrGraphicsBindingOpenGLXcbKHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_GRAPHICS_BINDING_OPENGL_XCB_KHR`.
        /// Provided by `XR_KHR_opengl_enable`.
        /// </summary>
        GraphicsBindingOpenGLXcbKHR = 1000023002,

        /// <summary>
        /// Struct is of type `XrGraphicsBindingOpenGLWaylandKHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_GRAPHICS_BINDING_OPENGL_WAYLAND_KHR`.
        /// Provided by `XR_KHR_opengl_enable`.
        /// </summary>
        GraphicsBindingOpenGLWaylandKHR = 1000023003,

        /// <summary>
        /// Struct is of type `XrSwapchainImageOpenGLKHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_SWAPCHAIN_IMAGE_OPENGL_KHR`.
        /// Provided by `XR_KHR_opengl_enable`.
        /// </summary>
        SwapchainImageOpenGLKHR = 1000023004,

        /// <summary>
        /// Struct is of type `XrGraphicsRequirementsOpenGLKHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_GRAPHICS_REQUIREMENTS_OPENGL_KHR`.
        /// Provided by `XR_KHR_opengl_enable`.
        /// </summary>
        GraphicsRequirementsOpenGLKHR = 1000023005,

        /// <summary>
        /// Struct is of type `XrGraphicsBindingOpenGLESAndroidKHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_GRAPHICS_BINDING_OPENGL_ES_ANDROID_KHR`.
        /// Provided by `XR_KHR_opengl_es_enable`.
        /// </summary>
        GraphicsBindingOpenGLESAndroidKHR = 1000024001,

        /// <summary>
        /// Struct is of type `XrSwapchainImageOpenGLESKHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_SWAPCHAIN_IMAGE_OPENGL_ES_KHR`.
        /// Provided by `XR_KHR_opengl_es_enable`.
        /// </summary>
        SwapchainImageOpenGLESKHR = 1000024002,

        /// <summary>
        /// Struct is of type `XrGraphicsRequirementsOpenGLESKHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_GRAPHICS_REQUIREMENTS_OPENGL_ES_KHR`.
        /// Provided by `XR_KHR_opengl_es_enable`.
        /// </summary>
        GraphicsRequirementsOpenGLESKHR = 1000024003,

        /// <summary>
        /// Struct is of type `XrGraphicsBindingVulkanKHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_GRAPHICS_BINDING_VULKAN_KHR`.
        /// Provided by `XR_KHR_vulkan_enable`.
        /// </summary>
        GraphicsBindingVulkanKHR = 1000025000,

        /// <summary>
        /// Struct is of type `XrSwapchainImageVulkanKHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_SWAPCHAIN_IMAGE_VULKAN_KHR`.
        /// Provided by `XR_KHR_vulkan_enable`.
        /// </summary>
        SwapchainImageVulkanKHR = 1000025001,

        /// <summary>
        /// Struct is of type `XrGraphicsRequirementsVulkanKHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_GRAPHICS_REQUIREMENTS_VULKAN_KHR`.
        /// Provided by `XR_KHR_vulkan_enable`.
        /// </summary>
        GraphicsRequirementsVulkanKHR = 1000025002,

        /// <summary>
        /// Struct is of type `XrGraphicsBindingD3D11KHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_GRAPHICS_BINDING_D3D11_KHR`.
        /// Provided by `XR_KHR_D3D11_enable`.
        /// </summary>
        GraphicsBindingD3D11KHR = 1000027000,

        /// <summary>
        /// Struct is of type `XrSwapchainImageD3D11KHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_SWAPCHAIN_IMAGE_D3D11_KHR`.
        /// Provided by `XR_KHR_D3D11_enable`.
        /// </summary>
        SwapchainImageD3D11KHR = 1000027001,

        /// <summary>
        /// Struct is of type `XrGraphicsRequirementsD3D11KHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_GRAPHICS_REQUIREMENTS_D3D11_KHR`.
        /// Provided by `XR_KHR_D3D11_enable`.
        /// </summary>
        GraphicsRequirementsD3D11KHR = 1000027002,

        /// <summary>
        /// Struct is of type `XrGraphicsBindingD3D12KHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_GRAPHICS_BINDING_D3D12_KHR`.
        /// Provided by `XR_KHR_D3D12_enable`.
        /// </summary>
        GraphicsBindingD3D12KHR = 1000028000,

        /// <summary>
        /// Struct is of type `XrSwapchainImageD3D12KHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_SWAPCHAIN_IMAGE_D3D12_KHR`.
        /// Provided by `XR_KHR_D3D12_enable`.
        /// </summary>
        SwapchainImageD3D12KHR = 1000028001,

        /// <summary>
        /// Struct is of type `XrGraphicsRequirementsD3D12KHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_GRAPHICS_REQUIREMENTS_D3D12_KHR`.
        /// Provided by `XR_KHR_D3D12_enable`.
        /// </summary>
        GraphicsRequirementsD3D12KHR = 1000028002,

        /// <summary>
        /// Struct is of type `XrGraphicsBindingMetalKHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_GRAPHICS_BINDINGS_METAL_KHR`.
        /// Provided by `XR_KHR_metal_enable`.
        /// </summary>
        GraphicsBindingMetalKHR = 1000029000,

        /// <summary>
        /// Struct is of type `XrSwapchainImageMetalKHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_SWAPCHAIN_IMAGE_METAL_KHR`.
        /// Provided by `XR_KHR_metal_enable`.
        /// </summary>
        SwapchainImageMetalKHR = 1000029001,

        /// <summary>
        /// Struct is of type `XrGraphicsRequirementsMetalKHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_GRAPHICS_REQUIREMENTS_METAL_KHR`.
        /// Provided by `XR_KHR_metal_enable`.
        /// </summary>
        GraphicsRequirementsMetalKHR = 1000029002,

        /// <summary>
        /// Struct is of type `XrSystemEyeGazeInteractionPropertiesEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_EYE_GAZE_INTERACTION_PROPERTIES_EXT`.
        /// Provided by `XR_EXT_eye_gaze_interaction`.
        /// </summary>
        SystemEyeGazeInteractionPropertiesEXT = 1000030000,

        /// <summary>
        /// Struct is of type `XrEyeGazeSampleTimeEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_EYE_GAZE_SAMPLE_TIME_EXT`.
        /// Provided by `XR_EXT_eye_gaze_interaction`.
        /// </summary>
        EyeGazeSampleTimeEXT = 1000030001,

        /// <summary>
        /// Struct is of type `XrVisibilityMaskKHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_VISIBILITY_MASK_KHR`.
        /// Provided by `XR_KHR_visibility_mask`.
        /// </summary>
        VisibilityMaskKHR = 1000031000,

        /// <summary>
        /// Struct is of type `XrEventDataVisibilityMaskChangedKHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_VISIBILITY_MASK_CHANGED_KHR`.
        /// Provided by `XR_KHR_visibility_mask`.
        /// </summary>
        EventDataVisibilityMaskChangedKHR = 1000031001,

        /// <summary>
        /// Struct is of type `XrSessionCreateInfoOverlayEXTX`.
        /// Equivalent to the OpenXR value `XR_TYPE_SESSION_CREATE_INFO_OVERLAY_EXTX`.
        /// Provided by `XR_EXTX_overlay`.
        /// </summary>
        SessionCreateInfoOverlayEXTX = 1000033000,

        /// <summary>
        /// Struct is of type `XrEventDataMainSessionVisibilityChangedEXTX`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_MAIN_SESSION_VISIBILITY_CHANGED_EXTX`.
        /// Provided by `XR_EXTX_overlay`.
        /// </summary>
        EventDataMainSessionVisibilityChangedEXTX = 1000033003,

        /// <summary>
        /// Struct is of type `XrCompositionLayerColorScaleBiasKHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_COMPOSITION_LAYER_COLOR_SCALE_BIAS_KHR`.
        /// Provided by `XR_KHR_composition_layer_color_scale_bias`.
        /// </summary>
        CompositionLayerColorScaleBiasKHR = 1000034000,

        /// <summary>
        /// Struct is of type `XrSpatialAnchorCreateInfoMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ANCHOR_CREATE_INFO_MSFT`.
        /// Provided by `XR_MSFT_spatial_anchor`.
        /// </summary>
        SpatialAnchorCreateInfoMSFT = 1000039000,

        /// <summary>
        /// Struct is of type `XrSpatialAnchorSpaceCreateInfoMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ANCHOR_SPACE_CREATE_INFO_MSFT`.
        /// Provided by `XR_MSFT_spatial_anchor`.
        /// </summary>
        SpatialAnchorSpaceCreateInfoMSFT = 1000039001,

        /// <summary>
        /// Struct is of type `XrCompositionLayerImageLayoutFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_COMPOSITION_LAYER_IMAGE_LAYOUT_FB`.
        /// Provided by `XR_FB_composition_layer_image_layout`.
        /// </summary>
        CompositionLayerImageLayoutFB = 1000040000,

        /// <summary>
        /// Struct is of type `XrCompositionLayerAlphaBlendFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_COMPOSITION_LAYER_ALPHA_BLEND_FB`.
        /// Provided by `XR_FB_composition_layer_alpha_blend`.
        /// </summary>
        CompositionLayerAlphaBlendFB = 1000041001,

        /// <summary>
        /// Struct is of type `XrViewConfigurationDepthRangeEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_VIEW_CONFIGURATION_DEPTH_RANGE_EXT`.
        /// Provided by `XR_EXT_view_configuration_depth_range`.
        /// </summary>
        ViewConfigurationDepthRangeEXT = 1000046000,

        /// <summary>
        /// Struct is of type `XrGraphicsBindingEGLMNDX`.
        /// Equivalent to the OpenXR value `XR_TYPE_GRAPHICS_BINDING_EGL_MNDX`.
        /// Provided by `XR_MNDX_egl_enable`.
        /// </summary>
        GraphicsBindingEGLMNDX = 1000048004,

        /// <summary>
        /// Struct is of type `XrSpatialGraphNodeSpaceCreateInfoMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_GRAPH_NODE_SPACE_CREATE_INFO_MSFT`.
        /// Provided by `XR_MSFT_spatial_graph_bridge`.
        /// </summary>
        SpatialGraphNodeSpaceCreateInfoMSFT = 1000049000,

        /// <summary>
        /// Struct is of type `XrSpatialGraphStaticNodeBindingCreateInfoMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_GRAPH_STATIC_NODE_BINDING_CREATE_INFO_MSFT`.
        /// Provided by `XR_MSFT_spatial_graph_bridge`.
        /// </summary>
        SpatialGraphStaticNodeBindingCreateInfoMSFT = 1000049001,

        /// <summary>
        /// Struct is of type `XrSpatialGraphNodeBindingPropertiesGetInfoMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_GRAPH_NODE_BINDING_PROPERTIES_GET_INFO_MSFT`.
        /// Provided by `XR_MSFT_spatial_graph_bridge`.
        /// </summary>
        SpatialGraphNodeBindingPropertiesGetInfoMSFT = 1000049002,

        /// <summary>
        /// Struct is of type `XrSpatialGraphNodeBindingPropertiesMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_GRAPH_NODE_BINDING_PROPERTIES_MSFT`.
        /// Provided by `XR_MSFT_spatial_graph_bridge`.
        /// </summary>
        SpatialGraphNodeBindingPropertiesMSFT = 1000049003,

        /// <summary>
        /// Struct is of type `XrSystemHandTrackingPropertiesEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_HAND_TRACKING_PROPERTIES_EXT`.
        /// Provided by `XR_EXT_hand_tracking`.
        /// </summary>
        SystemHandTrackingPropertiesEXT = 1000051000,

        /// <summary>
        /// Struct is of type `XrHandTrackerCreateInfoEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_HAND_TRACKER_CREATE_INFO_EXT`.
        /// Provided by `XR_EXT_hand_tracking`.
        /// </summary>
        HandTrackerCreateInfoEXT = 1000051001,

        /// <summary>
        /// Struct is of type `XrHandJointsLocateInfoEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_HAND_JOINTS_LOCATE_INFO_EXT`.
        /// Provided by `XR_EXT_hand_tracking`.
        /// </summary>
        HandJointsLocateInfoEXT = 1000051002,

        /// <summary>
        /// Struct is of type `XrHandJointLocationsEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_HAND_JOINT_LOCATIONS_EXT`.
        /// Provided by `XR_EXT_hand_tracking`.
        /// </summary>
        HandJointLocationsEXT = 1000051003,

        /// <summary>
        /// Struct is of type `XrHandJointVelocitiesEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_HAND_JOINT_VELOCITIES_EXT`.
        /// Provided by `XR_EXT_hand_tracking`.
        /// </summary>
        HandJointVelocitiesEXT = 1000051004,

        /// <summary>
        /// Struct is of type `XrSystemHandTrackingMeshPropertiesMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_HAND_TRACKING_MESH_PROPERTIES_MSFT`.
        /// Provided by `XR_MSFT_hand_tracking_mesh`.
        /// </summary>
        SystemHandTrackingMeshPropertiesMSFT = 1000052000,

        /// <summary>
        /// Struct is of type `XrHandMeshSpaceCreateInfoMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_HAND_MESH_SPACE_CREATE_INFO_MSFT`.
        /// Provided by `XR_MSFT_hand_tracking_mesh`.
        /// </summary>
        HandMeshSpaceCreateInfoMSFT = 1000052001,

        /// <summary>
        /// Struct is of type `XrHandMeshUpdateInfoMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_HAND_MESH_UPDATE_INFO_MSFT`.
        /// Provided by `XR_MSFT_hand_tracking_mesh`.
        /// </summary>
        HandMeshUpdateInfoMSFT = 1000052002,

        /// <summary>
        /// Struct is of type `XrHandMeshMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_HAND_MESH_MSFT`.
        /// Provided by `XR_MSFT_hand_tracking_mesh`.
        /// </summary>
        HandMeshMSFT = 1000052003,

        /// <summary>
        /// Struct is of type `XrHandPoseTypeInfoMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_HAND_POSE_TYPE_INFO_MSFT`.
        /// Provided by `XR_MSFT_hand_tracking_mesh`.
        /// </summary>
        HandPoseTypeInfoMSFT = 1000052004,

        /// <summary>
        /// Struct is of type `XrSecondaryViewConfigurationSessionBeginInfoMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SECONDARY_VIEW_CONFIGURATION_SESSION_BEGIN_INFO_MSFT`.
        /// Provided by `XR_MSFT_secondary_view_configuration`.
        /// </summary>
        SecondaryViewConfigurationSessionBeginInfoMSFT = 1000053000,

        /// <summary>
        /// Struct is of type `XrSecondaryViewConfigurationStateMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SECONDARY_VIEW_CONFIGURATION_STATE_MSFT`.
        /// Provided by `XR_MSFT_secondary_view_configuration`.
        /// </summary>
        SecondaryViewConfigurationStateMSFT = 1000053001,

        /// <summary>
        /// Struct is of type `XrSecondaryViewConfigurationFrameStateMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SECONDARY_VIEW_CONFIGURATION_FRAME_STATE_MSFT`.
        /// Provided by `XR_MSFT_secondary_view_configuration`.
        /// </summary>
        SecondaryViewConfigurationFrameStateMSFT = 1000053002,

        /// <summary>
        /// Struct is of type `XrSecondaryViewConfigurationFrameEndInfoMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SECONDARY_VIEW_CONFIGURATION_FRAME_END_INFO_MSFT`.
        /// Provided by `XR_MSFT_secondary_view_configuration`.
        /// </summary>
        SecondaryViewConfigurationFrameEndInfoMSFT = 1000053003,

        /// <summary>
        /// Struct is of type `XrSecondaryViewConfigurationLayerInfoMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SECONDARY_VIEW_CONFIGURATION_LAYER_INFO_MSFT`.
        /// Provided by `XR_MSFT_secondary_view_configuration`.
        /// </summary>
        SecondaryViewConfigurationLayerInfoMSFT = 1000053004,

        /// <summary>
        /// Struct is of type `XrSecondaryViewConfigurationSwapchainCreateInfoMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SECONDARY_VIEW_CONFIGURATION_SWAPCHAIN_CREATE_INFO_MSFT`.
        /// Provided by `XR_MSFT_secondary_view_configuration`.
        /// </summary>
        SecondaryViewConfigurationSwapchainCreateInfoMSFT = 1000053005,

        /// <summary>
        /// Struct is of type `XrControllerModelKeyStateMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_CONTROLLER_MODEL_KEY_STATE_MSFT`.
        /// Provided by `XR_MSFT_controller_model`.
        /// </summary>
        ControllerModelKeyStateMSFT = 1000055000,

        /// <summary>
        /// Struct is of type `XrControllerModelPropertiesMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_CONTROLLER_MODEL_NODE_PROPERTIES_MSFT`.
        /// Provided by `XR_MSFT_controller_model`.
        /// </summary>
        ControllerModelNodePropertiesMSFT = 1000055001,

        /// <summary>
        /// Struct is of type `XrControllerModelPropertiesMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_CONTROLLER_MODEL_PROPERTIES_MSFT`.
        /// Provided by `XR_MSFT_controller_model`.
        /// </summary>
        ControllerModelPropertiesMSFT = 1000055002,

        /// <summary>
        /// Struct is of type `XrControllerModelNodeStateMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_CONTROLLER_MODEL_NODE_STATE_MSFT`.
        /// Provided by `XR_MSFT_controller_model`.
        /// </summary>
        ControllerModelNodeStateMSFT = 1000055003,

        /// <summary>
        /// Struct is of type `XrControllerModelStateMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_CONTROLLER_MODEL_STATE_MSFT`.
        /// Provided by `XR_MSFT_controller_model`.
        /// </summary>
        ControllerModelStateMSFT = 1000055004,

        /// <summary>
        /// Struct is of type `XrViewConfigurationViewFovEPIC`.
        /// Equivalent to the OpenXR value `XR_TYPE_VIEW_CONFIGURATION_VIEW_FOV_EPIC`.
        /// Provided by `XR_EPIC_view_configuration_fov`.
        /// </summary>
        ViewConfigurationViewFovEPIC = 1000059000,

        /// <summary>
        /// Struct is of type `XrHolographicWindowAttachmentMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_HOLOGRAPHIC_WINDOW_ATTACHMENT_MSFT`.
        /// Provided by `XR_MSFT_holographic_window_attachment`.
        /// </summary>
        HolographicWindowAttachmentMSFT = 1000063000,

        /// <summary>
        /// Struct is of type `XrCompositionLayerReprojectionInfoMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_COMPOSITION_LAYER_REPROJECTION_INFO_MSFT`.
        /// Provided by `XR_MSFT_composition_layer_reprojection`.
        /// </summary>
        CompositionLayerReprojectionInfoMSFT = 1000066000,

        /// <summary>
        /// Struct is of type `XrCompositionLayerReprojectionPlaneOverrideMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_COMPOSITION_LAYER_REPROJECTION_PLANE_OVERRIDE_MSFT`.
        /// Provided by `XR_MSFT_composition_layer_reprojection`.
        /// </summary>
        CompositionLayerReprojectionPlaneOverrideMSFT = 1000066001,

        /// <summary>
        /// Struct is of type `XrAndroidSurfaceSwapchainCreateInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_ANDROID_SURFACE_SWAPCHAIN_CREATE_INFO_FB`.
        /// Provided by `XR_FB_android_surface_swapchain_create`.
        /// </summary>
        AndroidSurfaceSwapchainCreateInfoFB = 1000070000,

        /// <summary>
        /// Struct is of type `XrCompositionLayerSecureContentFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_COMPOSITION_LAYER_SECURE_CONTENT_FB`.
        /// Provided by `XR_FB_composition_layer_secure_content`.
        /// </summary>
        CompositionLayerSecureContentFB = 1000072000,

        /// <summary>
        /// Struct is of type `XrBodyTrackerCreateInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_BODY_TRACKER_CREATE_INFO_FB`.
        /// Provided by `XR_FB_body_tracking`.
        /// </summary>
        BodyTrackerCreateInfoFB = 1000076001,

        /// <summary>
        /// Struct is of type `XrBodyJointsLocateInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_BODY_JOINTS_LOCATE_INFO_FB`.
        /// Provided by `XR_FB_body_tracking`.
        /// </summary>
        BodyJointsLocateInfoFB = 1000076002,

        /// <summary>
        /// Struct is of type `XrSystemBodyTrackingPropertiesFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_BODY_TRACKING_PROPERTIES_FB`.
        /// Provided by `XR_FB_body_tracking`.
        /// </summary>
        SystemBodyTrackingPropertiesFB = 1000076004,

        /// <summary>
        /// Struct is of type `XrBodyJointLocationsFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_BODY_JOINT_LOCATIONS_FB`.
        /// Provided by `XR_FB_body_tracking`.
        /// </summary>
        BodyJointLocationsFB = 1000076005,

        /// <summary>
        /// Struct is of type `XrBodySkeletonFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_BODY_SKELETON_FB`.
        /// Provided by `XR_FB_body_tracking`.
        /// </summary>
        BodySkeletonFB = 1000076006,

        /// <summary>
        /// Struct is of type `XrInteractionProfileDpadBindingEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_INTERACTION_PROFILE_DPAD_BINDING_EXT`.
        /// Provided by `XR_EXT_dpad_binding`.
        /// </summary>
        InteractionProfileDpadBindingEXT = 1000078000,

        /// <summary>
        /// Struct is of type `XrInteractionProfileAnalogThresholdVALVE`.
        /// Equivalent to the OpenXR value `XR_TYPE_INTERACTION_PROFILE_ANALOG_THRESHOLD_VALVE`.
        /// Provided by `XR_VALVE_analog_threshold`.
        /// </summary>
        InteractionProfileAnalogThresholdVALVE = 1000079000,

        /// <summary>
        /// Struct is of type `XrHandJointsMotionRangeInfoEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_HAND_JOINTS_MOTION_RANGE_INFO_EXT`.
        /// Provided by `XR_EXT_hand_joints_motion_range`.
        /// </summary>
        HandJointsMotionRangeInfoEXT = 1000080000,

        /// <summary>
        /// Struct is of type `XrLoaderInitInfoAndroidKHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_LOADER_INIT_INFO_ANDROID_KHR`.
        /// Provided by `XR_KHR_loader_init_android`.
        /// </summary>
        LoaderInitInfoAndroidKHR = 1000089000,

        /// <summary>
        /// Struct is of type `XrVulkanInstanceCreateInfoKHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_VULKAN_INSTANCE_CREATE_INFO_KHR`.
        /// Provided by `XR_KHR_vulkan_enable2`.
        /// </summary>
        VulkanInstanceCreateInfoKHR = 1000090000,

        /// <summary>
        /// Struct is of type `XrVulkanDeviceCreateInfoKHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_VULKAN_DEVICE_CREATE_INFO_KHR`.
        /// Provided by `XR_KHR_vulkan_enable2`.
        /// </summary>
        VulkanDeviceCreateInfoKHR = 1000090001,

        /// <summary>
        /// Struct is of type `XrVulkanGraphicsDeviceGetInfoKHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_VULKAN_GRAPHICS_DEVICE_GET_INFO_KHR`.
        /// Provided by `XR_KHR_vulkan_enable2`.
        /// </summary>
        VulkanGraphicsDeviceGetInfoKHR = 1000090003,

        /// <summary>
        /// Struct is of type <see cref="XrCompositionLayerEquirect2KHR"/>.
        /// Provided by `XR_KHR_composition_layer_equirect2`.
        /// </summary>
        XR_TYPE_COMPOSITION_LAYER_EQUIRECT2_KHR = CompositionLayerEquirect2KHR,

        /// <summary>
        /// Struct is of type <see cref="XrCompositionLayerEquirect2KHR"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_COMPOSITION_LAYER_EQUIRECT2_KHR`.
        /// Provided by `XR_KHR_composition_layer_equirect2`.
        /// </summary>
        CompositionLayerEquirect2KHR = 1000091000,

        /// <summary>
        /// Struct is of type `XrSceneObserverCreateInfoMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SCENE_OBSERVER_CREATE_INFO_MSFT`.
        /// Provided by `XR_MSFT_scene_understanding`.
        /// </summary>
        SceneObserverCreateInfoMSFT = 1000097000,

        /// <summary>
        /// Struct is of type `XrSceneCreateInfoMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SCENE_CREATE_INFO_MSFT`.
        /// Provided by `XR_MSFT_scene_understanding`.
        /// </summary>
        SceneCreateInfoMSFT = 1000097001,

        /// <summary>
        /// Struct is of type `XrNewSceneComputeInfoMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_NEW_SCENE_COMPUTE_INFO_MSFT`.
        /// Provided by `XR_MSFT_scene_understanding`.
        /// </summary>
        NewSceneComputeInfoMSFT = 1000097002,

        /// <summary>
        /// Struct is of type `XrVisualMeshComputeLodInfoMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_VISUAL_MESH_COMPUTE_LOD_INFO_MSFT`.
        /// Provided by `XR_MSFT_scene_understanding`.
        /// </summary>
        VisualMeshComputeLodInfoMSFT = 1000097003,

        /// <summary>
        /// Struct is of type `XrSceneComponentsMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SCENE_COMPONENTS_MSFT`.
        /// Provided by `XR_MSFT_scene_understanding`.
        /// </summary>
        SceneComponentsMSFT = 1000097004,

        /// <summary>
        /// Struct is of type `XrSceneComponentsGetInfoMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SCENE_COMPONENTS_GET_INFO_MSFT`.
        /// Provided by `XR_MSFT_scene_understanding`.
        /// </summary>
        SceneComponentsGetInfoMSFT = 1000097005,

        /// <summary>
        /// Struct is of type `XrSceneComponentLocationsMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SCENE_COMPONENT_LOCATIONS_MSFT`.
        /// Provided by `XR_MSFT_scene_understanding`.
        /// </summary>
        SceneComponentLocationsMSFT = 1000097006,

        /// <summary>
        /// Struct is of type `XrSceneComponentsLocateInfoMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SCENE_COMPONENTS_LOCATE_INFO_MSFT`.
        /// Provided by `XR_MSFT_scene_understanding`.
        /// </summary>
        SceneComponentsLocateInfoMSFT = 1000097007,

        /// <summary>
        /// Struct is of type `XrSceneObjectsMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SCENE_OBJECTS_MSFT`.
        /// Provided by `XR_MSFT_scene_understanding`.
        /// </summary>
        SceneObjectsMSFT = 1000097008,

        /// <summary>
        /// Struct is of type `XrSceneComponentParentFilterInfoMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SCENE_COMPONENT_PARENT_FILTER_INFO_MSFT`.
        /// Provided by `XR_MSFT_scene_understanding`.
        /// </summary>
        SceneComponentParentFilterInfoMSFT = 1000097009,

        /// <summary>
        /// Struct is of type `XrSceneObjectTypesFilterInfoMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SCENE_OBJECT_TYPES_FILTER_INFO_MSFT`.
        /// Provided by `XR_MSFT_scene_understanding`.
        /// </summary>
        SceneObjectTypesFilterInfoMSFT = 1000097010,

        /// <summary>
        /// Struct is of type `XrScenePlanesMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SCENE_PLANES_MSFT`.
        /// Provided by `XR_MSFT_scene_understanding`.
        /// </summary>
        ScenePlanesMSFT = 1000097011,

        /// <summary>
        /// Struct is of type `XrScenePlaneAlignmentFilterInfoMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SCENE_PLANE_ALIGNMENT_FILTER_INFO_MSFT`.
        /// Provided by `XR_MSFT_scene_understanding`.
        /// </summary>
        ScenePlaneAlignmentFilterInfoMSFT = 1000097012,

        /// <summary>
        /// Struct is of type `XrSceneMeshesMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SCENE_MESHES_MSFT`.
        /// Provided by `XR_MSFT_scene_understanding`.
        /// </summary>
        SceneMeshesMSFT = 1000097013,

        /// <summary>
        /// Struct is of type `XrSceneMeshBuffersGetInfoMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SCENE_MESH_BUFFERS_GET_INFO_MSFT`.
        /// Provided by `XR_MSFT_scene_understanding`.
        /// </summary>
        SceneMeshBuffersGetInfoMSFT = 1000097014,

        /// <summary>
        /// Struct is of type XrSceneMeshBuffersMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SCENE_MESH_BUFFERS_MSFT`.
        /// Provided by `XR_MSFT_scene_understanding`.
        /// </summary>
        SceneMeshBuffersMSFT = 1000097015,

        /// <summary>
        /// Struct is of type `XrSceneMeshVertexBufferMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SCENE_MESH_VERTEX_BUFFERS_MSFT`.
        /// Provided by `XR_MSFT_scene_understanding`.
        /// </summary>
        SceneMeshVertexBufferMSFT = 1000097016,

        /// <summary>
        /// Struct is of type `XrSceneMeshIndicesUint32MSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SCENE_MESH_INDICES_UINT32_MSFT`.
        /// Provided by `XR_MSFT_scene_understanding`.
        /// </summary>
        SceneMeshIndicesUint32MSFT = 1000097017,

        /// <summary>
        /// Struct is of type `XrSceneMeshIndicesUint16MSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SCENE_MESH_INDICES_UINT16_MSFT`.
        /// Provided by `XR_MSFT_scene_understanding`.
        /// </summary>
        SceneMeshIndicesUint16MSFT = 1000097018,

        /// <summary>
        /// Struct is of type `XrSerializedSceneFragmentDataGetInfoMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SERIALIZED_SCENE_FRAGMENT_DATA_GET_INFO_MSFT`.
        /// Provided by `XR_MSFT_scene_understanding_serialization`.
        /// </summary>
        SerializedSceneFragmentDataGetInfoMSFT = 1000098000,

        /// <summary>
        /// Struct is of type `XrSceneDeserializeInfoMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SCENE_DESERIALIZE_INFO_MSFT`.
        /// Provided by `XR_MSFT_scene_understanding_serialization`.
        /// </summary>
        SceneDeserializeInfoMSFT = 1000098001,

        /// <summary>
        /// Struct is of type `XrEventDataDisplayRefreshRateChangedFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_DISPLAY_REFRESH_RATE_CHANGED_FB`.
        /// Provided by `XR_FB_display_refresh_rate`.
        /// </summary>
        EventDataDisplayRefreshRateChangedFB = 1000101000,

        /// <summary>
        /// Struct is of type `XrViveTrackerPathsHTCX`.
        /// Equivalent to the OpenXR value `XR_TYPE_VIVE_TRACKER_PATHS_HTCX`.
        /// Provided by `XR_HTCX_vive_tracker_interaction`.
        /// </summary>
        ViveTrackerPathsHTCX = 1000103000,

        /// <summary>
        /// Struct is of type `XrEventDataViveTrackerConnectedHTCX`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_VIVE_TRACKER_CONNECTED_HTCX`.
        /// Provided by `XR_HTCX_vive_tracker_interaction`.
        /// </summary>
        EventDataViveTrackerConnectedHTCX = 1000103001,

        /// <summary>
        /// Struct is of type `XrSystemFacialTrackingPropertiesHTC`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_FACIAL_TRACKING_PROPERTIES_HTC`.
        /// Provided by `XR_HTC_facial_tracking`.
        /// </summary>
        SystemFacialTrackingPropertiesHTC = 1000104000,

        /// <summary>
        /// Struct is of type `XrFacialTrackerCreateInfoHTC`.
        /// Equivalent to the OpenXR value `XR_TYPE_FACIAL_TRACKER_CREATE_INFO_HTC`.
        /// Provided by `XR_HTC_facial_tracking`.
        /// </summary>
        FacialTrackerCreateInfoHTC = 1000104001,

        /// <summary>
        /// Struct is of type `XrFacialExpressionsHTC`.
        /// Equivalent to the OpenXR value `XR_TYPE_FACIAL_EXPRESSIONS_HTC`.
        /// Provided by `XR_HTC_facial_tracking`.
        /// </summary>
        FacialExpressionsHTC = 1000104002,

        /// <summary>
        /// Struct is of type `XrSystemColorSpacePropertiesFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_COLOR_SPACE_PROPERTIES_FB`.
        /// Provided by `XR_FB_color_space`.
        /// </summary>
        SystemColorSpacePropertiesFB = 1000108000,

        /// <summary>
        /// Struct is of type `XrHandTrackingMeshFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_HAND_TRACKING_MESH_FB`.
        /// Provided by `XR_FB_hand_tracking_mesh`.
        /// </summary>
        HandTrackingMeshFB = 1000110001,

        /// <summary>
        /// Struct is of type `XrHandTrackingScaleFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_HAND_TRACKING_SCALE_FB`.
        /// Provided by `XR_FB_hand_tracking_mesh`.
        /// </summary>
        HandTrackingScaleFB = 1000110003,

        /// <summary>
        /// Struct is of type `XrHandTrackingAimStateFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_HAND_TRACKING_AIM_STATE_FB`.
        /// Provided by `XR_FB_hand_tracking_aim`.
        /// </summary>
        HandTrackingAimStateFB = 1000111001,

        /// <summary>
        /// Struct is of type `XrHandTrackingCapsulesStateFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_HAND_TRACKING_CAPSULES_STATE_FB`.
        /// Provided by `XR_FB_hand_tracking_aim`.
        /// </summary>
        HandTrackingCapsulesStateFB = 1000112000,

        /// <summary>
        /// Struct is of type `XrSystemSpatialEntityPropertiesFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_SPATIAL_ENTITY_PROPERTIES_FB`.
        /// `Provided by XR_FB_spatial_entity`.
        /// </summary>
        SystemSpatialEntityPropertiesFB = 1000113004,

        /// <summary>
        /// Struct is of type `XrSpatialAnchorCreateInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ANCHOR_CREATE_INFO_FB`.
        /// Provided by `XR_FB_spatial_entity`.
        /// </summary>
        SpatialAnchorCreateInfoFB = 1000113003,

        /// <summary>
        /// Struct is of type `XrSpaceComponentStatusSetInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPACE_COMPONENT_STATUS_SET_INFO_FB`.
        /// Provided by `XR_FB_spatial_entity`.
        /// </summary>
        SpaceComponentStatusSetInfoFB = 1000113007,

        /// <summary>
        /// Struct is of type `XrSpaceComponentStatusFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPACE_COMPONENT_STATUS_FB`.
        /// Provided by `XR_FB_spatial_entity`.
        /// </summary>
        SpaceComponentStatusFB = 1000113001,

        /// <summary>
        /// Struct is of type `XrEventDataSpatialAnchorCreateCompleteFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_SPATIAL_ANCHOR_CREATE_COMPLETE_FB`.
        /// Provided by `XR_FB_spatial_entity`.
        /// </summary>
        EventDataSpatialAnchorCreateCompleteFB = 1000113005,

        /// <summary>
        /// Struct is of type `XrEventDataSpaceSetStatusCompleteFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_SPACE_SET_STATUS_COMPLETE_FB`.
        /// Provided by `XR_FB_spatial_entity`.
        /// </summary>
        EventDataSpaceSetStatusCompleteFB = 1000113006,

        /// <summary>
        /// Struct is of type `XrFoveationProfileCreateInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_FOVEATION_PROFILE_CREATE_INFO_FB`.
        /// Provided by `XR_FB_foveation`.
        /// </summary>
        FoveationProfileCreateInfoFB = 1000114000,

        /// <summary>
        /// Struct is of type `XrSwapchainCreateInfoFoveationFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SWAPCHAIN_CREATE_INFO_FOVEATION_FB`.
        /// Provided by `XR_FB_foveation`.
        /// </summary>
        SwapchainCreateInfoFoveationFB = 1000114001,

        /// <summary>
        /// Struct is of type `XrSwapchainStateFoveationFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SWAPCHAIN_STATE_FOVEATION_FB`.
        /// Provided by `XR_FB_foveation`.
        /// </summary>
        SwapchainStateFoveationFB = 1000114002,

        /// <summary>
        /// Struct is of type `XrFoveationLevelProfileCreateInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_FOVEATION_LEVEL_PROFILE_CREATE_INFO_FB`.
        /// Provided by `XR_FB_foveation_configuration`.
        /// </summary>
        FoveationLevelProfileCreateInfoFB = 1000115000,

        /// <summary>
        /// Struct is of type `XrKeyboardSpaceCreateInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_KEYBOARD_SPACE_CREATE_INFO_FB`.
        /// Provided by `XR_FB_keyboard_tracking`.
        /// </summary>
        KeyboardSpaceCreateInfoFB = 1000116009,

        /// <summary>
        /// Struct is of type `XrKeyboardTrackingQueryFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_KEYBOARD_TRACKING_QUERY_FB`.
        /// Provided by `XR_FB_keyboard_tracking`.
        /// </summary>
        KeyboardTrackingQueryFB = 1000116004,

        /// <summary>
        /// Struct is of type `XrSystemKeyboardTrackingPropertiesFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_KEYBOARD_TRACKING_PROPERTIES_FB`.
        /// Provided by `XR_FB_keyboard_tracking`.
        /// </summary>
        SystemKeyboardTrackingPropertiesFB = 1000116002,

        /// <summary>
        /// Struct is of type `XrTriangleMeshCreateInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_TRIANGLE_MESH_CREATE_INFO_FB`.
        /// Provided by `XR_FB_triangle_mesh`.
        /// </summary>
        TriangleMeshCreateInfoFB = 1000117001,

        /// <summary>
        /// Struct is of type `XrSystemPassthroughPropertiesFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_PASSTHROUGH_PROPERTIES_FB`.
        /// Provided by `XR_FB_passthrough`.
        /// </summary>
        SystemPassthroughPropertiesFB = 1000118000,

        /// <summary>
        /// Struct is of type `XrPassthroughCreateInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_PASSTHROUGH_CREATE_INFO_FB`.
        /// Provided by `XR_FB_passthrough`.
        /// </summary>
        PassthroughCreateInfoFB = 1000118001,

        /// <summary>
        /// Struct is of type `XrPassthroughLayerCreateInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_PASSTHROUGH_LAYER_CREATE_INFO_FB`.
        /// Provided by `XR_FB_passthrough`.
        /// </summary>
        PassthroughLayerCreateInfoFB = 1000118002,

        /// <summary>
        /// Struct is of type `XrCompositionLayerPassthroughFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_COMPOSITION_LAYER_PASSTHROUGH_FB`.
        /// Provided by `XR_FB_passthrough`.
        /// </summary>
        CompositionLayerPassthroughFB = 1000118003,

        /// <summary>
        /// Struct is of type `XrGeometryInstanceCreateInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_GEOMETRY_INSTANCE_CREATE_INFO_FB`.
        /// Provided by `XR_FB_passthrough`.
        /// </summary>
        GeometryInstanceCreateInfoFB = 1000118004,

        /// <summary>
        /// Struct is of type `XrGeometryInstanceTransformFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_GEOMETRY_INSTANCE_TRANSFORM_FB`.
        /// Provided by `XR_FB_passthrough`.
        /// </summary>
        GeometryInstanceTransformFB = 1000118005,

        /// <summary>
        /// Struct is of type `XrSystemPassthroughProperties2FB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_PASSTHROUGH_PROPERTIES2_FB`.
        /// Provided by `XR_FB_passthrough`.
        /// </summary>
        SystemPassthroughProperties2FB = 1000118006,

        /// <summary>
        /// Struct is of type `XrPassthroughStyleFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_PASSTHROUGH_STYLE_FB`.
        /// Provided by `XR_FB_passthrough`.
        /// </summary>
        PassthroughStyleFB = 1000118020,

        /// <summary>
        /// Struct is of type `XrPassthroughColorMapMonoToRgbaFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_PASSTHROUGH_COLOR_MAP_MONO_TO_RGBA_FB`.
        /// Provided by `XR_FB_passthrough`.
        /// </summary>
        PassthroughColorMapMonoToRgbaFB = 1000118021,

        /// <summary>
        /// Struct is of type `XrPassthroughColorMapMonoToMonoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_PASSTHROUGH_COLOR_MAP_MONO_TO_MONO_FB`.
        /// Provided by `XR_FB_passthrough`.
        /// </summary>
        PassthroughColorMapMonoToMonoFB = 1000118022,

        /// <summary>
        /// Struct is of type `XrPassthroughBrightnessContrastSaturationFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_PASSTHROUGH_BRIGHTNESS_CONTRAST_SATURATION_FB`.
        /// Provided by `XR_FB_passthrough`.
        /// </summary>
        PassthroughBrightnessContrastSaturationFB = 1000118023,

        /// <summary>
        /// Struct is of type `XrEventDataPassthroughStateChangedFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_PASSTHROUGH_STATE_CHANGED_FB`.
        /// Provided by `XR_FB_passthrough`.
        /// </summary>
        EventDataPassthroughStateChangedFB = 1000118030,

        /// <summary>
        /// Struct is of type `XrRenderModelPathInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_RENDER_MODEL_PATH_INFO_FB`.
        /// Provided by `XR_FB_render_model`.
        /// </summary>
        RenderModelPathInfoFB = 1000119000,

        /// <summary>
        /// Struct is of type `XrRenderModelPropertiesFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_RENDER_MODEL_PROPERTIES_FB`.
        /// Provided by `XR_FB_render_model`.
        /// </summary>
        RenderModelPropertiesFB = 1000119001,

        /// <summary>
        /// Struct is of type `XrRenderModelBufferFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_RENDER_MODEL_BUFFER_FB`.
        /// Provided by `XR_FB_render_model`.
        /// </summary>
        RenderModelBufferFB = 1000119002,

        /// <summary>
        /// Struct is of type `XrRenderModelLoadInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_RENDER_MODEL_LOAD_INFO_FB`.
        /// Provided by `XR_FB_render_model`.
        /// </summary>
        RenderModelLoadInfoFB = 1000119003,

        /// <summary>
        /// Struct is of type `XrSystemRenderModelPropertiesFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_RENDER_MODEL_PROPERTIES_FB`.
        /// Provided by `XR_FB_render_model`.
        /// </summary>
        SystemRenderModelPropertiesFB = 1000119004,

        /// <summary>
        /// Struct is of type `XrRenderModelCapabilitiesRequestFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_RENDER_MODEL_CAPABILITIES_REQUEST_FB`.
        /// Provided by `XR_FB_render_model`.
        /// </summary>
        RenderModelCapabilitiesRequestFB = 1000119005,

        /// <summary>
        /// Struct is of type `XrBindingModificationsKHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_BINDING_MODIFICATIONS_KHR`.
        /// Provided by `XR_KHR_binding_modification`.
        /// </summary>
        BindingModificationsKHR = 1000120000,

        /// <summary>
        /// Struct is of type `XrViewLocateFoveatedRenderingVARJO`.
        /// Equivalent to the OpenXR value `XR_TYPE_VIEW_LOCATE_FOVEATED_RENDERING_VARJO`.
        /// Provided by `XR_VARJO_foveated_rendering`.
        /// </summary>
        ViewLocateFoveatedRenderingVARJO = 1000121000,

        /// <summary>
        /// Struct is of type `XrFoveatedViewConfigurationViewVARJO`.
        /// Equivalent to the OpenXR value `XR_TYPE_FOVEATED_VIEW_CONFIGURATION_VIEW_VARJO`.
        /// Provided by `XR_VARJO_foveated_rendering`.
        /// </summary>
        FoveatedViewConfigurationViewVARJO = 1000121001,

        /// <summary>
        /// Struct is of type `XrSystemFoveatedRenderingPropertiesVARJO`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_FOVEATED_RENDERING_PROPERTIES_VARJO`.
        /// Provided by `XR_VARJO_foveated_rendering`.
        /// </summary>
        SystemFoveatedRenderingPropertiesVARJO = 1000121002,

        /// <summary>
        /// Struct is of type `XrCompositionLayerDepthTestVARJO`.
        /// Equivalent to the OpenXR value `XR_TYPE_COMPOSITION_LAYER_DEPTH_TEST_VARJO`.
        /// Provided by `XR_VARJO_composition_layer_depth_test`.
        /// </summary>
        CompositionLayerDepthTestVARJO = 1000122000,

        /// <summary>
        /// Struct is of type `XrSystemMarkerTrackingPropertiesVARJO`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_MARKER_TRACKING_PROPERTIES_VARJO`.
        /// Provided by `XR_VARJO_marker_tracking`.
        /// </summary>
        SystemMarkerTrackingPropertiesVARJO = 1000124000,

        /// <summary>
        /// Struct is of type `XrEventDataMarkerTrackingUpdateVARJO`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_MARKER_TRACKING_UPDATE_VARJO`.
        /// Provided by `XR_VARJO_marker_tracking`.
        /// </summary>
        EventDataMarkerTrackingUpdateVARJO = 1000124001,

        /// <summary>
        /// Struct is of type `XrMarkerSpaceCreateInfoVARJO`.
        /// Equivalent to the OpenXR value `XR_TYPE_MARKER_SPACE_CREATE_INFO_VARJO`.
        /// Provided by `XR_VARJO_marker_tracking`.
        /// </summary>
        MarkerSpaceCreateInfoVARJO = 1000124002,

        /// <summary>
        /// Struct is of type `XrFrameEndInfoML`.
        /// Equivalent to the OpenXR value `XR_TYPE_FRAME_END_INFO_ML`.
        /// Provided by `XR_ML_frame_end_info`.
        /// </summary>
        FrameEndInfoML = 1000135000,

        /// <summary>
        /// Struct is of type `XrGlobalDimmerFrameEndInfoML`.
        /// Equivalent to the OpenXR value `XR_TYPE_GLOBAL_DIMMER_FRAME_END_INFO_ML`.
        /// Provided by `XR_ML_global_dimmer`.
        /// </summary>
        GlobalDimmerFrameEndInfoML = 1000136000,

        /// <summary>
        /// Struct is of type `XrCoordinateSpaceCreateInfoML`.
        /// Equivalent to the OpenXR value `XR_TYPE_COORDINATE_SPACE_CREATE_INFO_ML`.
        /// Provided by `XR_ML_compat`.
        /// </summary>
        CoordinateSpaceCreateInfoML = 1000137000,

        /// <summary>
        /// Struct is of type `XrSystemMarkerUnderstandingPropertiesML`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_MARKER_UNDERSTANDING_PROPERTIES_ML`.
        /// Provided by `XR_ML_marker_understanding`.
        /// </summary>
        SystemMarkerUnderstandingPropertiesML = 1000138000,

        /// <summary>
        /// Struct is of type `XrMarkerDetectorCreateInfoML`.
        /// Equivalent to the OpenXR value `XR_TYPE_MARKER_DETECTOR_CREATE_INFO_ML`.
        /// Provided by `XR_ML_marker_understanding`.
        /// </summary>
        MarkerDetectorCreateInfoML = 1000138001,

        /// <summary>
        /// Struct is of type `XrMarkerDetectorArucoInfoML`.
        /// Equivalent to the OpenXR value `XR_TYPE_MARKER_DETECTOR_ARUCO_INFO_ML`.
        /// Provided by `XR_ML_marker_understanding`.
        /// </summary>
        MarkerDetectorArucoInfoML = 1000138002,

        /// <summary>
        /// Struct is of type `XrMarkerDetectorSizeInfoML`.
        /// Equivalent to the OpenXR value `XR_TYPE_MARKER_DETECTOR_SIZE_INFO_ML`.
        /// Provided by `XR_ML_marker_understanding`.
        /// </summary>
        MarkerDetectorSizeInfoML = 1000138003,

        /// <summary>
        /// Struct is of type `XrMarkerDetectorAprilTagInfoML`.
        /// Equivalent to the OpenXR value `XR_TYPE_MARKER_DETECTOR_APRIL_TAG_INFO_ML`.
        /// Provided by `XR_ML_marker_understanding`.
        /// </summary>
        MarkerDetectorAprilTagInfoML = 1000138004,

        /// <summary>
        /// Struct is of type `XrMarkerDetectorCustomProfileInfoML`.
        /// Equivalent to the OpenXR value `XR_TYPE_MARKER_DETECTOR_CUSTOM_PROFILE_INFO_ML`.
        /// Provided by `XR_ML_marker_understanding`.
        /// </summary>
        MarkerDetectorCustomProfileInfoML = 1000138005,

        /// <summary>
        /// Struct is of type `XrMarkerDetectorSnapshotInfoML`.
        /// Equivalent to the OpenXR value `XR_TYPE_MARKER_DETECTOR_SNAPSHOT_INFO_ML`.
        /// Provided by `XR_ML_marker_understanding`.
        /// </summary>
        MarkerDetectorSnapshotInfoML = 1000138006,

        /// <summary>
        /// Struct is of type `XrMarkerDetectorStateML`.
        /// Equivalent to the OpenXR value `XR_TYPE_MARKER_DETECTOR_STATE_ML`.
        /// Provided by `XR_ML_marker_understanding`.
        /// </summary>
        MarkerDetectorStateML = 1000138007,

        /// <summary>
        /// Struct is of type `XrMarkerSpaceCreateInfoML`.
        /// Equivalent to the OpenXR value `XR_TYPE_MARKER_SPACE_CREATE_INFO_ML`.
        /// Provided by `XR_ML_marker_understanding`.
        /// </summary>
        MarkerSpaceCreateInfoML = 1000138008,

        /// <summary>
        /// Struct is of type `XrLocalizationMapML`.
        /// Equivalent to the OpenXR value `XR_TYPE_LOCALIZATION_MAP_ML`.
        /// Provided by `XR_ML_localization_map`.
        /// </summary>
        LocalizationMapML = 1000139000,

        /// <summary>
        /// Struct is of type `XrEventDataLocalizationChangedML`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_LOCALIZATION_CHANGED_ML`.
        /// Provided by `XR_ML_localization_map`.
        /// </summary>
        EventDataLocalizationChangedML = 1000139001,

        /// <summary>
        /// Struct is of type `XrMapLocalizationRequestInfoML`.
        /// Equivalent to the OpenXR value `XR_TYPE_MAP_LOCALIZATION_REQUEST_INFO_ML`.
        /// Provided by `XR_ML_localization_map`.
        /// </summary>
        MapLocalizationRequestInfoML = 1000139002,

        /// <summary>
        /// Struct is of type `XrLocalizationMapImportInfoML`.
        /// Equivalent to the OpenXR value `XR_TYPE_LOCALIZATION_MAP_IMPORT_INFO_ML`.
        /// Provided by `XR_ML_localization_map`.
        /// </summary>
        LocalizationMapImportInfoML = 1000139003,

        /// <summary>
        /// Struct is of type `XrLocalizationEnableEventsInfoML`.
        /// Equivalent to the OpenXR value `XR_TYPE_LOCALIZATION_ENABLE_EVENTS_INFO_ML`.
        /// Provided by `XR_ML_localization_map`.
        /// </summary>
        LocalizationEnableEventsInfoML = 1000139004,

        /// <summary>
        /// Struct is of type `XrSpatialAnchorsCreateInfoFromPoseML`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ANCHORS_CREATE_INFO_FROM_POSE_ML`.
        /// Provided by `XR_ML_spatial_anchors`.
        /// </summary>
        SpatialAnchorsCreateInfoFromPoseML = 1000140000,

        /// <summary>
        /// Struct is of type `XrCreateSpatialAnchorsCompletionML`.
        /// Equivalent to the OpenXR value `XR_TYPE_CREATE_SPATIAL_ANCHORS_COMPLETION_ML`.
        /// Provided by `XR_ML_spatial_anchors`.
        /// </summary>
        CreateSpatialAnchorsCompletionML = 1000140001,

        /// <summary>
        /// Struct is of type `XrSpatialAnchorStateML`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ANCHOR_STATE_ML`.
        /// Provided by `XR_ML_spatial_anchors`.
        /// </summary>
        SpatialAnchorStateML = 1000140002,

        /// <summary>
        /// Struct is of type `XrSpatialAnchorsCreateStorageInfoML`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ANCHORS_CREATE_STORAGE_INFO_ML`.
        /// Provided by `XR_ML_spatial_anchors_storage`.
        /// </summary>
        SpatialAnchorsCreateStorageInfoML = 1000141000,

        /// <summary>
        /// Struct is of type `XrSpatialAnchorsQueryInfoRadiusML`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ANCHORS_QUERY_INFO_RADIUS_ML`.
        /// Provided by `XR_ML_spatial_anchors_storage`.
        /// </summary>
        SpatialAnchorsQueryInfoRadiusML = 1000141001,

        /// <summary>
        /// Struct is of type `XrSpatialAnchorsQueryCompletionML`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ANCHORS_QUERY_COMPLETION_ML`.
        /// Provided by `XR_ML_spatial_anchors_storage`.
        /// </summary>
        SpatialAnchorsQueryCompletionML = 1000141002,

        /// <summary>
        /// Struct is of type `XrSpatialAnchorsCreateInfoFromUuidsML`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ANCHORS_CREATE_INFO_FROM_UUIDS_ML`.
        /// Provided by `XR_ML_spatial_anchors_storage`.
        /// </summary>
        SpatialAnchorsCreateInfoFromUuidsML = 1000141003,

        /// <summary>
        /// Struct is of type `XrSpatialAnchorsPublishInfoML`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ANCHORS_PUBLISH_INFO_ML`.
        /// Provided by `XR_ML_spatial_anchors_storage`.
        /// </summary>
        SpatialAnchorsPublishInfoML = 1000141004,

        /// <summary>
        /// Struct is of type `XrSpatialAnchorsPublishCompletionML`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ANCHORS_PUBLISH_COMPLETION_ML`.
        /// Provided by `XR_ML_spatial_anchors_storage`.
        /// </summary>
        SpatialAnchorsPublishCompletionML = 1000141005,

        /// <summary>
        /// Struct is of type `XrSpatialAnchorsDeleteInfoML`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ANCHORS_DELETE_INFO_ML`.
        /// Provided by `XR_ML_spatial_anchors_storage`.
        /// </summary>
        SpatialAnchorsDeleteInfoML = 1000141006,

        /// <summary>
        /// Struct is of type `XrSpatialAnchorsDeleteCompletionML`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ANCHORS_DELETE_COMPLETION_ML`.
        /// Provided by `XR_ML_spatial_anchors_storage`.
        /// </summary>
        SpatialAnchorsDeleteCompletionML = 1000141007,

        /// <summary>
        /// Struct is of type `XrSpatialAnchorsUpdateExpirationInfoML`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ANCHORS_UPDATE_EXPIRATION_INFO_ML`.
        /// Provided by `XR_ML_spatial_anchors_storage`.
        /// </summary>
        SpatialAnchorsUpdateExpirationInfoML = 1000141008,

        /// <summary>
        /// Struct is of type `XrSpatialAnchorsUpdateExpirationCompletionML`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ANCHORS_UPDATE_EXPIRATION_COMPLETION_ML`.
        /// Provided by `XR_ML_spatial_anchors_storage`.
        /// </summary>
        SpatialAnchorsUpdateExpirationCompletionML = 1000141009,

        /// <summary>
        /// Struct is of type `XrSpatialAnchorsPublishCompletionDetailsML`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ANCHORS_PUBLISH_COMPLETION_DETAILS_ML`.
        /// Provided by `XR_ML_spatial_anchors_storage`.
        /// </summary>
        SpatialAnchorsPublishCompletionDetailsML = 1000141010,

        /// <summary>
        /// Struct is of type `XrSpatialAnchorsDeleteCompletionDetailsML`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ANCHORS_DELETE_COMPLETION_DETAILS_ML`.
        /// Provided by `XR_ML_spatial_anchors_storage`.
        /// </summary>
        SpatialAnchorsDeleteCompletionDetailsML = 1000141011,

        /// <summary>
        /// Struct is of type `XrSpatialAnchorsUpdateExpirationCompletionDetailsML`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ANCHORS_UPDATE_EXPIRATION_COMPLETION_DETAILS_ML`.
        /// Provided by `XR_ML_spatial_anchors_storage`.
        /// </summary>
        SpatialAnchorsUpdateExpirationCompletionDetailsML = 1000141012,

        /// <summary>
        /// Struct is of type `XrEventDataHeadsetFitChangedML`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_HEADSET_FIT_CHANGED_ML`.
        /// Provided by `XR_ML_user_calibration`.
        /// </summary>
        EventDataHeadsetFitChangedML = 1000472000,

        /// <summary>
        /// Struct is of type `XrEventDataEyeCalibrationChangedML`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_EYE_CALIBRATION_CHANGED_ML`.
        /// Provided by `XR_ML_user_calibration`.
        /// </summary>
        EventDataEyeCalibrationChangedML = 1000472001,

        /// <summary>
        /// Struct is of type `XrUserCalibrationEnableEventsInfoML`.
        /// Equivalent to the OpenXR value `XR_TYPE_USER_CALIBRATION_ENABLE_EVENTS_INFO_ML`.
        /// Provided by `XR_ML_user_calibration`.
        /// </summary>
        UserCalibrationEnableEventsInfoML = 1000472002,

        /// <summary>
        /// Struct is of type `XrSpatialAnchorPersistenceInfoMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ANCHOR_PERSISTENCE_INFO_MSFT`.
        /// Provided by `XR_MSFT_spatial_anchor_persistence`.
        /// </summary>
        SpatialAnchorPersistenceInfoMSFT = 1000142000,

        /// <summary>
        /// Struct is of type `XrSpatialAnchorFromPersistedAnchorCreateInfoMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ANCHOR_FROM_PERSISTED_ANCHOR_CREATE_INFO_MSFT`.
        /// Provided by `XR_MSFT_spatial_anchor_persistence`.
        /// </summary>
        SpatialAnchorFromPersistedAnchorCreateInfoMSFT = 1000142001,

        /// <summary>
        /// Struct is of type `XrSceneMarkersMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SCENE_MARKERS_MSFT`.
        /// Provided by `XR_MSFT_scene_marker`.
        /// </summary>
        SceneMarkersMSFT = 1000147000,

        /// <summary>
        /// Struct is of type `XrSceneMarkerTypeFilterMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SCENE_MARKER_TYPE_FILTERS_MSFT`.
        /// Provided by `XR_MSFT_scene_marker`.
        /// </summary>
        SceneMarkerTypeFilterMSFT = 1000147001,

        /// <summary>
        /// Struct is of type `XrSceneMarkerQRCodesMSFT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SCENE_MARKER_QR_CODES_MSFT`.
        /// Provided by `XR_MSFT_scene_marker`.
        /// </summary>
        SceneMarkerQRCodesMSFT = 1000147002,

        /// <summary>
        /// Struct is of type `XrSpaceQueryInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPACE_QUERY_INFO_FB`.
        /// Provided by `XR_FB_spatial_entity_query`.
        /// </summary>
        SpaceQueryInfoFB = 1000156001,

        /// <summary>
        /// Struct is of type `XrSpaceQueryResultsFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPACE_QUERY_RESULTS_FB`.
        /// Provided by `XR_FB_spatial_entity_query`.
        /// </summary>
        SpaceQueryResultsFB = 1000156002,

        /// <summary>
        /// Struct is of type `XrSpaceStorageLocationFilterInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPACE_STORAGE_LOCATION_FILTER_INFO_FB`.
        /// Provided by `XR_FB_spatial_entity_query`.
        /// </summary>
        SpaceStorageLocationFilterInfoFB = 1000156003,

        /// <summary>
        /// Struct is of type `XrSpaceUuidFilterInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPACE_UUID_FILTER_INFO_FB`.
        /// Provided by `XR_FB_spatial_entity_query`.
        /// </summary>
        SpaceUuidFilterInfoFB = 1000156054,

        /// <summary>
        /// Struct is of type `XrSpaceComponentFilterInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPACE_COMPONENT_FILTER_INFO_FB`.
        /// Provided by `XR_FB_spatial_entity_query`.
        /// </summary>
        SpaceComponentFilterInfoFB = 1000156052,

        /// <summary>
        /// Struct is of type `XrEventDataSpaceQueryResultsAvailableFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_SPACE_QUERY_RESULTS_AVAILABLE_FB`.
        /// Provided by `XR_FB_spatial_entity_query`.
        /// </summary>
        EventDataSpaceQueryResultsAvailableFB = 1000156103,

        /// <summary>
        /// Struct is of type `XrEventDataSpaceQueryCompleteFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_SPACE_QUERY_COMPLETE_FB`.
        /// Provided by `XR_FB_spatial_entity_query`.
        /// </summary>
        EventDataSpaceQueryCompleteFB = 1000156104,

        /// <summary>
        /// Struct is of type `XrSpaceSaveInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPACE_SAVE_INFO_FB`.
        /// Provided by `XR_FB_spatial_entity_storage`.
        /// </summary>
        SpaceSaveInfoFB = 1000158000,

        /// <summary>
        /// Struct is of type `XrSpaceEraseInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPACE_ERASE_INFO_FB`.
        /// Provided by `XR_FB_spatial_entity_storage`.
        /// </summary>
        SpaceEraseInfoFB = 1000158001,

        /// <summary>
        /// Struct is of type `XrEventDataSpaceSaveCompleteFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_SPACE_SAVE_COMPLETE_FB`.
        /// Provided by `XR_FB_spatial_entity_storage`.
        /// </summary>
        EventDataSpaceSaveCompleteFB = 1000158106,

        /// <summary>
        /// Struct is of type `XrEventDataSpaceEraseCompleteFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_SPACE_ERASE_COMPLETE_FB`.
        /// Provided by `XR_FB_spatial_entity_storage`.
        /// </summary>
        EventDataSpaceEraseCompleteFB = 1000158107,

        /// <summary>
        /// Struct is of type `XrSwapchainImageFoveationVulkanFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SWAPCHAIN_IMAGE_FOVEATION_VULKAN_FB`.
        /// Provided by `XR_FB_foveation_vulkan`.
        /// </summary>
        SwapchainImageFoveationVulkanFB = 1000160000,

        /// <summary>
        /// Struct is of type `XrSwapchainStateAndroidSurfaceDimensionsFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SWAPCHAIN_STATE_ANDROID_SURFACE_DIMENSIONS_FB`.
        /// Provided by `XR_FB_swapchain_update_state_android_surface`.
        /// </summary>
        SwapchainStateAndroidSurfaceDimensionsFB = 1000161000,

        /// <summary>
        /// Struct is of type `XrSwapchainStateSamplerOpenGLESFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SWAPCHAIN_STATE_SAMPLER_OPENGL_ES_FB`.
        /// Provided by `XR_FB_swapchain_update_state_opengl_es`.
        /// </summary>
        SwapchainStateSamplerOpenGLESFB = 1000162000,

        /// <summary>
        /// Struct is of type `XrSwapchainStateSamplerVulkanFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SWAPCHAIN_STATE_SAMPLER_VULKAN_FB`.
        /// Provided by `XR_FB_swapchain_update_state_vulkan`.
        /// </summary>
        SwapchainStateSamplerVulkanFB = 1000163000,

        /// <summary>
        /// Struct is of type `XrSpaceShareInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPACE_SHARE_INFO_FB`.
        /// Provided by `XR_FB_spatial_entity_sharing`.
        /// </summary>
        SpaceShareInfoFB = 1000169001,

        /// <summary>
        /// Struct is of type `XrEventDataSpaceShareCompleteFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_SPACE_SHARE_COMPLETE_FB`.
        /// Provided by `XR_FB_spatial_entity_sharing`.
        /// </summary>
        EventDataSpaceShareCompleteFB = 1000169002,

        /// <summary>
        /// Struct is of type `XrCompositionLayerSpaceWarpInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_COMPOSITION_LAYER_SPACE_WARP_INFO_FB`.
        /// Provided by `XR_FB_space_warp`.
        /// </summary>
        CompositionLayerSpaceWarpInfoFB = 1000171000,

        /// <summary>
        /// Struct is of type `XrSystemSpaceWarpPropertiesFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_SPACE_WARP_PROPERTIES_FB`.
        /// Provided by `XR_FB_space_warp`.
        /// </summary>
        SystemSpaceWarpPropertiesFB = 1000171001,

        /// <summary>
        /// Struct is of type `XrHapticAmplitudeEnvelopeVibrationFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_HAPTIC_AMPLITUDE_ENVELOPE_VIBRATION_FB`.
        /// Provided by `XR_FB_haptic_amplitude_envelope`.
        /// </summary>
        HapticAmplitudeEnvelopeVibrationFB = 1000173001,

        /// <summary>
        /// Struct is of type `XrSemanticLabelsFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SEMANTIC_LABELS_FB`.
        /// Provided by `XR_FB_scene`.
        /// </summary>
        SemanticLabelsFB = 1000175000,

        /// <summary>
        /// Struct is of type `XrRoomLayoutFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_ROOM_LAYOUT_FB`.
        /// Provided by `XR_FB_scene`.
        /// </summary>
        RoomLayoutFB = 1000175001,

        /// <summary>
        /// Struct is of type `XrBoundary2DFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_BOUNDARY_2D_FB`.
        /// Provided by `XR_FB_scene`.
        /// </summary>
        Boundary2DFB = 1000175002,

        /// <summary>
        /// Struct is of type `XrSemanticLabelsSupportInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SEMANTIC_LABELS_SUPPORT_INFO_FB`.
        /// Provided by `XR_FB_scene`.
        /// </summary>
        SemanticLabelsSupportInfoFB = 1000175010,

        /// <summary>
        /// Struct is of type `XrDigitalLensControlALMALENCE`.
        /// Equivalent to the OpenXR value `XR_TYPE_DIGITAL_LENS_CONTROL_ALMALENCE`.
        /// Provided by `XR_ALMALENCE_digital_lens_control`.
        /// </summary>
        DigitalLensControlALMALENCE = 1000196000,

        /// <summary>
        /// Struct is of type `XrEventDataSceneCaptureCompleteFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_SCENE_CAPTURE_COMPLETE_FB`.
        /// Provided by `XR_FB_scene_capture`.
        /// </summary>
        EventDataSceneCaptureCompleteFB = 1000198001,

        /// <summary>
        /// Struct is of type `XrSceneCaptureRequestInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SCENE_CAPTURE_REQUEST_INFO_FB`.
        /// Provided by `XR_FB_scene_capture`.
        /// </summary>
        SceneCaptureRequestInfoFB = 1000198050,

        /// <summary>
        /// Struct is of type `XrSpaceContainerFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPACE_CONTAINER_FB`.
        /// Provided by `XR_FB_spatial_entity_container`.
        /// </summary>
        SpaceContainerFB = 1000199000,

        /// <summary>
        /// Struct is of type `XrFoveationEyeTrackedProfileCreateInfoMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_FOVEATION_EYE_TRACKED_PROFILE_CREATE_INFO_META`.
        /// Provided by `XR_META_foveation_eye_tracked`.
        /// </summary>
        FoveationEyeTrackedProfileCreateInfoMETA = 1000200000,

        /// <summary>
        /// Struct is of type `XrFoveationEyeTrackedStateMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_FOVEATION_EYE_TRACKED_STATE_META`.
        /// Provided by `XR_META_foveation_eye_tracked`.
        /// </summary>
        FoveationEyeTrackedStateMETA = 1000200001,

        /// <summary>
        /// Struct is of type `XrSystemFoveationEyeTrackedPropertiesMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_FOVEATION_EYE_TRACKED_PROPERTIES_META`.
        /// Provided by `XR_META_foveation_eye_tracked`.
        /// </summary>
        SystemFoveationEyeTrackedPropertiesMETA = 1000200002,

        /// <summary>
        /// Struct is of type `XrSystemFaceTrackingPropertiesFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_FACE_TRACKING_PROPERTIES_FB`.
        /// Provided by `XR_FB_face_tracking`.
        /// </summary>
        SystemFaceTrackingPropertiesFB = 1000201004,

        /// <summary>
        /// Struct is of type `XrFaceTrackerCreateInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_FACE_TRACKER_CREATE_INFO_FB`.
        /// Provided by `XR_FB_face_tracking`.
        /// </summary>
        FaceTrackerCreateInfoFB = 1000201005,

        /// <summary>
        /// Struct is of type `XrFaceExpressionInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_FACE_EXPRESSION_INFO_FB`.
        /// Provided by `XR_FB_face_tracking`.
        /// </summary>
        FaceExpressionInfoFB = 1000201002,

        /// <summary>
        /// Struct is of type `XrFaceExpressionWeightsFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_FACE_EXPRESSION_WEIGHTS_FB`.
        /// Provided by `XR_FB_face_tracking`.
        /// </summary>
        FaceExpressionWeightsFB = 1000201006,

        /// <summary>
        /// Struct is of type `XrEyeTrackerCreateInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_EYE_TRACKER_CREATE_INFO_FB`.
        /// Provided by `XR_FB_eye_tracking_social`.
        /// </summary>
        EyeTrackerCreateInfoFB = 1000202001,

        /// <summary>
        /// Struct is of type `XrEyeGazesInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_EYE_GAZES_INFO_FB`.
        /// Provided by `XR_FB_eye_tracking_social`.
        /// </summary>
        EyeGazesInfoFB = 1000202002,

        /// <summary>
        /// Struct is of type `XrEyeGazesFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_EYE_GAZES_FB`.
        /// Provided by `XR_FB_eye_tracking_social`.
        /// </summary>
        EyeGazesFB = 1000202003,

        /// <summary>
        /// Struct is of type `XrSystemEyeTrackingPropertiesFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_EYE_TRACKING_PROPERTIES_FB`.
        /// Provided by `XR_FB_eye_tracking_social`.
        /// </summary>
        SystemEyeTrackingPropertiesFB = 1000202004,

        /// <summary>
        /// Struct is of type `XrPassthroughKeyboardHandsIntensityFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_PASSTHROUGH_KEYBOARD_HANDS_INTENSITY_FB`.
        /// Provided by `XR_FB_passthrough_keyboard_hands`.
        /// </summary>
        PassthroughKeyboardHandsIntensityFB = 1000203002,

        /// <summary>
        /// Struct is of type `XrCompositionLayerSettingsFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_COMPOSITION_LAYER_SETTINGS_FB`.
        /// Provided by `XR_FB_composition_layer_settings`.
        /// </summary>
        CompositionLayerSettingsFB = 1000204000,

        /// <summary>
        /// Struct is of type `XrHapticPcmVibrationFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_HAPTIC_PCM_VIBRATION_FB`.
        /// Provided by `XR_FB_haptic_pcm`.
        /// </summary>
        HapticPcmVibrationFB = 1000209001,

        /// <summary>
        /// Struct is of type `XrDevicePcmSampleRateFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_DEVICE_PCM_SAMPLE_RATE_STATE_FB`.
        /// Provided by `XR_FB_haptic_pcm`.
        /// </summary>
        DevicePcmSampleRateStateFB = 1000209002,

        /// <summary>
        /// Struct is of type `XrFrameSynthesisInfoEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_FRAME_SYNTHESIS_INFO_EXT`.
        /// Provided by `XR_EXT_frame_synthesis`.
        /// </summary>
        FrameSynthesisInfoEXT = 1000211000,

        /// <summary>
        /// Struct is of type `XrFrameSynthesisConfigViewEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_FRAME_SYNTHESIS_CONFIG_VIEW_EXT`.
        /// Provided by `XR_EXT_frame_synthesis`.
        /// </summary>
        FrameSynthesisConfigViewEXT = 1000211001,

        /// <summary>
        /// Struct is of type `XrCompositionLayerDepthTestFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_COMPOSITION_LAYER_DEPTH_TEST_FB`.
        /// Provided by `XR_FB_composition_layer_depth_test`.
        /// </summary>
        CompositionLayerDepthTestFB = 1000212000,

        /// <summary>
        /// Struct is of type `XrLocalDimmingFrameEndInfoMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_LOCAL_DIMMING_FRAME_END_INFO_META`.
        /// Provided by `XR_META_local_dimming`.
        /// </summary>
        LocalDimmingFrameEndInfoMETA = 1000216000,

        /// <summary>
        /// Struct is of type `XrPassthroughPreferencesMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_PASSTHROUGH_PREFERENCES_META`.
        /// Provided by `XR_META_passthrough_preferences`.
        /// </summary>
        PassthroughPreferencesMETA = 1000217000,

        /// <summary>
        /// Struct is of type `XrSystemVirtualKeyboardPropertiesMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_VIRTUAL_KEYBOARD_PROPERTIES_META`.
        /// Provided by `XR_META_virtual_keyboard`.
        /// </summary>
        SystemVirtualKeyboardPropertiesMETA = 1000219001,

        /// <summary>
        /// Struct is of type `XrVirtualKeyboardCreateInfoMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_VIRTUAL_KEYBOARD_CREATE_INFO_META`.
        /// Provided by `XR_META_virtual_keyboard`.
        /// </summary>
        VirtualKeyboardCreateInfoMETA = 1000219002,

        /// <summary>
        /// Struct is of type `XrVirtualKeyboardSpaceCreateInfoMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_VIRTUAL_KEYBOARD_SPACE_CREATE_INFO_META`.
        /// Provided by `XR_META_virtual_keyboard`.
        /// </summary>
        VirtualKeyboardSpaceCreateInfoMETA = 1000219003,

        /// <summary>
        /// Struct is of type `XrVirtualKeyboardLocationInfoMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_VIRTUAL_KEYBOARD_LOCATION_INFO_META`.
        /// Provided by `XR_META_virtual_keyboard`.
        /// </summary>
        VirtualKeyboardLocationInfoMETA = 1000219004,

        /// <summary>
        /// Struct is of type `XrVirtualKeyboardModelVisibilitySetInfoMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_VIRTUAL_KEYBOARD_MODEL_VISIBILITY_SET_INFO_META`.
        /// Provided by `XR_META_virtual_keyboard`.
        /// </summary>
        VirtualKeyboardModelVisibilitySetInfoMETA = 1000219005,

        /// <summary>
        /// Struct is of type `XrVirtualKeyboardAnimationStateMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_VIRTUAL_KEYBOARD_ANIMATION_STATE_META`.
        /// Provided by `XR_META_virtual_keyboard`.
        /// </summary>
        VirtualKeyboardAnimationStateMETA = 1000219006,

        /// <summary>
        /// Struct is of type `XrVirtualKeyboardModelAnimationStatesMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_VIRTUAL_KEYBOARD_MODEL_ANIMATION_STATES_META`.
        /// Provided by `XR_META_virtual_keyboard`.
        /// </summary>
        VirtualKeyboardModelAnimationStatesMETA = 1000219007,

        /// <summary>
        /// Struct is of type `XrVirtualKeyboardTextureDataMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_VIRTUAL_KEYBOARD_TEXTURE_DATA_META`.
        /// Provided by `XR_META_virtual_keyboard`.
        /// </summary>
        VirtualKeyboardTextureDataMETA = 1000219009,

        /// <summary>
        /// Struct is of type `XrVirtualKeyboardInputInfoMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_VIRTUAL_KEYBOARD_INPUT_INFO_META`.
        /// Provided by `XR_META_virtual_keyboard`.
        /// </summary>
        VirtualKeyboardInputInfoMETA = 1000219010,

        /// <summary>
        /// Struct is of type `XrVirtualKeyboardTextContextChangeInfoMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_VIRTUAL_KEYBOARD_TEXT_CONTEXT_CHANGE_INFO_META`.
        /// Provided by `XR_META_virtual_keyboard`.
        /// </summary>
        VirtualKeyboardTextContextChangeInfoMETA = 1000219011,

        /// <summary>
        /// Struct is of type `XrEventDataVirtualKeyboardCommitTextMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_VIRTUAL_KEYBOARD_COMMIT_TEXT_META`.
        /// Provided by `XR_META_virtual_keyboard`.
        /// </summary>
        EventDataVirtualKeyboardCommitTextMETA = 1000219014,

        /// <summary>
        /// Struct is of type `XrEventDataVirtualKeyboardBackspaceMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_VIRTUAL_KEYBOARD_BACKSPACE_META`.
        /// Provided by `XR_META_virtual_keyboard`.
        /// </summary>
        EventDataVirtualKeyboardBackspaceMETA = 1000219015,

        /// <summary>
        /// Struct is of type `XrEventDataVirtualKeyboardEnterMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_VIRTUAL_KEYBOARD_ENTER_META`.
        /// Provided by `XR_META_virtual_keyboard`.
        /// </summary>
        EventDataVirtualKeyboardEnterMETA = 1000219016,

        /// <summary>
        /// Struct is of type `XrEventDataVirtualKeyboardShownMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_VIRTUAL_KEYBOARD_SHOWN_META`.
        /// Provided by `XR_META_virtual_keyboard`.
        /// </summary>
        EventDataVirtualKeyboardShownMETA = 1000219017,

        /// <summary>
        /// Struct is of type `XrEventDataVirtualKeyboardHiddenMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_VIRTUAL_KEYBOARD_HIDDEN_META`.
        /// Provided by `XR_META_virtual_keyboard`.
        /// </summary>
        EventDataVirtualKeyboardHiddenMETA = 1000219018,

        /// <summary>
        /// Struct is of type `XrExternalCameraOCULUS`.
        /// Equivalent to the OpenXR value `XR_TYPE_EXTERNAL_CAMERA_OCULUS`.
        /// Provided by `XR_OCULUS_external_camera`.
        /// </summary>
        ExternalCameraOCULUS = 1000226000,

        /// <summary>
        /// Struct is of type `XrVulkanSwapchainCreateInfoMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_VULKAN_SWAPCHAIN_CREATE_INFO_META`.
        /// Provided by `XR_META_vulkan_swapchain_create_info`.
        /// </summary>
        VulkanSwapchainCreateInfoMETA = 1000227000,

        /// <summary>
        /// Struct is of type `XrPerformanceMetricsStateMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_PERFORMANCE_METRICS_STATE_META`.
        /// Provided by `XR_META_performance_metrics`.
        /// </summary>
        PerformanceMetricsStateMETA = 1000232001,

        /// <summary>
        /// Struct is of type `XrPerformanceMetricsCounterMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_PERFORMANCE_METRICS_COUNTER_META`.
        /// Provided by `XR_META_performance_metrics`.
        /// </summary>
        PerformanceMetricsCounterMETA = 1000232002,

        /// <summary>
        /// Struct is of type `XrSpaceListSaveInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPACE_LIST_SAVE_INFO_FB`.
        /// Provided by `XR_FB_spatial_entity_storage_batch`.
        /// </summary>
        SpaceListSaveInfoFB = 1000238000,

        /// <summary>
        /// Struct is of type `XrEventDataSpaceListSaveCompleteFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_SPACE_LIST_SAVE_COMPLETE_FB`.
        /// Provided by `XR_FB_spatial_entity_storage_batch`.
        /// </summary>
        EventDataSpaceListSaveCompleteFB = 1000238001,

        /// <summary>
        /// Struct is of type `XrSpaceUserCreateInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPACE_USER_CREATE_INFO_FB`.
        /// Provided by `XR_FB_spatial_entity_user`.
        /// </summary>
        SpaceUserCreateInfoFB = 1000241001,

        /// <summary>
        /// Struct is of type `XrSystemHeadsetIDPropertiesMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_HEADSET_ID_PROPERTIES_META`.
        /// Provided by `XR_META_headset_id`.
        /// </summary>
        SystemHeadsetIDPropertiesMETA = 1000245000,

        /// <summary>
        /// Struct is of type `XrRecommendedLayerResolutionMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_RECOMMENDED_LAYER_RESOLUTION_META`.
        /// Provided by `XR_META_recommended_layer_resolution`.
        /// </summary>
        RecommendedLayerResolutionMETA = 1000254000,

        /// <summary>
        /// Struct is of type `XrRecommendedLayerResolutionGetInfoMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_RECOMMENDED_LAYER_RESOLUTION_GET_INFO_META`.
        /// Provided by `XR_META_recommended_layer_resolution`.
        /// </summary>
        RecommendedLayerResolutionGetInfoMETA = 1000254001,

        /// <summary>
        /// Struct is of type `XrSystemPassthroughColorLutPropertiesMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_PASSTHROUGH_COLOR_LUT_PROPERTIES_META`.
        /// Provided by `XR_META_passthrough_color_lut`.
        /// </summary>
        SystemPassthroughColorLutPropertiesMETA = 1000266000,

        /// <summary>
        /// Struct is of type `XrPassthroughColorLutCreateInfoMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_PASSTHROUGH_COLOR_LUT_CREATE_INFO_META`.
        /// Provided by `XR_META_passthrough_color_lut`.
        /// </summary>
        PassthroughColorLutCreateInfoMETA = 1000266001,

        /// <summary>
        /// Struct is of type `XrPassthroughColorLutUpdateInfoMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_PASSTHROUGH_COLOR_LUT_UPDATE_INFO_META`.
        /// Provided by `XR_META_passthrough_color_lut`.
        /// </summary>
        PassthroughColorLutUpdateInfoMETA = 1000266002,

        /// <summary>
        /// Struct is of type `XrPassthroughColorMapLutMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_PASSTHROUGH_COLOR_MAP_LUT_META`.
        /// Provided by `XR_META_passthrough_color_lut`.
        /// </summary>
        PassthroughColorMapLutMETA = 1000266100,

        /// <summary>
        /// Struct is of type `XrPassthroughColorMapInterpolatedLutMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_PASSTHROUGH_COLOR_MAP_INTERPOLATED_LUT_META`.
        /// Provided by `XR_META_passthrough_color_lut`.
        /// </summary>
        PassthroughColorMapInterpolatedLutMETA = 1000266101,

        /// <summary>
        /// Struct is of type `XrSpaceTriangleMeshGetInfoMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPACE_TRIANGLE_MESH_GET_INFO_META`.
        /// Provided by `XR_META_spatial_entity_mesh`.
        /// </summary>
        SpaceTriangleMeshGetInfoMETA = 1000269001,

        /// <summary>
        /// Struct is of type `XrSpaceTriangleMeshMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPACE_TRIANGLE_MESH_META`.
        /// Provided by `XR_META_spatial_entity_mesh`.
        /// </summary>
        SpaceTriangleMeshMETA = 1000269002,

        /// <summary>
        /// Struct is of type `XrSystemPropertiesBodyTrackingFullBodyMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_PROPERTIES_BODY_TRACKING_FULL_BODY_META`.
        /// Provided by `XR_META_body_tracking_full_body`.
        /// </summary>
        SystemPropertiesBodyTrackingFullBodyMETA = 1000274000,

        /// <summary>
        /// Struct is of type `XrEventDataPassthroughLayerResumedMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_PASSTHROUGH_LAYER_RESUMED_META`.
        /// Provided by `XR_META_passthrough_layer_resumed_event`.
        /// </summary>
        EventDataPassthroughLayerResumedMETA = 1000282000,

        /// <summary>
        /// Struct is of type `XrSystemFaceTrackingProperties2FB`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_FACE_TRACKING_PROPERTIES2_FB`.
        /// Provided by `XR_FB_face_tracking2`.
        /// </summary>
        SystemFaceTrackingProperties2FB = 1000287013,

        /// <summary>
        /// Struct is of type `XrFaceTrackerCreateInfo2FB`.
        /// Equivalent to the OpenXR value `XR_TYPE_FACE_TRACKER_CREATE_INFO2_FB`.
        /// Provided by `XR_FB_face_tracking2`.
        /// </summary>
        FaceTrackerCreateInfo2FB = 1000287014,

        /// <summary>
        /// Struct is of type `XrFaceExpressionInfo2FB`.
        /// Equivalent to the OpenXR value `XR_TYPE_FACE_EXPRESSION_INFO2_FB`.
        /// Provided by `XR_FB_face_tracking2`.
        /// </summary>
        FaceExpressionInfo2FB = 1000287015,

        /// <summary>
        /// Struct is of type `XrFaceExpressionWeights2FB`.
        /// Equivalent to the OpenXR value `XR_TYPE_FACE_EXPRESSION_WEIGHTS2_FB`.
        /// Provided by `XR_FB_face_tracking2`.
        /// </summary>
        FaceExpressionWeights2FB = 1000287016,

        /// <summary>
        /// Struct is of type `XrSystemSpatialEntitySharingPropertiesMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_SPATIAL_ENTITY_SHARING_PROPERTIES_META`.
        /// Provided by `XR_META_spatial_entity_sharing`.
        /// </summary>
        SystemSpatialEntitySharingPropertiesMETA = 1000290000,

        /// <summary>
        /// Struct is of type `XrShareSpaceInfoMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_SHARE_SPACES_INFO_META`.
        /// Provided by `XR_META_spatial_entity_sharing`.
        /// </summary>
        ShareSpacesInfoMETA = 1000290001,

        /// <summary>
        /// Struct is of type `XrEventDataShareSpacesCompleteMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_SHARE_SPACES_COMPLETE_META`.
        /// Provided by `XR_META_spatial_entity_sharing`.
        /// </summary>
        EventDataShareSpacesCompleteMETA = 1000290002,

        /// <summary>
        /// Struct is of type `XrEnvironmentDepthProviderCreateInfoMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_ENVIRONMENT_DEPTH_PROVIDER_CREATE_INFO_META`.
        /// Provided by `XR_META_environment_depth`.
        /// </summary>
        EnvironmentDepthProviderCreateInfoMETA = 1000291000,

        /// <summary>
        /// Struct is of type `XrEnvironmentDepthSwapchainCreateInfoMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_ENVIRONMENT_DEPTH_SWAPCHAIN_CREATE_INFO_META`.
        /// Provided by `XR_META_environment_depth`.
        /// </summary>
        EnvironmentDepthSwapchainCreateInfoMETA = 1000291001,

        /// <summary>
        /// Struct is of type `XrEnvironmentDepthSwapchainStateMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_ENVIRONMENT_DEPTH_SWAPCHAIN_STATE_META`.
        /// Provided by `XR_META_environment_depth`.
        /// </summary>
        EnvironmentDepthSwapchainStateMETA = 1000291002,

        /// <summary>
        /// Struct is of type `XrEnvironmentDepthImageAcquireInfoMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_ENVIRONMENT_DEPTH_IMAGE_ACQUIRE_INFO_META`.
        /// Provided by `XR_META_environment_depth`.
        /// </summary>
        EnvironmentDepthImageAcquireInfoMETA = 1000291003,

        /// <summary>
        /// Struct is of type `XrEnvironmentDepthImageViewMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_ENVIRONMENT_DEPTH_IMAGE_VIEW_META`.
        /// Provided by `XR_META_environment_depth`.
        /// </summary>
        EnvironmentDepthImageViewMETA = 1000291004,

        /// <summary>
        /// Struct is of type `XrEnvironmentDepthImageMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_ENVIRONMENT_DEPTH_IMAGE_META`.
        /// Provided by `XR_META_environment_depth`.
        /// </summary>
        EnvironmentDepthImageMETA = 1000291005,

        /// <summary>
        /// Struct is of type `XrEnvironmentDepthHandRemovalSetInfoMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_ENVIRONMENT_DEPTH_HAND_REMOVAL_SET_INFO_META`.
        /// Provided by `XR_META_environment_depth`.
        /// </summary>
        EnvironmentDepthHandRemovalSetInfoMETA = 1000291006,

        /// <summary>
        /// Struct is of type `XrSystemEnvironmentDepthPropertiesMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_ENVIRONMENT_DEPTH_PROPERTIES_META`.
        /// Provided by `XR_META_environment_depth`.
        /// </summary>
        SystemEnvironmentDepthPropertiesMETA = 1000291007,

        /// <summary>
        /// Struct is of type `XrRenderModelCreateInfoEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_RENDER_MODEL_CREATE_INFO_EXT`.
        /// Provided by `XR_EXT_render_model`.
        /// </summary>
        RenderModelCreateInfoEXT = 1000300000,

        /// <summary>
        /// Struct is of type `XrRenderModelPropertiesGetInfoEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_RENDER_MODEL_PROPERTIES_GET_INFO_EXT`.
        /// Provided by `XR_EXT_render_model`.
        /// </summary>
        RenderModelPropertiesGetInfoEXT = 1000300001,

        /// <summary>
        /// Struct is of type `XrRenderModelPropertiesEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_RENDER_MODEL_PROPERTIES_EXT`.
        /// Provided by `XR_EXT_render_model`.
        /// </summary>
        RenderModelPropertiesEXT = 1000300002,

        /// <summary>
        /// Struct is of type `XrRenderModelSpaceCreateInfoEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_RENDER_MODEL_SPACE_CREATE_INFO_EXT`.
        /// Provided by `XR_EXT_render_model`.
        /// </summary>
        RenderModelSpaceCreateInfoEXT = 1000300003,

        /// <summary>
        /// Struct is of type `XrRenderModelStateGetInfoEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_RENDER_MODEL_STATE_GET_INFO_EXT`.
        /// Provided by `XR_EXT_render_model`.
        /// </summary>
        RenderModelStateGetInfoEXT = 1000300004,

        /// <summary>
        /// Struct is of type `XrRenderModelStateEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_RENDER_MODEL_STATE_EXT`.
        /// Provided by `XR_EXT_render_model`.
        /// </summary>
        RenderModelStateEXT = 1000300005,

        /// <summary>
        /// Struct is of type `XrRenderModelAssetCreateInfoEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_RENDER_MODEL_ASSET_CREATE_INFO_EXT`.
        /// Provided by `XR_EXT_render_model`.
        /// </summary>
        RenderModelAssetCreateInfoEXT = 1000300006,

        /// <summary>
        /// Struct is of type `XrRenderModelAssetDataGetInfoEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_RENDER_MODEL_ASSET_DATA_GET_INFO_EXT`.
        /// Provided by `XR_EXT_render_model`.
        /// </summary>
        RenderModelAssetDataGetInfoEXT = 1000300007,

        /// <summary>
        /// Struct is of type `XrRenderModelAssetDataEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_RENDER_MODEL_ASSET_DATA_EXT`.
        /// Provided by `XR_EXT_render_model`.
        /// </summary>
        RenderModelAssetDataEXT = 1000300008,

        /// <summary>
        /// Struct is of type `XrRenderModelAssetPropertiesGetInfoEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_RENDER_MODEL_ASSET_PROPERTIES_GET_INFO_EXT`.
        /// Provided by `XR_EXT_render_model`.
        /// </summary>
        RenderModelAssetPropertiesGetInfoEXT = 1000300009,

        /// <summary>
        /// Struct is of type `XrRenderModelAssetPropertiesEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_RENDER_MODEL_ASSET_PROPERTIES_EXT`.
        /// Provided by `XR_EXT_render_model`.
        /// </summary>
        RenderModelAssetPropertiesEXT = 1000300010,

        /// <summary>
        /// Struct is of type `XrInteractionRenderModelIdsEnumerateInfoEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_INTERACTION_RENDER_MODEL_IDS_ENUMERATE_INFO_EXT`.
        /// Provided by `XR_EXT_interaction_render_model`.
        /// </summary>
        InteractionRenderModelIdsEnumerateInfoEXT = 1000301000,

        /// <summary>
        /// Struct is of type `XrInteractionRenderModelSubactionPathInfoEXT.
        /// Equivalent to the OpenXR value `XR_TYPE_INTERACTION_RENDER_MODEL_SUBACTION_PATH_INFO_EXT`.
        /// Provided by `XR_EXT_interaction_render_model`.
        /// </summary>
        InteractionRenderModelSubactionPathInfoEXT = 1000301001,

        /// <summary>
        /// Struct is of type `XrEventDataInteractionRenderModelsChangedEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_INTERACTION_RENDER_MODELS_CHANGED_EXT`.
        /// Provided by `XR_EXT_interaction_render_model`.
        /// </summary>
        EventDataInteractionRenderModelsChangedEXT = 1000301002,

        /// <summary>
        /// Struct is of type `XrInteractionRenderModelTopLevelUserPathGetInfoEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_INTERACTION_RENDER_MODEL_TOP_LEVEL_USER_PATH_GET_INFO_EXT`.
        /// Provided by `XR_EXT_interaction_render_model`.
        /// </summary>
        InteractionRenderModelTopLevelUserPathGetInfoEXT = 1000301003,

        /// <summary>
        /// Struct is of type `XrPassthroughCreateInfoHTC`.
        /// Equivalent to the OpenXR value `XR_TYPE_PASSTHROUGH_CREATE_INFO_HTC`.
        /// Provided by `XR_HTC_passthrough`.
        /// </summary>
        PassthroughCreateInfoHTC = 1000317001,

        /// <summary>
        /// Struct is of type `XrPassthroughColorHTC`.
        /// Equivalent to the OpenXR value `XR_TYPE_PASSTHROUGH_COLOR_HTC`.
        /// Provided by `XR_HTC_passthrough`.
        /// </summary>
        PassthroughColorHTC = 1000317002,

        /// <summary>
        /// Struct is of type `XrPassthroughMeshTransformInfoHTC`.
        /// Equivalent to the OpenXR value `XR_TYPE_PASSTHROUGH_MESH_TRANSFORM_INFO_HTC`.
        /// Provided by `XR_HTC_passthrough`.
        /// </summary>
        PassthroughMeshTransformInfoHTC = 1000317003,

        /// <summary>
        /// Struct is of type `XrCompositionLayerPassthroughHTC`.
        /// Equivalent to the OpenXR value `XR_TYPE_COMPOSITION_LAYER_PASSTHROUGH_HTC`.
        /// Provided by `XR_HTC_passthrough`.
        /// </summary>
        CompositionLayerPassthroughHTC = 1000317004,

        /// <summary>
        /// Struct is of type `XrFoveationApplyInfoHTC`.
        /// Equivalent to the OpenXR value `XR_TYPE_FOVEATION_APPLY_INFO_HTC`.
        /// Provided by `XR_HTC_foveation`.
        /// </summary>
        FoveationApplyInfoHTC = 1000318000,

        /// <summary>
        /// Struct is of type `XrFoveationDynamicModeInfoHTC`.
        /// Equivalent to the OpenXR value `XR_TYPE_FOVEATION_DYNAMIC_MODE_INFO_HTC`.
        /// Provided by `XR_HTC_foveation`.
        /// </summary>
        FoveationDynamicModeInfoHTC = 1000318001,

        /// <summary>
        /// Struct is of type `XrFoveationCustomModeInfoHTC`.
        /// Equivalent to the OpenXR value `XR_TYPE_FOVEATION_CUSTOM_MODE_INFO_HTC`.
        /// Provided by `XR_HTC_foveation`.
        /// </summary>
        FoveationCustomModeInfoHTC = 1000318002,

        /// <summary>
        /// Struct is of type `XrSystemAnchorPropertiesHTC`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_ANCHOR_PROPERTIES_HTC`.
        /// Provided by `XR_HTC_anchor`.
        /// </summary>
        SystemAnchorPropertiesHTC = 1000319000,

        /// <summary>
        /// Struct is of type `XrSpatialAnchorCreateInfoHTC`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ANCHOR_CREATE_INFO_HTC`.
        /// Provided by `XR_HTC_anchor`.
        /// </summary>
        SpatialAnchorCreateInfoHTC = 1000319001,

        /// <summary>
        /// Struct is of type `XrSystemBodyTrackingPropertiesHTC`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_BODY_TRACKING_PROPERTIES_HTC`.
        /// Provided by `XR_HTC_body_tracking`.
        /// </summary>
        SystemBodyTrackingPropertiesHTC = 1000320000,

        /// <summary>
        /// Struct is of type `XrBodyTrackerCreateInfoHTC`.
        /// Equivalent to the OpenXR value `XR_TYPE_BODY_TRACKER_CREATE_INFO_HTC`.
        /// Provided by `XR_HTC_body_tracking`.
        /// </summary>
        BodyTrackerCreateInfoHTC = 1000320001,

        /// <summary>
        /// Struct is of type `XrBodyJointsLocateInfoHTC`.
        /// Equivalent to the OpenXR value `XR_TYPE_BODY_JOINTS_LOCATE_INFO_HTC`.
        /// Provided by `XR_HTC_body_tracking`.
        /// </summary>
        BodyJointsLocateInfoHTC = 1000320002,

        /// <summary>
        /// Struct is of type `XrBodyJointLocationsHTC`.
        /// Equivalent to the OpenXR value `XR_TYPE_BODY_JOINT_LOCATIONS_HTC`.
        /// Provided by `XR_HTC_body_tracking`.
        /// </summary>
        BodyJointLocationsHTC = 1000320003,

        /// <summary>
        /// Struct is of type `XrBodySkeletonHTC`.
        /// Equivalent to the OpenXR value `XR_TYPE_BODY_SKELETON_HTC`.
        /// Provided by `XR_HTC_body_tracking`.
        /// </summary>
        BodySkeletonHTC = 1000320004,

        /// <summary>
        /// Struct is of type `XrActiveActionSetPropertiesEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_ACTIVE_ACTION_SET_PRIORITIES_EXT`.
        /// Provided by `XR_EXT_active_action_set_priority`.
        /// </summary>
        ActiveActionSetPrioritiesEXT = 1000373000,

        /// <summary>
        /// Struct is of type `XrSystemForceFeedbackCurlPropertiesMNDX`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_FORCE_FEEDBACK_CURL_PROPERTIES_MNDX`.
        /// Provided by `XR_MNDX_force_feedback_curl`.
        /// </summary>
        SystemForceFeedbackCurlPropertiesMNDX = 1000375000,

        /// <summary>
        /// Struct is of type `XrForceFeedbackCurlApplyLocationsMNDX`.
        /// Equivalent to the OpenXR value `XR_TYPE_FORCE_FEEDBACK_CURL_APPLY_LOCATIONS_MNDX`.
        /// Provided by `XR_MNDX_force_feedback_curl`.
        /// </summary>
        ForceFeedbackCurlApplyLocationsMNDX = 1000375001,

        /// <summary>
        /// Struct is of type `XrBodyTrackerCreateInfoBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_BODY_TRACKER_CREATE_INFO_BD`.
        /// Provided by `XR_BD_body_tracking`.
        /// </summary>
        BodyTrackerCreateInfoBD = 1000385001,

        /// <summary>
        /// Struct is of type `XrBodyJointsLocateInfoBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_BODY_JOINTS_LOCATE_INFO_BD`.
        /// Provided by `XR_BD_body_tracking`.
        /// </summary>
        BodyJointsLocateInfoBD = 1000385002,

        /// <summary>
        /// Struct is of type `XrBodyJointLocationsBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_BODY_JOINT_LOCATIONS_BD`.
        /// Provided by `XR_BD_body_tracking`.
        /// </summary>
        BodyJointLocationsBD = 1000385003,

        /// <summary>
        /// Struct is of type `XrSystemBodyTrackingPropertiesBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_BODY_TRACKING_PROPERTIES_BD`.
        /// Provided by `XR_BD_body_tracking`.
        /// </summary>
        SystemBodyTrackingPropertiesBD = 1000385004,

        /// <summary>
        /// Struct is of type `XrSystemSpatialSensingPropertiesBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_SPATIAL_SENSING_PROPERTIES_BD`.
        /// Provided by `XR_BD_spatial_sensing`.
        /// </summary>
        SystemSpatialSensingPropertiesBD = 1000389000,

        /// <summary>
        /// Struct is of type `XrSpatialEntityComponentGetInfoBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ENTITY_COMPONENT_GET_INFO_BD`.
        /// Provided by `XR_BD_spatial_sensing`.
        /// </summary>
        SpatialEntityComponentGetInfoBD = 1000389001,

        /// <summary>
        /// Struct is of type `XrSpatialEntityLocationGetInfoBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ENTITY_LOCATION_GET_INFO_BD`.
        /// Provided by `XR_BD_spatial_sensing`.
        /// </summary>
        SpatialEntityLocationGetInfoBD = 1000389002,

        /// <summary>
        /// Struct is of type `XrSpatialEntityComponentDataLocationBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ENTITY_COMPONENT_DATA_LOCATION_BD`.
        /// Provided by `XR_BD_spatial_sensing`.
        /// </summary>
        SpatialEntityComponentDataLocationBD = 1000389003,

        /// <summary>
        /// Struct is of type `XrSpatialEntityComponentDataSemanticBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ENTITY_COMPONENT_DATA_SEMANTIC_BD`.
        /// Provided by `XR_BD_spatial_sensing`.
        /// </summary>
        SpatialEntityComponentDataSemanticBD = 1000389004,

        /// <summary>
        /// Struct is of type `XrSpatialEntityComponentDataBoundingBox2DBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ENTITY_COMPONENT_DATA_BOUNDING_BOX_2D_BD`.
        /// Provided by `XR_BD_spatial_sensing`.
        /// </summary>
        SpatialEntityComponentDataBoundingBox2DBD = 1000389005,

        /// <summary>
        /// Struct is of type `XrSpatialEntityComponentDataPolygonBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ENTITY_COMPONENT_DATA_POLYGON_BD`.
        /// Provided by `XR_BD_spatial_sensing`.
        /// </summary>
        SpatialEntityComponentDataPolygonBD = 1000389006,

        /// <summary>
        /// Struct is of type `XrSpatialEntityComponentDataBoundingBox3DBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ENTITY_COMPONENT_DATA_BOUNDING_BOX_3D_BD`.
        /// Provided by `XR_BD_spatial_sensing`.
        /// </summary>
        SpatialEntityComponentDataBoundingBox3DBD = 1000389007,

        /// <summary>
        /// Struct is of type `XrSpatialEntityComponentDataTriangleMeshBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ENTITY_COMPONENT_DATA_TRIANGLE_MESH_BD`.
        /// Provided by `XR_BD_spatial_sensing`.
        /// </summary>
        SpatialEntityComponentDataTriangleMeshBD = 1000389008,

        /// <summary>
        /// Struct is of type `XrSenseDataProviderCreateInfoBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SENSE_DATA_PROVIDER_CREATE_INFO_BD`.
        /// Provided by `XR_BD_spatial_sensing`.
        /// </summary>
        SenseDataProviderCreateInfoBD = 1000389009,

        /// <summary>
        /// Struct is of type `XrSenseDataProviderStartInfoBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SENSE_DATA_PROVIDER_START_INFO_BD`.
        /// Provided by `XR_BD_spatial_sensing`.
        /// </summary>
        SenseDataProviderStartInfoBD = 1000389010,

        /// <summary>
        /// Struct is of type `XrEventDataSenseDataProviderStateChangedBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_SENSE_DATA_PROVIDER_STATE_CHANGED_BD`.
        /// Provided by `XR_BD_spatial_sensing`.
        /// </summary>
        EventDataSenseDataProviderStateChangedBD = 1000389011,

        /// <summary>
        /// Struct is of type `XrEventDataSenseDataUpdatedBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_SENSE_DATA_UPDATED_BD`.
        /// Provided by `XR_BD_spatial_sensing`.
        /// </summary>
        EventDataSenseDataUpdatedBD = 1000389012,

        /// <summary>
        /// Struct is of type `XrSenseDataQueryInfoBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SENSE_DATA_QUERY_INFO_BD`.
        /// Provided by `XR_BD_spatial_sensing`.
        /// </summary>
        SenseDataQueryInfoBD = 1000389013,

        /// <summary>
        /// Struct is of type `XrSenseDataQueryCompletionBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SENSE_DATA_QUERY_COMPLETION_BD`.
        /// Provided by `XR_BD_spatial_sensing`.
        /// </summary>
        SenseDataQueryCompletionBD = 1000389014,

        /// <summary>
        /// Struct is of type `XrSenseDataFilterUuidBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SENSE_DATA_FILTER_UUID_BD`.
        /// Provided by `XR_BD_spatial_sensing`.
        /// </summary>
        SenseDataFilterUuidBD = 1000389015,

        /// <summary>
        /// Struct is of type `XrSenseDataFilterSemanticBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SENSE_DATA_FILTER_SEMANTIC_BD`.
        /// Provided by `XR_BD_spatial_sensing`.
        /// </summary>
        SenseDataFilterSemanticBD = 1000389016,

        /// <summary>
        /// Struct is of type `XrQueriedSenseDataGetInfoBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_QUERIED_SENSE_DATA_GET_INFO_BD`.
        /// Provided by `XR_BD_spatial_sensing`.
        /// </summary>
        QueriedSenseDataGetInfoBD = 1000389017,

        /// <summary>
        /// Struct is of type `XrQueriedSenseDataBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_QUERIED_SENSE_DATA_BD`.
        /// Provided by `XR_BD_spatial_sensing`.
        /// </summary>
        QueriedSenseDataBD = 1000389018,

        /// <summary>
        /// Struct is of type `XrSpatialEntityStateBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ENTITY_STATE_BD`.
        /// Provided by `XR_BD_spatial_sensing`.
        /// </summary>
        SpatialEntityStateBD = 1000389019,

        /// <summary>
        /// Struct is of type `XrSpatialEntityAnchorCreateInfoBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ENTITY_ANCHOR_CREATE_INFO_BD`.
        /// Provided by `XR_BD_spatial_sensing`.
        /// </summary>
        SpatialEntityAnchorCreateInfoBD = 1000389020,

        /// <summary>
        /// Struct is of type `XrAnchorSpaceCreateInfoBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_ANCHOR_SPACE_CREATE_INFO_BD`.
        /// Provided by `XR_BD_spatial_sensing`.
        /// </summary>
        AnchorSpaceCreateInfoBD = 1000389021,

        /// <summary>
        /// Struct is of type `XrSystemSpatialAnchorPropertiesBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_SPATIAL_ANCHOR_PROPERTIES_BD`.
        /// Provided by `XR_BD_spatial_anchor`.
        /// </summary>
        SystemSpatialAnchorPropertiesBD = 1000390000,

        /// <summary>
        /// Struct is of type `XrSpatialAnchorCreateInfoBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ANCHOR_CREATE_INFO_BD`.
        /// Provided by `XR_BD_spatial_anchor`.
        /// </summary>
        SpatialAnchorCreateInfoBD = 1000390001,

        /// <summary>
        /// Struct is of type `XrSpatialAnchorCreateCompletionBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ANCHOR_CREATE_COMPLETION_BD`.
        /// Provided by `XR_BD_spatial_anchor`.
        /// </summary>
        SpatialAnchorCreateCompletionBD = 1000390002,

        /// <summary>
        /// Struct is of type `XrSpatialAnchorPersistInfoBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ANCHOR_PERSIST_INFO_BD`.
        /// Provided by `XR_BD_spatial_anchor`.
        /// </summary>
        SpatialAnchorPersistInfoBD = 1000390003,

        /// <summary>
        /// Struct is of type `XrSpatialAnchorUnpersistInfoBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ANCHOR_UNPERSIST_INFO_BD`.
        /// Provided by `XR_BD_spatial_anchor`.
        /// </summary>
        SpatialAnchorUnpersistInfoBD = 1000390004,

        /// <summary>
        /// Struct is of type `XrSystemSpatialAnchorSharingPropertiesBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_SPATIAL_ANCHOR_SHARING_PROPERTIES_BD`.
        /// Provided by `XR_BD_spatial_anchor_sharing`.
        /// </summary>
        SystemSpatialAnchorSharingPropertiesBD = 1000391000,

        /// <summary>
        /// Struct is of type `XrSpatialAnchorShareInfoBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ANCHOR_SHARE_INFO_BD`.
        /// Provided by `XR_BD_spatial_anchor_sharing`.
        /// </summary>
        SpatialAnchorShareInfoBD = 1000391001,

        /// <summary>
        /// Struct is of type `XrSharedSpatialAnchorDownloadInfoBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SHARED_SPATIAL_ANCHOR_DOWNLOAD_INFO_BD`.
        /// Provided by `XR_BD_spatial_anchor_sharing`.
        /// </summary>
        SharedSpatialAnchorDownloadInfoBD = 1000391002,

        /// <summary>
        /// Struct is of type `XrSystemSpatialScenePropertiesBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_SPATIAL_SCENE_PROPERTIES_BD`.
        /// Provided by `XR_BD_spatial_scene`.
        /// </summary>
        SystemSpatialScenePropertiesBD = 1000392000,

        /// <summary>
        /// Struct is of type `XrSceneCaptureInfoBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SCENE_CAPTURE_INFO_BD`.
        /// Provided by `XR_BD_spatial_scene`.
        /// </summary>
        SceneCaptureInfoBD = 1000392001,

        /// <summary>
        /// Struct is of type `XrSystemSpatialMeshPropertiesBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_SPATIAL_MESH_PROPERTIES_BD`.
        /// Provided by `XR_BD_spatial_mesh`.
        /// </summary>
        SystemSpatialMeshPropertiesBD = 1000393000,

        /// <summary>
        /// Struct is of type `XrSenseDataProviderCreateInfoSpatialMeshBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SENSE_DATA_PROVIDER_CREATE_INFO_SPATIAL_MESH_BD`.
        /// Provided by `XR_BD_spatial_mesh`.
        /// </summary>
        SenseDataProviderCreateInfoSpatialMeshBD = 1000393001,

        /// <summary>
        /// Struct is of type `XrFuturePollResultProgressBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_FUTURE_POLL_RESULT_PROGRESS_BD`.
        /// Provided by `XR_BD_future_progress`.
        /// </summary>
        FuturePollResultProgressBD = 1000394001,

        /// <summary>
        /// Struct is of type `XrSystemSpatialPlanePropertiesBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_SPATIAL_PLANE_PROPERTIES_BD`.
        /// Provided by `XR_BD_spatial_plane`.
        /// </summary>
        SystemSpatialPlanePropertiesBD = 1000396000,

        /// <summary>
        /// Struct is of type `XrSpatialEntityComponentDataPlaneOrientationBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ENTITY_COMPONENT_DATA_PLANE_ORIENTATION_BD`.
        /// Provided by `XR_BD_spatial_plane`.
        /// </summary>
        SpatialEntityComponentDataPlaneOrientationBD = 1000396001,

        /// <summary>
        /// Struct is of type `XrSenseDataFilterPlaneOrientationBD`.
        /// Equivalent to the OpenXR value `XR_TYPE_SENSE_DATA_FILTER_PLANE_ORIENTATION_BD`.
        /// Provided by `XR_BD_spatial_plane`.
        /// </summary>
        SenseDataFilterPlaneOrientationBD = 1000396002,

        /// <summary>
        /// Struct is of type `XrHandTrackingDataSourceInfoEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_HAND_TRACKING_DATA_SOURCE_INFO_EXT`.
        /// Provided by `XR_EXT_hand_tracking_data_source`.
        /// </summary>
        HandTrackingDataSourceInfoEXT = 1000428000,

        /// <summary>
        /// Struct is of type `XrHandTrackingDataSourceStateEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_HAND_TRACKING_DATA_SOURCE_STATE_EXT`.
        /// Provided by `XR_EXT_hand_tracking_data_source`.
        /// </summary>
        HandTrackingDataSourceStateEXT = 1000428001,

        /// <summary>
        /// Struct is of type `XrPlaneDetectorCreateInfoEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_PLANE_DETECTOR_CREATE_INFO_EXT`.
        /// Provided by `XR_EXT_plane_detection`.
        /// </summary>
        PlaneDetectorCreateInfoEXT = 1000429001,

        /// <summary>
        /// Struct is of type `XrPlaneDetectorBeginInfoEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_PLANE_DETECTOR_BEGIN_INFO_EXT`.
        /// Provided by `XR_EXT_plane_detection`.
        /// </summary>
        PlaneDetectorBeginInfoEXT = 1000429002,

        /// <summary>
        /// Struct is of type `XrPlaneDetectorGetInfoEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_PLANE_DETECTOR_GET_INFO_EXT`.
        /// Provided by `XR_EXT_plane_detection`.
        /// </summary>
        PlaneDetectorGetInfoEXT = 1000429003,

        /// <summary>
        /// Struct is of type `XrPlaneDetectorLocationsEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_PLANE_DETECTOR_LOCATIONS_EXT`.
        /// Provided by `XR_EXT_plane_detection`.
        /// </summary>
        PlaneDetectorLocationsEXT = 1000429004,

        /// <summary>
        /// Struct is of type `XrPlaneDetectorLocationEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_PLANE_DETECTOR_LOCATION_EXT`.
        /// Provided by `XR_EXT_plane_detection`.
        /// </summary>
        PlaneDetectorLocationEXT = 1000429005,

        /// <summary>
        /// Struct is of type `XrPlaneDetectorPolygonBufferEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_PLANE_DETECTOR_POLYGON_BUFFER_EXT`.
        /// Provided by `XR_EXT_plane_detection`.
        /// </summary>
        PlaneDetectorPolygonBufferEXT = 1000429006,

        /// <summary>
        /// Struct is of type `XrSystemPlaneDetectionPropertiesEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_PLANE_DETECTION_PROPERTIES_EXT`.
        /// Provided by `XR_EXT_plane_detection`.
        /// </summary>
        SystemPlaneDetectionPropertiesEXT = 1000429007,

        /// <summary>
        /// Struct is of type <see cref="XrFutureCancelInfoEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_FUTURE_CANCEL_INFO_EXT`.
        /// Provided by `XR_EXT_future`.
        /// </summary>
        FutureCancelInfoEXT = 1000469000,

        /// <summary>
        /// Struct is of type <see cref="XrFuturePollInfoEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_FUTURE_POLL_INFO_EXT`.
        /// Provided by `XR_EXT_future`.
        /// </summary>
        FuturePollInfoEXT = 1000469001,

        /// <summary>
        /// Struct is of type <see cref="XrFutureCompletionEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_FUTURE_COMPLETION_EXT`.
        /// Provided by `XR_EXT_future`.
        /// </summary>
        FutureCompletionEXT = 1000469002,

        /// <summary>
        /// Struct is of type <see cref="XrFuturePollResultEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_FUTURE_POLL_RESULT_EXT`.
        /// Provided by `XR_EXT_future`.
        /// </summary>
        FuturePollResultEXT = 1000469003,

        /// <summary>
        /// Struct is of type `XrEventDataUserPresenceChangedEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_USER_PRESENCE_CHANGED_EXT`.
        /// Provided by `XR_EXT_user_presence`.
        /// </summary>
        EventDataUserPresenceChangedEXT = 1000470000,

        /// <summary>
        /// Struct is of type `XrSystemUserPresencePropertiesEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_USER_PRESENCE_PROPERTIES_EXT`.
        /// Provided by `XR_EXT_user_presence`.
        /// </summary>
        SystemUserPresencePropertiesEXT = 1000470001,

        /// <summary>
        /// Struct is of type `XrSystemNotificationsSetInfoML`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_NOTIFICATIONS_SET_INFO_ML`.
        /// Provided by `XR_ML_system_notifications`.
        /// </summary>
        SystemNotificationsSetInfoML = 1000473000,

        /// <summary>
        /// Struct is of type `XrWorldMeshDetectorCreateInfoML`.
        /// Equivalent to the OpenXR value `XR_TYPE_WORLD_MESH_DETECTOR_CREATE_INFO_ML`.
        /// Provided by `XR_ML_world_mesh_detection`.
        /// </summary>
        WorldMeshDetectorCreateInfoML = 1000474001,

        /// <summary>
        /// Struct is of type `XrWorldMeshStateRequestInfoML`.
        /// Equivalent to the OpenXR value `XR_TYPE_WORLD_MESH_STATE_REQUEST_INFO_ML`.
        /// Provided by `XR_ML_world_mesh_detection`.
        /// </summary>
        WorldMeshStateRequestInfoML = 1000474002,

        /// <summary>
        /// Struct is of type `XrWorldMeshBlockStateML`.
        /// Equivalent to the OpenXR value `XR_TYPE_WORLD_MESH_BLOCK_STATE_ML`.
        /// Provided by `XR_ML_world_mesh_detection`.
        /// </summary>
        WorldMeshBlockStateML = 1000474003,

        /// <summary>
        /// Struct is of type `XrWorldMeshStateRequestCompletionML`.
        /// Equivalent to the OpenXR value `XR_TYPE_WORLD_MESH_STATE_REQUEST_COMPLETION_ML`.
        /// Provided by `XR_ML_world_mesh_detection`.
        /// </summary>
        WorldMeshStateRequestCompletionML = 1000474004,

        /// <summary>
        /// Struct is of type `XrWorldMeshBufferRecommendedSizeInfoML`.
        /// Equivalent to the OpenXR value `XR_TYPE_WORLD_MESH_BUFFER_RECOMMENDED_SIZE_INFO_ML`.
        /// Provided by `XR_ML_world_mesh_detection`.
        /// </summary>
        WorldMeshBufferRecommendedSizeInfoML = 1000474005,

        /// <summary>
        /// Struct is of type `XrWorldMeshBufferSizeML`.
        /// Equivalent to the OpenXR value `XR_TYPE_WORLD_MESH_BUFFER_SIZE_ML`.
        /// Provided by `XR_ML_world_mesh_detection`.
        /// </summary>
        WorldMeshBufferSizeML = 1000474006,

        /// <summary>
        /// Struct is of type `XrWorldMeshBufferML`.
        /// Equivalent to the OpenXR value `XR_TYPE_WORLD_MESH_BUFFER_ML`.
        /// Provided by `XR_ML_world_mesh_detection`.
        /// </summary>
        WorldMeshBufferML = 1000474007,

        /// <summary>
        /// Struct is of type `XrWorldMeshBlockRequestML`.
        /// Equivalent to the OpenXR value `XR_TYPE_WORLD_MESH_BLOCK_REQUEST_ML`.
        /// Provided by `XR_ML_world_mesh_detection`.
        /// </summary>
        WorldMeshBlockRequestML = 1000474008,

        /// <summary>
        /// Struct is of type `XrWorldMeshGetInfoML`.
        /// Equivalent to the OpenXR value `XR_TYPE_WORLD_MESH_GET_INFO_ML`.
        /// Provided by `XR_ML_world_mesh_detection`.
        /// </summary>
        WorldMeshGetInfoML = 1000474009,

        /// <summary>
        /// Struct is of type `XrWorldMeshBlockML`.
        /// Equivalent to the OpenXR value `XR_TYPE_WORLD_MESH_BLOCK_ML`.
        /// Provided by `XR_ML_world_mesh_detection`.
        /// </summary>
        WorldMeshBlockML = 1000474010,

        /// <summary>
        /// Struct is of type `XrWorldMeshRequestCompletionML`.
        /// Equivalent to the OpenXR value `XR_TYPE_WORLD_MESH_REQUEST_COMPLETION_ML`.
        /// Provided by `XR_ML_world_mesh_detection`.
        /// </summary>
        WorldMeshRequestCompletionML = 1000474011,

        /// <summary>
        /// Struct is of type `XrWorldMeshRequestCompletionInfoML`.
        /// Equivalent to the OpenXR value `XR_TYPE_WORLD_MESH_REQUEST_COMPLETION_INFO_ML`.
        /// Provided by `XR_ML_world_mesh_detection`.
        /// </summary>
        WorldMeshRequestCompletionInfoML = 1000474012,

        /// <summary>
        /// Struct is of type `XrSystemFacialExpressionPropertiesML`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_FACIAL_EXPRESSION_PROPERTIES_ML`.
        /// Provided by `XR_ML_facial_expression`.
        /// </summary>
        SystemFacialExpressionPropertiesML = 1000482004,

        /// <summary>
        /// Struct is of type `XrFacialExpressionClientCreateInfoML`.
        /// Equivalent to the OpenXR value `XR_TYPE_FACIAL_EXPRESSION_CLIENT_CREATE_INFO_ML`.
        /// Provided by `XR_ML_facial_expression`.
        /// </summary>
        FacialExpressionClientCreateInfoML = 1000482005,

        /// <summary>
        /// Struct is of type `XrFacialExpressionBlendShapeGetInfoML`.
        /// Equivalent to the OpenXR value `XR_TYPE_FACIAL_EXPRESSION_BLEND_SHAPE_GET_INFO_ML`.
        /// Provided by `XR_ML_facial_expression`.
        /// </summary>
        FacialExpressionBlendShapeGetInfoML = 1000482006,

        /// <summary>
        /// Struct is of type `XrFacialExpressionBlendShapePropertiesML`.
        /// Equivalent to the OpenXR value `XR_TYPE_FACIAL_EXPRESSION_BLEND_SHAPE_PROPERTIES_ML`.
        /// Provided by `XR_ML_facial_expression`.
        /// </summary>
        FacialExpressionBlendShapePropertiesML = 1000482007,

        /// <summary>
        /// Struct is of type `XrSystemSimultaneousHandsAndControllersPropertiesMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_SIMULTANEOUS_HANDS_AND_CONTROLLERS_PROPERTIES_META`.
        /// Provided by `XR_META_simultaneous_hands_and_controllers`.
        /// </summary>
        SystemSimultaneousHandsAndControllersPropertiesMETA = 1000532001,

        /// <summary>
        /// Struct is of type `XrSimultaneousHandsAndControllersTrackingResumeInfoMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_SIMULTANEOUS_HANDS_AND_CONTROLLERS_TRACKING_RESUME_INFO_META`.
        /// Provided by `XR_META_simultaneous_hands_and_controllers`.
        /// </summary>
        SimultaneousHandsAndControllersTrackingResumeInfoMETA = 1000532002,

        /// <summary>
        /// Struct is of type `XrSimultaneousHandsAndControllersTrackingPauseInfoMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_SIMULTANEOUS_HANDS_AND_CONTROLLERS_TRACKING_PAUSE_INFO_META`.
        /// Provided by `XR_META_simultaneous_hands_and_controllers`.
        /// </summary>
        SimultaneousHandsAndControllersTrackingPauseInfoMETA = 1000532003,

        /// <summary>
        /// The struct is of type `XrColocationDiscoveryStartInfoMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_COLOCATION_DISCOVERY_START_INFO_META`.
        /// Provided by `XR_META_colocation_discovery`.
        /// </summary>
        ColocationDiscoveryStartInfoMETA = 1000571010,

        /// <summary>
        /// The struct is of type `XrColocationDiscoveryStopInfoMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_COLOCATION_DISCOVERY_STOP_INFO_META`.
        /// Provided by `XR_META_colocation_discovery`.
        /// </summary>
        ColocationDiscoveryStopInfoMETA = 1000571011,

        /// <summary>
        /// The struct is of type `XrColocationAdvertisementStartInfoMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_COLOCATION_ADVERTISEMENT_START_INFO_META`.
        /// Provided by `XR_META_colocation_discovery`.
        /// </summary>
        ColocationAdvertisementStartInfoMETA = 1000571012,

        /// <summary>
        /// The struct is of type `XrColocationAdvertisementStopInfoMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_COLOCATION_ADVERTISEMENT_STOP_INFO_META`.
        /// Provided by `XR_META_colocation_discovery`.
        /// </summary>
        ColocationAdvertisementStopInfoMETA = 1000571013,

        /// <summary>
        /// The struct is of type `XrEventDataStartColocationAdvertisementCompleteMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_START_COLOCATION_ADVERTISEMENT_COMPLETE_META`.
        /// Provided by `XR_META_colocation_discovery`.
        /// </summary>
        EventDataStartColocationAdvertisementCompleteMETA = 1000571020,

        /// <summary>
        /// The struct is of type `XrEventDataStopColocationAdvertisementCompleteMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_STOP_COLOCATION_ADVERTISEMENT_COMPLETE_META`.
        /// Provided by `XR_META_colocation_discovery`.
        /// </summary>
        EventDataStopColocationAdvertisementCompleteMETA = 1000571021,

        /// <summary>
        /// The struct is of type `XrEventDataColocationAdvertisementCompleteMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_COLOCATION_ADVERTISEMENT_COMPLETE_META`.
        /// Provided by `XR_META_colocation_discovery`.
        /// </summary>
        EventDataColocationAdvertisementCompleteMETA = 1000571022,

        /// <summary>
        /// The struct is of type `XrEventDataStartColocationDiscoveryCompleteMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_START_COLOCATION_DISCOVERY_COMPLETE_META`.
        /// Provided by `XR_META_colocation_discovery`.
        /// </summary>
        EventDataStartColocationDiscoveryCompleteMETA = 1000571023,

        /// <summary>
        /// The struct is of type `XrEventDataColocationDiscoveryResultMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_COLOCATION_DISCOVERY_RESULT_META`.
        /// Provided by `XR_META_colocation_discovery`.
        /// </summary>
        EventDataColocationDiscoveryResultMETA = 1000571024,

        /// <summary>
        /// The struct is of type `XrEventDataColocationDiscoveryCompleteMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_COLOCATION_DISCOVERY_COMPLETE_META`.
        /// Provided by `XR_META_colocation_discovery`.
        /// </summary>
        EventDataColocationDiscoveryCompleteMETA = 1000571025,

        /// <summary>
        /// The struct is of type `XrEventDataStopColocationDiscoveryCompleteMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_STOP_COLOCATION_DISCOVERY_COMPLETE_META`.
        /// Provided by `XR_META_colocation_discovery`.
        /// </summary>
        EventDataStopColocationDiscoveryCompleteMETA = 1000571026,

        /// <summary>
        /// The struct is of type `XrSystemColocationDiscoveryPropertiesMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_COLOCATION_DISCOVERY_PROPERTIES_META`.
        /// Provided by `XR_META_colocation_discovery`.
        /// </summary>
        SystemColocationDiscoveryPropertiesMETA = 1000571030,

        /// <summary>
        /// The struct is of type `XrShareSpacesRecipientGroupsMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_SHARE_SPACES_RECIPIENT_GROUPS_META`.
        /// Provided by `XR_META_spatial_entity_group_sharing`.
        /// </summary>
        ShareSpacesRecipientGroupsMETA = 1000572000,

        /// <summary>
        /// The struct is of type `XrSpaceGroupUuidFilterInfoMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPACE_GROUP_UUID_FILTER_INFO_META`.
        /// Provided by `XR_META_spatial_entity_group_sharing`.
        /// </summary>
        SpaceGroupUuidFilterInfoMETA = 1000572001,

        /// <summary>
        /// The struct is of type `XrSystemSpatialEntityGroupSharingPropertiesMETA`.
        /// Equivalent to the OpenXR value `XR_TYPE_SYSTEM_SPATIAL_ENTITY_GROUP_SHARING_PROPERTIES_META`.
        /// Provided by `XR_META_spatial_entity_group_sharing`.
        /// </summary>
        SystemSpatialEntityGroupSharingPropertiesMETA = 1000572100,

        /// <summary>
        /// The struct is of type <see cref="XrSpatialCapabilityComponentTypesEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_CAPABILITY_COMPONENT_TYPES_EXT`.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        SpatialCapabilityComponentTypesEXT = 1000740000,

        /// <summary>
        /// The struct is of type <see cref="XrSpatialContextCreateInfoEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_CONTEXT_CREATE_INFO_EXT`.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        SpatialContextCreateInfoEXT = 1000740001,

        /// <summary>
        /// The struct is of type <see cref="XrCreateSpatialContextCompletionEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_CREATE_SPATIAL_CONTEXT_COMPLETION_EXT`.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        CreateSpatialContextCompletionEXT = 1000740002,

        /// <summary>
        /// The struct is of type <see cref="XrSpatialDiscoverySnapshotCreateInfoEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_DISCOVERY_SNAPSHOT_CREATE_INFO_EXT`.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        SpatialDiscoverySnapshotCreateInfoEXT = 1000740003,

        /// <summary>
        /// The struct is of type <see cref="XrCreateSpatialDiscoverySnapshotCompletionInfoEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_CREATE_SPATIAL_DISCOVERY_SNAPSHOT_COMPLETION_INFO_EXT`.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        CreateSpatialDiscoverySnapshotCompletionInfoEXT = 1000740004,

        /// <summary>
        /// The struct is of type <see cref="XrCreateSpatialDiscoverySnapshotCompletionEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_CREATE_SPATIAL_DISCOVERY_SNAPSHOT_COMPLETION_EXT`.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        CreateSpatialDiscoverySnapshotCompletionEXT = 1000740005,

        /// <summary>
        /// The struct is of type <see cref="XrSpatialComponentDataQueryConditionEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_COMPONENT_DATA_QUERY_CONDITION_EXT`.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        SpatialComponentDataQueryConditionEXT = 1000740006,

        /// <summary>
        /// The struct is of type <see cref="XrSpatialComponentDataQueryResultEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_COMPONENT_DATA_QUERY_RESULT_EXT`.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        SpatialComponentDataQueryResultEXT = 1000740007,

        /// <summary>
        /// The struct is of type <see cref="XrSpatialBufferGetInfoEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_BUFFER_GET_INFO_EXT`.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        SpatialBufferGetInfoEXT = 1000740008,

        /// <summary>
        /// The struct is of type <see cref="XrSpatialComponentBounded2DListEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_COMPONENT_BOUNDED_2D_LIST_EXT`.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        SpatialComponentBounded2DListEXT = 1000740009,

        /// <summary>
        /// The struct is of type <see cref="XrSpatialComponentBounded3DListEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_COMPONENT_BOUNDED_3D_LIST_EXT`.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        SpatialComponentBounded3DListEXT = 1000740010,

        /// <summary>
        /// The struct is of type <see cref="XrSpatialComponentParentListEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_COMPONENT_PARENT_LIST_EXT`.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        SpatialComponentParentListEXT = 1000740011,

        /// <summary>
        /// The struct is of type <see cref="XrSpatialComponentMesh3DListEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_COMPONENT_MESH_3D_LIST_EXT`.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        SpatialComponentMesh3DListEXT = 1000740012,

        /// <summary>
        /// The struct is of type <see cref="XrSpatialEntityFromIdCreateInfoEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ENTITY_FROM_ID_CREATE_INFO_EXT`.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        SpatialEntityFromIdCreateInfoEXT = 1000740013,

        /// <summary>
        /// The struct is of type <see cref="XrSpatialUpdateSnapshotCreateInfoEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_UPDATE_SNAPSHOT_CREATE_INFO_EXT`.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        SpatialUpdateSnapshotCreateInfoEXT = 1000740014,

        /// <summary>
        /// The struct is of type <see cref="XrEventDataSpatialDiscoveryRecommendedEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_EVENT_DATA_SPATIAL_DISCOVERY_RECOMMENDED_EXT`.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        EventDataSpatialDiscoveryRecommendedEXT = 1000740015,

        /// <summary>
        /// The struct is of type <see cref="XrSpatialFilterTrackingStateEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_FILTER_TRACKING_STATE_EXT`.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        SpatialFilterTrackingStateEXT = 1000740016,

        /// <summary>
        /// The struct is of type <see cref="XrSpatialCapabilityConfigurationPlaneTrackingEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_CAPABILITY_CONFIGURATION_PLANE_TRACKING_EXT`.
        /// Provided by `XR_EXT_spatial_plane_tracking`.
        /// </summary>
        SpatialCapabilityConfigurationPlaneTrackingEXT = 1000741000,

        /// <summary>
        /// The struct is of type <see cref="XrSpatialComponentPlaneAlignmentListEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_COMPONENT_PLANE_ALIGNMENT_LIST_EXT`.
        /// Provided by `XR_EXT_spatial_plane_tracking`.
        /// </summary>
        SpatialComponentPlaneAlignmentListEXT = 1000741001,

        /// <summary>
        /// The struct is of type <see cref="XrSpatialComponentMesh2DListEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_COMPONENT_MESH_2D_LIST_EXT`.
        /// Provided by `XR_EXT_spatial_plane_tracking`.
        /// </summary>
        SpatialComponentMesh2DListEXT = 1000741002,

        /// <summary>
        /// The struct is of type <see cref="XrSpatialComponentPolygon2DListEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_COMPONENT_POLYGON_2D_LIST_EXT`.
        /// Provided by `XR_EXT_spatial_plane_tracking`.
        /// </summary>
        SpatialComponentPolygon2DListEXT = 1000741003,

        /// <summary>
        /// The struct is of type <see cref="XrSpatialComponentPlaneSemanticLabelListEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_COMPONENT_PLANE_SEMANTIC_LABEL_LIST_EXT`.
        /// Provided by `XR_EXT_spatial_plane_tracking`.
        /// </summary>
        SpatialComponentPlaneSemanticLabelListEXT = 1000741004,

        /// <summary>
        /// The struct is of type <see cref="XrSpatialCapabilityConfigurationQrCodeEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_CAPABILITY_CONFIGURATION_QR_CODE_EXT`.
        /// Provided by `XR_EXT_spatial_marker_tracking`.
        /// </summary>
        SpatialCapabilityConfigurationQrCodeEXT = 1000743000,

        /// <summary>
        /// The struct is of type <see cref="XrSpatialCapabilityConfigurationMicroQrCodeEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_CAPABILITY_CONFIGURATION_MICRO_QR_CODE_EXT`.
        /// Provided by `XR_EXT_spatial_marker_tracking`.
        /// </summary>
        SpatialCapabilityConfigurationMicroQrCodeEXT = 1000743001,

        /// <summary>
        /// The struct is of type <see cref="XrSpatialCapabilityConfigurationArucoMarkerEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_CAPABILITY_CONFIGURATION_ARUCO_MARKER_EXT`.
        /// Provided by `XR_EXT_spatial_marker_tracking`.
        /// </summary>
        SpatialCapabilityConfigurationArucoMarkerEXT = 1000743002,

        /// <summary>
        /// The struct is of type <see cref="XrSpatialCapabilityConfigurationAprilTagEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_CAPABILITY_CONFIGURATION_APRIL_TAG_EXT`.
        /// Provided by `XR_EXT_spatial_marker_tracking`.
        /// </summary>
        SpatialCapabilityConfigurationAprilTagEXT = 1000743003,

        /// <summary>
        /// The struct is of type <see cref="XrSpatialMarkerSizeEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_MARKER_SIZE_EXT`.
        /// Provided by `XR_EXT_spatial_marker_tracking`.
        /// </summary>
        SpatialMarkerSizeEXT = 1000743004,

        /// <summary>
        /// The struct is of type <see cref="XrSpatialMarkerStaticOptimizationEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_MARKER_STATIC_OPTIMIZATION_EXT`.
        /// Provided by `XR_EXT_spatial_marker_tracking`.
        /// </summary>
        SpatialMarkerStaticOptimizationEXT = 1000743005,

        /// <summary>
        /// The struct is of type <see cref="XrSpatialComponentMarkerListEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_COMPONENT_MARKER_LIST_EXT`.
        /// Provided by `XR_EXT_spatial_marker_tracking`.
        /// </summary>
        SpatialComponentMarkerListEXT = 1000743006,

        /// <summary>
        /// The struct is of type <see cref="XrSpatialCapabilityConfigurationAnchorEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_CAPABILITY_CONFIGURATION_ANCHOR_EXT`.
        /// Provided by `XR_EXT_spatial_anchor`.
        /// </summary>
        SpatialCapabilityConfigurationAnchorEXT = 1000762000,

        /// <summary>
        /// The struct is of type <see cref="XrSpatialComponentAnchorListEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_COMPONENT_ANCHOR_LIST_EXT`.
        /// Provided by `XR_EXT_spatial_anchor`.
        /// </summary>
        SpatialComponentAnchorListEXT = 1000762001,

        /// <summary>
        /// The struct is of type <see cref="XrSpatialAnchorCreateInfoEXT"/>.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ANCHOR_CREATE_INFO_EXT`.
        /// Provided by `XR_EXT_spatial_anchor`.
        /// </summary>
        SpatialAnchorCreateInfoEXT = 1000762002,

        /// <summary>
        /// The struct is of type `XrSpatialPersistenceContextCreateInfoEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_PERSISTENCE_CONTEXT_CREATE_INFO_EXT`.
        /// Provided by `XR_EXT_spatial_persistence`.
        /// </summary>
        SpatialPersistenceContextCreateInfoEXT = 1000763000,

        /// <summary>
        /// The struct is of type `XrCreateSpatialPersistenceContextCompletionEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_CREATE_SPATIAL_PERSISTENCE_CONTEXT_COMPLETION_EXT`.
        /// Provided by `XR_EXT_spatial_persistence`.
        /// </summary>
        CreateSpatialPersistenceContextCompletionEXT = 1000763001,

        /// <summary>
        /// The struct is of type `XrSpatialContextPersistenceConfigEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_CONTEXT_PERSISTENCE_CONFIG_EXT`.
        /// Provided by `XR_EXT_spatial_persistence`.
        /// </summary>
        SpatialContextPersistenceConfigEXT = 1000763002,

        /// <summary>
        /// The struct is of type `XrSpatialDiscoveryPersistenceUuidFilterEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_DISCOVERY_PERSISTENCE_UUID_FILTER_EXT`.
        /// Provided by `XR_EXT_spatial_persistence`.
        /// </summary>
        SpatialDiscoveryPersistenceUuidFilterEXT = 1000763003,

        /// <summary>
        /// The struct is of type `XrSpatialComponentPersistenceListEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_COMPONENT_PERSISTENCE_LIST_EXT`.
        /// Provided by `XR_EXT_spatial_persistence`.
        /// </summary>
        SpatialComponentPersistenceListEXT = 1000763004,

        /// <summary>
        /// The struct is of type `XrSpatialEntityPersistInfoEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ENTITY_PERSIST_INFO_EXT`.
        /// Provided by `XR_EXT_spatial_persistence_operations`.
        /// </summary>
        SpatialEntityPersistInfoEXT = 1000781000,

        /// <summary>
        /// The struct is of type `XrPersistSpatialEntityCompletionEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_PERSIST_SPATIAL_ENTITY_COMPLETION_EXT`.
        /// Provided by `XR_EXT_spatial_persistence_operations`.
        /// </summary>
        PersistSpatialEntityCompletionEXT = 1000781001,

        /// <summary>
        /// The struct is of type `XrSpatialEntityUnpersistInfoEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPATIAL_ENTITY_UNPERSIST_INFO_EXT`.
        /// Provided by `XR_EXT_spatial_persistence_operations`.
        /// </summary>
        SpatialEntityUnpersistInfoEXT = 1000781002,

        /// <summary>
        /// The struct is of type `XrUnpersistSpatialEntityCompletionEXT`.
        /// Equivalent to the OpenXR value `XR_TYPE_UNPERSIST_SPATIAL_ENTITY_COMPLETION_EXT`.
        /// Provided by `XR_EXT_spatial_persistence_operations`.
        /// </summary>
        UnpersistSpatialEntityCompletionEXT = 1000781003,

        /// <summary>
        /// The struct is of type `XrGraphicsBindingVulkan2KHR`.
        ///Equivalent to the OpenXR value `XR_TYPE_GRAPHICS_BINDING_VULKAN2_KHR`.
        /// Provided by `XR_KHR_vulkan_enable2`.
        /// </summary>
        GraphicsBindingVulkan2KHR = GraphicsBindingVulkanKHR,

        /// <summary>
        /// The struct is of type `XrSwapchainImageVulkan2KHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_SWAPCHAIN_IMAGE_VULKAN2_KHR`.
        /// Provided by `XR_KHR_vulkan_enable2`.
        /// </summary>
        SwapchainImageVulkan2KHR = SwapchainImageVulkanKHR,

        /// <summary>
        /// The struct is of type `XrGraphicsRequirementsVulkan2KHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_GRAPHICS_REQUIREMENTS_VULKAN2_KHR`.
        /// Provided by `XR_KHR_vulkan_enable2`.
        /// </summary>
        GraphicsRequirementsVulkan2KHR = GraphicsRequirementsVulkanKHR,

        /// <summary>
        /// The struct is of type `XrDevicePcmSampleRateGetInfoFB`.
        /// Equivalent to the OpenXR value `XR_TYPE_DEVICE_PCM_SAMPLE_RATE_GET_INFO_FB`.
        /// Provided by `XR_FB_haptic_pcm`.
        /// </summary>
        DevicePcmSampleRateGetInfoFB = DevicePcmSampleRateStateFB,

        /// <summary>
        /// The struct is of type `XrSpacesLocateInfoKHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPACES_LOCATE_INFO_KHR`.
        /// Provided by `XR_KHR_locate_spaces`.
        /// </summary>
        SpacesLocateInfoKHR = SpacesLocateInfo,

        /// <summary>
        /// The struct is of type `XrSpaceLocationsKHR`.
        /// Equivalent to the OpenXR value `XR_TYPE_SPACE_LOCATIONS_KHR`.
        /// Provided by `XR_KHR_locate_spaces`.
        /// </summary>
        SpaceLocationsKHR = SpaceLocations,

        /// <summary>
        /// The struct is of type `XrSpaceVelocitiesKHR`.
        ///Equivalent to the OpenXR value `XR_TYPE_SPACE_VELOCITIES_KHR`.
        /// Provided by `XR_KHR_locate_spaces`.
        /// </summary>
        SpaceVelocitiesKHR = SpaceVelocities,
    }
}
