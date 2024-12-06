using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakBullet : EnemyBullet
{


    public override  void virtualStart()
    {
        dir = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        float rot_z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);

    }

    public virtual void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (!hitInfo.gameObject.tag.Equals("Enemy") && !hitInfo.gameObject.tag.Equals("AuxCollider"))
        {
            if (hitInfo.gameObject.tag.Equals("Player"))
            {
                PlayerController player = hitInfo.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.TakeDamage(damage);
                }

            }
            if (!hitInfo.gameObject.tag.Equals("Enemy") && !hitInfo.gameObject.tag.Equals("Bullet") && !hitInfo.gameObject.tag.Equals("Muebles") && !hitInfo.gameObject.tag.Equals("Item") && !hitInfo.gameObject.tag.Equals("zone") && !hitInfo.gameObject.tag.Equals("Boss")) Destroy(this.gameObject);

        }
    }
}
