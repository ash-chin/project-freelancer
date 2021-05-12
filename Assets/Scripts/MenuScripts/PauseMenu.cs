using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    /* 
     * This variable is public because we want to access 
     * it from other scripts. 
     * This variable is static because we dont want to refference
     * this specific pause menu script we just want to easily
     * check from other scripts whether the game menu is paused
     * 
     * An example of using this variables value is if the pause
     * menu is open then maybe we want the music volume to turn down
     * of fx volume to turn off the way you access it is by
     * PauseMenu.GameIsPaused this should work from any other script 
     */
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject bountyMenuUI; // Ash addition

    void Update()
    {
        
        if (UnityEngine.InputSystem.Keyboard.current.digit1Key.wasPressedThisFrame) 
        {
            if (GameIsPaused)
            {
                Resume();
            }

            else
            {

                Pause();
            }

        }
    }


    void Pause()
    {
        //active pause menu
        pauseMenuUI.SetActive(true);
        //this line is what actually pauses the game
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("Loading Main Menu...");
        SceneManager.LoadScene("MainMenu");
    }

    public void Resume()
    {
        //resuming so disable menu
        pauseMenuUI.SetActive(false);
        bountyMenuUI.SetActive(false);
        //this line is what restarts the game after pausing
        Time.timeScale = 1f;
        GameIsPaused = false;

    }

    public void Exit()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }

    // ASH ADDITIONS
    public void ViewBounties()
    {
        pauseMenuUI.SetActive(false);
        bountyMenuUI.SetActive(true);
    }

    public void bountiesBack()
    {
        // Use to go back to the pause menu
        pauseMenuUI.SetActive(true);
        bountyMenuUI.SetActive(false);
    }
}
