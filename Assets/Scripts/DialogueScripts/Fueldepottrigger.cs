using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fueldepottrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            manager.StartDialogue(dialogue);
            this.enabled = false;
        }
    }
}
