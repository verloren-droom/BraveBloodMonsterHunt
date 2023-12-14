using System;
using DG.Tweening;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.InputSystem;

namespace BraveBloodMonsterHunt
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField, Min(0)] private float moveSpeed = 3.0f;
        
        private Rigidbody2D m_RB;

        private Vector2 m_MoveDirection;
        
        [SerializeField, BoxGroup("Animation")]
        private Animator anim;

        [SerializeField, BoxGroup("Animation"), AnimatorParam(nameof(anim))]
        private string moveParam;

        private Vector3 currentScale;

        private void Awake()
        {
            m_RB = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            currentScale = transform.localScale;
        }
        
        public void MoveInput(InputAction.CallbackContext context)
        {
            m_MoveDirection = context.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            var targetPosition = (Vector2)transform.position + m_MoveDirection;

            bool isMove = targetPosition == (Vector2)transform.position;
            
            anim.SetFloat(moveParam, m_MoveDirection.magnitude);
            
            if (isMove)
            {
                return;
            }
            m_RB.DOMove(targetPosition, moveSpeed).SetSpeedBased();
            transform.localScale = new Vector3(currentScale.x * (m_MoveDirection.x > 0 ? 1 : -1), currentScale.y, currentScale.z);
        }

        private void OnDisable()
        {
            m_RB.DOKill();
        }

        private void OnDestroy()
        {
            m_RB.DOKill();
        }
    }
}
