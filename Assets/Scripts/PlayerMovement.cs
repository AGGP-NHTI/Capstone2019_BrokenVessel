using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float maxSpeed = 7;
    public float jumpForce = 50;
    bool facingRight = true;
    int airTime = 0;
    int airLimit = 30;

    bool grounded = false;
    bool hitHead = false;
    bool isJumping = false;
    public Transform groundCheck;
    public Transform headCheck;
    public LayerMask realGround;
    Vector2 boxCheckSize = new Vector2(.9f, .25f);

    Rigidbody2D rig;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    
    }


    void FixedUpdate()
    //protected override void ComputeVelocity()
    {
        float move = 0;
        //float move = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.A))
        {
            move = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move = 1;
        }

        if (move > 0 && facingRight != true)
        {
            Flip();
            facingRight = true;
        }
        else if(move < 0 && facingRight != false)
        {
            Flip();
            facingRight = false;
        }

        grounded = Physics2D.OverlapBox(groundCheck.position, boxCheckSize, 0, realGround);
        hitHead = Physics2D.OverlapBox(headCheck.position, boxCheckSize, 0, realGround);
        //Debug.Log("g" + grounded);
        //Debug.Log("h" + hitHead);

        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            isJumping = true;
            rig.AddForce(Vector2.up * jumpForce);
        }
        if (Input.GetKey(KeyCode.Space) && isJumping && hitHead == false)
        {
            airTime++;
            //Debug.Log(airTime);
            if (airTime <= 15)
            {
                rig.AddForce(Vector2.up * jumpForce * 2);
            }
            else if (airTime < 30)
            {
                rig.AddForce(Vector2.up * jumpForce);
            }
            if (airTime >= airLimit)
            {
                rig.velocity = new Vector2(move * maxSpeed, 0);
                isJumping = false;
            }
        }
        else if (!Input.GetKey(KeyCode.Space) || hitHead || airTime > airLimit || isJumping == false)
        {
            airTime = 0;
            isJumping = false;
            rig.AddForce(Vector2.down * jumpForce * 2);
        }

        rig.velocity = new Vector2(move * maxSpeed, 0);
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
