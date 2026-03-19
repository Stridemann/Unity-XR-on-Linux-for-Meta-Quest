---
uid: openxr-settings
---
# OpenXR settings reference

Understand the settings you can configure for OpenXR in the **XR Plugin Management** settings.

The OpenXR plug-in provides additional settings you can configure for your project in the **XR Plug-in Management** section of the **Project Settings** window.

Some OpenXR settings require specific project configuration to work. The XR Plug-in Management [Project Validation](xref:um-xr-plugin-management#project-validation) detects and alerts you to issues with your project configuration. To learn about specific configuration requirements and how to use the project validation system, refer to [Configure your project](xref:openxr-project-validation).

The settings that are available depend on your target platform (PC or Android) and [Plug-in providers](xref:um-xr-support-packages). The options that are available for each setting might vary depending on the target plug-in provider.

Provider plug-ins and other XR packages can also add their own settings, which aren’t described in the following sections. For more information about the specific settings available on your target XR platform, refer to the documentation for the relevant package.

The following sections describe the main OpenXR settings.

## Shared settings

OpenXR provides settings that are common to PC and Android platforms.

> [!NOTE]
> The available options might vary between these platforms.

The following table describes the settings that are common to PC and Android platforms:

| **Property**                  | Description |
| :---------------------------- | :---------- |
| **Render Mode**                | Choose the rendering strategy. To learn more, refer to [Set the render mode](xref:openxr-project-config#render-mode).  |
| **Auto Color Submission Mode** | If enabled, Unity uses the default color submission mode for your platform. This is typically `8bpc RGBA/BGRA`. You can choose your own Color Submission Mode if you disable this property, as outlined in [Color Submission Mode](#color-submission-mode).  |
| **Latency Optimization**       | Choose how the OpenXR plug-in minimizes latency for input polling or rendering. <br>The options are: <ul>**Prioritize Rendering**: The time between when a frame is simulated and when the frame is submitted to the device for rendering is minimized. This reduces the extrapolation/warping of the rendered scene and objects. </li><li>**Prioritize Input Polling**: The time between when input is polled and when the frame is submitted to the device for rendering is minimized. This makes interacting with the world feel more responsive.</li> </ul> For more information refer to [Set latency optimization](xref:openxr-project-config#latency-optimization) |
| **Depth Submission Mode**      | Choose how depth information is passed to the renderer. To learn how to choose the appropriate option for your project, refer to [Set the depth submission mode](xref:openxr-project-config#depth-submission-mode).  |
| **Foveated Rendering Api**     | Choose the API if your project uses foveated rendering. To learn more, refer to [Foveated rendering](xref:openxr-foveated-rendering). |
| **Use Open XR Predicted Time** | When enabled, Unity uses OpenXR's time prediction methods to predict the display presentation time of the next frame. OpenXR time prediction ensures that the user's view on the device matches their movement to enhance real-time feedback. **Use OpenXR Time Prediction** results in smoother rendering on OpenXR runtimes through synchronization between application and display rendering.<br>Unity recommends that you enable this setting for smoother rendering on headsets to reduce unwanted effects such as motion sickness. Other time-based manipulations in your project might affect the performance of this setting. |
| **Additional Graphics Queue (Vulkan)** | When enabled, Unity creates an additional graphics queue that the Quest runtime uses for [Mixed Reality Capture](https://developers.meta.com/horizon/resources/mixed-reality-capture-and-casting) (Meta developer documentation). This enables the Quest runtime to dedicate the primary graphics queue to standard rendering tasks, and the additional queue for rendering required by Mixed Reality Capture. Enable **Additional Graphics Queue (Vulkan)** if your project targets Meta Quest runtimes and uses Meta’s Mixed Reality Capture. If your project targets other runtimes, or doesn’t use Mixed Reality Capture on Quest, you can safely disable this setting. |

### Color submission mode

When you disable **Auto Color Submission Mode**, you can choose the **Color Formats** for your **Color Submission Mode**.

To understand the available options and which color submission mode to use for your project, refer to [Set the color submission mode](xref:openxr-project-config#color-submission-mode).

### Enabled interaction profiles

Use the **Enabled Interaction Profiles** section to add the configuration profiles you want to use in your project. To learn about the available OpenXR interaction profiles, visit [Input in Unity OpenXR](xref:openxr-input).

## PC settings

The following options are available only from the **Windows, Linux, Mac** tab:

| **Property** | **Description** |
| :----------- | :-------------- |
| **Play Mode OpenXR Runtime** | Specify a different OpenXR runtime for your computer in Play mode. To learn more refer to [Choose an OpenXR runtime to use in Play mode](xref:openxr-project-config#openxr-runtime).  |

## Android settings

The following options are available only from the **Android** tab:

| **Property** | **Description** |
| :----------- | :-------------- |
| **Offscreen Rendering Only (Vulkan)** | When enabled, the device's runtime will render its own buffers to an offscreen display and Unity won't allocate buffers for the main display. If you disable **Offscreen Rendering Only**, Unity will allocate buffers to blit to the main display. <strong>Note:</strong> If a device can't allocate offscreen buffers when this setting is enabled, the application won't render.<br>This feature is only compatible with Android build targets for XR platforms. You must disable this feature for handheld devices. |

## Additional resources

* [XR plug-in management](xref:um-xr-plugin-management) (Unity User Manual)
* [Project configuration](xref:openxr-project-config)
