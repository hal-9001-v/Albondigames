using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGUI : MonoBehaviour
{
    Animator anim;
     public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = PlayerController.FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.hpArray[player.hp];
    }
}
