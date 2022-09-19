using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float jumpSpeed = 10f;
    [SerializeField] private float leftBoundary;
    [SerializeField] private float rightBoundary;
    // public float collisionOffset = 0.05f;
    // public ContactFilter2D movementFilter;
    public int maxHealth = 20;
    public HealthBar healthBar;
    public int currentHealth;
    Vector2 movementInput;
    bool jumpPressed;
    //todo
    // bool isJumping = false;
    Rigidbody2D rb;
    CapsuleCollider2D capsuleCollider2D;
    Animator animator;
    SpriteRenderer sr;
    [SerializeField] private LayerMask jumpableGround;
    // List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public SwordAttack swordAttack;
    bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    // // Update is called once per frame
    // void Update()
    // {

    // }
    void takeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(movementInput.x * moveSpeed, rb.velocity.y);
            if (jumpPressed)
            {
                // isJumping = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                jumpPressed = false;
            }
            if (rb.velocity != Vector2.zero)
            {
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
            if (rb.velocity.x > 0)
            {
                sr.flipX = false;
            }
            else if (rb.velocity.x < 0)
            {
                sr.flipX = true;
            }

        }
        float predictX = rb.position.x + rb.velocity.x * Time.fixedDeltaTime;
        if (predictX < leftBoundary || predictX > rightBoundary)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        // if (!canMove) return;
        // if (movementInput != Vector2.zero) {
        //     bool success = TryMove(movementInput);
        //     if (!success) {
        //         success = TryMove(new Vector2(movementInput.x, 0));
        //     }
        //     if (!success) {
        //         success = TryMove(new Vector2(0, movementInput.y));
        //     }
        //     animator.SetBool("isMoving", success);
        // }
        // else {
        //     animator.SetBool("isMoving", false);
        // }

        // if (movementInput.x < 0) {
        //     sr.flipX = true;
        // }
        // else if (movementInput.x > 0) {
        //     sr.flipX = false;
        // }
    }

    // private bool TryMove(Vector2 direction) {
    //     if (direction == Vector2.zero) return false;
    //     int count = rb.Cast(direction, movementFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + collisionOffset);
    //     if (count == 0) {
    //         rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    //         return true;
    //     }
    //     return false;
    // }

    void OnMove(InputValue movementValue)
    {
        movementInput = new Vector2(movementValue.Get<Vector2>().x, 0);
    }

    void OnJump()
    {
        if (isGrounded())
        {
            jumpPressed = true;
        }
    }

    void OnFire()
    {
        animator.SetTrigger("swordAttack");
    }

    public void SwordAttack()
    {
        LockMovement();
        if (sr.flipX)
        {
            swordAttack.AttackLeft();
        }
        else
        {
            swordAttack.AttackRight();
        }
    }
    public void EndSwordAttack()
    {
        UnlockMovement();
        swordAttack.StopAttack();
    }
    public void LockMovement()
    {
        if (isGrounded())
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }
    private bool isGrounded()
    {
        return Physics2D.CapsuleCast(capsuleCollider2D.bounds.center, capsuleCollider2D.bounds.size, 0, 0, Vector2.down, .1f, jumpableGround);
    }
}
