using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Rigidbody rb;
    public float jumpPower = 8f;
    State currentState;

    void Start()
    {
        Time.timeScale = 1;
        currentState = new StateRoll(this);
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ProcessMovement();
    }


    Vector3 moveDirection;
    bool right= false; 
    void ProcessMovement()
    { 
        currentState.Do();
    }
    public class StateRoll : State
    {
        public StateRoll(PlayerController pl)
        {
            this.pl = pl;
        }
        PlayerController pl;
        public override void Do()
        {
            bool jumpPressed = Input.GetButtonDown("Jump");
            if (jumpPressed)
            {
                pl.rb.velocity = new Vector3(pl.rb.velocity.x, pl.jumpPower, pl.rb.velocity.z);
                pl.currentState = new StateJump(pl);
                return;
            }
            if (pl.rb.velocity.y < -0.1)
            {
                pl.currentState=new StateFall(pl);
                return;
            }
            float leftRightInput = Input.GetAxis("Horizontal");
            if (leftRightInput > 0 && !pl.right)
            {
                pl.right = true;
                pl.rb.velocity = new Vector3(-pl.rb.velocity.x, pl.rb.velocity.y, pl.rb.velocity.z);
            }
            else if (leftRightInput < 0 && pl.right)
            {
                pl.right = false;
                pl.rb.velocity = new Vector3(-pl.rb.velocity.x, pl.rb.velocity.y, pl.rb.velocity.z);
            }
            pl.moveDirection = new Vector3(leftRightInput, 0, 1f);
            pl.moveDirection = pl.moveDirection * pl.moveSpeed;
            pl.rb.AddForce(pl.moveDirection);
        }
        public override string ToString() => "Roll";
    }
    public class StateJump : State 
    {
        PlayerController pl;
        public StateJump(PlayerController pl)
        {
            this.pl = pl;
        }
        public override void Do()
        {
            if (pl.rb.velocity.y < 0)
            {
                pl.currentState = new StateFall(pl);
                return;
            }
            float leftRightInput = Input.GetAxis("Horizontal");
            if (leftRightInput > 0 && !pl.right)
            {
                pl.right = true;
                pl.rb.velocity = new Vector3(-pl.rb.velocity.x, pl.rb.velocity.y, pl.rb.velocity.z);
            }
            else if (leftRightInput < 0 && pl.right)
            {
                pl.right = false;
                pl.rb.velocity = new Vector3(-pl.rb.velocity.x, pl.rb.velocity.y, pl.rb.velocity.z);
            }
            pl.moveDirection = new Vector3(leftRightInput, 0, 0.2f);
            pl.moveDirection = pl.moveDirection * pl.moveSpeed;
            pl.rb.AddForce(pl.moveDirection);
        }
        public override string ToString() => "Jump";
    }
    public class StateFall : State
    {
        PlayerController pl;

        public StateFall(PlayerController pl)
        {
            this.pl = pl;
        }
        public override void Do()
        {
            
            if (pl.rb.velocity.y > -0.1)
            {
                pl.currentState = new StateRoll(pl);
                return;
            }
            float leftRightInput = Input.GetAxis("Horizontal");
            if (leftRightInput > 0 && !pl.right)
            {
                pl.right = true;
                pl.rb.velocity = new Vector3(-pl.rb.velocity.x, pl.rb.velocity.y, pl.rb.velocity.z);
            }
            else if (leftRightInput < 0 && pl.right)
            {
                pl.right = false;
                pl.rb.velocity = new Vector3(-pl.rb.velocity.x, pl.rb.velocity.y, pl.rb.velocity.z);
            }
            pl.moveDirection = new Vector3(leftRightInput, 0, 0.2f);
            pl.moveDirection = pl.moveDirection * pl.moveSpeed;
            pl.rb.AddForce(pl.moveDirection);
        }
        public override string ToString() => "Fall";
    }

}

public class State {
    public virtual void Do() { }
    public override string ToString() => "State";
}

