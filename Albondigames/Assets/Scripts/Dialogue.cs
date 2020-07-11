using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] text;
    public float delay = 0;

    public bool autoDialogue;
    public float endDelay = 0.5f;

    public UnityEvent atStartActions;
    public UnityEvent atEndActions;


    DialogueManager myDialogueManager;
    void Start()
    {
        myDialogueManager = GameObject.FindObjectOfType<DialogueManager>();

    }

    public void triggerDialogue()
    {

        //Try to trigger this dialogue
        if (autoDialogue)
        {
            myDialogueManager.triggerDialogue(this);
        }
        else
        {
            myDialogueManager.triggerAutoDialogue(this);
        }

    }

    public void printThis()
    {
        Debug.Log("HOI");
    }

}
