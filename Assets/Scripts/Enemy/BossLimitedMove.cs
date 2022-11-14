using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossLimitedMove : StateMachineBehaviour
{
    Transform playerTransform;
    Transform bossTransform;
    Rigidbody2D rb2;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float attackRange = 2.5f;


    
    // [SerializeField] private float
        // groundCheckDistance,
        // wallCheckDistance;

    // [SerializeField]
    // private Transform
        // groundCheck, wallCheck;

    // [SerializeField]
    // private LayerMask whatIsGround;

    // private Vector2 movement;

    // private bool groundDetected;
    // private bool wallDetected;
    
    
    Transform leftEdge;
    Transform rightEdge;
    private bool movingLeft = true;

    private Boss boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        bossTransform = animator.GetComponent<Transform>();
        leftEdge = GameObject.Find("LeftEdge").transform;
        rightEdge = GameObject.Find("RightEdge").transform;
        rb2 = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
        
        movingLeft = !boss.bossIsFlipped;
    }


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //TODO: add y axis constraint
        
        if (Math.Abs(playerTransform.position.y - rb2.position.y) <= 3 && Math.Abs(playerTransform.position.x - rb2.position.x) <= attackRange)
        {
            boss.lookAtPlayer();
            movingLeft = !boss.bossIsFlipped;
            
            animator.SetTrigger("Attack");
            
            // Vector2 targetPosition = new Vector2(playerTransform.position.x, rb2.position.y);
            // Vector2 newPosition = Vector2.MoveTowards(rb2.position, targetPosition, speed * Time.deltaTime);
            // rb2.MovePosition(newPosition);

            return;
        }
        
        /*
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, whatIsGround);
        
        if (!groundDetected || wallDetected)
        {
            boss.DirectionChange();
            
        }
        else
        {
            Vector2 targetPosition = new Vector2(rb2.position.x + wallCheckDistance, rb2.position.y);
            Vector2 newPosition = Vector2.MoveTowards(rb2.position, targetPosition, speed * Time.deltaTime);
            rb2.MovePosition(newPosition);
        }
        */
        
        
        if (movingLeft)
        {
            // (1.0 == 10.0 / 10.0) might not return true every time.
            if (Mathf.Approximately(bossTransform.position.x, leftEdge.position.x))
            {
                boss.DirectionChange();
                // animator.ResetTrigger("FireballAttack");
                // animator.ResetTrigger("RegularAttack");
                movingLeft = false;
            }
            else
            {
                Vector2 targetPosition = new Vector2(leftEdge.position.x, rb2.position.y);
                Vector2 newPosition = Vector2.MoveTowards(rb2.position, targetPosition, speed * Time.deltaTime);
                rb2.MovePosition(newPosition);
            }
        }
        else
        {
            if (Mathf.Approximately(bossTransform.position.x, rightEdge.position.x))
            {
                boss.DirectionChange();
                // animator.ResetTrigger("FireballAttack");
                // animator.ResetTrigger("RegularAttack");
                movingLeft = true;
            }
            else
            {
                Vector2 targetPosition = new Vector2(rightEdge.position.x, rb2.position.y);
                Vector2 newPosition = Vector2.MoveTowards(rb2.position, targetPosition, speed * Time.deltaTime);
                rb2.MovePosition(newPosition);
            }
        }
        

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        // animator.ResetTrigger("FireballAttack");
        // animator.ResetTrigger("RegularAttack");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}