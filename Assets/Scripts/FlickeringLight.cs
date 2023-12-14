using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace DefaultNamespace
{
    public class FlickeringLight : MonoBehaviour
    {
        [SerializeField] private Light2D light2D;

        [SerializeField, MinMaxSlider(0.0f, 10.0f)]
        private Vector2 minMaxIntensity = new Vector2(0.5f, 1.0f);

        [SerializeField] private float flickerSpeed = 2.0f;

        [SerializeField] private bool isFlickering;
        
        private float m_CurrentIntensity;

        private void Start()
        {
            m_CurrentIntensity = light2D.intensity;
        }

        private void LateUpdate()
        {
            if (isFlickering)
            {
                m_CurrentIntensity += Time.deltaTime * flickerSpeed * Random.Range(-1f, 1f);
                m_CurrentIntensity = Mathf.Clamp(m_CurrentIntensity, minMaxIntensity.x, minMaxIntensity.y);

                light2D.intensity = m_CurrentIntensity;
            }
        }
    }
}