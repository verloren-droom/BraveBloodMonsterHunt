using System;
using DG.Tweening;
using UnityEngine;
using NaughtyAttributes;

namespace BraveBloodMonsterHunt
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField, Min(0)] private float moveSpeed = 1.2f;
    
        private Rigidbody2D m_RB;
        
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

        private void FixedUpdate()
        {
            var playerDirection = (Vector2)(PlayerManager.Instance.PlayerTransform.position - transform.position).normalized;
            
            anim.SetFloat(moveParam, playerDirection.magnitude);
            
            var targetPosition = (Vector2)transform.position + playerDirection;
            if (targetPosition == (Vector2)transform.position) return;
            m_RB.DOMove(targetPosition, moveSpeed).SetSpeedBased();
            transform.localScale = new Vector3(currentScale.x * (playerDirection.x > 0 ? 1 : -1), currentScale.y, currentScale.z);
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
