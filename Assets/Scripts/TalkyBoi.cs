using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkyBoi : MonoBehaviour {

    public GameObject Cube1;
    public GameObject Cube2;
    public float Distance_;
    public bool textBoxVisible = false;
    public GameObject textBox;
    public GameObject textBoxInstance;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Distance_ = Vector3.Distance(Cube1.transform.position, Cube2.transform.position);
        if(Distance_ < 7)
        {
            Debug.Log("ALERT ENEMY");
            if(textBoxVisible == false)
            {
                textBoxInstance = Instantiate(textBox);
                textBoxVisible = true;
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
