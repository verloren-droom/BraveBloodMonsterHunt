using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace BraveBloodMonsterHunt
{
    public class ExperienceSystem : MonoBehaviour
    {
        [ShowNonSerializedField] private const int MAX_LEVEL = 100;
        [SerializeField, MinValue(1), MaxValue(MAX_LEVEL)] private int level = 1;
        [SerializeField] private UnityEvent onLevelChanged;

        [ShowNonSerializedField] private const int BASE_EXP = 100;
        [ShowNonSerializedField] private const int MAX_EXP = 99999;
        [ShowNonSerializedField] private int currentExperience = 0;
        [SerializeField, CurveRange(1, BASE_EXP, MAX_LEVEL, MAX_EXP, EColor.Blue)] private AnimationCurve experienceCurve;
        [SerializeField] private UnityEvent onExperienceChanged;
        
        public int Level
        {
            get => level;
            private set
            {
                level = Mathf.Clamp(value, 1, MAX_LEVEL);
                onLevelChanged?.Invoke();
            }
        }

        public void AddOnLevelChangedEvent(UnityAction action)
        {
            onLevelChanged?.AddListener(action);
        }
        
        public int CurrentExperience
        {
            get => currentExperience;
            private set
            {
                currentExperience = Mathf.Clamp(value, 0, MAX_EXP);
                onExperienceChanged?.Invoke();
            }
        }

        [ShowNativeProperty]
        public int NextLevelExperience => Mathf.RoundToInt(experienceCurve.Evaluate(level));

        public void GainExperience(int exp)
        {
            CurrentExperience += exp;
            while (CurrentExperience >= NextLevelExperience)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Level++;
            CurrentExperience -= NextLevelExperience;
        }

        public void AddOnExperienceChanged(UnityAction action)
        {
            onExperienceChanged?.AddListener(action);
        }
    }
}