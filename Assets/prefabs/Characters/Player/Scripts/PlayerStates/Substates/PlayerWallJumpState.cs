using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerUseAbilityState
{
    private int wallJumpDirection;
    private bool isGrounded;
    private bool isTouchingWall;
    
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationTriggerParameter) : base(player, stateMachine, playerData, animationTriggerParameter)
    {
    }

    public override void Enter(params Object[] args)
    {
        base.Enter();
        
        Player.JumpState.ResetJumpTimesLeft();
        Player.JumpState.DecreaseJumpTimesLeft();
        Player.CheckIfShouldFlip(wallJumpDirection);
        Player.SetVelocity(PlayerData.wallJumpVelocity, new Vector2(PlayerData.wallJumpAngle.x * wallJumpDirection, PlayerData.wallJumpAngle.y));
        Player.InputHandler.ConsumeJumpInput();
        Player.InAirState.SetIsRising();
    }

    public override void Update()
    {
        base.Update();

        if (StateMachine.CurState == this)
        {
            // if (Time.time >= StateStartTime + PlayerData.wallJumpTime)
            // {
            //     isAbilityDone = true;
            // }
            // //todo: what if player touches ground or wall before isAbilityDone is set to true?

            if (animationFinished || isGrounded || Player.CheckIfTouchingWall())
            {
                isAbilityDone = true;
            }
        }
    }

    public override void Check()
    {
        base.Check();
        isGrounded = Player.CheckIfGrounded();
        isTouchingWall = Player.CheckIfTouchingWall();
    }

    public void SetWallJumpDirection(bool isTouchingWall)
    {
        wallJumpDirection = (isTouchingWall ? -Player.FacingDirection : Player.FacingDirection);
    }
    
}
