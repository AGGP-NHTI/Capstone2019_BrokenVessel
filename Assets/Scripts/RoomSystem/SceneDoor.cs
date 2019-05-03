using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BrokenVessel.Interact
{
    public class SceneDoor : Interact
    {
        public string Scene;
        public bool winDoor;
        public bool cheese = false;

        public override void Impulse()
        {
            if (winDoor) { MenuControl.MC.Win(cheese); }
            else
            {
                SceneManager.LoadScene(Scene);
            }
        }
    }
}