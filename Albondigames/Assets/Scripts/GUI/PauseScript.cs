using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
       // Start is called before the first frame update
    public bool en;
    public GameObject pmui;
    void Start()
    {
        en = false;
        pmui.SetActive(false);
    }

    // Update is called once per frame

    public void Update() {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!en)
            {
                en = true;
                pmui.SetActive(true);
                Time.timeScale = 0;
                AudioListener.pause = true;
            }
           

        else if (en)  {
            en = false;
            pmui.SetActive(false);
            AudioListener.pause = false;
            Time.timeScale = 1;

        }

        }
    }
        
        
}
