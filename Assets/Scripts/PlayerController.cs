using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f;
    [HideInInspector] public Rigidbody rb;
    public float jumpPower = 8f;
    StateMachine<PlayerController> stateMachine;

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody>();
        stateMachine = new(this, new StateRoll());
    }

    void Update()
    {
        if (!PauseManager.IsPaused)
        {
            ProcessMovement();
        }

    }



    [HideInInspector] public Vector3 moveDirection;
    [HideInInspector] public bool right= false; 
    void ProcessMovement()
    { 
        stateMachine.TriggerStateUpdate();
    }
    
    
    

}

