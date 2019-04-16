using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public static MenuControl MC;

    [SerializeField] GameObject PauseScreen;

    [SerializeField]
    private KeyCode pauseKey = KeyCode.Escape;

    [SerializeField] Transform[] hearts;
    [SerializeField] Texture fullHeart;
    [SerializeField] Texture emptyHeart;

    public bool Pause { get => Input.GetKeyDown(pauseKey); }

    bool ScenePaused = false;

    private void Awake()
    {
        MC = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            TogglePlay();
        }
    }

    public void UpdateHealth(int currentHP)
    {
        for (int i = 0; i < 10; i++)
        {
            if(i < currentHP)
            {
                hearts[i].GetComponent<RawImage>().texture = fullHeart;
            }
            else
            {
                hearts[i].GetComponent<RawImage>().texture = emptyHeart;
            }
        }
    }

    public void TogglePlay()
    {
        if(ScenePaused)
        {
            ScenePaused = false;
            PauseScreen.SetActive(false);
            resumeActors();
        }
        else
        {
            ScenePaused = true;
            PauseScreen.SetActive(true);
            stopActors();
        }
    }

    public void stopActors()
    {
        BrokenVessel.Actor.Actor.paused = true;
        foreach (Rigidbody2D rb2d in FindObjectsOfType<Rigidbody2D>())
        {
            rb2d.simulated = false;
        }
    }

    public void resumeActors()
    {
        PauseScreen.SetActive(false);
        BrokenVessel.Actor.Actor.paused = false;
        foreach (Rigidbody2D rb2d in FindObjectsOfType<Rigidbody2D>())
        {
            rb2d.simulated = true;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("PlayGame");
    }

    public void QuitPlayTime()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
