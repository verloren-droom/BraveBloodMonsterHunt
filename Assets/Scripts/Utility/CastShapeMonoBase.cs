using UnityEngine;

namespace BraveBloodMonsterHunt.Utility
{
    public abstract class CastShapeMonoBase : MonoBehaviour
    {
#if UNITY_EDITOR
        protected readonly Color m_GizomsColor = Color.cyan;
 
        protected virtual void Reset () { }
        protected virtual void OnDrawGizmosSelected () { }
#endif
    }
}