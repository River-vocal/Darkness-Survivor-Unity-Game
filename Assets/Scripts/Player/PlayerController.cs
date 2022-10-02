using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxRunSpeedOnGround;

    //velocity change per fixedUpdate timeInterval
    [SerializeField] private float velocityAccelerationPerFixedUpdate;

    //currently set to the same as runAccelerationPerFixedUpdate, reserved for further possible change
    [SerializeField] private float velocityDecelerationPerFixedUpdate;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float timeToJumpToHeighest;
    public bool isFacingRight;


    // [SerializeField] private float airMoveSpeed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Vector2 movementInput;
    private bool canMove;
    private bool jumpPressed;
    private float gravityStrength;
    private float gravityScale;
    private float jumpImpulse;
    private float accelerationForceFactor;
    private float decelerationForceFactor;

    //plan to move out of controller
    [SerializeField] private int maxHealth;
    private int currentHealth;
    [SerializeField] private HealthBar healthBar;

    //todo
    // bool isJumping = false;
    private Rigidbody2D body;
    private CapsuleCollider2D capsuleCollider2D;
    private Animator animator;
    
    public float attackRange;
    public int attackDamage;

    public Datas playerdata = new Datas();

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 20;
        attackRange = 0.5f;
        attackDamage = 10;
        canMove = true;
        isFacingRight = true;
        maxRunSpeedOnGround = 10;
        velocityAccelerationPerFixedUpdate = 10;
        velocityDecelerationPerFixedUpdate = 20;
        jumpHeight = 10;
        timeToJumpToHeighest = 1f;
        jumpPressed = false;

        velocityAccelerationPerFixedUpdate = Mathf.Clamp(velocityAccelerationPerFixedUpdate, 0.01f, maxRunSpeedOnGround);
        velocityDecelerationPerFixedUpdate = Mathf.Clamp(velocityDecelerationPerFixedUpdate, 0.01f, maxRunSpeedOnGround);
        gravityStrength = -(2 * jumpHeight) / (timeToJumpToHeighest * timeToJumpToHeighest);
        jumpImpulse = Mathf.Abs(gravityStrength) * timeToJumpToHeighest;
        gravityScale = gravityStrength / Physics2D.gravity.y;
        accelerationForceFactor = velocityAccelerationPerFixedUpdate / Time.fixedDeltaTime / maxRunSpeedOnGround;
        decelerationForceFactor = velocityDecelerationPerFixedUpdate / Time.fixedDeltaTime / maxRunSpeedOnGround;

        
        //Todo: plan to move
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);

        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();


        //Track data of playerdata
        //Initial states
        GlobalAnalysis.player_remaining_healthpoints = currentHealth;
        playerdata.level = "player_1";
        playerdata.num_players = 1;
        playerdata.num_bosses = 1;
        playerdata.state = "start";
        playerdata.timestamp = GlobalAnalysis.getTimeStamp();
        playerdata.player_remaining_healthpoints = currentHealth;
        playerdata.boss_remaining_healthpoints = GlobalAnalysis.boss_remaining_healthpoints;
        string json = JsonUtility.ToJson(playerdata);

        StartCoroutine(GlobalAnalysis.postRequest("test", json));
    }

    private void Update() {
        if (body.velocity != Vector2.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        //not a good idea to use spriteRenderer.flipX to flip, see https://forum.unity.com/threads/flip-x-or-scale-x.1042324/
        if (movementInput.x != 0) {
            if ((movementInput.x > 0) != isFacingRight) {
                Turn();
            }
        }
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            Run(1);
            Jump();
        }
        jumpPressed = false;
    }

    /*Funuction used to handle movement in x axis
        @paramter lerpFactor: used to control the target speed, for ground movement, set to 1. 
                        Reserved for future possible circumstances when we want target speed to be a portion of max speed
    */                  
    private void Run(float lerpFactor) {
        float targetSpeed = movementInput.x * maxRunSpeedOnGround;
        targetSpeed = Mathf.Lerp(body.velocity.x, targetSpeed, lerpFactor);
        float speedDiff = targetSpeed - body.velocity.x;
        float forceFactor = ((speedDiff < 0 && body.velocity.x <= 0) || (speedDiff > 0 && body.velocity.x >= 0)) ? accelerationForceFactor : decelerationForceFactor;
        //Todo:if in air, would multiply forceFactor by a airfactor
        float force = forceFactor * speedDiff;
        body.AddForce(force * Vector2.right, ForceMode2D.Force);
    }

    private void Jump() {
        if (jumpPressed && isGrounded()) {
            body.AddForce(jumpImpulse * Vector2.up, ForceMode2D.Impulse);
        }
    }

    private void Turn() {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        isFacingRight = !isFacingRight;
    }
    void OnMove(InputValue movementValue)
    {
        movementInput = new Vector2(movementValue.Get<Vector2>().x, 0);
    }

    void OnJump()
    {
        jumpPressed = true;
    }

    void OnFire()
    {
        animator.SetTrigger("attack");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
        GlobalAnalysis.player_remaining_healthpoints = currentHealth;

        if (currentHealth <= 0)
        {
            animator.SetTrigger("Kill");
            canMove = false;
            Invoke("PlayerDeath", 1f);

            
            playerdata.level = "1";
            playerdata.num_players = 1;
            playerdata.num_bosses = 1;
            playerdata.state = "end";
            playerdata.timestamp = GlobalAnalysis.getTimeStamp();
            playerdata.player_remaining_healthpoints = currentHealth;
            playerdata.boss_remaining_healthpoints = GlobalAnalysis.boss_remaining_healthpoints;
            string json = JsonUtility.ToJson(playerdata);

            StartCoroutine(GlobalAnalysis.postRequest("test", json));
        }
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
    private bool isGrounded()
    {
        return Physics2D.CapsuleCast(capsuleCollider2D.bounds.center, capsuleCollider2D.bounds.size, 0, 0, Vector2.down, .1f, groundLayer);
    }

    private bool isFacingWall()
    {
        return Physics2D.CapsuleCast(capsuleCollider2D.bounds.center, capsuleCollider2D.bounds.size, 0, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
    }

    void takeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
    }
}
