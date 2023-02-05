using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//we only stay in TakeDamageState for the duration of knockback animation time, blinking and invulnerable are handled
//by coroutine. Use animationEvent to trigger exiting state, so that player can move while blinking and invulnerable
public class PlayerKouchokuState : PlayerUseAbilityState
{
    public PlayerKouchokuState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationTriggerParameter) : base(player, stateMachine, playerData, animationTriggerParameter)
    {
    }


    public override void Enter(params Object[] args)
    {
        base.Enter();
        
        //knock back
        Player.SetVelocity(Vector2.zero);
        // var sender = (GameObject)parameters[0];

        
    }

    public override void Update()
    {
        base.Update();

        if (StateMachine.CurState == this)
        {
            if (animationFinished)
            {
                isAbilityDone = true;
            }
        }
    }
    
}