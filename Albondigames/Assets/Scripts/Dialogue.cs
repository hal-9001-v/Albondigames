using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public string[] text;
    public float delay = 0;

    DialogueManager myDialogueManager;
    void Start()
    {
        myDialogueManager = GameObject.FindObjectOfType<DialogueManager>();
        
    }

    private void triggerDialogue() {
        //Try to trigger this dialogue
        myDialogueManager.triggerAutoDialogue(this);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) {
            triggerDialogue();
        }
    }

}
