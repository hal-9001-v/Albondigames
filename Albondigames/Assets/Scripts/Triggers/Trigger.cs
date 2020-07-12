using UnityEngine;

class Trigger : MonoBehaviour
{
    [Range(0, 10)]
    public float range;

    public void triggerAction()
    {
        GetComponent<Triggerable>().trigger();
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
