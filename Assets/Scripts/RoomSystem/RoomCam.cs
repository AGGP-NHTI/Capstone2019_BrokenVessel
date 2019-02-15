using BrokenVessel.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrokenVessel.RoomSystem
{
	public class RoomCam : MonoBehaviour
	{
		[SerializeField]
		private float innerBuffer = 2.5f;
		[SerializeField]
		private float outerBuffer = 5f;
		
		[SerializeField]
		private Transform tracker;
		
		void Update()
		{
			Vector3 currentPos = transform.position;
			Vector3 targetPos = tracker.position;

			// Normalize z values
			targetPos.z = currentPos.z;

			// Clamp targetPos
			targetPos = VectorClampRect(targetPos, RoomManager.CurrentRect);

			// Set pos
			transform.position = (5f * currentPos + targetPos) / 6f; ;
		}
		
		private Vector3 VectorClampRect(Vector3 vec, Rect rect)
		{
			float width = rect.width / 2f; // - innerBuffer
			float height = rect.height / 2f; // - innerBuffer

			Vector3 newVec = vec;
			newVec.x = Mathf.Clamp(vec.x, rect.x - width, rect.x + width);
			newVec.y = Mathf.Clamp(vec.y, rect.y - height, rect.y + height);

			if (newVec != vec)
			{
				//newVec.x = Mathf.Log(newVec.x, vec.x);
				//newVec.y = Mathf.Log(newVec.y, vec.y);
				newVec = (9f * newVec + vec) / 10f;
			}

			return newVec;
		}
	}
}