using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrokenVessel.SceneTransitions
{
	[System.Serializable]
	public class Room
	{
		public float leftX;
		public float rightX;
		public float topY;
		public float bottomY;

		public Room(float leftX, float rightX, float topY, float bottomY)
		{
			this.leftX = leftX;
			this.rightX = rightX;
			this.topY = topY;
			this.bottomY = bottomY;
		}
	}
}