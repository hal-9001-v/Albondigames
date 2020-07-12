using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraZoomNode : MonoBehaviour
{
    public float zoom;
    public float delay;
    public float timeToGet;

    public UnityEvent atStartEvent;

    public UnityEvent delayEvent;

    public UnityEvent atEndEvent;

}
