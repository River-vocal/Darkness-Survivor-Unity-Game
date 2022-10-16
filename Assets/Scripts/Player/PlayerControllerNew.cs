using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControllerNew : MonoBehaviour
{
    [Header("Movement Settings")] 
    [SerializeField] private int jumpChances = 2;
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
    [SerializeField] private float jumpBufferTime = 0.1f;
    [SerializeField] private float coyoteTime = 0.2f;
    
    [Header("Layer Settings")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header("Check Settings")] 
    [SerializeField] private float wallCheckDistance = 0.2f;
    [SerializeField] private float groundCheckDistance = 0.2f;
    
    [Header("Temporarily serialized")]
    [SerializeField] public bool isFacingRight = true;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isFacingWall;
    [SerializeField] private bool isWallSliding;
    [SerializeField] private float wallSlideSpeed = 2f;
    [SerializeField] private float variableJumpHeightMultiplier = 0.5f;
    [SerializeField] private Vector2 wallJumpDirection = new Vector2(1, 2);
    [SerializeField] private Vector2 wallHopDirection = new Vector2(1, 0.5f);
    [SerializeField] private float wallJumpForce = 10f;
    [SerializeField] private float wallHopForce = 6f;

    //runtime variables
    private Rigidbody2D body;
    private CapsuleCollider2D capsuleCollider2D;
    private Animator animator;
    private Vector2 movementInput;
    //canMove is a lock key altered manually by code from other well, like when you attack, attack code may lock the canMove key
    private bool canMove = true;
    private bool jumpPressed = false;
    private bool jumpReleased = true;
    private int remainingJumpChances;
    private float gravityStrength;
    private float gravityScale;
    private float jumpImpulse;
    private float accelerationForceFactor;
    private float decelerationForceFactor;
    private int xVelocityDirectionAtJump = 0;
    private bool isRunning = false;
    private bool canJump = true;
    private bool inAttackPeriod = false;
    private float jumpBufferTimer;
    private float coyoteTimer;

    //attack related
    public float attackRange = 0.5f;
    public int attackDamage = 10;

    public int BulletCount = 3;
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    
    private static readonly int YVelocity = Animator.StringToHash("yVelocity");

    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
    
    private static readonly int IsWallSliding = Animator.StringToHash("isWallSliding");

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();

        velocityAccelerationPerFixedUpdate = Mathf.Clamp(velocityAccelerationPerFixedUpdate, 0.01f, maxRunSpeedOnGround);
        velocityDecelerationPerFixedUpdate = Mathf.Clamp(velocityDecelerationPerFixedUpdate, 0.01f, maxRunSpeedOnGround);
        gravityStrength = -(2 * jumpHeight) / (timeToJumpToHeighest * timeToJumpToHeighest);
        jumpImpulse = Mathf.Abs(gravityStrength) * timeToJumpToHeighest;
        gravityScale = gravityStrength / Physics2D.gravity.y;
        accelerationForceFactor = velocityAccelerationPerFixedUpdate / Time.fixedDeltaTime / maxRunSpeedOnGround;
        decelerationForceFactor = velocityDecelerationPerFixedUpdate / Time.fixedDeltaTime / maxRunSpeedOnGround;
        remainingJumpChances = jumpChances;
        body.gravityScale = gravityScale;
        wallJumpDirection.Normalize();
        wallHopDirection.Normalize();
        
        //attack related, should remove
        BulletCount = 3;

        //Track data of playerdata
        //Initial states
        GlobalAnalysis.cleanData();
    }

    private void Update()
    {
        //update player runtime status variables, should always be called first
        UpdatePlayerStatus();
        
        //animation related
        UpdateAnimations();
        
        //if jump pressed within jump buffer time
        if (jumpPressed)
        {
            if (!Jump())
            {
                jumpBufferTimer -= Time.deltaTime;
                if (jumpBufferTimer <= 0)
                {
                    jumpPressed = false;
                }
            }
            else
            {
                jumpPressed = false;
            }
        }
    }

    private void UpdatePlayerStatus()
    {
        //don't alter the execution sequence!
        CheckSurroundings();
        
        //reset coyoteTime
        if (isGrounded)
        {
            coyoteTimer = coyoteTime;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }
        
        //check if player needs flip
        if (movementInput.x != 0)
        {
            if ((movementInput.x > 0) != isFacingRight)
            {
                Flip();
            }
        }
        
        //check if player is running
        isRunning = (body.velocity.x != 0);
        
        //check if can jump
        if ((isGrounded && body.velocity.y <= 0) || isWallSliding)
        {
            remainingJumpChances = jumpChances;
        }
        canJump = (remainingJumpChances > 0);
    }
    
    private void CheckSurroundings()
    {
        var bounds = capsuleCollider2D.bounds;
        isGrounded = Physics2D.CapsuleCast(bounds.center, bounds.size, 0, 0, Vector2.down, groundCheckDistance, groundLayer);
        isFacingWall = Physics2D.CapsuleCast(bounds.center, bounds.size, 0, 0, transform.right, wallCheckDistance, wallLayer);

        //when player is facing the wall but still has y speed >0, we don't want to label this as player is sliding down the wall
        isWallSliding = (isFacingWall && !isGrounded && body.velocity.y < 0);
        if (isWallSliding)
        {
            //confused, if not cap x to 0, when player bumps into wall, x speed is always not 0, and is leaving the wall
            body.velocity = new Vector2(0, body.velocity.y);
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    
    private void FixedUpdate()
    {
        InterfereWithMovement();
    }

    //interfere with x axis movement, both in air and on ground, should be called by fixedUpdate
    //this function enables adaptive x axis acceleration and deceleration
    private void Run()
    {
        //when player in sliding on wall or canMove is locked manually, don't interfere
        if (isWallSliding || !canMove)
        {
            return;
        }
        if (xVelocityDirectionAtJump == 0 && movementInput.x != 0)
        {
            xVelocityDirectionAtJump = body.velocity.x != 0 ? (int)Mathf.Sign(body.velocity.x) : 0;
        }
        bool sameDirection = (xVelocityDirectionAtJump == (int)Mathf.Sign(movementInput.x));
        float lerpFactor = isGrounded || sameDirection ? 1 : airMaxSpeedFactor;
        float targetSpeed = movementInput.x * maxRunSpeedOnGround;
        targetSpeed = Mathf.Lerp(body.velocity.x, targetSpeed, lerpFactor);
        var velocity = body.velocity;
        float speedDiff = targetSpeed - velocity.x;
        float forceFactor = ((speedDiff < 0 && velocity.x <= 0) || (speedDiff > 0 && velocity.x >= 0)) ?
                                accelerationForceFactor * (isGrounded || sameDirection ? 1 : airAccelerationFactor) :
                                decelerationForceFactor * (isGrounded || sameDirection ? 1 : airDecelerationFactor);

        float force = forceFactor * speedDiff;
        body.AddForce(force * Vector2.right, ForceMode2D.Force);
    }

    private void InterfereWithMovement() 
    {
        //interfere with x axis
        Run();
        
        //interfere with y axis when player is not grounded, notice that y axis interfere is not locked by canMove 
        if (isGrounded) return;
        //if sliding on wall, cap wall sliding speed
        if (isWallSliding)
        {
            if (body.velocity.y < -wallSlideSpeed)
            {
                // Debug.Log(body.velocity.y);
                body.velocity = new Vector2(body.velocity.x, -wallSlideSpeed);
            }
        }
        //in air
        else
        {
            //moving up
            if (body.velocity.y > 0)
            {
                //reset gravityScale
                body.gravityScale = gravityScale;
                //variable jump height
                if (jumpReleased)
                {
                    body.velocity = new Vector2(body.velocity.x, body.velocity.y * variableJumpHeightMultiplier);
                }
            }
            //moving down
            else
            {
                //if user press down button when falling, make the player fall faster
                body.gravityScale = gravityScale * (movementInput.y < 0 ? fastFallGravityMult : fallGravityMult);
            }
        }
    }

    //jump is now called by update
    private bool Jump()
    {
        if (canJump && canMove)
        {
            --remainingJumpChances;

            //normal jump
            if (!isWallSliding)
            {
                body.velocity = new Vector2(body.velocity.x, 0);
                body.AddForce(jumpImpulse * Vector2.up, ForceMode2D.Impulse);
                xVelocityDirectionAtJump = body.velocity.x != 0 ? (int)Mathf.Sign(body.velocity.x) : 0;
            }
            else
            {
                Debug.Log("aiusdhfiausdhf");
                var force = new Vector2((isFacingRight ? -wallJumpForce : wallJumpForce) * wallJumpDirection.x,
                    wallJumpForce * wallJumpDirection.y);
                body.AddForce(force, ForceMode2D.Impulse);
                
                // //wall hop
                // if (movementInput.x == 0)
                // {
                //     var force = new Vector2(wallHopForce * wallHopDirection.x * (isFacingRight ? -1 : 1),
                //         wallHopForce * wallHopDirection.y);
                //     body.AddForce(force, ForceMode2D.Impulse);
                // }
                // //wall sliding
                // else
                // {
                //     
                //     var force = new Vector2(isFacingRight ? wallJumpForce * wallJumpDirection.x : -wallJumpForce * wallJumpDirection.x,
                //         wallJumpForce * wallJumpDirection.y);
                //     body.AddForce(force, ForceMode2D.Impulse);
                // }
                Flip();
                xVelocityDirectionAtJump = isFacingRight ? 1 : -1;
                isWallSliding = false;
            }

            return true;
        }

        return false;
    }

    //reserved for other system to lock movement, only lock movement in x axis
    public void LockMovement()
    {
        if (isGrounded)
        {
            body.velocity = new Vector2(0, body.velocity.y);
        }
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }

    private void UpdateAnimations()
    {
        animator.SetBool(IsMoving, body.velocity != Vector2.zero);
        animator.SetBool(IsGrounded, isGrounded);
        animator.SetFloat(YVelocity, body.velocity.y);
        animator.SetBool(IsWallSliding, isWallSliding);
    }

    public bool usePressed;
    
    public void OnUse(InputAction.CallbackContext ctx)
    {
        usePressed = ctx.performed && !ctx.canceled;
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        jumpReleased = ctx.canceled;
        jumpPressed = ctx.performed;
        jumpBufferTimer = jumpBufferTime;
    }
    
    public void OnFire(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started)
        {
            animator.SetTrigger("attack");
        }
        
        
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

    // private void health_OnDamaged(object sender, System.EventArgs e)
    // {
    //     GlobalAnalysis.player_remaining_healthpoints = health.CurHealth;
    // }
    // private void health_OnDead(object sender, System.EventArgs e)
    // {
    //     animator.SetTrigger("Kill");
    //     canMove = false;
    //     Invoke("PlayerDeath", 1f);

    //     GlobalAnalysis.state = "player_dead";
    //     AnalysisSender.Instance.postRequest("play_info", GlobalAnalysis.buildPlayInfoData());
    //     GlobalAnalysis.cleanData();

    // }

    public void PlayerDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }




    public int getBulletCount()
    {
        return BulletCount;
    }
}
