using System.Collections;
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


    public bool wallClimb = false;
    public bool wallJump = false;

    bool sideCollide = false;

    [SerializeField] Transform center;

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
    }

    private void Update()
    {
        grounded = Physics2D.OverlapBox(groundCheck.position, boxCheckSize, 0, realGround);
        sideCollide = Physics2D.OverlapBox(center.position, new Vector2(1.55f, 1f), 0, realGround);

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
        }
        if (dashUnlocked)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton2) && dashTimer <= 0) //X Button on Xbox360
            {
                dash = true;
                dashTimer = .6f;
                dashDuration = .2f;
            }
            if (dash)
            {
                dashDuration -= Time.deltaTime;
                if (dashDuration <= 0)
                {
                    dash = false;
                }
            }
        }
        //----------------------------------------------------------------------------------------
        //---WALL GRAB----------------------------------------------------------------------------
        if (wallGrabUnlocked)
        {
            if (!grounded && !Input.GetKeyDown(KeyCode.Space) && sideCollide)
            {
                wallJump = false;
                wallClimb = true;
            }
            else
            {
                wallClimb = false;
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
                wallJump = true;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                wallJump = false;
            }
        }
        if (wallGrabUnlocked)
        {
            if (!grounded && !Input.GetKeyDown(KeyCode.JoystickButton0) && sideCollide) //A button
            {
                wallJump = false;
                wallClimb = true;
            }
            else
            {
                wallClimb = false;
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
                wallJump = true;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                wallJump = false;
            }
        }
        //----------------------------------------------------------------------------------------
    }
}