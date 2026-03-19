using System.Collections;
using UnityEngine.UI;
using UnityEngine.XR.OpenXR.Input;

namespace UnityEngine.XR.OpenXR.Samples.ControllerSampleXRInput
{
    public abstract class ActionToControlXRInput<TUsageType> : MonoBehaviour
    {
        [SerializeField]
        XRNode m_XRNode;

        [SerializeField]
        string m_UsageString;

        [SerializeField]
        [Tooltip("Optional text element that will be set to the name of the action")]
        Text m_Text;

        protected InputDevice device { get; private set; }
        protected InputFeatureUsage<TUsageType> usage { get; private set; }

        protected virtual void OnEnable()
        {
            usage = new InputFeatureUsage<TUsageType>(m_UsageString);
            StartCoroutine(SearchForDevice());
        }

        IEnumerator SearchForDevice()
        {
            if (m_Text != null)
                m_Text.text = m_UsageString;

            while (isActiveAndEnabled)
            {
                device = InputDevices.GetDeviceAtXRNode(m_XRNode);
                if (device.isValid)
                    break;

                yield return new WaitForSeconds(1.0f);
            }
        }
    }
}
