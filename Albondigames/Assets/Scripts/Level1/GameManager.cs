using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private CameraController camera;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
         player = FindObjectOfType<PlayerController>();

      if (camera != null){
        StartCoroutine(WaitCamera());
      }


    IEnumerator WaitCamera()
    {
        while (!camera.HasArrived())
        {
            yield return null;
        }
        camera.GoToNextNode();
    }

    //Eventos
    public void StopPlayer()
    {
        if (player != null) player.canMove = false;
    }

    public void MovePlayer()
    {
        if (player != null) player.canMove = true;
    }
    public void MoverCamara1()
    {
        camera.MoveCamera(new Vector2(1070, 360), 1);
    }
 
    public void loadPreviousScene()
    {
        PlayerStats ps = FindObjectOfType<PlayerStats>();

        SceneManager.LoadScene(ps.level);
    }

    public void loadNextScene(){

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


    }
    
    public void quit(){

        Application.Quit();

    }
     public void JToMove()
    {
        if (player != null) player.jToMove = true;
    }

    public void DToMove()
    {
        if (player != null) player.jToMove = false;
    }

    public void DontLookAtMe()
    {
        camera.MoveCamera(new Vector2(-6.68f, -0.69f), 2);
    }

    public void DontLookAtMe2()
    {
        camera.MoveCamera(new Vector2(-10.68f, -0.69f), 2);
    }

    public void printWig()
    {
        print("Wig");
    }
}
