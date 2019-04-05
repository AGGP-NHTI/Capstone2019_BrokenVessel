using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossFightStart : MonoBehaviour
{
    public ScrapQueen boss;

    void OnTriggerStay2D(Collider2D collide)
    {
        if (collide.gameObject.layer == 9)
        {
            boss.start = true;
        }
    }
}

