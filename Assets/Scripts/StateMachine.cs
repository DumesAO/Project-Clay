using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class StateMachine<TContext> 
{
    private readonly TContext _context;
    private IState<TContext> _currentState;

    public StateMachine(TContext context, IState<TContext> initialState)
    {
        _currentState = initialState;
        _context = context;

        _currentState.OnStateBegin(_context);
    }

    public void SwitchStateTo(IState<TContext> newState)
    {
        _currentState.OnStateEnd(_context);

        _currentState = newState;

        _currentState.OnStateBegin(_context);
    }

    public void TriggerStateUpdate()
    {
        _currentState.OnUpdate(_context);

        _currentState.DoTransitions(_context,this);
    }
}
public interface IState<TContext>
{
    void OnStateBegin(TContext context);
    void OnUpdate(TContext context);
    void OnStateEnd(TContext context);

    void DoTransitions(TContext context,StateMachine<TContext> machine);
}

public sealed class StateRoll : IState<PlayerController>
{
    public void OnStateBegin(PlayerController context) {
        Debug.Log("State Roll Beins");

    }
    public void OnStateEnd(PlayerController context) {
        Debug.Log("State Roll Ends");
    }
    public void OnUpdate(PlayerController context)
    {
        float leftRightInput = Input.GetAxis("Horizontal");
        if (leftRightInput > 0 && !context.right)
        {
            context.right = true;
            context.rb.velocity = new Vector3(-context.rb.velocity.x, context.rb.velocity.y, context.rb.velocity.z);
        }
        else if (leftRightInput < 0 && context.right)
        {
            context.right = false;
            context.rb.velocity = new Vector3(-context.rb.velocity.x, context.rb.velocity.y, context.rb.velocity.z);
        }
        context.moveDirection = new Vector3(leftRightInput*2, 0, 1);
        context.moveDirection = context.moveDirection * context.moveSpeed;
        context.rb.AddForce(context.moveDirection);
        context.rb.velocity = new Vector3(context.rb.velocity.x, context.rb.velocity.y, Math.Clamp(context.rb.velocity.z, -2, 2));
    }

    public void DoTransitions(PlayerController context, StateMachine<PlayerController> machine)
    {
        bool jumpPressed = Input.GetButtonDown("Jump");
        if (jumpPressed)
        {
            machine.SwitchStateTo(new StateJump());
            return;
        }
        if (context.rb.velocity.y < -0.1)
        {
            machine.SwitchStateTo(new StateFall());
            return;
        }
    }
}

public sealed class StateJump : IState<PlayerController>
{
    public void OnStateBegin(PlayerController context) {
        Debug.Log("State Jump Beins");
        context.rb.AddForce(new Vector3(0, context.jumpPower*100, 0));
        context.rb.useGravity = false;
    }
    public void OnStateEnd(PlayerController context) {
        Debug.Log("State Jump Ends");
        context.rb.useGravity=true;
    }
    public void OnUpdate(PlayerController context)
    {
        float leftRightInput = Input.GetAxis("Horizontal");
        if (leftRightInput > 0 && !context.right)
        {
            context.right = true;
            context.rb.velocity = new Vector3(-context.rb.velocity.x, context.rb.velocity.y, context.rb.velocity.z);
        }
        else if (leftRightInput < 0 && context.right)
        {
            context.right = false;
            context.rb.velocity = new Vector3(-context.rb.velocity.x, context.rb.velocity.y, context.rb.velocity.z);
        }
        context.moveDirection = new Vector3(leftRightInput * 2, 0, 1);
        context.moveDirection = context.moveDirection * context.moveSpeed;
        context.rb.AddForce(context.moveDirection);
        Vector3 gravity = -9.81f * 0.7f * Vector3.up;
        context.rb.AddForce(gravity);
        context.rb.velocity = new Vector3(context.rb.velocity.x, context.rb.velocity.y, Math.Clamp(context.rb.velocity.z,-2, 2));
    }

    public void DoTransitions(PlayerController context, StateMachine<PlayerController> machine)
    {
        if (context.rb.velocity.y < -0.1)
        {
            machine.SwitchStateTo(new StateFall());
            return;
        }
    }
}
public sealed class StateFall : IState<PlayerController>
{
    public void OnStateBegin(PlayerController context) {
        Debug.Log("State Fall Beins");
    }
    public void OnStateEnd(PlayerController context) {
        Debug.Log("State Fall Ends");
    }
    public void OnUpdate(PlayerController context)
    {
        float leftRightInput = Input.GetAxis("Horizontal");
        if (leftRightInput > 0 && !context.right)
        {
            context.right = true;
            context.rb.velocity = new Vector3(-context.rb.velocity.x, context.rb.velocity.y, context.rb.velocity.z);
        }
        else if (leftRightInput < 0 && context.right)
        {
            context.right = false;
            context.rb.velocity = new Vector3(-context.rb.velocity.x, context.rb.velocity.y, context.rb.velocity.z);
        }
        context.moveDirection = new Vector3(leftRightInput * 2, 0, 1);
        context.moveDirection = context.moveDirection * context.moveSpeed;
        context.rb.AddForce(context.moveDirection);
        context.rb.velocity = new Vector3(context.rb.velocity.x, context.rb.velocity.y, Math.Clamp(context.rb.velocity.z, -2, 2));
    }

    public void DoTransitions(PlayerController context, StateMachine<PlayerController> machine)
    {
        if (context.rb.velocity.y > -0.1)
        {
            machine.SwitchStateTo(new StateRoll());
            return;
        }
    }
}

