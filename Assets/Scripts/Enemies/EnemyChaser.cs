using System.Collections;
using UnityEngine;

public class EnemyChaser : EnemyController
{
    private Vector2 moveDir;
    public Rigidbody2D rb;

    public int damage = 1;

    private bool moving = true;

    public float speed;
    public float restTime;

    private bool onImpact;

    Coroutine impactPID;

    public Animator anim;
    public SpriteRenderer spR;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        rb = GetComponent<Rigidbody2D>();

        initializeParent();
    }

    public override void takeDamageEffect()
    {

        if (impactPID != null)
        {
            StopCoroutine(impactPID);
        }

        if (gameObject.active)
            impactPID = StartCoroutine(OnImpactTimer());
    }
    private void Update()
    {
        flipSprt(spR);
    }



    void FixedUpdate()
    {
        if (moving)
        {
            if (onImpact)
            {
                rb.velocity = impactDirection.normalized * speed * 0.1f;
            }
            else
            {
                moveDir = (player.transform.position - gameObject.transform.position).normalized;
                rb.velocity = moveDir * speed;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {

        if ((hitInfo.gameObject.tag.Equals("AuxCollider")) && (moving = true))
            if (moving)
            {
                Debug.Log("Too slow!");
                StartCoroutine(Attack());

            }
    }

    IEnumerator Attack()
    {
        moving = false;
        anim.SetBool("hasHitted", true);
        player.GetComponent<PlayerController>().TakeDamage(damage);
        rb.velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(restTime);
        anim.SetBool("hasHitted", false);
        moving = true;
    }

    IEnumerator OnImpactTimer()
    {

        onImpact = true;

        yield return new WaitForSeconds(0.1f);

        onImpact = false;
    }


    protected override void startValues()
    {
        moving = true;
        hp = 10;
    }
    public override void Die()
    {

        SoundManager.PlaySound(SoundManager.Sound.medDies, 0.1f);

        gameObject.SetActive(false);
    }
}
