using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SurferRun.Util;

namespace SurferRun.Editor
{

    [CustomPropertyDrawer(typeof(MinMax))]
    public class MinMaxDrawer : PropertyDrawer
    {

        private readonly float offset = 0.0f;
        private readonly float labelWidthScale = 0.15f;
        private readonly float fieldWidthScale = 0.3f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, label);

            float labelWidth = position.width * labelWidthScale;
            float fieldWidth = position.width * fieldWidthScale;

            Rect minLabelRect = new Rect(position.x, position.y, labelWidth, position.height);
            Rect minValueRect = new Rect(minLabelRect.x + minLabelRect.width + offset, position.y, fieldWidth, position.height);

            Rect maxLabelRect = new Rect(minValueRect.x + minValueRect.width + offset, position.y, labelWidth, position.height);
            Rect maxValueRect = new Rect(maxLabelRect.x + maxLabelRect.width + offset, position.y, fieldWidth, position.height);

            EditorGUI.LabelField(minLabelRect, "Min:");
            EditorGUI.PropertyField(minValueRect, property.FindPropertyRelative("min"), GUIContent.none);

            EditorGUI.LabelField(maxLabelRect, "Max:");
            EditorGUI.PropertyField(maxValueRect, property.FindPropertyRelative("max"), GUIContent.none);

            if (GUI.changed)
            {
                if (property.FindPropertyRelative("min").floatValue >
                     property.FindPropertyRelative("max").floatValue)
                {
                    property.FindPropertyRelative("max").floatValue =
                        property.FindPropertyRelative("min").floatValue;
                }
            }

            EditorGUI.EndProperty();
        }
    }
}
