using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadingScreen : MonoBehaviour
{

    public Sprite[] sprites;
    public SpriteRenderer target;
    public int extraTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScreen());

    }

    IEnumerator LoadScreen() {

        int index = 0;
        while (index < 10 + extraTime)
        {
            target.sprite = sprites[1];
            yield return new WaitForSeconds(0.1f);
            target.sprite = sprites[0];
            yield return new WaitForSeconds(0.1f);

            index++;

        }
        AsyncOperation loadLevel = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        index = 0;
        while (!loadLevel.isDone && index < 10) {

            target.sprite = sprites[1];
            yield return new WaitForSeconds(0.1f);
            target.sprite = sprites[0];
            yield return new WaitForSeconds(0.1f);

            index++;

        }


       
    }

}
