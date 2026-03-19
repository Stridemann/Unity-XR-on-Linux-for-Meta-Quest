using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.OpenXR.Input;

namespace UnityEngine.XR.OpenXR.Samples.ControllerSampleXRInput
{
    public class MarkLateLatchNodeXRInput : MonoBehaviour
    {
        [SerializeField]
        XRDisplaySubsystem.LateLatchNode m_LateLatchNode;

        // Set one pose type usage for each hand to be late latched - e.g.: Aim, AimPosition, Grip, or GripPosition. For head node, leave it unset.
        [SerializeField]
        string m_UsageName;

        InputDevice m_Device;
        XRDisplaySubsystem m_DisplaySubsystem;

        bool TryEnsureLateLatchCalled()
        {
            if (m_Device.isValid)
                return true;

            m_Device = InputDevices.GetDeviceAtXRNode(ConvertLateLatchNodeToXRNode(m_LateLatchNode));
            if (!m_Device.isValid)
                return false;

            if (!string.IsNullOrEmpty(m_UsageName))
                OpenXRInput.TrySetControllerLateLatchAction(m_Device, m_UsageName);

            return true;
        }

        XRNode ConvertLateLatchNodeToXRNode(XRDisplaySubsystem.LateLatchNode node)
        {
            switch (node)
            {
                case XRDisplaySubsystem.LateLatchNode.Head:
                    return XRNode.Head;

                case XRDisplaySubsystem.LateLatchNode.LeftHand:
                    return XRNode.LeftHand;

                case XRDisplaySubsystem.LateLatchNode.RightHand:
                    return XRNode.RightHand;

                default:
                    return XRNode.Head;
            }
        }

        void Start()
        {
            List<XRDisplaySubsystem> subsys = new List<XRDisplaySubsystem>();
            SubsystemManager.GetSubsystems<XRDisplaySubsystem>(subsys);

            if (subsys.Count >= 1)
                m_DisplaySubsystem = subsys[0];
        }

        void Update()
        {
            if (!TryEnsureLateLatchCalled() || m_DisplaySubsystem == null)
                return;

            transform.position += new Vector3(0.00001f, 0, 0);
            Quaternion rot = transform.rotation;
            rot.x += 0.00001f;
            transform.rotation = rot;
            m_DisplaySubsystem.MarkTransformLateLatched(transform, m_LateLatchNode);
        }
    }
}
