using UnityEngine;

namespace BrokenVessel.Player
{
	[RequireComponent(typeof(PlayerInput))]
	[RequireComponent(typeof(PlayerPhysics))]
	public class Player : MonoBehaviour
	{
		private PlayerInput input;
		private PlayerPhysics phys;

		void Start()
		{
			input = GetComponent<PlayerInput>();
			phys = GetComponent<PlayerPhysics>();
		}
		
		void Update()
		{
			if (input.jump) { phys.Jump(); }
			if (input.jumpEnd) { phys.HalveJump(); }
			if (input.left == input.right) { phys.Move(0); }
			else if (input.left) { phys.Move(-1); }
			else if (input.right) { phys.Move(1); }
		}
	}
}