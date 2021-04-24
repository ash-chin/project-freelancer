using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class POCStartMenu : MonoBehaviour
{
    public string sceneChoice;
    public void StartGame()
    {
        SceneManager.LoadScene(sceneChoice);
    }
}
