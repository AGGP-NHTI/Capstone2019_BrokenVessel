using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour {

    PlayerData data;
    PlayerMovement pm;
    PlayerAbilities pa;

    float maxSpeed = 0;
    float jumpForce = 0;

    float dashTimer = 0f;

    bool sideCollide = false;
    public Transform center;

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

        maxSpeed = data.speed;
        jumpForce = data.jumpForce;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //----------------------------------------------------------------------------------------
        Vector2 processVelocity = rig.velocity;
        processVelocity.x = pm.move * maxSpeed;
        grounded = Physics2D.OverlapBox(groundCheck.position, boxCheckSize, 0, realGround);
        sideCollide = Physics2D.OverlapBox(center.position, new Vector2(.5f, 1f), 0, realGround);
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

        //----------------------------------------------------------------------------------------
        //---JUMP---------------------------------------------------------------------------------
        if (grounded && pm.isJumping)
        {
            processVelocity.y = jumpForce;
        }
        if (!pm.isJumping)
        {
            if (processVelocity.y > 0)
            {
                processVelocity.y /= 3f;
            }
        }
        //----------------------------------------------------------------------------------------
        //---PROCESS------------------------------------------------------------------------------
        if (!grounded)
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
}
