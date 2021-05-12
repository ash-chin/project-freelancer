using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DemoUIScript : MonoBehaviour
{
    [TextArea(3, 12)]
    public string[] sentences;
    public GameObject instructions;
    public Text instructionText;
    private int sentenceIndex = 0;
    private float timer;
    public float timeFraction;
    private bool activationBarrier = false;

    private void Start()
    {
        instructions.SetActive(true);
        instructionText.enabled = true;
        instructionText.text = sentences[sentenceIndex];
    }

    private void MoveSentence()
    {
        if (sentenceIndex < sentences.Length - 1)
        {
            sentenceIndex++;
            instructionText.text = sentences[sentenceIndex];
        } 
        else
        {
            instructionText.text = "";
            instructionText.enabled = false;
            instructions.SetActive(false);
            // turn off the lights on your way out
            this.enabled = false;
        }
    }

    private void Update()
    {

        timer += Time.deltaTime;
        // I want to acknowledge up front that this method fucking sucks, but I haven't found another way to do it yet, so rip
        if (timer >= timeFraction && activationBarrier)
        {
            MoveSentence();
            activationBarrier = false;
            timer = 0;
        }

        if ((Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.sKey.wasPressedThisFrame) && sentenceIndex == 0)
        {
            activationBarrier = true;
        } 
        else if ((Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.dKey.wasPressedThisFrame) && sentenceIndex == 1)
        {
            activationBarrier = true;
        }
        else if ((Keyboard.current.qKey.wasPressedThisFrame || Keyboard.current.eKey.wasPressedThisFrame) && sentenceIndex == 2)
        {
            activationBarrier = true;
        }
        else if ((Keyboard.current.iKey.wasPressedThisFrame || Keyboard.current.kKey.wasPressedThisFrame) && sentenceIndex == 3)
        {
            activationBarrier = true;
        }
        else if ((Keyboard.current.jKey.wasPressedThisFrame || Keyboard.current.lKey.wasPressedThisFrame) && sentenceIndex == 4)
        {
            activationBarrier = true;
        }
        else if ((Keyboard.current.uKey.wasPressedThisFrame || Keyboard.current.oKey.wasPressedThisFrame) && sentenceIndex == 5)
        {
            activationBarrier = true;
        }
        else if ((Keyboard.current.tKey.wasPressedThisFrame) && sentenceIndex == 6)
        {
            activationBarrier = true;
        }
        else if ((Keyboard.current.pKey.wasPressedThisFrame) && sentenceIndex == 7)
        {
            activationBarrier = true;
        }
        else if ((Keyboard.current.tKey.wasPressedThisFrame) && sentenceIndex == 8)
        {
            activationBarrier = true;
        }
        else if (timer > timeFraction && sentenceIndex == 9)
        {
            activationBarrier = true;
        }
    }
}
