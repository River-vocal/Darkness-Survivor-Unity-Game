using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    
    #endregion

    #region Components
    private Rigidbody2D rb;
    public Animator Animator { get; private set; }

    public PlayerInputHandler InputHandler { get; private set; }

    [SerializeField] private PlayerData playerData;

    #endregion

    #region Runtime Variables

    public Vector2 CurVelocity { get; private set; }
    public int FacingDirection { get; private set; }
    
    private Vector2 velocityHolder;
    [SerializeField] private Transform groundCheck;

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody2D>();
        FacingDirection = 1;
        StateMachine.Init(IdleState);
    }

    private void Update()
    {
        CurVelocity = rb.velocity;
        StateMachine.CurState.Update();
    }

    private void FixedUpdate()
    {
        StateMachine.CurState.FixedUpdate();
    }

    #endregion
    
    #region Setters

    public void SetXVelocity(float v)
    {
        velocityHolder.Set(v, CurVelocity.y);
        rb.velocity = velocityHolder;
        CurVelocity = velocityHolder;
    }
    
    public void SetYVelocity(float v)
    {
        velocityHolder.Set(CurVelocity.x, v);
        rb.velocity = velocityHolder;
        CurVelocity = velocityHolder;
    }
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0, 180, 0);
    }
    #endregion

    #region Checkers

    public void CheckIfShouldFlip(int x)
    {
        if (x != 0 && x != FacingDirection)
        {
            Flip();
        }
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckDistance, playerData.groundLayer);
    }

    #endregion

    #region Gizmos

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, playerData.groundCheckDistance);
    }

    #endregion
}
