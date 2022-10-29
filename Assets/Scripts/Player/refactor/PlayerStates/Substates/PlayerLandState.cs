using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationTriggerParameter) : base(player, stateMachine, playerData, animationTriggerParameter)
    {
    }

    public override void Update()
    {
        base.Update();

        if ((int)movementInput.x != 0)
        {
            stateMachine.ChangeState(player.MoveState);
        }
        else if (animationFinished)
        {
            Debug.Log("land to idle!");
            stateMachine.ChangeState(player.IdleState);
        }
    }
}