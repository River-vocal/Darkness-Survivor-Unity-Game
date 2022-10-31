using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseAbilityState : PlayerState
{
    protected bool isAbilityDone;
    private bool isGrounded;
    public PlayerUseAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationTriggerParameter) : base(player, stateMachine, playerData, animationTriggerParameter)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (isAbilityDone)
        {
            if (isGrounded && Player.CurVelocity.y <= 0)
            {
                if ((int)Player.InputHandler.MovementInput.x == 0)
                {
                    StateMachine.ChangeState(Player.IdleState);
                }
                else
                {
                    StateMachine.ChangeState(Player.MoveState);
                }
            }
            else
            {
                StateMachine.ChangeState(Player.InAirState);
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
        isGrounded = Player.CheckIfGrounded();
    }
}
