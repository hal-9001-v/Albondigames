using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePX;

public class RandomEnemy : EnemyController
{
    public float speed;
    public bool moving;
    public float waitTime;
    public float shootTime;
    public GameObject enemyBulletGO;
    public Vector2 shootDir;
    public float bulletSpeed;
    public int bulletDamage;
    public bool hitOnce;
    public int hitDamage;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(!moving)
        {
            moving = true;
            StartCoroutine(Move());         
        }
    }
    void FixedUpdate()
    {
        shootDir = (player.transform.position - gameObject.transform.position).normalized;
    }

    IEnumerator Move()
    {
        int dir = UnityEngine.Random.Range(0,4);
        Vector2 moveDir = Vector2.zero;

        switch (dir){
            case 0:
                moveDir = Vector2.up;
                break;

            case 1:
                moveDir = Vector2.down;
                break;

            case 2:
                moveDir = Vector2.right;
                break;

            case 3:
                moveDir = Vector2.left;
                break;
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = moveDir * speed;
        yield return new WaitForSeconds(waitTime);
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Shoot();
        yield return new WaitForSeconds(shootTime);
        moving = false;
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
