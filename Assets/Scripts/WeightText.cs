using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(EnemyWeight))]
public class WeightText : MonoBehaviour
{
    private EnemyWeight weight;
    public TMP_Text text;


    public GameObject enemy;
    void Start()
    {
        text.text = weight.weight.ToString();
    }
    private void Awake()
    {
        weight= GetComponent<EnemyWeight>();
    }
}
