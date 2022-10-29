using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }
    public bool JumpInput { get; private set; }

    [SerializeField] private float jumpBufferTime = 0.1f;

    private float jumpBufferStartTime;

    public void Update()
    {
        if (Time.time > jumpBufferStartTime + jumpBufferTime)
        {
            JumpInput = false;
        }
    }
    public void OnMoveInput(InputAction.CallbackContext ctx)
    {
        MovementInput = ctx.ReadValue<Vector2>();
    }

    public void OnJumpInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            JumpInput = true;
            jumpBufferStartTime = Time.time;
        }
    }

    public void ConsumeJumpInput()
    {
        JumpInput = false;
    }
}
