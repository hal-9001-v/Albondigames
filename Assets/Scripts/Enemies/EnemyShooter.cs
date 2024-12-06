using System.Collections;
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

    public Animator anim;
    public SpriteRenderer spR;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        initializeParent();
    }

    public override void takeDamageEffect() { 
        
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
            StartCoroutine(Shoot());
            lastFire = Time.time;
        }

        flipSprt(spR);


    }

    IEnumerator Shoot()
    {
        anim.SetBool("isAtaccking", true);
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("isAtaccking", false);
        GameObject enemyBullet = Instantiate(enemyBulletGO, transform.position, transform.rotation) as GameObject;
        enemyBullet.GetComponent<EnemyBullet>().damage = bulletDamage;
        enemyBullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        enemyBullet.GetComponent<Rigidbody2D>().velocity = shootDir.normalized * bulletSpeed;
        
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

    protected override void startValues()
    {
        bulletSpeed = 6;
        hp = 8;
    }

    public override void Die()
    {

        SoundManager.PlaySound(SoundManager.Sound.weakDies, 0.1f);

        gameObject.SetActive(false);
    }
}
