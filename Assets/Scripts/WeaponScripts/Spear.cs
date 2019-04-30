using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Weapon
{

    Vector3 mouse;
    bool attacking = false;
    public float stabCooldown = 0.2f;
    public float r1;
    public float r2;

    void Update()
    {
        mouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)) - transform.position;
        //transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mouse.y, mouse.x) * Mathf.Rad2Deg);
        transform.localScale = transform.parent.localScale * 7f;
        Rotation();
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
        gameObject.transform.localPosition = new Vector3(0, .5f, 0);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(stabCooldown);
        attacking = false;
    }
    void Rotation()
    {
        r1 = Input.GetAxis("RightHorizontal");
        r2 = Input.GetAxis("RightVertical");
        Vector2 aim = new Vector2(r1, r2);
        if (aim.magnitude > 0.2f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, (Mathf.Atan2(r1, r2) - 90) * 180 / Mathf.PI)); // this does the actual rotaion according to inputs
        }
    }
}