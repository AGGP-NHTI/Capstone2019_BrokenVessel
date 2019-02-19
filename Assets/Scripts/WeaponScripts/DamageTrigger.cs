using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour {

    [SerializeField] int OwnerLayer;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit Trigger");
        if (collision.gameObject.layer != OwnerLayer)
        {
            if (collision.gameObject.GetComponent<PlayerData>())
            {
                collision.gameObject.GetComponent<PlayerData>().takeDamage(10, 5);
            }
            if (transform.parent)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }

}
