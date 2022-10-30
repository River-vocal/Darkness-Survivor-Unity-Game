using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    protected bool IsGrounded;
    protected bool IsTouchingWall;
    protected PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationTriggerParameter) : base(player, stateMachine, playerData, animationTriggerParameter)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (IsGrounded)
        {
            StateMachine.ChangeState(Player.IdleState);
        }
        else if (!IsTouchingWall || (int)Player.InputHandler.MovementInput.x == -Player.FacingDirection)
        {
            Player.CheckIfShouldFlip((int)Player.InputHandler.MovementInput.x);
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
        IsGrounded = Player.CheckIfGrounded();
        IsTouchingWall = Player.CheckIfTouchingWall();
    }

    public override void StartAnimation()
    {
        base.StartAnimation();
    }

    public override void AnimationFinished()
    {
        base.AnimationFinished();
    }
}
