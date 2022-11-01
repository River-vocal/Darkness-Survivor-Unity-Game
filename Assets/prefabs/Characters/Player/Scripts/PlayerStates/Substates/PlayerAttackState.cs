using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerUseAbilityState
{
    private static readonly int AttackComboIndex = Animator.StringToHash("attackComboIndex");
    private float originalGravityScale;
    
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationTriggerParameter) : base(player, stateMachine, playerData, animationTriggerParameter)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.Animator.SetInteger(AttackComboIndex, Player.InputHandler.AttackComboIndex);
        Player.RigidBody.drag = PlayerData.attackMovementDrag;
        originalGravityScale = Player.RigidBody.gravityScale;
        Player.RigidBody.gravityScale = PlayerData.attackGravityScale;
        Player.InputHandler.ConsumeAttackInput();
        Player.InputHandler.ResetComboDetection();
    }

    public override void Exit()
    {
        base.Exit();
        Player.RigidBody.drag = 0f;
        Player.RigidBody.gravityScale = originalGravityScale;
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
            else
            {
            }
        }
    }
}