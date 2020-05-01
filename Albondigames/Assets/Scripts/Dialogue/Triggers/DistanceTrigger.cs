using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTrigger : Trigger
{
    public bool Automatic;
    public bool onlyOnce;

    private bool done;

    [Range(0, 100)]
    public float range;

    // Start is called before the first frame update
    void Start()
    {
        getDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (onlyOnce && done) return;

        // Distance Check


        if (Automatic)
            {
                //done = true;
                //return;
            }

            //!Automatic
            if (Input.GetKeyDown(KeyCode.Space))
            {

                
                triggerDialogue();
            }

        
        
    }
}
