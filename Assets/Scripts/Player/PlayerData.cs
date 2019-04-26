using System.Collections;
using System.Collections.Generic;
using BrokenVessel.Player;
using UnityEngine;

public class PlayerData : BrokenVessel.Actor.Actor
{
    public static PlayerData PD;

    public int health = 10;
    public float energy = 100.0f;
    public int metalScrap = 0;

    public float iFrameTimer = 0f;
    public bool ignoreDamage = false;

    private void Awake()
    {
        PD = this;
    }

    private void Start()
    {
        MenuControl.MC.UpdateHealth(health);
        MenuControl.MC.UpdateScrap(metalScrap);
    }

    public void takeDamage(int value, float iFrames, Vector2 Knockback)
    {
        //Debug.Log("ow");
        if(iFrameTimer <= 0 && !ignoreDamage)
        {
            GetComponent<Rigidbody2D>().AddForce(Knockback, ForceMode2D.Impulse);
            //Debug.Log("dmg");
            health -= value;
            iFrameTimer = iFrames;
            MenuControl.MC.UpdateHealth(health);
            //knockback
        }
        if(health <= 0)
        {
            GetComponent<Player>().dead = true;
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        GetComponent<Rigidbody2D>().freezeRotation = false;
        yield return new WaitForSeconds(5);
        MenuControl.MC.GameOver();
    }

    public void Heal(int value)
    {
        health += value;
        if (health >= 10)
        {
            health = 10;
        }
        MenuControl.MC.UpdateHealth(health);
    }

    public void gainScrap(int value)
    {
        metalScrap += value;
        MenuControl.MC.UpdateScrap(metalScrap);
    }

    public void loseScrap(int value)
    {
        metalScrap -= value;
        if(metalScrap < 0)
        {
            metalScrap = 0;
        }
        MenuControl.MC.UpdateScrap(metalScrap);
    }

    void Update()
    {
        if (paused) { return; }
        if (iFrameTimer > 0)
        {
            iFrameTimer -= Time.deltaTime;
        }
    }

}
