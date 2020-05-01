using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTrigger : Trigger
{
    // Start is called before the first frame update
    void Start()
    {
        getDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            triggerDialogue();
        }
    }
}
