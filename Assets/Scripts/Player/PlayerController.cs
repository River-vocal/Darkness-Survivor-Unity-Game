using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")] 
    [SerializeField] private int jumpChances = 3;
    [SerializeField] private float maxRunSpeedOnGround = 10f;
    [SerializeField] private float airMaxSpeedFactor = 0.2f;

    //velocity change per fixedUpdate timeInterval
    [SerializeField] private float velocityAccelerationPerFixedUpdate = 10f;

    [SerializeField] private float velocityDecelerationPerFixedUpdate = 20f;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float timeToJumpToHeighest = 0.4f;
    [SerializeField][Range(0f, 1)] private float airAccelerationFactor = 0.2f;
    [SerializeField][Range(0f, 1)] private float airDecelerationFactor = 0.2f;
    [SerializeField] private float fallGravityMult = 1.2f;
    [SerializeField] private float fastFallGravityMult = 1.3f;

    public bool isFacingRight = true;


    [Header("Layer Settings")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private Health health;

    //runtime variables
    private Rigidbody2D body;
    private CapsuleCollider2D capsuleCollider2D;
    private Animator animator;

    private Vector2 movementInput;
    private bool canMove = true;
    private bool jumpPressed = false;
    private bool jumpReleased = true;
    private int remainingJumpChances;
    private float gravityStrength;
    private float gravityScale;
    private float jumpImpulse;
    private float accelerationForceFactor;
    private float decelerationForceFactor;
    [SerializeField] bool onGround;
    [SerializeField] private bool isTouchingWall;
    [SerializeField] private bool isWallSliding;
    [SerializeField] private float wallSlideSpeed = 2f;
    [SerializeField] private float variableJumpHeightMultiplier = 0.5f;
    private int velocityDirectionAtJump = 0;

    //attack related
    public float attackRange = 0.5f;
    public int attackDamage = 10;

    public int BulletCount = 3;
    private static readonly int IsMoving = Animator.StringToHash("isMoving");

    private void Awake()
    {
        health = GetComponent<Health>();
        health.OnDamaged += health_OnDamaged;
        health.OnDead += health_OnDead;
    }

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        BulletCount = 3;

        velocityAccelerationPerFixedUpdate = Mathf.Clamp(velocityAccelerationPerFixedUpdate, 0.01f, maxRunSpeedOnGround);
        velocityDecelerationPerFixedUpdate = Mathf.Clamp(velocityDecelerationPerFixedUpdate, 0.01f, maxRunSpeedOnGround);
        gravityStrength = -(2 * jumpHeight) / (timeToJumpToHeighest * timeToJumpToHeighest);
        jumpImpulse = Mathf.Abs(gravityStrength) * timeToJumpToHeighest;
        gravityScale = gravityStrength / Physics2D.gravity.y;
        accelerationForceFactor = velocityAccelerationPerFixedUpdate / Time.fixedDeltaTime / maxRunSpeedOnGround;
        decelerationForceFactor = velocityDecelerationPerFixedUpdate / Time.fixedDeltaTime / maxRunSpeedOnGround;
        remainingJumpChances = jumpChances;
        SetGravityScale(gravityScale);

        //Track data of playerdata
        //Initial states
        GlobalAnalysis.cleanData();
        GlobalAnalysis.player_initail_healthpoints = health.CurHealth;

    }

    private void Update()
    {
        if (isTouchingWall)
        {
            Debug.Log("y speed " + body.velocity.y);
        }
        CheckDirection();
        UpdateAnimation();
        CheckSurroundings();
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            Run();
            Jump();
            if (!isWallSliding)
            {
                if (body.velocity.y < 0)
                {
                    SetGravityScale(gravityScale * (movementInput.y < 0 ? fastFallGravityMult : fallGravityMult));
                }
                else
                {
                    SetGravityScale(gravityScale);
                }
            }
            
            ApplyMovement();
        }
        jumpPressed = false;
    }

    /*Funuction used to handle movement in x axis
        @paramter lerpFactor: used to control the target speed, for ground movement, set to 1. 
                        Reserved for future possible circumstances when we want target speed to be a portion of max speed
    */
    private void Run()
    {
        if (velocityDirectionAtJump == 0 && movementInput.x != 0)
        {
            velocityDirectionAtJump = body.velocity.x != 0 ? (int)Mathf.Sign(body.velocity.x) : 0;
        }
        bool sameDirection = velocityDirectionAtJump == Mathf.Sign(movementInput.x);
        float lerpFactor = onGround || sameDirection ? 1 : airMaxSpeedFactor;
        float targetSpeed = movementInput.x * maxRunSpeedOnGround;
        targetSpeed = Mathf.Lerp(body.velocity.x, targetSpeed, lerpFactor);
        float speedDiff = targetSpeed - body.velocity.x;
        float forceFactor = ((speedDiff < 0 && body.velocity.x <= 0) || (speedDiff > 0 && body.velocity.x >= 0)) ?
                                accelerationForceFactor * (onGround || sameDirection ? 1 : airAccelerationFactor) :
                                decelerationForceFactor * (onGround || sameDirection ? 1 : airDecelerationFactor);

        float force = forceFactor * speedDiff;
        body.AddForce(force * Vector2.right, ForceMode2D.Force);
    }

    private void ApplyMovement() 
    {
        if (isWallSliding)
        {
            if (body.velocity.y < -wallSlideSpeed)
            {
                body.velocity = new Vector2(body.velocity.x, -wallSlideSpeed);
            }
        }    
    }

    private void Jump()
    {
        if (jumpPressed && remainingJumpChances > 0)
        {
            body.velocity = new Vector2(body.velocity.x, 0);

            --remainingJumpChances;
            body.AddForce(jumpImpulse * Vector2.up, ForceMode2D.Impulse);
            velocityDirectionAtJump = body.velocity.x != 0 ? (int)Mathf.Sign(body.velocity.x) : 0;
        }

        if (jumpReleased && body.velocity.y > 0)
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y * variableJumpHeightMultiplier);
        }
    }
    private void SetGravityScale(float scale)
    {
        body.gravityScale = scale;
    }

    private void CheckDirection()
    {
        if (movementInput.x != 0)
        {
            if ((movementInput.x > 0) != isFacingRight)
            {
                Turn();
            }
        }
    }

    private void UpdateAnimation()
    {
        animator.SetBool(IsMoving, body.velocity != Vector2.zero);
    }
    
    private void Turn()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void IsWallSliding()
    {
        isWallSliding = (isTouchingWall && !onGround && body.velocity.y <= 0);
    }
    private void CheckSurroundings()
    {
        onGround = IsGrounded();
        if (onGround)
        {
            remainingJumpChances = jumpChances;
        }

        isTouchingWall = IsTouchingWall();
        IsWallSliding();
    }
    public void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        jumpReleased = ctx.canceled;
        jumpPressed = ctx.performed;
    }
    
    public void OnFire(InputAction.CallbackContext ctx)
    {
        animator.SetTrigger("attack");
    }
    
    private bool IsGrounded()
    {
        var bounds = capsuleCollider2D.bounds;
        return Physics2D.CapsuleCast(bounds.center, bounds.size, 0, 0, Vector2.down, .1f, groundLayer);
    }

    private bool IsTouchingWall()
    {
        var bounds = capsuleCollider2D.bounds;
        return Physics2D.CapsuleCast(bounds.center, bounds.size, 0, 0, transform.right, 0.1f, wallLayer);
    }

    public void LockMovement()
    {
        if (IsGrounded())
        {
            body.velocity = new Vector2(0, body.velocity.y);
        }
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }


    
    
    
    

    public void IncreaseBullet()
    {
        BulletCount++;
    }

    public void DecreaseBullet()
    {
        if (BulletCount > 0)
        {
            BulletCount--;
        }
    }

    public int GetBulletCount()
    {
        return BulletCount;
    }

    private void health_OnDamaged(object sender, System.EventArgs e)
    {
        GlobalAnalysis.player_remaining_healthpoints = health.CurHealth;
    }
    private void health_OnDead(object sender, System.EventArgs e)
    {
        animator.SetTrigger("Kill");
        canMove = false;
        Invoke("PlayerDeath", 1f);

        GlobalAnalysis.state = "player_dead";
        AnalysisSender.Instance.postRequest("play_info", GlobalAnalysis.buildPlayInfoData());
        GlobalAnalysis.cleanData();

    }

    public void PlayerDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }




    public int getBulletCount()
    {
        return BulletCount;
    }
}
