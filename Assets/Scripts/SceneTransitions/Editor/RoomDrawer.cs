using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BrokenVessel.SceneTransitions
{
	[CustomPropertyDrawer(typeof(Room))]
	public class RoomDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			// Using BeginProperty / EndProperty on the parent property means that
			// prefab override logic works on the entire property.
			EditorGUI.BeginProperty(position, label, property);

			// Draw label
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			// Don't make child fields be indented
			var indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			// Calculate rects
			float w = position.width / 4f;
			var leftRect = new Rect(position.x, position.y, w-2, position.height);
			var rightRect = new Rect(position.x + w, position.y, w-2, position.height);
			var topRect = new Rect(position.x + w * 2f, position.y, w-2, position.height);
			var bottomRect = new Rect(position.x + w * 3f, position.y, w-2, position.height);

			// Draw fields - pass GUIContent.none to each so they are drawn without labels
			EditorGUI.PropertyField(leftRect, property.FindPropertyRelative("leftX"), GUIContent.none);
			EditorGUI.PropertyField(rightRect, property.FindPropertyRelative("rightX"), GUIContent.none);
			EditorGUI.PropertyField(topRect, property.FindPropertyRelative("topY"), GUIContent.none);
			EditorGUI.PropertyField(bottomRect, property.FindPropertyRelative("bottomY"), GUIContent.none);

			// Set indent back to what it was
			EditorGUI.indentLevel = indent;

			EditorGUI.EndProperty();
		}
	}
}