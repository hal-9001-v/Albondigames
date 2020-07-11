using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp = 3;
    public bool inmune = false;
  
     public void TakeDamage()
    {
        if (!inmune)
        {
            //SoundManager.PlaySound(SoundManager.Sound.mariOomph, 0.5f);

            hp -= 1;
            StartCoroutine(Inmunity());
        }

        if (hp <= 0)
        {
            die();
        }
    }

 IEnumerator Inmunity()
    {
        inmune = true;
        yield return new WaitForSeconds(0.75f);
        inmune = false;
    }

   

   
    public void die()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col){

        if (col.gameObject.tag.Equals("Punch"))
        {
            TakeDamage();
        } 

         
        
    }
}
