using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed = 5;
    public Vector2 movement = Vector2.one;
    public bool shootByX = false;
    

	void Start ()
    {
        Destroy(gameObject, 10f);
        if (shootByX) { GetComponent<Rigidbody2D>().velocity = movement * speed; }
        else { GetComponent<Rigidbody2D>().velocity = transform.forward * speed; }
    }
}
