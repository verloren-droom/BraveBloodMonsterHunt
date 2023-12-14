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
        
        /// <summary>
        /// get/private set current level
        /// </summary>
        public int Level
        {
            get => level;
            private set
            {
                level = Mathf.Clamp(value, 1, MAX_LEVEL);
                onLevelChanged?.Invoke();
            }
        }

        /// <summary>
        /// add level changed event
        /// </summary>
        /// <param name="action"></param>
        public void AddOnLevelChangedEvent(UnityAction action)
        {
            onLevelChanged?.AddListener(action);
        }

        /// <summary>
        /// remove level changed event
        /// </summary>
        /// <param name="action"></param>
        public void RemoveOnLevelChangedEvent(UnityAction action)
        {
            onLevelChanged?.RemoveListener(action);
        }
        
        /// <summary>
        /// get/private set current experience
        /// </summary>
        public int CurrentExperience
        {
            get => currentExperience;
            private set
            {
                currentExperience = Mathf.Clamp(value, 0, MAX_EXP);
                onExperienceChanged?.Invoke();
            }
        }

        /// <summary>
        /// get next level need experience
        /// </summary>
        [ShowNativeProperty]
        public int NextLevelExperience => Mathf.RoundToInt(experienceCurve.Evaluate(level));
        
        /// <summary>
        /// add experience changed event
        /// </summary>
        /// <param name="action"></param>
        public void AddOnExperienceChangedEvent(UnityAction action)
        {
            onExperienceChanged?.AddListener(action);
        }

        /// <summary>
        /// remove experience changed event
        /// </summary>
        /// <param name="action"></param>
        public void RemoveOnExperienceChangedEvent(UnityAction action)
        {
            onExperienceChanged?.RemoveListener(action);
        }

        /// <summary>
        /// gain experience
        /// </summary>
        /// <param name="exp">experience value</param>
        public void GainExperience(int exp)
        {
            CurrentExperience += exp;
            while (CurrentExperience >= NextLevelExperience)
            {
                LevelUp();
            }
        }

        /// <summary>
        /// level up
        /// </summary>
        private void LevelUp()
        {
            Level++;
            CurrentExperience -= NextLevelExperience;
        }
    }
}