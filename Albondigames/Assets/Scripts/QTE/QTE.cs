﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QTE : MonoBehaviour
{
    public bool waitQTE;

    public bool spamQTE;
    public int timesPressed;

    public bool coordinateQTE;

    public float timeAvaliable;

    public KeyCode interactionKey;

    private QTESystem mySystem;

    public UnityEvent atStartActions;
    public UnityEvent atEndActions;
    public UnityEvent atFailureActions;

    private void Start()
    {
        mySystem = GameObject.FindObjectOfType<QTESystem>();
        
    }

    public void triggerQTE() {
        if (waitQTE) {
            mySystem.triggerWaitQTE(this);
            return;
        }

        if (spamQTE) {
            mySystem.triggerspamQTE(this);
            return;
        }

        if (coordinateQTE) {
            mySystem.triggerCoordinateQTE(this);
            return;
        }
    }



   


}
