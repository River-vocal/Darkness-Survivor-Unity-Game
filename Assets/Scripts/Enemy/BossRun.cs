using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossRun : StateMachineBehaviour
{
    Transform playerTransform;
    Transform bossTransform;
    Rigidbody2D rb2;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float attackRange = 2.5f;
    private Boss boss;

    private TwoBoss currentBoss;
    private GameObject boss1;
    private GameObject boss2;
    private float bossMoveRange = 1.8f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        bossTransform = animator.GetComponent<Transform>();
        rb2 = animator.GetComponent<Rigidbody2D>();
        if (animator.name == "Boss") {
            boss = animator.GetComponent<Boss>();
        } else {
            currentBoss = animator.GetComponent<TwoBoss>();
            boss1 = GameObject.Find("Boss1");
            boss2 = GameObject.Find("Boss2");
            attackRange = 1.8f;
        }
    }

    
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.name == "Boss") {
            boss.lookAtPlayer();
        } 
        else {
            currentBoss.lookAtPlayer();
        }
        
        if (Math.Abs(playerTransform.position.x - rb2.position.x) <= attackRange)
        {
            animator.SetTrigger("Attack");
            return;
        }
        Vector2 targetPosition = new Vector2(playerTransform.position.x, rb2.position.y);
        Vector2 newPosition = Vector2.MoveTowards(rb2.position, targetPosition, speed * Time.deltaTime);
        if (animator.name == "Boss") {
            rb2.MovePosition(newPosition);
        } 
        else {
            if (animator.name == "Boss1") {
                if (boss2 == null || !boss2.activeSelf) {
                    rb2.MovePosition(newPosition);
                }
                else if (Math.Abs(newPosition.x - boss2.transform.position.x) > bossMoveRange) {
                    rb2.MovePosition(newPosition);
                }
            } 
            else if (animator.name == "Boss2") {
                if (boss1 == null || !boss1.activeSelf) {
                    rb2.MovePosition(newPosition);
                }
                else if (Math.Abs(newPosition.x - boss1.transform.position.x) > bossMoveRange) {
                    rb2.MovePosition(newPosition);
                }
            }
        }
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
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
