﻿using System;
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
        GameObject go = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
            player = go.GetComponent<PlayerController>();
        StartCoroutine(WaitCamera());

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
        camera.MoveCamera(new Vector2(1070, 360), 1);
    }
 
    public void loadPreviousScene()
    {
        PlayerStats ps = FindObjectOfType<PlayerStats>();

        SceneManager.LoadScene(ps.level);
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
