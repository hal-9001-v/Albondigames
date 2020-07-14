using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemys : MonoBehaviour
{
    public GameObject[] goList;
    // Start is called before the first frame update
    public void spawn()
    {
        foreach(GameObject go in goList)
        {
            go.active = true;
        }
    }

    public void DeSpawn(){

        foreach(GameObject go in goList)
        {
            go.active = false;
        }

    }
}
