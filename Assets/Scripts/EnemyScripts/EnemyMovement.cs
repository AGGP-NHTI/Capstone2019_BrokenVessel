using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float speed = 5;

    public bool followLedge = false;
    public bool bounceOff = false;
    public bool moveOnJump = false;
    public bool activeIfPlayer = false;
    public enum DetectionType {none, ray, circle, box};
    public DetectionType choice = DetectionType.none;
    public float range = 10f;

    public bool hitHead = false;
    bool onLedge = false;
    bool grounded = true;
    bool isJumping = false;
    int timer = 0;
    bool seePlayer = false;

    public Transform faceCheck;
    public Transform ledgeCheck;
    public LayerMask realGround;
    public LayerMask target;

    Rigidbody2D rig;

    void Start ()
    {
        rig = GetComponent<Rigidbody2D>();
	}
	
	void Update()
    {
        grounded = Physics2D.CircleCast(ledgeCheck.position, .2f, Vector2.zero, 0, realGround);
        onLedge = !Physics2D.CircleCast(ledgeCheck.position, .075f, Vector2.zero, 0, realGround);

        if (followLedge && grounded)
        {
            rig.gravityScale = 0;
            rig.freezeRotation = true;
        }
        else
        {
            rig.gravityScale = 1;
            rig.freezeRotation = false;
        }
        if(bounceOff && (hitHead || onLedge) && !moveOnJump)
        {
            Flip();
            speed = -speed;
        }
        if(bounceOff && moveOnJump && hitHead)
        {
            Flip();
            speed = -speed;
        }
        if (!grounded && !moveOnJump)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            RaycastHit2D hit = Physics2D.Raycast(ledgeCheck.position, Vector2.down, .3f, realGround);
            if(hit)
            {
                transform.position = new Vector3(transform.position.x, hit.transform.position.y);
            }
        }
        else if (followLedge && hitHead)
        {
            rig.isKinematic = true;
            transform.position = faceCheck.position;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z + (90 * transform.localScale.x)));
            rig.isKinematic = false;
        }
        hitHead = false;
    }
    private void FixedUpdate()
    {
        switch (choice)
        {
            case DetectionType.none:
                seePlayer = true;
                break;
            case DetectionType.ray:
                seePlayer = Physics2D.Raycast(faceCheck.position, Vector2.right * transform.localScale.x, range, target);
                Debug.DrawRay(faceCheck.position, Vector2.right * transform.localScale.x, Color.red, range);
                break;
            case DetectionType.circle:
                seePlayer = Physics2D.CircleCast(transform.position, range, Vector2.zero, 0, target);
                break;
            case DetectionType.box:
                Debug.Log("ERROR: NOT ADDED");
                break;
        }

        if (seePlayer)
        {
            if (moveOnJump)
            {
                if (!onLedge)
                {
                    isJumping = true;
                }
                if(isJumping && timer >= 80)
                {
                    rig.AddForce(transform.up * Mathf.Abs(speed) * 450, ForceMode2D.Force);
                    rig.AddForce(transform.right * speed * 150);
                }
                if (timer >= 90)
                {
                    rig.AddForce(transform.up * -speed * 500, ForceMode2D.Force);
                    timer = 0;
                    isJumping = false;
                }
                timer++;
            }
            else
            {
                if (!onLedge || !followLedge)
                {
                    rig.AddForce(transform.right * speed * 100, ForceMode2D.Force);
                }
                else if (grounded)
                {
                    transform.Rotate(new Vector3(0, 0, -10 * transform.localScale.x));
                    rig.velocity = Vector3.zero;
                }
            }
        }
        seePlayer = false;
    }


    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
