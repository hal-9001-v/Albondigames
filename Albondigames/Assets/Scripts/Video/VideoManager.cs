using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class VideoManager : MonoBehaviour
{

    
    
    public UnityEngine.Video.VideoPlayer vp;
    // Start is called before the first frame update
    void Start()
    {
        vp = GetComponent<UnityEngine.Video.VideoPlayer>();
    }



    // Update is called once per frame
    void Update()
    {
        if(!vp.isPlaying) {
            SceneManager.LoadScene("Level1");
        }
    }
}
