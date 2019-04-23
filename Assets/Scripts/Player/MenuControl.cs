using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public static MenuControl MC;

    [SerializeField] GameObject TileScreen;
    [SerializeField] GameObject PauseScreen;
    [SerializeField] GameObject Hud;

    [SerializeField] GameObject selector;
    [SerializeField] bool selectedPlay = true;

    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;
    [SerializeField] private KeyCode returnKey = KeyCode.Return;

    [SerializeField] Transform[] hearts;
    [SerializeField] Texture fullHeart;
    [SerializeField] Texture emptyHeart;

    [SerializeField] Text ScrapAmount;

    public bool Pause { get => Input.GetKeyDown(pauseKey); }
    public bool Select { get => Input.GetKeyDown(returnKey); }

    bool ScenePaused = false;
    bool MainMenu = true;

    private void Awake()
    {
        MC = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if(MainMenu)
        {
            TileScreen.SetActive(true);
            PauseScreen.SetActive(false);
            Hud.SetActive(false);
        }
        else
        {
            TileScreen.SetActive(false);
            Hud.SetActive(true);
            if (Input.GetKeyDown(pauseKey))
            {
                TogglePlay();
            }
        }
        if(TileScreen.activeSelf)
        {
            selector.SetActive(true);
            if (Input.GetAxis("Horizontal") > .1)
            {
                selector.transform.localPosition = new Vector3(86, 4, 0);
                selectedPlay = false;
            }
            if (Input.GetAxis("Horizontal") < -.1)
            {
                selector.transform.localPosition = new Vector3(-114, 4, 0);
                selectedPlay = true;
            }
            if (Input.GetKeyDown(returnKey))
            {
                if (selectedPlay) { PlayGame(); }
                else { QuitGame(); }
            }
        }
        else if (PauseScreen.activeSelf)
        {
            selector.SetActive(true);
            if (Input.GetAxis("Vertical") > .1)
            {
                selector.transform.localPosition = new Vector3(-9, -32, 0);
                selectedPlay = true;
            }
            if (Input.GetAxis("Vertical") < -.1)
            {
                selector.transform.localPosition = new Vector3(-10, -70, 0);
                selectedPlay = false;
            }
            if (Input.GetKeyDown(returnKey))
            {
                if (selectedPlay) { TogglePlay(); }
                else { QuitPlayTime(); }
            }
        }
        else
        {
            selector.SetActive(false);
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

    public void UpdateScrap(int currentScrap)
    {
        ScrapAmount.text = "x " + currentScrap;
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
        Debug.Log("Play");
        selector.transform.localPosition = new Vector3(-9, -32, 0);
        selectedPlay = true;
        MainMenu = false;
        //SceneManager.LoadScene("Joe's Work");
        //SceneManager.LoadScene("PlayGame");
    }

    public void QuitPlayTime()
    {
        selector.transform.localPosition = new Vector3(-114, 4, 0);
        selectedPlay = true;
        MainMenu = true;
        SceneManager.LoadScene("MainMenu");
        if(GameObject.Find("Canvas"))
        {
            Destroy(gameObject);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}
