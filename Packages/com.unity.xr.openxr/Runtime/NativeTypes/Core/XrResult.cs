using System;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// XR Results returned by XR subsystem callbacks
    /// </summary>
    public enum XrResult
    {
        /// <summary>
        /// Function successfully completed.
        /// </summary>
        Success = 0,

        /// <summary>
        /// The specified timeout time occurred before the operation could complete.
        /// </summary>
        [Obsolete("This value is misspelled and therefore deprecated in OpenXR Plug-in version 1.14.0. Use TimeoutExpired instead.", false)]
        TimeoutExpored = 1,

        /// <summary>
        /// The specified timeout time occurred before the operation could complete.
        /// </summary>
        TimeoutExpired = 1,

        /// <summary>
        /// The session will be lost soon.
        /// </summary>
        LossPending = 3,

        /// <summary>
        /// No event was available.
        /// </summary>
        EventUnavailable = 4,

        /// <summary>
        /// The space’s bounds are not known at the moment.
        /// </summary>
        SpaceBoundsUnavailable = 7,

        /// <summary>
        /// The session is not in the focused state.
        /// </summary>
        SessionNotFocused = 8,

        /// <summary>
        /// A frame has been discarded from composition.
        /// </summary>
        FrameDiscarded = 9,

        /// <summary>
        /// The function usage was invalid in some way.
        /// </summary>
        ValidationFailure = -1,

        /// <summary>
        /// The runtime failed to handle the function in an unexpected way that is not covered by another error result.
        /// </summary>
        RuntimeFailure = -2,

        /// <summary>
        /// A memory allocation has failed.
        /// </summary>
        OutOfMemory = -3,

        /// <summary>
        /// The runtime does not support the requested API version.
        /// </summary>
        ApiVersionUnsupported = -4,

        /// <summary>
        /// Initialization of object could not be completed.
        /// </summary>
        InitializationFailed = -6,

        /// <summary>
        /// The requested function was not found or is otherwise unsupported.
        /// </summary>
        FunctionUnsupported = -7,

        /// <summary>
        /// The requested feature is not supported.
        /// </summary>
        FeatureUnsupported = -8,

        /// <summary>
        /// A requested extension is not supported.
        /// </summary>
        ExtensionNotPresent = -9,

        /// <summary>
        /// The runtime supports no more of the requested resource.
        /// </summary>
        LimitReached = -10,

        /// <summary>
        /// The supplied size was smaller than required.
        /// </summary>
        SizeInsufficient = -11,

        /// <summary>
        /// A supplied object handle was invalid.
        /// </summary>
        HandleInvalid = -12,

        /// <summary>
        /// The `XrInstance` was lost or could not be found. It will need to be destroyed and optionally recreated.
        /// </summary>
        InstanceLost = -13,

        /// <summary>
        /// The session is already running.
        /// </summary>
        SessionRunning = -14,

        /// <summary>
        /// The session is not yet running.
        /// </summary>
        SessionNotRunning = -16,

        /// <summary>
        /// The `XrSession` was lost. It will need to be destroyed and optionally recreated.
        /// </summary>
        SessionLost = -17,

        /// <summary>
        /// The provided `XrSystemId` was invalid.
        /// </summary>
        SystemInvalid = -18,

        /// <summary>
        /// The provided `XrPath` was not valid.
        /// </summary>
        PathInvalid = -19,

        /// <summary>
        /// The maximum number of supported semantic paths has been reached.
        /// </summary>
        PathCountExceeded = -20,

        /// <summary>
        /// The semantic path character format is invalid.
        /// </summary>
        PathFormatInvalid = -21,

        /// <summary>
        /// The semantic path is unsupported.
        /// </summary>
        PathUnsupported = -22,

        /// <summary>
        /// The layer was NULL or otherwise invalid.
        /// </summary>
        LayerInvalid = -23,

        /// <summary>
        /// The number of specified layers is greater than the supported number.
        /// </summary>
        LayerLimitExceeded = -24,

        /// <summary>
        /// The image rect was negatively sized or otherwise invalid.
        /// </summary>
        SwapchainRectInvalid = -25,

        /// <summary>
        /// The image format is not supported by the runtime or platform.
        /// </summary>
        SwapchainFormatUnsupported = -26,

        /// <summary>
        /// The API used to retrieve an action’s state does not match the action’s type.
        /// </summary>
        ActionTypeMismatch = -27,

        /// <summary>
        /// The session is not in the ready state.
        /// </summary>
        SessionNotReady = -28,

        /// <summary>
        /// The session is not in the stopping state.
        /// </summary>
        SessionNotStopping = -29,

        /// <summary>
        /// The provided `XrTime` was zero, negative, or out of range.
        /// </summary>
        TimeInvalid = -30,

        /// <summary>
        /// The specified reference space is not supported by the runtime or system.
        /// </summary>
        ReferenceSpaceUnsupported = -31,

        /// <summary>
        /// The file could not be accessed.
        /// </summary>
        FileAccessError = -32,

        /// <summary>
        /// The file’s contents were invalid.
        /// </summary>
        FileContentsInvalid = -33,

        /// <summary>
        /// The specified form factor is not supported by the current runtime or platform.
        /// </summary>
        FormFactorUnsupported = -34,

        /// <summary>
        /// The specified form factor is supported, but the device is currently not available, e.g. not plugged in or powered off.
        /// </summary>
        FormFactorUnavailable = -35,

        /// <summary>
        /// A requested API layer is not present or could not be loaded.
        /// </summary>
        ApiLayerNotPresent = -36,

        /// <summary>
        /// The call was made without having made a previously required call.
        /// </summary>
        CallOrderInvalid = -37,

        /// <summary>
        /// The given graphics device is not in a valid state. The graphics device could be lost or initialized without meeting graphics requirements.
        /// </summary>
        GraphicsDeviceInvalid = -38,

        /// <summary>
        /// The supplied pose was invalid with respect to the requirements.
        /// </summary>
        PoseInvalid = -39,

        /// <summary>
        /// The supplied index was outside the range of valid indices.
        /// </summary>
        IndexOutOfRange = -40,

        /// <summary>
        /// The specified view configuration type is not supported by the runtime or platform.
        /// </summary>
        ViewConfigurationTypeUnsupported = -41,

        /// <summary>
        /// The specified environment blend mode is not supported by the runtime or platform.
        /// </summary>
        EnvironmentBlendModeUnsupported = -42,

        /// <summary>
        /// The name provided was a duplicate of an already-existing resource.
        /// </summary>
        NameDuplicated = -44,

        /// <summary>
        /// The name provided was invalid.
        /// </summary>
        NameInvalid = -45,

        /// <summary>
        /// A referenced action set is not attached to the session.
        /// </summary>
        ActionsetNotAttached = -46,

        /// <summary>
        /// The session already has attached action sets.
        /// </summary>
        ActionsetsAlreadyAttached = -47,

        /// <summary>
        /// The localized name provided was a duplicate of an already-existing resource.
        /// </summary>
        LocalizedNameDuplicated = -48,

        /// <summary>
        /// The localized name provided was invalid.
        /// </summary>
        LocalizedNameInvalid = -49,

        /// <summary>
        /// The `xrGetGraphicsRequirements`* call was not made before calling `xrCreateSession`.
        /// </summary>
        GraphicsRequirementsCallMissing = -50,

        /// <summary>
        /// The loader was unable to find or load a runtime.
        /// </summary>
        RuntimeUnavailable = -51,

        /// <summary>
        /// One or more of the extensions being enabled has dependency on extensions that are not enabled.
        /// </summary>
        ExtensionDependencyNotEnabled = -1000710001,

        /// <summary>
        /// Insufficient permissions. This error is included for use by vendor extensions. The precise definition of
        /// `PermissionInsufficient` and actions possible by the developer or user to resolve it can vary by platform,
        /// extension or function. The developer should refer to the documentation of the function that returned the
        /// error code and extension it was defined.
        /// </summary>
        PermissionInsufficient = -1000710000,

        /// <summary>
        /// `xrSetAndroidApplicationThreadKHR` failed as thread id is invalid. (Added by the `XR_KHR_android_thread_settings` extension)
        /// </summary>
        AndroidThreadSettingsIdInvalidKHR = -1000003000,

        /// <summary>
        /// `xrSetAndroidApplicationThreadKHR` failed setting the thread attributes/priority. (Added by the `XR_KHR_android_thread_settings` extension)
        /// </summary>
        [Obsolete("This enum value is misspelled and therefore deprecated in OpenXR Plug-in version 1.14.0. Use AndroidThreadSettingsFailureKHR instead.", false)]
        AndroidThreadSettingsdFailureKHR = -1000003001,

        /// <summary>
        /// `xrSetAndroidApplicationThreadKHR` failed setting the thread attributes/priority. (Added by the `XR_KHR_android_thread_settings` extension)
        /// </summary>
        AndroidThreadSettingsFailureKHR = -1000003001,

        /// <summary>
        /// Spatial anchor could not be created at that location. (Added by the `XR_MSFT_spatial_anchor` extension)
        /// </summary>
        CreateSpatialAnchorFailedMSFT = -1000039001,

        /// <summary>
        /// The secondary view configuration was not enabled when creating the session. (Added by the `XR_MSFT_secondary_view_configuration` extension)
        /// </summary>
        SecondaryViewConfigurationTypeNotEnabledMSFT = -1000053000,

        /// <summary>
        /// The controller model key is invalid. (Added by the `XR_MSFT_controller_model` extension)
        /// </summary>
        ControllerModelKeyInvalidMSFT = -1000055000,

        /// <summary>
        /// The reprojection mode is not supported. (Added by the `XR_MSFT_composition_layer_reprojection` extension)
        /// </summary>
        ReprojectionModeUnsupportedMSFT = -1000066000,

        /// <summary>
        /// Compute new scene not completed. (Added by the `XR_MSFT_scene_understanding` extension)
        /// </summary>
        ComputeNewSceneNotCompletedMSFT = -1000097000,

        /// <summary>
        /// Scene component id invalid. (Added by the `XR_MSFT_scene_understanding` extension)
        /// </summary>
        SceneComponentIdInvalidMSFT = -1000097001,

        /// <summary>
        /// Scene component type mismatch. (Added by the `XR_MSFT_scene_understanding` extension)
        /// </summary>
        SceneComponentTypeMismatchMSFT = -1000097002,

        /// <summary>
        /// Scene mesh buffer id invalid. (Added by the `XR_MSFT_scene_understanding` extension)
        /// </summary>
        SceneMeshBufferIdInvalidMSFT = -1000097003,

        /// <summary>
        /// Scene compute feature incompatible. (Added by the `XR_MSFT_scene_understanding` extension)
        /// </summary>
        SceneComputeFeatureIncompatibleMSFT = -1000097004,

        /// <summary>
        /// Scene compute consistency mismatch. (Added by the `XR_MSFT_scene_understanding` extension)
        /// </summary>
        SceneComputeConsistencyMismatchMSFT = -1000097005,

        /// <summary>
        /// The display refresh rate is not supported by the platform. (Added by the `XR_FB_display_refresh_rate` extension)
        /// </summary>
        DisplayRefreshRateUnsupportedFB = -1000101000,

        /// <summary>
        /// The color space is not supported by the runtime. (Added by the `XR_FB_color_space` extension)
        /// </summary>
        ColorSpaceUnsupportedFB = -1000108000,

        /// <summary>
        /// The component type is not supported for this space. (Added by the `XR_FB_spatial_entity` extension)
        /// </summary>
        SpaceComponentNotSupportedFB = -1000113000,

        /// <summary>
        /// The required component is not enabled for this space. (Added by the `XR_FB_spatial_entity` extension)
        /// </summary>
        SpaceComponentNotEnabledFB = -1000113001,

        /// <summary>
        /// A request to set the component’s status is currently pending. (Added by the `XR_FB_spatial_entity` extension)
        /// </summary>
        SpaceComponentStatusPendingFB = -1000113002,

        /// <summary>
        /// The component is already set to the requested value. (Added by the `XR_FB_spatial_entity` extension)
        /// </summary>
        SpaceComponentStatusAlreadySetFB = -1000113003,

        /// <summary>
        /// The object state is unexpected for the issued command. (Added by the `XR_FB_passthrough` extension)
        /// </summary>
        UnexpectedStatePassthroughFB = -1000118000,

        /// <summary>
        /// Trying to create an MR feature when one was already created and only one instance is allowed. (Added by the `XR_FB_passthrough` extension)
        /// </summary>
        FeatureAlreadyCreatedPassthroughFB = -1000118001,

        /// <summary>
        /// Requested functionality requires a feature to be created first. (Added by the `XR_FB_passthrough` extension)
        /// </summary>
        FeatureRequiredPassthroughFB = -1000118002,

        /// <summary>
        /// Requested functionality is not permitted - application is not allowed to perform the requested operation. (Added by the `XR_FB_passthrough` extension)
        /// </summary>
        NotPermittedPassthroughFB = -1000118003,

        /// <summary>
        /// There were insufficient resources available to perform an operation. (Added by the `XR_FB_passthrough` extension)
        /// </summary>
        InsufficientResourcesPassthroughFB = -1000118004,

        /// <summary>
        /// Unknown Passthrough error (no further details provided). (Added by the `XR_FB_passthrough` extension)
        /// </summary>
        UnknownPassthroughFB = -1000118050,

        /// <summary>
        /// The model key is invalid. (Added by the `XR_FB_render_model` extension)
        /// </summary>
        RenderModelKeyInvalidFB = -1000119000,

        /// <summary>
        /// The model is unavailable. (Added by the `XR_FB_render_model` extension)
        /// </summary>
        RenderModelUnavailableFB = 1000119020,

        /// <summary>
        /// Marker tracking is disabled or the specified marker is not currently tracked. (Added by the `XR_VARJO_marker_tracking` extension)
        /// </summary>
        MarkerNotTrackedVARJO = -1000124000,

        /// <summary>
        /// The specified marker ID is not valid. (Added by the `XR_VARJO_marker_tracking` extension)
        /// </summary>
        MarkerIdInvalidVARJO = -1000124001,

        /// <summary>
        /// The com.magicleap.permission.MARKER_TRACKING permission was denied. (Added by the `XR_ML_marker_understanding` extension)
        /// </summary>
        MarkerDetectorPermissionDeniedML = -1000138000,

        /// <summary>
        /// The specified marker could not be located spatially. (Added by the `XR_ML_marker_understanding` extension)
        /// </summary>
        MarkerDetectorLocateFailedML = -1000138001,

        /// <summary>
        /// The marker queried does not contain data of the requested type. (Added by the `XR_ML_marker_understanding` extension)
        /// </summary>
        MarkerDetectorInvalidDataQueryML = -1000138002,

        /// <summary>
        /// `createInfo` contains mutually exclusive parameters, such as setting `XR_MARKER_DETECTOR_CORNER_REFINE_METHOD_APRIL_TAG_ML` with `XR_MARKER_TYPE_ARUCO_ML`. (Added by the `XR_ML_marker_understanding` extension)
        /// </summary>
        MarkerDetectorInvalidCreateInfoML = -1000138003,

        /// <summary>
        /// The marker id passed to the function was invalid. (Added by the `XR_ML_marker_understanding` extension)
        /// </summary>
        MarkerInvalidML = -1000138004,

        /// <summary>
        /// The localization map being imported is not compatible with current OS or mode. (Added by the `XR_ML_localization_map` extension)
        /// </summary>
        LocalizationMapIncompatibleML = -1000139000,

        /// <summary>
        /// The localization map requested is not available. (Added by the `XR_ML_localization_map` extension)
        /// </summary>
        LocalizationMapUnavailableML = -1000139001,

        /// <summary>
        /// The map localization service failed to fulfill the request, retry later. (Added by the `XR_ML_localization_map` extension)
        /// </summary>
        LocalizationMapFailML = -1000139002,

        /// <summary>
        /// The com.magicleap.permission.SPACE_IMPORT_EXPORT permission was denied. (Added by the `XR_ML_localization_map` extension)
        /// </summary>
        LocalizationMapImportExportPermissionDeniedML = -1000139003,

        /// <summary>
        /// The com.magicleap.permission.SPACE_MANAGER permission was denied. (Added by the `XR_ML_localization_map` extension)
        /// </summary>
        LocalizationMapPermissionDeniedML = -1000139004,

        /// <summary>
        /// The map being imported already exists in the system. (Added by the `XR_ML_localization_map` extension)
        /// </summary>
        LocalizationMapAlreadyExistsML = -1000139005,

        /// <summary>
        /// The map localization service cannot export cloud based maps. (Added by the `XR_ML_localization_map` extension)
        /// </summary>
        LocalizationMapCannotExportCloudMapML = -1000139006,

        /// <summary>
        /// The `com.magicleap.permission.SPATIAL_ANCHOR` permission was not granted. (Added by the `XR_ML_spatial_anchors` extension)
        /// </summary>
        SpatialAnchorsPermissionDeniedML = -1000140000,

        /// <summary>
        /// Operation failed because the system is not localized into a localization map. (Added by the `XR_ML_spatial_anchors` extension)
        /// </summary>
        SpatialAnchorsNotLocalizedML = -1000140001,

        /// <summary>
        /// Operation failed because it is performed outside of the localization map. (Added by the `XR_ML_spatial_anchors` extension)
        /// </summary>
        SpatialAnchorsOutOfMapBoundsML = -1000140002,

        /// <summary>
        /// Operation failed because the space referenced cannot be located. (Added by the `XR_ML_spatial_anchors` extension)
        /// </summary>
        SpatialAnchorsSpaceNotLocatableML = -1000140003,

        /// <summary>
        /// The anchor references was not found. (Added by the `XR_ML_spatial_anchors_storage` extension)
        /// </summary>
        SpatialAnchorsAnchorNotFoundML = -1000141000,

        /// <summary>
        /// A spatial anchor was not found associated with the spatial anchor name provided (Added by the `XR_MSFT_spatial_anchor_persistence` extension)
        /// </summary>
        SpatialAnchorNameNotFoundMSFT = -1000142001,

        /// <summary>
        /// The spatial anchor name provided was not valid (Added by the `XR_MSFT_spatial_anchor_persistence` extension)
        /// </summary>
        SpatialAnchorNameInvalidMSFT = -1000142002,

        /// <summary>
        /// Marker does not encode a string. (Added by the `XR_MSFT_scene_marker` extension)
        /// </summary>
        SceneMarkerDataNotStringMSFT = 1000147000,

        /// <summary>
        /// Anchor import from cloud or export from device failed. (Added by the `XR_FB_spatial_entity_sharing` extension)
        /// </summary>
        SpaceMappingInsufficientFB = -1000169000,

        /// <summary>
        /// Anchors were downloaded from the cloud but failed to be imported/aligned on the device. (Added by the `XR_FB_spatial_entity_sharing` extension)
        /// </summary>
        SpaceLocalizationFailedFB = -1000169001,

        /// <summary>
        /// Timeout occurred while waiting for network request to complete. (Added by the `XR_FB_spatial_entity_sharing` extension)
        /// </summary>
        SpaceNetworkTimeoutFB = -1000169002,

        /// <summary>
        /// The network request failed. (Added by the `XR_FB_spatial_entity_sharing` extension)
        /// </summary>
        SpaceNetworkRequestFailedFB = -1000169003,

        /// <summary>
        /// Cloud storage is required for this operation but is currently disabled. (Added by the `XR_FB_spatial_entity_sharing` extension)
        /// </summary>
        SpaceCloudStorageDisabledFB = -1000169004,

        /// <summary>
        /// The provided data buffer did not match the required size. (Added by the `XR_META_passthrough_color_lut` extension)
        /// </summary>
        PassthroughColorLutBufferSizeMismatchMETA = -1000266000,

        /// <summary>
        /// Warning: The requested depth image is not yet available. (Added by the `XR_META_environment_depth` extension)
        /// </summary>
        EnvironmentDepthNotAvailableMETA = 1000291000,

        /// <summary>
        /// The render model ID is invalid. (Added by the `XR_EXT_render_model` extension)
        /// </summary>
        RenderModelIdInvalidEXT = -1000300000,

        /// <summary>
        /// The render model asset is unavailable. (Added by the `XR_EXT_render_model` extension)
        /// </summary>
        RenderModelAssetUnavailableEXT = -1000300001,

        /// <summary>
        /// A glTF extension is required. (Added by the `XR_EXT_render_model` extension)
        /// </summary>
        RenderModelGltfExtensionRequiredEXT = -1000300002,

        /// <summary>
        /// The provided XrRenderModelEXT was not created from a XrRenderModelIdEXT from `XR_EXT_interaction_render_model` (Added by the `XR_EXT_interaction_render_model` extension)
        /// </summary>
        NotInteractionRenderModelEXT = -1000301000,

        /// <summary>
        /// Tracking optimization hint is already set for the domain. (Added by the `XR_QCOM_tracking_optimization_settings` extension)
        /// </summary>
        HintAlreadySetQCOM = -1000306000,

        /// <summary>
        /// The provided space is valid but not an anchor. (Added by the `XR_HTC_anchor` extension)
        /// </summary>
        NotAnAnchorHTC = -1000319000,

        /// <summary>
        /// The spatial entity id is invalid. (Added by the `XR_BD_spatial_sensing` extension)
        /// </summary>
        SpatialEntityIdInvalidBD = -1000389000,

        /// <summary>
        /// The spatial sensing service is unavailable. (Added by the `XR_BD_spatial_sensing` extension)
        /// </summary>
        SpatialSensingServiceUnavailableBD = -1000389001,

        /// <summary>
        /// The spatial entity does not support anchor. (Added by the `XR_BD_spatial_sensing` extension)
        /// </summary>
        AnchorNotSupportedForEntityBD = -1000389002,

        /// <summary>
        /// The spatial anchor was not found. (Added by the `XR_BD_spatial_sensing` extension)
        /// </summary>
        SpatialAnchorNotFoundBD = -1000390000,

        /// <summary>
        /// The spatial anchor sharing network experienced a timeout. (Added by the `XR_BD_spatial_sensing` extension)
        /// </summary>
        SpatialAnchorSharingNetworkTimeoutBD = -1000391000,

        /// <summary>
        ///  The spatial anchor sharing experienced an authentication failure. (Added by the `XR_BD_spatial_sensing` extension)
        /// </summary>
        SpatialAnchorSharingAuthenticationFailureBD = -1000391001,

        /// <summary>
        ///  The spatial anchor sharing experienced a network failure. (Added by the `XR_BD_spatial_sensing` extension)
        /// </summary>
        SpatialAnchorSharingNetworkFailureBD = -1000391002,

        /// <summary>
        ///  The spatial anchor sharing localization failed. (Added by the `XR_BD_spatial_sensing` extension)
        /// </summary>
        SpatialAnchorSharingLocaliztionFailBD = -1000391003,

        /// <summary>
        /// The spatial anchor sharing map is insufficient. (Added by the `XR_BD_spatial_sensing` extension)
        /// </summary>
        SpatialAnchorSharingMapInsufficientBD = -1000391004,

        /// <summary>
        /// The scene capture is failed, for example exiting abnormally. (Added by the `XR_BD_spatial_scene` extension)
        /// </summary>
        SceneCaptureFailureBD = -1000392000,

        /// <summary>
        /// The space passed to the function was not locatable. (Added by the `XR_EXT_plane_detection` extension)
        /// </summary>
        SpaceNotLocatableEXT = -1000429000,

        /// <summary>
        /// The permission for this resource was not granted. (Added by the `XR_EXT_plane_detection` extension)
        /// </summary>
        PlaneDetectionPermissionDeniedEXT = -1000429001,

        /// <summary>
        /// Returned by completion function to indicate future is not ready. (Added by the `XR_EXT_future` extension)
        /// </summary>
        FuturePendingEXT = -1000469001,

        /// <summary>
        /// Returned by completion function to indicate future is not valid. (Added by the `XR_EXT_future` extension)
        /// </summary>
        FutureInvalidEXT = -1000469002,

        /// <summary>
        /// The `com.magicleap.permission.SYSTEM_NOTIFICATION` permission was not granted. (Added by the `XR_ML_system_notifications` extension)
        /// </summary>
        SystemNotificationPermissionDeniedML = -1000473000,

        /// <summary>
        /// Incompatible SKU detected. (Added by the `XR_ML_system_notifications` extension)
        /// </summary>
        SystemNotificationIncompatibleSkuML = -1000473001,

        /// <summary>
        /// The world mesh detector permission was not granted. (Added by the `XR_ML_world_mesh_detection` extension)
        /// </summary>
        WorldMeshDetectorPermissionDeniedML = -1000474000,

        /// <summary>
        /// At the time of the call the runtime was unable to locate the space and cannot fulfill your request. (Added by the `XR_ML_world_mesh_detection` extension)
        /// </summary>
        WorldMeshDetectorSpaceNotLocatableML = -1000474001,

        /// <summary>
        /// Permission to track facial expressions was not granted (Added by the `XR_ML_facial_expression` extension)
        /// </summary>
        FacialExpressionPermissionDeniedML = 1000482000,

        /// <summary>
        /// The network request failed. (Added by the `XR_META_colocation_discovery` extension)
        /// </summary>
        ColocationDiscoveryNetworkFailedMETA = -1000571001,

        /// <summary>
        /// The runtime does not have any methods available to perform discovery. (Added by the `XR_META_colocation_discovery` extension)
        /// </summary>
        ColocationDiscoveryNoDiscoveryMethodMETA = -1000571002,

        /// <summary>
        /// Colocation advertisement has already been enabled. (Added by the `XR_META_colocation_discovery` extension)
        /// </summary>
        ColocationDiscoveryAlreadyAdvertisingMETA = 1000571003,

        /// <summary>
        /// Colocation discovery has already been enabled. (Added by the `XR_META_colocation_discovery` extension)
        /// </summary>
        ColocationDiscoveryAlreadyDiscoveringMETA = 1000571004,

        /// <summary>
        /// The group UUID was not be found within the runtime. (Added by the `XR_META_spatial_entity_group_sharing` extension)
        /// </summary>
        SpaceGroupNotFoundMETA = -1000572002,

        /// <summary>
        /// The specified spatial capability is not supported by the runtime or the system. (Added by the `XR_EXT_spatial_entity` extension)
        /// </summary>
        SpatialCapabilityUnsupportedEXT = -1000740001,

        /// <summary>
        /// The specified spatial entity id is invalid or an entity with that id does not exist in the environment. (Added by the `XR_EXT_spatial_entity` extension)
        /// </summary>
        SpatialEntityIdInvalidEXT = -1000740002,

        /// <summary>
        /// The specified spatial buffer id is invalid or does not exist in the spatial snapshot being used to query for the buffer data. (Added by the `XR_EXT_spatial_entity` extension)
        /// </summary>
        SpatialBufferIdInvalidEXT = -1000740003,

        /// <summary>
        /// The specified spatial component is not supported by the runtime or the system for the given capability. (Added by the `XR_EXT_spatial_entity` extension)
        /// </summary>
        SpatialComponentUnsupportedForCapabilityEXT = -1000740004,

        /// <summary>
        /// The specified spatial capability configuration is invalid. (Added by the `XR_EXT_spatial_entity` extension)
        /// </summary>
        SpatialCapabilityConfigurationInvalidEXT = -1000740005,

        /// <summary>
        ///The specified spatial component is not enabled for the spatial context. (Added by the `XR_EXT_spatial_entity` extension)
        /// </summary>
        SpatialComponentNotEnabledEXT = -1000740006,

        /// <summary>
        /// The specified spatial persistence scope is not supported by the runtime or the system. (Added by the `XR_EXT_spatial_persistence` extension)
        /// </summary>
        SpatialPersistenceScopeUnsupportedEXT = -1000763001,

        /// <summary>
        /// The scope configured for the persistence context is incompatible for the current spatial entity. (Added by the `XR_EXT_spatial_persistence_operations` extension)
        /// </summary>
        SpatialPersistenceScopeIncompatibleEXT = -1000781001,

        /// <summary>
        /// Provided by `XR_KHR_maintenance1`
        /// </summary>
        ExtensionDependencyNotEnabledKHR = ExtensionDependencyNotEnabled,

        /// <summary>
        /// Provided by `XR_KHR_maintenance1`
        /// </summary>
        PermissionInsufficientKHR = PermissionInsufficient,

        /// <summary>
        /// Max XR Result Value
        /// </summary>
        MaxResult = 0x7FFFFFFF
    }

    /// <summary>
    /// Extension methods for <see cref="XrResult"/>.
    /// </summary>
    public static class XrResultExtensions
    {
        /// <summary>
        /// Indicates whether the `XrResult` represents a success code, inclusive of <see cref="XrResult.Success"/>.
        /// </summary>
        /// <param name="xrResult">The `XrResult`.</param>
        /// <returns><see langword="true"/> if the `XrResult` represents a success code. Otherwise, <see langword="false"/>.</returns>
        public static bool IsSuccess(this XrResult xrResult)
        {
            return xrResult >= 0;
        }

        /// <summary>
        /// Indicates whether the `XrResult` represents an unqualified success, ie <see cref="XrResult.Success"/>.
        /// </summary>
        /// <param name="xrResult">The `XrResult`.</param>
        /// <returns><see langword="true"/> if the `XrResult` represents an unqualified success. Otherwise, <see langword="false"/>.</returns>
        public static bool IsUnqualifiedSuccess(this XrResult xrResult)
        {
            return xrResult == 0;
        }

        /// <summary>
        /// Indicates whether the `XrResult` represents an error code.
        /// </summary>
        /// <param name="xrResult">The `XrResult`.</param>
        /// <returns><see langword="true"/> if the `XrResult` represents an error code. Otherwise, <see langword="false"/>.</returns>
        public static bool IsError(this XrResult xrResult)
        {
            return xrResult < 0;
        }
    }
}
