using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Events;
using UnityEngine;

class Trigger : MonoBehaviour
{
    public UnityEvent actions;

    private void triggerActions()
    {
        actions.Invoke();
    }
}
