using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  
    private Rigidbody2D rb2d;
    private Vector3 moveDir;
    private float MOVEMENT_SPEED = 10f;
    private BoxCollider2D bx2d;
    [SerializeField] private LayerMask lm;
    string solo = "Space";

    //GAME CONTROL BOOLEANS
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


    private void Awake(){
    rb2d = GetComponent<Rigidbody2D>();
    bx2d = GetComponent<BoxCollider2D>();
}

    // Update is called once per frame
    void Update()
    {
        if(!invertedControls) {
            
            if ((moveDCounterOn && moveDCounter> 0) | (moveACounterOn && moveACounter > 0) ){
                
                if(moveDCounterOn && Input.GetKeyDown(KeyCode.D)){
                moveDCounter--;
                } else  if(moveACounterOn && Input.GetKeyDown(KeyCode.A)){
                moveACounter--;
                }  
                } else {
                    moveDCounterOn = false;
                    moveACounterOn = false;
                    moveDCounter = 5;
                    moveACounter = 5;
                    HandleMovement();
                    if(IsGrounded() && Input.GetKey(KeyCode.Space) && canMove) {
                    float jumpVelocity = 12f;
                    rb2d.velocity = Vector2.up * jumpVelocity;
                }
                
        }
        
        } else {
            HandleInvertedMovement();
            if(IsGrounded() && Input.GetKey(KeyCode.Space) && canMove) {
                float jumpVelocity = 12f;
                rb2d.velocity = Vector2.up * jumpVelocity;
            }
        
            }
    }

    private void FixedUpdate() {
    }

    public void Punch(){

        //TO DO

    }

    public void Burp(){
   if(Input.GetKeyDown(KeyCode.E)) {
            float r = Random.Range(1, 5);
               Debug.Log(r);
            switch (r) {
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
        }
    }


   private bool IsGrounded(){
        float xtraheight = .1f;
        RaycastHit2D rcht =  Physics2D.Raycast(bx2d.bounds.center, Vector2.down, bx2d.bounds.extents.y + xtraheight, lm);
        Color rc;
        if(rcht.collider != null) {
            rc = Color.green;
        } else {
            rc = Color.red;
        }
        Debug.DrawRay(bx2d.bounds.center, Vector2.down * (bx2d.bounds.extents.y + xtraheight), rc);
        return rcht.collider != null;
   }

    private void HandleMovement(){
        if (canPunch){
            Punch();
        }
        float midAirControl = 5f;
        Burp();
        if (Input.GetKey(KeyCode.A) && canMove) {
            if(IsGrounded()){
            rb2d.velocity = new Vector2( -MOVEMENT_SPEED, rb2d.velocity.y);
            } else {
            rb2d.velocity += new Vector2( -MOVEMENT_SPEED*midAirControl* Time.deltaTime, 0);
            rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -MOVEMENT_SPEED, +MOVEMENT_SPEED), rb2d.velocity.y);
            }
        } else if(Input.GetKey(KeyCode.D) && !jToMove && canMove)  {
                        if(IsGrounded()){
            rb2d.velocity = new Vector2( MOVEMENT_SPEED, rb2d.velocity.y);
            } else {
            rb2d.velocity += new Vector2( +MOVEMENT_SPEED *midAirControl * Time.deltaTime, 0);
            rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -MOVEMENT_SPEED, +MOVEMENT_SPEED), rb2d.velocity.y);
            }
        } else if (Input.GetKey(KeyCode.J) && jToMove && canMove) {
               if(IsGrounded()){
            rb2d.velocity = new Vector2( MOVEMENT_SPEED, rb2d.velocity.y);
            } else {
            rb2d.velocity += new Vector2( +MOVEMENT_SPEED *midAirControl * Time.deltaTime, 0);
            rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -MOVEMENT_SPEED, +MOVEMENT_SPEED), rb2d.velocity.y);
            }
        }else {
            if(IsGrounded()) {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
        }
    }

private void HandleInvertedMovement(){
      
if (canPunch){
            Punch();
        }
        float midAirControl = 5f;
        Burp();
        if (Input.GetKey(KeyCode.D) && canMove) {
            if(IsGrounded()){
            rb2d.velocity = new Vector2( -MOVEMENT_SPEED, rb2d.velocity.y);
            } else {
            rb2d.velocity += new Vector2( -MOVEMENT_SPEED*midAirControl* Time.deltaTime, 0);
            rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -MOVEMENT_SPEED, +MOVEMENT_SPEED), rb2d.velocity.y);
            }
        } else if(Input.GetKey(KeyCode.A) && !jToMove && canMove)  {
                        if(IsGrounded()){
            rb2d.velocity = new Vector2( MOVEMENT_SPEED, rb2d.velocity.y);
            } else {
            rb2d.velocity += new Vector2( +MOVEMENT_SPEED *midAirControl * Time.deltaTime, 0);
            rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -MOVEMENT_SPEED, +MOVEMENT_SPEED), rb2d.velocity.y);
            }
        } else if (Input.GetKey(KeyCode.J) && jToMove && canMove) {
               if(IsGrounded()){
            rb2d.velocity = new Vector2( MOVEMENT_SPEED, rb2d.velocity.y);
            } else {
            rb2d.velocity += new Vector2( +MOVEMENT_SPEED *midAirControl * Time.deltaTime, 0);
            rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -MOVEMENT_SPEED, +MOVEMENT_SPEED), rb2d.velocity.y);
            }
        }else {
            if(IsGrounded()) {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
        }


    }

private void HandleClimbMovement(){



}

private void HandleInvertedClimbMovement(){



}


}
