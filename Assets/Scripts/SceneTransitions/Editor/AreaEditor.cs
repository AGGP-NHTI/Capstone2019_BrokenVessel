using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BrokenVessel.SceneTransitions
{
	[CustomEditor(typeof(Area))]
	[CanEditMultipleObjects]
	public class AreaEditor : Editor
	{
		SerializedProperty collectionProp;

		private void OnEnable()
		{
			collectionProp = serializedObject.FindProperty("collection");
		}

		/*public override void OnInspectorGUI()
		{
			serializedObject.Update();

			EditorGUILayout.IntSlider(numberProp, 0, 100, new GUIContent("Number"));

			serializedObject.ApplyModifiedProperties();
		}*/

		void OnSceneGui()
		{
			Vector3[] verts = new Vector3[]
			{
				new Vector3(-5, 2, 0),
				new Vector3(-5, -2, 0),
				new Vector3(5, 2, 0),
				new Vector3(5, -2, 0)
			};

			Handles.DrawSolidRectangleWithOutline(verts, new Color(0.5f, 0.5f, 0.5f, 0.1f), new Color(0, 0, 0, 1));

			/*foreach (Room room in collectionProp)
			{
				Vector3[] verts = new Vector3[]
				{
					new Vector3(room.leftX, room.topY, 0),
					new Vector3(room.leftX, room.topY, 0),
					new Vector3(room.rightX, room.bottomY, 0),
					new Vector3(room.rightX, room.bottomY, 0)
				};

				Handles.DrawSolidRectangleWithOutline(verts, new Color(0.5f, 0.5f, 0.5f, 0.1f), new Color(0, 0, 0, 1));
			}*/
		}
	}
}