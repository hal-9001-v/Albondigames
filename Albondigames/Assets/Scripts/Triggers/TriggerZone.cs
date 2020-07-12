using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    public bool oneUse = false;
    public UnityEvent onEnter;
    public UnityEvent onExit;

    private bool used = false;

void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            if (!used)
            {
                if (oneUse) used = true;
                onEnter.Invoke();
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player")) 
        {
            onExit.Invoke();
            if(oneUse) Destroy(this.gameObject);
        }
    }
}
