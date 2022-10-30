using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerUseAbilityState
{
    private int jumpTimesLeft;
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationTriggerParameter) : base(player, stateMachine, playerData, animationTriggerParameter)
    {
        jumpTimesLeft = playerData.jumpTimes;
    }

    public override void Enter()
    {
        base.Enter();
        Player.SetYVelocity(PlayerData.jumpVelocity);
        isAbilityDone = true;
        Player.InAirState.SetIsRising();
        --jumpTimesLeft;
    }

    public bool CanJump()
    {
        return jumpTimesLeft > 0;
    }

    public void ResetJumpTimesLeft()
    {
        jumpTimesLeft = PlayerData.jumpTimes;
    }

    public void DecreaseJumpTimesLeft()
    {
        --jumpTimesLeft;
    }
}
