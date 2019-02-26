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
			if (grounded = CheckFloor(out float dist))
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
			if (grounded) { velocity.y = jumpStrength; }
		}

		public void HalveJump()
		{
			if (velocity.y > 0 && halvedJump) { velocity.y /= 2f; halvedJump = false; }
		}

		public void Move(float dir)
		{
			// Apply friction
			if (dir == 0 && velocity.x != 0 || Mathf.Abs(velocity.x) > maxSpeed)
			{
				float fric = (grounded ? groundFriction : airFriction) * Time.deltaTime;

				velocity.x -= fric * Mathf.Sign(velocity.x);
				
				// Zero out velocity if slow enough
				if (Mathf.Abs(velocity.x) < fric)
				{
					velocity.x = 0;
				}
			}

			float newVelX = velocity.x + dir * (grounded ? groundSpeed : airSpeed) * Time.deltaTime;
			if (Mathf.Abs(velocity.x) <= maxSpeed)
			{
				velocity.x = Mathf.Clamp(newVelX, -maxSpeed, maxSpeed); // Cap to max speed
			}
		}

		public void Dash(float dir)
		{
			velocity.x = Mathf.Sign(dir) * dashSpeed;
		}

		private bool CheckFloor()
		{
			float temp;
			return CheckFloor(out temp);
		}
		
		private bool CheckFloor(out float dist)
		{
			float half = box.size.y / 2f;
			float quarter = box.size.y / 4f;
			Vector2 size = box.size;
			size.y /= 2f;
			
			RaycastHit2D hit = Physics2D.BoxCast(transform.position + Vector3.up * quarter, size, 0, Vector2.down, -velocity.y * Time.deltaTime + half, collisionMask);

			dist = hit.distance - half;

			return hit.distance != 0;
		}
	}
}