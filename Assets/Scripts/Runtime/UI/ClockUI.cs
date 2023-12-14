using System;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace BraveBloodMonsterHunt.UI
{
    public class ClockUI : MonoBehaviour
    {
        [SerializeField, Required] private TextMeshProUGUI clockText;
        [SerializeField] private bool isEnableTimer = true;

        [ShowNonSerializedField] private float currentTimer;

        /// <summary>
        /// set is enable clock timer
        /// </summary>
        /// <param name="enable">true -> enable timer, false => disable timer</param>
        public void SetIsEnableTimer(bool enable)
        {
            isEnableTimer = enable;
        }
        
        /// <summary>
        /// update timer
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private string UpdateTimer(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60);
            int second = Mathf.FloorToInt(time % 60);

            return $"{minutes:00}:{second:00}";
        }

        private void Update()
        {
            if (isEnableTimer)
            {
                currentTimer += Time.deltaTime;
                clockText.text = UpdateTimer(currentTimer);
            }
        }

        /// <summary>
        /// reset timer
        /// </summary>
        public void ResetTimer()
        {
            currentTimer = 0.0f;
        }
    }
}