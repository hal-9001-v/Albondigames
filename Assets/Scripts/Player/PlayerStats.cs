using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    PlayerController ps;
    // Start is called before the fir
    public int hp = 6;
   public float bulletSize = 2f;
    public int speed = 3;
    public float bulletSpeed = 9.05f;
    public int bulletDamage = 1;
    public int meleeDamage = 1;
    public float shootCD = 0.5f;
    public int level = 0;

    void Start()
    {

        ps = FindObjectOfType<PlayerController>();
        
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);


    }

    // Update is called once per frame
    public void updateStats() {

        hp = ps.hp;
        bulletSize = ps.bulletSize;
        speed = ps.speed;
        bulletSpeed = ps.bulletSpeed;
        bulletDamage = ps.bulletDamage;
        meleeDamage = ps.meleeDamage;
        shootCD = ps.shootCD;
        level = ps.level;


    }
}
