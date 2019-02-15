using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public GameObject HitBox;
    public GameObject WorldScriptManager;

    public virtual void Update()
    {
        if (WorldScriptManager == null)
        {
            WorldScriptManager = GameObject.Find("WorldScriptManager");
        }
    }

    public virtual void Attack()
    {

    }
}
