using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOutOfControlState : PlayerUseAbilityState
{
    private static readonly int OutOfControl = Animator.StringToHash("outOfControl");

    public PlayerOutOfControlState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationTriggerParameter) : base(player, stateMachine, playerData, animationTriggerParameter)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.SetVelocity(Vector2.zero);
    }

    public override void Update()
    {
        base.Update();

        if (StateMachine.CurState == this)
        {
            if (animationFinished)
            {
                Player.Animator.SetBool(OutOfControl, false);
            }
        }
    }

    public void SetAbilityDone()
    {
        isAbilityDone = true;
    }
}
