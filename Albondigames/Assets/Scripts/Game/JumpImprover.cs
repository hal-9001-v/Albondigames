using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpImprover : MonoBehaviour
{
    
    float fm = 2.5f;
    float ljm = 2f;

    private Rigidbody2D rb2d;

    private void Awake(){

     rb2d = GetComponent<Rigidbody2D>();

    }

    private void Update(){

        if(rb2d.velocity.y <0){
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fm-1) * Time.deltaTime;
        }   else if(rb2d.velocity.y > 0 && Input.GetKey(KeyCode.Space)){
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (ljm-1) * Time.deltaTime;
        }


    }


}
