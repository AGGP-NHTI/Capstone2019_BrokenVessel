﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour {

    PlayerData data;

    bool dashUnlocked = false;
    bool wallGrabUnlocked = false;
    bool grappleHookUnlocked = false;
    bool phaseUnlocked = false;
    bool phaseDashUnlocked = false;
    bool chargeJumpUnlocked = false;
    int shipControlUnlocked = 0;

    [SerializeField] Transform groundCheck;
    public LayerMask realGround;
    Vector2 boxCheckSize = new Vector2(.9f, .25f);
    bool grounded = true;

    public bool dash = false;
    public float dashTimer = 0f;
    float dashDuration = 0f;


    public bool wallclimb = false;
    bool collide = false;
    bool frontCollide = false;
    bool backCollide = false;
    int walljumpFrames = 0;
    int wallside = 0;

    Rigidbody2D rig;
    [SerializeField] Transform center;
    [SerializeField] Transform back;
    [SerializeField] Transform front;

    private void Start()
    {
        //data = GameObject.Find("DATA").GetComponent<PlayerData>();
        data = GetComponent<PlayerData>();

        dashUnlocked = data.dash;
        wallGrabUnlocked = data.wallGrab;
        grappleHookUnlocked = data.grappleHook;
        phaseUnlocked = data.phase;
        phaseDashUnlocked = data.phaseDash;
        chargeJumpUnlocked = data.chargeJump;
        shipControlUnlocked = data.shipControl;

        rig = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        grounded = Physics2D.OverlapBox(groundCheck.position, boxCheckSize, 0, realGround);
        backCollide = Physics2D.OverlapBox(front.position, new Vector2(.5f, 1f), 0, realGround);
        frontCollide = Physics2D.OverlapBox(front.position, new Vector2(.5f, 1f), 0, realGround);

        //---DASH---------------------------------------------------------------------------------
        if (dashUnlocked)
        {
            if (Input.GetKeyDown(KeyCode.Q) && dashTimer <= 0)
            {
                dash = true;
                dashTimer = .6f;
                dashDuration = .2f;
            }
            if(dash)
            {
                dashDuration -= Time.deltaTime;
                if(dashDuration <= 0)
                {
                    dash = false;
                }
            }
            if(grounded && dashTimer > 0)
            {
                dashTimer -= Time.deltaTime;
            }
        }
        //----------------------------------------------------------------------------------------
        //---WALL GRAB----------------------------------------------------------------------------
        if (wallGrabUnlocked)
        {
            if (!grounded && !Input.GetKey(KeyCode.Space))
            {
                if ((frontCollide || backCollide) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
                {
                    if (transform.localScale.x > 0)
                    {
                        wallclimb = true;
                        wallside = 1;
                    }
                    if (transform.localScale.x < 0)
                    {
                        wallclimb = true;
                        wallside = -1;
                    }
                }
            }
            if (wallclimb)
            {
                if (!Input.GetKey(KeyCode.Space))
                {
                    rig.velocity = Vector2.down;
                }
                else
                {
                    Debug.Log("else");
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && (frontCollide || backCollide) && !grounded)
            {
                Debug.Log("boing");
                walljumpFrames = 30;
            }
            if (walljumpFrames > 0)
            {
                rig.velocity = new Vector2(7 * -wallside, 5);
                walljumpFrames--;
            }

            wallclimb = false;
        }
        //----------------------------------------------------------------------------------------
    }
}
