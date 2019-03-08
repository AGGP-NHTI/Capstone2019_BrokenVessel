using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContact : MonoBehaviour
{
    [SerializeField] float damage = 10;
    [SerializeField] float iFrames = 1;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerData>())
        {
            collision.gameObject.GetComponent<PlayerData>().takeDamage(damage, iFrames);
        }
    }
}
