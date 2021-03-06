﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public float coolDown;
    private float lastShoot;
    public WindowProj proj;
    PlayerController player;
    public bool isThrowing;
     private void Awake() {
    player = FindObjectOfType<PlayerController>();    
    }
    // Start is called before the first frame update
    void Start()
    {
        lastShoot = Time.time;
        proj.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if((Time.time - lastShoot) > coolDown)
        {
            Shoot();
            lastShoot = Time.time;
        }
    }

    void Shoot()
    {
        
        StartCoroutine(ThrowAnim());
        StartCoroutine(Instant());

        //Debug.Log(Vector2.Distance(gameObject.transform.position, player.transform.position));

        if(Vector2.Distance(gameObject.transform.position, player.transform.position) < 50f){
                SoundManager.PlaySound(SoundManager.Sound.throwing, 0.3f);
                isThrowing = true;
            }


    }

    IEnumerator ThrowAnim(){

        yield return new WaitForSeconds(0.5f);
        isThrowing = false;

    }

    IEnumerator Instant(){

        yield return new WaitForSeconds(0.25f);
        Instantiate(proj, transform.position + new Vector3(1.5f,-2,0), transform.rotation);

    }
}
