using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelMessage : MonoBehaviour
{
    public GameObject instructionBackground;
    public Text instructionText;
    private float timer = 0;
    private bool activated = false;

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("This should occur at least once");
        if (other.tag == "Player")
        {

            instructionBackground.SetActive(true);
            instructionText.enabled = true;
            instructionText.text = "Great! Now that you've refueled it's time to track down a bounty. \n" +
                "There should be a small sun sinking towards the wormhole!";
            activated = true;
            
        }
    }

    private void FixedUpdate()
    {
        if (activated)
        {
            timer += Time.deltaTime;
        }
        if (timer >= 7f)
        {
            instructionBackground.SetActive(false);
            instructionText.enabled = false;
            this.enabled = false;
        }
    }
}
