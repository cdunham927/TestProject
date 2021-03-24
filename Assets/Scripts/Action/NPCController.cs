using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    public Dialogue[] dialogues;
    DialogueController dCont;
    public bool hasTalkedTo = false;

    private void Awake()
    {
        dCont = FindObjectOfType<DialogueController>();
    }

    private void OnMouseDown()
    {
        if (!hasTalkedTo)
        {
            dCont.StartDialogue(dialogues[0]);
            hasTalkedTo = true;
        }
        else
        {
            dCont.StartDialogue(dialogues[1]);
        }
    }
}
