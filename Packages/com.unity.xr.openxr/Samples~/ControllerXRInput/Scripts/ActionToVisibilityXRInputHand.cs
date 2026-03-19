using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.OpenXR.Input;

namespace UnityEngine.XR.OpenXR.Samples.ControllerSampleXRInput
{
    public class ActionToVisibilityXRInputHand : MonoBehaviour
    {
        [SerializeField]
        bool m_IsRightHand;

        [SerializeField]
        GameObject m_Target;

        [SerializeField]
        string m_HandDeviceName;

        [SerializeField]
        string m_UsageString;

        List<InputDevice> m_DevicesReuse;
        List<InputFeatureUsage> m_UsagesReuse;

        void OnEnable()
        {
            m_DevicesReuse = new List<InputDevice>();
            m_UsagesReuse = new List<InputFeatureUsage>();

            if (m_Target == null)
                m_Target = gameObject;
            m_Target.SetActive(false);

            StartCoroutine(UpdateVisibility());
        }

        IEnumerator UpdateVisibility()
        {
            while (isActiveAndEnabled)
            {
                InputDevices.GetDevicesWithCharacteristics(
                    m_IsRightHand
                    ? InputDeviceCharacteristics.Right
                    : InputDeviceCharacteristics.Left,
                    m_DevicesReuse);

                var device = new InputDevice();
                for (int deviceIndex = 0; deviceIndex < m_DevicesReuse.Count; ++deviceIndex)
                {
                    if (m_DevicesReuse[deviceIndex].name != m_HandDeviceName)
                        continue;

                    device = m_DevicesReuse[deviceIndex];
                    break;
                }

                if (device.isValid && device.TryGetFeatureUsages(m_UsagesReuse) && m_UsagesReuse.Count > 0)
                {
                    for (int usageIndex = 0; usageIndex < m_UsagesReuse.Count; ++usageIndex)
                    {
                        if (m_UsagesReuse[usageIndex].name == m_UsageString)
                        {
                            m_Target.SetActive(true);
                            break;
                        }
                    }

                    break;
                }

                yield return new WaitForSeconds(1.0f);
            }
        }
    }
}
