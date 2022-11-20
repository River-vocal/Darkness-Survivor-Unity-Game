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

    public override void Enter(params Object[] args)
    {
        base.Enter();
        if (jumpTimesLeft-- != PlayerData.jumpTimes)
        {
            Player.SetYVelocity(PlayerData.jumpVelocity * PlayerData.inAirJumpMultiplier);
        }
        else
        {
            Player.SetYVelocity(PlayerData.jumpVelocity);
        }
        isAbilityDone = true;
        Player.InputHandler.ConsumeJumpInput();
        Player.InAirState.SetIsRising();
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
