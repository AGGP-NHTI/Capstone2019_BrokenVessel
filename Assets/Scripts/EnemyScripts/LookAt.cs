using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {

    public Transform FocusObject;
    [SerializeField] Vector3 offSet;

    void Update()
    {
        if (FocusObject)
        {
            transform.rotation = Quaternion.LookRotation(-transform.position - (FocusObject.position - offSet), Vector3.up);
        }
    }
}
