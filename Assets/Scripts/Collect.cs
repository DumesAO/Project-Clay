using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Collect : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text text;
    public int weight = 10;
    public GameObject ball;
    
    void Start()
    {
        text.text = weight.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Debug.Log(collision.gameObject.tag);
            if (Int32.Parse(collision.gameObject.tag) < weight)
            {
                weight += Int32.Parse(collision.gameObject.tag);
                text.text=weight.ToString();
                Destroy(collision.gameObject);
                ball.transform.localScale =new Vector3(ball.transform.localScale.x+ Int32.Parse(collision.gameObject.tag) * 0.001f, ball.transform.localScale.y+ Int32.Parse(collision.gameObject.tag) * 0.001f, ball.transform.localScale.z+ Int32.Parse(collision.gameObject.tag) * 0.001f);
            }
        }
    }
}
