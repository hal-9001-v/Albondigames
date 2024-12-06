using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

    public string name;
    public string decription;
    public Sprite itemImage;
    public int itemParam;
    

}

public class itemHandler : MonoBehaviour
{

    public Item item;
    public PlayerController player;
    public float healthChange;
    public float movementSpeedChange;
    public float attackSpeedChange;
    public float meleeDamageChange;
    public float bulletSizeChange;
    public float bulletSpeedChange;
    public float bulletDamageChange;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.FindObjectOfType<PlayerController>();
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision == null) return;

        if (collision.tag == "Player")
        {
            if (player.hp == 12 && healthChange > 0)
            {

            }

            else if (healthChange > 0 && player.hp != 12)
            {

                player.heal(healthChange);

               Destroy(gameObject);

            }

            else if (healthChange == 0)
            {

                if(movementSpeedChange != 0) player.addSpeed(movementSpeedChange);

                if (bulletSizeChange != 0) player.addBulSize(bulletSizeChange);
                if (attackSpeedChange != 0) player.addBulDelay(attackSpeedChange);
                if (bulletSpeedChange != 0) player.addBulSpeed(bulletSpeedChange);
                if (bulletDamageChange != 0) player.addBulDmg(bulletDamageChange);
                if (meleeDamageChange != 0) player.addMelDmg(meleeDamageChange);
                Destroy(gameObject);

            }


        } 

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
