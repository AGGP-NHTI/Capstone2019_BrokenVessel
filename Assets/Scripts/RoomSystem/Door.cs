using UnityEngine;

namespace BrokenVessel.Interact
{
	public class Door : Interact
	{
		public Transform door;

		public override void Impulse()
		{
			Player.Player.This.transform.position = door.position;
		}
	}
}