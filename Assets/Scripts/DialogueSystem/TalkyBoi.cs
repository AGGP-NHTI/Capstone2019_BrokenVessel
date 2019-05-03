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

    public BasicInkExample BIE;

    public int choice = 0;
    float canMove = 0;

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
                BIE.ChangeSelectorView(true);
                BIE.RemoveChildren();
                BIE.StartStory();
                MenuControl.MC.stopActors();
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                canvas.SetActive(true);
                BIE.RemoveChildren();
                BIE.StartStory();
                MenuControl.MC.stopActors();
            }
            if (canvas.activeSelf)
            {
                if (Input.GetAxis("Vertical") > .85f && (canMove <= 0 || canMove > .5f))
                {
                    Debug.Log("UP");
                    if (--choice < 0) { choice = 0; }
                    canMove += Time.deltaTime;
                }
                else if (Input.GetAxis("Vertical") < -.85f && (canMove <= 0 || canMove > .5f))
                {
                    if (++choice <= BIE.GetChoice()) { choice = BIE.GetChoice() - 1; }
                    canMove += Time.deltaTime;
                }
                else { canMove = 0; }
                BIE.SetSelector(choice);

                if (Input.GetKeyDown(KeyCode.JoystickButton0))
                {
                    BIE.FinalizeSelect(choice);
                    choice = 0;
                    BIE.ChangeSelectorView(false);
                }
                
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
