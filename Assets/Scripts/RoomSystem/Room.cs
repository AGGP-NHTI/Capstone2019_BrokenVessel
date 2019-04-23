using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace BrokenVessel.RoomSystem
{
	public enum RoomType { Room, Corridor };

	public class Room : MonoBehaviour
	{
		public RoomType type = RoomType.Room;
		public Rect rect;
		public List<int> doors = new List<int>();
	}
}