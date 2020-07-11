using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QTESystem : MonoBehaviour
{
    [HideInInspector]
    public bool free;
    [HideInInspector]
    public QTE currentQTE;

    public ReadyState readyState;
    public WaitState waitState;
    public SpamState spamState;
    public CoordinateState coordinateState;


    private void Awake()
    {
        readyState = new ReadyState(this);
        spamState = new SpamState(this);
        waitState = new WaitState(this);
        coordinateState = new CoordinateState(this);

        readyState.enter();

    }

    public void triggerWaitQTE(QTE qte)
    {
        if (free)
        {
            currentQTE = qte;
            readyState.setKey(qte.interactionKey);
            readyState.exit();


            Debug.Log("Starting QTE of wait");
        }
    }

    public void triggerSpamQTE(QTE qte)
    {
        if (free)
        {
            currentQTE = qte;
            readyState.setKey(qte.interactionKey);
            readyState.exitToSpam();


            Debug.Log("Starting QTE of Spam");
        }
    }

    public void triggerCoordinateQTE(QTE qte)
    {
        if (free)
        {
            currentQTE = qte;
            readyState.setKey(qte.interactionKey);
            readyState.exitToCoordinate();

            Debug.Log("Starting QTE of coordination");
        }
    }


    public class ReadyState : IState
    {
        QTESystem mySystem;
        KeyCode interactionKey;

        public void setKey(KeyCode key)
        {
            interactionKey = key;
        }

        public ReadyState(QTESystem mySystem)
        {
            this.mySystem = mySystem;
        }

        public void enter()
        {
            mySystem.free = true;
        }

        public IEnumerator Execute()
        {
            yield return null;
        }

        //ExitToWaiT
        public void exit()
        {
            mySystem.free = false;
            mySystem.waitState.setKey(interactionKey);
            mySystem.waitState.enter();

        }

        public void exitToSpam()
        {
            mySystem.free = false;
            mySystem.spamState.setKey(interactionKey);
            mySystem.spamState.enter();
        }

        public void exitToCoordinate()
        {
            mySystem.free = false;
            mySystem.spamState.setKey(interactionKey);
            mySystem.coordinateState.enter();
        }


    }


    public class WaitState : IState
    {
        QTESystem mySystem;
        KeyCode interactionKey;

        public void setKey(KeyCode key)
        {
            interactionKey = key;
        }

        public WaitState(QTESystem mySystem)
        {
            this.mySystem = mySystem;
        }

        public void enter()
        {
            mySystem.StartCoroutine(Execute());
            mySystem.currentQTE.atStartActions.Invoke();
        }

        public IEnumerator Execute()
        {
            Debug.Log("Need to press " + interactionKey.ToString());
            while (!Input.GetKeyDown(interactionKey))
                yield return null;
            exit();
        }

        public void exit()
        {
            mySystem.currentQTE.atEndActions.Invoke();
            mySystem.readyState.enter();

        }

    }

    public class SpamState : IState
    {
        QTESystem mySystem;
        KeyCode interactionKey;

        int timesPressed;
        float timeAvaliable;

        public SpamState(QTESystem mySystem)
        {
            this.mySystem = mySystem;
        }

        public void setKey(KeyCode key)
        {
            interactionKey = key;
        }

        public void enter()
        {
            mySystem.StartCoroutine(Execute());

            //How many times to press
            timesPressed = mySystem.currentQTE.timesPressed;

            //Time to press enogugh times
            timeAvaliable = mySystem.currentQTE.timeAvaliable;

            mySystem.currentQTE.atStartActions.Invoke();

        }

        public IEnumerator Execute()
        {

            Debug.Log("Need to SPAM " + interactionKey.ToString());
            int pressedCounter = 0;
            float timePassed = 0;
            while (timesPressed < pressedCounter)
            {
                if (Input.GetKeyDown(interactionKey))
                {
                    pressedCounter++;
                }

                timePassed += Time.deltaTime;

                if (timePassed > timeAvaliable)
                {
                    failureExit();
                }


                yield return null;
            }

            exit();

        }

        public void exit()
        {
            mySystem.readyState.enter();
            mySystem.currentQTE.atEndActions.Invoke();

        }

        public void failureExit()
        {
            mySystem.readyState.enter();
            mySystem.currentQTE.atFailureActions.Invoke();

        }


    }

    public class CoordinateState : IState
    {
        QTESystem mySystem;
        KeyCode interactionKey;

        public void setKey(KeyCode key)
        {
            interactionKey = key;
        }

        public CoordinateState(QTESystem mySystem)
        {
            this.mySystem = mySystem;
        }

        public void enter()
        {

        }

        public IEnumerator Execute()
        {
            yield return null;
        }

        public void exit()
        {

            mySystem.waitState.enter();
        }

        public void exitToSpam()
        {
            mySystem.readyState.enter();
        }


    }

    private interface IState
    {
        void setKey(KeyCode key);

        void enter();

        void exit();

        IEnumerator Execute();


    }

}
