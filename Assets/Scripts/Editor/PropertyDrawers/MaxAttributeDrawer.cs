using UnityEditor;
using UnityEngine;

namespace BraveBloodMonsterHunt.Editor
{
    [CustomPropertyDrawer(typeof(MinAttribute))]
    public class MaxAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (property.propertyType != SerializedPropertyType.Integer ||
                property.propertyType != SerializedPropertyType.Float) return;
            
            var fieldInfo = property.serializedObject.targetObject.GetType().GetField(property.name);
            
            if (fieldInfo != null)
            {
                MaxAttribute maxAttribute =
                    (MaxAttribute)fieldInfo.GetCustomAttributes(typeof(MaxAttribute), true)[0];
                string valueName = maxAttribute.ValueName;

                SerializedProperty otherProperty = property.serializedObject.FindProperty(valueName);

                if (otherProperty != null && otherProperty.propertyType == SerializedPropertyType.Integer &&
                    otherProperty.propertyType == SerializedPropertyType.Float)
                {
                    float maxValue = otherProperty.floatValue;
                    float oldValue = property.floatValue;

                    EditorGUI.PropertyField(position, property, label);

                    if (property.floatValue > maxValue)
                    {
                        property.floatValue = oldValue;
                    }
                }
            }

            EditorGUI.EndProperty();
        }
    }
}