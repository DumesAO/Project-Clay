using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject spawner;
    public GameObject sObject;
    public int chance;
    public bool random;
    public int minWeight;
    public int maxWeight;
    public int weight;
    int ac;
    float x;
    int rW;
    Vector3 pos;
    void Update()
    {
        
        
    }
    private void FixedUpdate()
    {
        if (!PauseManager.IsPaused)
        {
            ac = Random.Range(1, 100);
            if (ac <= chance)
            {
                x = Random.Range(-0.4f, 0.4f);
                EnemyWeight enemyWeight=sObject.GetComponent<EnemyWeight>();
                if (random)
                {
                    rW =Collect.weight+Random.Range(minWeight, maxWeight);
                    rW = System.Math.Max(1, rW);
                    enemyWeight.weight= rW;
                }
                else
                {
                    enemyWeight.weight=weight;
                }
                pos = new Vector3(x, 0.1f, spawner.transform.position.z + 8);
                if (Physics.OverlapBox(pos, new Vector3(0.02f, 0.02f, 0.02f)).Length >= 1)
                {
                    return;
                }
                Spawn(sObject, pos);
            }
        }
    }

    void Spawn(GameObject go,Vector3 position)
    {
        Instantiate(go,position,go.transform.rotation);
    }
}
