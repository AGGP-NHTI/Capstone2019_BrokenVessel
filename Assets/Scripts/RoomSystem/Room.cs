using BrokenVessel.Utility;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace BrokenVessel.RoomSystem
{
	public enum RoomType { Room, Corridor };

	public class Room : MonoBehaviour
	{
		public RoomType type = RoomType.Room;
		public Rect rect;
		public List<int> doors = new List<int>();
	}

	[CustomEditor(typeof(Room))]
	public class RoomEditor : Editor
	{
		void OnSceneGUI()
		{
			// Shorthand ref
			Room room = (Room)target;
			
			// Get resized rects
			Rect newRect = RectUtils.ResizeRect(
				room.rect,
				Handles.CubeHandleCap,
				Color.blue,
				GetColorType(room),
				HandleUtility.GetHandleSize(Vector3.zero) * .1f,
				.1f);

			// Set each room with new rects
			room.rect = newRect;
		}

		private Color GetColorType(Room room)
		{
			switch (room.type)
			{
				case RoomType.Room: return new Color(1f, 1f, 1f, 0.33f);
				case RoomType.Corridor: return new Color(0.5f, 0.5f, 0.5f, 0.33f);
			}

			return new Color(0, 0, 0, 0);
		}

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			
			if (GUILayout.Button("Save to File"))
			{
				SaveToFile((Room)target);
			}

			if (GUILayout.Button("Load Connected Rooms"))
			{
				RoomManager.LoadRoomByDoorID(((Room)target).doors);
			}
		}

		private void SaveToFile(Room room)
		{
			// Get room rect
			string text = $"{room.rect.x}\t{room.rect.y}\t{room.rect.width}\t{room.rect.height}\n";
			
			// Get door IDs
			for (int i = 0; i < room.doors.Count; ++i)
			{
				text += $"{room.doors[i]}\t";
			}

			// Newline
			text += "\n";

			foreach (Transform child in room.gameObject.transform)
			{
				// Get original prefab name
				text += $"{PrefabUtility.GetCorrespondingObjectFromSource(child).name}\t";

				// Get transform
				text += $"{child.position.x}\t{child.position.y}\t{child.position.z}\t";
				text += $"{child.rotation.x}\t{child.rotation.y}\t{child.rotation.z}\t{child.rotation.w}\t";
				text += $"{child.localScale.x}\t{child.lossyScale.y}\t{child.lossyScale.z}\t";

				// Newline
				text += "\n";
			}

			File.WriteAllText($"{Application.dataPath}/Resources/Rooms/{room.name}.txt", text);
			AssetDatabase.Refresh();
		}
	}
}