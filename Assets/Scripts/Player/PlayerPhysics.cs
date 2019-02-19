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
		private float maxSpeed = 5;
		[SerializeField]
		private float groundSpeed = 25;
		[SerializeField]
		private float airSpeed = 5;
		[SerializeField]
		private float groundFric = 25;
		[SerializeField]
		private float airFric = 0;

		[Header("Physics")]
		[SerializeField]
		private float gravity = 20;
		[SerializeField]
		private float terminalVelocity = 20;
		[SerializeField]
		private LayerMask collisionMask;

		private Vector2 velocity = Vector2.zero;
		private bool grounded = false;
		private bool halvedJump = false;
		private BoxCollider2D box;

		void Start()
		{
			box = GetComponent<BoxCollider2D>();
		}

		void Update()
		{
			// Get position
			Vector3 pos = transform.position;

			// Apply gravity
			velocity.y -= gravity * Time.deltaTime;
			velocity.y = Mathf.Max(velocity.y, -terminalVelocity); // Cap to terminal velocity

			// Check floor
			float dist;
			if (CheckFloor(out dist))
			{
				pos.y -= dist; // Snap to floor
				velocity.y = 0; // Remove gravity
				halvedJump = true; // Reset jump halver
			}

			// Move position
			pos += (Vector3)velocity * Time.deltaTime;

			// Set position
			transform.position = pos;
		}

		public void Jump()
		{
			if (CheckFloor()) { velocity.y = jumpStrength; }
		}

		public void HalveJump()
		{
			if (velocity.y > 0 && halvedJump) { velocity.y /= 2f; halvedJump = true; }
		}

		public void Move(float dir)
		{
			// Apply friction
			if (dir == 0 && velocity.x != 0)
			{
				float fric = CheckFloor() ? groundFric : airFric * Time.deltaTime;

				velocity.x -= fric * Mathf.Sign(velocity.x);

				// Zero out velocity if slow enough
				if (Mathf.Abs(velocity.x) < fric)
				{
					velocity.x = 0;
				}
			}

			velocity.x += dir * (CheckFloor() ? groundSpeed : airSpeed) * Time.deltaTime;
			velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed); // Cap to max speed
		}

		private bool CheckFloor()
		{
			float temp;
			return CheckFloor(out temp);
		}
		
		private bool CheckFloor(out float dist)
		{
			RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, -velocity.y * Time.deltaTime + box.size.y / 2f, collisionMask);

			dist = hit.distance - box.size.y / 2f;

			return hit.distance != 0;
		}
	}
}