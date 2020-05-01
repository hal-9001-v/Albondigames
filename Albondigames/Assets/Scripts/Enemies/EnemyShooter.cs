using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : EnemyController
{

    //bullet variables
    private Vector2 shootDir;
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;
    public GameObject enemyBulletGO;
    public int bulletDamage;
    public int hitDamage;
    public bool hitOnce;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        shootDir = (player.transform.position - gameObject.transform.position).normalized;
    }

    void Update()
    {
        if (Time.time > lastFire + fireDelay)
        {
            Shoot();
            lastFire = Time.time;
        }
    }

    void Shoot()
    {
        GameObject enemyBullet = Instantiate(enemyBulletGO, transform.position, transform.rotation) as GameObject;
        enemyBullet.GetComponent<EnemyBullet>().damage = this.bulletDamage;
        enemyBullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        enemyBullet.GetComponent<Rigidbody2D>().velocity = shootDir * bulletSpeed;
        
    }

    void OnTriggerStay2D(Collider2D hitInfo)
    {
        if ((hitInfo.gameObject.tag.Equals("AuxCollider")) && !hitOnce)
        {
            hitOnce = true;
            player.GetComponent<PlayerController>().TakeDamage(hitDamage);
            StartCoroutine(Wait());

        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        hitOnce = false;
    }
}
