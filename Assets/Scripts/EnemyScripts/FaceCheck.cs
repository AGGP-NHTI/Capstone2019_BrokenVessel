using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCheck : MonoBehaviour
{
    [SerializeField] int targetLayer = 0;
    public bool hit = false;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == targetLayer && hit) //|| other.gameObject.layer == 9)
        {
            hit = false;
            Debug.Log(hit);
        }
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == targetLayer&& !hit) //|| other.gameObject.layer == 9)
        {
            hit = true;
        }
    }
}
