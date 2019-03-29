using UnityEngine;

namespace BrokenVessel.Interact
{
	[RequireComponent(typeof(BoxCollider2D))]
	public abstract class Interact : MonoBehaviour
	{
		public abstract void Impulse();

		void Reset()
		{
			GetComponent<BoxCollider2D>().isTrigger = true;
			gameObject.layer = 2;
		}
	}
}