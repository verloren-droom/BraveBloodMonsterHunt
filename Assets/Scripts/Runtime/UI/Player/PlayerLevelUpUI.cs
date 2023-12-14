using System;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BraveBloodMonsterHunt
{
    public class PlayerLevelUpUI : MonoBehaviour
    {
        [System.Serializable]
        private class UpgradeUI
        {
            public TextMeshProUGUI upgradeName;
            public Button upgradeButton;
            public TextMeshProUGUI upgradeDescription;
        }
        
        [SerializeField] private UpgradeUI upgrade1, upgrade2, upgrade3, upgrade4;

        private void Start()
        {
            // Initialize upgrade UI
            upgrade1.upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);
            upgrade2.upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);
            upgrade3.upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);
            upgrade4.upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);
        }
        
        private void OnUpgradeButtonClicked()
        {
        }
        
    }
}