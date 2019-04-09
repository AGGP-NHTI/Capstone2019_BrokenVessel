using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbEnemyMovement : BrokenVessel.Actor.Actor
{


    [SerializeField] float speed = 5;
    [SerializeField] bool bounceOff = false;

    bool onLedge = false;
    bool grounded = true;

    Transform faceCheck;
    [SerializeField] Transform ledgeCheck;
    [SerializeField] LayerMask realGround;
    public LayerMask target;

    Rigidbody2D rig;
    EnemyCombat ec;
    FaceCheck fc;

    void Start ()
    {
        ec = GetComponent<EnemyCombat>();
        rig = GetComponent<Rigidbody2D>();
        fc = GetComponentInChildren<FaceCheck>();
        faceCheck = fc.gameObject.transform;
    }
	
	void Update ()
    {
        if (paused) { return; }
        if (!ec.dead)
        {
            grounded = Physics2D.CircleCast(ledgeCheck.position, .2f, Vector2.zero, 0, realGround);
            onLedge = !Physics2D.CircleCast(ledgeCheck.position, .075f, Vector2.zero, 0, realGround);

            if (grounded)//if onground
            {
                rig.gravityScale = 0;
                rig.freezeRotation = true;
            }
            else //if falling
            {
                rig.gravityScale = 1;
                rig.freezeRotation = false;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                RaycastHit2D hit = Physics2D.Raycast(ledgeCheck.position, Vector2.down, .25f, realGround);
                if (hit)
                {
                    transform.position = new Vector3(transform.position.x, hit.transform.position.y + (hit.transform.localScale.y / 2));
                }
            }

            if (fc.hit)//walked into something
            {
                if (bounceOff)//turn around
                {
                    Flip();
                    speed = -speed;
                }
                else //grab wall walked into
                {
                    rig.isKinematic = true;
                    transform.position = faceCheck.position;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z + (90 * transform.localScale.x)));
                    rig.isKinematic = false;
                }
            }
            fc.hit = false;

            //--Movement-----------------------------------------------------------------------------

            Vector2 processVelocity = transform.InverseTransformDirection(rig.velocity);
            if ((ec.seePlayer && !ec.AlwaysMove) || (ec.AlwaysMove && !ec.attacking))
            {

                if (!onLedge)
                {
                    processVelocity.x = speed;
                }
                else if (grounded)
                {
                    transform.Rotate(new Vector3(0, 0, -speed * transform.localScale.x));
                    processVelocity = Vector2.zero;
                }

            }
            rig.velocity = transform.TransformDirection(processVelocity);

        }
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
