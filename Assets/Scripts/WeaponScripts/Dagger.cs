using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : Weapon
{

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    public override void Update () {
        base.Update();
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
        HitBox.GetComponent<BoxCollider>().enabled = true;
        gameObject.transform.Translate(1f, 0, 0);
        yield return new WaitForSeconds(.1f);
        gameObject.transform.Translate(-1f, 0, 0);
        HitBox.GetComponent<BoxCollider>().enabled = false;
        //StartCoroutine(SwingSpear());
    }
}
