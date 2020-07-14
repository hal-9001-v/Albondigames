using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windowanim : MonoBehaviour
{
    public Window window;
    Animator animator;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        animator = GetComponent<Animator>();
        window = GetComponentInParent<Window>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Throwing", window.isThrowing);
    }
}
