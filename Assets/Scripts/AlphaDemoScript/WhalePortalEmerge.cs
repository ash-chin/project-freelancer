using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhalePortalEmerge : MonoBehaviour
{
    // the whale which will be activated when the player 
    public WhaleMovement whale;
    // this determines how long the portal lasts
    public float portalDuration;
    // this is the timer used to track duration
    private float timer = 0;
    // bool used to alternate timer;
    private bool playerHasEntered;
    // the portal it emerges from;
    public GameObject portal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            whale.enabled = true;
            playerHasEntered = true;
        }
    }

    private void Start()
    {
        timer = 0;
        whale.enabled = false;
        playerHasEntered = false;
    }

    private void FixedUpdate()
    {
        if (playerHasEntered)
        {
            timer += Time.deltaTime;
        }

        if (timer >= portalDuration)
        {
            portal.SetActive(false);
            this.enabled = false;
        }
    }
}
