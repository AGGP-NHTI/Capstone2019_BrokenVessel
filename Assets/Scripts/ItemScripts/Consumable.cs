using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BrokenVessel.Interact
{
    public class Consumable : Interact
    {
        public string type = "";
        public int amount = 0;
        [SerializeField] Transform body;
        private void Update()
        {
            body.Rotate(Vector2.up, 150 * Time.deltaTime);
        }
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
