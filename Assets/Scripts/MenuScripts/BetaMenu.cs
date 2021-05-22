using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaMenu : MonoBehaviour
{
    public Canvas mainCanvas;
    public Canvas controlsCanvas;
    public Canvas howToPlayCanvas;

    public void ControlsToStart()
    {
        controlsCanvas.enabled = false;
        mainCanvas.enabled = true;
    }

    public void HowToToStart()
    {
        howToPlayCanvas.enabled = false;
        mainCanvas.enabled = true;
    }

    public void ToControls()
    {
        mainCanvas.enabled = false;
        controlsCanvas.enabled = true;
    }

    public void ToHowTo()
    {
        mainCanvas.enabled = false;
        howToPlayCanvas.enabled = true;
    }

    private void Start()
    {
        mainCanvas.enabled = true;
        controlsCanvas.enabled = false;
        howToPlayCanvas.enabled = false;
    }
}
