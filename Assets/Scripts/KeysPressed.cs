using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysPressed : MonoBehaviour {

    public bool wHeld = false;
    public bool aHeld = false;
    public bool sHeld = false;
    public bool dHeld = false;
    public bool qHeld = false;
    public bool eHeld = false;
    public bool jHeld = false;
    public bool spaceHeld = false;
    public bool upArrowHeld = false;
    public bool leftArrowHeld = false;
    public bool downArrowHeld = false;
    public bool rightArrowHeld = false;

    private void Update()
    {
        CheckKeys();
    }

    public void CheckKeys()
    {
        if (Input.GetKey("w") == true)
        {
            wHeld = true;
        }
        else
        {
            wHeld = false;
        }
        if (Input.GetKey("a") == true)
        {
            aHeld = true;
        }
        else
        {
            aHeld = false;
        }
        if (Input.GetKey("s") == true)
        {
            sHeld = true;
        }
        else
        {
            sHeld = false;
        }
        if (Input.GetKey("d") == true)
        {
            dHeld = true;
        }
        else
        {
            dHeld = false;
        }
        if (Input.GetKey("q") == true)
        {
            qHeld = true;
        }
        else
        {
            qHeld = false;
        }
        if (Input.GetKey("e") == true)
        {
            eHeld = true;
        }
        else
        {
            eHeld = false;
        }
        if (Input.GetKey("j") == true)
        {
            jHeld = true;
        }
        else
        {
            jHeld = false;
        }
        if (Input.GetKey("space") == true)
        {
            spaceHeld = true;
        }
        else
        {
            spaceHeld = false;
        }
        if (Input.GetKey(KeyCode.UpArrow) == true)
        {
            upArrowHeld = true;
        }
        else
        {
            upArrowHeld = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow) == true)
        {
            leftArrowHeld = true;
        }
        else
        {
            leftArrowHeld = false;
        }
        if (Input.GetKey(KeyCode.DownArrow) == true)
        {
            downArrowHeld = true;
        }
        else
        {
            downArrowHeld = false;
        }
        if (Input.GetKey(KeyCode.RightArrow) == true)
        {
            rightArrowHeld = true;
        }
        else
        {
            rightArrowHeld = false;
        }
    }
}
