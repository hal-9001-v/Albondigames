using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTrigger : Trigger
{
    public bool Automatic;
    public bool onlyOnce;

    private bool done = false;

    [Range(0, 100)]
    public float range;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        getDialogue();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.y), new Vector2(transform.position.x, transform.position.y)) < range)
        {

            if (Automatic)
            {
                if (onlyOnce && !done)
                {
                    triggerDialogue();
                    done = true;
                    return;
                }
            }

            //!Automatic
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (done) return;

                if (onlyOnce) done = true;

                triggerDialogue();
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
