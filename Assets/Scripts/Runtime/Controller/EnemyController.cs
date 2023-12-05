using System;
using NaughtyAttributes;
using UnityEngine;

namespace BraveBloodMonsterHunt
{
    [DisallowMultipleComponent]
    public class EnemyController : MonoBehaviour, IObserver
    {
        [SerializeField, Required] private EnemyMovement enemyMovement;
        
        private void Start()
        {
            FindObjectOfType<Player>()?.AddObserver(this);
        }

        private void OnDestroy()
        {
            
            FindObjectOfType<Player>()?.RemoveObserver(this);
        }

        public void UpdateNotify()
        {
            enemyMovement.enabled = false;
        }
    }
}