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
    public TMP_Text finishText;
    public static int weight;
    public GameObject ball;
    int am;
    Rigidbody rb;
    public Canvas finishMenu;

    void Start()
    {
        weight = 10;
        text.text = weight.ToString();
        rb = GetComponent<Rigidbody>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            EnemyWeight enemyWeight=collision.gameObject.GetComponent<EnemyWeight>();
            if (enemyWeight.weight <= weight)
            {
                am= enemyWeight.weight* enemyWeight.weight/weight/10+1;
                Destroy(collision.gameObject);
                addWeight(am);
             
            }
            else
            {
                
                am = -weight/2/weight/enemyWeight.weight-2;
                addWeight(am);
                rb.AddForce(new Vector3(-rb.velocity.x*1.2f,-rb.velocity.y*1.2f,-(rb.velocity.z+0.4f)),ForceMode.Impulse);
            }
        }
        if (collision.gameObject.layer == 8)
        {
            EnemyWeight enemyWeight = collision.gameObject.GetComponent<EnemyWeight>();
            if (enemyWeight.weight > weight)
            {
                addWeight(-weight / 2);
                rb.AddForce(new Vector3(-rb.velocity.x * 2f, -rb.velocity.y * 2f, -(rb.velocity.z + 2f)), ForceMode.Impulse);
            }
            else { 
                rb.velocity *= 0;
                addWeight(weight);
            }
            finishText.text = weight.ToString();
            finishMenu.enabled= true;
            Time.timeScale = 0;
            PauseManager.IsPaused = true;

        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            addWeight(-2);
        }
    }

    void addWeight(int amount)
    {
        weight+= amount;
        if (weight < 2)
        {
            weight= 2;
        }
        text.text = weight.ToString();
        ball.transform.localScale = new Vector3(Mathf.Log(weight,2), Mathf.Log(weight, 2), Mathf.Log(weight, 2));
    }
}
