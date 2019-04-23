using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Weapon {

    Vector3 mouse;
    bool attacking = false;
    public float stabCooldown = 0.2f;


    void Update ()
    {
        mouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)) - transform.position;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mouse.y, mouse.x) * Mathf.Rad2Deg);
        transform.localScale = transform.parent.localScale;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(Input.GetAxis("RightHorizontal"), Input.GetAxis("RightVertical")));
    }

    public override void Attack()
    {
        if (!attacking)
        {
            StartCoroutine(PushSpear());
            base.Attack();
        }
    }

    public IEnumerator PushSpear()
    {
        attacking = true;
        yield return new WaitForSeconds(.1f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.transform.Translate(transform.parent.localScale.x, 0, 0);
        yield return new WaitForSeconds(.1f);
        gameObject.transform.localPosition = new Vector3(0,.5f,0);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(stabCooldown);
        attacking = false;
    }
}