using System.Collections.Generic;
using UnityEngine.UI;

namespace UnityEngine.XR.OpenXR.Samples.ControllerSampleXRInput
{
    public class DisplayDeviceInfoFromActionXRInput : MonoBehaviour
    {
        [SerializeField]
        XRNode m_XRNode;

        [SerializeField]
        GameObject m_RootObject = null;

        [SerializeField]
        Text m_TargetText;

        InputDevice m_Device;
        List<InputFeatureUsage> m_UsagesReuse;

        void OnEnable()
        {
            if (m_UsagesReuse == null)
                m_UsagesReuse = new List<InputFeatureUsage>();

            if (m_TargetText == null)
                Debug.LogWarning("DisplayDeviceInfo Monobehaviour has no Target Text set. No information will be displayed.");
        }

        void Update()
        {
            if (!m_Device.isValid)
            {
                m_Device = InputDevices.GetDeviceAtXRNode(m_XRNode);
                if (!m_Device.isValid)
                {
                    if (m_RootObject != null)
                        m_RootObject.SetActive(false);

                    if (m_TargetText != null)
                        m_TargetText.text = "<No Device Connected>";

                    return;
                }
            }

            if (m_RootObject != null)
                m_RootObject.SetActive(true);

            if (m_TargetText == null)
                return;

            m_TargetText.text = $"{m_Device.name}\n";

            bool useComma = false;
            m_Device.TryGetFeatureUsages(m_UsagesReuse);
            foreach (var usage in m_UsagesReuse)
            {
                if (!useComma)
                {
                    useComma = true;
                    m_TargetText.text += $"{usage}";
                }
                else
                {
                    m_TargetText.text += $"{usage},";
                }
            }

            if (m_TargetText.text.Length > 30)
                m_TargetText.text = m_TargetText.text.Substring(0, 30);
        }
    }
}
