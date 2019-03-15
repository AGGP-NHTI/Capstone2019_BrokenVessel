using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkyBoi : MonoBehaviour {

    //learn how to draw your Ink document story into 3dTextBubbles

    public GameObject Cube1;
    public GameObject Cube2;
    public float Distance_;
    public bool textBoxVisible = false;
    public GameObject textBox;
    public GameObject textBoxInstance;
    public GameObject WorldScriptManager;
    public GameObject canvas;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Distance_ = Vector3.Distance(Cube1.transform.position, Cube2.transform.position);
        if(Distance_ < 7)
        {
            if (textBoxVisible == false)
            {
                textBoxInstance = Instantiate(textBox);
                textBoxInstance.transform.parent = GameObject.Find("Cube").transform;
                textBoxVisible = true;
            }
            if (WorldScriptManager.GetComponent<KeysPressed>().jHeld == true)
            {
                canvas.SetActive(true);
            }
        }
        else
        {
            if (textBoxVisible == true)
            {
                Destroy(textBoxInstance);
                textBoxVisible = false;
            }
        }
	}
}
