using UnityEngine.UI;

namespace UnityEngine.XR.OpenXR.Samples.ControllerSampleXRInput
{
    public class ActionToVector2SliderXRInput : ActionToControlXRInput<Vector2>
    {
        public Slider xAxisSlider => m_XAxisSlider;
        public Slider yAxisSlider => m_YAxisSlider;

        [SerializeField]
        Slider m_XAxisSlider = null;

        [SerializeField]
        Slider m_YAxisSlider = null;

        Graphic m_Graphic;
        Graphic[] m_Graphics;

        void Start()
        {
            if (m_XAxisSlider == null)
                Debug.LogWarning("ActionToSlider Monobehaviour started without any associated X-axis slider.  This input won't be reported.", this);

            if (m_YAxisSlider == null)
                Debug.LogWarning("ActionToSlider Monobehaviour started without any associated Y-axis slider.  This input won't be reported.", this);

            m_Graphic = gameObject.GetComponent<Graphic>();
            m_Graphics = gameObject.GetComponentsInChildren<Graphic>();
        }

        void Update()
        {
            if (m_XAxisSlider != null && m_YAxisSlider != null && device.isValid)
            {
                bool retrieved = device.TryGetFeatureValue(usage, out var vec);
                SetVisible(retrieved);

                if (!retrieved)
                    vec = Vector2.zero;

                xAxisSlider.value = vec.x;
                yAxisSlider.value = vec.y;
            }
            else
            {
                SetVisible(false);
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
