using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour {

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


    float timer = 0;

    public Transform faceCheck;
    public GameObject projectile;
	
	void Update ()
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
                    if(Physics2D.Raycast(faceCheck.position, transform.right * transform.localScale.x, range, target))
                    {
                        seePlayer = true;
                    }
                    Debug.DrawRay(faceCheck.position, Vector2.right * transform.localScale.x, Color.red, range);
                    break;
                case DetectionType.circle:
                    if(Physics2D.CircleCast(transform.position, range, Vector2.zero, 0, target))
                    {
                        seePlayer = true;
                    }
                    break;
                case DetectionType.box:
                    Debug.Log("ERROR: NOT ADDED");
                    break;
            }
        }


        timer -= Time.deltaTime;
        if (seePlayer && timer <= 0)
        {

            if (meleeEnemy)
            {
                attacking = true;
                Debug.Log("attack");
                timer = 1f;
                StartCoroutine("meleeAttack");
            }
            if (rangeEnemy)
            {
                attacking = true;
                Debug.Log("pew");
                timer = .5f;
                StartCoroutine("rangeAttack");
            }
        }

    }

    IEnumerable meleeAttack()
    {
        yield return 1;
        attacking = false;
    }

    IEnumerable rangeAttack()
    {
        yield return 1;
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
