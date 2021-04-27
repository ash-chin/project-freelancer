using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fueldepottrigger : MonoBehaviour
{
    public BoxCollider colider;
    public Dialogue dialogue;
    public DialogueManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            manager.StartDialogue(dialogue);
            colider.enabled = false;
        }
    }
}
