using Timers;
using UnityEngine;
using UnityEngine.Events;

namespace BraveBloodMonsterHunt
{
    [DisallowMultipleComponent]
    public class Death : MonoBehaviour
    {
        [SerializeField] private bool isDestroy;
        public UnityEvent onDied;
        
        /// <summary>
        /// Check if the health value is greater than zero
        /// </summary>
        /// <param name="health">health value</param>
        public void CheckDeath(int health)
        {
            if (health > 0) return;
            // TimersManager.SetTimer(this, 0.5f, Die);
            Die();
        }
        
        private void Die()
        {
            onDied?.Invoke();
            
            if (isDestroy)
                Destroy(gameObject);
            else
                gameObject.SetActive(false);
            
        }

        public void AddOnDiedEvent(UnityAction action)
        {
            onDied?.AddListener(action);
        }
    }
}