using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //stats variables
    public int hp;

    public bool inmune;
    public bool alive;

    public float bulletSize = 2f;

    //movement variables
    public int speed;
    Rigidbody2D rb;
    public Vector2 movement;
    public Vector2 previousMove;


    //bullet variables
    public float bulletSpeed;
    private float shootTimeCounter;
    public float shootCD;

    public GameObject bulletGO;

    public int bulletDamage;
    public int meleeDamage = 1;

    public LayerMask enemyMask;
    public int facing;
    Vector2 attackPoint;

    public bool boolMelee;
    public bool hasWeapon;

    float horizontalDirection;
    float verticalDirection;
    public Animator anim;
    int dir;
    public Vector3 dirMov;
    public float playerShootDist;
    public bool dance;
    public bool talking;
    public int level;

    public bool stopped = false;
    public PlayerStats ps;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hp = 6;

        previousMove = Vector2.zero;
        ps = FindObjectOfType<PlayerStats>();

        initVars();
    }

    void initVars() {
     hp = ps.hp;
     bulletSize = ps.bulletSize;
     speed = ps.speed;
     bulletSpeed = ps.bulletSpeed;
     bulletDamage = ps.bulletDamage;
     meleeDamage = ps.meleeDamage;
     shootCD = ps.shootCD;
     level = ps.level;


}

   


    // Update is called once per frame
    void Update()
    {
        if (!stopped)
        {
            if (alive)
            {

                makeMovement();
                //Get input
                horizontalDirection = Input.GetAxis("HorizontalShoot");
                verticalDirection = Input.GetAxis("VerticalShoot");

                horizontalDirection = Mathf.Round(horizontalDirection);
                verticalDirection = Mathf.Round(verticalDirection);
                if (movement.sqrMagnitude == 0)
                {
                    anim.SetBool("isRunning", false);
                }
                else
                {
                    anim.SetBool("isRunning", true);
                    previousMove = movement;
                }

                if (horizontalDirection == 0 && verticalDirection == 0)
                {
                    anim.SetFloat("StaticHorizontal", previousMove.x);
                    anim.SetFloat("StaticVertical", previousMove.y);
                }
                else
                {
                    anim.SetFloat("StaticHorizontal", horizontalDirection);
                    anim.SetFloat("StaticVertical", verticalDirection);
                }

                if (hasWeapon)
                {
                    equipWeapon();
                }

            }
        }

    }

    private void FixedUpdate()
    {
        if (alive)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
    }



    void shoot()
    {
        dirMov = new Vector3(movement.normalized.x, movement.normalized.y, 0);


        if (horizontalDirection == 0 && verticalDirection == 0) return;

        if (shootTimeCounter > shootCD)
        {
            //Reset Counter
            shootTimeCounter = 0;

            //Shoot if there is some input
            if (horizontalDirection != 0 || verticalDirection != 0)
            {
                SoundManager.PlaySound(SoundManager.Sound.mariShoot, 0.1f);
                GameObject bullet;




                bullet = Instantiate(bulletGO, transform.position + new Vector3(-0.3f, -0.3f), transform.rotation) as GameObject;




                //Set Damage to bullet
                bullet.GetComponent<BulletController>().dir = new Vector2(horizontalDirection, verticalDirection);
                bullet.GetComponent<BulletController>().damage = bulletDamage;

                bullet.transform.localScale = new Vector2(bulletSize, bulletSize);


                //Get RigidBody Component to modify it
                Rigidbody2D bulletrb = bullet.GetComponent<Rigidbody2D>();

                //Modify bullet
                bulletrb.gravityScale = 0;



                bulletrb.velocity = new Vector2(horizontalDirection * bulletSpeed, verticalDirection * bulletSpeed);

            }
        }
        else
        {
            shootTimeCounter += Time.deltaTime;
        }

    }

    public void nextScene()
    {
        ps.updateStats();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Changing Room");
        /*switch (level)
        {

            case 1:
                //Boss 1
                SceneManager.LoadScene("Scenes/LokiBossFight");
                gameObject.transform.position = new Vector2(0.1505976f, -5.068519f);
                level = 2 ;
                break;
            case 2:
                //Nivel2
                SceneManager.LoadScene("Scenes/Scene2");
                gameObject.transform.position = new Vector2(3.720128f, 5.258526f);
                level = 3;
                break;
            case 3:
                //Boss2
                SceneManager.LoadScene("Scenes/LokiBossFight");
                gameObject.transform.position = new Vector2(0.1505976f, -5.068519f);
                level = 4;
                break;
            case 4:
                //Nivel3
                SceneManager.LoadScene("Scenes/Scene3");
                gameObject.transform.position = new Vector2(3.720128f, 5.258526f);
                level = 5;
                break;
            case 5:
                //Boss 3
                SceneManager.LoadScene("Scenes/RaBossFight");
                gameObject.transform.position = new Vector2(0.1505976f, -5.068519f);
                level++;
                break;

                //Extra
            case 7:
                SceneManager.LoadScene("Scenes/Scene2");
                gameObject.transform.position = new Vector2(3.720128f, 5.258526f);
                level++;
                break;
            case 8:
                SceneManager.LoadScene("Scenes/Scene2");
                gameObject.transform.position = new Vector2(3.720128f, 5.258526f);
                level++;
                break;
            case 9:
                SceneManager.LoadScene("Scenes/Scene2");
                gameObject.transform.position = new Vector2(3.720128f, 5.258526f);
                level++;
                break;
        }*/

    }


    public virtual void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag.Equals("zone") && dance == false) dance = true;



        if (col.gameObject.tag.Equals("BossRoom"))
        {

            nextScene();

        }


    }

    public virtual void OnTriggerExit2D(Collider2D col)
    {

        if (col.gameObject.tag.Equals("zone") && dance == true) dance = false;

    }
    void meleeAttack()
    {
        if (horizontalDirection == 0 && verticalDirection == 0) return;

        if (shootTimeCounter > shootCD * 2)
        {
            shootTimeCounter = 0;
            attackPoint = new Vector2(transform.position.x, transform.position.y) + new Vector2(0.75f, 0.75f) * new Vector2(horizontalDirection, verticalDirection).normalized;

            // Debug.Log(attackPoint);

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint, 1, enemyMask);

            foreach (Collider2D enemy in hitEnemies)
            {
                SoundManager.PlaySound(SoundManager.Sound.meleeDmg, 0.3f);
                Debug.Log("I hit: " + enemy.gameObject.name);

                EnemyController ec = enemy.gameObject.GetComponent<EnemyController>();

                ec.takeDamage(meleeDamage);
                ec.setImpactVector(attackPoint);
            }
            StartCoroutine(meleeAnim());
        }
        else
        {
            shootTimeCounter += Time.deltaTime;
        }
    }

    IEnumerator meleeAnim()
    {
        anim.SetBool("isMeeling", true);
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("isMeeling", false);
    }

    private void makeMovement()
    {

        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

    }

    public void TakeDamage(int dam)
    {

        if (!inmune)
        {
            SoundManager.PlaySound(SoundManager.Sound.mariOomph, 0.5f);

            hp -= dam;
            StartCoroutine(Inmunity());
        }

        if (hp <= 0)
        {
            die();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint, 1);
    }

    IEnumerator Inmunity()
    {
        inmune = true;
        yield return new WaitForSeconds(0.75f);
        inmune = false;
    }

    void die()
    {

        alive = false;

        rb.velocity = Vector2.zero;

        Debug.LogWarning("Player Died");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void heal(float heart)
    {

        SoundManager.PlaySound(SoundManager.Sound.healthPickUp, 0.1f);
        hp += (int)heart;


    }
    public void addBulSize(float param)
    {

        bulletSize += param;
    }
    public void addSpeed(float param)
    {
        SoundManager.PlaySound(SoundManager.Sound.speedBoost, 0.1f);

        speed += (int)param;

    }


    public void addBulDelay(float param)
    {

        if (shootCD >= 0.05)
        {
            SoundManager.PlaySound(SoundManager.Sound.pill, 0.1f);

            shootCD -= param;
        }
        else Debug.Log("Too fast a shooting speed");
    }

    public void addBulSpeed(float param)
    {

        bulletSpeed += param;

    }

    public void addBulDmg(float param)
    {
        SoundManager.PlaySound(SoundManager.Sound.ice, 0.1f);

        bulletDamage += (int)param;

    }
    public void addMelDmg(float param)
    {
        SoundManager.PlaySound(SoundManager.Sound.sharpen, 0.1f);

        meleeDamage += (int)param;

    }

    public void equipWeapon()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            boolMelee = !boolMelee;
        }
        */

        if (boolMelee)
        {
            anim.SetBool("HasVasooka", false);
            anim.SetBool("HasEscoba", true);
            meleeAttack();
        }
        else
        {
            anim.SetBool("HasEscoba", false);
            anim.SetBool("HasVasooka", true);
            shoot();
        }
    }
}
