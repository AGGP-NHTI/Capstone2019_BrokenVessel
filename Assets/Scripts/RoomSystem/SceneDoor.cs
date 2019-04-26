using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrokenVessel.Interact
{
    public class SceneDoor : Interact
    {
        public string Scene;
        public bool cheese = false;

        public override void Impulse()
        {
            MenuControl.MC.Win(cheese);
        }
    }
}