using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    public CanvasGroup endImage;
    public float preTime;
    public float fadeTime;
    public float lastTime;

    private float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= preTime)
        {
            GameEnding();
        }
    }

    private void GameEnding()
    {
        endImage.alpha = (timer - preTime) / fadeTime;

        if (timer >= preTime + fadeTime + lastTime)
        {
            SceneManager.LoadScene("POCIntro");
        }
    }
}
