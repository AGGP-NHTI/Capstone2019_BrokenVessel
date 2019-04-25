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
    [SerializeField] GameObject BossSection;

    [SerializeField] GameObject selector;
    [SerializeField] bool selectedPlay = true;

    [SerializeField] private KeyCode ControllerJump = KeyCode.JoystickButton0; //A
    [SerializeField] private KeyCode ControllerPause = KeyCode.JoystickButton7;
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;
    [SerializeField] private KeyCode returnKey = KeyCode.Return;

    [SerializeField] Transform[] hearts;
    [SerializeField] Texture fullHeart;
    [SerializeField] Texture emptyHeart;

    [SerializeField] Text ScrapAmount;

    public bool Pause { get => Input.GetKeyDown(pauseKey) || Input.GetKeyDown(ControllerPause); }
    public bool Select { get => Input.GetKeyDown(returnKey) || Input.GetKeyDown(ControllerJump); }

    bool ScenePaused = false;
    bool MainMenu = true;

    private void Awake()
    {
        MC = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (MainMenu)
        {
            TileScreen.SetActive(true);
            PauseScreen.SetActive(false);
            Hud.SetActive(false);
        }
        else
        {
            TileScreen.SetActive(false);
            Hud.SetActive(true);
            if (Pause)
            {
                TogglePlay();
            }
        }
        if (PauseScreen.activeSelf)
        {
            selector.SetActive(true);
            if (Input.GetAxis("Vertical") > .1)
            {
                selector.transform.localPosition = new Vector3(-18, -32, 0);
                selectedPlay = true;
            }
            if (Input.GetAxis("Vertical") < -.1)
            {
                selector.transform.localPosition = new Vector3(-18, -70, 0);
                selectedPlay = false;
            }
            if (Select)
            {
                if (selectedPlay) { TogglePlay(); }
                else { QuitPlayTime(); }
            }
        }
        else if (TileScreen.activeSelf)
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
            if (Select)
            {
                if (selectedPlay) { PlayGame(); }
                else { QuitGame(); }
            }
        }
        else
        {
            selector.SetActive(false);
        }
    }

    public void OpenBossBar(EnemyCombat EC)
    {
        if(BossSection.activeSelf == false)
        {
            BossSection.SetActive(true);
            BossHealth.BH.maxHealth = EC.health;
            BossHealth.BH.Refer = EC;
        }
    }

    public void CloseBossBar()
    {
        if (BossSection.activeSelf == true)
        {
            BossHealth.BH.maxHealth = 0;
            BossHealth.BH.Refer = null;
            BossSection.SetActive(false);
        }
    }

    public void UpdateHealth(int currentHP)
    {
        for (int i = 0; i < 10; i++)
        {
            if (i < currentHP)
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
        if (ScenePaused)
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
        selectedPlay = true;
        MainMenu = false;
        SceneManager.LoadScene("VERT SLICE");
        selector.SetActive(false);
        selector.transform.localPosition = new Vector3(-9, -32, 0);
        //SceneManager.LoadScene("PlayGame");
    }

    public void QuitPlayTime()
    {
        TogglePlay();
        selectedPlay = true;
        MainMenu = true;
        SceneManager.LoadScene("MainMenu");
        selector.SetActive(false);
        selector.transform.localPosition = new Vector3(-114, 4, 0);
        if (GameObject.Find("Canvas"))
        {
            Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
        Destroy(gameObject);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}
