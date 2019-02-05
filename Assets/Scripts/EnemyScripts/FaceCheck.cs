using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCheck : MonoBehaviour
{
    EnemyMovement me;
	void Start ()
    {
        me = GetComponentInParent<EnemyMovement>();
	}
	
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 0)
        {
            me.hitHead = true;
        }
    }
}
