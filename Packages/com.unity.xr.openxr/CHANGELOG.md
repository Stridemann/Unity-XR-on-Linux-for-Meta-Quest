# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

<!--
> **Notes**
> When updating the Changelog, please ensure we follow the standards for ordering headers as outlined here: [US-0039](https://standards.ds.unity3d.com/Standards/US-0039/). Specifically: Under ## headers, ### \<type\> headers are listed in this order: Added, Changed, Deprecated, Removed, Fixed, Security
-->

## [1.16.1] - 2025-11-21

### Changed

* Changed wording of validation rule that checks for symmetric projection when using the Multiview Render Regions Optimizations Mode.
* Changed OpenXR loader version to 1.1.53.
* Changed the visibility mask to not create an occlusion mesh when using a 2022.3 editor to reduce memory footprint.

### Removed

* Removed an unit test `PluginVersion` which was used internally to validate package version.

### Fixed

 * Fixed the OpenXR test "DuplicateSettingAssetTest" to communicate with asset database when swapping asset files.
 * Fixed an issue in OpenXR Runtime Debugger window to show correctly sized buttons. ([OXRB-682](https://issuetracker.unity3d.com/issues/folder-button-in-openxr-runtime-debugger-window-is-oversized))
 * Fixed a dependency issue by including AndroidJNI as a dependency. ([OXRB-696](https://jira.unity3d.com/browse/OXRB-696))
 * Fixed a crash when using eye tracked foveated rendering and sleeping/waking device by making sure primary swapchain reference was reset before applying foveation profile.
 * Fixed unreliable writing of `xr-meta-enabled` to boot.config by ensuring it is always set based on the actual feature state.
 * Fixed Bluetooth permissions being removed when building apps using the Meta Quest Feature. The permissions are now kept when required by Unity APIs or a plugin's manifest. For more information on Android permissions in Unity, refer to [Android App Manifest](https://docs.unity3d.com/6000.2/Documentation/Manual/android-manifest.html).
 * Fixed issue where project settings window would fail to render if an OpenXR Feature was removed while the editor was running. ([OXRB-715](https://jira.unity3d.com/browse/OXRB-715))
 * Fixed an error in Project Validation related to EntityId's in Unity 6000.4+.

## [1.16.0] - 2025-10-15

### Added

 * Added public debug symbols for the UnityOpenXR library for Android platform that are available when building an APK with the Debug Symbols dropdown set to Public or Debugging. For more information, refer to [Android Symbols](https://docs.unity3d.com/6000.3/Documentation/Manual/android-symbols.html).
 * Added validation rule for Multi View Per View Viewport and Render Graph.

### Changed

* Changed the return values of the following pre-release APIs from `XrResult` to `OpenXRResultStatus`:
  * `OpenXRNativeAPI.xrEnumerateSpatialPersistenceScopesEXT(uint, out uint, XrSpatialPersistenceScopeEXT*)`
  * `OpenXRNativeAPI.xrEnumerateSpatialPersistenceScopesEXT(Allocator, out NativeArray<XrSpatialPersistenceScopeEXT>`
* Changed the OpenXR test "DuplicateSettingAssetTest" to be ignored.

### Fixed

* Fixed a crash with `OpenXRProjectionLayer` when a texture with a size larger than the view was used.
* Fixed a crash when the calls are sent to OpenXR library display subsystem before the OpenXR library is initialized manually.
* Fixed an issue where the user's OpenXR settings could be erased after creating a build.
* Fixed missing diagnostic logs for the available and requested OpenXR extensions under the OpenXR Runtime Info section.
* Fixed one frame flash of black screen when recentering view in floor space.
* Fixed OpenXR Project Settings so that changes to enabled interaction profiles are correctly saved when you close and re-open the Editor. ([OXRB-662](https://issuetracker.unity3d.com/product/unity/issues/guid/OXRB-662))

## [1.16.0-pre.2] - 2025-09-26

### Added

* Added the following members to the [OpenXR native API](xref:openxr-native-api):
  * `ISpatialConfiguration`
  * `XrBaseInStructure`
  * `OpenXRResultStatus.ToString`
  * `XrFovf` (removing the previous `COMPOSITION_LAYERS` preprocessor directive)
  * Added constructor overloads to structs that take `NativeArray<T>.ReadOnly` as input parameters.
  * `XrSpatialDiscoverySnapshotCreateInfoEXT.defaultValue`
  * `OpenXRNativeApi.xrPollFutureEXT(XrFutureEXT)`
  * `OpenXRNativeApi.xrCancelFutureEXT(XrFutureEXT)`
  * `OpenXRResultStatus.StatusCode.Unsupported`
  * static factory methods to construct `XrPosef`, `XrVector3f`, and `XrQuaternionf` from session-space data
  * `XrVector3f.ToSessionSpaceVector3`
  * `XrQuaternionf.ToSessionSpaceQuaternion`
  * `XrPosef.ToSessionSpacePose`
  * `XrPosef(UnityEngine.Pose)` constructor
  * `XrSpatialComponentDataQueryResultEXT(void*)` constructor
  * `XrSpatialComponentDataQueryResult.SetEntityIds` and `XrSpatialComponentDataQueryResult.SetEntityStates`
* Added a new validation rule for Meta Quest feature to ensure Application entry point being set to GameActivity for Unity 6.0+.

### Deprecated

* Deprecated Hololens Microsoft Hand Interaction Profile.

### Removed

* Removed a pre-release overload for `OpenXRNativeAPI.xrQuerySpatialComponentDataEXT` that didn't allow you to receive component data chained to the next pointer of the query condition.

### Fixed

* Fixed the Render Mode setting dropdown in OpenXR Settings to align consistently with the UI of other settings.
* Fixed `MockOpenXREnvironmentSettings` so that changes made to mock runtime settings don't affect the serialized settings assets in your project.
* Fixed `MockOpenXREnvironmentSettings` so that it doesn't save any unwanted changes to your EditorBuildSettings.
* Fixed swapchain diagnostic reports not being removed from cache after swapchains are destroyed.
* Fixed a compile error when using this package in Unity 6000.3.0b1 or newer, which was caused by the deprecation of the VR module in that Editor version.
* Fixed a crash when the OpenXR library was loaded with a path containing emojis on OSX.

## [1.16.0-pre.1] - 2025-08-14

### Added

* Added support for OpenXR features to define the OpenXR API version required to fully function.
* Added members to the `XrStructureType` enum. `XrStructureType` now contains all OpenXR values as of OpenXR version 1.1.49.
* Added support for multiple OpenXR features to define a custom loader and define the OpenXR API version that is supported by the feature's loader.
* Added the [OpenXR native API](xref:openxr-native-api) with initial support for the following extensions:
  * `XR_EXT_future`
  * `XR_EXT_spatial_anchor`
  * `XR_EXT_spatial_entity`
  * `XR_EXT_spatial_marker_tracking`
  * `XR_EXT_spatial_persistence`
  * `XR_EXT_spatial_persistence_operations`
  * `XR_EXT_spatial_plane_tracking`
* Added `RenderTexture` support for Composition Layers.
* Added class `MockOpenXREnvironment` and `MockOpenXREnvironmentSettings` for testing low-level runtime functionality using the Mock Runtime feature.
* Added [PollEventRouter](xref:UnityEngine.XR.OpenXR.NativeTypes.PollEventRouter), a new type that allows you to subscribe to `xrPollEvent` events.
* Added feature to enable Automatic Viewport Dynamic Resolution for compatible devices. Refer to [Automatic Viewport Dynamic Resolution](xref:openxr-automatic-dynamic-resolution) for information.
* Added MSAA support for Windows+Vulkan builds.
* Added support for enabling Subsampled Layout on Vulkan devices.
  * Added `Subsampled Layout (Vulkan)` setting to the Foveated Rendering Feature as an additional setting.
  * Added `FoveatedRenderingFeature.TrySetSubsampledLayoutEnabled` and `FoveatedRenderingFeature.isSubsampledLayoutEnabled` for runtime control.
* Added `OpenXRFeatureAttribute.CustomRuntimeLoaderName` for customizing the name used by a custom OpenXR loader provided by an OpenXR feature.

### Changed
* Changed OpenXR loader version to 1.1.49.
* Changed sample count of SwapchainCreateInfo to 1 as default instead of recommendedSwapchainSampleCount returning from the runtime.
* Updated minimum supported version from 2021.3 to 2022.3.
* Changed the OpenXR Validation for the minimum Android API level to only apply to Unity 6.2 and older versions.

### Removed
* Removed the validation warning to enable the OpenXR package if an OpenXR feature is enabled. Instead, OpenXR validation will only run when the OpenXR loader is selected as active loader.
* Remove HoloLens documentation as Unity has deprecated its support. Refer to [the public announcement](https://discussions.unity.com/t/hololens-2-deprecation/1661529) for more information.

## [1.15.1] - 2025-07-23

### Fixed

* Fixed compile errors due to `AssetDatabase.CanOpenAssetInEditor(int)` and `AssetDatabase.OpenAsset(int)` were deprecated in Unity 6.3 and newer.
* Fixed an issue in which the wrong normalized device coordinates (NDC) convention was used for SpaceWarp motion vectors. Application developers must now specify whether the current device uses right- or left-handed NDC for motion vectors when SpaceWarp is enabled.

## [1.15.0] - 2025-06-12

### Added

* Added settings and a component to enable URP Application Spacewarp for compatible devices. Refer to [URP Application Spacewarp](xref: openxr-spacewarp) for information.
* Added a validation rule for warning of Magic Leap 2 limit to existing projects from Unity 6.3.

### Changed

* Check whether the requested eye tracking permission has been granted before requesting permission.
* Changed the Composition Layers native implementation to create global references of Android surface objects.

### Fixed

* Fixed compile errors due to `TreeView`, `TreeViewState` and `TreeViewItem` were deprecated in Unity 6.2 and newer.
* Fixed an issue to only apply OpenXR related bootconfig when OpenXR Loader is enabled.
* Enabled `XR_FB_swapchain_update_state` extension by default if available to fix `XR_ERROR_EXTENSION_DEPENDENCY_NOT_ENABLED` runtime error when `XR_FB_foveation` extension was enabled by default.
* Fixed incorrect display of the `Use OpenXR Predicted Time` toggle in Unity versions below 6.2.
* Fixed a crash that could occur when changing scenes containing an `OpenXRDefaultLayer` with extensions.
* Fixed compile errors due to `EditorUtility.InstanceIDToObject` being deprecated in Unity 6.3 and newer.
* Fixed foveation data corruption leading to visual anomalies when the OpenXR swapchain was frequently resized.
* Fixed an increase in memory that was seen in older editors that are not checking for the OffScreen Rendering Only bootconfig value.

## [1.15.0-pre.2] - 2025-05-15

### Added

* Added `OpenXRLayerUtility.RequestRenderTextureId` and `OpenXRLayerUtility.RequestStereoRenderTextureIds`. These functions have callback parameters because they execute on the graphics thread.
* Added support for a new Multiview Render Regions Optimization mode - All Passes, which gets applied to as many passes as possible instead of only the final pass of the render in XR.

### Changed

* Changed the Symmetric Projection validation rule that checks for Vulkan api to a warning instead of an error.
* Deprecated  `OpenXRLayerUtility.FindRenderTexture`, `OpenXRLayerUtility.FindAndWriteToRenderTexture`, `OpenXRLayerUtility.FindAndWriteToStereoRenderTextures`, and `OpenXRLayerUtility.FindAndWriteToStereoRenderTextures`.
* Swapchain images are acquired on the graphics thread with `OpenXRLayerUtility.RequestRenderTextureId` and `OpenXRLayerUtility.RequestStereoRenderTextureIds` and automatically released by the OpenXR plugin on the graphics thread.
* Enable `XR_FB_foveation` and `XR_FB_foveation_configuration` extensions by default if available for runtime.
* Changed Multiview Render Regions (Vulkan) setting for Meta Quest Support from a check box to dropdown menu with three options: None, FinalPass and AllPasses.

### Fixed

* Fixed errors when opening Profiler (Standalone Process) with OpenXR Plugin is present in the project.
* Fixed Mock Runtime occlusion mesh rendering.
* Fixed an issue that OpenXR Project Settings window appears blank and Editor freezes when upgrading package version to 1.14.1 or higher.

## [1.15.0-pre.1] - 2025-04-24

### Added

* Added `OpenXREditorSettings.VulkanOffscreenSwapchainNoMainDisplay` property to request at startup to use offscreen rendering for devices that require it. The setting can be enabled through the Project settings UI and build code. This setting should be disabled for handheld platforms.
* Added Meta Quest input profile features when switching to Meta Quest Build Profile.
* Added `Use OpenXR Predicted Time` UI setting to enable OpenXR time prediction which allows the hardware and runtime to set the display time prediction for the next frame instead of Unity.
* Added a new OpenXrSetting, [LatencyOptimization](xref:project-configuration#latency-optimization) to allow developers to choose opt to prioritize rendering latency or input polling latency.
* Added optional project validation check for Link Time Optimization with Meta Quest.
* Added `UnityXRDisplay::DestroyTexture` API so providers can access them to destroy textures created by `UnityXRDisplay::CreateTexture` API.
* Added a validation rule for setting Latency Optimization to Prioritize Input Polling with Meta Quest Support enabled.

### Changed

* Recompiled the native Android plug-in with support for [16 KB page sizes](https://developer.android.com/guide/practices/page-sizes) on Android 15 or newer.

### Fixed

* Fixed offscreen rendering flags to be added only when the "Offscreen Rendering Only (Vulkan)" Android OpenXR UI setting is enabled. [IN-78279](https://unity3d.atlassian.net/servicedesk/customer/portal/2/IN-78279).
* Fixed Meshing and Intercept features not being enabled by default when the sample was imported.
* Fixed OpenXRCustomLayerHandler class so it now explicitiy calls acquire and release on swapchains for every layer on the main thread and caches swapchain image render textures for efficiency.
* Fixed an issue where `OpenXRInput.TryGetInputSourceName` failed to retrieve the input source name.

## [1.14.3] - 2025-04-18

### Fixed

* Fixed Multiview Render Regions feature regression.

## [1.14.2] - 2025-03-20

### Added

* Added a validation rule about the minimum Android API level is 24.
* Added support for visible triangle mesh to be available for use in post processing.

### Changed

* Modified the Meta Quest Support feature to use the updated Khronos OpenXR loader library.
* Changed OpenXR loader version to 1.1.45.

### Fixed

* Fixed an issue that enabled interaction profiles setting not being saved after reopening the project.
* Fixed a bug that depth submission mode corrupting the spacewarp depth texture.
* Fixed a bug that wrong view configuration type being selected as primary view and causing rendering issues.
* Fixed an issue on Android platforms where `xrCreateSwapchainAndroidSurfaceKHR` was attempted to be used even when the XR Composition Layers package wasn't installed.
* Fixed an issue that Meta XR simulator can't launch properly on Mac.

## [1.14.1] - 2025-02-14

### Added

* Added a validation rule to the Meta Quest Support feature to ensure that your app's default interface orientation uses a supported value.

### Changed

* Modified the Meta Quest Support feature to use the updated Khronos OpenXR loader library.

### Fixed

* Fixed the OpenXRFeatureBuildHooks class so it now grabs the OpenXRSettings object from the given build target during build processing.
* Fixed duplicate OpenXR feature assets being added to the OpenXRSettings asset when upgrading to a newer OpenXR package version.
* Fixed rendering problems with Mixed Reality Capture.
* Fixed a debug log spamming issue regarding ignored event types. This message could be logged multiple times per frame and was not helpful.
* Fixed Meta-specific eye-tracking manifest permissions to be added only to apps targeting Meta Quest Pro devices.
* Fixed an issue where spacewarp may assign motion vector textures to wrong render passes when using secondary views.
* Fixed a bug when allocating array swapchain failed, the index was incremented incorrectly.
* Fixed an issue when OpenXR feature didn't have all the attribute entries setup.

## [1.14.0] - 2024-12-13

### Added

* Added support for the RG16f texture format and a selection mode for the texture format of the Space Warp motion vector texture for the Meta Quest feature. You can select the format for the Space Warp motion vector texture in Editor > Project Settings > XR Plug-in Management > OpenXR > Android tab > Meta Quest Support feature settings.
* Added the following extension methods for `XrResult`: `IsSuccess()`, `IsUnqualifiedSuccess()`, and `IsError()`, which you can use to more conveniently reason about `XrResult` values.
* Added new values to the `XrResult` enum to match the values present in the native `XrResult` type in OpenXR 1.1.36.
* Added support for locating the OpenXR loader for the Meta XR Simulator on OSX.
* Added new Optimize Multiview Render Regions settings option for Meta Quest devices. In the context of XR rendering, Multiview Render Regions enables the drivers to skip shader invocations (and rendering work) for screen areas outside of the user's view (such as the nasal regions that the user can't see within the headset). You can toggle on support for Multiview Render Regions in Editor > Project Settings > XR Plug-in Management > OpenXR > Android tab > Meta Quest Support feature settings. Note that Multiview Render Regions requires Symmetric Projection settings toggle turned on, and Render Mode set to Single Pass Instanced \ Multi-view. Multiview Render Regions is only supported in the Vulkan Graphics API, and it is activated at application start up.
* Added project validation warning to prevent user from using URP upscaling with XR.

### Deprecated

* Deprecated `XrResult.TimeoutExpored` and `XrResult.AndroidThreadSettingsdFailureKHR` and replaced them with correctly spelled enum values.

### Fixed

* Occlusion Mask vertices projected based on FOV.

## [1.13.2] - 2024-11-15

### Added

* Added UWP platform as one of the supported build targets for OpenXR Composition Layer feature.

### Fixed

* Fixed compiler errors when using experimental versions of the XR Composition Layers package (versions older than 1.0.0). You are required to use XR Composition Layers 1.0.0 or newer for OpenXR support.

## [1.13.1] - 2024-10-31

### Fixed

* Conformance Automation Feature:  Removed assert for optional entry point xrSetInputDeviceVelocityUNITY.
* Fixed an issue where `OpenXRInput.TryGetInputSourceName` failed to retrieve the input source name.

## [1.13.0] - 2024-09-16

### Added

* Added Meta XR Simulator as runtime option for OpenXR. One can search for the `com.meta.xr.simulator` package to add the Open XR runtime to the dropdown list of available runtimes during Play Mode.
* Added a `Quest 3S` option to `Target Devices` in the Meta Quest Support settings. This specifies the application's support for Quest 3S devices within the Android manifest.
* Added `IUnityXRDisplay::CreateTexture` API to C# script so providers can access them for functionality.
* Added APIs to provide an alternative to the Input System: `OpenXRFeature.GetAction`, `OpenXRInput.GetActionHandle`, `OpenXRInput.SendHapticImpulse`, `OpenXRInput.StopHapticImpulse`, `OpenXRInput.TrySetControllerLateLatchAction`, and `OpenXRInput.GetActionIsActive`.
* Added a "Controller XRInput" sample to demonstrate how to access input data without the Input System.
* Added experimental support for XR Composition Layers support. Install the Unity [Composition Layer (com.unity.xr.compositionlayers)](https://docs.unity3d.com/Packages/com.unity.xr.compositionlayers@latest?subfolder=/changelog/CHANGELOG.html) package to use composition layers in an OpenXR project.
* Added support for the `XR_KHR_maintenance1` extension. This low-level extension provides support for new capabilities promoted in the OpenXR 1.1 standard, which may be needed for backwards compatibility for using new extensions in OpenXR loaders using the version 1.0 of the standard. You need to request this extension if your project will make use of OpenXR 1.1 extenstions that need this extension and are backwards compatible with loaders based on the 1.0 version of the OpenXR standard. If your project targets only the OpenXR 1.1 version, you don't need to request this extension. To use the `XR_KHR_maintenance1` extension, you must request it as part of your own [OpenXR feature definition](xref:openxr-features). Refer to [XR_KHR_maintenance1](https://registry.khronos.org/OpenXR/specs/1.1/html/xrspec.html#XR_KHR_maintenance1) for more information on the extension.
* Added OpenXR 1.1 Runtime support.

### Changed

* Added [Known Issue](xref:openxr-manual#known-issues) to documentation covering how OpenXR doesn't provide Acceleration or Angular Acceleration values for input devices, and these will always return 0.
* Added a validation rule requiring Multi-view to be enabled in order to enable Symmetric Projection.
* Implements the construction and maintenance of native composition layers in C# via our LayerProvider classes inheriting from OpenXRCustomLayerHandler.
* Changed OpenXR loader version to 1.1.36.

### Fixed

* Symmetric Projection was being gated behind foveation extensions being enabled. This wasn't necessary and that dependency has been removed.
* Fixed a bug that causes a user to need a validation rule to make OpenXR loader active.
* Fixed `m_BlendMode` from being overwritten whenever the `DisplaySubsystem` restarts.
* Fixed Metal API initialization.
* Fixed Custom Composition Layer Feature not showing up in the OpenXR feature setting UI after importing into project.
* Fixed a crash with composition layers in scene due to race condition.

## [1.12.1] - 2024-09-05

* Fixed rendering bug when rendering viewport scale < 1.
* Fixed issue where the Editor would hang when renaming and then viewing OpenXR Settings.

## [1.12.0] - 2024-08-01

### Added

* Add class `MetaQuestTouchPlusControllerProfile.QuestTouchPlusController` interaction profile to support Meta Quest Touch Plus Controllers.
* Added a validation rule to warn you that soft shadows can negatively affect performance for Microsoft HoloLens.
* Added a validation rule to infrom you to make the OpenXR Loader an active loader in XR Plugin Management if there are active OpenXR features in the OpenXR settings.
* Added `OpenXRSettings.autoColorSubmissionMode` boolean property to specify OpenXR to use a platform-supported color format in the display swapchain, or for you to manually set which format should OpenXR to use.
* Added `OpenXRSettings.colorSubmissionModes` array property to control the color format used by the OpenXR display swapchain. The available options can be chosen from the `OpenXRSettings.ColorSubmissionModeGroup` enum. OpenXR will use supported color formats by the platform, starting from the first option in the list. In case none of the color formats supplied is supported by the platform, an error will be thrown. If no color formats are assigned, the format used will be RGBA32.
* Added `Automatic Color Submission Mode` toggle and `Color Submission Modes` list to the OpenXR project settings tab, allowing you to manually specify the preferred color format to be used by the display swapchain. OpenXR will use supported color formats by the platform, starting from the first option in the list. In case none of the color formats supplied is supported by the platform, an error will be thrown. If no color formats are assigned, the format used will be RGBA32.

### Fixed

* Fixed sort order of OpenXR features listed based first on priority, then by name, preventing spurious changes in Settings files.

## [1.11.0] - 2024-05-01

### Added
* Added the `XrPerformanceSettings.SetPerformanceLevelHint` static method, which lets you send performance level suggestions to the runtime. These hints help an OpenXR runtime to optimize the use of hardware resources according to the application needs. Refer to [XR performance settings](xref:openxr-performance-settings#performance-settings-level-hints) for more information. This requires the [XR_EXT_performance_settings](https://registry.khronos.org/OpenXR/specs/1.0/html/xrspec.html#XR_EXT_performance_settings) extension is enabled in the OpenXR runtime.
* Added the `XrPerformanceSettings.OnXrPerformanceChangeNotification` event, which provides notifications from the OpenXR runtime when a device's thermal, rendering, or compositor performance state changes. The performance states include: `Normal`, `Warning`, and `Impaired`. Refer to [XR performance settings](xref:openxr-performance-settings#performance-settings-notifications) for more information. This requires the [XR_EXT_performance_settings](https://registry.khronos.org/OpenXR/specs/1.0/html/xrspec.html#XR_EXT_performance_settings) extension is enabled in the OpenXR runtime.
* Added a`Quest 3` option to the `Target Devices` set in the Meta Quest Support settings. This specifies the application's support for Quest 3 devices within the Android manifest.
* Added missing ButtonControl actions `pinch_ext`, `aim_activate_ext` and `grasp_ext` for Hand Interaction Profile.
* Added API `OpenXRUtility.ComputePoseToWorldSpace` to recalculate object position and rotation from tracking-space to world-space. Applicable for use cases such as teleporting.
* Added support for using Local Floor Reference Space in the OpenXR runtime. Useful in applications that require the user to be seated or in a standing fixed position.
* Added PreInit flags to allow users to disable the main frame buffer and use offscreen swapchain when using Vulkan on Android in the OpenXR settings.
* Added functional DX12 mock runtime support to the OpenXR package.
* Added `RegisterStatsDescriptor` method to the `OpenXRFeature` abstract class, which lets your OpenXR feature to register a new statistic that can be feed into and queried at runtime. This method is not thread safe.
* Added `SetStatAsFloat` method to the `OpenXRFeature` abstract class, which lets your OpenXR feature to feed a float value into a specific statistic that was previously registered using the `RegisterStatsDescriptor`. This method is thread safe.
* Added `SetStatAsUInt` method to the `OpenXRFeature` abstract class, which lets your OpenXR feature to feed an unsigned integer value into a specific statistic that was previously registered using the `RegisterStatsDescriptor`. This method is thread safe.
* Added the `OpenXRSettings.RefreshRecenterSpace` static method, which lets you regenerate the internal XR space used for recentering, without the need to wait until a recenter event is received by the OpenXR SDK. Note that this method does not perform a recenter event in any way, as this can only be done by the device runtime.
* Added Pre-pass Foveated Rendering for both d3d12 and Vulkan. This feature utilizes Fragment Density Mapping on Quest Devices and Fragment Shading Rate on PC to optimize rendering performance. Refer to [Foveated Rendering](xref:openxr-foveated-rendering) for more information.
* Added `OpenXRUtility.IsSessionFocused` to return if the current session is in the focused state.
* Added `OpenXRUtility.IsUserPresent` to return the change of user presence, such as when the user has taken off or put on an XR headset. If the system does not support user presence sensing, runtime assumes that the user is always present and IsUserPresent always returns True. If the system supports the sensing of user presence, returns true when detected the presence of a user and returns false when detected the absence of a user.

### Changed
* Changed tracking origin to use Local Floor in place of Floor by default.
* Changed delta time calculation to utilize OpenXR for frame estimation. Previously internal Unity mechanisms were used. This feature is currently disabled but will be available in the future.
* When retrieving input feature from [CommonUsages.userPresence](https://docs.unity3d.com/ScriptReference/XR.CommonUsages-userPresence.html), if XR_EXT_user_presence is supported by the current system, retrieved value will be the same as `OpenXRUtility.IsUserPresent`. If `XR_EXT_user_presence` is not supported, retrieved value will be based on `OpenXRUtility.IsSessionFocused`, which is the default return value before 1.11.0.

### Fixed
* Fixed Marshal to correctly read the number of bytes for booleans in C++.
* Fixed crash that occurs while cleaning up destroyed swapchains in OpenXR.
* Fixed Depth Texture flag disparity between the OpenXR and Oculus providers.

## [1.10.0] - 2024-01-26

### Added
* Added `OpenXRSettings.VulkanAdditionalGraphicsQueue` property to request at startup an additional Vulkan graphics queue in devices that require it. The setting can be enabled through the Project settings UI and build code.
* Added `Optimize Buffer Discards (Vulkan)` feature setting option within MetaQuestFeature.
* Added a new optional validation rule to switch to use InputSystem.Controls.StickControl instead of Vector2Control for thumbsticks. StickControl allows more input options for thumbstick-based control, such as acting as both a combined 2D vector, two independent axes or a four-way Dpad with 4 independent buttons.

### Changed
* Updated project validation rule for Meta Quest Feature - "Select Oculus Touch Interaction Profile or Meta Quest Pro Touch Interaction Profile to pair with." from error to warning to allow other interaction profiles to be enabled as well, like eye gaze or palm pose.

### Fixed
* Fixed Meta XR `Space Warp` feature crashing issue when trying to access the depth swapchain.
* Fixed Meta XR `Space Warp` depth subimage imageArrayIndex setup wrong issue for multiview render mode.
* Fixed a bug where Palm Pose won't work if the Eye Gaze Interaction Profile is added to the project.
* Fixed "The type 'AnalyticsResult' is defined in an assembly that is not referenced" error thrown.
* Fixed UnitySwapchain destructor to completely destroy swapchains to avoid memory leak.
* Fixed OpenXR Settings UI to not render settings on platforms that aren't supported.
* Fixed Android manifest entries were added when the feature was not enabled.

## [1.9.1] - 2023-10-19

### Added
* Implemented `InputSystem.customBindingPathValidators` interface to show UI warnings in the `InputAsset` Editor when added bindings to a disabled interaction profile and when none of the controller type interaction profiles are enabled, but added bindings using generic XRController.
* Added APIs `GetInteractionProfileType` and `GetDeviceLayoutName` to support binding path validator feature.
* Added API `TrySetControllerLateLatchAction` to support controller late latching. Also added MarkLateLatchNode.cs in Controller Sample for usages example.
* Added support for Meta XR feature `Symmetric Projection`, `Space Warp`, and `Eye Tracked Foveation`.
* Added the option to enable `Subsampled Layout` when creating a Vulkan swapchain.
* Added support for recentering Floor tracking origin using new API, `OpenXRSettings.SetAllowRecentering`. This static method allows the app to recenter the tracking origin space when requested by the runtime. Note that calling the method won't trigger a recenter event.
* Added support for Meta XR feature `System Splash Screen`.
* Added public API for MetaQuestFeature.forceRemoveInternetPermission.

### Changed
* Updated Input System package dependency to 1.6.3.
* Updated XR management dependency to 4.4.0.
* Updated OpenXR loader to 1.0.29.
* Changed MockRuntime and its FeatureSet to public.

### Fixed
* Fixed incorrect XR Validation rules after regenerating the OpenXR package settings asset.
* Fixed analytics code being called when disabled.
* Fixed latency issue with controller poses.
* Fixed an XR_ERROR_SESSION_NOT_RUNNING error if xrRequestExitSession is called when the session is not running.

## [1.8.2] - 2023-07-18

### Added
* Added a new optional validation rule to recommend disabling Screen Space Ambient Occlusion render feature in UniversalRenderer assets to avoid significant performance overhead.

### Changed
* Removed `Windows Mixed Reality feature group` from Windows build target.

### Fixed
* Fixed issue that UWP player tries to initialize XR when "Initialize XR on Startup" is unchecked.
* Fixed `SetOutput Failed.` and `SetInput Failed.` log spamming issue when audio sources is not available.
* Fixed performance drop after headset becomes inactive.
* Fixed Hand Common Poses Interaction Profile and Palm Pose feature documentation links broken.
* Fixed Mock Runtime option toggles on accidentally after entering Play Mode.
* Fixed hand tracking position and rotation not being tracked with XR Hand device.

## [1.8.1] - 2023-06-09

### Added
* Added functionality in `OpenXRFeatureBuildHooks` exposing the BootConfig for reading/writing.
* Added `Force Remove Internet Permission` setting to the Meta Quest Feature settings, allowing to remove Internet permissions added automatically to the application manifest.
* Added class HPReverbG2ControllerProfile.ReverbG2Controller and a new interaction profile to support the HP Reverb G2 controllers.
* Added Hand Interaction Profile and added PalmPose and dpad EXT implementations.

### Changed
* Modified `ModifyAndroidManifestMeta` class to provide required Android manifest entries using a new internal XR system, instead of manually modifying the manifest file.
* Updated the IsTracked and TrackingState Input Features names with a Usage prefix to prevent name collisions.
* Updated OpenXR loader to 1.0.27.

### Fixed
* Fixed type `XrCompositionLayerPassthroughFB` not appearing in the runtime debugger.
* Fixed crash when deploying to Android on Unity 2023 due to using type `Activity` over `GameActivity`.
* Fixed issue on Android where screen captures only capture the view on one eye.

## [1.7.0] - 2023-02-21

### Added
* Added API `OpenXRRuntime.retryInitializationOnFormFactorErrors` to retry xrGetSystem during initialization if xrGetSystem returns a form factor error.
* Enable XR_META_performance_metrics. This enables performance stats for Meta Quest devices on OpenXR.
* Add class MetaQuestTouchProControllerProfile.QuestProTouchController new interaction profile to support Meta Quest pro controllers.
* Added ability for OpenXRFeature derived classes to add Awake() functions.
* Added API `OpenXRInput.GetActionIsActive` to check whether an InputAction has any bindings which are currently active.
* Added API `OpenXRInput.GetActionHandle` to get the action handle of an InputAction and returns 0 if not found.

### Changed
* Updated documentation for the Meta Quest feature.

### Fixed
* Fixed - Meta builds now don't include Bluetooth permissions in Android manifest by default when using Microphone class in script code.
* Fixed crash in OpenXR runtime debugger when cache size is set to 0.
* Fixed OpenXR project validation to check for correct versions of OpenGLES in Unity 2023 and up.
* Fixed crash when runtime reports an invalid view configuration from xrWaitFrame.
* Fixed - OpenXR plugin will only look up functions from supported and enabled extensions.
* Fixed GPU selection in multi-GPU scenarios.

## [1.6.0] - 2022-11-29
### Added
* Added `Meta Quest Feature` support for Android and deprecated previous `Oculus Quest Feature`. Also added a new validation rule to support new Meta Quest Feature migration.
* Added API `MetaQuestFeature.AddTargetDevice` to add additional target devices to the devices list in the MetaQuestFeatureEditor.
* Added Thumbrest Touch support in Oculus Touch Controller Interaction Profile.
* Added a new optional validation rule to switch to use InputSystem.XR.PoseControl instead of OpenXR.Input.PoseControl, which fixed PoseControl conflicts in InputSystem.XR and OpenXR.Input. Note: If opt in to use InputSystem.XR.PoseControl, `USE_INPUT_SYSTEM_POSE_CONTROL` will be added in Scripting Define Symbols under Player settings.

### Changed
* Updated Input System dependency to 1.4.4.

### Fixed
* Fixed issue where game window would show as black in URP.
* Fixed `InputDevice.TryGetHapticCapabilities` always returning True with OpenXR.
* Fixed repeated "Failed to restart OpenXR" warnings when no HMD is attached.
* Fixed invalid pose values getting populated when tracked flags are invalid.
* Fixed XR_SPACE_BOUNDS_UNAVAILABLE return code marked as Error in the log.

## [1.5.2] - 2022-09-08
### Added
* Added support for Android cross-vendor loader.

### Changed
* Updated Input System dependency to 1.4.2.

### Fixed
* Fixed `XRInputSubsystem.TryGetBoundaryPoints` returning inaccurate values. If you have guardian/boundary setup in the headset, `TryGetBoundaryPoints` will return a List<Vector3> of size 4 representing the four vertices of the Play Area rectangle, which is centered at the origin and edges corresponding to the X and Z axes of the provided space. Not all systems or spaces may support boundaries.
* Fixed an issue that controllers position not getting updated and stuck to the floor level when Oculus Integration Asset installed in the project.
* Fixed an issue that OpenXR libraries were included in build when OpenXR SDK is not enabled.
* Improved domain reload performance by removing unnecessary checks when entering Playmode.

## [1.5.1] - 2022-08-11
### Added
* Added generic Project Validation status in the **Project Settings** window under **XR Plug-in Management** if you have [XR Core Utilities](https://docs.unity3d.com/Packages/com.unity.xr.core-utils@latest) 2.1.0 or later installed. These results include the checks for all XR plug-ins that provide validation rules.
* Added API `OpenXRFeature.SetEnvironmentBlendMode` to set the current XR Environment Blend Mode if it is supported by the active runtime. If not supported, fall back  to the runtime preference.
* Added API `OpenXRFeature.GetEnvironmentBlendMode` to return the current XR Environment Blend Mode.
* Added support for `XR_MSFT_holographic_windown_attachment` extension on UWP so that installing Microsoft Mixed Reality OpenXR Plug-in is no longer required if targeting HoloLens V2 devices. And removed corresponding project validator.
* Added support for `XR_FB_foveation`, `XR_FB_foveation_configuration`, `XR_FB_swapchain_update_state`, `XR_FB_foveation_vulkan` and `XR_FB_space_warp` extensions.
* Added ability to recover the application after Oculus Link was aborted and re-established. Attempt to restart every 5 seconds after Link disconnected.
* Added validation rule for duplicate settings in OpenXRPackageSettings.asset.

### Changed
* Updated Oculus Android manifest with focusAware, supportedDevices and headTracking tags added. Also added a new validation rule to check if Oculus target devices are selected.
* Updated OpenXR Loader to 1.0.23.
* Updated Input System dependency to 1.4.1.

### Fixed
* Fixed compilation errors on Game Core platforms where `ENABLE_VR` is not currently defined. Requires Input System 1.4.0 or newer.
* Fixed an issue that was causing Oculus Android Vulkan builds rendering broken after sleep / awake HMD.
* Fixed Oculus controllers tracking issues when both OpenXR package and Oculus package are installed in the project.
* Fixed an issue in Play Mode OpenXR Runtime selection that `Other` option would be reverted to `System Default` after entering and exiting playmode.
* Fixed a bug in `XR_VARJO_quad_views` support.

## [1.4.2] - 2022-05-12
### Fixed
* Fixed unnecessary destroying session on pause and resume.

## [1.4.1] - 2022-04-13
### Added
* Added runtime failures handling to completely shut down OpenXR when runtime error occurred.
* Added support to dynamically discover runtimes by registry key.
* Added logging for no MainCamera tag detected when depthSubmission mode enabled.
* Added console error logging if entering playmode on unsupported platforms.
* Added support to automatically open OpenXR project validator if any issues detected after package update.
* Added API `OpenXRFeature.GetViewConfigurationTypeForRenderPass`, which returns viewConfigurationType for the given renderPass index.
* Added pre-init support for UWP / WSA platform. Note: OpenXR got unchecked by upgrading to this version (only on UWP), but options chosen under `Features` remained as they were.

### Changed
* Updated OpenXR Loader to 1.0.20.
* Updated Render Mode naming to Single Pass Instanced / Multiview for Android platform.
* Updated Input System dependency to 1.3.0.
* Updated XR mirror view to be based on the occlusion mesh line loop data obtained from `xrGetVisibilityMaskKHR`.

### Fixed
* Fixed an issue that would cause failure to load OpenXR loader when non-ascii characters in project path.
* Fixed an editor crash issue when updating OpenXR package version and then enter Playmode.
* Fixed `EyeGaze` functionality not working in the `Controller` sample.
* Fixed Oculus `MenuButton` not being recognized in script.
* Fixed an issue that some OpenXR Editor settings not being serialized properly.
* Fixed `Failed to suggest bindings for interaction profile` console error spamming when a runtime doesn't support a certain interaction profile.

## [1.3.1] - 2021-11-17
### Changed
* Updated documentation link to 1.3.
* Updated Oculus Android manifest with intent-filter.

### Fixed
* Fixed an issue in `OpenXRRestarter` that would prevent a subsequent restart.
* Fixed an issue in `OpenXRRestarter` that would cause a restart to fail depending on where it was initiated in the stack.
* Fixed an issue in UWP that would prevent the main window from being rendered to when using remoting.
* Fixed incorrect negative values on controller linear velocities.
* Fixed a bug in HMD trackingState that would cause tracking state to flip back and forth between two states every frame.

## [1.3.0] - 2021-10-20
### Added
* Added API `OpenXRInput.SendHapticImpulse`
* Added API `OpenXRInput.StopHaptics`
* Added API `OpenXRInput.TryGetInputSourceName`
* Added event `OpenXRRuntime.wantsToRestart`
* Added event `OpenXRRuntime.wantsToQuit`
* Added support for `XR_OCULUS_audio_device_guid` extension.
* Added `Haptic` control to OpenXR Controller Profile layouts that can be bound to an action and used to trigger haptics using the new `OpenXRInput.SendHapticImpulse` API.

### Changed
* Improved `OpenXRFeatureSetManager.SetFeaturesFromEnabledFeatureSets` so that no internal methods are required after calling this.
* Updated `Controller` sample to improve the visuals and use the new APIs.

### Fixed
* Fixed ARM32 crash when OpenXR API layers were present
  * [ISSUE-1355859](https://issuetracker.unity3d.com/issues/xr-uwp-any-openxr-layer-will-crash-unityopenxr-dot-dll-on-hololens2-when-compiling-with-arm32)
* Fixed issue that would cause console errors if `OpenXRFeature.enable` was changed while  the OpenXR Projects Settings windows was open.
* Fixed potential Android crash when shutting down.
* Fixed potential crash with `XR_MSFT_SECONDARY_VIEW_CONFIGURATION_EXTENSION_NAME`
* Fixed issue with alpha channel on Projection layer causing visual artifacts.
  * [ISSUE-1338699](https://issuetracker.unity3d.com/issues/xr-openxr-a-black-background-is-rendered-in-hmd-when-using-solid-color-camera-clear-flags)
  * [ISSUE-1349271](https://issuetracker.unity3d.com/issues/xr-sdk-openxr-post-processing-postexposure-effect-has-visual-artifacts)
  * [ISSUE-1346455](https://issuetracker.unity3d.com/issues/xr-sdk-openxr-right-eye-contains-artifacts-when-taa-anti-aliasing-is-used-with-multi-pass-rendering-mode)
  * [ISSUE-1336968](https://issuetracker.unity3d.com/issues/xr-openxr-oculus-post-processing-view-in-hmd-is-darker-and-sometimes-flickers-when-changing-depth-of-field-focus-distance)
  * [ISSUE-1338696](https://issuetracker.unity3d.com/issues/xr-sdk-openxr-black-edges-are-rendered-on-the-game-objects-when-fxaa-anti-aliasing-is-used)
* Fixed bug that caused Vulkan support on Oculus to stop working.
* Fixed missing usages on `HoloLensHand` layout.
* Fixed vulkan shared depth buffer on versions `2021.2.0b15` and `2022.1.0a8` or higher.

## [1.2.8] - 2021-07-29
### Fixed
* Fixed an issue that was causing Oculus Android OpenGL builds to stop working after v31 of the oculus software was installed.
* Fixed a bug that would cause Asset Bundles to fail building in some circumstances when OpenXR was included in the project.
* Fixed a crash that would occur if XR was shut down from within a Feature callback.
* Fixed a bug that was causing duplicate entries in the OpenXR Package Settings file.
* Fixed a bug causing angular velocities on both the HMD and controllers to have the wrong sign when compared to the other Unity XR plugins.

## [1.2.3] - 2021-06-17
### Added
* Ensured a deterministic order of features within the OpenXR Settings.

### Changed
* Updated OpenXR Loader to 1.0.17.
* Changed `feature set` text to `feature group` in the top level XR-Management list.

### Fixed
* Fixed missing haptic output on HTC Vive controller profile.
* Fixed bug that caused `ThumbstickTouched` binding on the `ValveIndex` controller profile to not work.
* Fixed issue that would cause the app to not exit when closed by the runtime.

## [1.2.2] - 2021-06-01
### Added
* Editor runtime override will no longer change the runtime for standalone builds executed using `Build and Run`.

### Changed
* Renamed `Feature Sets` to `Feature Groups` in the UI.

### Removed
* Removed unnecessary build hook for `EyeGaze` that was causing incorrect capabilities to be set on `HoloLens2`.

### Fixed
* Fixed a bug when using SteamVR runtime that would cause the view to stop rendering and input to stop tracking if the main thread stalled for too long.
* Fixed bug with feature sets that could cause the XR Management check box to be out of sync with the checkbox on the OpenXR Settings page.
* Fixed bug with HTC Vive controller profile that caused the `aim` and `grip` poses to be identical.

## [1.2.0] - 2021-05-06
### Added
* Enabled Android build target for Oculus Quest via the `Oculus Quest Support` feature.
* OpenXR Settings UI reworked to make managing features and interaction profiles easier.
* Added menu item to open Project Validation window (`Window > XR > OpenXR > Project Validation`).
* Project validation window now supports manual fixes for an issue.
* Project validation window now supports optional help links for an issue.
* Added `OpenXRFeature.OnEnableChanged` method to give features a chance to handle their enabled state changing.
* Added `IPackageSettings.GetFeatures` method that returns all features of a given type from all build targets.

### Removed
* Removed `experimental` text from OpenXR plugin help icon.
* Removed `Linear` color space restriction for all build targets and graphics apis except OpenGLES.

### Fixed
* Fixed bug with haptics that caused `XRControllerWithRumble.SendImpulse` to not work with `OpenXR`.
* Fixed bug that could cause some interaction profile device layouts to not be registered on startup.

## [1.1.1] - 2021-04-06
### Added
* Oculus controller profile now exposes both grip and aim poses.
* Added support for `XR_VARJO_QUAD_VIEWS` extension.
* Added `XR_COMPOSITION_LAYER_UNPREMULTIPLIED_ALPHA_BIT` and `XR_COMPOSITION_LAYER_BLEND_TEXTURE_SOURCE_ALPHA_BIT` bits to the composition layer flags.
* Added `XrSecondaryViewConfigurationSwapchainCreateInfoMSFT` to to `XrSwapchainCreateInfo` when using a secondary view.
* MockRuntime First Person Observer View support
* MockRuntime input support
* MockRuntime vulkan_enable2 support
* MockRuntime d3d11_enable support

### Changed
* `OpenXRSettings.renderMode` and `OpenXrSettings.depthSubmissionMode` can now be changed via script outside of play mode.

### Fixed
* Fixed issue where OpenXR layouts were not visible in the InputSystem bindings dialog.
* Fix for managed stripping levels of Medium and High
* Fixed bugs in `XR_KHR_VULKAN_ENABLE2` extension support

## [1.0.2] - 2021-02-04
### Fixed
* Resolve further release verification issues.

## [1.0.1] - 2021-02-03
### Fixed
* Resolve release verification issues.

## [1.0.0] - 2021-01-27
### Added
* Runtime Debugger to allow for the inspection of OpenXR calls that occur while OpenXR is actively running.
* XR Plug-In Management dependency update to 4.0.1
* Input System dependency updated to 1.0.2

## [0.1.2-preview.3] - 2021-01-19
### Added
* Implemented `XR_KHR_loader_init` and `XR_KHR_loader_init_android`.
* Added support for `XR_KHR_vulkan_enable2` alongside `XR_KHR_vulkan_enable`.

### Changed
* Updated dependency of `com.unity.xr.management` from `4.0.0-pre.2` to `4.0.0-pre.3`.

## [0.1.2-preview.2] - 2021-01-05
### Fixed
* Fix publishing pipeline.

## [0.1.2-preview.1] - 2020-12-18
### Changed
* Minor documentation getting started tweaks.
* Minor diagnostic logging tweaks.

### Fixed
* Fixed package errors when Analytics package is absent (case 1300418).
* Fixed tracking origin issue which was causing wrong camera height (case 1300457).
* Fixed issue where large portions of the world were incorrectly culled at certain camera orientations.
* Fixed potential error message when clicking `Fix All` in OpenXR Project Validation window.
* Fixed an issue with sample importing.

## [0.1.1-preview.1] - 2020-12-16

### This is the first release of *OpenXR Plugin \<com.unity.xr.openxr\>*.
