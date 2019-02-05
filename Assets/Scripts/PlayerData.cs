using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

    public float health = 100.0f;
    public float energy = 100.0f;
    public int metalScrap = 0;

    public float speed = 7f;
    public float jumpForce = 11f;

    public float iFrameTimer = 0f;
    public bool ignoreDamage = false;

    public bool dash = false;
    public bool wallGrab = false;
    public bool grappleHook = false;
    public bool phase = false;
    public bool phaseDash = false;
    public bool chargeJump = false;
    public int shipControl = 0;

    [SerializeField] GameObject testShield;

    public void takeDamage(float value, float iFrames) //, Vector2 Knockback
    {
        Debug.Log("ow");
        if(iFrameTimer <= 0 && !ignoreDamage)
        {
            testShield.GetComponent<MeshRenderer>().enabled = true;
            health -= value;
            iFrameTimer = iFrames;
            //knockback
        }
    }
    void Update()
    {
        if(iFrameTimer > 0)
        {
            iFrameTimer -= Time.deltaTime;
        }
        if(iFrameTimer < 0)
        {
            testShield.GetComponent<MeshRenderer>().enabled = false;
        }
    }

}
