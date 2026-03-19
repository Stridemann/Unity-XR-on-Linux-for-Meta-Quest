---
uid: openxr-meta-quest-plus-touch-controller-profile
---
# Meta Quest Touch Plus Controller Profile

Enables the OpenXR interaction profile for Meta Quest Touch Plus controllers and exposes the `<QuestTouchPlusController>` device layout within the [Unity Input System](xref:input-system-index).

## Available controls

The following table outlines the mapping between the OpenXR paths and Unity's implementation:

| OpenXR Path | Unity Control Name | Type |
|----|----|----|
|`/input/thumbstick`| thumbstick | Vector2 |
|`/input/squeeze/value`| grip | Float |
|`/input/squeeze/value`| gripPressed | Boolean (float cast to Boolean) |
|`/input/menu/click`| menu (Left Hand Only)| Boolean |
|`/input/system/click`| menu (Right Hand Only)| Boolean |
|`/input/a/click`| primaryButton (Right Hand Only) | Boolean |
|`/input/a/touch`| primaryTouched (Right Hand Only) | Boolean |
|`/input/b/click`| secondaryButton (Right Hand Only) | Boolean |
|`/input/b/touch`| secondaryTouched (Right Hand Only) | Boolean |
|`/input/x/click`| primaryButton (Left Hand Only) | Boolean |
|`/input/x/touch`| primaryTouched (Left Hand Only) | Boolean |
|`/input/y/click`| secondaryButton (Left Hand Only) | Boolean |
|`/input/y/touch`| secondaryTouched (Left Hand Only) | Boolean |
|`/input/trigger/value`| trigger | Float |
|`/input/trigger/value`| triggerPressed | Boolean (float cast to Boolean) |
|`/input/trigger/touch`| triggerTouched| Boolean (float cast to Boolean) |
|`/input/thumbstick/click`| thumbstickClicked | Boolean |
|`/input/thumbstick/touch`| thumbstickTouched | Boolean |
|`/input/thumbrest/touch`| thumbrestTouched | Boolean |
|`/input/grip/pose` | devicePose | Pose |
|`/input/aim/pose` | pointerPose | Pose |
|`/input/trigger/force` | triggerForce | Float |
|`/input/trigger/curl_meta` | triggerCurl | Float |
|`/input/trigger/slide_meta` | triggerSlide | Float |
|`/input/trigger/proximity_meta` | triggerProximity | Boolean |
|`/input/thumb_meta/proximity_meta` | thumbProximity | Boolean |
|`/output/haptic` | haptic | Vibrate |
| Unity Layout Only  | isTracked | Flag Data |
| Unity Layout Only  | trackingState | Flag Data |
| Unity Layout Only  | devicePosition | Vector3 |
| Unity Layout Only  | deviceRotation | Quaternion |

[!include[](snippets/unity-layout.md)]
