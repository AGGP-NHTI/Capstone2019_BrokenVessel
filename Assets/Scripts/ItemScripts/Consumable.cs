using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BrokenVessel.Interact
{
    public class Consumable : Interact
    {
        public string type = "";
        public int amount = 0;
        public override void Impulse()
        {
            switch(type)
            {
                case "Health":
                    PlayerData.PD.Heal(amount);
                    break;
                case "Scrap":
                    PlayerData.PD.gainScrap(amount);
                    break;
            }
            Destroy(gameObject);
        }
    }
}
