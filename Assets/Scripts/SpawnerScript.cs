using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject spawner;
    public GameObject sObject;
    public int weight;
    int ac;
    float x;

    void Update()
    {
        
        ac=Random.Range(1, 100);
        if (ac > 90)
        {
            x=Random.Range(-0.4f, 0.4f);
            sObject.tag = weight.ToString();
            Spawn(sObject,new Vector3(x,0.1f,spawner.transform.position.z+7));
        }
    }

    void Spawn(GameObject go,Vector3 position)
    {
        Instantiate(go,position,go.transform.rotation);
    }
}
