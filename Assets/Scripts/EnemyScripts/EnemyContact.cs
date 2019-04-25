using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContact : BrokenVessel.Actor.Actor
{
    [SerializeField] int damage = 10;
    [SerializeField] float knockBack = 10;
    [SerializeField] float iFrames = 1;

    private void Start()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (paused) { return; }
        if (collision.gameObject.GetComponent<PlayerData>())
        {
            Debug.Log("internal REEEEEEE");
            Vector2 knock = (collision.gameObject.transform.position - gameObject.transform.position).normalized * knockBack;
            collision.gameObject.GetComponent<PlayerData>().takeDamage(damage, iFrames, knock);

            gameObject.GetComponent<Rigidbody2D>().AddForce(-knock);
        }
    }
}
