using UnityEngine;

namespace BrokenVessel.Player
{
	[RequireComponent(typeof(PlayerInput))]
	[RequireComponent(typeof(PlayerPhysics))]
	public class Player : MonoBehaviour
	{
		private PlayerInput input;
		private PlayerPhysics phys;

		public static Player This { get; private set; }

		void Start()
		{
			input = GetComponent<PlayerInput>();
			phys = GetComponent<PlayerPhysics>();
			This = this;
		}
		
		void Update()
		{
			if (input.Jump) { phys.Jump(); }
			if (input.JumpEnd) { phys.HalveJump(); }
			if (input.Left == input.Right) { phys.Move(0); }
			else if (input.Left) { phys.Move(-1); }
			else if (input.Right) { phys.Move(1); }
			if (input.Dash != 0) { phys.Dash(input.Dash); }

			if (input.Interact)
			{
				foreach (Interact inter in FindObjectsOfType<Interact>())
				{
					if (GetComponent<BoxCollider2D>().bounds.Intersects(inter.GetComponent<BoxCollider2D>().bounds))
					{
						inter.Impulse();
					}
				}
			}
		}
	}
}