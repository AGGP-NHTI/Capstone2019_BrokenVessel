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
        transform.localScale = transform.parent.localScale;
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
        if (r1 == 0f && r2 == 0f)
        { // this statement allows it to recenter once the inputs are at zero 
            Vector3 curRot = this.transform.localEulerAngles; // the object you are rotating
            Vector3 homeRot;
            if (curRot.y > 180f)
            { // this section determines the direction it returns home 
                Debug.Log(curRot.y);
                homeRot = new Vector3(0f, 90f, 0f); //it doesnt return to perfect zero 
            }
            else
            {                                                                      // otherwise it rotates wrong direction 
                homeRot = Vector3.zero;
            }
           transform.rotation = Quaternion.Euler(Vector3.Slerp(curRot, homeRot, Time.deltaTime * 2));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, (Mathf.Atan2(r1, r2)-90) * 180 / Mathf.PI)); // this does the actual rotaion according to inputs
        }
    }
}