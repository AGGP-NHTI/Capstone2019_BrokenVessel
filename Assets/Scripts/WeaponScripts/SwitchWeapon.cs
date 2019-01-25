using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapon : MonoBehaviour {

    public Weapon weapon1;
    public Weapon weapon2;

	// Use this for initialization
	void Start () {
        weapon1.equipped = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("e"))
        {
            Switch();
        }
	}

    void Switch()
    {
        weapon1.equipped = !weapon1.equipped;
        weapon2.equipped = !weapon2.equipped;
    }
}
