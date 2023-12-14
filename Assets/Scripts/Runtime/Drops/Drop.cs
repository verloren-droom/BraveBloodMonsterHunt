using System;
using BraveBloodMonsterHunt.CombatSystem;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace BraveBloodMonsterHunt
{
    [DisallowMultipleComponent, RequireComponent(typeof(Collider2D))]
    public partial class Drop : DropBase
    {
        [SerializeField] private DropType itemType;
        public DropType DropItemType => itemType;
        
        [SerializeField, ShowIf(nameof(IsPickupItemForHealth)), Label("Health"), MinMaxSlider(0, 1000)] private Vector2Int minMaxHealthValue;
        [SerializeField, ShowIf(nameof(IsPickupItemForExperience)), Label("Experience"), MinMaxSlider(0, 99999)] private Vector2Int minMaxExperienceValue;

        private void OnTriggerEnter2D(Collider2D other)
        {
            switch (triggerMode)
            {
                case TriggerMode.Tag:
                    if (other != null && other.CompareTag(triggerTag))
                    {
                        switch (DropItemType)
                        {
                            case DropType.Experience:
                                if (other.TryGetComponent<ExperienceSystem>(out var exp))
                                {
                                    exp.GainExperience(UnityEngine.Random.Range(minMaxExperienceValue.x, minMaxExperienceValue.y));
                                }
                                break;
                            case DropType.Health:
                                if (other.TryGetComponent<Health>(out var health))
                                {
                                    health.Value += UnityEngine.Random.Range(minMaxHealthValue.x, minMaxHealthValue.y);
                                }
                                break;
                        }
                    }
                    break;
                case TriggerMode.Event:
                    onEnterEvent?.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            if (isDestroy) Destroy(gameObject, 0.25f);
        }
    }

    partial class Drop
    {
#if UNITY_EDITOR
        private bool IsPickupItemForExperience => (itemType == DropType.Experience);
        private bool IsPickupItemForHealth => (itemType == DropType.Health);
#endif
    }
}
