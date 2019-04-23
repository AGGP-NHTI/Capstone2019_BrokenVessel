using System.Collections;
using System.Collections.Generic;
using BrokenVessel.Interact;
using UnityEngine;

public class EnemyDrops : MonoBehaviour
{
    [SerializeField] GameObject healthPickUp;
    [SerializeField] GameObject scrapPickUp;
    enum Drops { none, health, scrap};
    [SerializeField] bool isTypeRandom = true;
    [SerializeField] Drops dropType;

    [SerializeField] bool isAmountRandom = true;
    [SerializeField] int Amount = 1;
    [SerializeField] Vector2 range = Vector2.one;

    public void DropItem()
    {
        if(isTypeRandom) { dropType = (Drops)Random.Range(0, 3); }
        if (dropType == Drops.scrap) { range *= 10; }
        if (isAmountRandom) { Amount = (int)Random.Range(range.x, range.y); }

        GameObject temp;
        if (Amount > 1)
        {
            switch (dropType)
            {
                case Drops.health:
                    temp = Instantiate(healthPickUp, transform.position, Quaternion.identity) as GameObject;
                    temp.GetComponent<Consumable>().amount = Amount;
                    break;
                case Drops.scrap:
                    temp = Instantiate(scrapPickUp, transform.position, Quaternion.identity) as GameObject;
                    temp.GetComponent<Consumable>().amount = Amount;
                    break;
            }
        }
    }
}
