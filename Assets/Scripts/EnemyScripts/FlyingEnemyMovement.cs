using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyMovement : MonoBehaviour {

    [SerializeField] bool approachPlayer;
    [SerializeField] float approachDistance = 2;

    Transform target;
    Vector3 offSet;

    EnemyCombat ec;
    Rigidbody2D rig;

    void Start ()
    {
        target = transform;
        offSet = Vector3.zero;
        ec = GetComponent<EnemyCombat>();
        rig = GetComponent<Rigidbody2D>();
        rig.gravityScale = 0;
    }
	
	void Update ()
    {
		if(!ec.dead)
        {
            if(ec.seePlayer)
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
                offSet.y += 1.15f; //adding so target is center of player
            }
            else
            {
                target = transform;
                offSet = Vector3.zero;
            }

            //--Movement-----------------------------------------------------------------------------
            Vector2 processVelocity = transform.InverseTransformDirection(rig.velocity);
            if ((ec.seePlayer && !ec.AlwaysMove) || (ec.AlwaysMove && !ec.attacking))
            {

            }
            rig.velocity = transform.TransformDirection(processVelocity);
        }

	}
}
