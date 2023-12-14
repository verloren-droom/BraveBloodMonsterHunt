using UnityEngine;

namespace BraveBloodMonsterHunt.Utility
{
    public class CastBox2DMono : CastShapeMonoBase
    {
        [SerializeField] private Vector2 center;
        [SerializeField] private Vector2 size = Vector2.one;

        public Vector2 Center
        {
            get => center;
            set => center = value;
        }
        public Vector2 Size
        {
            get => size;
            set => size = value;
        }
 
#if UNITY_EDITOR
        protected override void Reset ()
        {
            center = Vector2.zero;
            size = Vector2.one;
        }
 
        protected override void OnDrawGizmosSelected ()
        {
            Matrix4x4 gizmosMatrixRecord = Gizmos.matrix;
            Color gizmosColorRecord = Gizmos.color;
            Gizmos.color = m_GizomsColor;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(center, size);
            Gizmos.color = gizmosColorRecord;
            Gizmos.matrix = gizmosMatrixRecord;
        }
#endif
    }
}