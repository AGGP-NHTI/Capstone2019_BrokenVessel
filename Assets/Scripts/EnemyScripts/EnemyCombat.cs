﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : BrokenVessel.Actor.Actor {

    public float health = 100;
    public bool dead = false;

    [SerializeField] bool directSight = false;
    public enum DetectionType { none, ignore, ray, circle, cone };
    public DetectionType choice = DetectionType.none;
    public float range = 10f;
    public bool seePlayer = false;

    public float playerDistance = -1f;

    [SerializeField] LayerMask target;


    public bool AlwaysMove = false;

    public bool attacking = false;

    public Transform faceCheck;
    Transform targetTransform;

	[SerializeField]
	private GameObject hitEffect;

    [SerializeField] EnemyContact Contact;

    void Start()
    {
        targetTransform = GameObject.Find("Player").transform;
    }

    void Update ()
    {
        if (paused) { return; }
        switch (choice)
        {
            case DetectionType.none:
                seePlayer = false;
                break;
            case DetectionType.ignore:
                seePlayer = true;
                break;
        }
        if (directSight)
        {
            switch (choice)
            {
                case DetectionType.ray:
                    seePlayer = Physics2D.Raycast(faceCheck.position, transform.right * transform.localScale.x, range, target);
                    Debug.DrawRay(faceCheck.position, Vector2.right * transform.localScale.x * range, Color.red, range);
                    break;
                case DetectionType.circle:
                    seePlayer = Physics2D.CircleCast(transform.position, range, Vector2.zero, 0, target);
                    break;
                case DetectionType.cone:
                    Debug.Log("ERROR: NOT ADDED");
                    break;
            }
        }
        else
        {
            switch (choice)
            {
                case DetectionType.ray:
                    if (Physics2D.Raycast(faceCheck.position, transform.right * transform.localScale.x, range, target))
                    {
                        seePlayer = true;
                    }
                    Debug.DrawRay(faceCheck.position, Vector2.right * transform.localScale.x, Color.red, range);
                    break;
                case DetectionType.circle:
                    if (Physics2D.CircleCast(transform.position, range, Vector2.zero, 0, target))
                    {
                        seePlayer = true;
                    }
                    break;
                case DetectionType.cone:
                    Debug.Log("ERROR: NOT ADDED");
                    break;
            }
        }

        if(seePlayer)
        {
            playerDistance = (transform.position - targetTransform.position).magnitude;
            foreach (LookAt la in GetComponentsInChildren<LookAt>())
            {
                la.FocusObject = targetTransform;
            }
            if (!directSight && playerDistance > range * 1.5f)
            {
                seePlayer = false;
            }
        }
        else
        {
            foreach (LookAt la in GetComponentsInChildren<LookAt>())
            {
                la.FocusObject = null;
            }
            playerDistance = -1f;
        }
    }

    public void takeDamage(float value) //Vector2 knockback)
    {
		if (health > 0)
		{
			health -= value;

			GameObject particle = Instantiate(hitEffect, transform.position, Quaternion.identity);
			Destroy(particle, 2f);

			if (health <= 0)
			{
				StartCoroutine(Die());
			}
		}
    }

    IEnumerator Die()
    {
        if(Contact)
        {
            Destroy(Contact.gameObject);
        }
        dead = true;
        choice = DetectionType.none;
        AlwaysMove = false;
        GetComponent<Rigidbody2D>().gravityScale = 1;
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<EnemyDrops>().DropItem();
        GetComponent<Collider2D>().isTrigger = false;
        //particles
        //animation

        yield return new WaitForSeconds(2);
        if (!GetComponent<ScrapQueen>()){ Destroy(gameObject); }
        else { Destroy(gameObject, 20); }
    }
}
