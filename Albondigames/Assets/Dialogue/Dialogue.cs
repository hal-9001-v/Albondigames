using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour
{
    public float delay;
    public string name;
    public string[] lines;

    public UnityEvent atStartOfDialogue;
    public UnityEvent atEndOfDialogue;

    private void Start()
    {
        if (GetComponent<Trigger>() == null) {
            Debug.LogError("No Trigger on "+gameObject.name);
        }
    }

}
