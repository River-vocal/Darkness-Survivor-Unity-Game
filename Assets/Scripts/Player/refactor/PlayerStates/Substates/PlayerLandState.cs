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

        if (StateMachine.CurState == this)
        {
            if ((int)MovementInput.x != 0)
            {
                StateMachine.ChangeState(Player.MoveState);
            }
            else if (animationFinished)
            {
                StateMachine.ChangeState(Player.IdleState);
            }
        }
    }
}