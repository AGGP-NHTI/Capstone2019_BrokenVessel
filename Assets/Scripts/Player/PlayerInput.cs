using UnityEngine;

namespace BrokenVessel.Player
{
	public class PlayerInput : MonoBehaviour
	{
		[SerializeField]
		private KeyCode jumpKey = KeyCode.Space;
		[SerializeField]
		private KeyCode interactKey = KeyCode.E;
		[SerializeField]
		private KeyCode leftKey = KeyCode.A;
		[SerializeField]
		private KeyCode rightKey = KeyCode.D;
		[SerializeField]
		private float dblClickThreshold = 0.2f;

		public bool Jump { get => Input.GetKeyDown(jumpKey); }
		public bool JumpEnd { get => Input.GetKeyUp(jumpKey); }
		public bool Interact { get => Input.GetKeyDown(interactKey); }
		public bool Left { get => Input.GetKey(leftKey); }
		public bool Right { get => Input.GetKey(rightKey); }
		public float Dash { get; private set; }

		private float time = 0;
		private KeyCode lastKey = KeyCode.None;

		void Update()
		{
			Dash = 0;

			if (Input.GetKeyDown(rightKey) || Input.GetKeyDown(leftKey))
			{
				if (Input.GetKeyDown(rightKey) && lastKey != rightKey) { lastKey = KeyCode.None; }
				if (Input.GetKeyDown(leftKey) && lastKey != leftKey) { lastKey = KeyCode.None; }

				if (Time.time < time + dblClickThreshold && lastKey != KeyCode.None)
				{
					Dash = lastKey == rightKey ? 1 : -1;
				}
				else
				{
					time = Time.time;
				}
				lastKey = KeyCode.None;
			}

			if (Input.GetKeyDown(rightKey)) { lastKey = rightKey; }
			if (Input.GetKeyDown(leftKey)) { lastKey = leftKey; }
		}
	}
}