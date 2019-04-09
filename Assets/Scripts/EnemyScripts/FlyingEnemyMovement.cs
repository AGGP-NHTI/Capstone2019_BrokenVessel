using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BrokenVessel.Actor;
public class FlyingEnemyMovement : BrokenVessel.Actor.Actor
{

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
	
	void Update()
    {
        if (paused) { GetComponent<Rigidbody2D>().simulated = false; return; }
        else { GetComponent<Rigidbody2D>().simulated = false; }

        if (!ec.dead)
        {
            if(ec.seePlayer)
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
                foreach(LookAt la in GetComponentsInChildren<LookAt>())
                {
                    la.FocusObject = target;
                }
                process = target.position;
                offSet.y += 1.15f; //adding so target is center of player
                offSet = (offSet - process).normalized * orbitField;
                offSet.y = Mathf.Abs(offSet.y) + 2f;
            }
            else
            {
                target = transform;
                foreach (LookAt la in GetComponentsInChildren<LookAt>())
                {
                    la.FocusObject = null;
                }
                process = transform.position;
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
                    processVelocity = Vector2.zero;
                }
            }
            rig.velocity = transform.TransformDirection(processVelocity);
            if (ec.playerDistance == -1)
            {
                rig.velocity = Vector3.zero;
            }
        }

	}
}
