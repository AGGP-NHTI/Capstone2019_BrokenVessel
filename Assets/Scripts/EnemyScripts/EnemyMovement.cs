using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float speed = 5;

    public bool followLedge = false;
    public bool bounceOff = false;
    public bool moveOnJump = false;

    public bool hitHead = false;
    bool onLedge = false;
    bool grounded = true;
    bool isJumping = false;
    int timer = 0;

    public Transform faceCheck;
    public Transform ledgeCheck;
    public LayerMask realGround;
    public LayerMask target;

    Rigidbody2D rig;

    EnemyCombat ec;

    void Start ()
    {
        ec = GetComponent<EnemyCombat>();
        rig = GetComponent<Rigidbody2D>();
	}

    void Update()
    {
        grounded = Physics2D.CircleCast(ledgeCheck.position, .2f, Vector2.zero, 0, realGround);
        onLedge = !Physics2D.CircleCast(ledgeCheck.position, .075f, Vector2.zero, 0, realGround);

        if (followLedge && grounded)
        {
            rig.gravityScale = 0;
            rig.freezeRotation = true;
        }
        else
        {
            rig.gravityScale = 1;
            rig.freezeRotation = false;
        }
        if (bounceOff && (hitHead || onLedge) && !moveOnJump)
        {
            Flip();
            speed = -speed;
        }
        if (bounceOff && moveOnJump && hitHead)
        {
            Flip();
            speed = -speed;
        }
        if (!grounded && !moveOnJump)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            RaycastHit2D hit = Physics2D.Raycast(ledgeCheck.position, Vector2.down, .25f, realGround);
            if (hit)
            {
                transform.position = new Vector3(transform.position.x, hit.transform.position.y + (hit.transform.localScale.y / 2));
            }
        }
        else if (followLedge && hitHead)
        {
            rig.isKinematic = true;
            transform.position = faceCheck.position;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z + (90 * transform.localScale.x)));
            rig.isKinematic = false;
        }
        hitHead = false;

        //---------------------------------------------------------------------------------------
        //--Movement-----------------------------------------------------------------------------

        Vector2 processVelocity = transform.InverseTransformDirection(rig.velocity);
        if (ec.seePlayer || (ec.AlwaysMove && !ec.attacking))
        {
            if (moveOnJump)
            {
                if (!onLedge)
                {
                    isJumping = true;
                }
                if (isJumping && timer >= 80)
                {
                    processVelocity.y = Mathf.Abs(speed) * 2;
                    processVelocity.x = speed;
                }
                if (timer >= 90)
                {
                    processVelocity.y = -speed;
                    timer = 0;
                    isJumping = false;
                }
                timer++;
            }
            else
            {
                if (!onLedge || !followLedge)
                {
                    processVelocity.x = speed;
                }
                else if (grounded)
                {
                    transform.Rotate(new Vector3(0, 0, -15 * transform.localScale.x));
                    processVelocity = Vector2.zero;
                }
            }
        }
        rig.velocity = transform.TransformDirection(processVelocity);
    }


    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
