using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EffectCamera : MonoBehaviour
{ 
    public Image myImage;

    public UnityEvent afterFadeOutEffect;

    public void fadeOutEffect(float wait)
    {
        StartCoroutine(FadeOutEffect(wait));
    }

    public void fadeInEffect(float wait)
    {
        StartCoroutine(FadeInEffect(wait));
    }

    IEnumerator FadeOutEffect(float wait)
    {
        float alpha = 0;

        myImage.color = new Color(0, 0, 0, 0);

        for (int i = 0; i < 20; i++)
        {
            alpha += 0.05f;

            myImage.color = new Color(0, 0, 0, alpha);


            yield return new WaitForSeconds(wait);

        }

        afterFadeOutEffect.Invoke();
    }

    IEnumerator FadeInEffect(float wait)
    {
        float alpha = 1;


        myImage.color = new Color(0, 0, 0, 1);

        for (int i = 0; i < 20; i++)
        {
            alpha -= 0.05f;

            myImage.color = new Color(0, 0, 0, alpha);


            yield return new WaitForSeconds(wait);

        }

    }


}
