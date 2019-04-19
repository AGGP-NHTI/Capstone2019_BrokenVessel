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
    public GameObject SceneController;

    // Use this for initialization
    void Start () {
        SceneController = GameObject.Find("Scene Controller");
	}
	
	// Update is called once per frame
	void Update () {
        Distance_ = Vector3.Distance(Player.transform.position, Speaker.transform.position);
        if(Distance_ < 7)
        {
            if (textBoxVisible == false)
            {
                textBoxInstance = Instantiate(textBox);
                textBoxInstance.transform.parent = Speaker.transform;
                textBoxVisible = true;
            }
            if (WorldScriptManager.GetComponent<KeysPressed>().jHeld == true)
            {
                canvas.SetActive(true);
                SceneController.GetComponent<BasicInkExample>().RemoveChildren();
                SceneController.GetComponent<BasicInkExample>().StartStory();
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
