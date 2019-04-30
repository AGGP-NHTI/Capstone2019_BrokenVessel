using UnityEngine;

namespace BrokenVessel.Player
{
	[RequireComponent(typeof(PlayerInput))]
	[RequireComponent(typeof(PlayerPhysics))]
    [RequireComponent(typeof(SwitchWeapon))]
    public class Player : MonoBehaviour
	{
		private PlayerInput input;
		private PlayerPhysics phys;
        private SwitchWeapon combat;
        public bool dead = false;

		public static Player This { get; private set; }

		void Start()
		{
			input = GetComponent<PlayerInput>();
			phys = GetComponent<PlayerPhysics>();
            combat = GetComponent<SwitchWeapon>();
			This = this;
		}
		
		void Update()
		{
<<<<<<< HEAD
            if (dead) { return; }

=======
>>>>>>> parent of 8379194... Animation stuff
            if (input.Attack) { combat.Attack(); }
            if(input.Swap) { combat.SwitchEquip(); }
			if (input.Jump) { phys.Jump(); }
			if (input.JumpEnd) { phys.HalveJump(); }
			if (input.Left == input.Right) { phys.Move(0); }
			else if (input.Left) { phys.Move(-1); }
			else if (input.Right) { phys.Move(1); }
			if (input.Dash != 0) { phys.Dash(input.Dash); }

			if (input.Interact)
			{
				foreach (Interact.Interact inter in FindObjectsOfType<Interact.Interact>())
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