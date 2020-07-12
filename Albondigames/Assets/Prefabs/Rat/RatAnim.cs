using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatAnim : MonoBehaviour
{
    Animator animator;
    SpeedTester speed;
    // Start is called before the first frame update
    void Start()
    {
         speed = FindObjectOfType<SpeedTester>();
         animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Moving", speed.TestSpeed());//
    }
}
