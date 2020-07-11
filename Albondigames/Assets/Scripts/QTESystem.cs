using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QTESystem : MonoBehaviour
{
    public bool busy;

    public void triggerWaitQTE(QTE qte)
    {
        if (busy)
        {

        }
    }

    public void triggerSpamQTE(QTE qte) {
        if (busy)
        {

        }
    }

    public void triggerCoordinateQTE(QTE qte)
    {
        if (busy)
        {

        }
    }


    class ReadyState : IState
    {
        public void enter()
        {
        }

        public IEnumerator Execute()
        {
            yield return null;
        }

        public void exit()
        {
        }
    }
    private interface IState
    {
        void enter();

        void exit();

        IEnumerator Execute();


    }

}
