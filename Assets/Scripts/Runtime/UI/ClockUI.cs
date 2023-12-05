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

        public void SetIsEnableTimer(bool enable)
        {
            isEnableTimer = enable;
        }
        
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
                clockText.text = UpdateTimer(Time.time);
            }
        }
    }
}