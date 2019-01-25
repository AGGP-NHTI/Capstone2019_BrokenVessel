﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

    public GameObject Pivot;
    public GameObject HitBox;
    public GameObject WorldScriptManager;

    // Use this for initialization
    void Start () {
        //StartCoroutine(SwingSword());
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("q"))
        {
            StartCoroutine(SwingSword());
        }
    }

    public IEnumerator SwingSword()
    {
        yield return new WaitForSeconds(.1f);
        HitBox.GetComponent<MeshCollider>().enabled = true;
        gameObject.transform.RotateAround(Pivot.transform.position, Vector3.forward * -1, 90);
        yield return new WaitForSeconds(.1f);
        gameObject.transform.RotateAround(Pivot.transform.position, Vector3.forward * -1, -90);
        HitBox.GetComponent<MeshCollider>().enabled = false;
    }
}
