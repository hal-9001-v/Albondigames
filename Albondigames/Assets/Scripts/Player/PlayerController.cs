using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Unity Components
    private Rigidbody2D rb2d;
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
    private float midAirControl = 5f;
    float jumpVelocity = 20f;
    private bool inmune = false;
    bool b = false;

    public int hp = 3;
    public int dmg = 1;
    //GAME CONTROL BOOLEANS
    // MOVEMENT
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

    // LEVEL 1
    public bool jToMove = false; //
    public bool canMove = true; //
    public bool moveDCounterOn = false;//
    public int moveDCounter = 0;//

    // LEVEL 2
    public bool invertedControls = false;//
    public bool canPunch = true;//


    // LEVEL 3
    public bool moveACounterOn = false;//
    public int moveACounter = 0;//


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bx2d = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
        if (ps == null)
            ps = FindObjectOfType<PlayerStats>();
        punch = FindObjectOfType<Punch>();
        initVars();
        punch.SetActive(b);

       
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning) { MOVEMENT_SPEED = 15f; } else { MOVEMENT_SPEED = 10f; }
        AnimationHandler();
        if (!canClimb)
        {
            if (!invertedControls)
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
            else
            {
                HandleInvertedMovement();
                if (IsGrounded() && Input.GetKey(KeyCode.Space) && canMove)
                {
                    float jumpVelocity = 12f;
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



    public void TakeDamage()
    {
        if (!inmune)
        {
            //SoundManager.PlaySound(SoundManager.Sound.mariOomph, 0.5f);

            hp -= 1;
            StartCoroutine(Inmunity());
        }

        if (hp <= 0)
        {
            die();
        }
    }

    IEnumerator Inmunity()
    {
        inmune = true;
        yield return new WaitForSeconds(0.75f);
        inmune = false;
    }


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
        yield return new WaitForSeconds(0.75f);
        canMove = true;
        isBurping = false;
        inmune = false;
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

            TakeDamage();

        }
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
        b = true;
        inmune = true;
        punch.SetActive(b);
        StartCoroutine(PunchWait());
    }

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
            spr.flipX = true;
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
        else if (Input.GetKey(KeyCode.J) && jToMove && canMove)
        {
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
        if (Input.GetKey(KeyCode.D) && canMove)
        {
            spr.flipX = true;
            if (IsGrounded())
            {
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
            spr.flipX = false;
            if (IsGrounded())
            {
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
            if (IsGrounded())
            {
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
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
        }

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



