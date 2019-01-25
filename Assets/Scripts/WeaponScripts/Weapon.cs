using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public bool equipped = false;
    public GameObject HitBox;
    public GameObject WorldScriptManager;

    public virtual void Update()
    {
        if (WorldScriptManager == null)
        {
            WorldScriptManager = GameObject.Find("WorldScriptManager");
        }
        if (equipped)
        {
            if (Input.GetKeyDown("q"))
            {
                Attack();
            }
        }
    }

    public virtual void Attack()
    {

    }
}
