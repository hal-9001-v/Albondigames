using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QTESystem : MonoBehaviour
{
    public bool busy;
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


    }

    public void triggerWaitQTE(QTE qte)
    {
        if (busy)
        {
            currentQTE = qte;
            readyState.setKey(qte.interactionKey);
            readyState.exit();


        }
    }

    public void triggerSpamQTE(QTE qte)
    {
        if (busy)
        {
            currentQTE = qte;
            readyState.setKey(qte.interactionKey);
            readyState.exitToSpam();


        }
    }

    public void triggerCoordinateQTE(QTE qte)
    {
        if (busy)
        {
            currentQTE = qte;
            readyState.setKey(qte.interactionKey);
            readyState.exitToCoordinate();
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

        }

        public IEnumerator Execute()
        {
            yield return null;
        }

        //ExitToWaiT
        public void exit()
        {
            mySystem.waitState.setKey(interactionKey);
            mySystem.waitState.enter();

        }

        public void exitToSpam()
        {
            mySystem.spamState.setKey(interactionKey);
            mySystem.spamState.enter();
        }

        public void exitToCoordinate()
        {
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
            while (!Input.GetKeyDown(interactionKey))
                yield return null;
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
            mySystem.spamState.enter();
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
