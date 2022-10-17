using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerUseAbilityState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationTriggerParameter) : base(player, stateMachine, playerData, animationTriggerParameter)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetYVelocity(playerData.jumpVelocity);
    }
}
