using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;
using UnityEngine.SpatialTracking;
using UnityEngine.XR;

namespace UnityEngine.XR.OpenXR.Samples.ControllerSampleXRInput
{
    [RequireComponent(typeof(TrackedPoseDriver))]
    public class PoseProvider : BasePoseProvider
    {
        public override PoseDataFlags GetPoseFromProvider(out Pose output)
        {
            output = Pose.identity;

            if (!m_Device.isValid)
            {
                m_Device = InputDevices.GetDeviceAtXRNode(m_XRNode);
                if (!m_Device.isValid)
                    return PoseDataFlags.NoData;
            }

            var successFlags = PoseDataFlags.NoData;

            if (m_Device.TryGetFeatureValue(m_PositionUsage, out var position))
            {
                output.position = position;
                successFlags |= PoseDataFlags.Position;
            }

            if (m_Device.TryGetFeatureValue(m_RotationUsage, out var rotation))
            {
                output.rotation = rotation;
                successFlags |= PoseDataFlags.Rotation;
            }

            return successFlags;
        }

        void Start()
        {
            m_LastPose = Pose.identity;
            m_Driver = GetComponent<TrackedPoseDriver>();
            m_PositionUsage = new InputFeatureUsage<Vector3>(m_PositionUsageString);
            m_RotationUsage = new InputFeatureUsage<Quaternion>(m_RotationUsageString);
        }

        [SerializeField]
        XRNode m_XRNode;

        [SerializeField]
        string m_PositionUsageString;

        [SerializeField]
        string m_RotationUsageString;

        InputDevice m_Device;
        InputFeatureUsage<Vector3> m_PositionUsage;
        InputFeatureUsage<Quaternion> m_RotationUsage;

        Pose m_LastPose;
        TrackedPoseDriver m_Driver;
    }
}
