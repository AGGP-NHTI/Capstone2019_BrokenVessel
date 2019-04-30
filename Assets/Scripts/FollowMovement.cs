using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMovement : MonoBehaviour {

    [SerializeField] bool targetIsRadius = false;
    public float limitUp    =    0;
    public float limitDown  =    0;
    public float limitLeft  =    0;
    public float limitRight =    0;
    public float speed      =    3;
    public float offSetX    =    0;
    public float offSetY    =    2;
    public float offSetZ    =   -10;


    GameObject player;

    [SerializeField] bool isEnemy = false;
    public bool seePlayer = false;

    void Start ()
    {
        player = GameObject.Find("Player");
    }
	
	void Update ()
    {
        if ((isEnemy && seePlayer) || !isEnemy)
        {
            Vector3 target;

            if (targetIsRadius)
            {
                target = (transform.position - player.transform.position).normalized * offSetX;
                target.y = Mathf.Abs(target.y);
                target.y += offSetY;
                target = player.transform.position + target;
            }
            else
            {
                target = player.transform.position;
                target.x += offSetX;
                target.y += offSetY;
                target.z = offSetZ;
            }
            if (limitUp == 0 && limitDown == 0 && limitRight == 0 && limitLeft == 0)
            {

            }
            else
            {
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
            }

            transform.position = Vector3.Lerp(transform.position, target, speed * Time.fixedDeltaTime);
        }
    }
}
