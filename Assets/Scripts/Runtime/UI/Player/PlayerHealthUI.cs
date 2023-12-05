using System;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace BraveBloodMonsterHunt
{
    [DisallowMultipleComponent]
    public class PlayerHealthUI : MonoBehaviour
    {
        [SerializeField, Required] private Slider healthBar;
        [SerializeField, Required] private Health playerHealth;
        
        private void Awake()
        {
            healthBar.maxValue = playerHealth.Value;
            healthBar.value = playerHealth.Value;
        }
        
        public void UpdateUI()
        {
            healthBar.value = playerHealth.Value;
            healthBar.DOValue(playerHealth.Value, 0.5f);
        }
    }
}