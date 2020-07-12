using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Unity Components
    private Rigidbody2D rb2d;
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
    private bool inmune = false;
    bool b = false;

    public int hp = 3;
    //GAME CONTROL BOOLEANS
    // MOVEMENT
    private bool canBurp = true;
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
    bool left = false;
    int selPunch;

    // LEVEL 1
    public bool jToMove = false; //
    public bool canMove = true; //

    // LEVEL 2
    public bool invertedControls = false;//
    public bool canPunch = true;//

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bx2d = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
        shaker = FindObjectOfType<CamShake>();
        if (ps == null)
            ps = FindObjectOfType<PlayerStats>();
        initVars();
        punchArray[0].SetActive(b);
        punchArray[1].SetActive(b);

        ps.level = SceneManager.GetActiveScene().buildIndex;

    }
    
    public void ChangeBurp(){

        canBurp = !canBurp;

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
                HandleMovement();
                if (IsGrounded() && Input.GetKey(KeyCode.Space) && canMove)
                {
                    SoundManager.PlaySound(SoundManager.Sound.jump, 0.5f);
                    rb2d.velocity = Vector2.up * jumpVelocity;
                }
            }


            else
            {
                HandleInvertedMovement();
                if (IsGrounded() && Input.GetKey(KeyCode.Space) && canMove)
                {
                    SoundManager.PlaySound(SoundManager.Sound.jump, 0.5f);
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
        hp = 3;

    }

    //TO DO
    // WHEN CHANGING SCENES CALL UPDATESTATS();

    public void updateStats()
    {
    }



    public void TakeDamage()
    {
        if (!inmune)
        {
            SoundManager.PlaySound(SoundManager.Sound.takeDmg, 0.5f);

            shaker.Shake(0.25f);
            blood = GameAssets.i.ps[0];
            Instantiate(blood, transform.localPosition, transform.rotation);
            hp -= 1;
            StartCoroutine(Inmunity());
        }

        if (hp <= 0)
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

        int NUMBEROFDEATH = 6;
        int selectedScene;

        selectedScene = Random.Range(3, 6);

        rb2d.velocity = Vector2.zero;
        
        canMove = false;

        Debug.LogWarning("Player Died");
        SoundManager.PlaySound(SoundManager.Sound.charDeath, 0.1f);

        yield return new WaitForSeconds(0.5f);
        
        SceneManager.LoadScene(selectedScene);
    }

    public void kill() {
        StartCoroutine(Die());
    }

    public void Burp()
    {
        if (Input.GetKeyDown(KeyCode.E) && isGrounded && canBurp)
        {
            canBurp = false;
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
        shaker.Shake(0.5f);
        yield return new WaitForSeconds(0.75f);
        canMove = true;
        isBurping = false;
        inmune = false;
        canBurp = true;
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
            if (!inmune) SoundManager.PlaySound(SoundManager.Sound.golpetaso, 0.5f);

            TakeDamage();

        }

        if (col.gameObject.tag.Equals("EnemyBullet"))
        {
            TakeDamage();
            SoundManager.PlaySound(SoundManager.Sound.bullet, 0.5f);
            Destroy(col.gameObject);

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
        canPunch = false;
        b = true;
        inmune = true;
        if (left)
        {
            selPunch = 1;
            StartCoroutine(PunchRoutine(selPunch));
        }
        else
        {
            selPunch = 0;
            StartCoroutine(PunchRoutine(selPunch));
        }
        StartCoroutine(PunchWait());
        StartCoroutine(PunchShake());
    }

    IEnumerator PunchRoutine(int i)
    {

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

    IEnumerator PunchShake()
    {

        yield return new WaitForSeconds(0.6f);
        SoundManager.PlaySound(SoundManager.Sound.punch, 0.3f);
        shaker.Shake(0.25f);


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
            left = true;
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
            left = false;
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
            left = false;
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
            left = true;
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
        else if (Input.GetKey(KeyCode.A) && !jToMove && canMove)
        {
            left = false;
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
            left = false;
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


    public void nextScene()
    {
        updateStats();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Changing Room");

    }
    public void invertControls()
    {
        this.invertedControls =! invertedControls;

    }

}



