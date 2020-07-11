using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QTE : Triggerable
{
    public bool waitQTE;

    public bool spamQTE;
    public int haveToPress;

    public bool coordinateQTE;

    public float avaliableTime;

    public KeyCode interactionKey;

    private QTESystem mySystem;

    public UnityEvent atStartActions;
    public UnityEvent atEndActions;
    public UnityEvent atFailureActions;

    private void Awake()
    {
        mySystem = GameObject.FindObjectOfType<QTESystem>();

    }

    public override void trigger()
    {
        if (waitQTE)
        {
            mySystem.triggerWaitQTE(this);
            return;
        }

        if (spamQTE)
        {
            mySystem.triggerSpamQTE(this);
            return;
        }

        if (coordinateQTE)
        {
            mySystem.triggerCoordinateQTE(this);
            return;
        }

    }
}
