using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyWeight))]
public class BossScript : MonoBehaviour
{
    EnemyWeight weight;
    void Start()
    {
        weight= GetComponent<EnemyWeight>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer==6)
        {
            if (Collect.weight > weight.weight)
                Collect.weight *= 2;
            else
                Collect.weight /= 2;
            ShowFinishWindow();
        }
    }
    private void ShowFinishWindow()
    {

    }
}
