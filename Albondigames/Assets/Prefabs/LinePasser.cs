using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LinePasser : Triggerable
{
CameraController camera;
/// <summary>
/// Awake is called when the script instance is being loaded.
/// </summary>
void Awake()
{
    camera = FindObjectOfType<CameraController>();
}
    public override void trigger()
    {

        StartCoroutine(triggerScene());
        camera.TransitionToBlack(0.9f);
        Debug.Log("Changing Room");

    
    
    }

    IEnumerator triggerScene(){

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }

    }

