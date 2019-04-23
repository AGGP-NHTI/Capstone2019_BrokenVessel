using UnityEngine;

namespace BrokenVessel.Interact
{
    public class InteractButton : Interact
    {
        public GameObject hatch;

        private bool activated = false;

        public override void Impulse()
        {
            if(!activated)
            {
                hatch.transform.position -= new Vector3(3, 0, 0);
            }
            activated = true;
        }
    }
}