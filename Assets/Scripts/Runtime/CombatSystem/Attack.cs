using System;
using BraveBloodMonsterHunt.Utility;
using NaughtyAttributes;
using Timers;
using UnityEngine;
using UnityEngine.Events;

namespace BraveBloodMonsterHunt.CombatSystem
{
    /// <summary>
    /// Attack Component
    /// </summary>
    [DisallowMultipleComponent, RequireComponent(typeof(Collider2D))]
    public partial class Attack : MonoBehaviour
    {
        [ShowNonSerializedField] private const int MAX_DAMAGE = 9999;
        [SerializeField, MinMaxSlider(1, MAX_DAMAGE)] private Vector2Int minMaxDamage = new Vector2Int(2, 4);
        
        [SerializeField] private bool cooling;
        [SerializeField, Min(0.0f), ShowIf(nameof(cooling))] private float attackDuration = 1.0f;
        
        [SerializeField, ShowIf(nameof(IsShowDuration)), ProgressBar("Elapsed", nameof(attackDuration), EColor.Yellow)]
        private float elapsedTime;

        [SerializeField] private TriggerMode triggerMode;

        // tag
        [SerializeField, Tag, ShowIf(nameof(IsModeWithTag))]
        private string attackTargetTag; 
        
        // attack events
        [SerializeField, Foldout("Attack Event"), ShowIf(nameof(IsModeWithEvent))] private UnityEvent<int> onAttackEnter;
        [SerializeField, Foldout("Attack Event"), ShowIf(nameof(IsModeWithEvent))] private UnityEvent<int> onAttackStay;
        
        private bool m_CanAttack = true;
        private int m_CanAttackID;

        [SerializeField, BoxGroup("Animation")] private Animator anim;
        [SerializeField, BoxGroup("Animation"), AnimatorParam(nameof(anim)), ShowIf(nameof(IsAnimatorNotNull))]
        private string attackTriggerParam;

        public int DamageValue => RandomUtility.RangeVector2(minMaxDamage);
        private bool IsShowDuration() => Application.isPlaying && cooling;
        private bool IsAnimatorNotNull => anim != null;
        
        /// <summary>
        /// update attack status
        /// </summary>
        private void UpdateCanAttack()
        {
            m_CanAttackID = TimersManager.SetTimer(this, attackDuration, () =>
            {
                m_CanAttack = true;
                if (IsAnimatorNotNull)
                {
                    anim.ResetTrigger(attackTriggerParam);
                }
                
            });
            
            m_CanAttack = false;
        }

        /// <summary>
        /// add attack event enter event
        /// </summary>
        /// <param name="action"></param>
        public void AddOnAttackEnterEvent(UnityAction<int> action)
        {
            onAttackEnter?.AddListener(action);
        }
        /// <summary>
        /// remove attack enter event
        /// </summary>
        /// <param name="action"></param>
        public void RemoveOnAttackEnterEvent(UnityAction<int> action)
        {
            onAttackEnter?.RemoveListener(action);
        }

        /// <summary>
        /// add attack stay event
        /// </summary>
        /// <param name="action"></param>
        public void AddOnAttackStayEvent(UnityAction<int> action)
        {
            onAttackStay?.AddListener(action);
        }
        /// <summary>
        /// remove attack stay event
        /// </summary>
        /// <param name="action"></param>
        public void RemoveOnAttackStayEvent(UnityAction<int> action)
        {
            onAttackStay?.RemoveListener(action);
        }

        private void Start()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        private void Update()
        {
            if (IsShowDuration())
            {
                elapsedTime = Mathf.Clamp(TimersManager.ElapsedTime(m_CanAttackID), 0,  attackDuration);
            }
        }
        
        private void AttackForTrigger(Collider2D other)
        {
            if (triggerMode == TriggerMode.Event)
            {
                onAttackEnter?.Invoke(DamageValue);
                if (IsAnimatorNotNull)
                {
                    anim.SetTrigger(attackTriggerParam);
                }
            }
            else if (triggerMode ==  TriggerMode.Tag && other.CompareTag(attackTargetTag) && other.TryGetComponent<Damageable>(out var damageable))
            {
                damageable.TakeDamage(DamageValue);
                if (IsAnimatorNotNull)
                {
                    anim.SetTrigger(attackTriggerParam);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!m_CanAttack) return;
            
            AttackForTrigger(other);

            if (!cooling) return;
            UpdateCanAttack();
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            if (!m_CanAttack) return;
            
            AttackForTrigger(other);

            if (!cooling) return;
            UpdateCanAttack();
        }

        // private void OnDestroy()
        // {
        //     onAttackEnter.RemoveAllListeners();
        //     onAttackStay.RemoveAllListeners();
        // }
    }

    public partial class Attack : MonoBehaviour
    {
#if UNITY_EDITOR
        private bool IsModeWithTag => triggerMode == TriggerMode.Tag;
        private bool IsModeWithEvent => triggerMode == TriggerMode.Event;
#endif
    }
}