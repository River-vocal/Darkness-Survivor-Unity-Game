using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    
    #endregion

    #region Components
    public Rigidbody2D RigidBody;
    public Animator Animator { get; private set; }

    public PlayerInputHandler InputHandler { get; private set; }

    [SerializeField] private PlayerData playerData;

    #endregion

    #region Runtime Variables

    public Vector2 CurVelocity { get; private set; }
    public int FacingDirection { get; private set; }
    
    private Vector2 velocityHolder;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        //use same animation Trigger since we use blend for in air animation
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "wallJump");
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData, "ledgeClimb");
        DashState = new PlayerDashState(this, StateMachine, playerData, "dash");
        AttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RigidBody = GetComponent<Rigidbody2D>();
        FacingDirection = 1;
        StateMachine.Init(IdleState);
    }

    private void Update()
    {
        CurVelocity = RigidBody.velocity;
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
        RigidBody.velocity = velocityHolder;
        CurVelocity = velocityHolder;
    }
    
    public void SetYVelocity(float v)
    {
        velocityHolder.Set(CurVelocity.x, v);
        RigidBody.velocity = velocityHolder;
        CurVelocity = velocityHolder;
    }

    public void SetVelocity(float v, Vector2 direction)
    {
        direction.Normalize();
        velocityHolder.Set(v * direction.x, v * direction.y);
        RigidBody.velocity = velocityHolder;
        CurVelocity = velocityHolder;
    }
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0, 180, 0);
    }

    private void StartAnimation() => StateMachine.CurState.StartAnimation();

    private void AnimationFinished() => StateMachine.CurState.AnimationFinished();
    
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

    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.wallLayer);
    }

    public bool CheckIfBackTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.left * FacingDirection, playerData.backWallCheckDistance, playerData.wallLayer);
    }
    #endregion

    #region Gizmos

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, playerData.groundCheckDistance);
        Gizmos.DrawWireSphere(wallCheck.position, playerData.wallCheckDistance);
    }
    #endregion
}
