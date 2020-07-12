using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
     Animator animator;
    PlayerController player;
    public bool ignore = false;
    void Awake()
    {

        animator = GetComponent<Animator>();
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ignore)
        {
            animator.SetBool("Moving", player.isWalking);//
            animator.SetBool("Grounded", player.isGrounded);//
            animator.SetBool("Burping", player.isBurping);//
            animator.SetBool("Running", player.isRunning);//
            animator.SetBool("Punching", player.isPunching);//
            animator.SetBool("Peeing", player.isPeeing);
            animator.SetBool("Falling", player.isFalling);
            animator.SetBool("Lieing", player.isLieing);
            animator.SetBool("Lied", player.isLed);
            animator.SetBool("GettingUp", player.isGettingUp);
            animator.SetBool("Climbing", (player.isClimbing | player.isMoving) && player.canClimb);
            animator.SetBool("IdleClimbing", player.canClimb);//
        }
    }

    public void setIgnore() { 
        ignore = true;
    }


    public void unSetIgnore()
    {
        ignore = false;
    }
}
