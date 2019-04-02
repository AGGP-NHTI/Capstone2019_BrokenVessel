using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Weapon
{

    // Use this for initialization
    void Start () {
        
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
        HitBox.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.transform.Translate(0, 0, -2f);
        yield return new WaitForSeconds(.1f);
        gameObject.transform.Translate(0, 0, 2f);
        HitBox.GetComponent<BoxCollider2D>().enabled = false;
    }
}
