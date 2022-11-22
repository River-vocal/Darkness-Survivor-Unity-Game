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
        Collider2D collision = Physics2D.OverlapCircle(Player.attackCheck.position, PlayerData.attackCheckDistance,
            PlayerData.enemyLayer);
        //trash code below
        if (collision != null)
        {
            // Player.VisualEffectSystemManager.GenerateHitSparklingEffect(collision.transform);
            Player.cinemachineImpulseSource.GenerateImpulse();
            Player.VisualEffectSystemManager.GenerateExplosionNovaFire(collision.transform);

            if (collision.tag == "Drop")
            {
                collision.GetComponent<EnemyDrops>().DropDeath();
            }
            if (collision.tag == "Golem")
            {
                collision.GetComponent<Golem>().GolemDeath();
            }
            if (collision.tag == "Projectile")
            {
                collision.GetComponent<GolemProjectile>().ProjectileDestroy();
            }
            else
            {
                Health health = collision.GetComponent<Health>();
                if (health) health.CurHealth -= PlayerData.attackDamage;
            }
        }
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
                Player.InputHandler.StopComboDetection();
            }
        }
    }
}