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
        if (player != null) {
            player.canMove = false;
            player.ChangeBurp();
        }
    }

    public void MovePlayer()
    {
        if (player != null){
         player.canMove = true;
        player.ChangeBurp();
        }
    }
    public void MoverCamara1()
    {
        camera.MoveCamera(new Vector2(269, 68), 1);
    }
 
    public void loadPreviousScene()
    {
        PlayerStats ps = FindObjectOfType<PlayerStats>();

        SceneManager.LoadScene(ps.level);
    }

    public void loadNextSceneEnd1(){

        SceneManager.LoadScene("Level1");


    }
    public void loadNextSceneEnd2(){

        SceneManager.LoadScene("Level2");


    }
    public void loadNextSceneEnd3(){

        SceneManager.LoadScene("Level3");


    }

    public void loadMenu(){

        SceneManager.LoadScene("MainMenu");


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
        camera.MoveCamera(new Vector2(-6.68f, -0.69f), 0.5f);
    }

    public void DontLookAtMe2()
    {
        camera.MoveCamera(new Vector2(-10.68f, -0.69f), 0.5f);
    }

    public void printWig()
    {
        print("Wig");
    }
    public void loadCreditScene()
    {
        SceneManager.LoadScene("CreditScene");
    }
    public void loadNegroScene()
    {
        SceneManager.LoadScene("Negro");

    }

    public void loadLogoScene1()
    {
        SceneManager.LoadScene("LogoScene1");

    }

    //Sound Play
    public void PlaySoundDoorSlam(){
        SoundManager.PlaySound(SoundManager.Sound.doorSlam,0.5f);
    }
     public void PlaySoundQTE(){
        SoundManager.PlaySound(SoundManager.Sound.qteClick,0.5f);
    }
     public void PlayLadder(){
        SoundManager.PlaySound(SoundManager.Sound.caidaEscalera,0.05f);
    }

    public void PlayPee(){
        StartCoroutine(PeeWait());
    }

    IEnumerator PeeWait(){

        yield return new WaitForSeconds(4f);

        SoundManager.PlaySound(SoundManager.Sound.mear,2f);

    }

}
