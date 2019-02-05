using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour {

    public bool contactEnemy = false;
    public bool rangeEnemy = false;
    public bool meleeEnemy = false;

    public enum DetectionType { none, ray, circle, box };
    public DetectionType choice = DetectionType.none;
    public float range = 10f;
    bool seePlayer = false;
    public LayerMask target;

    public Transform faceCheck;

    Rigidbody2D rig;

    // Use this for initialization
    void Start ()
    {
        rig = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        switch (choice)
        {
            case DetectionType.none:
                seePlayer = true;
                break;
            case DetectionType.ray:
                seePlayer = Physics2D.Raycast(faceCheck.position, Vector2.right * transform.localScale.x, range, target);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(contactEnemy && collision.gameObject.layer == target)
        {
            collision.gameObject.GetComponent<PlayerData>().takeDamage(10, 5);
        }

    }
}
