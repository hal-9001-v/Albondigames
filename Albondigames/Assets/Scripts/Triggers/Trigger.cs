using UnityEngine;

class Trigger : MonoBehaviour
{
    private Triggerable t;
    [Range(0, 10)]
    public float range;

    private void Start()
    {
        t = GetComponent<Triggerable>();
    }

    public void triggerAction()
    {
        t.trigger();
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
