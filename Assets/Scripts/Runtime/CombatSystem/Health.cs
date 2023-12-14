using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace BraveBloodMonsterHunt.CombatSystem
{
    /// <summary>
    /// Health Component
    /// </summary>
    [DisallowMultipleComponent]
    public partial class Health : MonoBehaviour
    {
        [SerializeField, Min(0)] private int maxHealth = 100;
        [SerializeField, Min(0)] private int health = 100;
        [SerializeField] private UnityEvent<int> onHealthChanged;
        
        /// <summary>
        /// get/set current health value
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

        /// <summary>
        /// get/set max health value
        /// </summary>
        public int MaxValue
        {
            get => maxHealth;
            set
            {
                maxHealth = value;
                maxHealth = Mathf.Min(maxHealth, 0);
            }
        }

        /// <summary>
        /// add health changed event
        /// </summary>
        /// <param name="action"></param>
        public void AddOnHealthValueChangedEvent(UnityAction<int> action)
        {
            onHealthChanged?.AddListener(action);
        }
        /// <summary>
        /// remove health changed event
        /// </summary>
        /// <param name="action"></param>
        public void RemoveOnHealthValueChangedEvent(UnityAction<int> action)
        {
            onHealthChanged?.RemoveListener(action);
        }
    }

    public partial class Health
    {
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.Label(transform.position + Vector3.up * 1.1f, $"HP: {Value}", new GUIStyle() {fontSize = 14, alignment = TextAnchor.MiddleCenter, normal = new GUIStyleState() {textColor = Color.red}});
        }
#endif
    }
}