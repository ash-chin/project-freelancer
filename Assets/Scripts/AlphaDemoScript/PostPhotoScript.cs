using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostPhotoScript : MonoBehaviour
{
    public GameObject instructionBackground;
    public Text instructionText;
    public Player_Space_Ship_Movement player;
    private bool achieved = false;
    private float timer = 0;

    private void Display()
    {
        instructionBackground.SetActive(true);
        instructionText.enabled = true;
        instructionText.text = "Good work freelancer! It seems like you've collected everything there is in this sector. " +
            "You should take that portal over there to the rest of the game!";
    }

    private void FixedUpdate()
    {
        if (player.money >= 200)
        {
            achieved = true;
        }

        if (achieved)
        {
            timer += Time.deltaTime;
        }

        if (timer >= 7f)
        {
            instructionText.enabled = false;
            instructionBackground.SetActive(false);
            this.enabled = false;
        }
    }
}
