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

    public override void Enter(params Object[] args)
    {
        base.Enter();
        Player.JumpState.ResetJumpTimesLeft();
        Player.DashState.ResetCanDash();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        JumpInput = Player.InputHandler.JumpPressed;
        MovementInput = Player.InputHandler.MovementInput;
        
        if (JumpInput && Player.JumpState.CanJump())
        {
            StateMachine.ChangeState(Player.JumpState);
        }
        else if (!isGrounded)
        {
            Player.InAirState.StartCoyoteTime();
            StateMachine.ChangeState(Player.InAirState);
        }
        else if (Player.InputHandler.DashPressed && Player.DashState.CheckIfCanDash())
        {
            StateMachine.ChangeState(Player.DashState);
        }
        else if (Player.InputHandler.AttackComboIndex > 0)
        {
            StateMachine.ChangeState(Player.AttackState);
        }
        else if (Player.InputHandler.RangeAttackPressed)
        {
            StateMachine.ChangeState(Player.RangeAttackState);
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
