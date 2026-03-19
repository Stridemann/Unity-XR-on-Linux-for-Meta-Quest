using UnityEngine.XR.OpenXR.Input;

namespace UnityEngine.XR.OpenXR.Samples.ControllerSampleXRInput
{
    public class ActionToHapticsXRInput : ActionToControlXRInput<bool>
    {
        public float amplitude
        {
            get => m_Amplitude;
            set => m_Amplitude = value;
        }

        public float duration
        {
            get => m_Duration;
            set => m_Duration = value;
        }

        public float frequency
        {
            get => m_Frequency;
            set => m_Frequency = value;
        }

        [SerializeField]
        float m_Amplitude = 1.0f;

        [SerializeField]
        float m_Duration = 0.1f;

        [SerializeField]
        float m_Frequency = 0.0f;

        bool m_WasPressedLastFrame;

        void Update()
        {
            if (!device.isValid)
                return;

            if (!device.TryGetFeatureValue(usage, out var isPressed))
            {
                m_WasPressedLastFrame = false;
                return;
            }

            if (isPressed && !m_WasPressedLastFrame)
                OpenXRInput.SendHapticImpulse(device, m_Amplitude, m_Duration, m_Frequency);

            m_WasPressedLastFrame = isPressed;
        }
    }
}
