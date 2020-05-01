using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 2;
    public Rigidbody2D rb;
    public float bulletTime;

    void Start()
    {
        StartCoroutine(BulletLife());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (!hitInfo.gameObject.tag.Equals("Player") && !hitInfo.gameObject.tag.Equals("AuxCollider") && !hitInfo.gameObject.tag.Equals("EnemyBullet"))
        {
            if (hitInfo.tag.Equals("Enemy"))
            {
                hitInfo.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            }
            Destroy(this.gameObject);
        }     
    }

    IEnumerator BulletLife()
    {
        yield return new WaitForSeconds(bulletTime);
        Destroy(this.gameObject);
    }
}
