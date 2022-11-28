using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerUseAbilityState
{
    private static readonly int AttackComboIndex = Animator.StringToHash("attackComboIndex");
    private float originalGravityScale;

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData,
        string animationTriggerParameter) : base(player, stateMachine, playerData, animationTriggerParameter)
    {
    }

    public override void Enter(params Object[] args)
    {
        base.Enter();
        Player.Animator.SetInteger(AttackComboIndex, Player.InputHandler.AttackComboIndex);
        Player.SetDrag(PlayerData.attackMovementDrag);
        Player.SetGravityScale(PlayerData.attackGravityScale);
        Player.InputHandler.ConsumeAttackInput();
        Player.InputHandler.ResetComboDetection();
    }

    public override void Exit()
    {
        base.Exit();
        Player.ResetDrag();
        Player.ResetGravityScale();
        Player.ParallaxController.ResetFollowing();
        Player.SwordAttackVFX.SetActive(false);
        Player.attackManager.set.Clear();
    }

    public override void Update()
    {
        base.Update();

        if (StateMachine.CurState == this)
        {
            if (animationFinished)
            {
                isAbilityDone = true;
                Player.InputHandler.StopComboDetection();
            }
        }
    }
}