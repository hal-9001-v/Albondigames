using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
  PlayerController ps;
    // Start is called before the fir
    public int hp = 3;
    public int level = 0;
    public bool lvl1CheckPointReached = false;
   public  bool lvl2CheckPointReached = false;
   public  bool lvl3CheckPointReached = false;



    public void reset() {
           hp = 6;
           level = 0;
           lvl1CheckPointReached = false;
           lvl2CheckPointReached = false;
           lvl3CheckPointReached = false;

}

    void Awake()
    {
        ps = FindObjectOfType<PlayerController>();
        DontDestroyOnLoad(gameObject);
    }

}
