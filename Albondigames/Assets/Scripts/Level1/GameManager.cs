using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private CameraController camera;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        StartCoroutine(WaitCamera());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator WaitCamera()
    {
        while (!camera.HasArrived())
        {
            yield return new WaitForEndOfFrame();
        }
        camera.GoToNextNode();
    }

    //Eventos
    public void StopPlayer()
    {
        player.canMove = false;
    }

    public void MovePlayer()
    {
        player.canMove = true;
    }

    public void MoverCamara1()
    {
        camera.MoveCamera(new Vector2(50,60),20);
    }

    public void JToMove()
    {
        player.jToMove = true;
    }

    public void DToMove()
    {
        player.jToMove = false;
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
