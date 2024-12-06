using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GamePX;


public class DialogueInteractable : Dialogue
{
    public Question[] questions;

    private void Start()
    {
        if (GetComponent<Trigger>() == null) {
            Debug.LogError("No Trigger on "+gameObject.name);
        }  
    }
}
[System.Serializable]
public class AnswerComparator {
    public string dialogueName;
    public int value;
    public UnityEvent atSuccess;
    public UnityEvent atFailure;

    
    public void compare (int answer){

        if (answer == value)
        {
            Debug.Log("Success at Comparison: " + dialogueName);
            atSuccess.Invoke();
        }
        else {
            Debug.Log("Failure at Comparison: "+dialogueName);
            atFailure.Invoke();
        }
}
    }

[System.Serializable]
public class Question {
    public string[] lines;
    public string[] options;
    public AnswerComparator comparator;
} 