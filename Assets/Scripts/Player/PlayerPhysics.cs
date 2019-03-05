using UnityEngine;

namespace BrokenVessel.Player
{
	[RequireComponent(typeof(BoxCollider2D))]
	public class PlayerPhysics : MonoBehaviour
	{
		[Header("Stats")]
		[SerializeField]
		private float jumpStrength = 10;
		[SerializeField]
		private float dashSpeed = 10;
		[SerializeField]
		private float maxSpeed = 5;

		[Header("Physics")]
		[SerializeField]
		private float groundSpeed = 45;
		[SerializeField]
		private float groundFriction = 25;
		[SerializeField]
		private float airSpeed = 10;
		[SerializeField]
		private float airFriction = 0;
		[SerializeField]
		private float gravity = 20;
		[SerializeField]
		private float terminalVelocity = 20;

		[Header("Technical")]
		[SerializeField]
		private LayerMask collisionMask;

		private Vector2 velocity = Vector2.zero;
		private bool grounded = false;
		private bool halvedJump = false;
		private BoxCollider2D box;
		private Rigidbody2D rg;

		void Start()
		{
			box = GetComponent<BoxCollider2D>();
			rg = GetComponent<Rigidbody2D>();
		}

		void Update()
		{
			// Check floor
			if (grounded = CheckFloor())
			{
				halvedJump = true; // Reset jump halver
			}
		}

		public void Jump()
		{
			if (grounded)
			{
				rg.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
			}
		}

		public void HalveJump()
		{
			if (velocity.y > 0 && halvedJump)
			{
				halvedJump = false;

				Vector2 vel = rg.velocity;
				vel.y /= 2f;
				rg.velocity = vel;
			}
		}

		public void Move(float dir)
		{
			Vector2 vel = rg.velocity;

			if (dir == 0 && vel.x != 0 || Mathf.Abs(vel.x) > maxSpeed)
			{
				float fric = (grounded ? groundFriction : airFriction) * Time.deltaTime;

				vel.x -= fric * Mathf.Sign(vel.x);

				// Zero out velocity if slow enough
				if (Mathf.Abs(vel.x) < fric)
				{
					vel.x = 0;
				}
			}

			float newVelX = vel.x + dir * (grounded ? groundSpeed : airSpeed) * Time.deltaTime;
			if (Mathf.Abs(vel.x) <= maxSpeed)
			{
				vel.x = Mathf.Clamp(newVelX, -maxSpeed, maxSpeed); // Cap to max speed
			}

			rg.velocity = vel;
		}

		public void Dash(float dir)
		{
			rg.AddForce(Vector2.right * Mathf.Sign(dir) * dashSpeed, ForceMode2D.Impulse);
		}
		
		private bool CheckFloor()
		{
			return Physics2D.BoxCast(transform.position, box.size, 0, Vector2.down, 0.1f, collisionMask).distance != 0;
		}
	}
}