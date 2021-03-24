using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public Text npcNameUI;
    public Text dialogueUI;
    public GameObject uiParent;
    [SerializeField]
    int currentIndex = 0;

    Dialogue currentDialogue;

    public void StartDialogue(Dialogue d)
    {
        currentDialogue = d;
        uiParent.SetActive(true);
        currentIndex = 0;
        npcNameUI.text = currentDialogue.npcName;
        dialogueUI.text = currentDialogue.dialogue[currentIndex];
    }

    public void NextLine()
    {
        if (currentIndex == currentDialogue.dialogue.Length - 1) ExitDialogue();
        else
        {
            currentIndex++;
            dialogueUI.text = currentDialogue.dialogue[currentIndex];
            npcNameUI.text = currentDialogue.npcName;
        }
    }

    public void ExitDialogue()
    {
        dialogueUI.text = "";
        npcNameUI.text = "";
        uiParent.SetActive(false);
        currentIndex = 0;
    }
}
