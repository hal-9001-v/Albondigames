using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public string text;
    public int delay = 0;

    DialogueManager myDialogueManager;
    void Start()
    {
        myDialogueManager = GameObject.FindObjectOfType<DialogueManager>();
        triggerDialogue();
    }

    private void triggerDialogue() {
        //Try to trigger this dialogue
        myDialogueManager.triggerDialogue(this);
        
    }

}
