﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon {

    public GameObject Pivot;
    public GameObject Player;

    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    public void Update () {
        if (HitBox == null)
        {
            HitBox = GameObject.Find("SwordHitBox");
        }
        if (Pivot == null)
        {
            Pivot = GameObject.Find("Pivot");
        }
        if (Player == null)
        {
            Player = GameObject.Find("Player");
        }
    }

    public override void Attack()
    {
        StartCoroutine(SwingSword());
        base.Attack();
    }

    public IEnumerator SwingSword()
    {
        if (Player.GetComponent<PlayerMovement>().facingRight)
        {
            yield return new WaitForSeconds(.1f);
            HitBox.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.transform.RotateAround(Pivot.transform.position, Vector3.forward * -1, 90);
            yield return new WaitForSeconds(.1f);
            gameObject.transform.RotateAround(Pivot.transform.position, Vector3.forward * -1, -90);
            HitBox.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            yield return new WaitForSeconds(.1f);
            HitBox.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.transform.RotateAround(Pivot.transform.position, Vector3.forward, 90);
            yield return new WaitForSeconds(.1f);
            gameObject.transform.RotateAround(Pivot.transform.position, Vector3.forward, -90);
            HitBox.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
