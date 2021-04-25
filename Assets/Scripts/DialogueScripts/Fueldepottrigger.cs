using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fueldepottrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueManager manager;
    private bool firstTime = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && firstTime == true)
        {
            manager.StartDialogue(dialogue);
            firstTime = false;
        }
    }
}
