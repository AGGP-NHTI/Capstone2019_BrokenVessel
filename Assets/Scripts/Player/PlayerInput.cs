using UnityEngine;

namespace BrokenVessel.Player
{
	public class PlayerInput : MonoBehaviour
	{
		[SerializeField]
		private KeyCode jumpKey = KeyCode.Space;
		[SerializeField]
		private KeyCode leftKey = KeyCode.A;
		[SerializeField]
		private KeyCode rightKey = KeyCode.D;
		[SerializeField]
		private float dblClickThreshold = 0.2f;

		public bool Jump { get => Input.GetKeyDown(jumpKey); }
		public bool JumpEnd { get => Input.GetKeyUp(jumpKey); }
		public bool Left { get => Input.GetKey(leftKey); }
		public bool Right { get => Input.GetKey(rightKey); }
		public bool Dash { get; private set; }

		private float time = 0;

		void Update()
		{
			Dash = false;

			if (Input.GetKeyDown(rightKey) || Input.GetKeyDown(leftKey))
			{
				if (Time.time < time + dblClickThreshold)
				{
					Dash = true;
				}
				else
				{
					time = Time.time;
				}
			}
		}
	}
}