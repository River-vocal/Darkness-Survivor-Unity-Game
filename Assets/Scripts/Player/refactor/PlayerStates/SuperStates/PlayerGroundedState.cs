using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected Vector2 movementInput;
    protected bool jumpInput;
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationTriggerParameter) : base(player, stateMachine, playerData, animationTriggerParameter)
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
        jumpInput = player.InputHandler.JumpInput;
        if (jumpInput)
        {
            player.InputHandler.ConsumeJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        movementInput = player.InputHandler.MovementInput;
    }

    public override void Check()
    {
        base.Check();
    }
}
