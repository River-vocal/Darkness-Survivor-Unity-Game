using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool JumpReleased { get; private set; }
    
    [SerializeField] private float jumpBufferTime = 0.1f;

    private float jumpBufferStartTime;

    public void Update()
    {
        if (JumpPressed && Time.time > jumpBufferStartTime + jumpBufferTime)
        {
            JumpPressed = false;
        }
    }
    public void OnMoveInput(InputAction.CallbackContext ctx)
    {
        MovementInput = ctx.ReadValue<Vector2>();
    }

    public void OnJumpInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            JumpPressed = true;
            JumpReleased = false;
            jumpBufferStartTime = Time.time;
        }

        if (ctx.canceled)
        {
            JumpReleased = true;
        }
    }

    public void ConsumeJumpInput()
    {
        JumpPressed = false;
    }
}
