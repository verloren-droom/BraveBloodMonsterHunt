using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

namespace BraveBloodMonsterHunt
{
    public class CameraShaker : MonoBehaviour
    {
        private readonly List<ShakeRequest> shakeRequests = new List<ShakeRequest>();
        private CinemachineBasicMultiChannelPerlin m_Noise;
        [SerializeField] private float m_ShakeDecreaseAmount = 10f;

        public void RequestShake(float amount, float time)
        {
            shakeRequests.Add(new ShakeRequest
            {
                ShakeAmount = amount,
                ShakeTime = time
            });
        }
        private class ShakeRequest
        {
            public float ShakeAmount { get; set; }
            public float ShakeTime { get; set; }
        }

        private void Start()
        {
            m_Noise = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        private void Update()
        {
            if (shakeRequests.Count == 0)
            {
                m_Noise.m_AmplitudeGain = 0;
                return;
            }
            
            var strongestShake = shakeRequests.Max(s => s.ShakeAmount);
            m_Noise.m_AmplitudeGain = strongestShake;

            for (int i = shakeRequests.Count - 1; i >= 0; --i)
            {
                var request = shakeRequests[i];
                request.ShakeTime -= Time.deltaTime;
                if (request.ShakeTime <= 0)
                {
                    request.ShakeAmount -= Mathf.Max(Time.deltaTime * m_ShakeDecreaseAmount, 0);
                }

                shakeRequests.Remove(request);
            }
        }
    }
}