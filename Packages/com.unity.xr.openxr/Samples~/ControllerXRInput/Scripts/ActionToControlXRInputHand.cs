using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UnityEngine.XR.OpenXR.Samples.ControllerSampleXRInput
{
    public class ActionToControlXRInputHand<TUsageType> : MonoBehaviour
    {
        [SerializeField]
        bool m_IsRightHand;

        [SerializeField]
        string m_HandDeviceName;

        [SerializeField]
        string m_UsageString;

        [SerializeField]
        [Tooltip("Optional text element that will be set to the name of the action")]
        Text m_Text = null;

        protected InputDevice device { get; private set; }
        protected InputFeatureUsage<TUsageType> usage { get; private set; }

        List<InputDevice> m_DevicesReuse;

        void OnEnable()
        {
            usage = new InputFeatureUsage<TUsageType>(m_UsageString);
            m_DevicesReuse = new List<InputDevice>();
            StartCoroutine(SearchForDevice());
        }

        IEnumerator SearchForDevice()
        {
            if (m_Text != null)
                m_Text.text = m_UsageString;

            while (isActiveAndEnabled)
            {
                InputDevices.GetDevicesWithCharacteristics(
                    m_IsRightHand
                    ? InputDeviceCharacteristics.Right
                    : InputDeviceCharacteristics.Left,
                    m_DevicesReuse);

                for (int deviceIndex = 0; deviceIndex < m_DevicesReuse.Count; ++deviceIndex)
                {
                    if (m_DevicesReuse[deviceIndex].name != m_HandDeviceName)
                        continue;

                    device = m_DevicesReuse[deviceIndex];
                    break;
                }

                if (device.isValid)
                    break;

                yield return new WaitForSeconds(1.0f);
            }
        }
    }
}
