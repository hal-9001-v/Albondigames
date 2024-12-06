using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RaBossFight : MonoBehaviour
{

    public  float deadTime = 0;
    public float safeTime = 0.01f;
    public bool active = true;
    public int counter = 20;
    public SpriteRenderer sp;
    public float speed;
   public  PlayerController player;
    Vector2 iniScale;

    public UnityEvent atDead;
    // Start is called before the first frame update


    void Start()
    {
        sp.GetComponent<SpriteRenderer>().sprite = null;
        player = FindObjectOfType<PlayerController>();
        iniScale = sp.GetComponent<SpriteRenderer>().GetComponent<Collider2D>().transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {


    }


    void FixedUpdate() {
        exec();

        if (sp.GetComponent<SpriteRenderer>().sprite == null)
        {

            sp.GetComponent<SpriteRenderer>().GetComponent<Collider2D>().transform.localScale = new Vector2(0, 0);

        }
        else {

            sp.GetComponent<SpriteRenderer>().GetComponent<Collider2D>().transform.localScale = iniScale;


        }


    }

    void exec()
    {

        if (deadTime > 5)
        {
            counter--;
            deadTime = 0;
            //Aparece zona
            sp.GetComponent<SpriteRenderer>().sprite = GameAssets.i.raZone[0];
            SoundManager.PlaySound(SoundManager.Sound.raDanceOn, 0.5f);
           StartCoroutine(Move());

            if (safeTime < 0)
            {
                safeTime = 0.01f;
                danceCheck();

                sp.GetComponent<SpriteRenderer>().sprite = null;
                SoundManager.PlaySound(SoundManager.Sound.raDanceOff, 0.5f);
            }
            else
            {

                safeTime -= Time.deltaTime;

            }

            if (counter == 0) {

                //Se acaba el boss
                atDead.Invoke();
                enabled = false;

            }
        }
        else
        {

            deadTime += Time.deltaTime;

        }

     



    }




    void danceCheck()
    {


        if (player.dance != true && sp.GetComponent<SpriteRenderer>().sprite != null)
        {
            player.TakeDamage(1);
            SoundManager.PlaySound(SoundManager.Sound.raDanceDmg, 0.5f);

        }
    }

    

    IEnumerator Move()
    {
        int dir = UnityEngine.Random.Range(0, 4);
        yield return new WaitForSeconds(0f);

        Vector2 temp = new Vector2(Random.Range(-6.6f, 6.6f), Random.Range(-2.6f, 2.6f));
        sp.transform.position = temp;

    }

}

