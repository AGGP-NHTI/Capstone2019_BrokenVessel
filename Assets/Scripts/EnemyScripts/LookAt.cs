using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : BrokenVessel.Actor.Actor
{

    public Transform FocusObject;
    [SerializeField] Vector3 offSet;

    void Update()
    {
        if (paused) { return; }
        if (FocusObject)
        {
            transform.rotation = Quaternion.LookRotation(-transform.position + (FocusObject.position + offSet), Vector3.up);
        }
    }
}
