using System;
using DG.Tweening;
using UnityEngine;

namespace BraveBloodMonsterHunt
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField, Min(0)] private float moveSpeed = 1.5f;
    
        private Rigidbody2D m_RB;

        private void Awake()
        {
            m_RB = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            var playerDirection = (Vector2)(PlayerManager.Instance.PlayerTransform.position - transform.position);
            playerDirection.Normalize();
            
            var targetPosition = (Vector2)transform.position + playerDirection;
            if (targetPosition == (Vector2)transform.position) return;
            m_RB.DOMove(targetPosition, moveSpeed).SetSpeedBased();
        }
    }
}
