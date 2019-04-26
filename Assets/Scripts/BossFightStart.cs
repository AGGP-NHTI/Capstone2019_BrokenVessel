using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossFightStart : MonoBehaviour
{
    public ScrapQueen boss;
    public AudioSource BossTheme;
    public AudioSource LevelMusic;

    void OnTriggerStay2D(Collider2D collide)
    {
        if (collide.gameObject.layer == 9)
        {
            boss.start = true;
            BossTheme.enabled = true;
            LevelMusic.enabled = false;
        }
    }
}

