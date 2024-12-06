using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHandler : MonoBehaviour
{
    EnemyController[] children;

    // Start is called before the first frame update
    void Start()
    {
        children = GetComponentsInChildren<EnemyController>();

        deactiveChildren();
    }

    public void activeChildren()
    {   
        if (children == null) return;
        
        foreach (EnemyController child in children) {
            child.gameObject.SetActive(true);
  
        }
    }

    public void deactiveChildren() {
        if (children == null) return;

        foreach (EnemyController child in children)
        {
            child.restore();
            child.gameObject.SetActive(false);
        }
    }

}
