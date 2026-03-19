using UnityEngine.UI;

namespace UnityEngine.XR.OpenXR.Samples.ControllerSampleXRInput
{
    public class ActionToSliderXRInput : ActionToControlXRInput<float>
    {
        [SerializeField]
        Slider m_Slider;

        Graphic m_Graphic;
        Graphic[] m_Graphics;

        void Start()
        {
            if (m_Slider == null)
                Debug.LogWarning("ActionToSliderXRInput MonoBehaviour started without any associated slider. This input will not be reported.", this);

            m_Graphic = gameObject.GetComponent<Graphic>();
            m_Graphics = gameObject.GetComponentsInChildren<Graphic>();
        }

        void Update()
        {
            if (m_Slider != null && device.isValid && device.TryGetFeatureValue(usage, out var axisValue))
            {
                SetVisible(true);
                m_Slider.value = axisValue;
            }
            else
            {
                SetVisible(false);
                m_Slider.value = 0f;
            }
        }

        void SetVisible(bool visible)
        {
            if (m_Graphic != null)
                m_Graphic.enabled = visible;

            if (m_Graphics != null)
            {
                for (int graphicIndex = 0; graphicIndex < m_Graphics.Length; ++graphicIndex)
                    m_Graphics[graphicIndex].enabled = visible;
            }
        }
    }
}
