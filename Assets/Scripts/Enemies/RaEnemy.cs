using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaEnemy : EnemyController
{
    private Vector2 moveDir;
    public Rigidbody2D rb;
    public float speed;
    public bool shootReady = true;
    public bool moving;
    public float waitTime;
    public float shootTime;
    public GameObject enemyBulletGO;
    public Vector2 shootDir;
    public float bulletSpeed;
    public int bulletDamage;
    public bool hitOnce;
    public int hitDamage;
    float deadTime;
    bool dead;
    public RaBossFight ra;
    Vector2 iniscale;
    public SpriteRenderer sp;
    void Start()
    {
        ra = FindObjectOfType<RaBossFight>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        moving = true;
        initializeParent();
        iniscale = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.transform.position, gameObject.transform.position) > 5)
        {
            moving = true;
        }
        else moving = false;

    }
    void FixedUpdate()
    {
        if (!ra.isActiveAndEnabled) {

            gameObject.SetActive(false);

        }

        if (!dead)
        {
            if (moving)
            {


                moveDir = (player.transform.position - gameObject.transform.position).normalized;
                rb.velocity = moveDir.normalized * speed;

            }
            else
            {

                rb.velocity = impactDirection.normalized * speed * 0.1f;


            }

            Shoot();

        }
        revive();

    }





    void Shoot()
    {
        int rndBullet = UnityEngine.Random.Range(0, 5);


        if (waitTime > shootTime)
        {
            SoundManager.PlaySound(SoundManager.Sound.raBallShoots, 0.1f);
            waitTime = 0;
            gameObject.GetComponent<Rigidbody2D>().velocity = moveDir * speed;
            shootDir = ((player.transform.position - gameObject.transform.position)).normalized;
            enemyBulletGO.GetComponent<SpriteRenderer>().sprite = GameAssets.i.raSprites[rndBullet];
            GameObject enemyBullet = Instantiate(enemyBulletGO, transform.position, transform.rotation) as GameObject;
            enemyBullet.GetComponent<EnemyBullet>().damage = this.bulletDamage;
            enemyBullet.AddComponent<Rigidbody2D>().gravityScale = 0;
            enemyBullet.GetComponent<Rigidbody2D>().velocity = shootDir.normalized * bulletSpeed;
        }
        else {

            waitTime += Time.deltaTime;

        }


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

    public override void takeDamageEffect()
    {
        //throw new System.NotImplementedException();
    }


    protected override void startValues()
    {

        hp = 35;
        speed = 2.06f;
        moving = true;
        waitTime = 1.01f;
        shootTime = 0.2f;
        bulletSpeed = 50;
        bulletDamage = 2;
        hitDamage = 1;


    }

    void revive()
    {

        if (dead)
        {
            if (deadTime > 10)
            {
                deadTime = 0;
                gameObject.transform.localScale = iniscale;
                SoundManager.PlaySound(SoundManager.Sound.raBallRevive, 0.1f);
                gameObject.transform.position = new Vector2(-6.612f, 1.0016f);
                dead = false;
                hp = 15;
            }
            else
            {

                deadTime += Time.deltaTime;

            }

        }


    }
    public override void Die()
    {

        SoundManager.PlaySound(SoundManager.Sound.raBallDies, 0.5f);
        gameObject.transform.localScale = new Vector2(0, 0);
        dead = true;
    }
 

}
