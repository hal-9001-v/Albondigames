using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public float coolDown;
    private float lastShoot;
    public WindowProj proj;
    // Start is called before the first frame update
    void Start()
    {
        lastShoot = Time.time;
        proj.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if((Time.time - lastShoot) > coolDown)
        {
            Shoot();
            lastShoot = Time.time;
        }
    }

    void Shoot()
    {
        Instantiate(proj, transform.position + new Vector3(4.75f,-5,0), transform.rotation);

    }
}
