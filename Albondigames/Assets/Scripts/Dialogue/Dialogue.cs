using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GamePX;

public class Dialogue : MonoBehaviour
{

    public float delay;
    public string name;
    public string[] lines;

    public UnityEvent atStartOfDialogue;
    public UnityEvent atEndOfDialogue;


}
