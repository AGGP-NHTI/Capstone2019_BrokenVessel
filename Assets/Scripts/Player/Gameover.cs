using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    [SerializeField] private KeyCode ControllerJump = KeyCode.JoystickButton0; //A
    [SerializeField] private KeyCode ControllerPause = KeyCode.JoystickButton7;
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;
    [SerializeField] private KeyCode returnKey = KeyCode.Return;

    public bool Select { get => Input.GetKeyDown(returnKey) || Input.GetKeyDown(ControllerJump) || Input.GetKeyDown(pauseKey) || Input.GetKeyDown(ControllerPause); }
    void Update()
    {
        if(Select)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
