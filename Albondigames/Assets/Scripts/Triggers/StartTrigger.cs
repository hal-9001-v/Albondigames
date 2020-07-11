using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Events;

class StartTrigger : Trigger
{

    Triggerable t;
    bool done;

    private void Start()
    {
        t = GetComponent<Triggerable>();
    }

    private void Update()
    {
        if (!done)
        {
            t.trigger();
            done = true;
        }

    }
}
