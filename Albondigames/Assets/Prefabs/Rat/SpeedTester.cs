using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTester : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public RatBehaviour rbh;
    public bool isMoving;
    // Start is called before the first frame update
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rbh = GetComponent<RatBehaviour>();
    }
   
   /// <summary>
   /// Update is called every frame, if the MonoBehaviour is enabled.
   /// </summary>
   void Update()
   {
      if(rbh.alive){
        if(rb2d.velocity != new Vector2(0,0)){
            isMoving = true;

        } else {
          isMoving = false;
            }
        }
    } 
   }
  