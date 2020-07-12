using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraNode : MonoBehaviour
{
    [Range(1, 10)]
    public float gizmosDrawRange;
    public float delay;
    public float timeToGet;

    public UnityEvent atStartEvent;

    public UnityEvent delayEvent;

    public UnityEvent atEndEvent;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, gizmosDrawRange);
    }
}
