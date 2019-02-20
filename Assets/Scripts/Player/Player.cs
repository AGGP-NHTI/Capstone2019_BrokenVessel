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
			if (input.Jump) { phys.Jump(); }
			if (input.JumpEnd) { phys.HalveJump(); }
			if (input.Left == input.Right) { phys.Move(0); }
			else if (input.Left) { phys.Move(-1); }
			else if (input.Right) { phys.Move(1); }
			if (input.Dash) { phys.Dash(); }
		}
	}
}