using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACABScripts : MonoBehaviour
{


    public PlayerController playerGO;

    private Vector2 moveDirection;
    public float chaseDistance = 10000000000;
    public float moveSpeed = 5;
    public int hp = 6;

    public float dmgCooldown = 1f;
    private float lastHit;
    public float hitForce = 3;
    public bool isHit = false;
    public float knockDownTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.FindObjectOfType<PlayerController>();
        moveDirection = Vector2.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isHit)
        {
            if (Mathf.Abs(playerGO.transform.position.x - gameObject.transform.position.x) <= Mathf.Abs(chaseDistance))
            {
                if (playerGO.transform.position.x - gameObject.transform.position.x < 0)
                {
                    moveDirection.x = -1;
                }
                else
                {
                    moveDirection.x = 1;
                }

                gameObject.GetComponent<Rigidbody2D>().velocity = moveSpeed * moveDirection;
            }
        }
        else
        {
            Vector2 knockBack = new Vector2(-moveDirection.x * hitForce, 0);
            gameObject.GetComponent<Rigidbody2D>().velocity = knockBack;
        }

        if (hp <= 0)
        {
            StartCoroutine(die());
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Punch") && ((Time.time - lastHit) > dmgCooldown))
        {
            StartCoroutine(takeDamage());
            StartCoroutine(knockDownController());
        }

    }

    IEnumerator die()
    {
        yield return null;
        Destroy(gameObject);
    }

    IEnumerator takeDamage()
    {            
        ParticleSystem blood;
        blood = GameAssets.i.ps[2];
        
            Instantiate(blood, transform.position + new Vector3(0,2,0), transform.rotation);
        SoundManager.PlaySound(SoundManager.Sound.acabDmg, 0.5f);
        hp -= 1;
        isHit = true;
        yield return new WaitForSeconds(knockDownTime);

    }

    IEnumerator knockDownController()
    {
        yield return new WaitForSeconds(dmgCooldown);
        isHit = false;
    }
}


