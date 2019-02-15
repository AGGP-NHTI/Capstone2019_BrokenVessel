using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] int OwnerLayer;
    public float speed = 5;

	void Start ()
    {
        Destroy(gameObject, 5f);
        GetComponent<Rigidbody2D>().velocity = transform.forward * speed;
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit Trigger");
        if (collision.gameObject.layer != OwnerLayer)
        {
            if (collision.gameObject.GetComponent<PlayerData>())
            {
                collision.gameObject.GetComponent<PlayerData>().takeDamage(10, 5);
            }
            Destroy(gameObject);
        }
    }
}
