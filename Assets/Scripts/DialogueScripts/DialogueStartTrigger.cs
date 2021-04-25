using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStartTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueManager manager;

    private void Start()
    {
        manager.StartDialogue(dialogue);
    }
}
