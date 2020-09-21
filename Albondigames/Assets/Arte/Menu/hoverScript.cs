using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class hoverScript : MonoBehaviour
{

      float i;
     bool done = true;
    float speed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.P)){

            SceneManager.LoadScene("Azotea");

        }

        if (done)
        {
            i += 0.1f;
        }
        else {

            i -= 0.1f;

        }
        Hover();


    }

    public void Hover() {


        if (done)
        {

            gameObject.transform.position = transform.position + (new Vector3(0, speed, 0));

        }
        else {

            gameObject.transform.position = transform.position + (new Vector3(0, -speed, 0));


        }

        if (i >= 3f)
        {

         
            done = false;
        }
        else if (i <= -3f) {

            done = true;

        }


    }

}
