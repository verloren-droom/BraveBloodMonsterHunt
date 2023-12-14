#if UNITY_EDITOR

using System.Reflection;
using BraveBloodMonsterHunt.Utility;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace BraveBloodMonsterHunt.Editor
{
    public abstract class CastShapeTool<T> : EditorTool where T : CastShapeMonoBase
    {
        protected readonly Color m_HandleEnableColor = Color.cyan;
        protected readonly Color m_HandleDisableColor = new Color(0f, 0.7f, 0.7f);
     
        public override GUIContent toolbarIcon
        {
            get
            {
                PropertyInfo propertyInfo = typeof(PrimitiveBoundsHandle).GetProperty("editModeButton", BindingFlags.NonPublic | BindingFlags.Static);
                return (GUIContent)propertyInfo.GetValue(null);
            }
        }
     
        protected abstract PrimitiveBoundsHandle BoundsHandle { get; }
     
        protected abstract void CopyColliderPropertiesToHandle (T castShape);
        protected abstract void CopyHandlePropertiesToCollider (T castShape);
     
        protected Vector3 InvertScaleVector (Vector3 scaleVector)
        {
            for (int axis = 0; axis < 3; ++axis)
            {
                scaleVector[axis] = scaleVector[axis] == 0f ? 0f : 1f / scaleVector[axis];
            }
     
            return scaleVector;
        }
     
        public override void OnToolGUI (EditorWindow window)
        {
            foreach (var obj in targets)
            {
                if (obj is not T castShape || Mathf.Approximately(castShape.transform.lossyScale.sqrMagnitude, 0f))
                    continue;
                
                using (new Handles.DrawingScope(Matrix4x4.TRS(castShape.transform.position, castShape.transform.rotation, Vector3.one)))
                {
                    CopyColliderPropertiesToHandle(castShape);
     
                    BoundsHandle.SetColor(castShape.enabled ? m_HandleEnableColor : m_HandleDisableColor);
     
                    EditorGUI.BeginChangeCheck();
     
                    BoundsHandle.DrawHandle();
     
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(castShape, $"Modify {ObjectNames.NicifyVariableName(target.GetType().Name)}");
                        CopyHandlePropertiesToCollider(castShape);
                    }
                }
            }
        }
     
        protected static Vector3 TransformColliderCenterToHandleSpace (Transform colliderTransform, Vector3 colliderCenter)
        {
            return Handles.inverseMatrix * (colliderTransform.localToWorldMatrix * colliderCenter);
        }
     
        protected static Vector3 TransformHandleCenterToColliderSpace (Transform colliderTransform, Vector3 handleCenter)
        {
            return colliderTransform.localToWorldMatrix.inverse * (Handles.matrix * handleCenter);
        }
    }
}
#endif
