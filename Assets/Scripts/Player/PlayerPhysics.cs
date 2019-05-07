using UnityEngine;

namespace BrokenVessel.Player
{
	[RequireComponent(typeof(BoxCollider2D))]
	public class PlayerPhysics : BrokenVessel.Actor.Actor
    {
		[Header("Stats")]
		[SerializeField]
		private float jumpStrength = 10;
		[SerializeField]
		private float wallJumpStrength = 10;
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
		private float wallStickTime = 0.1f;
		[SerializeField]
		private float wallJumpAngle = 45;
		[SerializeField]
		private WallJumpSide wallJumpSide = WallJumpSide.AwayWall;

		public enum WallJumpSide
		{
			ToWall, AwayWall, Both
		}

		[Header("Technical")]
		[SerializeField]
		private LayerMask collisionMask;
		[SerializeField]
		private Animator anim;
		[SerializeField]
		private Transform mesh;
		
		private bool grounded = false;
		private bool halvedJump = false;
		private float stickFrames = 0;
		private bool canStickWall = false;
		private float lastDir = 0;
		private BoxCollider2D box;
		private Rigidbody2D rg;

		void Start()
		{
			box = GetComponent<BoxCollider2D>();
			rg = GetComponent<Rigidbody2D>();
		}

		void Update()
		{
            if (paused) { return; }
            // Check floor
            if (grounded = CheckFloor())
			{
				halvedJump = true; // Reset jump halver
			}

			// Stick to wall
			if (CheckWall() && canStickWall && !grounded && stickFrames <= 0)
			{
				stickFrames = wallStickTime;
				canStickWall = false;
			}
			// Decrement
			if (stickFrames > 0) { stickFrames -= Time.deltaTime; }

			// Release and reset if not next to wall or grounded
			if (!CheckWall() || grounded)
			{
				stickFrames = 0;
				canStickWall = true;
			}

			// Animation
			anim.SetBool("PlayerJumping", !grounded);
			mesh.localPosition = Vector3.zero;
			anim.SetBool("PlayerLeftWallSlide", !grounded && (CheckRightWall() || CheckLeftWall()));

		}

		public void Jump()
		{
            if (paused) { return; }

            if (grounded)
			{
				rg.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
			}
			else if (CheckWall())
			{
				if (wallJumpSide == WallJumpSide.ToWall) { lastDir = -lastDir; }

				// Prevent wall jumping into the wall
				if (lastDir == 0)
				{
					return;
				}
				else if (wallJumpSide == WallJumpSide.Both)
				{
					lastDir = CheckLeftWall() ? 1 : -1;
				}
				else if (lastDir > 0 && !CheckLeftWall() || lastDir < 0 && !CheckRightWall())
				{
					return;
				}

				// Remove y velocity and stick frames
				rg.velocity = new Vector2(rg.velocity.x, 0);
				stickFrames = 0;

				// Apply force
				rg.AddForce(Quaternion.Euler(0, 0, (90f - wallJumpAngle) * (lastDir > 0 ? -1 : 1)) * Vector2.up * wallJumpStrength, ForceMode2D.Impulse);
			}
		}

		public void HalveJump()
		{
            if (paused) { return; }
            Vector2 vel = rg.velocity;

			if (halvedJump && vel.y > 0)
			{
				halvedJump = false;
				
				vel.y /= 2f;
				rg.velocity = vel;
			}
		}

		public void Move(float dir)
		{
            if (paused) { return; }
            lastDir = dir;

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

			if (stickFrames > 0)
			{
				vel.x = 0;
			}

			rg.velocity = vel;

			// Animation
			anim.SetBool("PlayerWalking", dir != 0 && grounded);
			if (dir == -1) { mesh.localScale = new Vector3(20, 20, 20); }
			if (dir == 1) { mesh.localScale = new Vector3(-20, 20, 20); }
		}

		public void Dash(float dir)
		{
            if (paused) { return; }
            rg.AddForce(Vector2.right * Mathf.Sign(dir) * dashSpeed, ForceMode2D.Impulse);
				anim.SetTrigger("PlayerDash");
		}
		
		private bool CheckFloor()
		{
            return Physics2D.BoxCast(transform.position, box.size, 0, Vector2.down, 0.1f, collisionMask).distance != 0;
		}

		private bool CheckWall()
		{
            Vector2 size = box.size;
			size.x *= 1.05f;
			size.y *= 0.75f;
			return Physics2D.OverlapBox(transform.position, size, 0, collisionMask) != null;
		}

		private bool CheckLeftWall()
		{
			Vector2 size = box.size;
			size.y *= 0.75f;
			return Physics2D.OverlapBox(transform.position + Vector3.left * 0.025f, size, 0, collisionMask) != null;
		}

		private bool CheckRightWall()
		{
			Vector2 size = box.size;
			size.y *= 0.75f;
			return Physics2D.OverlapBox(transform.position + Vector3.right * 0.025f, size, 0, collisionMask) != null;
		}
	}
}