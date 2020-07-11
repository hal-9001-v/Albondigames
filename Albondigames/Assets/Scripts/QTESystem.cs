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


    private void Awake()
    {
        readyState = new ReadyState(this);
        spamState = new SpamState(this);


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

        public void exit()
        {

            mySystem.waitState.enter();
        }

        public void exitToSpam()
        {
            mySystem.spamState.enter();
        }

        public void exitToCoordinate() { 
            mySystem.t
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
        }

        public IEnumerator Execute()
        {
            while(!Input.GetKeyDown(interactionKey))
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
            mySystem.currentQTE.atStartActions.Invoke();

        }

        public IEnumerator Execute()
        {
         //   while (Input.GetKeyDown) { }
            yield return null;
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
