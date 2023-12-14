#if UNITY_EDITOR

using BraveBloodMonsterHunt.Utility;
using UnityEditor.EditorTools;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace BraveBloodMonsterHunt.Editor
{
    [EditorTool("Edit Cast Shape", typeof(CastBox2DMono))]
    public class CastBox2DTool : CastShapeTool<CastBox2DMono>
    {
 
        private readonly BoxBoundsHandle m_BoundsHandle = new BoxBoundsHandle();

        protected override PrimitiveBoundsHandle BoundsHandle => m_BoundsHandle;
     
        protected override void CopyColliderPropertiesToHandle (CastBox2DMono castShape)
        {
            m_BoundsHandle.center = TransformColliderCenterToHandleSpace(castShape.transform, castShape.Center);
            m_BoundsHandle.size = Vector3.Scale(castShape.Size, castShape.transform.lossyScale);
        }
     
        protected override void CopyHandlePropertiesToCollider (CastBox2DMono castShape)
        {
            castShape.Center = TransformHandleCenterToColliderSpace(castShape.transform, m_BoundsHandle.center);
            var size = Vector2.Scale(m_BoundsHandle.size, InvertScaleVector(castShape.transform.lossyScale));
            size = new Vector3(Mathf.Abs(size.x), Mathf.Abs(size.y));
            castShape.Size = size;
        }
    }
}
#endif
