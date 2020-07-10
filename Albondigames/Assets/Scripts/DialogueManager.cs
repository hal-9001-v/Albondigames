using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject myTextObject;
    private TextMeshProUGUI myText;

    string textToWrite;
    int delay = 0;

    void Awake()
    {
        //Initializing
        myText = myTextObject.GetComponent<TextMeshProUGUI>();
    }

    public void triggerDialogue(Dialogue dialogue)
    {
        textToWrite = dialogue.text;
        delay = dialogue.delay;

        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        char[] letters = textToWrite.ToCharArray();

        myText.text = "";

        int counter = 0;
        while (counter < textToWrite.Length)
        {

            myText.text += letters[counter];
            counter++;

            yield return new WaitForSeconds(0.1f);
        }


        yield return null;
    }

}
