using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BraveBloodMonsterHunt
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField, Min(0)] private float moveSpeed = 3.0f;
        
        private Rigidbody2D m_RB;

        private Vector2 m_MoveDirection;

        private void Awake()
        {
            m_RB = GetComponent<Rigidbody2D>();
        }

        public void MoveInput(InputAction.CallbackContext context)
        {
            m_MoveDirection = context.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            var targetPosition = (Vector2)transform.position + m_MoveDirection;
            
            if (targetPosition == (Vector2)transform.position) return;
            m_RB.DOMove(targetPosition, moveSpeed).SetSpeedBased();
            
        }
    }
}
