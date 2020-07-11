using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Events;

class StartTrigger : Trigger
{
    bool done;

    private void Update()
    {
        if (!done)
        {
            actions.Invoke();
            done = true;
        }

    }
}
