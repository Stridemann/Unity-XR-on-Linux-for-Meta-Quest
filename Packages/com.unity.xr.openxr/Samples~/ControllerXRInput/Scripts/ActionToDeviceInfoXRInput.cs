using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.XR;

namespace UnityEngine.XR.OpenXR.Samples.ControllerSampleXRInput
{
    public class ActionToDeviceInfoXRInput : MonoBehaviour
    {
        [SerializeField]
        XRNode m_XRNode;

        [SerializeField]
        Text m_Text = null;

        InputDevice m_Device;
        List<InputFeatureUsage> m_UsagesReuse;

        void OnEnable() => m_UsagesReuse = new List<InputFeatureUsage>();

        void Update()
        {
            if (!m_Device.isValid)
            {
                m_Device = InputDevices.GetDeviceAtXRNode(m_XRNode);
                if (!m_Device.isValid)
                    return;
            }

            m_Device.TryGetFeatureUsages(m_UsagesReuse);
            m_Text.text = $"{m_Device.name}\n{string.Join(",", m_UsagesReuse.Select(u => u.name))}";
        }
    }
}
