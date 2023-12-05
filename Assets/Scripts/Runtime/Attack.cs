using System;
using NaughtyAttributes;
using Timers;
using UnityEngine;
using UnityEngine.Events;

namespace BraveBloodMonsterHunt
{
    [DisallowMultipleComponent]
    public partial class Attack : MonoBehaviour
    {
        [SerializeField] private bool cooling;
        [SerializeField, MinMaxSlider(0, 100)] private Vector2Int minMaxDamage = new Vector2Int(1, 2);
        [SerializeField, ShowIf(nameof(IsShowDuration)), ProgressBar("Elapsed", nameof(interval), EColor.Yellow)]
        private float elapsedTime;
        [SerializeField, Min(0.0f), ShowIf(nameof(cooling))] private float interval = 1.0f;

        [SerializeField] private TriggerMode triggerMode;

        // tag
        [SerializeField, Tag, ShowIf(nameof(IsModeWithTag))]
        private string attackTag; 
        
        // attack events
        [SerializeField, Foldout("Attack Event"), ShowIf(nameof(IsModeWithEvent))] private UnityEvent<int> onAttackEnter;
        [SerializeField, Foldout("Attack Event"), ShowIf(nameof(IsModeWithEvent))] private UnityEvent<int> onAttackStay;
        
        private bool m_CanAttack = true;
        private int m_CanAttackID;

        private bool IsShowDuration() => Application.isPlaying && cooling;

        public int DamageValue => UnityEngine.Random.Range(minMaxDamage.x, minMaxDamage.y);
        
        /// <summary>
        /// update attack status
        /// </summary>
        private void UpdateCanAttack()
        {
            m_CanAttackID = TimersManager.SetTimer(this, interval, () => { m_CanAttack = true; });
            m_CanAttack = false;
        }

        public void AddOnAttackEnterEvent(UnityAction<int> action)
        {
            onAttackEnter?.AddListener(action);
        }

        public void AddAttackStayEvent(UnityAction<int> action)
        {
            onAttackStay?.AddListener(action);
        }

        private void Update()
        {
            if (IsShowDuration())
            {
                elapsedTime = Mathf.Clamp(TimersManager.ElapsedTime(m_CanAttackID), 0, interval);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!m_CanAttack) return;

            switch (triggerMode)
            {
                case TriggerMode.Event:
                    onAttackEnter?.Invoke(DamageValue);
                    break;
                case TriggerMode.Tag when other.CompareTag(attackTag) && other.TryGetComponent<Damageable>(out var damageable):
                    damageable.TakeDamage(DamageValue);
                    break;
            }

            if (!cooling) return;
            UpdateCanAttack();
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            if (!m_CanAttack) return;

            switch (triggerMode)
            {
                case TriggerMode.Event:
                    onAttackStay?.Invoke(DamageValue);
                    break;
                case TriggerMode.Tag when other.CompareTag(attackTag) && other.TryGetComponent<Damageable>(out var damageable):
                    damageable.TakeDamage(DamageValue);
                    break;
            }
            
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