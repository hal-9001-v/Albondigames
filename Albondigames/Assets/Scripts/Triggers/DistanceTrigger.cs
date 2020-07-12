using UnityEngine;


class DistanceTrigger : Trigger
{
    bool done;
    public bool automatic;
    public bool onlyOnce;

    GameObject player;

    public KeyCode interactionKey;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.y), new Vector2(transform.position.x, transform.position.y)) < range)
        {
            if (automatic)
            {
                if (onlyOnce && !done)
                {
                    triggerAction();
                    done = true;
                    return;
                }
            }

            //!Automatic
            if (Input.GetKeyDown(interactionKey))
            {
                if (done) return;

                if (onlyOnce) done = true;

                triggerAction();
            }

        }
    }
}
