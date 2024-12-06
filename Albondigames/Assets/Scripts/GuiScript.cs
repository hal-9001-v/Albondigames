using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiScript : MonoBehaviour
{

     Animator anim;
    public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {

        if (player.hp == 5)
        {

            anim.SetInteger("State", 100);
        }
        if (player.hp == 4)
        {
            
           anim.SetInteger("State", 75);
        }
        if (player.hp == 3)
        {

            anim.SetInteger("State", 50);
        }
        if (player.hp == 2)
        {

            anim.SetInteger("State", 25);
        }
        if (player.hp == 1)
        {

            anim.SetInteger("State", 0);
        }
        if (player.hp == 0)
        {

            anim.SetInteger("State", -5);
        }


    }
}
