using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class VideoManager : MonoBehaviour
{

    CameraController camera;
    
    public UnityEngine.Video.VideoPlayer vp;
    // Start is called before the first frame update
    void Start()
    {
        camera = FindObjectOfType<CameraController>();
        vp = GetComponent<UnityEngine.Video.VideoPlayer>();
    }



    // Update is called once per frame
    void Update()
    {

        if(!vp.isPlaying) {
            StartCoroutine(VideoWait());
        }
    
    }

    IEnumerator VideoWait(){
            yield return new WaitForSeconds(1f);

            camera.TransitionToBlack(1.5f);

        yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("Level1");


    }
}
