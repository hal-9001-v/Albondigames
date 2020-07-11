using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
  PlayerController ps;
    // Start is called before the fir
    public int hp = 3;
    public int level = 0;

    void Start()
    {

        ps = FindObjectOfType<PlayerController>();
        
    }

    public void reset() {
           hp = 6;
           level = 0;
}

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
