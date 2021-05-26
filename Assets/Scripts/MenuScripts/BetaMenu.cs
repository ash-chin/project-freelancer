using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetaMenu : MonoBehaviour
{
    // public Canvas mainCanvas;
    public GameObject mainCanvas;
    public Canvas controlsCanvas;
    public Canvas howToPlayCanvas;
    //public RawImage header;

    public void ControlsToStart()
    {
        controlsCanvas.enabled = false;
        //mainCanvas.enabled = true;
        mainCanvas.SetActive(true);
        //header.enabled = true;
    }

    public void HowToToStart()
    {
        howToPlayCanvas.enabled = false;
        // mainCanvas.enabled = true;
        mainCanvas.SetActive(true);
        //header.enabled = false;
    }

    public void ToControls()
    {
        // mainCanvas.enabled = false;
        mainCanvas.SetActive(false);
        controlsCanvas.enabled = true;
    }

    public void ToHowTo()
    {
        // mainCanvas.enabled = false;
        mainCanvas.SetActive(false);
        howToPlayCanvas.enabled = true;
    }

    private void Start()
    {
        // mainCanvas.enabled = true;
        mainCanvas.SetActive(true);
        controlsCanvas.enabled = false;
        howToPlayCanvas.enabled = false;
    }
}
