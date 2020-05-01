using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    bool busy;
    bool lineHasEnded = false;

    float dialogueDelay;

    string currentLine;

    Queue<string> sentences;
    TMP_Text text;
    UnityEvent endingEvent;

    private void Awake()
    {
        sentences = new Queue<string>();

        text = GetComponent<TMP_Text>();

        enabled = false;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //No dialogue is enable
        if (!busy)
        {
            //Dialogue is enable
            busy = true;

            displayTextBox();

            //Invoke events from dialogue script
            dialogue.atStartOfDialogue.Invoke();
            endingEvent = dialogue.atEndOfDialogue;

            //Delay between each letter
            dialogueDelay = dialogue.delay;

            //Remove old sentences
            sentences.Clear();

            //Load new sentences into Queue
            foreach (string line in dialogue.lines)
            {
                sentences.Enqueue(line);
            }

            loadNextSentence();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            updateDialogue();
        }
    }

    private void updateDialogue()
    {
        //Stop last coroutine 
        StopAllCoroutines();


        if (!lineHasEnded)
        {
            //Make sentence print at once
            lineHasEnded = true;
            text.text = currentLine;

            return;
        }

        //If there are senteces left
        if (sentences.Count != 0)
        {
            //Start writing next sentence
            loadNextSentence();

        }
        else
        {//No more lines here, end of dialogue
            Debug.Log("End of Dialogue");

            //Do Dialogue end events
            endingEvent.Invoke();

            hideTextBox();

            //No dialogue is active now
            busy = false;
        }
    }

    void loadNextSentence() {
        //Start of sentence
        lineHasEnded = false;

        Debug.Log("Next Sentence");
        currentLine = sentences.Dequeue();

        StopAllCoroutines();

        //Start printing new sentence
        StartCoroutine(typeNextSentence(currentLine));

    }

    IEnumerator typeNextSentence(string line)
    {

        text.text = "";

        //Print a char every frame
        foreach (char c in line.ToCharArray())
        {
            text.text += c;

            //Wait this time every frame
            yield return new WaitForSeconds(dialogueDelay);
        }

        //Line is printed
        lineHasEnded = true;

        yield return 0;
    }

    void displayTextBox() {
        enabled = true;
    }

    void hideTextBox() {
        enabled = false;
    }
}
