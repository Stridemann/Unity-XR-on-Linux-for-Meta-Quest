---
uid: openxr-htc-vive-controller-profile
---
# HTC Vive Controller Profile

Enables the OpenXR interaction profile for the HTC Vive Controller and exposes the `<ViveController>` device layout within the [Unity Input System](xref:input-system-index).

For more information about the HTC Vive interaction profile, refer to the [OpenXR Specification](https://www.khronos.org/registry/OpenXR/specs/1.0/html/xrspec.html#_htc_vive_controller_profile).

## Available controls

The following table outlines the mapping between the OpenXR paths and Unity's implementation:

| OpenXR Path | Unity Control Name | Type |
|----|----|----|
|`/input/system/click`| select | Boolean |
|`/input/squeeze/click`| grip | Float ( Boolean value cast to float)|
|`/input/squeeze/click`| gripButton | Boolean |
|`/input/menu/click` | menu | Boolean|
|`/input/trigger/value`|trigger|  Float |
|`/input/trigger/click`|triggerPressed| Boolean |
|`/input/trackpad`|trackpad| Vector2 |
|`/input/trackpad/click`|trackpadClicked| Boolean |
|`/input/trackpad/touch`|trackpadTouched| Boolean |
|`/input/grip/pose`| devicePose| Pose |
|`/input/aim/pose`|pointerPose| Pose |
|`/output/haptic` | haptic | Vibrate |
| Unity Layout Only  | isTracked | Flag Data |
| Unity Layout Only  | trackingState | Flag Data |
| Unity Layout Only  | devicePosition | Vector3 |
| Unity Layout Only  | deviceRotation | Quaternion |

[!include[](snippets/unity-layout.md)]
