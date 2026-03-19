using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.Android;
using UnityEngine.XR.OpenXR.Features;

namespace UnityEngine.XR.OpenXR
{
    public partial class OpenXRSettings
    {
#if UNITY_ANDROID
        string m_eyeTrackingQuestPermissionsToRequest = "com.oculus.permission.EYE_TRACKING";
        string m_eyeTrackingAndroidXRPermissionsToRequest = "android.permission.EYE_TRACKING_FINE";

        static void PermissionGrantedCallback(string permissionName)
        {
            if (permissionName == GetInstance(true).m_eyeTrackingQuestPermissionsToRequest || permissionName == GetInstance(true).m_eyeTrackingAndroidXRPermissionsToRequest)
            {
                Internal_SetHasEyeTrackingPermissions(true);
                return;
            }
        }

        static bool IsPermissionGranted(string permissionName)
        {
            return Permission.HasUserAuthorizedPermission(permissionName);
        }
#endif

        void ApplyPermissionSettings()
        {
            // Only Android need specific permissions for eye tracking for now
#if UNITY_ANDROID
            var foveatedRenderingFreature = GetFeature<FoveatedRenderingFeature>();

            if (foveatedRenderingFreature != null && foveatedRenderingFreature.enabled)
            {
                // Check if Meta Quest Feature is enabled
                bool metaQuestFeatureEnabled = false;
                var permissionsToRequest = new List<string>();
                foreach (var feature in ActiveBuildTargetInstance.features)
                {
                    if (String.Compare(feature.featureIdInternal, "com.unity.openxr.feature.metaquest", true) == 0 && feature.enabled)
                    {
                        metaQuestFeatureEnabled = true;
                        break;
                    }
                }
                if (metaQuestFeatureEnabled)
                {
                    if (IsPermissionGranted(m_eyeTrackingQuestPermissionsToRequest))
                        Internal_SetHasEyeTrackingPermissions(true);
                    else
                        permissionsToRequest.Add(m_eyeTrackingQuestPermissionsToRequest);
                }

                if (IsPermissionGranted(m_eyeTrackingAndroidXRPermissionsToRequest))
                    Internal_SetHasEyeTrackingPermissions(true);
                else
                    permissionsToRequest.Add(m_eyeTrackingAndroidXRPermissionsToRequest);
                if (permissionsToRequest.Count > 0)
                {
                    var permissionCallbacks = new PermissionCallbacks();
                    permissionCallbacks.PermissionGranted += PermissionGrantedCallback;
                    Permission.RequestUserPermissions(permissionsToRequest.ToArray(), permissionCallbacks);
                }
            }
#endif
        }

        [DllImport(LibraryName, EntryPoint = "OculusFoveation_SetHasEyeTrackingPermissions")]
        internal static extern void Internal_SetHasEyeTrackingPermissions([MarshalAs(UnmanagedType.I1)] bool value);
    }
}
