using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] public VoidEventChannel useEventChannel;
    public Vector2 MovementInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool JumpReleased { get; private set; }
    public bool DashPressed { get; private set; }

    public int AttackComboIndex { get; private set; } = 0;
    
    [SerializeField] private float jumpBufferTime = 0.2f;
    [SerializeField] private float dashBufferTime = 0.2f;

    private float jumpBufferStartTime;
    private float dashBufferStartTime;
    private bool comboDetectionOn = true;
    private int numberOfAttackAnimations = 4;
    
    public void Update()
    {
        if (JumpPressed && Time.time > jumpBufferStartTime + jumpBufferTime)
        {
            JumpPressed = false;
        }

        if (DashPressed && Time.time > dashBufferStartTime + dashBufferTime)
        {
            DashPressed = false;
        }
    }

    public void OnUseInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            useEventChannel.Broadcast();
        }
    }
    public void OnMoveInput(InputAction.CallbackContext ctx)
    {
        MovementInput = ctx.ReadValue<Vector2>();
        if (ctx.started)
        {
            InterruptAttack();
        }
    }

    public void OnJumpInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            JumpPressed = true;
            JumpReleased = false;
            jumpBufferStartTime = Time.time;
            InterruptAttack();
        }

        if (ctx.canceled)
        {
            JumpReleased = true;
        }
    }

    public void OnDashInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            DashPressed = true;
            dashBufferStartTime = Time.time;
            InterruptAttack();
        }
    }

    public void OnAttackInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started && AttackComboIndex <= 0)
        {
            if (AttackComboIndex < 0 && comboDetectionOn)
            {
                AttackComboIndex = (-AttackComboIndex) % numberOfAttackAnimations + 1;
            }
            else
            {
                AttackComboIndex = 1;
            }
        }
    }

    public void ConsumeJumpInput()
    {
        JumpPressed = false;
    }

    public void ConsumeDashInput()
    {
        DashPressed = false;
    }

    public void ConsumeAttackInput()
    {
        if (AttackComboIndex > 0)
        {
            AttackComboIndex = -AttackComboIndex;
        }
    }

    public void ResetComboDetection()
    {
        comboDetectionOn = true;
    }
    public void StopComboDetection()
    {
        comboDetectionOn = false;
    }

    private void InterruptAttack()
    {
        ConsumeAttackInput();
        StopComboDetection();
    }
}
