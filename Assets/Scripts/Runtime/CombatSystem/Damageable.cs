using BraveBloodMonsterHunt.Utility;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace BraveBloodMonsterHunt.CombatSystem
{
    /// <summary>
    /// Damageable Component
    /// </summary>
    [DisallowMultipleComponent]
    public partial class Damageable : MonoBehaviour
    {
        [SerializeField, Required] private Health health;
        [SerializeField, Required] private Defense defense;
        [SerializeField, ValidateInput(nameof(CheckSpriteRenderValid)), ReorderableList] private SpriteRenderer[] spriteRenderers;
        [SerializeField] private UnityEvent onDamaged;

        private Color[] m_DefaultColors;

        [SerializeField, BoxGroup("Animation")]
        private Animator anim;

        [SerializeField, BoxGroup("Animation"), AnimatorParam(nameof(anim)), ShowIf(nameof(IsAnimatorNotNull))]
        private string hitTriggerParam;
        
        private bool IsAnimatorNotNull => anim != null;

        private void Start()
        {
            m_DefaultColors = new Color[spriteRenderers.Length];
            for (int i = 0; i < m_DefaultColors.Length; i++)
            {
                m_DefaultColors[i] = spriteRenderers[i].color;
            }
        }

        /// <summary>
        /// add damaged event
        /// </summary>
        /// <param name="action"></param>
        public void AddOnDamagedEvent(UnityAction action)
        {
            onDamaged?.AddListener(action);
        }
        /// <summary>
        /// remove damaged event
        /// </summary>
        /// <param name="action"></param>
        public void RemoveOnDamagedEvent(UnityAction action)
        {
            onDamaged?.RemoveListener(action);
        }

        /// <summary>
        /// take damage
        /// </summary>
        /// <param name="damage"></param>
        public void TakeDamage(int damage = 1)
        {
            int currentDamage = (damage - defense.Value);
            
            if (currentDamage <= 0) return;
            
            health.Value -= currentDamage;
            for (int i = 0; i < spriteRenderers.Length; ++i)
            {
                spriteRenderers[i].DOColor(Color.red, 0.25f).SetLoops(4, LoopType.Yoyo).ChangeStartValue(m_DefaultColors[i]);
            }
            onDamaged?.Invoke();

            if (IsAnimatorNotNull)
            {
                anim.SetTrigger(hitTriggerParam);
            }
            
            FloatingTextManager.Instance.GetFloatingText($"-{currentDamage}", transform.position + new Vector3(0.5f, 0.85f, 0.0f), FloatingTextManager.ColorStyle.DecreaseHealth);
        }

        private void OnDisable()
        {
            foreach (var spriteRenderer in spriteRenderers)
                spriteRenderer.DOKill();
        }

        private void OnDestroy()
        {
            foreach (var spriteRenderer in spriteRenderers)
                spriteRenderer.DOKill();
        }
    }
    
    public partial class Damageable
    {
#if UNITY_EDITOR
        private bool CheckSpriteRenderValid()
        {
            return spriteRenderers.IsNotNullAndLengthGreaterZero();
        }
#endif
    }
}