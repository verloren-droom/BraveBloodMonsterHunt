using System;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace BraveBloodMonsterHunt
{
    [DisallowMultipleComponent]
    public class Damageable : MonoBehaviour
    {
        [SerializeField, Required] private Health health;
        [SerializeField, Required] private SpriteRenderer spriteRenderer;
        [SerializeField] private UnityEvent onDamaged;

        private Color m_DefaultColor;

        private void Awake()
        {
            m_DefaultColor = spriteRenderer.color;
        }

        public void AddOnDamagedEvent(UnityAction action)
        {
            onDamaged?.AddListener(action);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="damage"></param>
        public void TakeDamage(int damage = 1)
        {
            health.Value -= damage;
            spriteRenderer.DOColor(Color.red, 0.2f).SetLoops(2, LoopType.Yoyo).ChangeStartValue(m_DefaultColor);
            onDamaged?.Invoke();
            
            // 
            FloatingTextManager.Instance.GetFloatingText(damage.ToString(), transform.position);
        }
    }
}