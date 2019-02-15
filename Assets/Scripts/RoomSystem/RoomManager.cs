using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using BrokenVessel.Utility;

namespace BrokenVessel.RoomSystem
{
	public class RoomManager : MonoBehaviour
	{
		[SerializeField]
		private Transform player;
		[SerializeField]
		private TextAsset startingRoom;

		public static List<Room> Rooms { get; private set; } = new List<Room>();
		public static Room CurrentRoom { get; private set; }
		public static Rect CurrentRect { get => CurrentRoom.rect; }

		void Start()
		{
			LoadFromFile(startingRoom.name);

			CurrentRoom = Rooms[0];
			LoadRoomByDoorID(Rooms[0].doors);
		}

		void Update()
		{
			if (!RectUtils.VectorInRect(player.position, CurrentRect))
			{
				foreach (Room room in Rooms)
				{
					if (RectUtils.VectorInRect(player.position, room.rect))
					{
						CurrentRoom = room;
						LoadRoomByDoorID(room.doors);
						RemoveOldRooms();
						break;
					}
				}
			}
		}

		public static void LoadFromFile(string fileName)
		{
			// Create empty room
			GameObject room = new GameObject(fileName);
			room.AddComponent<Room>();
			Room scr = room.GetComponent<Room>();
			Rooms.Add(scr);
			
			// Get text
			string text = Resources.Load<TextAsset>(fileName).text;

			// Split text
			string[] array = text.Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

			// Counter
			int counter = 0;

			foreach (string line in array)
			{
				string[] values = line.Split("\t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

				if (counter == 0)
				{
					// Set rect
					scr.rect.x = float.Parse(values[0]);
					scr.rect.y = float.Parse(values[1]);
					scr.rect.width = float.Parse(values[2]);
					scr.rect.height = float.Parse(values[3]);
				}
				else if (counter == 1)
				{
					// Set door IDs
					for (int i = 0; i < values.Length; ++i)
					{
						scr.doors.Add(int.Parse(values[i]));
					}
				}
				else
				{
					// Spawn prefabs
					GameObject obj = PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>(values[0])) as GameObject;
					obj.transform.parent = room.transform;
					obj.transform.position = new Vector3(float.Parse(values[1]), float.Parse(values[2]), float.Parse(values[3]));
					obj.transform.rotation = new Quaternion(float.Parse(values[4]), float.Parse(values[5]), float.Parse(values[6]), float.Parse(values[7]));
					obj.transform.localScale = new Vector3(float.Parse(values[8]), float.Parse(values[9]), float.Parse(values[10]));
				}

				++counter;
			}
		}

		public static void LoadRoomByDoorID(List<int> doorIDs)
		{
			RemoveNullRooms();

			foreach (int doorID in doorIDs)
			{
				LoadRoomByDoorID(doorID);
			}
		}

		public static void LoadRoomByDoorID(int doorID)
		{
			string[] files = Directory.GetFiles($"{Application.dataPath}/Resources/", "*.txt");

			foreach (string file in files)
			{
				string name = Path.GetFileName(file);
				name = name.Remove(name.Length-4);

				// Skip if room exists
				Predicate<Room> nameFinder = (Room r) => { return r.name == name; };
				if (Rooms.Exists(nameFinder)) { continue; }

				using (StreamReader reader = new StreamReader(file))
				{
					reader.ReadLine(); // Ignore first line
					string[] doorIDs = reader.ReadLine().Split("\t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
					
					for (int i = 0; i < doorIDs.Length; ++i)
					{
						if (int.Parse(doorIDs[i]) == doorID)
						{
							string path = Path.GetFileName(file);
							path = path.Remove(path.Length - 4);
							LoadFromFile(path);

							Debug.Log($"Loaded {path} with door ID {doorID}");
						}
					}
				}
			}
		}

		private static void RemoveOldRooms()
		{
			foreach (Room room in Rooms.ToArray())
			{
				bool keep = false;

				foreach (int i in room.doors)
				{
					if (CurrentRoom.doors.Contains(i))
					{
						keep = true;
					}
				}

				// Remove if door ID doesn't match
				if (!keep)
				{
					Rooms.Remove(room);
					Destroy(room.gameObject);
				}
			}
		}

		private static void RemoveNullRooms()
		{
			foreach (Room room in Rooms.ToArray())
			{
				if (room == null) { Rooms.Remove(room); }
			}
		}
	}

	[CustomEditor(typeof(RoomManager))]
	public class RoomManagerEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			RoomManager scr = (RoomManager)target;
			if (GUILayout.Button("Load from File"))
			{
				string path = EditorUtility.OpenFilePanel("Load from File", $"{Application.dataPath}/Resources/", "txt");
				if (path.Length != 0)
				{
					path = Path.GetFileName(path);
					RoomManager.LoadFromFile(path.Remove(path.Length-4));
				}
			}
			/*if (GUILayout.Button("Clear Rooms"))
			{
				RoomManager.Rooms.Clear();
			}*/
		}
	}
}