using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrokenVessel.Actor;
public class EnemyMovement : BrokenVessel.Actor.Actor
{

    public float speed = 5;

    public bool bounceOffLedge = false;
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

    void Update()
    {
        if (paused) { return; }

        if (!ec.dead)
        {
            grounded = Physics2D.CircleCast(ledgeCheck.position, .2f, Vector2.zero, 0, realGround);
            onLedge = !Physics2D.CircleCast(ledgeCheck.position, .075f, Vector2.zero, 0, realGround);

            if (fc.hit || (onLedge && bounceOffLedge))
            {
                Flip();
                speed = -speed;
            }

            fc.hit = false;

            //---------------------------------------------------------------------------------------
            //--Movement-----------------------------------------------------------------------------

            Vector2 processVelocity = transform.InverseTransformDirection(rig.velocity);
            if ((ec.seePlayer && !ec.AlwaysMove) || (ec.AlwaysMove && !ec.attacking))
            {
                if (grounded)
                {
                    processVelocity.x = speed;
                }
                else
                {
                    if (!bounceOffLedge)
                    {
                        processVelocity.x = speed / 2;
                    }
                    processVelocity.y -= 4.5f;
                    if (processVelocity.y < -18f)
                    {
                        processVelocity.y = -18f;
                    }
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
