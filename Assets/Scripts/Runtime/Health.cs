using UnityEngine;
using UnityEngine.Events;

namespace BraveBloodMonsterHunt
{
    [DisallowMultipleComponent]
    public class Health : MonoBehaviour
    {
        [SerializeField, Min(0)] private int maxHealth = 100;
        [SerializeField, Min(0)] private int health = 100;
        [SerializeField] private UnityEvent<int> onHealthChanged;
        
        /// <summary>
        /// Current health value
        /// </summary>
        public int Value
        {
            get => health;
            set
            {
                health = value;
                health = Mathf.Clamp(health, 0, maxHealth);
                onHealthChanged?.Invoke(health);
            }
        }

        public int MaxValue
        {
            get => maxHealth;
            set
            {
                maxHealth = value;
                maxHealth = Mathf.Min(maxHealth, 0);
            }
        }

        public void AddOnHealthValueChangedEvent(UnityAction<int> action)
        {
            onHealthChanged.AddListener(action);
        }
    }
}