using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondSceneDemoUIScript : MonoBehaviour
{
    [TextArea(3, 12)]
    public string[] instructions;
    public GameObject instructionCanvas;
    public Text instructionText;

    private int index;
    private float timer;
    public float timeFraction;

    private void Start()
    {
        index = 0;
        timer = 0;
        instructionCanvas.SetActive(true);
        instructionText.text = instructions[index];
    }

    private void NextSentence()
    {
        if (index < (instructions.Length - 1))
        {
            index++;
            instructionText.text = instructions[index];
        }
        else
        {
            instructionText.enabled = false;
            instructionCanvas.SetActive(false);
            // turn off the lights on your way out
            this.enabled = false;
        }
    }


    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= timeFraction)
        {
            timer = 0;
            NextSentence();
        }
    }
}
