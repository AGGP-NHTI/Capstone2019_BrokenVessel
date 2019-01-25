using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Weapon
{

    // Use this for initialization
    void Start () {
        //StartCoroutine(SwingSpear());
    }

    // Update is called once per frame
    public override void Update() {
        base.Update();
        if (HitBox == null)
        {
            HitBox = GameObject.Find("SpearHitBox");
        }
    }

    public override void Attack()
    {
        StartCoroutine(SwingSpear());
        base.Attack();
    }

    public IEnumerator SwingSpear()
    {
        yield return new WaitForSeconds(.1f);
        HitBox.GetComponent<BoxCollider>().enabled = true;
        gameObject.transform.Translate(2f, 0, 0);
        yield return new WaitForSeconds(.1f);
        gameObject.transform.Translate(-2f, 0, 0);
        HitBox.GetComponent<BoxCollider>().enabled = false;
        //StartCoroutine(SwingSpear());
    }
}
