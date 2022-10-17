using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool isGrounded;
    private float xInput;
    private static readonly int YVelocity = Animator.StringToHash("yVelocity");
    private static readonly int InAir = Animator.StringToHash("inAir");

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationTriggerParameter) : base(player, stateMachine, playerData, animationTriggerParameter)
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
        xInput = player.InputHandler.MovementInput.x;
        
        if (isGrounded && player.CurVelocity.y <= 0)
        {
            player.Animator.SetBool(InAir, false);
            stateMachine.ChangeState(player.LandState);
        }
        else
        {
            player.CheckIfShouldFlip((int)xInput);
            player.SetXVelocity(xInput * playerData.movementVelocity);
            player.Animator.SetFloat(YVelocity, player.CurVelocity.y);
            player.Animator.SetBool(InAir, true);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Check()
    {
        base.Check();
        isGrounded = player.CheckIfGrounded();
    }
}