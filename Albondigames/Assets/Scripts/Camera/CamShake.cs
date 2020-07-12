using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{

    public Transform target;
    public Vector3 initialPos;
    float pendingShakeDuration = 0f;
    bool isShaking = false;
    float intensity = 0.5f;
    public Vector3 randomPoint;
    void Start()
    {
        target = GetComponent<Transform>();
    }


    public void Shake(float duration){
        if(duration > 0)
        {
            pendingShakeDuration += duration;
        }

    }

     private void Update() {
         if(!isShaking) initialPos = target.localPosition;
         if(pendingShakeDuration >0 && !isShaking){
            StartCoroutine(DoShake());
         }
    }

    IEnumerator DoShake(){

        isShaking = true;
        var startTime = Time.realtimeSinceStartup;
        while(Time.realtimeSinceStartup < startTime + pendingShakeDuration){

             randomPoint = new Vector3(Random.Range(-1,1f) * intensity, Random.Range(0f,1f) * intensity, 0);
            target.localPosition += randomPoint;
            yield return null;

        }
        
        target.localPosition = initialPos;
        pendingShakeDuration = 0f;
        isShaking = false;

    }
}
