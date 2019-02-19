using UnityEngine;

namespace BrokenVessel.Player
{
	public class PlayerPhysics : MonoBehaviour
	{
		[Header("Stats")]
		[SerializeField]
		[Range(0, 25)]
		private float jumpStrength = 10;

		[Header("Physics")]
		[SerializeField]
		[Range(0, 50)]
		private float gravity = 20;
		[SerializeField]
		[Range(0, 50)]
		private float terminalVelocity = 20;
		[SerializeField]
		private LayerMask collisionMask;

		private Vector2 velocity = Vector2.zero;
		private bool grounded = false;

		void Start()
		{

		}

		void Update()
		{
			// Get position
			Vector3 pos = transform.position;

			// Apply gravity
			velocity.y -= gravity * Time.deltaTime;
			velocity.y = Mathf.Max(velocity.y, -terminalVelocity);

			// Check floor
			float dist;
			if (CheckFloor(out dist))
			{
				pos.y -= dist;
				velocity.y = 0;
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

		private bool CheckFloor()
		{
			float temp;
			return CheckFloor(out temp);
		}
		
		private bool CheckFloor(out float dist)
		{
			RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, -velocity.y * Time.deltaTime + 0.5f, collisionMask);

			dist = hit.distance - 0.5f;

			return hit.distance != 0;
		}
	}
}