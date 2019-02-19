using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapon : MonoBehaviour {

    public GameObject equippedWeapon;
    public GameObject backupWeapon;
    public GameObject placeHolder;
    public GameObject equippedWeaponInstance;
    public GameObject Player;

    // Use this for initialization
    void Start () {
        Player = GameObject.Find("Player");
	}

    // Update is called once per frame
    void Update()
    {
        if (equippedWeaponInstance == null)
        {
            equippedWeaponInstance = Instantiate(equippedWeapon, Player.transform);
            equippedWeaponInstance.transform.parent = Player.transform;
        }

        if (Input.GetKeyDown("e"))
        {
            Switch();
            Destroy(equippedWeaponInstance);
        }
        if (Input.GetKeyDown("q"))
        {
            equippedWeaponInstance.GetComponent<Weapon>().Attack();
        }
    }

    void Switch()
    {
        placeHolder = equippedWeapon;
        equippedWeapon = backupWeapon;
        backupWeapon = placeHolder;
    }
}