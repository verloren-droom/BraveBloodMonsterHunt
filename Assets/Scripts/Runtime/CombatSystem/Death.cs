using System;
using Timers;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

namespace BraveBloodMonsterHunt.CombatSystem
{
    /// <summary>
    /// Death Component
    /// </summary>
    [DisallowMultipleComponent]
    public class Death : MonoBehaviour
    {
        [SerializeField] private bool isDestroy;
        [SerializeField] private UnityEvent onDied;
        
        [SerializeField, BoxGroup("Animation")]
        private Animator anim;

        [SerializeField, BoxGroup("Animation"), AnimatorParam(nameof(anim))]
        private string diedTriggerParam;

        /// <summary>
        /// Check if the health value is greater than zero
        /// </summary>
        /// <param name="health">health value</param>
        public void CheckDeath(int health)
        {
            if (health > 0) return;
            // TimersManager.SetTimer(this, 0.5f, Die);
            anim.SetTrigger(diedTriggerParam);
            Die();
        }
        
        public void Die()
        {
            onDied?.Invoke();
            
            if (isDestroy)
                Destroy(gameObject);
            else
                gameObject.SetActive(false);
        }

        /// <summary>
        /// add died event
        /// </summary>
        /// <param name="action"></param>
        public void AddOnDiedEvent(UnityAction action)
        {
            onDied?.AddListener(action);
        }
        /// <summary>
        /// remove died event
        /// </summary>
        /// <param name="action"></param>
        public void RemoveDiedEvent(UnityAction action)
        {
            onDied?.RemoveListener(action);
        }
    }
}