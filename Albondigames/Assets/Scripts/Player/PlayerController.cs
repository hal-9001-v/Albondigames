using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Unity Components
    private Rigidbody2D rb2d;
<<<<<<< HEAD
    private BoxCollider2D bx2d;
    [SerializeField] private LayerMask lm;
    [SerializeField] private LayerMask ladm;
     PlayerStats ps;
    ParticleSystem blood;
    public Punch[] punchArray = new Punch[2];
    public SpriteRenderer spr;
    public CamShake shaker;
    //Character attributes
    private float MOVEMENT_SPEED = 10f;
    private float xtraheight = .1f;
    private float midAirControl = 5f;
    float jumpVelocity = 20f;
=======
    private Vector3 moveDir;
    private BoxCollider2D bx2d;
    [SerializeField] private LayerMask lm;
    [SerializeField] private LayerMask ladm;
    public PlayerStats ps;
    public Punch punch;
    private SpriteRenderer spr;
    //Character attributes
    private float MOVEMENT_SPEED = 10f;
    private float CLIMBING_SPEED = 10f;
    private float xtraheight = .1f;
    private float midAirControl = 4f;
    float jumpVelocity = 15f;
>>>>>>> montaje
    private bool inmune = false;
    bool b = false;

    public int hp = 3;
<<<<<<< HEAD
    //GAME CONTROL BOOLEANS
    // MOVEMENT
    private bool canBurp = true;
=======
    public int dmg = 1;
    //GAME CONTROL BOOLEANS
    // MOVEMENT
>>>>>>> montaje
    public bool canClimb = false;
    public bool isMoving = false;
    public bool isGrounded = true;
    public bool isPunching = false;
    public bool isBurping = false;
    public bool isRunning = false;
    public bool isPeeing = false;
    public bool isFalling = false;
    public bool isLieing = false;
    public bool isLed = false;
    public bool isGettingUp = false;
    public bool isClimbing = false;
    public bool isWalking = false;
<<<<<<< HEAD
     bool left = false;
     int selPunch;
=======
>>>>>>> montaje

    // LEVEL 1
    public bool jToMove = false; //
    public bool canMove = true; //
<<<<<<< HEAD
=======
    public bool moveDCounterOn = false;//
    public int moveDCounter = 0;//
>>>>>>> montaje

    // LEVEL 2
    public bool invertedControls = false;//
    public bool canPunch = true;//


<<<<<<< HEAD
=======
    // LEVEL 3
    public bool moveACounterOn = false;//
    public int moveACounter = 0;//
>>>>>>> montaje


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bx2d = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
<<<<<<< HEAD
        shaker = FindObjectOfType<CamShake>();
        if (ps == null)
            ps = FindObjectOfType<PlayerStats>();
        initVars();
       punchArray[0].SetActive(b);
       punchArray[1].SetActive(b);
       
=======

        if (ps == null)
            ps = FindObjectOfType<PlayerStats>();

        punch = FindObjectOfType<Punch>();
        initVars();
        punch.SetActive(b);
>>>>>>> montaje
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning) { MOVEMENT_SPEED = 15f; } else { MOVEMENT_SPEED = 10f; }
        AnimationHandler();
        if (!canClimb)
        {
            if (!invertedControls)
<<<<<<< HEAD
            { 
                 HandleMovement();
                if (IsGrounded() && Input.GetKey(KeyCode.Space) && canMove)
                {
                    SoundManager.PlaySound(SoundManager.Sound.jump, 0.5f);
                    rb2d.velocity = Vector2.up * jumpVelocity;
                }
                }

            
=======
            {

                if ((moveDCounterOn && moveDCounter > 0) | (moveACounterOn && moveACounter > 0))
                {

                    if (moveDCounterOn && Input.GetKeyDown(KeyCode.D))
                    {
                        moveDCounter--;
                    }
                    else if (moveACounterOn && Input.GetKeyDown(KeyCode.A))
                    {
                        moveACounter--;
                    }
                }
                else
                {
                    moveDCounterOn = false;
                    moveACounterOn = false;
                    moveDCounter = 5;
                    moveACounter = 5;
                    HandleMovement();
                    if (IsGrounded() && Input.GetKey(KeyCode.Space) && canMove)
                    {
                        rb2d.velocity = Vector2.up * jumpVelocity;
                    }

                }

            }
>>>>>>> montaje
            else
            {
                HandleInvertedMovement();
                if (IsGrounded() && Input.GetKey(KeyCode.Space) && canMove)
                {
<<<<<<< HEAD
                    SoundManager.PlaySound(SoundManager.Sound.jump, 0.5f);
=======
                    float jumpVelocity = 20f;
>>>>>>> montaje
                    rb2d.velocity = Vector2.up * jumpVelocity;
                }
            }
        }
        else if (canClimb)
        {
            if (!invertedControls)
            {
                HandleClimbMovement();
            }
            else if (invertedControls)
            {
                HandleInvertedClimbMovement();
            }
        }

    }

    void initVars()
    {
        hp = ps.hp;

    }

    //TO DO
    // WHEN CHANGING SCENES CALL UPDATESTATS();

    public void updateStats()
    {
        ps.hp = hp;

    }

<<<<<<< HEAD
    
=======

>>>>>>> montaje

    public void TakeDamage()
    {
        if (!inmune)
        {
<<<<<<< HEAD
            SoundManager.PlaySound(SoundManager.Sound.takeDmg, 0.5f);

            shaker.Shake(0.25f);
            blood = GameAssets.i.ps[0];
            Instantiate(blood, transform.localPosition, transform.rotation);
=======
            //SoundManager.PlaySound(SoundManager.Sound.mariOomph, 0.5f);

>>>>>>> montaje
            hp -= 1;
            StartCoroutine(Inmunity());
        }

        if (hp <= 0)
        {
<<<<<<< HEAD
            StartCoroutine(Die());
=======
            die();
>>>>>>> montaje
        }
    }

    IEnumerator Inmunity()
    {
        inmune = true;
        yield return new WaitForSeconds(0.75f);
        inmune = false;
    }

<<<<<<< HEAD
    IEnumerator Die(){

        rb2d.velocity = Vector2.zero;
        gameObject.SetActive(false);
        Debug.LogWarning("Player Died");
        SoundManager.PlaySound(SoundManager.Sound.charDeath, 0.5f);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
  
   

    public void Burp()
    {
        if (Input.GetKeyDown(KeyCode.E) && isGrounded && canBurp)
        {
            canBurp = false;
=======

    public void die()
    {

        rb2d.velocity = Vector2.zero;

        Debug.LogWarning("Player Died");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }

    public void Burp()
    {
        if (Input.GetKeyDown(KeyCode.E) && isGrounded)
        {
>>>>>>> montaje
            float r = Random.Range(1, 5);
            Debug.Log(r);
            switch (r)
            {
                case 1:
                    SoundManager.PlaySound(SoundManager.Sound.eructo1, 0.1f);
                    break;
                case 2:
                    SoundManager.PlaySound(SoundManager.Sound.eructo2, 0.1f);
                    break;
                case 3:
                    SoundManager.PlaySound(SoundManager.Sound.eructo3, 0.1f);
                    break;
                case 4:
                    SoundManager.PlaySound(SoundManager.Sound.eructo4, 0.1f);
                    break;
                default:
                    SoundManager.PlaySound(SoundManager.Sound.eructo4, 0.1f);
                    break;
            }
            StartCoroutine(BurpWait());

        }

    }

    IEnumerator BurpWait()
    {
        isBurping = true;
        canMove = false;
<<<<<<< HEAD
        shaker.Shake(0.5f);
=======
>>>>>>> montaje
        yield return new WaitForSeconds(0.75f);
        canMove = true;
        isBurping = false;
        inmune = false;
<<<<<<< HEAD
        canBurp = true;
=======
>>>>>>> montaje
    }

    private bool IsGrounded()
    {
        RaycastHit2D rcht = Physics2D.Raycast(bx2d.bounds.center, Vector2.down, bx2d.bounds.extents.y + xtraheight, lm);
        Color rc;
        if (rcht.collider != null)
        {
            rc = Color.green;
        }
        else
        {
            rc = Color.red;
        }
        Debug.DrawRay(bx2d.bounds.center, Vector2.down * (bx2d.bounds.extents.y + xtraheight), rc);
        return rcht.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Ladder"))
        {
            canClimb = true;
        }

        if (col.gameObject.tag.Equals("Enemy"))
        {
<<<<<<< HEAD
        if(!inmune)   SoundManager.PlaySound(SoundManager.Sound.golpetaso, 0.5f);
=======
>>>>>>> montaje

            TakeDamage();

        }
<<<<<<< HEAD

        if (col.gameObject.tag.Equals("EnemyBullet"))
        {
            TakeDamage();
            SoundManager.PlaySound(SoundManager.Sound.bullet, 0.5f);
            Destroy(col.gameObject);

        }  

    

=======
>>>>>>> montaje
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Ladder"))
        {
            canClimb = false;
        }
    }
    void FalconPunch()
    {
<<<<<<< HEAD
        canPunch = false;
        b = true;
        inmune = true;
        if(left){
            selPunch = 1;
            StartCoroutine(PunchRoutine(selPunch));
        } else{
            selPunch = 0;
            StartCoroutine(PunchRoutine(selPunch));
        }
        StartCoroutine(PunchWait());
        StartCoroutine(PunchShake());
    }

    IEnumerator PunchRoutine(int i){

        yield return new WaitForSeconds(0.6f);
        punchArray[selPunch].SetActive(true);


    }  
     IEnumerator PunchWait()
    {
        b = false;
        isPunching = true;
        canMove = false;
        yield return new WaitForSeconds(0.75f);
        canMove = true;
        isPunching = false;
        inmune = false;
        canPunch = true;
        punchArray[selPunch].SetActive(b);
    }

    IEnumerator PunchShake(){

        yield return new WaitForSeconds(0.6f);
        SoundManager.PlaySound(SoundManager.Sound.punch, 0.3f);
        shaker.Shake(0.25f);
        
        
    }

  

=======
        b = true;
        inmune = true;
        punch.SetActive(b);
        StartCoroutine(PunchWait());
    }

>>>>>>> montaje
    void AnimationHandler()
    {

        if (rb2d.velocity.x != 0)
        {
            isMoving = true;
        }
        else { isMoving = false; }
        if (rb2d.velocity.y != 0)
        {
            isClimbing = true;
        }
        else { isClimbing = false; }
        if (IsGrounded()) { isGrounded = true; } else { isGrounded = false; }

    }
<<<<<<< HEAD
 
=======
    IEnumerator PunchWait()
    {
        b = false;
        isPunching = true;
        canMove = false;
        yield return new WaitForSeconds(0.75f);
        canMove = true;
        isPunching = false;
        inmune = false;
        punch.SetActive(b);
    }
>>>>>>> montaje


    private void HandleMovement()
    {
        rb2d.gravityScale = 3;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else { isRunning = false; }
        if (canPunch && Input.GetKeyDown(KeyCode.Q) && isGrounded)
        {
            FalconPunch();
        }
        Burp();
        if (Input.GetKey(KeyCode.A) && canMove)
        {
<<<<<<< HEAD
            left = true;
            spr.flipX = true;
            
=======
            spr.flipX = true;
>>>>>>> montaje
            if (IsGrounded())
            {
                isWalking = true;
                rb2d.velocity = new Vector2(-MOVEMENT_SPEED, rb2d.velocity.y);
            }
            else
            {
                rb2d.velocity += new Vector2(-MOVEMENT_SPEED * midAirControl * Time.deltaTime, 0);
                rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -MOVEMENT_SPEED, +MOVEMENT_SPEED), rb2d.velocity.y);
            }
        }
        else if (Input.GetKey(KeyCode.D) && !jToMove && canMove)
        {
<<<<<<< HEAD
            left = false;
            spr.flipX = false;

=======
            spr.flipX = false;
>>>>>>> montaje
            if (IsGrounded())
            {
                isWalking = true;
                rb2d.velocity = new Vector2(MOVEMENT_SPEED, rb2d.velocity.y);
            }
            else
            {
                rb2d.velocity += new Vector2(+MOVEMENT_SPEED * midAirControl * Time.deltaTime, 0);
                rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -MOVEMENT_SPEED, +MOVEMENT_SPEED), rb2d.velocity.y);
            }
        }
        else if (Input.GetKey(KeyCode.J) && jToMove && canMove)
        {
<<<<<<< HEAD
            left = false;
=======
>>>>>>> montaje
            spr.flipX = false;
            if (IsGrounded())
            {
                isWalking = true;
                rb2d.velocity = new Vector2(MOVEMENT_SPEED, rb2d.velocity.y);
            }
            else
            {
                rb2d.velocity += new Vector2(+MOVEMENT_SPEED * midAirControl * Time.deltaTime, 0);
                rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -MOVEMENT_SPEED, +MOVEMENT_SPEED), rb2d.velocity.y);
            }
        }
        else
        {
            if (IsGrounded())
            {
                isWalking = false;
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
        }
    }

    private void HandleInvertedMovement()
    {
<<<<<<< HEAD
       rb2d.gravityScale = 3;
=======
        rb2d.gravityScale = 3;
>>>>>>> montaje

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else { isRunning = false; }
        if (canPunch && Input.GetKeyDown(KeyCode.Q) && isGrounded)
        {
            FalconPunch();
        }
        Burp();
        if (Input.GetKey(KeyCode.D) && canMove)
        {
<<<<<<< HEAD
            left = true;
            spr.flipX = true;
            
            if (IsGrounded())
            {
                isWalking = true;
=======
            spr.flipX = true;
            if (IsGrounded())
            {
>>>>>>> montaje
                rb2d.velocity = new Vector2(-MOVEMENT_SPEED, rb2d.velocity.y);
            }
            else
            {
                rb2d.velocity += new Vector2(-MOVEMENT_SPEED * midAirControl * Time.deltaTime, 0);
                rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -MOVEMENT_SPEED, +MOVEMENT_SPEED), rb2d.velocity.y);
            }
        }
        else if (Input.GetKey(KeyCode.A) && !jToMove && canMove)
        {
<<<<<<< HEAD
            left = false;
            spr.flipX = false;

            if (IsGrounded())
            {
                isWalking = true;
=======
            spr.flipX = false;
            if (IsGrounded())
            {
>>>>>>> montaje
                rb2d.velocity = new Vector2(MOVEMENT_SPEED, rb2d.velocity.y);
            }
            else
            {
                rb2d.velocity += new Vector2(+MOVEMENT_SPEED * midAirControl * Time.deltaTime, 0);
                rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -MOVEMENT_SPEED, +MOVEMENT_SPEED), rb2d.velocity.y);
            }
        }
        else if (Input.GetKey(KeyCode.J) && jToMove && canMove)
        {
<<<<<<< HEAD
            left = false;
            spr.flipX = false;
            if (IsGrounded())
            {
                isWalking = true;
=======
            if (IsGrounded())
            {
>>>>>>> montaje
                rb2d.velocity = new Vector2(MOVEMENT_SPEED, rb2d.velocity.y);
            }
            else
            {
                rb2d.velocity += new Vector2(+MOVEMENT_SPEED * midAirControl * Time.deltaTime, 0);
                rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -MOVEMENT_SPEED, +MOVEMENT_SPEED), rb2d.velocity.y);
            }
        }
        else
        {
            if (IsGrounded())
            {
<<<<<<< HEAD
                isWalking = false;
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
        } 
=======
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
        }

>>>>>>> montaje
    }

    private void HandleClimbMovement()
    {

        rb2d.gravityScale = 0;
        if (Input.GetKeyDown(KeyCode.W))
        {
            rb2d.velocity = new Vector2(0, MOVEMENT_SPEED);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            rb2d.velocity = new Vector2(0, -MOVEMENT_SPEED);

        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            rb2d.velocity = new Vector2(-MOVEMENT_SPEED, 0);

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            rb2d.velocity = new Vector2(MOVEMENT_SPEED, 0);

        }
        else
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y);


        }




    }

    private void HandleInvertedClimbMovement()
    {

        rb2d.gravityScale = 0;
        if (Input.GetKeyDown(KeyCode.S))
        {
            rb2d.velocity = new Vector2(0, MOVEMENT_SPEED);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            rb2d.velocity = new Vector2(0, -MOVEMENT_SPEED);

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            rb2d.velocity = new Vector2(-MOVEMENT_SPEED, 0);

        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            rb2d.velocity = new Vector2(MOVEMENT_SPEED, 0);

        }
        else
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y);


        }

    }

}



