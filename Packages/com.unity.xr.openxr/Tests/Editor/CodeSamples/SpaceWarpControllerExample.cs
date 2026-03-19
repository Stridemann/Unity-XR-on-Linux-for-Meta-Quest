#region SpaceWarpControllerExample
using UnityEngine;
using UnityEngine.XR.OpenXR;
using UnityEngine.XR.OpenXR.Features;

namespace UnityEngine.XR.OpenXR.CodeSamples.Editor.Tests
{
    // Add to main scene camera
    public class SpaceWarpController : MonoBehaviour
    {
        // The SpaceWarpFeature object that corresponds to the
        // feature "Application SpaceWarp" in:
        // "Project Settings -> XR Plug-in Management -> OpenXR"
        private SpaceWarpFeature m_SpaceWarpFeature = null;

        // true if "Application SpaceWarp" feature is enabled
        // in Project Settings
        private bool m_SpaceWarpFeatureEnabled = false;

        void Start()
        {
            // Turn on SpaceWarp when scene loads if it is
            // enabled in Project Settings
            m_SpaceWarpFeature =
                OpenXRSettings.Instance.GetFeature<SpaceWarpFeature>();
            if (m_SpaceWarpFeature != null)
                m_SpaceWarpFeatureEnabled = m_SpaceWarpFeature.enabled;

            SpaceWarpFeature.SetSpaceWarp(m_SpaceWarpFeatureEnabled);
        }

        void Update()
        {
            // Update SpaceWarp with camera position and rotation
            // if it is enabled in Project Settings.
            // Note, Depending on the headset, SpaceWarp may not need
            // to be updated with the main camera’s current position
            // or rotation. Refer to the headset’s specifications to
            // determine if it’s required. If the headset *does not*
            // require SpaceWarp to be updated with the main camera's
            // current position and rotation, then comment out the
            // following code.
            if (m_SpaceWarpFeatureEnabled)
            {
                SpaceWarpFeature.SetAppSpacePosition(transform.position);
                SpaceWarpFeature.SetAppSpaceRotation(transform.rotation);
            }
        }
    }
}
#endregion
// Used in Documentation~/features/spacewarp.md
// This example demonstrates how to enable and update Application SpaceWarp at runtime.
