using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleTrigger : MonoBehaviour
{
    [Range(0, 10)]
    public float range;
    public UnityEvent actions;

    bool once;

    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if ((Vector3.Distance(player.transform.position, transform.position) < range)){

            trigger();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

    protected void trigger() {
        if (!once)
        {
            actions.Invoke();
            once = true;
        }
    }

}
