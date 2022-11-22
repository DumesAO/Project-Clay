using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public float offset;
    public float speed;
    public GameObject camera;
   
    void Update()
    {
        ProcessMovement();
    }

    void ProcessMovement()
    {
        Vector3 targetPos = new Vector3(camera.transform.position.x, camera.transform.position.y, target.transform.position.z + offset);
        Vector3 Follow = Vector3.Lerp(camera.transform.position,targetPos, speed);

        transform.position = Follow;
    }
}
