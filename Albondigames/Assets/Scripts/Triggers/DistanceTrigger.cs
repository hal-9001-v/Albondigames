using UnityEngine;


class DistanceTrigger : Trigger
{
    bool done;
    public bool automatic;
    public bool onlyOnce;

    Vector3 playerPosition;

    public KeyCode interactionKey;

    private void Start()
    {
        //playerPosition = FindGameObjectOfType()
    }

    private void Update()
    {
        if (Vector2.Distance(new Vector2(playerPosition.x, playerPosition.y), new Vector2(playerPosition.x, playerPosition.y)) < range)
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
