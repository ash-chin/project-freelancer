using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
   


    public void StartGame()
    {
        Debug.Log("Starting game...");
        SceneManager.LoadScene("TestEnvironment 1");
    }

    public void LevelSelector()
    {
        Debug.Log("Loading level selection");
        //SceneManager.LoadScene("LevelSelector");
    }

    public void Exit()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
