using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhalePortalEmerge : MonoBehaviour
{
    // how much time the portal exists for before the whale emerges
    public float pretime;
    // how long the portal lasts for
    public float portalEndurance;
    // the whale object;
    public GameObject whale;
    // the portal object;
    public GameObject portal;
    // the timer being updated;
    private float timer;


    private void Start()
    {
        whale.SetActive(false);
        portal.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            portal.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        if (portal.activeInHierarchy == true)
        {
            timer += Time.deltaTime;

            if (timer >= pretime)
            {
                whale.SetActive(true);
                if (timer >= portalEndurance)
                {
                    portal.SetActive(false);
                    this.enabled = false;
                }
            }
        }
    }
}
