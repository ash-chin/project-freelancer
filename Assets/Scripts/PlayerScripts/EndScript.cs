using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
    public Slider hull;
    public Slider fuel;
    public float fadeTime;
    public float hangTime;
    public CanvasGroup endScreen;
    private float timer = 0;
    private void Update()
    {
        Debug.Log("This is updating");
        if (hull.value == 0 | fuel.value == 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        timer += Time.deltaTime;
        endScreen.alpha = timer / fadeTime;

        if (timer >= (fadeTime + hangTime))
        {
            SceneManager.LoadScene("POCIntro");
        }
    }
}
