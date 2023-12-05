using NaughtyAttributes;
using UnityEngine;

namespace BraveBloodMonsterHunt
{
    public class EnemyAnimation : MonoBehaviour
    {
        [SerializeField] private Animator anim;

        [SerializeField, AnimatorParam(nameof(anim))]
        private string idleState;

        [SerializeField, AnimatorParam(nameof(anim))]
        private string runningState;

        [SerializeField, AnimatorParam(nameof(anim))]
        private string dieState;

        [SerializeField, AnimatorParam(nameof(anim))]
        private string attackState;
    }
}