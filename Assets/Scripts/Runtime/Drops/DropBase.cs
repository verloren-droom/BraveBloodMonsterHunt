using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace BraveBloodMonsterHunt
{
    public abstract partial class DropBase : MonoBehaviour
    {
        [SerializeField] protected TriggerMode triggerMode;
        
        [SerializeField, ShowIf(nameof(IsModeWithTag))] protected bool isDestroy = true;
        [SerializeField, Tag, ShowIf(nameof(IsModeWithTag))] protected string triggerTag;
        
        // Events
        [SerializeField, Foldout("Event"), ShowIf(nameof(IsModeWithEvent))] protected UnityEvent onEnterEvent;
        [SerializeField, Foldout("Event"), ShowIf(nameof(IsModeWithEvent))] protected UnityEvent onExitEvent;
        
        /// <summary>
        /// add trigger enter event
        /// </summary>
        /// <param name="action"></param>
        public void AddOnTriggerEnterEvent(UnityAction action)
        {
            onEnterEvent?.AddListener(action);
        }
        /// <summary>
        /// remove trigger enter event
        /// </summary>
        /// <param name="action"></param>
        public void RemoveOnTriggerEnter(UnityAction action)
        {
            onEnterEvent?.RemoveListener(action);
        }
        /// <summary>
        /// add trigger exit event
        /// </summary>
        /// <param name="action"></param>
        public void AddOnTriggerExit(UnityAction action)
        {
            onExitEvent?.AddListener(action);
        }
        /// <summary>
        /// remove trigger exit event
        /// </summary>
        /// <param name="action"></param>
        public void RemoveOnTriggerExit(UnityAction action)
        {
            onExitEvent?.RemoveListener(action);
        }
    }
    public partial class DropBase
    {
#if UNITY_EDITOR
        private bool IsModeWithTag => triggerMode == TriggerMode.Tag;
        private bool IsModeWithEvent => triggerMode == TriggerMode.Event;
#endif
    }
}