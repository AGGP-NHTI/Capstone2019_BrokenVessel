using UnityEngine;

namespace BrokenVessel.Player
{
	public class PlayerInput : MonoBehaviour
	{
        [SerializeField]
        private KeyCode attackKey = KeyCode.Mouse0;
        [SerializeField]
        private KeyCode ControllerAttack = KeyCode.JoystickButton5; //RB
        [SerializeField]
        private KeyCode swapKey = KeyCode.Mouse1;
        [SerializeField]
        private KeyCode ControllerSwap = KeyCode.JoystickButton4; //LB

        [SerializeField]
		private KeyCode jumpKey = KeyCode.Space;
        [SerializeField]
        private KeyCode ControllerJump = KeyCode.JoystickButton0; //A
        [SerializeField]
		private KeyCode interactKey = KeyCode.E;
        [SerializeField]
        private KeyCode ControllerInteract = KeyCode.JoystickButton3; //Y
        [SerializeField]
        private KeyCode ControllerDash = KeyCode.JoystickButton2; //X
        [SerializeField]
		private KeyCode leftKey = KeyCode.A;
		[SerializeField]
		private KeyCode rightKey = KeyCode.D;
		[SerializeField]
		private float dblClickThreshold = 0.2f;


        public bool Attack { get => Input.GetKeyDown(attackKey) || Input.GetKeyDown(ControllerAttack); }
        public bool Swap { get => Input.GetKeyDown(swapKey) || Input.GetKeyDown(ControllerSwap); }
        public bool Jump { get => Input.GetKeyDown(jumpKey) || Input.GetKeyDown(ControllerJump); }
		public bool JumpEnd { get => Input.GetKeyUp(jumpKey) || Input.GetKeyUp(ControllerJump); }
		public bool Interact { get => Input.GetKeyDown(interactKey) || Input.GetKeyDown(ControllerInteract); }
		public bool Left { get => Input.GetKey(leftKey) || Input.GetAxis("Horizontal") == -1; }
		public bool Right { get => Input.GetKey(rightKey) || Input.GetAxis("Horizontal") == 1; }
		public float Dash { get; private set; }

        private float time = 0;
		private KeyCode lastKey = KeyCode.None;

		void Update()
		{
			Dash = 0;

			if (Input.GetKeyDown(rightKey) || Input.GetKeyDown(leftKey) || Input.GetKeyDown(ControllerDash))
			{
				if (Input.GetKeyDown(rightKey) && lastKey != rightKey) { lastKey = KeyCode.None; }
				if (Input.GetKeyDown(leftKey) && lastKey != leftKey) { lastKey = KeyCode.None; }
                if (Input.GetKeyDown(ControllerDash) && lastKey != ControllerDash) { lastKey = KeyCode.None; }

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
            if (Input.GetKeyDown(ControllerDash)) { lastKey = ControllerDash; }
        }
	}
}