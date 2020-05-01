using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaser : EnemyController
{
    private Vector2 moveDir;
    public float speed;
    public Rigidbody2D rb;
    public int damage;
    public bool moving;
    public float restTime;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (moving)
        {
            moveDir = (player.transform.position - gameObject.transform.position).normalized;
            gameObject.GetComponent<Rigidbody2D>().velocity = moveDir * speed;
        }
        
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if ((hitInfo.gameObject.tag.Equals("AuxCollider")) && (moving = true))
        {
            Debug.Log("Too slow!");
            StartCoroutine(Attack());
            
        } 
    }

    IEnumerator Attack()
    {
        moving = false;
        player.GetComponent<PlayerController>().TakeDamage(damage);
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(restTime);
        moving = true;
    }
}
