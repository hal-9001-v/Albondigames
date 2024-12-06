using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class LokiBehaviour : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public bool agresive;
    public bool teleported;
    public bool teleportFinished;
    public bool attackFinished;

    public int hp;
    public GameObject enemyBulletGO;
    public float bulletSpeed;
    public Vector2 shootDir;
    GameObject player;

    public float teleportTime;
    public float idleTime;

    public SpriteRenderer spR;
    public Animator anim;

    public bool hitOnce;

    public Sprite[] bullets;

    public UnityEvent atDead;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        flipSprt(spR);

        if (agresive)
        {
            if (teleported && !attackFinished)
            {
                StartCoroutine(Attack());
            }
            if (!teleported && !teleportFinished)
            {
                StartCoroutine(Teleport());

            }
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }
    }

    IEnumerator Attack()
    {
        int projectiles = 3;
        while(projectiles >=0){
            int rndBullet = UnityEngine.Random.Range(0, 3);
            anim.SetBool("isAttacking", true);
            attackFinished = true;
            SoundManager.PlaySound(SoundManager.Sound.lokiShoot, 0.5f);

            Debug.Log("Boom");
            shootDir = (player.transform.position - gameObject.transform.position).normalized;
            GameObject enemyBullet = Instantiate(enemyBulletGO, transform.position, transform.rotation) as GameObject;
            enemyBullet.AddComponent<Rigidbody2D>().gravityScale = 0;
            enemyBullet.GetComponent<SpriteRenderer>().sprite = bullets[rndBullet];
            enemyBullet.transform.localScale = enemyBullet.transform.localScale * 3;
            enemyBullet.GetComponent<Rigidbody2D>().velocity = shootDir.normalized * bulletSpeed;
            yield return new WaitForSeconds(0.5f);
            projectiles--;
        }
        anim.SetBool("isAttacking", false);
        yield return new WaitForSeconds(idleTime);
        teleported = false;
        teleportFinished = false;
    }

    IEnumerator Teleport()
    {
        teleportFinished = true;
        Debug.Log("Swosh");
        transform.position = new Vector2(UnityEngine.Random.Range(minX, maxX), UnityEngine.Random.Range(minY, maxY));
        SoundManager.PlaySound(SoundManager.Sound.lokiTP, 0.5f);

        yield return new WaitForSeconds(teleportTime);
        teleported = true;
        attackFinished = false;
    }

    public void takeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }

        StartCoroutine(takeDamageEffect());

    }

    public void Die()
    {
        if (agresive) {
            agresive = false;
            SoundManager.PlaySound(SoundManager.Sound.lokiDeath, 0.5f);

            atDead.Invoke();
        }


    }

    IEnumerator takeDamageEffect()
    {
        anim.SetBool("isHitted", true);
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("isHitted", false);
    }

    public void flipSprt(SpriteRenderer spR)
    {
        if (player.transform.position.x > gameObject.transform.position.x)
        {
            spR.flipX = false;
        }
        else
        {
            spR.flipX = true;
        }
    }

    void OnCollider2DEnter(Collider2D hitInfo)
    {
        Debug.Log("AAAAA");
        if (hitInfo.tag.Equals("Bullet"))
        {
            hitOnce = true;
            StartCoroutine(Wait());
        }

    }

    IEnumerator Wait()
    {
        takeDamage(1);
        yield return new WaitForSeconds(2f);
        hitOnce = false;
    }
}
