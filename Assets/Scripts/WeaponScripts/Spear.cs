using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Weapon {

    Vector3 mouse;


    void Update ()
    {
        mouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)) - transform.position;
        transform.rotation = Quaternion.Euler(Mathf.Atan2(mouse.y, mouse.x) * Mathf.Rad2Deg, -90, 0);
    }

    public override void Attack()
    {
        StartCoroutine(PushSpear());
        base.Attack();
    }

    public IEnumerator PushSpear()
    {
        yield return new WaitForSeconds(.1f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.transform.Translate(0, 0, -1f);
        yield return new WaitForSeconds(.1f);
        gameObject.transform.localPosition = new Vector3(0,1,0);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}