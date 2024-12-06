using UnityEngine;
using System.Collections;

public class SimpleFollowCamara : EffectCamera
{

    public float interpVelocity;
    public float minDistance;
    public float followDistance;
    public PlayerController target;
    public Vector3 offset;
    Vector3 targetPos;
    // Use this for initialization
    void Start()
    {
        target = FindObjectOfType<PlayerController>();
        targetPos = transform.position;

        transform.position = targetPos;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            Vector3 posNoZ = transform.position;
            posNoZ.z = target.transform.position.z;

            Vector3 targetDirection = (target.transform.position - posNoZ);

            interpVelocity = targetDirection.magnitude * 5f;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);

        }
    }
}
