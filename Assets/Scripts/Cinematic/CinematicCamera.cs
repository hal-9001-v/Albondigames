using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CinematicCamera : MonoBehaviour
{
    public CameraNode[] nodes;

    Queue<CameraNode> nodeQueue;

    CameraNode currentNode;

    public SpriteRenderer sp;

    Vector3 destination;
    Vector3 startPosition;

    float timeToReachTarget;
    float timeCounter;

    bool arrived;

    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {

        nodeQueue = new Queue<CameraNode>();

        foreach (CameraNode node in nodes)
        {
            nodeQueue.Enqueue(node);
        }

        goToNextNode();

        //graphic.CrossFadeAlpha();
    }

    private void FixedUpdate()
    {
        timeCounter += Time.deltaTime / timeToReachTarget;

        if (currentNode != null)
        {
            if (Vector3.Distance(destination, transform.position) < 0.05 && !arrived)
            {
                arrived = true;
                StartCoroutine(StartTransition());
            }
            else
            {
                transform.position = Vector3.Lerp(startPosition, destination, timeCounter);
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

    IEnumerator FadeOutEffect(float wait) {
        Color operationColor = new Color(0.05f, 0.05f, 0.05f, 0f);

        for (int i = 0; i < 20; i++)
        {
            sp.color -= operationColor;
            yield return new WaitForSeconds(wait);
        }
    }

    public void showImage(float time) {
        StartCoroutine(ShowImage(time));
    }

    IEnumerator ShowImage(float time) {
        yield return new WaitForSeconds(time);

        sp.color = new Color(1, 1, 1, 1);
    }

    IEnumerator FadeInEffect(float wait)
    {
        Color operationColor = new Color(0.05f, 0.05f, 0.05f, 0f);
        sp.color = new Color(0f,0f,0f,1f);

        for (int i = 0; i < 20; i++)
        {
            sp.color += operationColor;
            yield return new WaitForSeconds(wait);
        }
    }


    public void fadeOut(float seconds) {
        StartCoroutine(FadeOutEffect(seconds));
    }


    public void fadeIn(float seconds)
    {
        StartCoroutine(FadeInEffect(seconds));
    }

    public void goToNextNode()
    {
        if (nodeQueue.Count != 0)
        {
            arrived = false;
            timeCounter = 0;

            currentNode = nodeQueue.Dequeue();

            startPosition = transform.position;
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

    public void startGif(float time) {
        StartCoroutine(StartGif(time));
    }

    IEnumerator StartGif(float time) {
        while (1 == 1) { 
            sp.sprite = sprites[0];

            yield return new WaitForSeconds(time);

            sp.sprite = sprites[1];

            yield return new WaitForSeconds(time);

        }
    }

    public void nextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    

}
