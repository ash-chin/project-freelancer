using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    public CanvasGroup endscreen;
    private float timer = 0;
    public float fadeDuration;
    public float endDuration;

    private void Start()
    {
        endscreen.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 6)
        {
            EndSequence();
        }
    }

    private void EndSequence()
    {
        endscreen.alpha = timer / fadeDuration;

        if (timer > (fadeDuration + endDuration))
        {
            SceneManager.LoadScene("POCIntro");
        }
    }
}
