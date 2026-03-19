using UnityEngine.UI;

namespace UnityEngine.XR.OpenXR.Samples.ControllerSampleXRInput
{
    public class ActionToAxisXRInput : ActionToControlXRInput<float>
    {
        [SerializeField]
        Slider m_Slider;

        void Update()
        {
            if (m_Slider != null && device.isValid && device.TryGetFeatureValue(usage, out var axisValue))
                m_Slider.value = axisValue;
        }
    }
}
