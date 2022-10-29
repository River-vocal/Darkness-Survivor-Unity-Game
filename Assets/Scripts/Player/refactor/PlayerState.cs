using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected float startTime;

    protected bool animationFinished;
    private string animationTriggerParameter;
    
    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationTriggerParameter)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animationTriggerParameter = animationTriggerParameter;
    }

    public virtual void Enter()
    {
        Check();
        player.Animator.SetBool(animationTriggerParameter, true);
        startTime = Time.time;
        animationFinished = false;
        Debug.Log("entering " + animationTriggerParameter);
    }

    public virtual void Exit()
    {
        player.Animator.SetBool(animationTriggerParameter, false);
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
    public virtual void startAnimation()
    {
        
    }

    public virtual void AnimationFinished()
    {
        animationFinished = true;
    }
}
