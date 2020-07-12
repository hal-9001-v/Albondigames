using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutSceneSystem : MonoBehaviour
{
    public CutSceneNode[] nodes;

    Queue<CutSceneNode> nodeQueue;

    CutSceneNode currentNode;

    Vector2 destination;
    Vector2 startPosition;

    float timeToReachTarget;
    float timeCounter;

    bool arrived;

    Transform target;

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

    IEnumerator StartTransition()
    {
        currentNode.delayEvent.Invoke();

        yield return new WaitForSeconds(currentNode.delay);

        currentNode.atEndEvent.Invoke();

        goToNextNode();
    }


    public void goToNextNode()
    {
        if (nodeQueue.Count != 0)
        {
            arrived = false;
            timeCounter = 0;

            currentNode = nodeQueue.Dequeue();

            startPosition = target.position;
            destination = currentNode.transform.position;

            timeToReachTarget = currentNode.timeToGet;

            currentNode.atStartEvent.Invoke();

        }
        else
        {
            currentNode.atEndEvent.Invoke();
            currentNode = null;
        }

    }
}
