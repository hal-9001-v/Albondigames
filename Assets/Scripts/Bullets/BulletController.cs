using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int damage = 2;

    public float bulletTime;

    public Animator anim;

    public Vector2 dir;

    public SpriteRenderer spR;

    Vector2 initPosition;
    void Start()
    {
        StartCoroutine(BulletLife());

        initPosition = transform.position;

        float rot_z = Mathf.Atan2(dir.x, -dir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (!hitInfo.gameObject.tag.Equals("Player") && !hitInfo.gameObject.tag.Equals("AuxCollider")) {
            
            if (hitInfo.tag.Equals("Enemy"))
            {
                EnemyController ec = hitInfo.GetComponent<EnemyController>();

                ec.setImpactVector(initPosition);
                ec.takeDamage(damage);

            }

            if (hitInfo.tag.Equals("Boss"))
            {
                LokiBehaviour ec = hitInfo.GetComponent<LokiBehaviour>();
                ec.takeDamage(damage);

            }
            if (!hitInfo.gameObject.tag.Equals("Player") && !hitInfo.gameObject.tag.Equals("Bullet") && !hitInfo.gameObject.tag.Equals("EnemyBullet") && !hitInfo.gameObject.tag.Equals("Muebles") && !hitInfo.gameObject.tag.Equals("Item") && !hitInfo.gameObject.tag.Equals("zone")) 
            StartCoroutine(destroyBullet());
        }
    }

    IEnumerator destroyBullet()
    {
        anim.SetBool("hasImpacted", true);
        SoundManager.PlaySound(SoundManager.Sound.bulletSound,0.05f);
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
    }

    IEnumerator BulletLife()
    {
        yield return new WaitForSeconds(bulletTime);
        Destroy(this.gameObject);
    }
}
