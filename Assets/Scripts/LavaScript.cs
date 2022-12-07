using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LavaScript : MonoBehaviour
{
    public TMP_Text text;



    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Collect.weight -= 2;
            text.text = Collect.weight.ToString();
        }
    }
}
