using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dialogue : Triggerable
{
    public string speaker;
    [TextArea(3, 10)]
    public string[] text;
    public float delay = 0;

    public bool autoDialogue;
    public bool locked;
    public float endDelay = 0.5f;

    public UnityEvent atStartActions;
    public UnityEvent atEndActions;


    DialogueManager myDialogueManager;
    void Awake()
    {
        myDialogueManager = GameObject.FindObjectOfType<DialogueManager>();

    }

    public override void trigger()
    {
        //Try to trigger this dialogue
        if (!autoDialogue)
        {
            myDialogueManager.triggerDialogue(this);
        }
        else
        {
            myDialogueManager.triggerAutoDialogue(this);
        }

    }
}
