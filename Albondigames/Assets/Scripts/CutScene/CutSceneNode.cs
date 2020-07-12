using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutSceneNode : Triggerable
{
    public bool keepY;
    public bool endRight;

    public bool peeAnimation;
    public bool fallAnimation;

    public float timeDoingAnimation;

    [Range(1, 10)]
    public float gizmosDrawRange;
    public float timeToReachTarget;

    public UnityEvent atStartEvent;

    public UnityEvent atEndOfMovementEvent;

    public UnityEvent atEndOfAnimationEvent;

    Transform target;
    PlayerController myPlayerController;

    bool moving;
    bool peeing;
    bool falling;

    float timeCounter = 0;

    Vector2 destination;
    Vector2 startPosition;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, gizmosDrawRange);
    }

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
        myPlayerController = target.gameObject.GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {

        if (peeing || falling)
        {
            timeCounter += Time.deltaTime / timeToReachTarget;

            if (timeCounter > timeDoingAnimation)
            {
                peeing = false;
                falling = false;

                myPlayerController.isPeeing = false;
                myPlayerController.isFalling = false;

                atEndOfAnimationEvent.Invoke();

            }
        }

        if (moving)
        {
            timeCounter += Time.deltaTime / timeToReachTarget;


            if (Vector3.Distance(destination, target.position) > 2)
            {
            
                destination.y = target.position.y;
                startPosition.y = target.position.y;

                

                target.position = Vector2.Lerp(startPosition, destination, timeCounter);

            }
            else
            {
                moving = false;
                myPlayerController.spr.flipX = endRight;

                atEndOfMovementEvent.Invoke();

                if (peeAnimation || fallAnimation)
                    stillAnimation();
            }
        }
    }



    private void Update()
    {
        if (moving)
        {
            myPlayerController.isWalking = true;
            //If Node is Right
            if (transform.position.x > target.position.x)
            {
                myPlayerController.spr.flipX = false;
            }
            else
            {
                myPlayerController.spr.flipX = true;
            }
        }

        if (peeing)
        {
            myPlayerController.isPeeing = true;
        }

        if (falling)
        {
            myPlayerController.isFalling = true;
        }
    }

    public override void trigger()
    {
        atStartEvent.Invoke();
        moving = true;
        startPosition = target.position;

        timeCounter = 0;
        destination = transform.position;




    }

    private void stillAnimation()
    {
        if (peeAnimation)
            peeing = true;


        if (fallAnimation)
            falling = true;

        timeCounter = 0;
    }

}



