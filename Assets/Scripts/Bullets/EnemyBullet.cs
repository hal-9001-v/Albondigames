using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public abstract class EnemyBullet : MonoBehaviour
{
    public int damage = 1;
    public Vector2 dir;

    void Start() {

        virtualStart();
    
    }
    public abstract void virtualStart();
}
