using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject myTextObject;
    private TextMeshProUGUI myText;

    private Queue<string> linesQueue;
    private string currentLine;

    private float delay = 0;
    public bool free = false;

    public HiddenState hiddenState;
    public TypeState typeState;
    public IdleState idleState;

    private Coroutine typingChild;
    private bool lineIsCompleted;

    void Awake()
    {
        //Initializing
        myText = myTextObject.GetComponent<TextMeshProUGUI>();
        linesQueue = new Queue<string>();

        //Creating States
        hiddenState = new HiddenState(this);
        typeState = new TypeState(this);
        idleState = new IdleState(this);


    }

    private void Start()
    {
        hiddenState.enter();
    }

    public void triggerDialogue(Dialogue dialogue)
    {
        if (free)
        {
            foreach (string line in dialogue.text)
            {
                linesQueue.Enqueue(line);
            }
            delay = dialogue.delay;

            hiddenState.exit();
        }
    }

    public void showText()
    {
        myText.enabled = true;
    }

    public void hideText()
    {
        myText.enabled = false;
    }

    public bool isEmpty()
    {
        return linesQueue.Count == 0;
    }
    public void nextSentence()
    {
        typingChild = StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        currentLine = linesQueue.Dequeue();

        lineIsCompleted = false;

        myText.text = "";

        foreach (char c in currentLine.ToCharArray())
        {
            myText.text += c;

            yield return new WaitForSeconds(delay);
        }

        lineIsCompleted = true;


        yield return null;
    }

    public void loadCurrentLine()
    {
        myText.text = currentLine;
        StopCoroutine(typingChild);
    }

    public bool isEndOfLine()
    {
        return lineIsCompleted;
    }


    public class HiddenState : IState
    {
        DialogueManager dm;

        public HiddenState(DialogueManager dm)
        {
            this.dm = dm;
        }

        //Hide textBox
        public void enter()
        {
            dm.free = true;
            dm.hideText();
        }

        //No execute is needed
        public IEnumerator Execute()
        {
            yield return null;
        }

        //Display textBox and go to Type State
        public void exit()
        {
            dm.free = false;
            dm.showText();
            dm.typeState.enter();

        }
    }
    public class TypeState : IState
    {
        DialogueManager dm;

        public TypeState(DialogueManager dm)
        {
            this.dm = dm;
        }

        //Start typing
        public void enter()
        {
            dm.nextSentence();

            dm.StartCoroutine(Execute());
        }

        //Check for input so full sentence will appear.
        public IEnumerator Execute()
        {
            yield return null;

            while (!Input.GetKeyDown(KeyCode.Z))
            {
                if (dm.isEndOfLine()) {
                    break;
                }
                yield return null;
            }

            dm.loadCurrentLine();
            exit();
        }

        public void exit()
        {
            dm.idleState.enter();
        }
    }

    public class IdleState : IState
    {
        DialogueManager dm;

        public IdleState(DialogueManager dm)
        {
            this.dm = dm;
        }

        //Start Looking for input
        public void enter()
        {
            Debug.Log("ENTERING IDLE");
            dm.StartCoroutine(Execute());

        }

        //Check for input so a new line will appear or dialogue will end
        public IEnumerator Execute()
        {
            yield return null;

            while (!Input.GetKeyDown(KeyCode.Z))
            {
                yield return null;
            }
            exit();

        }

        //Go to Hidden if no more lines exist or type a new line
        public void exit()
        {
            if (dm.isEmpty())
                dm.hiddenState.enter();
            else
                dm.typeState.enter();
        }
    }

    private interface IState
    {
        void enter();

        void exit();

        IEnumerator Execute();


    }
}
