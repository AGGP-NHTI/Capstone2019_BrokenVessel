using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed = 5;

	void Start ()
    {
        Destroy(gameObject, 10f);
        GetComponent<Rigidbody2D>().velocity = transform.forward * speed;
	}
}
