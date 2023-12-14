using System;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace BraveBloodMonsterHunt
{
    [DisallowMultipleComponent]
    public class MissileMovement : MonoBehaviour
    {
        [SerializeField, Min(0)] private float moveSpeed = 5.0f;
        [SerializeField, Min(0)] private float missileRadius = 5.0f;

        [Tag, SerializeField] private string targetTag;
        private GameObject m_Target;

        private Vector2 m_MoveDirection;

        private Rigidbody2D rb;

        [SerializeField, BoxGroup("Debug")] private bool isDebug;
        
        /// <summary>
        /// try find target gameobject
        /// </summary>
        /// <param name="target"></param>
        /// <returns>true -> find target, false -> didn't find the target</returns>
        private bool TryFindTarget(out GameObject target)
        {
            var res = new Collider2D[5];
            Physics2D.OverlapCircleNonAlloc(transform.position, missileRadius, res);
            foreach (var r in res)
            {
                if (r == null || !r.CompareTag(targetTag)) continue;
                target = r.gameObject;
                return true;
            }

            target = null;
            return false;
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        
        private void OnEnable()
        {
            // Initialise target position
            var targetPos = UnityEngine.Random.insideUnitCircle;
            if (TryFindTarget(out m_Target))
            {
                targetPos = m_Target.transform.position;
            }
            m_MoveDirection = (targetPos - (Vector2)transform.position).normalized;
            
            transform.rotation = Quaternion.LookRotation(transform.forward, m_MoveDirection);
        }

        private void FixedUpdate()
        {
            var targetPosition = (Vector2)transform.position + m_MoveDirection;
            
            rb.DOMove(targetPosition, moveSpeed).SetSpeedBased();
        }
        
        private void OnDisable()
        {
            rb.DOKill();
        }

        private void OnDestroy()
        {
            rb.DOKill();
        }

        private void OnDrawGizmos()
        {
            if (!isDebug) return;
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, missileRadius);
            if (m_Target == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, m_Target.transform.position);
        }
    }
}