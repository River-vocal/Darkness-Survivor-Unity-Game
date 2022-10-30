using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected Vector2 MovementInput;
    protected bool JumpInput;

    private bool isGrounded;
    
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationTriggerParameter) : base(player, stateMachine, playerData, animationTriggerParameter)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.JumpState.ResetJumpTimesLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        JumpInput = Player.InputHandler.JumpInput;
        MovementInput = Player.InputHandler.MovementInput;
        
        if (JumpInput && Player.JumpState.CanJump())
        {
            Player.InputHandler.ConsumeJumpInput();
            StateMachine.ChangeState(Player.JumpState);
        }
        else if (!isGrounded)
        {
            Player.InAirState.StartCoyoteTime();
            StateMachine.ChangeState(Player.InAirState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Check()
    {
        base.Check();
        isGrounded = Player.CheckIfGrounded();
    }
}
