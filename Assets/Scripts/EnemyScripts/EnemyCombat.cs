using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour {

    public LayerMask target;

    public bool contactEnemy = false;
    public bool rangeEnemy = false;
    public bool meleeEnemy = false;

    public enum DetectionType { none, ray, circle, box };
    public DetectionType choice = DetectionType.none;
    public float range = 10f;
    public bool seePlayer = false;


    float timer = 0;

    public Transform faceCheck;

    //Rigidbody2D rig;

    void Start ()
    {
        //rig = GetComponent<Rigidbody2D>();
    }
	
	void Update ()
    {
        switch (choice)
        {
            case DetectionType.none:
                seePlayer = true;
                break;
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

        timer -= Time.deltaTime;
        if (seePlayer && timer <= 0)
        {

            if (meleeEnemy)
            {
                Debug.Log("attack");
                timer = 1f;
                StartCoroutine("meleeAttack");
            }
            if (rangeEnemy)
            {
                Debug.Log("pew");
                timer = .5f;
                StartCoroutine("rangeAttack");
            }
        }

    }

    IEnumerable meleeAttack()
    {
        yield return 1;
    }

    IEnumerable rangeAttack()
    {
        yield return 1;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.layer);
        if (contactEnemy)
        {
            if (collision.gameObject.layer == 9)
            {
                collision.gameObject.GetComponent<PlayerData>().takeDamage(10, 5);
            }
        }

    }
}
