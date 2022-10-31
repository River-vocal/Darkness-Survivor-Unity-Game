using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationTriggerParameter) : base(player, stateMachine, playerData, animationTriggerParameter)
    {
    }

    public override void Update()
    {
        base.Update();

        if (StateMachine.CurState == this)
        {
            if (Player.InputHandler.JumpPressed)
            {
                Player.WallJumpState.SetWallJumpDirection(IsTouchingWall);
                StateMachine.ChangeState(Player.WallJumpState);
            }
            else
            {
                Player.SetXVelocity(0f);
                Player.SetYVelocity(-PlayerData.wallSlideVelocity);
            }
        }
    }
}
