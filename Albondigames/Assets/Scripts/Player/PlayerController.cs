using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Unity Components
    public Rigidbody2D rb2d;
    private BoxCollider2D bx2d;
    [SerializeField] private LayerMask lm;
    [SerializeField] private LayerMask ladm;
    PlayerStats ps;
    ParticleSystem blood;
    public Punch[] punchArray = new Punch[2];
    public SpriteRenderer spr;
    public CamShake shaker;
    public CameraController camera;
    //Character attributes
    private Vector3 moveDir;
    private float MOVEMENT_SPEED = 10f;
    private float xtraheight = .1f;
    private float midAirControl = 5f;
    float jumpVelocity = 20f;
    private bool inmune = false;
    bool b = false;
    public int level = 0;
    public int hp = 3;
    //GAME CONTROL BOOLEANS
    // MOVEMENT
    public bool canBurp = true;
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
    bool forceWalk = false;
    int selPunch;
    public Vector3 balconPos;
    // LEVEL 1
    public bool jToMove = false; //
    public bool canMove = true; //

    // LEVEL 2
    public bool invertedControls = false;//
    public bool canPunch = true;//


    //CheckPoints
    public bool lvl1CheckPointReached = false;
    public bool lvl2CheckPointReached = false;
    public bool lvl3CheckPointReached = false;

    public GameObject[] PD1;

    private void Awake()
    {
        camera = FindObjectOfType<CameraController>();
        PD1 = GameObject.FindGameObjectsWithTag("START");
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

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
    int i = 0;
    if(lvl1CheckPointReached && ps.level==1){
        camera.checkPointBool = false;
        foreach (GameObject g in PD1){
            if(PD1 != null) PD1[i].SetActive(false);
            i++;
        }
        gameObject.transform.position = new Vector3(20f, -2.14f, gameObject.transform.position.z);
    } 
    if (lvl2CheckPointReached && ps.level == 2 ){
            foreach (GameObject g in PD1){
            if(PD1 != null) PD1[i].SetActive(false);
            i++;
        }        gameObject.transform.position = new Vector3(85f, -3f, gameObject.transform.position.z);
    }
    if(lvl3CheckPointReached  && ps.level==3){
         foreach (GameObject g in PD1){
            if(PD1 != null) PD1[i].SetActive(false);
            i++;
        }
        gameObject.transform.position = new Vector3(26.2f, -1.65f, gameObject.transform.position.z);
    }
    }
    
    
    public void ChangeBurp(){

        canBurp = !canBurp;

    }

  

    // Update is called once per frame
    void Update()
    {
        
        if(IsGrounded()){
            balconPos = gameObject.transform.position;
        }
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
    lvl1CheckPointReached= ps.lvl1CheckPointReached;
    lvl2CheckPointReached= ps.lvl2CheckPointReached;
    lvl3CheckPointReached =  ps.lvl3CheckPointReached;
    }

    //TO DO
    // WHEN CHANGING SCENES CALL UPDATESTATS();

    public void updateStats()
    {
        ps.level = level;
        ps.lvl1CheckPointReached = lvl1CheckPointReached;
        ps.lvl2CheckPointReached = lvl2CheckPointReached;
        ps.lvl3CheckPointReached = lvl3CheckPointReached;
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
        spr.enabled = false;
        int NUMBEROFDEATH = 6;
        int selectedScene;

        
        rb2d.velocity = Vector2.zero;
        
        canMove = false;

        //Debug.LogWarning("Player Died");
        SoundManager.PlaySound(SoundManager.Sound.charDeath, 0.1f);

        yield return new WaitForSeconds(0.5f);

        if( ps.level == 1){
        SceneManager.LoadScene("End1");
        } else if ( ps.level == 2) {
        SceneManager.LoadScene("End2");
        } else if ( ps.level == 3) {
        SceneManager.LoadScene("End3");
        } else {
        SceneManager.LoadScene("End1");
        }

    
    }

  public void ActivateCheckpoint(){
        int selectedScene;

        selectedScene = ps.level;

        if(selectedScene == 1){
        ps.lvl1CheckPointReached = true;
        lvl1CheckPointReached = true;
        } else if (selectedScene == 2) {
        ps.lvl2CheckPointReached = true;
        lvl2CheckPointReached = true;;

        } else if (selectedScene == 3) {
            ps.lvl3CheckPointReached = true;
            lvl3CheckPointReached = true;
        }
        
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

          if (col.gameObject.tag.Equals("Hostia"))
        {
            SoundManager.PlaySound(SoundManager.Sound.punch, 0.5f);
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

public void FalconPunch()
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

public void PunchAnim(float f){

    
        StartCoroutine(PunchWaitAnim(f));

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

    IEnumerator PunchWaitAnim(float f)
    {
        b = false;
        yield return new WaitForSeconds(f);
        StartCoroutine(PunchShake());
        isPunching = true;
        canMove = false;
        yield return new WaitForSeconds(0.75f);
        isPunching = false;
        inmune = false;
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
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
        }

        if(rb2d.velocity.x == 0 && !forceWalk){
            isWalking = false;
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

        float moveX = 0;
        float moveY = 0;
        MOVEMENT_SPEED = 10;
        rb2d.gravityScale = 0;
        if (Input.GetKey(KeyCode.W))
        {            
            moveY = 10f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveY = -10f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveX = -10f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX = 10f;
        }
       

        moveDir = new Vector3 (moveX,moveY).normalized;


    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if(canClimb)
        rb2d.velocity = moveDir*MOVEMENT_SPEED;
    }

    private void HandleInvertedClimbMovement()
    {

       
        float moveX = 0;
        float moveY = 0;
        MOVEMENT_SPEED = 10;
        rb2d.gravityScale = 0;
        if (Input.GetKey(KeyCode.S))
        {            
            moveY = 10f;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            moveY = -10f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX = -10f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveX = 10f;
        }
       

        moveDir = new Vector3 (moveX,moveY).normalized;


    }

    public void BalconDamage(){
        
        TakeDamage();
        gameObject.transform.position = balconPos;

    }

    
    public void nextScene()
    {
        level++;
        updateStats();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Changing Room");

    }
    public void invertControls()
    {
        this.invertedControls =! invertedControls;

    }


    public void GetUpAnimationHelper(){

        StartCoroutine(GetUpAnimationHelperNumerator());

    }

    IEnumerator GetUpAnimationHelperNumerator(){
        yield return new WaitForSeconds(1f);
        isLieing = true;
        yield return new WaitForSeconds(1.5f);
        Debug.Log("lmao");
        isLed = true;
                yield return new WaitForSeconds(6f);
        Debug.Log("lmao2");
        isGettingUp = true;
        isLed = false;
        isLieing = false;

        
    }

    public void ForceWalk(){
        forceWalk = true;
    
    }

    public void StopForceWalk(){

        forceWalk = false;

    }

   
}



