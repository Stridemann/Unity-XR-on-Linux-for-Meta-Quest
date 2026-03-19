using UnityEngine.UI;

namespace UnityEngine.XR.OpenXR.Samples.ControllerSampleXRInput
{
    public class ActionToButtonXRInput : ActionToControlXRInput<bool>
    {
        [SerializeField]
        Image m_Image;

        [SerializeField]
        Color m_NormalColor = Color.red;

        [SerializeField]
        Color m_PressedColor = Color.green;

        void Awake()
        {
            if (m_Image != null)
                m_Image.color = m_NormalColor;
        }

        void Update()
        {
            if (m_Image != null && device.isValid && device.TryGetFeatureValue(usage, out var isPressed))
                m_Image.color = isPressed ? m_PressedColor : m_NormalColor;
        }
    }
}
