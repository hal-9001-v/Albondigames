using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gui : MonoBehaviour
{

     Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            
           anim.SetInteger("State", 75);
        }
        if (Input.GetKey(KeyCode.E))
        {

            anim.SetInteger("State", 50);
        }
        if (Input.GetKey(KeyCode.R))
        {

            anim.SetInteger("State", 25);
        }
        if (Input.GetKey(KeyCode.T))
        {

            anim.SetInteger("State", 0);
        }
        if (Input.GetKey(KeyCode.Y))
        {

            anim.SetInteger("State", -5);
        }

    }
}
