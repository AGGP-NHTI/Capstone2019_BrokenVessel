using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour {

    public int OwnerLayer;
    public bool isProjectile = true;
    public int damage = 10;

    private void Start()
    {
        gameObject.layer = 0;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != OwnerLayer)
        {
            if (collision.gameObject.GetComponent<PlayerData>())
            {
                collision.gameObject.GetComponent<PlayerData>().takeDamage(damage, 5, Vector2.zero);
            }
            if (collision.gameObject.GetComponent<EnemyCombat>())
            {
                collision.gameObject.GetComponent<EnemyCombat>().takeDamage(damage);
            }
            if(collision.gameObject.layer == 12 || collision.gameObject.layer == 4)//skip is water or weapon
            {
                return;
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
