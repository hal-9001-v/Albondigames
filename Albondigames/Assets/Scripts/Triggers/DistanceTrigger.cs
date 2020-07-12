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
        player = GameObject.FindObjectOfType<PlayerController>().gameObject;
    }

    private void Update()
    {

        if (onlyOnce && done)
            return;

        if (Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.y), new Vector2(transform.position.x, transform.position.y)) < range)
        {
            if (automatic)
            {
                    Debug.Log("Hoi");
                    triggerAction();
                    done = true;
                    return;
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
