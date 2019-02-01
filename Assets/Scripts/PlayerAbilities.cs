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


    public bool wallclimb = false;
    public bool wallJump = false;
    bool sideCollide = false;
    public int wallside = 0;

    Rigidbody2D rig;
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

        rig = GetComponent<Rigidbody2D>();

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
        //----------------------------------------------------------------------------------------
        //---WALL GRAB----------------------------------------------------------------------------
        if (wallGrabUnlocked)
        {
            if (!grounded && !Input.GetKey(KeyCode.Space))
            {
                if (sideCollide && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
                {
                    wallclimb = true;
                    wallside = (int)transform.localScale.x;
                }
            }


            if (Input.GetKeyDown(KeyCode.Space) && sideCollide && !grounded)
            {
                wallJump = true;
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                wallJump = false;
            }

            wallclimb = false;
        }
        //----------------------------------------------------------------------------------------
    }
}
