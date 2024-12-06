using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //stats variables
    public int hp;

    //movement variables
    public int speed;
    Rigidbody2D rb;
    Vector2 movement;
    
    //bullet variables
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;
    public GameObject bulletGO;
    public int bulletDamage;

    /*
    //melee variables
    public Transform attackPos;
    public LayerMask whatsIsEnemy;
    public float attackRange;
    public float startAttackTime;
    public float lastAttackTime;*/
    public bool inmune;
    public bool alive;

    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        hp = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive){
            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");

            float shootHorizontal = Input.GetAxis("HorizontalShoot");
            float shootVertical = Input.GetAxis("VerticalShoot");

            if ((shootHorizontal != 0 || shootVertical != 0) && Time.time > lastFire + fireDelay)
            {
                Shoot(shootHorizontal, shootVertical);
                lastFire = Time.time;
            }

            /*
            if (lastAttackTime <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatsIsEnemy);
                }

                lastAttackTime = startAttackTime;
            }
            else
            {
                lastAttackTime -= Time.deltaTime;
            } */
        }


    }

    private void FixedUpdate()
    {
        if (alive)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
        
    }

    void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(bulletGO, transform.position + new Vector3(-0.2f, -0.5f, 0.0f), transform.rotation) as GameObject;
        bullet.GetComponent<BulletController>().damage = this.bulletDamage;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
            0
        );
    }

    public void TakeDamage(int dam)
    {
        Debug.Log("BOOM");

        if (!inmune)
        {
            hp -= dam;
            StartCoroutine(Inmunity());
        }

        if(hp <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Inmunity()
    {
        inmune = true;
        yield return new WaitForSeconds(0.75f);
        inmune = false;
    }

    IEnumerator Die()
    {
        alive = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

}
