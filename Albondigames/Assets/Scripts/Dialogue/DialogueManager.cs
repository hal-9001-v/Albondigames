using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    //TECLA CON LA QUE INTERACTUAR
    private KeyCode interactionKey = KeyCode.Z;

    private TextMeshProUGUI myNameText;
    public GameObject myNameObject;

    private TextMeshProUGUI myText;
    public GameObject myTextObject;

    public GameObject myImageObject;
    private SpriteRenderer myRawImage;

    private Queue<string> linesQueue;
    private string currentLine;

    private float delay = 0;

    [HideInInspector]
    public Dialogue currentDialogue;

    [HideInInspector]
    public bool free = false;

    public HiddenState hiddenState;
    public TypeState typeState;
    public IdleState idleState;

    public AutoTypeState autoTypeState;
    public TimedIdleState timedIdleState;

    private Coroutine typingChild;
    private bool lineIsCompleted;


    void Awake()
    {
        //Initializing
        myText = myTextObject.GetComponent<TextMeshProUGUI>();
        myRawImage = myImageObject.GetComponent<SpriteRenderer>();
        myNameText = myNameObject.GetComponent<TextMeshProUGUI>();

        linesQueue = new Queue<string>();

        //Creating States
        hiddenState = new HiddenState(this);
        typeState = new TypeState(this);
        idleState = new IdleState(this);

        autoTypeState = new AutoTypeState(this);
        timedIdleState = new TimedIdleState(this);


    }

    private void Start()
    {
        hiddenState.enter();
    }

    public void triggerDialogue(Dialogue dialogue)
    {
        if (free || currentDialogue.locked)
        {
            myNameText.text = dialogue.speaker;

            currentDialogue = dialogue;

            foreach (string line in dialogue.text)
            {
                linesQueue.Enqueue(line);
            }
            delay = dialogue.delay;

            hiddenState.exit();
        }
    }

    public void triggerAutoDialogue(Dialogue dialogue)
    {
        if (free || currentDialogue.locked)
        {
            myNameText.text = dialogue.speaker;

            currentDialogue = dialogue;

            timedIdleState.delayTime = dialogue.endDelay;

            foreach (string line in dialogue.text)
            {
                linesQueue.Enqueue(line);
            }
            delay = dialogue.delay;

            hiddenState.exitToAuto();
        }
    }

    public void showText()
    {
        myNameText.enabled = true;
        myText.enabled = true;
        myRawImage.enabled = true;
    }

    public void hideText()
    {
        myNameText.enabled = false;
        myText.enabled = false;
        myRawImage.enabled = false;
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
        //Letters to pass before it sounds again
        int PERIOD = 5;
        int counter = 0;

        currentLine = linesQueue.Dequeue();

        lineIsCompleted = false;

        myText.text = "";

        foreach (char c in currentLine.ToCharArray())
        {
            myText.text += c;


            yield return new WaitForSeconds(delay);

            if (counter == PERIOD)
            {
                //SONIDO
            }
            counter++;
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

            if (dm.currentDialogue != null)
            {
                dm.currentDialogue.atEndActions.Invoke();
            }
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
            dm.currentDialogue.atStartActions.Invoke();
            dm.typeState.enter();

        }

        public void exitToAuto()
        {
            dm.free = false;
            dm.showText();
            dm.currentDialogue.atStartActions.Invoke();
            dm.autoTypeState.enter();

        }
    }
    public class TypeState : IState
    {
        DialogueManager dm;
        KeyCode interactionKey;

        public TypeState(DialogueManager dm)
        {
            this.dm = dm;
            this.interactionKey = dm.interactionKey;
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

            while (!Input.GetKeyDown(interactionKey))
            {
                if (dm.isEndOfLine())
                {
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


    public class AutoTypeState : IState
    {
        DialogueManager dm;

        public AutoTypeState(DialogueManager dm)
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

            while (!dm.isEndOfLine())
            {
                yield return null;
            }

            exit();
        }

        public void exit()
        {
            dm.timedIdleState.enter();
        }
    }


    public class IdleState : IState
    {
        DialogueManager dm;
        KeyCode interactionKey;

        public IdleState(DialogueManager dm)
        {
            this.dm = dm;
            this.interactionKey = dm.interactionKey;

        }

        //Start Looking for input
        public void enter()
        {
            dm.StartCoroutine(Execute());

        }

        //Check for input so a new line will appear or dialogue will end
        public IEnumerator Execute()
        {
            yield return null;

            while (!Input.GetKeyDown(interactionKey))
            {
                yield return null;
            }
            exit();

        }

        //Go to Hidden if no more lines exist or type a new line
        public void exit()
        {
            if (dm.isEmpty())
            {
                if (!dm.currentDialogue.locked)
                {
                    dm.hiddenState.enter();

                }
                else
                {
                    dm.currentDialogue.atEndActions.Invoke();
                }
            }

            else
                dm.typeState.enter();
        }
    }

    public class TimedIdleState : IState
    {
        DialogueManager dm;

        public float delayTime = 1f;

        public TimedIdleState(DialogueManager dm)
        {
            this.dm = dm;
        }

        //Start Looking for input
        public void enter()
        {
            dm.StartCoroutine(Execute());

        }

        //Check for input so a new line will appear or dialogue will end
        public IEnumerator Execute()
        {
            yield return new WaitForSeconds(delayTime);
            exit();

        }

        //Go to Hidden if no more lines exist or type a new line
        public void exit()
        {
            if (dm.isEmpty())
            {
                if (!dm.currentDialogue.locked)
                {
                    dm.hiddenState.enter();
                }
                else
                {
                    dm.currentDialogue.atEndActions.Invoke();
                }
            }
            else
                dm.autoTypeState.enter();
        }
    }


    private interface IState
    {
        void enter();

        void exit();

        IEnumerator Execute();


    }
}
