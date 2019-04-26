using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : Weapon
{

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    public void Update () {
        if (HitBox == null)
        {
            HitBox = GameObject.Find("DaggerHitBox");
        }
    }

    public override void Attack()
    {
        StartCoroutine(SwingDagger());
        base.Attack();
    }

    public IEnumerator SwingDagger()
    {
        yield return new WaitForSeconds(.1f);
        HitBox.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.transform.Translate(0, 0, 1f);
        yield return new WaitForSeconds(.1f);
        gameObject.transform.Translate(0, 0, -1f);
        HitBox.GetComponent<BoxCollider2D>().enabled = false;
    }
}
