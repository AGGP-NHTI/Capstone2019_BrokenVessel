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

		public bool jump { get => Input.GetKeyDown(jumpKey); }
		public bool jumpEnd { get => Input.GetKeyUp(jumpKey); }
		public bool left { get => Input.GetKey(leftKey); }
		public bool right { get => Input.GetKey(rightKey); }
	}
}