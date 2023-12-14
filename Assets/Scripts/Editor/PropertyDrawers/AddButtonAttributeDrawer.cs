using UnityEditor;
using UnityEngine;

namespace BraveBloodMonsterHunt.Editor
{
    [CustomPropertyDrawer(typeof(AddButtonAttribute))]
    public class AddButtonAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            float width = position.width / 3;
            Rect objectRect = new(position.x, position.y, position.width - width, position.height);
            Rect buttonRect = new(position.x + position.width - width, position.y, width, position.height);

            bool isAddButton = property.propertyType == SerializedPropertyType.ObjectReference &&
                               property.objectReferenceValue == null;
            
            EditorGUI.PropertyField(isAddButton ? objectRect : position, property, GUIContent.none);
            AddButtonAttribute attr = attribute as AddButtonAttribute;
            
            if (isAddButton && GUI.Button(buttonRect, "New"))
            {
                var fieldInfo = property.serializedObject.targetObject.GetType().GetField(property.name);
                if (fieldInfo != null)
                {
                    var fieldType = fieldInfo.FieldType;
                    
                    if (typeof(ScriptableObject).IsAssignableFrom(fieldType))
                    {
                        UnityEngine.Object newObj = ScriptableObject.CreateInstance(fieldType);
                        string extension = ".asset";

                        string path = EditorUtility.SaveFilePanel("Create New File", "", "New" + extension, extension);
                        if (path != "")
                        {
                            AssetDatabase.CreateAsset(newObj, path);
                            property.objectReferenceValue = newObj;
                        }
                    }
                }
            }

            EditorGUI.EndProperty();
        }
    }
}