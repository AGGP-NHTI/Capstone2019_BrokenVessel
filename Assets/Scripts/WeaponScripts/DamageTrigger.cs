using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour {

    [SerializeField] int OwnerLayer;
    public bool isProjectile = true;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit Trigger");
        if (collision.gameObject.layer != OwnerLayer)
        {
            if (collision.gameObject.GetComponent<PlayerData>())
            {
                collision.gameObject.GetComponent<PlayerData>().takeDamage(10, 5);
            }
            if (collision.gameObject.GetComponent<EnemyCombat>())
            {
                collision.gameObject.GetComponent<EnemyCombat>().takeDamage(10);
            }

            if (isProjectile)
            {
                if (transform.parent)
                {
                    Destroy(transform.parent.gameObject);
                }
                Destroy(gameObject);
            }
        }
    }

}
