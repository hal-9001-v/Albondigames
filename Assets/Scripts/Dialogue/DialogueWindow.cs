using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GamePX;
using TMPro;

public class DialogueWindow : MonoBehaviour
{

    public float delay;
    public float endDelay;
    public string dialogueName;
    public string[] lines;

    TMP_Text text;
    
    TMP_Text nameText;
    public GameObject nameTextObject;

    public UnityEvent atStartOfDialogue;
    public UnityEvent atEndOfDialogue;

    private void Start()
    {
        nameText = nameTextObject.GetComponent<TMP_Text>();
        nameText.text = dialogueName;

        text = GetComponent<TMP_Text>();

        StartCoroutine(typeSentences());


    }
    IEnumerator typeSentences()
    {
        foreach (string line in lines)
        {
            text.text = "";

            //Print a char every frame
            foreach (char c in line.ToCharArray())
            {
                text.text += c;

                //Wait this time every frame
                yield return new WaitForSeconds(delay);
            }

            yield return new WaitForSeconds(endDelay);

        }

        Destroy(this.gameObject);

        yield return 0;
    }

}
