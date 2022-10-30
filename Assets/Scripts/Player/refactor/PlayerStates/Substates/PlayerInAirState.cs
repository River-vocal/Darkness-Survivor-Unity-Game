using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isBackTouchingWall;
    private float xInput;
    private static readonly int YVelocity = Animator.StringToHash("yVelocity");
    private static readonly int InAir = Animator.StringToHash("inAir");
    private bool coyoteTime;
    private bool isRising;
    
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationTriggerParameter) : base(player, stateMachine, playerData, animationTriggerParameter)
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
        CheckCoyoteTime();
        ApplyVariableJumpHeight();
        xInput = Player.InputHandler.MovementInput.x;
        
        if (isGrounded && Player.CurVelocity.y <= 0)
        {
            StateMachine.ChangeState(Player.LandState);
        }
        else if ((isTouchingWall || isBackTouchingWall) && Player.InputHandler.JumpPressed)
        {
            Player.WallJumpState.SetWallJumpDirection(isTouchingWall);
            StateMachine.ChangeState(Player.WallJumpState);
        }
        else if (Player.InputHandler.JumpPressed && Player.JumpState.CanJump())
        {
            StateMachine.ChangeState(Player.JumpState);
        }
        else if (isTouchingWall && Player.CurVelocity.y <= 0)
        {
            StateMachine.ChangeState(Player.WallSlideState);
        }
        else
        {
            Player.CheckIfShouldFlip((int)xInput);
            Player.SetXVelocity(xInput * PlayerData.movementVelocity);
            Player.Animator.SetFloat(YVelocity, Player.CurVelocity.y);
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
        isTouchingWall = Player.CheckIfTouchingWall();
        isBackTouchingWall = Player.CheckIfBackTouchingWall();
    }

    public void StartCoyoteTime()
    {
        coyoteTime = true;
    }

    public void SetIsRising()
    {
        isRising = true;
    }
    
    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > StateStartTime + PlayerData.coyoteTime)
        {
            coyoteTime = false;
            Player.JumpState.DecreaseJumpTimesLeft();
        }
    }

    private void ApplyVariableJumpHeight()
    {
        if (isRising)
        {
            if (Player.CurVelocity.y <= 0)
            {
                isRising = false;
            }
            else if (Player.InputHandler.JumpReleased)
            {
                Player.SetYVelocity(Player.CurVelocity.y * PlayerData.variableJumpHeightMultiplier);
            }
        }

    }
}