using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour {

    PlayerData data;
    PlayerMovement pm;
    PlayerAbilities pa;

    float maxSpeed = 0;
    float jumpForce = 0;

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
        rig.velocity = new Vector2(processVelocity.x, processVelocity.y);
        //----------------------------------------------------------------------------------------
    }
}
