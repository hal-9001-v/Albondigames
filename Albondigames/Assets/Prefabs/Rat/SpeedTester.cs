using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTester : MonoBehaviour
{
    Rigidbody2D rb2d;
    RatBehaviour rbh;
    // Start is called before the first frame update
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rbh = GetComponent<RatBehaviour>();
    }
   
    public bool TestSpeed(){

        if(rbh.alive){
        if(rb2d.velocity.x != 0){

            return true;

        } else return false;
        }else return false;
    }
}
