using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemyMovement : BrokenVessel.Actor.Actor
{

    public float speed = 5;
    public float jump = 3;
    float jumpTimer = 0;
    [SerializeField] bool jumpIsAttack = false;

    [SerializeField] Transform faceCheck;
    [SerializeField] Transform ledgeCheck;
    [SerializeField] LayerMask realGround;
    public LayerMask target;

    Rigidbody2D rig;
    EnemyCombat ec;
    FaceCheck face;
    FaceCheck feet;

    void Start()
    {
        ec = GetComponent<EnemyCombat>();
        rig = GetComponent<Rigidbody2D>();
        face = faceCheck.gameObject.GetComponent<FaceCheck>();
        feet = ledgeCheck.gameObject.GetComponent<FaceCheck>();
    }

    void Update()
    {
        if (paused) { return; }
        if (!ec.dead)
        {
            if (face.hit)
            {
                Flip();
                speed = -speed;
            }
            face.hit = false;

            //---------------------------------------------------------------------------------------
            //--Movement-----------------------------------------------------------------------------

            Vector2 processVelocity = transform.InverseTransformDirection(rig.velocity);

            
            if (feet.hit)
            {
                processVelocity.x = speed / 2;
                jumpTimer -= Time.deltaTime;
                if ((jumpTimer <= 0 && !jumpIsAttack) || (jumpTimer <= 0 && jumpIsAttack && ec.seePlayer))
                {
                    jumpTimer = .5f;
                    processVelocity.x = speed * 2;
                    processVelocity.y = jump;
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
