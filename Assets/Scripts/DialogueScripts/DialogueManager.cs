using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public TMP_Text dialogueText;
    private Queue<string> sentences;
    public GameObject background;
    private string currentSentence;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        background.SetActive(true);
        nameText.text = dialogue.characterName;
        
        sentences.Clear();

        foreach (string words in dialogue.stenences)
        {
            sentences.Enqueue(words);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        currentSentence = sentences.Dequeue();

        dialogueText.text = currentSentence;
    }

    public void EndDialogue()
    {
        background.SetActive(false);
    }
}
