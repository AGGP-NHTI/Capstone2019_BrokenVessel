using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour {

<<<<<<< HEAD
    public float health = 100;
    public bool dead = false;

    public enum DetectionType { none, ignore, ray, circle, box };
    public DetectionType choice = DetectionType.none;
    public float range = 10f;
    public bool seePlayer = false;

=======
>>>>>>> 93491491453444499017bf9beb832beae984d059
    public LayerMask target;

    public bool contactEnemy = false;
    public bool rangeEnemy = false;
    public bool meleeEnemy = false;

    public bool forgetPlayer = false;
    public bool AlwaysMove = false;

    public bool attacking = false;

    public enum DetectionType { none, ignore, ray, circle, box };
    public DetectionType choice = DetectionType.none;
    public float range = 10f;
    public bool seePlayer = false;

    [SerializeField] GameObject weapon;

    float timer = 1f;

<<<<<<< HEAD



    void Update()
=======
    public Transform faceCheck;
    public GameObject projectile;
	
	void Update ()
>>>>>>> 93491491453444499017bf9beb832beae984d059
    {
        switch (choice)
        {
            case DetectionType.none:
                seePlayer = false;
                break;
            case DetectionType.ignore:
                seePlayer = true;
                break;
        }
        if (forgetPlayer)
        {
            switch (choice)
            {
                case DetectionType.ray:
                    seePlayer = Physics2D.Raycast(faceCheck.position, transform.right * transform.localScale.x, range, target);
                    Debug.DrawRay(faceCheck.position, Vector2.right * transform.localScale.x, Color.red, range);
                    break;
                case DetectionType.circle:
                    seePlayer = Physics2D.CircleCast(transform.position, range, Vector2.zero, 0, target);
                    break;
                case DetectionType.box:
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
                case DetectionType.box:
                    Debug.Log("ERROR: NOT ADDED");
                    break;
            }
        }

        if (!attacking && (meleeEnemy || rangeEnemy))
        {
            timer -= Time.deltaTime;
        }
        if (seePlayer && timer <= 0)
        {

            if (meleeEnemy)
            {
                attacking = true;

                timer = 1f;
                StartCoroutine(meleeAttack());

            }
            if (rangeEnemy)
            {
                attacking = true;
                Debug.Log("pew");
                timer = .5f;
                StartCoroutine(rangeAttack());
            }
        }

    }

    public void takeDamage(float value) //Vector2 knockback)
    {
        health -= value;
        if(health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        dead = true;
        choice = DetectionType.none;
        AlwaysMove = false;
        GetComponent<Rigidbody2D>().gravityScale = 1;

        //particles
        //animation
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }



    IEnumerator meleeAttack()
    {
        weapon.transform.rotation = Quaternion.Euler(0, 0, -45);
        yield return new WaitForSeconds(2);
        weapon.transform.rotation = Quaternion.Euler(0, 0, -90);
        Debug.Log("melee attack");
        //damage trigger = true
        yield return new WaitForSeconds(2);
        weapon.transform.rotation = Quaternion.identity;
        attacking = false;
    }

    IEnumerator rangeAttack()
    {
        yield return new WaitForSeconds(.5f);
        //pew
        yield return new WaitForSeconds(.5f);
        attacking = false;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (contactEnemy)
        {
            if (collision.gameObject.layer == 9)
            {
                collision.gameObject.GetComponent<PlayerData>().takeDamage(10, 5);
            }
        }

    }
}
