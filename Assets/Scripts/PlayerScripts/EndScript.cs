using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
    public string restart;
    public float fadeTime;
    public float hangTime;
    public CanvasGroup endScreen;
    private float timer = 0;

    public void EndGame()
    {
        timer += Time.deltaTime;
        endScreen.alpha = timer / fadeTime;

        if (timer >= (fadeTime + hangTime))
        {
            SceneManager.LoadScene(restart);
        }
    }
}
