using System;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

namespace BraveBloodMonsterHunt.CombatSystem
{
    /// <summary>
    /// Defense Component
    /// </summary>
    [DisallowMultipleComponent]
    public partial class Defense : MonoBehaviour
    {
        [SerializeField] private int baseDefense = 2;
        
        [SerializeField, BoxGroup("Animation")]
        private Animator anim;

        [SerializeField, BoxGroup("Animation"), AnimatorParam(nameof(anim)), ShowIf(nameof(IsAnimatorNotNull))]
        private string defenseTriggerParam;
        
        private bool IsAnimatorNotNull => anim != null;

        /// <summary>
        /// get/set defense value
        /// </summary>
        public int Value
        {
            get => baseDefense;
            set
            {
                baseDefense = value;
            }
        }
    }

    public partial class Defense
    {
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            // Handles.Label(transform.position + Vector3.up * 1f, $"HP: {Value}", new GUIStyle() {fontSize = 14, alignment = TextAnchor.MiddleCenter, normal = new GUIStyleState() {textColor = Color.red}});
        }
#endif
    }
}