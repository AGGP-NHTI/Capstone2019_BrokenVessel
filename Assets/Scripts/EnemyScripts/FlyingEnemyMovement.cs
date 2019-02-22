using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyMovement : MonoBehaviour {

    [SerializeField] float speed = 2;
    [SerializeField] float orbitField = 3;
    [SerializeField] bool approachPlayer;
    [SerializeField] float approachDistance = 2;

    Transform target;
    Vector3 process;
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
                process = target.position;
                offSet.y += 1.15f; //adding so target is center of player
                offSet = (offSet - process).normalized * orbitField;
                offSet.y = Mathf.Abs(offSet.y) + 2f;
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
                process += offSet;
                processVelocity = (transform.position - process).normalized * -speed;
                if (Vector3.Distance(process, transform.position) < .1f)
                {
                    processVelocity = Vector3.zero;
                }
            }
            rig.velocity = transform.TransformDirection(processVelocity);
        }

	}
}
