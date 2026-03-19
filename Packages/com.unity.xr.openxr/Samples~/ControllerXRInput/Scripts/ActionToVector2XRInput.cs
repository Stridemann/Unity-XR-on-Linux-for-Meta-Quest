using UnityEngine.UI;

namespace UnityEngine.XR.OpenXR.Samples.ControllerSampleXRInput
{
    public class ActionToVector2XRInput : ActionToControlXRInput<Vector2>
    {
        [SerializeField]
        RectTransform m_Handle;

        void Update()
        {
            if (m_Handle != null && device.isValid && device.TryGetFeatureValue(usage, out var vec))
                m_Handle.anchorMin = m_Handle.anchorMax = (vec + Vector2.one) * 0.5f;
        }
    }
}
