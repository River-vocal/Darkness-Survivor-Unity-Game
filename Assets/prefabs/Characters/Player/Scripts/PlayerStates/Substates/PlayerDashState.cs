using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerUseAbilityState
{
    public bool CanDash { get; private set; }
    private float lastDashTime = 0f;
    private bool isTouchingWall;
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationTriggerParameter) : base(player, stateMachine, playerData, animationTriggerParameter)
    {
    }

    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= lastDashTime + PlayerData.dashCoolDown;
    }

    public void ResetCanDash()
    {
        CanDash = true;
    }

    public override void Enter()
    {
        base.Enter();
        CanDash = false;
        Player.InputHandler.ConsumeDashInput();
        Player.RigidBody.drag = PlayerData.dashDrag;
        //change facing direction if necessary
        Player.CheckIfShouldFlip((int)Player.InputHandler.MovementInput.x);
        Player.SetXVelocity(PlayerData.dashVelocity * Player.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
        lastDashTime = Time.time;
        Player.RigidBody.drag = 0f;
    }

    public override void Update()
    {
        base.Update();
        
        if (StateMachine.CurState == this)
        {
            if (animationFinished || isTouchingWall)
            {
                isAbilityDone = true;
            }
            else
            {
                Player.SetYVelocity(Player.CurVelocity.y * PlayerData.dashYVelocityMultiplier);
            }
        }
    }

    public override void Check()
    {
        base.Check();

        isTouchingWall = Player.CheckIfTouchingWall();
    }
}
