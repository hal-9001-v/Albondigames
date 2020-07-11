using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowProj : MonoBehaviour
{
    public float fallSpeed = 0.4f;
    public float lifeTime = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(0, -fallSpeed, 0);
        StartCoroutine(Dissapear(lifeTime));
    }

    IEnumerator Dissapear(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

}
