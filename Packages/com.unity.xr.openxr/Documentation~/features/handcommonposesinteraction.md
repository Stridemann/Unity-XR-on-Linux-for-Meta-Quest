---
uid: openxr-hand-common-poses-interaction
---
# Hand Common Poses Interaction

Unity OpenXR provides support for the Hand Interaction extension specified by Khronos. Use this layout to retrieve the bindings data that the extension returns. This extension defines four commonly used action poses for all user hand
interaction profiles including both hand tracking devices and motion controller devices.

Enables the OpenXR interaction feature for Hand Common Poses Interaction and exposes the `<HandInteractionPoses>` layout within the [Unity Input System](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/).

For more information about the hand interaction extension, refer to the [OpenXR Specification](https://registry.khronos.org/OpenXR/specs/1.0/html/xrspec.html#XR_EXT_hand_interaction).

## Available controls

The following table outlines the mapping between the OpenXR paths and Unity's implementation:

| OpenXR Path | Unity Control Name | Type |
|----|----|----|
|`/input/grip/pose` | devicePose | Pose |
|`/input/aim/pose` | pointerPose | Pose |
|`/input/pinch_ext/pose` | pinchPose | Pose |
|`/input/poke_ext/pose` | pokePose | Pose |


