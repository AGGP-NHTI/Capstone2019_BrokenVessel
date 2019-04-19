using System.Collections;
using System.Collections.Generic;
using BrokenVessel.Interact;
using UnityEngine;

public class EnemyDrops : MonoBehaviour
{
    [SerializeField] GameObject healthPickUp;
    [SerializeField] GameObject scrapPickUp;

    [SerializeField] bool isTypeRandom = true;
    enum drops { Health, Scrap };
    [SerializeField] drops Drop = drops.Health;

    [SerializeField] bool isAmountRandom = true;
    [SerializeField] int minHealthAmount = 0;
    [SerializeField] int maxHealthAmount = 0;
    [SerializeField] int minScrapAmount = 0;
    [SerializeField] int maxScrapAmount = 0;
    [SerializeField] int Amount = 0;


    public void DropItems()
    {
        if (isTypeRandom) { Drop = (drops)Random.Range(0f, 1f); }
        if(isAmountRandom)
        {
            if (Drop == drops.Scrap) { Amount = (int)Random.Range(minScrapAmount, maxScrapAmount + 1); }
            else { Amount = (int)Random.Range(minHealthAmount, maxHealthAmount + 1); }
        }
        if (Amount > 0)
        {
            GameObject temp;
            switch (Drop)
            {
                case drops.Health:
                    temp = Instantiate(healthPickUp, transform.position, Quaternion.identity) as GameObject;
                    temp.GetComponent<Consumables>().type = "Health";
                    temp.GetComponent<Consumables>().amount = Amount;
                    break;
                case drops.Scrap:
                    temp = Instantiate(scrapPickUp, transform.position, Quaternion.identity) as GameObject;
                    temp.GetComponent<Consumables>().type = "Scrap";
                    temp.GetComponent<Consumables>().amount = Amount;
                    break;
            }
        }
    }
}
