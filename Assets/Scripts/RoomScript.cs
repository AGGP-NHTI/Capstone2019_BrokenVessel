using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour {

    public float Lup = 0;
    public float Ldown = 0;
    public float Lright = 0;
    public float Lleft = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            FollowMovement cms = GameObject.Find("Camera").GetComponent<FollowMovement>();
            cms.limitUp = Lup;
            cms.limitDown = Ldown;
            cms.limitRight = Lright;
            cms.limitLeft = Lleft;
        }
    }


}
