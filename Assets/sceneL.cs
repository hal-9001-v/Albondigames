using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneL: MonoBehaviour
{
    public float time;

    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
	public void Start(){

lastScene(time);
}

    public IEnumerator LastScene(float time) {
	yield return new WaitForSeconds(time);
        SceneManager.LoadScene(0);

        yield return 0;

    }

    public void lastScene(float time) {
        StartCoroutine(LastScene(time));

    }



}
