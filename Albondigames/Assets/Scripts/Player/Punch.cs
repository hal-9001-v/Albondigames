using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
   public bool canHit = true;
    Enemy enemy;
    // Start is called before the first frame update

     private void Awake() {
         enemy = FindObjectOfType<Enemy>();
    }
    void Start()
    {
        
    }


   public void SetActive(bool b){

        gameObject.SetActive(b);

    }

 
}
