using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemyMovement : MonoBehaviour {

    public float speed = 5;
    public float jump = 3;
    float jumpTimer = 0;

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
        if (!ec.dead)
        {
            if (face.hit)
            {
                Flip();
                speed = -speed;
            }
            if (!feet.hit && jumpTimer < 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                RaycastHit2D hit = Physics2D.Raycast(ledgeCheck.position, Vector2.down, .25f, realGround);
                if (hit)
                {
                    transform.position = new Vector3(transform.position.x, hit.transform.position.y + (hit.transform.localScale.y / 2));
                }
            }

            face.hit = false;

            //---------------------------------------------------------------------------------------
            //--Movement-----------------------------------------------------------------------------

            Vector2 processVelocity = transform.InverseTransformDirection(rig.velocity);
            if ((ec.seePlayer && !ec.AlwaysMove) || (ec.AlwaysMove && !ec.attacking))
            {
                if (feet.hit)
                {
                    jumpTimer -= Time.deltaTime;
                    if(jumpTimer <= 0)
                    {
                        jumpTimer = 1f;
                        processVelocity.x = speed;
                        processVelocity.y = jump;
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
