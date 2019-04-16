using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public bool facingRight = true;

    public bool isJumping = false;
    public float move = 0;

    void Update()
    {
        move = Input.GetAxis("Horizontal");
      
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
        if (Input.GetButton(KeyCode.Button0))
        {
            isJumping = true;
        }
        if (Input.GetButton(KeyCode.Button0))
        {
            isJumping = false;
        }
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
