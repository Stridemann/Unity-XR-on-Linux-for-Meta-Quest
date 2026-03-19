---
uid: openxr-hand-interaction-profile
---
# Hand Interaction Profile

The hand interaction profile is designed for runtimes which provide hand inputs using hand tracking devices instead of controllers with triggers or buttons.
The hand interaction profile allows hand tracking devices to provide commonly used gestures and action poses. Enable this OpenXR interaction profile to expose the `<HandInteraction>` device layout within the [Unity Input System](xref:input-system-index).

For more information about the hand interaction profile, refer to the [OpenXR Specification](https://registry.khronos.org/OpenXR/specs/1.0/html/xrspec.html#XR_EXT_hand_interaction).

## Available controls

The following table outlines the mapping between the OpenXR paths and Unity's implementation:

| OpenXR Path | Unity Control Name | Type |
|----|----|----|
|`/input/grip/pose` | devicePose | Pose |
|`/input/aim/pose` | pointerPose | Pose |
|`/input/pinch_ext/pose` | pinchPose | Pose |
|`/input/poke_ext/pose` | pokePose | Pose |
|`/input/pinch_ext/value`| pinchValue | Float |
|`/input/pinch_ext/ready_ext` | pinchReady | Boolean|
|`/input/aim_activate_ext/value`| pointerActivateValue | Float |
|`/input/aim_activate_ext/ready_ext` | pointerActivateReady | Boolean|
|`/input/grasp_ext/value`| graspValue | Float |
|`/input/grasp_ext/ready_ext` | graspReady | Boolean|
| Unity Layout Only  | isTracked | Flag Data |
| Unity Layout Only  | trackingState | Flag Data |
| Unity Layout Only  | devicePosition | Vector3 |
| Unity Layout Only  | deviceRotation | Quaternion |

[!include[](snippets/unity-layout.md)]
