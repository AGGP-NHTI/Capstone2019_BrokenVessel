using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCheck : MonoBehaviour
{
    [SerializeField] int targetLayer = 0;
    public bool hit = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == targetLayer) //|| other.gameObject.layer == 9)
        {
            hit = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == targetLayer) //|| other.gameObject.layer == 9)
        {
            hit = false;
        }
    }
}
