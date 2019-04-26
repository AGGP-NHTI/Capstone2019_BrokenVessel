﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    Vector3 mouse;
    bool attacking = false;
    public float shootCooldown = 0.1f;
    public float r1;
    public float r2;
    [SerializeField] Transform Body;
    [SerializeField] Transform barrelEnd;
    [SerializeField] GameObject PlayerBullet;
    [SerializeField] float projectileSpeed;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //mouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)) - transform.position;
        //transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mouse.y, mouse.x) * Mathf.Rad2Deg);
        Rotation();
        transform.localScale = transform.parent.localScale * 1f;

        if (transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z < 270)
        {
            Debug.Log("Flip");
            Body.localPosition = new Vector3(0.219f, 1.268f, 0);
            Body.localRotation = Quaternion.Euler(0, -90, 180);
        }
        else
        {
            Body.localPosition = new Vector3(0.219f, -1.268f, 0);
            Body.localRotation = Quaternion.Euler(0, -90, 0);
        }
    }

    public override void Attack()
    {
        if (!attacking)
        {
            StartCoroutine(UseGun());
            base.Attack();
        }
    }

    public IEnumerator UseGun()
    {
        attacking = true;
        yield return new WaitForSeconds(.1f);
        GameObject temp = Instantiate(PlayerBullet, barrelEnd.position, barrelEnd.rotation) as GameObject;
        Projectile refer = temp.GetComponent<Projectile>();
        refer.speed = projectileSpeed;
        Debug.Log("Gun Move");
        yield return new WaitForSeconds(shootCooldown);
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
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, (Mathf.Atan2(r1, r2) - 90) * 180 / Mathf.PI)); // this does the actual rotaion according to inputs
        }
    }
}
