using System;
using System.Linq;
using BraveBloodMonsterHunt.Utility;
using NaughtyAttributes;
using Timers;
using UnityEngine;
using UnityEngine.Events;

namespace BraveBloodMonsterHunt
{
    [System.Serializable]
    internal class PortalGenItem
    {
        public GameObject obj;
        public float weight;
    }
    
    public class Portal : MonoBehaviour
    {
        [SerializeField, ReorderableList, ValidateInput(nameof(CheckSpawnObjsLength))] private PortalGenItem[] objs;
        [SerializeField] private Transform target;

        [SerializeField] private float interval = 1.0f;
        [SerializeField] private UnityEvent onObjectGenerateEvent;
        
        [SerializeField, BoxGroup("Animation")] private Animator anim;
        [SerializeField, AnimatorParam(nameof(anim)), BoxGroup("Animation")]
        private string isOpen;
        [SerializeField, AnimatorParam(nameof(anim)), BoxGroup("Animation")]
        private string isClose;

        private void OnEnable()
        {
            TimersManager.SetTimer(this, interval, () =>
            {
                anim.SetTrigger(isOpen);
                GenObj();
            });
            
            onObjectGenerateEvent?.AddListener((() =>
            {
                anim.SetTrigger(isClose);
            }));
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        [Button("Generate Object")]
        public void GenObj()
        {
            var weights = objs.Select(d => d.weight).ToArray();
            var ind = RandomUtility.RandomArrayIndex(weights);

            if (ind != -1 && objs[ind] != null)
            {
                Instantiate(objs[ind].obj, target != null ? target.position : transform.position, Quaternion.identity);
                onObjectGenerateEvent?.Invoke();
            }
        }
        
        /// <summary>
        /// add object generate event
        /// </summary>
        /// <param name="action"></param>
        public void AddOnObjectGenerateEvent(UnityAction action)
        {
            onObjectGenerateEvent?.AddListener(action);
        }
        /// <summary>
        /// remove object generate event
        /// </summary>
        /// <param name="action"></param>
        public void RemoveOnObjectGenerateEvent(UnityAction action)
        {
            onObjectGenerateEvent?.RemoveListener(action);
        }

#if UNITY_EDITOR
        private bool CheckSpawnObjsLength()
        {
            return objs.IsNotNullAndLengthGreaterZero();
        }
#endif
    }
}