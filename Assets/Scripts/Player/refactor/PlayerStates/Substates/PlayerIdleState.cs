using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationTriggerParameter) : base(player, stateMachine, playerData, animationTriggerParameter)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetXVelocity(0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (movementInput.x != 0)
        {
            stateMachine.ChangeState(player.MoveState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Check()
    {
        base.Check();
    }
}
