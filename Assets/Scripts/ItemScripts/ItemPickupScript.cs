using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupScript : MonoBehaviour {
    GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            GameObject[] inventoryStorage = new GameObject[player.GetComponent<InventoryScript>().inventoryStorage.Length];

            if (gameObject.GetComponent<ItemData>().autoPickup)
            {
                addItemToInv(inventoryStorage);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                addItemToInv(inventoryStorage);
            }

            player.GetComponent<InventoryScript>().inventoryStorage = inventoryStorage;
        }
    }

    private GameObject[] addItemToInv(GameObject[] origInv)
    {
        GameObject[] newInv = new GameObject[origInv.Length + 1];
        for(int i = 0; i < origInv.Length + 1; i++)
        {
            newInv[i] = origInv[i];
        }

        newInv[origInv.Length] = gameObject;

        return newInv;
    }
}
