using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{

    Transform target;
    Vector3 initialPos;
    float pendingShakeDuration = 0f;
    bool isShaking = false;
    float intensity = 0.5f;
    void Start()
    {
        target = GetComponent<Transform>();
        initialPos = target.localPosition;
    }


    public void Shake(float duration){
        if(duration > 0)
        {
            pendingShakeDuration += duration;
        }

    }

     private void Update() {
         if(pendingShakeDuration >0 && !isShaking){
            StartCoroutine(DoShake());
         }
    }

    IEnumerator DoShake(){

        isShaking = true;
        var startTime = Time.realtimeSinceStartup;
        while(Time.realtimeSinceStartup < startTime + pendingShakeDuration){

            var randomPoint = new Vector3(Random.Range(-1,1f) * intensity, Random.Range(-1f,1f) * intensity, initialPos.z);
            target.localPosition = randomPoint;
            yield return null;

        }

        target.localPosition = initialPos;
        pendingShakeDuration = 0f;
        isShaking = false;

    }
}
