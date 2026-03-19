using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UnityEngine.XR.OpenXR.Samples.ControllerSampleXRInput
{
    public class ActionToAxisXRInputHand : ActionToControlXRInputHand<float>
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
