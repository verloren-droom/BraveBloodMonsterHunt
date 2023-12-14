#if UNITY_EDITOR

using BraveBloodMonsterHunt.Utility;
using UnityEditor;
using UnityEngine;

namespace BraveBloodMonsterHunt.Editor
{
    [CustomEditor(typeof(CastBox2DMono)), CanEditMultipleObjects]
    public class CastBox2DEditor : UnityEditor.Editor
    {
        private SerializedProperty m_Script;
        private SerializedProperty m_Center;
        private SerializedProperty m_Size;
 
        private void OnEnable ()
        {
            m_Script = serializedObject.FindProperty("m_Script");
            m_Center = serializedObject.FindProperty("center");
            m_Size = serializedObject.FindProperty("size");
        }
 
        public override void OnInspectorGUI ()
        {
            serializedObject.Update();
 
            GUI.enabled = false;
            EditorGUILayout.PropertyField(m_Script);
            GUI.enabled = true;
            EditorGUILayout.EditorToolbarForTarget(EditorGUIUtility.TrTempContent("Edit Shape"), target);
            EditorGUILayout.PropertyField(m_Center);
            EditorGUILayout.PropertyField(m_Size);
 
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif
