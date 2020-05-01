using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicTrigger : Trigger
{
    // Start is called before the first frame update
    void Start()
    {
        getDialogue();
    }

    public void triggerThisDialogue() {
        triggerDialogue();
    }
}
