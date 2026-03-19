---
uid: openxr-microsoft-mixed-reality-motion-controller-profile
---
# Microsoft Mixed Reality Motion Controller Profile

Enables the OpenXR interaction profile for the Microsoft Mixed Reality Motion controller and exposes the `<WMRSpatialController>` device layout within the [Unity Input System](xref:input-system-index).

For more information about the Microsoft Mixed Reality Motion Controller interaction profile, refer to the [OpenXR Specification](https://www.khronos.org/registry/OpenXR/specs/1.0/html/xrspec.html#_microsoft_mixed_reality_motion_controller_profile).

## Available controls

The following table outlines the mapping between the OpenXR paths and Unity's implementation:

| OpenXR Path | Unity Control Name | Type |
|----|----|----|
|`/input/thumbstick`| joystick | Vector2 |
|`/input/trackpad`| touchpad | Vector2 |
|`/input/squeeze/click`| grip | Float (Boolean cast to float) |
|`/input/squeeze/click`| gripPressed | Boolean |
|`/input/menu/click`| menu | Boolean |
|`/input/trigger/value`| trigger | Float |
|`/input/trigger/value`| triggerPressed | Boolean (float cast to Boolean) |
|`/input/thumbstick/click`| joystickClicked | Boolean |
|`/input/trackpad/click`| touchpadClicked | Boolean |
|`/input/trackpad/touch`| touchpadTouched | Boolean |
|`/input/grip/pose` | devicePose | Pose |
|`/input/aim/pose` | pointerPose | Pose |
|`/output/haptic` | haptic | Vibrate |
| Unity Layout Only  | isTracked | Flag Data |
| Unity Layout Only  | trackingState | Flag Data |
| Unity Layout Only  | devicePosition | Vector3 |
| Unity Layout Only  | deviceRotation | Quaternion |
| Unity Layout Only  | pointerPosition | Vector3 |
| Unity Layout Only  | pointerRotation | Quaternion |

[!include[](snippets/unity-layout.md)]
