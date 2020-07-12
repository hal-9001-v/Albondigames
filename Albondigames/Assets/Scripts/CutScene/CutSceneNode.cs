using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutSceneNode : MonoBehaviour
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


    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;

        nodeQueue = new Queue<CutSceneNode>();

        foreach (CutSceneNode node in nodes)
        {
            nodeQueue.Enqueue(node);
        }

        goToNextNode();

    }

    private void FixedUpdate()
    {
        if (currentNode != null)
        {
            timeCounter += Time.deltaTime / timeToReachTarget;

            if (Vector3.Distance(destination, target.position) < 0.05 && !arrived)
            {
                arrived = true;
                StartCoroutine(StartTransition());
            }
            else
            {
                target.position = Vector3.Lerp(startPosition, destination, timeCounter);
            }
        }
    }
