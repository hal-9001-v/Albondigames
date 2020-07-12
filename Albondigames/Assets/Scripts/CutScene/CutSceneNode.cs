using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;

public class CutSceneNode : Triggerable
{
    public bool keepY;
    public bool endRight;

    public bool peeAnimation;
    public bool lieAnimation;
    public bool getUpAnimation;

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
    bool lying;
    bool gettingUp;

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

        if (peeing || lying || gettingUp)
        {
            timeCounter += Time.deltaTime / timeToReachTarget;

            if (timeCounter > timeDoingAnimation)
            {
                peeing = false;
                lying = false;

                myPlayerController.isPeeing = false;
                myPlayerController.isLieing = false;
                
                myPlayerController.isGettingUp = false;

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

                if (peeAnimation || lieAnimation || getUpAnimation)
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

        if (lying)
        {
            myPlayerController.isLieing = true;
            myPlayerController.isGrounded = true;
            myPlayerController.isClimbing = false;

            Animator a = myPlayerController.gameObject.GetComponent<Animator>();

            a.SetBool("Lieing", true);
            a.SetBool("Grounded",true);//ç
            a.SetBool("Climbing", false);

        }

        if (gettingUp) {
            myPlayerController.isGettingUp = true;
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


        if (lieAnimation)
            lying = true;

        if (getUpAnimation)
            gettingUp = true;


        timeCounter = 0;
    }

}



