using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace BraveBloodMonsterHunt
{
    [DisallowMultipleComponent, RequireComponent(typeof(Collider2D))]
    public partial class Drop : MonoBehaviour
    {
        [SerializeField] private DropType itemType;
        public DropType DropItemType => itemType;
        
        [SerializeField, ShowIf(nameof(IsPickupItemForHealth)), Label("Health"), MinMaxSlider(0, 1000)] private Vector2Int minMaxHealthValue;
        [SerializeField, ShowIf(nameof(IsPickupItemForExperience)), Label("Experience"), MinMaxSlider(0, 99999)] private Vector2Int minMaxExperienceValue;
        
        [SerializeField] private TriggerMode triggerMode;
        
        [SerializeField, ShowIf(nameof(IsModeWithTag))] private bool isDestroy = true;
        [SerializeField, Tag, ShowIf(nameof(IsModeWithTag))] private string itemTag;
        
        // Events
        [SerializeField, Foldout("Event"), ShowIf(nameof(IsModeWithEvent))] private UnityEvent onEnter;

        public void AddOnEnterEvent(UnityAction action)
        {
            onEnter?.AddListener(action);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other != null && other.CompareTag(itemTag))
            {
                switch (itemType)
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
                
                if (isDestroy) Destroy(gameObject);
            }
            
            onEnter?.Invoke();
        }

        // private void OnDestroy()
        // {
        //     onEnter.RemoveAllListeners();
        // }
    }

    public partial class Drop : MonoBehaviour
    {
#if UNITY_EDITOR
        private bool IsPickupItemForExperience => (itemType == DropType.Experience);
        private bool IsPickupItemForHealth => (itemType == DropType.Health);
        private bool IsModeWithTag => triggerMode == TriggerMode.Tag;
        private bool IsModeWithEvent => triggerMode == TriggerMode.Event;
#endif
    }
}
