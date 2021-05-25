using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

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
    public GameObject galleryMenuUI; // located under player object
    public Canvas gallery;
    public Canvas OuterHud;
    // public AudioMixer mainMixer; // Ash addition - currently not in use


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
        OuterHud.enabled = false;
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
        OuterHud.enabled = true;
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

    public void ViewGallery()
    {
        pauseMenuUI.SetActive(false);
        galleryMenuUI.SetActive(true);
        gallery.enabled = true;
    }

    public void galleryBack()
    {
        // Use to go back to the pause menu
        pauseMenuUI.SetActive(true);
        galleryMenuUI.SetActive(false);
        gallery.enabled = false;
    }

/*    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("mainVolume", volume);
    }*/
}
