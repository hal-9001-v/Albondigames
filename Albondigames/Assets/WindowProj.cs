using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowProj : MonoBehaviour
{
    public float fallSpeed = 0.4f;
    public float lifeTime = 1;
    public bool throwing = false;
   public void SetActive(bool b){

        gameObject.SetActive(b);

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(0, -fallSpeed, 0);
        StartCoroutine(Dissapear(lifeTime));
    }

    IEnumerator Dissapear(float lifeTime)
    {
        throwing = true;
        yield return new WaitForSeconds(lifeTime);
        throwing = false;
        Destroy(gameObject);

    }


}
