using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public float coolDown;
    private float lastShoot;
    public WindowProj proj;
<<<<<<< HEAD
    PlayerController player;
     private void Awake() {
    player = FindObjectOfType<PlayerController>();    
    }
=======
>>>>>>> montaje
    // Start is called before the first frame update
    void Start()
    {
        lastShoot = Time.time;
<<<<<<< HEAD
        proj.SetActive(true);
=======
>>>>>>> montaje
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
<<<<<<< HEAD
        Instantiate(proj, transform.position + new Vector3(4.75f,-5,0), transform.rotation);
        //Debug.Log(Vector2.Distance(gameObject.transform.position, player.transform.position));

        if(Vector2.Distance(gameObject.transform.position, player.transform.position) < 50f){
                SoundManager.PlaySound(SoundManager.Sound.throwing, 0.3f);
            }

=======
        Instantiate(proj);
>>>>>>> montaje
    }
}
