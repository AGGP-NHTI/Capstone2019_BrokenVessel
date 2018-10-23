using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float limitUp    =    5;
    public float limitDown  =    -5;
    public float limitLeft  =    -5;
    public float limitRight =    5;
    public float speed = 3;

    GameObject player;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void FixedUpdate ()
    {
        Vector3 target = player.transform.position;
        target.y += 2;
        target.z = -10;
        if (transform.position.y > limitUp && player.transform.position.y > limitUp)
        {
            target.y = limitUp;
        }
        if (transform.position.y < limitDown && player.transform.position.y < limitDown)
        {
            target.y = limitDown;
        }

        if (transform.position.x > limitRight && player.transform.position.x > limitRight)
        {
            target.x = limitRight;
        }
        if (transform.position.x < limitLeft && player.transform.position.x < limitLeft)
        {
            target.x = limitLeft;
        }

        transform.position = Vector3.Lerp(transform.position, target, speed * Time.fixedDeltaTime);
    }
}
