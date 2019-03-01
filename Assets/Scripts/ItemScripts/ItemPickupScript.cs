using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupScript : MonoBehaviour {
    public GameObject player;
    List<GameObject> inventoryStorage;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            player.GetComponent<InventoryScript>().inventoryStorage.Add(gameObject);
        }
    }
}
