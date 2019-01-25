using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysPressed : MonoBehaviour {

    public bool wHeld = false;
    public bool aHeld = false;
    public bool sHeld = false;
    public bool dHeld = false;
    public bool qHeld = false;

    private void Update()
    {
        CheckKeys();
    }

    public void CheckKeys()
    {
        if (Input.GetKey("w") == true)
        {
            wHeld = true;

            //dash();
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
    }
}
