using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float groundMoveSpeed = 7f;
    [SerializeField] private float airMoveSpeed;
    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Vector2 movementInput;
    private bool canMove = true;
    private bool jumpPressed;


    //plan to move out of controller
    [SerializeField] private int maxHealth = 20;
    private int currentHealth;
    [SerializeField] private HealthBar healthBar;

    //todo
    // bool isJumping = false;
    private Rigidbody2D body;
    private Vector3 originalLocalScale;
    private CapsuleCollider2D capsuleCollider2D;
    private Animator animator;
    
    public float attackRange = 0.5f;
    public int attackDamage = 10;
    public bool isFaceRight = true;

    public Datas playerdata = new Datas();
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);

        //currently not working
        airMoveSpeed = groundMoveSpeed / 5;
        originalLocalScale = transform.localScale;
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


    private void FixedUpdate()
    {
        float xSpeed = body.velocity.x;
        float ySpeed = body.velocity.y;

        if (canMove)
        {
            bool grounded = isGrounded();
            bool onWall = isOnWall();

            //player on ground
            if (grounded)
            {
                xSpeed = groundMoveSpeed * movementInput.x;
                if (jumpPressed)
                {
                    ySpeed = jumpSpeed;
                }
            }
            //player not on ground but attaching to wall
            else if (onWall)
            {
                xSpeed = 0f;
            }
            //player not on ground and not attaching to wall
            else
            {
                if (Mathf.Sign(xSpeed) == movementInput.x)
                {
                    // Debug.Log("moving at ground speed in air");
                    xSpeed = groundMoveSpeed * movementInput.x;
                }
                else
                {
                    // Debug.Log("moving at air speed in air");
                    xSpeed = airMoveSpeed * movementInput.x;
                }
            }
        }

        //whenever jumpPressed is set to true, it's always consumed, whenever consumed, set to false. So we can always set it to false
        jumpPressed = false;
        body.velocity = new Vector2(xSpeed, ySpeed);
        if (body.velocity != Vector2.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        //not a good idea to use spriteRenderer.flipX to flip, see https://forum.unity.com/threads/flip-x-or-scale-x.1042324/
        if (movementInput.x > 0)
        {
            transform.localScale = originalLocalScale;
            isFaceRight = true;
        }
        else if (movementInput.x < 0)
        {
            var tmp = originalLocalScale;
            tmp.x *= -1;
            transform.localScale = tmp;
            isFaceRight = false;
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = new Vector2(movementValue.Get<Vector2>().x, 0);
    }

    void OnJump()
    {
        Debug.Log("fauisdhgnilaushfiwef");
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

        DamagePopupManager.Create(damage, transform.position, 0);
        
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

    private bool isOnWall()
    {
        return Physics2D.CapsuleCast(capsuleCollider2D.bounds.center, capsuleCollider2D.bounds.size, 0, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
    }

    void takeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
    }
}
