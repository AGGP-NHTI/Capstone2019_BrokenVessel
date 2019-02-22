using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyMovement : MonoBehaviour {

    [SerializeField] bool approachPlayer;

    EnemyCombat ec;
    Rigidbody2D rig;

    void Start ()
    {
        ec = GetComponent<EnemyCombat>();
        rig = GetComponent<Rigidbody2D>();
        rig.gravityScale = 0;
    }
	
	void Update ()
    {
		if(!ec.dead)
        {

        }
	}
}
