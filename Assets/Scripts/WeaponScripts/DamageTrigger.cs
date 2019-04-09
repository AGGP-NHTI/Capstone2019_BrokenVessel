using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour {

    public int OwnerLayer;
    public bool isProjectile = true;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != OwnerLayer)
        {
            if (collision.gameObject.GetComponent<PlayerData>())
            {
                collision.gameObject.GetComponent<PlayerData>().takeDamage(10, 5, Vector2.zero);
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
