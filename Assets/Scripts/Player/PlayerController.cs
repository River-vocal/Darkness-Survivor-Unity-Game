using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float maxRunSpeedOnGround = 10f;
    [SerializeField] private float airMaxSpeedFactor = 0.2f;

    //velocity change per fixedUpdate timeInterval
    [SerializeField] private float velocityAccelerationPerFixedUpdate = 10f;

    //currently set to the same as runAccelerationPerFixedUpdate, reserved for further possible change
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
    private float gravityStrength;
    private float gravityScale;
    private float jumpImpulse;
    private float accelerationForceFactor;
    private float decelerationForceFactor;
    private bool onGround;
    private int velocityDirectionAtJump = 0;

    //attack related
    public float attackRange = 0.5f;
    public int attackDamage = 10;

    public int BulletCount = 3;

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
        SetGravityScale(gravityScale);

        //Track data of playerdata
        //Initial states
        GlobalAnalysis.player_initail_healthpoints = health.CurHealth;

    }

    private void Update()
    {
        onGround = isGrounded();
        animator.SetBool("isMoving", body.velocity != Vector2.zero);
        if (movementInput.x != 0)
        {
            if ((movementInput.x > 0) != isFacingRight)
            {
                Turn();
            }
        }
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            Run();
            Jump();
            if (body.velocity.y < 0)
            {
                SetGravityScale(gravityScale * (movementInput.y < 0 ? fastFallGravityMult : fallGravityMult));
            }
            else
            {
                SetGravityScale(gravityScale);
            }
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

        //Todo:if in air, would multiply forceFactor by a airfactor
        float force = forceFactor * speedDiff;
        body.AddForce(force * Vector2.right, ForceMode2D.Force);
    }

    private void SetGravityScale(float scale)
    {
        body.gravityScale = scale;
    }
    private void Jump()
    {
        if (jumpPressed && onGround)
        {
            body.AddForce(jumpImpulse * Vector2.up, ForceMode2D.Impulse);
            velocityDirectionAtJump = body.velocity.x != 0 ? (int)Mathf.Sign(body.velocity.x) : 0;
        }
    }

    private void Turn()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    private bool isGrounded()
    {
        return Physics2D.CapsuleCast(capsuleCollider2D.bounds.center, capsuleCollider2D.bounds.size, 0, 0, Vector2.down, .1f, groundLayer);
    }

    private bool isFacingWall()
    {
        return Physics2D.CapsuleCast(capsuleCollider2D.bounds.center, capsuleCollider2D.bounds.size, 0, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
    }
    void OnJump()
    {
        jumpPressed = true;
    }

    void OnFire()
    {
        animator.SetTrigger("attack");
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


    public void LockMovement()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(0, body.velocity.y);
        }
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }

    public int getBulletCount()
    {
        return BulletCount;
    }
}
