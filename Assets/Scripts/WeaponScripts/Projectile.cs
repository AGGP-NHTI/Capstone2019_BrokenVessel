using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : BrokenVessel.Actor.Actor {

    public float speed = 100;
    public Vector2 movement = Vector2.one;
    public bool shootByX = false;
    float lifeTime = 10f;

	void Start ()
    {
        
        if (shootByX) { GetComponent<Rigidbody2D>().velocity = movement * speed; }
        else { GetComponent<Rigidbody2D>().velocity = transform.forward * speed; }
    }
    private void Update()
    {
        if (paused) { return; }
        lifeTime -= Time.deltaTime;
        if(lifeTime < 0) { Destroy(gameObject); }
        
    }
}
