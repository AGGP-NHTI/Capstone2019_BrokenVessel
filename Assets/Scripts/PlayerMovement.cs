using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    PlayerData data;
    [SerializeField] PlayerAbilities pa;

    public float maxSpeed = 7;
    public float jumpForce = 50;

    bool facingRight = true;
    public bool grounded = false;
    public bool facingRight = true;
    bool isJumping = false;

    [SerializeField] Transform groundCheck;
    public Transform groundCheck;
    public LayerMask realGround;

    Vector2 boxCheckSize = new Vector2(.9f, .25f);

    Rigidbody2D rig;

    void Start()
    {
        data = GetComponent<PlayerData>();
        rig = GetComponent<Rigidbody2D>();

    }


    void Update()
    {
        Vector2 processVelocity = rig.velocity;
        float move = Input.GetAxis("Horizontal");
        processVelocity.x = move * maxSpeed;

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

        if (Input.GetKeyDown(KeyCode.Space) && (grounded || pa.wallclimb))
        {
            isJumping = true;
            processVelocity.y = jumpForce;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            if (processVelocity.y > 0)
            {
                processVelocity.y /= 3f;
            }
        }
        if (!grounded)
        {
            processVelocity.y -= 18f * Time.deltaTime;

            if (processVelocity.y < -18f)
            {
                processVelocity.y = -18f;
            }
        }
        else if(!isJumping)
        {
            isJumping = false;
            processVelocity.y = 0;
        }

        rig.velocity = new Vector2(move * maxSpeed, processVelocity.y);


    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
