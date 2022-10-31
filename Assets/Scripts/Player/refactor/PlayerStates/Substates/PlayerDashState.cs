using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerUseAbilityState
{
    public bool CanDash { get; private set; }
    private float lastDashTime;
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
        Player.SetXVelocity(PlayerData.dashVelocity * Player.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
        lastDashTime = Time.time;
    }

    public override void Update()
    {
        base.Update();
        
        if (StateMachine.CurState == this)
        {
            //early quit
        }
    }
}
