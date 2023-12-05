using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BraveBloodMonsterHunt
{
    public class PlayerExperienceUI : MonoBehaviour
    {
        [SerializeField, Required] private Slider playerExperienceBar;
        [SerializeField, Required] private TextMeshProUGUI levelText;
        [SerializeField, Required] private ExperienceSystem playerExperienceSystem;

        private void Start()
        {
            UpdateCurrentExperienceUI();
            UpdateNextLevelExperienceUI();
            UpdateLevelTextUI();
        }

        public void UpdateCurrentExperienceUI()
        {
            playerExperienceBar.DOValue(playerExperienceSystem.CurrentExperience, 0.5f);
        }

        public void UpdateNextLevelExperienceUI()
        {
            playerExperienceBar.maxValue = playerExperienceSystem.NextLevelExperience;
        }

        public void UpdateLevelTextUI()
        {
            levelText.text = "等级：" + playerExperienceSystem.Level.ToString();
        }
    }
}