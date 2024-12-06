using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    protected Dialogue myDialogue;

    protected void getDialogue()
    {
        myDialogue = GetComponent<Dialogue>();
    }

    protected void triggerDialogue() {
        FindObjectOfType<DialogueManager>().startDialogue(myDialogue);
    }

}
