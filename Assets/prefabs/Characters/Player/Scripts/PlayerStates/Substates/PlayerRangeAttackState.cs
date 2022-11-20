using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangeAttackState : PlayerUseAbilityState
{
    public PlayerRangeAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationTriggerParameter) : base(player, stateMachine, playerData, animationTriggerParameter)
    {
    }


    public override void Enter(params Object[] args)
    {
        base.Enter(args);
        
        Player.SetDrag(PlayerData.attackMovementDrag);
        Player.SetGravityScale(PlayerData.attackGravityScale);
        Player.InputHandler.ConsumeRangeAttackInput();
        
        //instantiate here
        
        Player.Instantiate(Player.ParticleSystemManager.playerBulletPrefab, Player.attackCheck.position, Player.attackCheck.rotation);
        Player.playerBulletCount--;
        Debug.Log("Player.playerBulletCount: " + Player.playerBulletCount);
    }

    public override void Exit()
    {
        base.Exit();
        
        Player.ResetDrag();
        Player.ResetGravityScale();
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
