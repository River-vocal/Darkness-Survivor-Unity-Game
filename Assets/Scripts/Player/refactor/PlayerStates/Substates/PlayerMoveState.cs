using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationTriggerParameter) : base(player, stateMachine, playerData, animationTriggerParameter)
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
        player.CheckIfShouldFlip((int)movementInput.x);
        player.SetXVelocity(playerData.movementVelocity * movementInput.x);

        if (movementInput.x == 0f)
        {
            player.StateMachine.ChangeState(player.IdleState);
        }
        
        if (jumpInput)
        {
            player.InputHandler.ConsumeJumpInput();
            stateMachine.ChangeState(player.JumpState);
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
