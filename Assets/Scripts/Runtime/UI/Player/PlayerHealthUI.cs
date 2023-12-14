using System;
using BraveBloodMonsterHunt.CombatSystem;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BraveBloodMonsterHunt
{
    [DisallowMultipleComponent]
    public class PlayerHealthUI : MonoBehaviour
    {
        [SerializeField, Required] private Slider healthBar;
        [SerializeField, Required] private Health playerHealth;
        [SerializeField, Required] private TextMeshProUGUI healthText;
        
        private void Awake()
        {
            healthBar.maxValue = playerHealth.Value;
            healthBar.value = playerHealth.Value;
            
            healthText.text = $"{healthBar.value}/{healthBar.maxValue}";
        }
        
        public void UpdateUI()
        {
            healthBar.value = playerHealth.Value;
            healthBar.DOValue(playerHealth.Value, 0.5f);

            healthText.text = $"{healthBar.value}/{healthBar.maxValue}";
        }
    }
}