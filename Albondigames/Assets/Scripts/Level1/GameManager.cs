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
}
