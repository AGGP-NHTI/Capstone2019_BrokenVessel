using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour {

    PlayerData data;
    PlayerMovement pm;
    PlayerAbilities pa;

    float speed = 0;
    float jumpForce = 0;

    bool wallJumping = false;
    int wallside = 0;

    bool sideCollide = false;
    [SerializeField] Transform center;

    bool grounded = false;
    public Transform groundCheck;
    public LayerMask realGround;

    Vector2 boxCheckSize = new Vector2(.9f, .25f);

    Rigidbody2D rig;

    void Start ()
    {
        data = GetComponent<PlayerData>();
        pm = GetComponent<PlayerMovement>();
        pa = GetComponent<PlayerAbilities>();
        rig = GetComponent<Rigidbody2D>();

        speed = data.speed;
        jumpForce = data.jumpForce;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //----------------------------------------------------------------------------------------
        Vector2 processVelocity = rig.velocity;
        if(!wallJumping)
        {
            processVelocity.x = pm.move * speed;
        }
        grounded = Physics2D.OverlapBox(groundCheck.position, boxCheckSize, 0, realGround);
        sideCollide = Physics2D.OverlapBox(center.position, new Vector2(1.6f, 1f), 0, realGround);
        //----------------------------------------------------------------------------------------
        //---DASH---------------------------------------------------------------------------------
        if ((pm.isJumping && grounded) || (sideCollide && !grounded))
        {
            pa.dashTimer = 0f;
        }
        if (pa.dash)
        {
            processVelocity.x = transform.localScale.x * 20;
        }
        if (grounded && pa.dashTimer > 0)
        {
            pa.dashTimer -= Time.deltaTime;
        }

        //----------------------------------------------------------------------------------------
        //---JUMP---------------------------------------------------------------------------------
        if (grounded && pm.isJumping)
        {
            processVelocity.y = jumpForce;
        }
        //----------------------------------------------------------------------------------------
        //---WALL JUMP----------------------------------------------------------------------------
        if (pa.wallClimb)
        {
            processVelocity.y = -1f;
        }
        if (wallJumping)
        {
            if (Mathf.Sign(pm.move) == Mathf.Sign(wallside))
            {
                //processVelocity.x = .5f * speed * pm.move;
                //processVelocity.y = -1f ;
                //pa.wallJump = false;
            }
        }

        if (grounded || sideCollide)
        {
            wallJumping = false;
        }
        if (pa.wallJump && sideCollide && !grounded)
        {
            wallJumping = true;
            processVelocity.y = jumpForce;
            processVelocity.x = speed * -wallside;
        }


        //----------------------------------------------------------------------------------------
        //---PROCESS------------------------------------------------------------------------------
        if (!pa.wallJump && !pm.isJumping)
        {
            if (processVelocity.y > 0)
            {
                processVelocity.y /= 3f;
            }
        }
        if (!grounded && !pa.wallClimb)
        {
            processVelocity.y -= 18f * Time.deltaTime;

            if (processVelocity.y < -18f)
            {
                processVelocity.y = -18f;
            }
        }
        if (pa.dash)
        {
            processVelocity.y = 0;
        }
        rig.velocity = new Vector2(processVelocity.x, processVelocity.y);
        //----------------------------------------------------------------------------------------
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (transform.position.x > collision.transform.position.x)
        {
            wallside = -1;
        }
        else if (transform.position.x < collision.transform.position.x)
        {
            wallside = 1;
        }
    }
}
