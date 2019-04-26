using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkyBoi : MonoBehaviour {

    //learn how to draw your Ink document story into 3dTextBubbles
    public GameObject Player;
    public GameObject Speaker;
    public float Distance_;
    public bool textBoxVisible = false;
    public GameObject textBox;
    public GameObject textBoxInstance;
    public GameObject WorldScriptManager;
    public GameObject canvas;
    public Transform spawnPosition;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        Distance_ = Vector3.Distance(Player.transform.position, Speaker.transform.position);
        if(Distance_ < 3)
        {
            if (textBoxVisible == false)
            {
                textBoxInstance = Instantiate(textBox, spawnPosition);
                textBoxInstance.transform.parent = Speaker.transform;
                textBoxVisible = true;
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton3))
            {
                canvas.SetActive(true);
                WorldScriptManager.GetComponent<BasicInkExample>().RemoveChildren();
                WorldScriptManager.GetComponent<BasicInkExample>().StartStory();
                MenuControl.MC.stopActors();
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
