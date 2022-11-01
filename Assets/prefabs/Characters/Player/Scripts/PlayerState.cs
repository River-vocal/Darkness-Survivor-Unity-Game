using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player Player;
    protected PlayerStateMachine StateMachine;
    protected PlayerData PlayerData;

    protected float StateStartTime;

    protected bool animationFinished;
    private string animationTriggerParameter;
    
    protected PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationTriggerParameter)
    {
        this.Player = player;
        this.StateMachine = stateMachine;
        this.PlayerData = playerData;
        this.animationTriggerParameter = animationTriggerParameter;
    }

    public virtual void Enter()
    {
        Check();
        Player.Animator.SetBool(animationTriggerParameter, true);
        StateStartTime = Time.time;
        animationFinished = false;
        // Debug.Log("entering " + animationTriggerParameter);
    }

    public virtual void Exit()
    {
        Player.Animator.SetBool(animationTriggerParameter, false);
    }

    public virtual void Update()
    {
        
    }

    public virtual void FixedUpdate()
    {
        Check();
    }

    //gets called from physics update and enter
    public virtual void Check()
    {
        
    }

    //call backs for animator to use
    public virtual void StartAnimation()
    {
        
    }

    public virtual void AnimationFinished()
    {
        animationFinished = true;
        // Debug.Log("animation finished!");
    }
}
