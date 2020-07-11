using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RatBehaviour : MonoBehaviour
{
    public PlayerController playerGO;

    private Vector2 moveDirection;
    public float chaseDistance = 5;
    public float moveSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.FindObjectOfType<PlayerController>();
        moveDirection = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
       if(Mathf.Abs(playerGO.transform.position.x - gameObject.transform.position.x) <= Mathf.Abs(chaseDistance))
       {
            if(playerGO.transform.position.x - gameObject.transform.position.x < 0)
            {
                moveDirection.x = -1;
            }
            else{
                moveDirection.x = 1;
            }

            gameObject.GetComponent<Rigidbody2D>().velocity = moveSpeed * moveDirection;
       }
    }
}
