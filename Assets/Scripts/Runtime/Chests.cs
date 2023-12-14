using System;
using NaughtyAttributes;
using Timers;
using UnityEngine;
using UnityEngine.Events;

namespace BraveBloodMonsterHunt
{
    public partial class Chests : DropBase
    {
        [SerializeField, BoxGroup("Animation")] private Animator anim;
        [SerializeField, AnimatorParam(nameof(anim)), BoxGroup("Animation")]
        private string openParam;

        private float m_Radius;

        public void GetInChestsObject()
        {
            // TODO:
            Debug.Log("Get in Chests Object!");
        }

        private void Start()
        {
            m_Radius = GetComponent<CircleCollider2D>().radius;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!TryGetComponent<CircleCollider2D>(out var col)) return;
            
            switch (triggerMode)
            {
                case TriggerMode.Tag:
                    if (!other.CompareTag(triggerTag)) return;
                    
                    anim.SetTrigger(openParam);
                    col.radius = m_Radius * 2.0f;
                    break;
                case TriggerMode.Event:
                    onEnterEvent?.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!TryGetComponent<CircleCollider2D>(out var col)) return;
            
            // TODO:
            switch (triggerMode)
            {
                case TriggerMode.Tag:
                    if (other.CompareTag(triggerTag))
                    {
                        anim.ResetTrigger(openParam);
                        col.radius = m_Radius;
                    }
                    break;
                case TriggerMode.Event:
                    onExitEvent?.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            if (isDestroy) Destroy(gameObject, 0.25f);
        }
    }
}
