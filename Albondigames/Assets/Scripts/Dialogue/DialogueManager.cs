using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    bool busy;
    bool lineHasEnded = false;
    bool simpleDialogue;
    bool waitingForAnswer;

    float dialogueDelay;

    string currentLine;

    Queue<string> sentences;
    TMP_Text text;
    UnityEvent endingEvent;

    public GameObject nameObject;
    TMP_Text nameText;

    public GameObject answer1Object;
    TMP_Text answer1Text;

    public GameObject answer2Object;
    TMP_Text answer2Text;

    public GameObject answer3Object;
    TMP_Text answer3Text;

    int answerSelection = 0;

    Queue<Question> questionQueue;
    Question currentQuestion;
    int questionNumber = 0;

    private void Awake()
    {
        sentences = new Queue<string>();
        questionQueue = new Queue<Question>();

        text = GetComponent<TMP_Text>();
        nameText = nameObject.GetComponent<TMP_Text>();

        answer1Text = answer1Object.GetComponent<TMP_Text>();
        answer2Text = answer2Object.GetComponent<TMP_Text>();
        answer3Text = answer3Object.GetComponent<TMP_Text>();

        hideTextBox();
    }

    public void startDialogue(Dialogue dialogue)
    {
        if (dialogue.GetType().Equals(typeof(DialogueInteractable)))
        {
            Debug.Log("Starting Interactable Dialogue");
            startInteractableDialogue((DialogueInteractable)dialogue);
        }
        else if (dialogue.GetType().Equals(typeof(Dialogue)))
        {
            Debug.Log("Starting simple dialogue");
            startSimpleDialogue(dialogue);
        }

    }

    private void startSimpleDialogue(Dialogue dialogue)
    {
        Debug.Log(busy);
        //No dialogue is enable
        if (!busy)
        {

            simpleDialogue = true;

            Debug.Log("New Dialogue with: " + dialogue.name);

            //Dialogue is enable
            busy = true;

            nameText.text = dialogue.name;

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

    void startInteractableDialogue(DialogueInteractable dialogue)
    {
        //No dialogue is enable
        if (!busy)
        {
            StopAllCoroutines();

            simpleDialogue = false;

            Debug.Log("New Dialogue with: " + dialogue.name);

            //Dialogue is enable
            busy = true;

            nameText.text = dialogue.name;

            displayTextBox();

            //Invoke events from dialogue script
            dialogue.atStartOfDialogue.Invoke();
            endingEvent = dialogue.atEndOfDialogue;

            //Delay between each letter
            dialogueDelay = dialogue.delay;

            //Remove old sentences
            sentences.Clear();

            questionQueue.Clear();
            foreach (Question q in dialogue.questions)
            {
                questionQueue.Enqueue(q);
            }

            currentQuestion = questionQueue.Dequeue();
            //Load new sentences into Queue from first question
            foreach (string line in currentQuestion.lines)
            {
                sentences.Enqueue(line);
            }

            loadNextSentence();
        }
    }


    private void Update()
    {
        if (busy)
        {
            if (simpleDialogue)
                simpleDialogueLoop();
            else
            {
                interactableDialogueLoop();
            }
        }
    }

    void simpleDialogueLoop()
    {
        if (Input.GetKeyDown(KeyCode.Z))
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

                //No dialogue is active now
                busy = false;

                hideTextBox();

                //Do Dialogue end events
                endingEvent.Invoke();
            }
        }
    }

    void interactableDialogueLoop()
    {
        if (waitingForAnswer)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                answerSelection--;
                if (answerSelection == -1)
                {
                    answerSelection = questionNumber - 1;
                }

            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                answerSelection++;
                if (answerSelection == 3)
                {
                    answerSelection = 0;
                }

            }
            showAnswerTexts();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            StopAllCoroutines();

            if (!lineHasEnded)
            {
                //Fill Question
                lineHasEnded = true;
                text.text = currentLine;

                return;
            }

            if (sentences.Count != 0)
            {
                loadNextSentence();

                return;
            }

            //Show All Answers
            if (!waitingForAnswer)
            {
                showAnswers();
            }
            else
            {
                busy = false;

                hideTextBox();

                waitingForAnswer = false;

                //Make Comparison and load next question
                currentQuestion.comparator.compare(answerSelection);

            }

        }
    }

    void showAnswers()
    {
        questionNumber = 0;
        waitingForAnswer = true;

        //Show Answers
        answer1Text.text = currentQuestion.options[0];
        questionNumber++;

        if (currentQuestion.options[1] != null)
        {
            questionNumber++;
            answer2Text.text = currentQuestion.options[1];
        }
        else
        {
            answer2Text.text = "";
        }

        if (currentQuestion.options[2] != null)
        {
            questionNumber++;
            answer3Text.text = currentQuestion.options[2];
        }
        else
        {
            answer3Text.text = "";
        }
    }

    void loadNextSentence()
    {
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

    void displayTextBox()
    {
        Debug.Log("Showing dialogue");
        GetComponent<CanvasGroup>().alpha = 1;
    }

    void hideTextBox()
    {
        Debug.Log("Hiding dialogue");

        GetComponent<CanvasGroup>().alpha = 0;

        answer1Text.alpha = 0;
        answer2Text.alpha = 0;
        answer3Text.alpha = 0;

    }

    void showAnswerTexts()
    {
        if (answerSelection == 0)
        {
            answer1Text.alpha = 1;
        }
        else
        {
            answer1Text.alpha = 0.5f;
        }

        if (answerSelection == 1)
        {
            answer2Text.alpha = 1;
        }
        else
        {
            answer2Text.alpha = 0.5f;
        }

        if (answerSelection == 2)
        {
            answer3Text.alpha = 1;
        }
        else
        {
            answer3Text.alpha = 0.5f;
        }
    }

}

