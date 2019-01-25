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

    bool dashing = false;
    int dashFrames = 0;
    float dashTimer = 0f;

    bool wallclimb = false;

    Rigidbody2D rig;

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

        //---DASH---------------------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            dashTimer = 0f;
        }
        if (dashUnlocked && Input.GetKeyDown(KeyCode.Q) && !dashing && dashTimer <= 0)
        {
            Debug.Log("DASH!");
            dashing = true;
            dashFrames = 7;
            dashTimer = .5f;
        }
        if (dashTimer > 0 && grounded)
        {
            dashTimer -= Time.deltaTime;
        }
        if (dashing)
        {
            dashFrames--;
            rig.velocity = Vector2.right * transform.localScale.x * 25;
            Debug.Log("DASH FRAME: " + dashFrames);
            if (dashFrames == 0)
            {
                Debug.Log("DASH END");
                dashing = false;
            }
        }
        //----------------------------------------------------------------------------------------
        //---WALL CLIMB---------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------
    }
}
