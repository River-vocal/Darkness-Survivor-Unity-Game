using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationTriggerParameter) : base(player, stateMachine, playerData, animationTriggerParameter)
    {
    }

    public override void Enter(params Object[] args)
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
        
        if (StateMachine.CurState == this)
        {
            Player.CheckIfShouldFlip((int)MovementInput.x);
            Player.SetXVelocity(PlayerData.movementVelocity * MovementInput.x);
            if (MovementInput.x == 0f)
            {
                Player.StateMachine.ChangeState(Player.IdleState);
            }
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
