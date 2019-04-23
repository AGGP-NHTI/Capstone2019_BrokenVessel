using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour
{
    public int speed = 30;
    public int heightLimit = 100;

    void Update()
    {
        if (transform.localPosition.y < heightLimit)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
    }
}
