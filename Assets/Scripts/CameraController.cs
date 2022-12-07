using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public float offset;
    public float speed;
    public GameObject gCamera;
   
    void Update()
    {
        ProcessMovement();
    }

    void ProcessMovement()
    {
        Vector3 targetPos = new Vector3(gCamera.transform.position.x, gCamera.transform.position.y, target.transform.position.z + offset);
        Vector3 Follow = Vector3.Lerp(gCamera.transform.position,targetPos, speed);

        transform.position = Follow;
    }
}
