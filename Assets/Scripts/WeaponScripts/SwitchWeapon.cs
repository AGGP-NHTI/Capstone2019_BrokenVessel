using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapon : MonoBehaviour {

    public Weapon equippedWeapon;
    public Weapon backupWeapon;
    public Weapon placeHolder;

	// Use this for initialization
	void Start () {
        
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            Switch();
        }
        if (Input.GetKeyDown("q"))
        {
            equippedWeapon.Attack();
        }
    }

    void Switch()
    {
        placeHolder = equippedWeapon;
        equippedWeapon = backupWeapon;
        backupWeapon = placeHolder;
    }
}
