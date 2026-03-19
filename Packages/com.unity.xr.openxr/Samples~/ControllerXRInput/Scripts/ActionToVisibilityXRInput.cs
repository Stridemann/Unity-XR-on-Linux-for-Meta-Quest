using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.OpenXR.Input;

namespace UnityEngine.XR.OpenXR.Samples.ControllerSampleXRInput
{
    public class ActionToVisibilityXRInput : MonoBehaviour
    {
        [SerializeField]
        XRNode m_XRNode;

        [SerializeField]
        GameObject m_Target;

        [SerializeField]
        string m_UsageString;

        List<InputFeatureUsage> m_UsagesReuse;

        void OnEnable()
        {
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
                var device = InputDevices.GetDeviceAtXRNode(m_XRNode);
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
