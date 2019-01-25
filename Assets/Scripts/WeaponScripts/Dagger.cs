using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MonoBehaviour {

    public GameObject HitBox;
    public GameObject WorldScriptManager;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            StartCoroutine(SwingDagger());
        }
    }

    public IEnumerator SwingDagger()
    {
        yield return new WaitForSeconds(.1f);
        HitBox.GetComponent<BoxCollider>().enabled = true;
        gameObject.transform.Translate(1f, 0, 0);
        yield return new WaitForSeconds(.1f);
        gameObject.transform.Translate(-1f, 0, 0);
        HitBox.GetComponent<BoxCollider>().enabled = false;
        //StartCoroutine(SwingSpear());
    }
}
