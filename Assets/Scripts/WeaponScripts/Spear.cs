using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour {

    public GameObject HitBox;
    public GameObject WorldScriptManager;

    // Use this for initialization
    void Start () {
        StartCoroutine(SwingSpear());
    }
	
	// Update is called once per frame
	void Update () {
		if(WorldScriptManager.GetComponent<KeysPressed>().wHeld)
        {
            SwingSpear();
        }
	}

    public IEnumerator SwingSpear()
    {
        yield return new WaitForSeconds(.1f);
        HitBox.GetComponent<BoxCollider>().enabled = true;
        gameObject.transform.Translate(1, 0, 0);
        yield return new WaitForSeconds(.1f);
        gameObject.transform.Translate(-1, 0, 0);
        HitBox.GetComponent<BoxCollider>().enabled = false;
        //StartCoroutine(SwingSpear());
    }
}
