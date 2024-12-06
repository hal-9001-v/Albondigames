using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    public int hp;
    private int MAXHP;

    Vector3 startPosition;

    protected GameObject player;
    protected Vector2 impactDirection;

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
    }

    //Put this at the end of start function of child
    protected void initializeParent()
    {
        saveStatus();
    }

    //Room Handling
    protected void saveStatus() {
        MAXHP = hp;
        startPosition = transform.position;
    }

    protected abstract void startValues();

    public void restore() {
        hp = MAXHP;
        transform.position = startPosition;

        startValues();
    }

    // Update is called once per frame
    public void takeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }

        takeDamageEffect();

    }

    public abstract void takeDamageEffect();

    public virtual void Die()
    {
        gameObject.SetActive(false);
    }

    
    public void setImpactVector(Vector2 hitPosition) {
        impactDirection = (new Vector2(transform.position.x, transform.position.y) - hitPosition).normalized;
    }

    public void flipSprt(SpriteRenderer spR)
    {
        if (player.transform.position.x > gameObject.transform.position.x)
        {
            spR.flipX = true;
        }
        else
        {
            spR.flipX = false;
        }
    }


  
}
