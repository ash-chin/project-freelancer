using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunDialogue : MonoBehaviour
{
    public Transform player;
    public Player_Asset_Manager assetManager;
    public Dialogue dialogue;
    public DialogueManager manager;
    private bool firstTime = true;

    private void OnTriggerEnter(Collider other)
    {
        if (firstTime && other.tag == "Player")
        {
            manager.StartDialogue(dialogue);
        }
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 1500)
        {
            assetManager.VariableDamage(10 * Time.deltaTime);
        }
    }
}
