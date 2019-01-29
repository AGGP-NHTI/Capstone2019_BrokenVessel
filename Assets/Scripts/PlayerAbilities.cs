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

    bool dashing = false;
    int dashFrames = 0;
    float dashTimer = 0f;

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
            if ((Input.GetKeyDown(KeyCode.Space) && grounded) || wallclimb)
            {
                dashTimer = 0f;
            }
            if (Input.GetKeyDown(KeyCode.Q) && !dashing && dashTimer <= 0)
            {
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
                Debug.Log("dashing");
                dashFrames--;
                rig.velocity = Vector2.right * transform.localScale.x * 25;
                if (dashFrames == 0)
                {
                    dashing = false;
                }
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
                        dashTimer = 0f;
                        wallside = 1;
                    }
                    if (transform.localScale.x < 0)
                    {
                        wallclimb = true;
                        dashTimer = 0f;
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
